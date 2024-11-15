using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using LAP.TUUA.UTIL;
using System.Collections;
using System.Diagnostics;
using System.Security.Permissions;
namespace LAP.TUUA.ACCESOS
{
    public partial class ACS_FormContingenciaDesktop : Form
    {

        public bool bLectura;
        public string strTrama;
        public ACS_SinContingencia Obj_SincContingencia;
        public ACS_Controlador Obj_Controlador;
        public string strTramaTest; 
        public int iPos;
        public int iPosMax;


        public ACS_FormContingenciaDesktop()
        {
            ACS_Util Obj_Util = new ACS_Util();
            try
            {
                ErrorHandler.CargarErrorTypes(AppDomain.CurrentDomain.BaseDirectory + "/resources");
                Obj_Controlador = new ACS_Controlador(this);
                Obj_Controlador.Iniciar();
                InciandooAccesosContigencia(Obj_Controlador.Obj_SincContingencia, Convert.ToBoolean((string)Property.htProperty["PEGAR_TRAMA"]));
            }
            catch (Exception ex)
            {
                string strTipError = ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);

                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                String strSTrace = "";
                int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                if (intOcurrencia > 0)
                {
                    strSTrace = ex.StackTrace.Substring(intOcurrencia);
                    intOcurrencia = strSTrace.IndexOf(")");
                    if (intOcurrencia > 0)
                        strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                }

                Obj_Util.EscribirLog(strSTrace, ex.GetType().Name);
                Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.ACS_Sistema", ex.GetBaseException().Message);
                Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.ACS_Sistema", ex.StackTrace.ToString());
            }
        }


                 
        public void InciandooAccesosContigencia(ACS_SinContingencia Obj_SincContingencia, bool bPegarTrama)
        {
            InitializeComponent();                        
            chkTrama.Enabled = bPegarTrama;
            bLectura = false;
            strTrama = "";
            this.Obj_SincContingencia = Obj_SincContingencia;
            

            if (ACS_Property.bModeTest) //esilva - 04.05.2011 - Test Tramas
            {
                tmrUploadTest.Enabled = true;
                tmrUploadTest.Interval = ACS_Property.iTimeOutTest;
                iPos = 0;
                iPosMax = BPConfig.htLabels.Count;
            }
            else
            {
                txtbTicket.KeyPress += new KeyPressEventHandler(KeyPressed);
            }
        }
        
        public void MostrarSemaforo(int color)
        {
            switch (color)
            {
                case 0:
                    this.pbxVerde.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/verde.bmp";
                    this.Invalidate();
                    break;
                case 1:
                    this.pbxAmarillo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/amarillo.bmp";
                    this.Invalidate();
                    break;
                case 2:
                    this.pbxSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/rojo.bmp";
                    this.Invalidate();
                    break;
                default:
                    break;
            }
        }

        public void LimpiarSemaforo()
        {
            this.pbxVerde.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.bmp";
            this.pbxAmarillo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.bmp";
            this.pbxSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.bmp";

        }
        
        public void Limpiar()
        {
            txtbTicket.Clear();
            txtbAerolinea.Clear();
            txtbFechaVuelo.Clear();
            txtbNroAsiento.Clear();
            txtbPasajero.Clear();
            txtbVuelo.Clear();
            lblMensaje.Text = "";
        }

        public void AlmacenarDetalle()
        {
            if (txtbTicket.Text.Trim().Length > 0)
            {
                dataGridView1.Rows.Insert(0, DateTime.Now.ToString(), "TICKET", txtbTicket.Text, lblMensaje.Text);
            }
            else
            {
                if (txtbPasajero.Text.Trim().Length > 0)
                {

                    dataGridView1.Rows.Insert(0, DateTime.Now.ToString(), "BOARDING", txtbAerolinea.Text + "-" + txtbVuelo.Text + "-" + txtbFechaVuelo.Text +
                                            "-" + txtbNroAsiento.Text + "-" + txtbPasajero.Text, lblMensaje.Text);

                }
                else
                {
                    dataGridView1.Rows.Insert(0, DateTime.Now.ToString(), "", "", lblMensaje.Text);

                }
            }

            lblTotal.Text = "Total Leidos:" + (dataGridView1.RowCount - 1 - 10).ToString();

            this.Invalidate();

        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (Monitor.TryEnter(Obj_SincContingencia, 2000))
            {
                try
                {
                    if (e.KeyChar == (char)Keys.Return)
                    {
                        Obj_SincContingencia.BLectura = true;
                        e.Handled = true;
                        this.Obj_Controlador.ejecutarLectura();
                    }
                    else if (!(Obj_SincContingencia.StrTrama == String.Empty && e.KeyChar.ToString() == "\n"))
                    {
                        lblMensaje.Text = "PROCESANDO...";
                        Obj_SincContingencia.StrTrama = Obj_SincContingencia.StrTrama + e.KeyChar.ToString();
                    }
                }
                finally
                {
                    Monitor.Exit(Obj_SincContingencia);
                }
            }
            else
            {
                Monitor.PulseAll(Obj_SincContingencia);
            }
        }

        public String ConvertirJulianoCalendario(int jd)
        {
            try
            {
                string strRpta = "";
                Hashtable htMes = new Hashtable();
                htMes.Add("1", 31);
                htMes.Add("2", 28);
                htMes.Add("3", 31);
                htMes.Add("4", 30);
                htMes.Add("5", 31);
                htMes.Add("6", 30);
                htMes.Add("7", 31);
                htMes.Add("8", 31);
                htMes.Add("9", 30);
                htMes.Add("10", 31);
                htMes.Add("11", 31);
                htMes.Add("12", 31);

                int intMes = 0;
                int intDia = 0;
                do
                {
                    intMes++;
                    intDia = jd;
                    jd = jd - (int)htMes[intMes.ToString()];

                } while (jd > 0);

                strRpta = DateTime.Today.Year.ToString() + ConvertirDosDigitos(intMes) +
                         ConvertirDosDigitos(intDia);
                return strRpta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public String ConvertirDosDigitos(int intValor)
        {
            String strRpta = "";
            if (intValor < 10)
                strRpta = "0" + intValor.ToString();
            else
                strRpta = intValor.ToString();

            return strRpta;
        }

        private void ACS_FormContingencia_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                dataGridView1.Rows.Add("", "", "", "");
            }

        }

        private void txtbTicket_TextChanged(object sender, EventArgs e)
        {

        }

        private void ACS_FormContingencia_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        //esilva - 04.05.2011 - Test Tramas
        private void tmrUploadTest_Tick(object sender, EventArgs e)
        {
            if (iPos < iPosMax)
            {
                string sIndex = iPos + "";
                strTramaTest = (string)BPConfig.htLabels[sIndex];
                btnAgregar.PerformClick();
                this.iPos++;
            }
            else
            {
                tmrUploadTest.Enabled = false;
                lblMensaje.Text = "CARGA TEST FINALIZADA...REINICIE SERVICIO";
            }
        }

        //esilva - 04.05.2011 - Test Tramas
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "PROCESANDO...";
            Obj_SincContingencia.StrTrama = strTramaTest;
            Obj_SincContingencia.BLectura = true;
        }

        private void chkTrama_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTrama.Checked)
            {
                txtbTicket.ReadOnly = false;
                btnEnviar.Enabled = true;
            }
            else
            {
                txtbTicket.ReadOnly = true;
                btnEnviar.Enabled = false;
                txtbTicket.Focus();
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "PROCESANDO...";
            if (chkTrama.Checked)
            {
                Obj_SincContingencia.StrTrama = "" + txtbTicket.Text;
                Obj_SincContingencia.BLectura = true;
                txtbTicket.Text = "";
                this.Obj_Controlador.ejecutarLectura();
            }
        }
    }
}
