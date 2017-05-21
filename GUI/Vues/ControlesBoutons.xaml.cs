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

namespace GUI.Vues
{
    /// <summary>
    /// Interaction logic for ControlesBoutons.xaml
    /// </summary>
    public partial class ControlesBoutons : UserControl
    {
        public ControlesBoutons()
        {
            InitializeComponent();
        }

        private void Bouton1_Click(object sender, RoutedEventArgs e)
        {
            switcher.Switcher.Switch(new Accueil());
        }

        private void Bouton2_Click(object sender, RoutedEventArgs e)
        {
            switcher.Switcher.Switch(new GestionUsers());
        }
    }
}
