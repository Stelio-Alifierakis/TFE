﻿using System;
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

using GUI.ViewModel;

namespace GUI
{
    /// <summary>
    /// Interaction logic for connection.xaml
    /// </summary>
    public partial class connection : Window
    {
        Com com;

        public connection()
        {
            InitializeComponent();
            com = new Com();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = txtBx_Identifiant.Text;
            string mdp = txtBx_Mdp.Password;
            //com.testCom();
            com.LancementAuth(login, mdp);
        }
    }
}
