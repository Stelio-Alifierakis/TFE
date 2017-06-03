using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using GUI.Model.login;
using GUI.ViewModel;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            //Create a custom principal with an anonymous identity at startup
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            //Show the login view
            AuthentificationViewModel viewModel = new AuthentificationViewModel(new AuthenticationService());
            IView loginWindow = new connection(viewModel);
            loginWindow.Show();
        }
    }
}
