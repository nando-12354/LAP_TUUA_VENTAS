using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace AppVuelos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool Valido()
        {
            if (tbxVuelo.Text == "")
            {
                erpVuelo.SetError(tbxVuelo, "Ingrese Numero de Vuelo");
                return false;
            }
            if (tbxVuelo.Text.Length < 5)
            {
                erpVuelo.SetError(tbxVuelo, "Numero de Vuelo Inválido");
                return false;
            }
            if (!(tbxVuelo.Text.Substring(0, 2).ToUpper() == "LA" || tbxVuelo.Text.Substring(0, 2).ToUpper() == "LP"))
            {
                erpVuelo.SetError(tbxVuelo, "Ingrese Numero de Vuelo de LAN");
                return false;
            }
            return true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Valido())
            {
                string strNumVuelo = tbxVuelo.Text.ToUpper();
                string strFechaVuelo = dtpFecha.Value.ToShortDateString();
                CultureInfo culturaPeru = new CultureInfo("es-PE");
                System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;
                //string strFileName = "";
                strFechaVuelo = strFechaVuelo.Substring(6, 4) + strFechaVuelo.Substring(3, 2) + strFechaVuelo.Substring(0, 2);

                LAP.TUUA.DAO.DAO_VueloProgramado objDAOVuelo = new LAP.TUUA.DAO.DAO_VueloProgramado();
                if (objDAOVuelo.insertar(strNumVuelo, strFechaVuelo))
                {
                    MessageBox.Show("Vuelo Ingresado Correctamente.", "Informacion", MessageBoxButtons.OK);
                    erpVuelo.Clear();
                }
            }
        }
    }
}
