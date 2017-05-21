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
using System.Windows.Navigation;
using System.Windows.Shapes;

using GUI.switcher;

namespace GUI.Vues
{
    /// <summary>
    /// Interaction logic for Accueil.xaml
    /// </summary>
    public partial class Accueil : UserControl, IsSwitchable
    {
        public Accueil()
        {
            InitializeComponent();
        }

        public void Etat(object etat)
        {
            throw new NotImplementedException();
        }
    }
}
