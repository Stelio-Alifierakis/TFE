using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using GUI.Vues;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        UserControl currentPage;

        public Main()
        {
            InitializeComponent();
            Init();
            switcher.Switcher.main = this;
            switcher.Switcher.Switch(new Accueil());
        }

        public void Init()
        {
            ControlesBoutons contr = new ControlesBoutons();
            controlBase.Children.Add(contr);
        }

        public void Navigation(UserControl nextPage)
        {
            contenuPrincipal.Children.Remove(currentPage);

            currentPage = nextPage;

            contenuPrincipal.Children.Add(currentPage);
        }

        public void Navigation(UserControl nextPage, object etat)
        {
            contenuPrincipal.Children.Remove(currentPage);

            currentPage = nextPage;

            contenuPrincipal.Children.Add(currentPage);

            switcher.IsSwitchable s = nextPage as switcher.IsSwitchable;

            if (s != null)
            {
                s.Etat(etat);
            }
            else
            {
                throw new ArgumentException("La prochaine page n'est pas switchable "+ nextPage.Name.ToString());
            }
        }
    }
}
