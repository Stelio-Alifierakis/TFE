using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

using ProxyNavigateur.Models;
using Rechercheur;

namespace proxy
{
    public class Eproxy : IEproxy
    {
        private ProxyServer proxyServer;
        private Dictionary<Guid, string> requestBodyHistory;
        private Rechercheur.Rechercheur r;
        private bool activationRechercheContenu = true;
        private bool activationRechercheUrl = true;

        private const string messageBlocage = "<!DOCTYPE html>" +
                      "<html><body><h1>" +
                      "Website Blocked" +
                      "</h1>" +
                      "<p>N'Dèye VERMONT et Stelio ALIFIERAKIS ont bloqué cette page !!!</p>";

        public Eproxy()
        {
            proxyServer = new ProxyServer();
            proxyServer.TrustRootCertificate = true;
            requestBodyHistory = new Dictionary<Guid, string>();
            Console.WriteLine(activationRechercheContenu + " " + activationRechercheUrl);     
        }

        public void setRechercheur(Rechercheur.Rechercheur r)
        {
            this.r = r;
        }

        public void StartProxy()
        {
            proxyServer.BeforeRequest += OnRequest;
            proxyServer.BeforeResponse += OnResponse;
            proxyServer.ServerCertificateValidationCallback += OnCertificateValidation;
            proxyServer.ClientCertificateSelectionCallback += OnCertificateSelection;

            var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true)
            {
            };

            proxyServer.AddEndPoint(explicitEndPoint);
            proxyServer.Start();

            var transparentEndPoint = new TransparentProxyEndPoint(IPAddress.Any, 8001, true)
            {
                GenericCertificateName = "google.com"
            };
            proxyServer.AddEndPoint(transparentEndPoint);

            proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);
        }

        public void Stop()
        {
            proxyServer.BeforeRequest -= OnRequest;
            proxyServer.BeforeResponse -= OnResponse;
            proxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;
            proxyServer.ClientCertificateSelectionCallback -= OnCertificateSelection;

            proxyServer.Stop();
        }

        public async Task OnRequest(object sender, SessionEventArgs e)
        {
            //Console.WriteLine("requête " + e.WebSession.Request.RequestUri);

            var requestHeaders = e.WebSession.Request.RequestHeaders;

            var method = e.WebSession.Request.Method.ToUpper();
            if ((method == "POST" || method == "PUT" || method == "PATCH"))
            {
                //Get/Set request body bytes
                byte[] bodyBytes = await e.GetRequestBody();
                await e.SetRequestBody(bodyBytes);

                //Get/Set request body as string
                string bodyString = await e.GetRequestBodyAsString();
                await e.SetRequestBodyString(bodyString);

                requestBodyHistory[e.Id] = bodyString;
            }
            //Console.WriteLine("Analyse de l'en-tête " + e.WebSession.Request.RequestUri.AbsoluteUri);
        }

        public async Task OnResponse(object sender, SessionEventArgs e)
        {
            int validSite = 2;

            //Console.WriteLine("requête " + e.WebSession.Request.RequestUri.AbsoluteUri);

            if (activationRechercheUrl && r.checkUrl(e.WebSession.Request.RequestUri.AbsoluteUri) == 1)
            {
                validSite = 1;
            }
            else if (activationRechercheUrl && r.checkUrl(e.WebSession.Request.RequestUri.AbsoluteUri) == 0)
            {
                validSite = 0;
            }

            Console.WriteLine("Validation URL : " + validSite + " -----------------> " + e.WebSession.Request.RequestUri.AbsoluteUri);

            if (validSite == 0)
            {
                await e.Ok(messageBlocage +
                      "<p>Raison : Site dans les listes bloquantes</p>" +
                      "</body>" +
                      "</html>");
            }
            else
            {
                if (requestBodyHistory.ContainsKey(e.Id))
                {
                    //access request body by looking up the shared dictionary using requestId
                    var requestBody = requestBodyHistory[e.Id];
                }

                var responseHeaders = e.WebSession.Response.ResponseHeaders;

                if (e.WebSession.Request.Method == "GET" || e.WebSession.Request.Method == "POST")
                {
                    if (e.WebSession.Response.ResponseStatusCode == "200")
                    {
                        if (e.WebSession.Response.ContentType != null && e.WebSession.Response.ContentType.Trim().ToLower().Contains("text/html"))
                        {
                            byte[] bodyBytes = await e.GetResponseBody();
                            await e.SetResponseBody(bodyBytes);

                            string body = await e.GetResponseBodyAsString();

                            if (activationRechercheContenu && validSite != 1 && r.checkUrl(e.WebSession.Request.RequestUri.AbsoluteUri) != 1 && r.valPhrase(body) > 20)
                            {
                                string theme = r.themePage(body);
                                await e.SetResponseBodyString(messageBlocage +
                                "Raison : " + theme +
                                "</body>" +
                                "</html>");
                            }
                            else
                            {
                                await e.SetResponseBodyString(body);
                            }

                            //Console.WriteLine("Analyse de la réponse " + e.WebSession.Request.RequestUri.AbsoluteUri + " réponse " + e.WebSession.Response.ResponseStatusCode);
                        }
                    }
                    else
                    {
                        Console.WriteLine(" erreur " + e.WebSession.Response.ResponseStatusCode);
                    }
                }
            }
        }

        public Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                e.IsValid = true;
            }
            return Task.FromResult(0);
        }

        public Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            return Task.FromResult(0);
        }
    }
}
