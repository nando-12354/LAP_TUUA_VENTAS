using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.ARCHIVAMIENTO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (PrevInstance())
            {
                MessageBox.Show("Existe otra instancia de la aplicacion de Archivamiento.");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string strpath = AppDomain.CurrentDomain.BaseDirectory + "resources/";
            if (!ErrorHandler.CargarErrorTypes(strpath))
            {
                MessageBox.Show(ErrorHandler.Desc_Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Property.CargarPropiedades(strpath))
            {
                MessageBox.Show(ErrorHandler.Desc_Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!LabelConfig.LoadData())
            {
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_008])["MESSAGE"];
                MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Property.cargarSPConfig(AppDomain.CurrentDomain.BaseDirectory);

            try
            {
                //Application.AddMessageFilter(new MessageFilter());
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                ErrorHandler.Trace(Define.ERR_DEFAULT, "Main: ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                MessageBox.Show(Define.ERR_DEFAULT, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool PrevInstance()
        {
            int intNumProc = System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length;
            if (intNumProc > 1)
                return true;
            else
                return false;
        }

    }
}
