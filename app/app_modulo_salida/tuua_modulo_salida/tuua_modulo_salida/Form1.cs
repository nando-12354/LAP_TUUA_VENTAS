using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tuua_modulo_salida.Dao;
using tuua_modulo_salida.Entidades;

namespace tuua_modulo_salida
{
    public partial class Form1 : Form
    {

        DAO_TemporalBoardingPass objTemporalBoardingPass = new DAO_TemporalBoardingPass();
        DAO_TemporalTicket objTemporalTicket = new DAO_TemporalTicket();
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.Location = new Point(this.ClientSize.Width / 2 - panel1.Size.Width / 2, this.ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            
            txtBP.Focus();
           
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            txtBP.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            procesarDatos();

            txtBP.Focus();
        }



        private void txtBP_KeyUp(object sender, KeyEventArgs e)
        {
            lblMensaje.Text = String.Empty;
            semaforo.Image = Resource1.grey_light;

            if (e.KeyCode == Keys.Enter) {
                procesarDatos();

                

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void procesarDatos() {

            try
            {

                lblMensaje.Text = String.Empty;
                semaforo.Image = Resource1.grey_light;
                string trama = txtBP.Text;
                bool flgValido = false;
                string mensaje = string.Empty;
                TemporalBoardingPass entTemporalBoardingPass = new TemporalBoardingPass();
                TemporalTicket entTemporalTicket = new TemporalTicket();

                int resultado = 0;
                string numCheckin = "";
                string numVueloIATA = "";
                string numVuelo = "";
                string numAsiento = "";
                string nomPasajero = "";
                int numDias = 0;
                string fchVuelo = "";

                mensaje = "Por favor, registre un Boarding Pass o Ticket válido";
                if (trama != String.Empty)
                {
                    if (IsTicket(trama))
                    {
                        
                        entTemporalTicket.CodNumeroTicket = trama.Trim();
                        resultado = objTemporalTicket.Ingresar(entTemporalTicket);
                        if (resultado == 0)
                        {
                            mensaje = "Registro exitoso";
                            flgValido = true;
                        }
                        if (resultado == 1)
                        {
                            mensaje = "El ticket ya tiene registro de salida.";
                            flgValido = false;
                        }
                        if (resultado == 2)
                        {
                            mensaje = "El ticket no ha sido encontrado.";
                            flgValido = false;
                        }
                        
                    }
                    else if (IsBoardingPass(trama))
                    {

                        numDias = Convert.ToInt32(trama.Substring(44, 3));
                        DateTime dteFecha = new DateTime(DateTime.Now.Year - 1, 12, 31);
                        dteFecha = dteFecha.AddDays(numDias);
                        fchVuelo = dteFecha.ToString("yyyyMMdd");
                        nomPasajero = trama.Substring(2, 20).Trim().Replace("-", " ").Replace("/", " ");
                        numAsiento = right("0000" + trama.Substring(48, 4).Trim(), 4);
                        numVueloIATA = trama.Substring(36, 2).Trim();
                        numVuelo = Convert.ToString(Convert.ToInt32(trama.Substring(39, 4)));

                        if (numVuelo.Length == 4)
                        {
                            numVuelo = numVueloIATA + numVuelo;
                        }
                        else
                        {
                            numVuelo = right("000" + numVuelo, 3);
                            numVuelo = numVueloIATA + numVuelo;
                        }

                        numCheckin = right("00000" + trama.Substring(53, 4).Trim(), 5);
                        entTemporalBoardingPass.NumCheckin = numCheckin;
                        entTemporalBoardingPass.NumVuelo = numVuelo;
                        entTemporalBoardingPass.NumAsiento = numAsiento;
                        entTemporalBoardingPass.NomPasajero = nomPasajero;
                        entTemporalBoardingPass.FchVuelo = fchVuelo;
                        resultado = objTemporalBoardingPass.Ingresar(entTemporalBoardingPass);
                        if (resultado == 0)
                        {
                            mensaje = "Registro exitoso";
                            flgValido = true;
                        }
                        if (resultado == 1)
                        {
                            mensaje = "El Boarding Pass ya tiene registro de salida.";
                            flgValido = false;

                        }
                        if (resultado == 2)
                        {
                            mensaje = "El Boarding Pass no ha sido encontrado.";
                            flgValido = false;
                        }
                    }

                }


                if (flgValido)
                {
                    mostrarConfirmacion(mensaje);
                }
                if (!flgValido)
                {

                    mostrarError(mensaje);
                }
                
            }
            catch (Exception ex)
            {

                mostrarError(ex.Message);
            }
            finally {
                txtBP.Text = String.Empty;
                

            }
            
        }

        private void mostrarError(string mensaje) {
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Text = mensaje;
            semaforo.Image = Resource1.red_light;
        }
        private void mostrarConfirmacion(string mensaje) {
            lblMensaje.ForeColor = Color.YellowGreen;
            lblMensaje.Text = mensaje;
            semaforo.Image = Resource1.green_light;
        }
        private void limpiarSemaforo() {
            lblMensaje.ForeColor = Color.YellowGreen;
            lblMensaje.Text = String.Empty;
            semaforo.Image = Resource1.grey_light;
        }


        #region funcionesUtiles

        bool IsTicket(string str)
        {
            if (str.Length == 16)
            {
                foreach (char c in str)
                {
                    if (c < '0' || c > '9')
                        return false;
                }

                return true;
            }

            return false;
        }

        public string right(string param, int lenght)
        {
            string result = param.Substring(param.Length - lenght, lenght);
            return result;
        }

        bool IsBoardingPass(string str)
        {
            if ((str.Length > 16) && (str.Length > 50))
                return true;
            else
                return false;
        }
        #endregion
        private void txtBP_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
