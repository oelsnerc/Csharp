using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Calc_Mobile
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new Form_Main());
        }
    }
}