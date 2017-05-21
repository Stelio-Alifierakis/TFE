using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace GUI.switcher
{
    public static class Switcher
    {
        public static Main main;

        public static void Switch(UserControl control)
        {
            main.Navigation(control);
        }

        public static void Switch(UserControl control, object etat)
        {
            main.Navigation(control, etat);
        }

    }
}
