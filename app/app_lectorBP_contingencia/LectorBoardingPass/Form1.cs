using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LectorBoardingPass
{
    public partial class Form1 : Form
    {
        List<Lectura> ls;
        String path;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            path = System.Configuration.ConfigurationManager.AppSettings["rutaArchivo"];
            ls = leerArchivo(path);
            dataGridView1.DataSource = ls.OrderByDescending(l=>l.Id).ToList();
            txt_trama.Focus();

            int cantTickets = ls.Where(l => l.TipoDocumento == "Ticket").Count();
            lblTicket.Text = "Tickets Leídos: " + cantTickets.ToString();
            int cantBPs = ls.Where(l => l.TipoDocumento == "BoardingPass").Count();
            lblBP.Text = "BoardingPass Leídos: " + cantBPs.ToString();

        }

        private void txt_trama_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter) {
                button1.PerformClick();

            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txt_trama.Text)) {

                    ls = leerArchivo(path);
                    int lastid = 0;
                    if (ls!=null && ls.Count>0) {
                        lastid = ls.Max(l => l.Id);

                    }
                    
                    Lectura lect = new Lectura();
                    lect.Trama = txt_trama.Text;
                    txt_trama.Text = string.Empty;
                    lect.Fecha = DateTime.Now.ToString();
                    lect.Id = lastid+1;
                    lect.TipoMolinete = System.Configuration.ConfigurationManager.AppSettings["tipoMolinete"];
                    if (lect.Trama.Trim().ToUpper().StartsWith("M"))
                    {
                        lect.TipoDocumento = "BoardingPass";
                    }
                    else {

                        lect.TipoDocumento = "Ticket";
                    }
                    string cadena = lect.Id.ToString() + "|" + lect.Trama+ "|"+lect.TipoDocumento+ "|" +lect.TipoMolinete+"|" +lect.Fecha.ToString();
                    // gravar archivo
                    grabarArchivo(path, cadena);

                    ls.Add(lect);
                   
                    dataGridView1.DataSource =  ls.OrderByDescending(l => l.Id).ToList();

                    txt_trama.Focus();

                    int cantTickets = ls.Where(l => l.TipoDocumento == "Ticket").Count();
                    lblTicket.Text = "Tickets Leídos: " + cantTickets.ToString();
                    int cantBPs = ls.Where(l => l.TipoDocumento == "BoardingPass").Count();
                    lblBP.Text = "BoardingPass Leídos: " + cantBPs.ToString();


                }
            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.Message);
            }
            


        }
        private void grabarArchivo(string path, string linea) {
            using (StreamWriter sw = File.AppendText(path)) {
                sw.WriteLine(linea);

            }
            
        }

        private List<Lectura> leerArchivo(string path) {
            List<Lectura> ls = new List<Lectura>();
            string[] lineas = File.ReadAllLines(path);

            foreach (string linea in lineas) {
                string[] columnas = linea.Split('|');
                Lectura lect = new Lectura();
                lect.Id = Int32.Parse(columnas[0]);
                lect.Trama = columnas[1];
                lect.TipoDocumento = columnas[2];
                lect.TipoMolinete = columnas[3];
                lect.Fecha = columnas[4];
               
                ls.Add(lect);

            }
            return ls;
        }
    }
}
