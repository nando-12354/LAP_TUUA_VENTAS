using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using System.IO;
using System.Reflection;

namespace Test
{
    public partial class Form1 : Form
    {
        BO_Alarmas oBO_Alarmas;

        public Form1()
        {
            InitializeComponent();

            try
            {
                string strPath = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "resources/");

                if (!ErrorHandler.CargarErrorTypes(strPath))
                {
                    //oLog.WriteLine("Err(Typ): " + ErrorHandler.Desc_Mensaje);
                }

                if (!Property.CargarPropiedades(strPath))
                {
                    //oLog.WriteLine("Err(Prp): " + ErrorHandler.Desc_Mensaje);
                }
                //oLog.WriteLine("4: " + System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Property.htProperty.Add("PATHRECURSOS", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                //oLog.WriteLine("5");
                oBO_Alarmas = new BO_Alarmas();

                oBO_Alarmas.EnviarCorreo("1");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
