using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.CONTROL;

namespace LAP.TUUA.ARCHIVAMIENTO
{
    public partial class HoldOnWindow : Form
    {
        private BO_Configuracion objBOConfiguracion;

        private Hashtable htEtapas;

        public HoldOnWindow(BO_Configuracion objBOConfiguracion)
        {
            InitializeComponent();
            this.objBOConfiguracion = objBOConfiguracion;
            DataTable dtCodigoEtapa = objBOConfiguracion.ObtenerCodEtapaArchivamiento();
            htEtapas = new Hashtable();
            for(int i=0; i<dtCodigoEtapa.Rows.Count;i++)
            {
                int cod_Campo = Int32.Parse(dtCodigoEtapa.Rows[i]["Cod_Campo"].ToString());
                String dsc_Campo = dtCodigoEtapa.Rows[i]["Dsc_Campo"].ToString();

                htEtapas.Add(cod_Campo, dsc_Campo);
            }

        }

        public void SetPaso(int paso)
        {
            String value = htEtapas[paso].ToString();
            if(value!=null)
            {
                lblPaso.Text = "Paso " + paso + " - " + value;    
            }
            else
            {
                lblPaso.Text = "";    
            }
            
        }

        private void HoldOnWindow_Load(object sender, EventArgs e)
        {
            lblPaso.Text = "";
        }

    }
}
