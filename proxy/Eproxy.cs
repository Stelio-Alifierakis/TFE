using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

using BaseDonnees.Models;
using Rechercheur;

namespace proxy
{
    /// <summary>
    /// Interface qui sert de contrat à la classe de proxy
    /// </summary>
    public class Eproxy : IEproxy
    {
        /// <summary>
        /// Variable qui contient le serveur proxy
        /// </summary>
        private ProxyServer proxyServer;

        /// <summary>
        /// Dictionnaire qui stocke les requestbody
        /// </summary>
        private Dictionary<Guid, string> requestBodyHistory;

        /// <summary>
        /// Dictionnaire qui va stocker si un site a été décrété valide ou non via l'URL
        /// </summary>
        private Dictionary<string, int> dicoValidSite;

        /// <summary>
        /// Variable qui stocke le rechercheur
        /// </summary>
        private IRechercheur r;

        /// <summary>
        /// Valeur booléenne qui va activer la recherche sur le contenu des réponses
        /// </summary>
        private bool activationRechercheContenu = true;

        private Activitateur actifRechercheContenu;

        /// <summary>
        /// Valeur booléenne qui va activer la recherche sur le contenu de l'URL
        /// </summary>
        private bool activationRechercheUrl = true;

        private Activitateur actifRechercheURL;

        /// <summary>
        /// Message affiché lors des blocages
        /// </summary>
        private readonly string messageBlocage = "<!DOCTYPE html>" +
                      "<html><body><h1>" +
                      "Website Blocked" +
                      "</h1>" +
                      "<p>N'Dèye VERMONT et Stelio ALIFIERAKIS ont bloqué cette page !!!</p>";

        /// <summary>
        /// Constructeur.
        /// Initialise le proxy et le dictionnaire
        /// </summary>
        /// <see cref="ProxyServer"/>
        public Eproxy()
        {
            actifRechercheContenu = new Activitateur(true);
            actifRechercheURL = new Activitateur(true);

            proxyServer = new ProxyServer();
            proxyServer.TrustRootCertificate = true;
            requestBodyHistory = new Dictionary<Guid, string>();
            dicoValidSite = new Dictionary<string, int>();

            actifRechercheContenu.actif = true;
            actifRechercheURL.actif = true;

            Console.WriteLine(activationRechercheContenu + " " + activationRechercheUrl);     
        }

        /// <summary>
        /// Initialise le rechercheur
        /// </summary>
        /// <param name="r"></param>
        public void setRechercheur(IRechercheur r)
        {
            this.r = r;
        }

        /// <summary>
        /// Fonction qui va démarrer le proxy.
        /// Elle va ajouter des évènement pour les requêtes et réponses HTTP
        /// Elle va ajouter des certificat pour le HTTPS
        /// </summary>
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

        /// <summary>
        /// Fonction qui va stopper le proxy
        /// Elle va libérer les évènements sur les requêtes et réponses HTTP
        /// Elle va libérer les certificats
        /// </summary>
        public void Stop()
        {
            proxyServer.BeforeRequest -= OnRequest;
            proxyServer.BeforeResponse -= OnResponse;
            proxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;
            proxyServer.ClientCertificateSelectionCallback -= OnCertificateSelection;

            proxyServer.Stop();
        }

        /// <summary>
        /// Fonction évènementielle qui se déclenchera à chaque requête
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>Réponse</returns>
        public async Task OnRequest(object sender, SessionEventArgs e)
        {
            //Console.WriteLine("requête " + e.WebSession.Request.RequestUri);

            var requestHeaders = e.WebSession.Request.RequestHeaders;

            int lockSiteUrl = r.checkUrl(e.WebSession.Request.RequestUri.AbsoluteUri);

            dicoValidSite.Add(e.WebSession.Request.RequestUri.AbsoluteUri, lockSiteUrl);

            Console.WriteLine("Validation URL : " + lockSiteUrl + " -----------------> " + e.WebSession.Request.RequestUri.AbsoluteUri);

            var method = e.WebSession.Request.Method.ToUpper();
            if ((method == "POST" || method == "PUT" || method == "PATCH"))
            {
                if (actifRechercheURL.actif && lockSiteUrl == 0)
                {
                    //Get/Set request body bytes
                    byte[] bodyBytes = await e.GetRequestBody();
                    await e.SetRequestBody(bodyBytes);

                    //Get/Set request body as string
                    string bodyString = await e.GetRequestBodyAsString();
                    await e.SetRequestBodyString(bodyString);

                    requestBodyHistory[e.Id] = bodyString;
                }
                else
                {
                    await e.Ok(messageBlocage +
                      "<p>Raison : Site dans les listes bloquantes</p>" +
                      "</body>" +
                      "</html>");
                }
            }
            //Console.WriteLine("Analyse de l'en-tête " + e.WebSession.Request.RequestUri.AbsoluteUri);
        }

        /// <summary>
        /// Fonction évènementielle qui se déclenchera à chaque réponses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task OnResponse(object sender, SessionEventArgs e)
        {
            int validSite = 2;

            //Console.WriteLine("requête " + e.WebSession.Request.RequestUri.AbsoluteUri);

            if (actifRechercheContenu.actif && dicoValidSite.ContainsKey(e.WebSession.Request.RequestUri.AbsoluteUri))
            {
                validSite = dicoValidSite[e.WebSession.Request.RequestUri.AbsoluteUri];
            }

            /*if (activationRechercheUrl && r.checkUrl(e.WebSession.Request.RequestUri.AbsoluteUri) == 1)
            {
                validSite = 1;
            }
            else if (activationRechercheUrl && r.checkUrl(e.WebSession.Request.RequestUri.AbsoluteUri) == 0)
            {
                validSite = 0;
            }*/

            //Console.WriteLine("Validation URL : " + validSite + " -----------------> " + e.WebSession.Request.RequestUri.AbsoluteUri);

            if (actifRechercheContenu.actif && validSite == 0)
            {
                await e.Ok(messageBlocage +
                      "<p>Raison : Site dans les listes bloquantes</p>" +
                      "</body>" +
                      "</html>");
            }
            else
            {
                Console.WriteLine("J'arrive ici");
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

                            Console.WriteLine("Analyse de la réponse " + e.WebSession.Request.RequestUri.AbsoluteUri + " réponse " + e.WebSession.Response.ResponseStatusCode);
                        }
                    }
                    else
                    {
                        Console.WriteLine(" erreur " + e.WebSession.Response.ResponseStatusCode);
                    }
                }
            }
        }

        /// <summary>
        /// Fonction évènementielle qui gère la validation de certificat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                e.IsValid = true;
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Fonction évènementielle qui gère la sélection de certificat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            return Task.FromResult(0);
        }

        public Activitateur retourActifURL()
        {
            //throw new NotImplementedException();
            return actifRechercheContenu;
        }

        public Activitateur retourActifContenu()
        {
            //throw new NotImplementedException();
            return actifRechercheContenu;
        }
    }
}
