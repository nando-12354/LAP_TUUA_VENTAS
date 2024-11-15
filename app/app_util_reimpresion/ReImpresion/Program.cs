using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LAP.TUUA.UTIL;

namespace REImpresion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Property.cargarSPConfig(AppDomain.CurrentDomain.BaseDirectory);

            Application.Run(new frmREImpresion());
        }
    }
}
