using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.ARCHIVAMIENTO
{
    public partial class Archieving : Form
    {
        public Usuario objUsuario;
        private List<ArbolModulo> listaPerfil;
        private LoginForm loginForm;
        private BO_Configuracion objBOConfiguracion;
        private BO_Consultas objBOConsultas;
        private BO_Archivamiento objBOArchivamiento;

        private String rangoFechaInicial;
        private String rangoFechaFinal;
        
        private BDTUUAHistorica bdTUUAHistorica;

        private HoldOnWindow holdOnWindow;
        
        private String Codigo_Archivo;
        private int Cod_Etapa;

        private String Cod_Periodo;
        private String tablasCopy;
        private String tablasDepura;

        public Archieving(Usuario objUsuario, LoginForm loginForm, List<ArbolModulo> listaPerfil)
        {
            InitializeComponent();
            objBOConfiguracion = new BO_Configuracion();
            objBOConsultas = new BO_Consultas();
            objBOArchivamiento = new BO_Archivamiento();

            this.loginForm = loginForm;
            this.objUsuario = objUsuario;
            this.listaPerfil = listaPerfil;

            bdTUUAHistorica = new BDTUUAHistorica(objBOConfiguracion, objBOArchivamiento, this.objUsuario, this);
            //bdTUUAHistorica.Iniciar();

            this.lblNombreUsuario.Text = getNombreCompletoUsuario();

            cargaComboPeriodo();

            //-- EAG 08/01/2010
            //holdOnWindow = new HoldOnWindow(objBOConfiguracion);
            //-- EAG 08/01/2010

            tablasCopy = "TUA_Ticket;TUA_TicketEstHist;TUA_BoardingBcbp;TUA_BoardingBcbpEstHist;TUA_LogOperacion;TUA_LogCompraVenta;TUA_LogOperacCaja";
            tablasDepura = "TUA_TicketEstHist;TUA_Ticket;TUA_BoardingBcbpEstHist;TUA_BoardingBcbp;TUA_LogCompraVenta;TUA_LogOperacCaja;TUA_LogOperacion";
        }

        public void RefreshArchieving()
        {
            GUIForArchivamiento();

            SetFechaDisponible();

            cargarHistorico();
        }
        
        private void GUIForReprocesamiento()
        {
            lblPendientedeProcesarTit.Visible = true;
            lblPendientedeProcesar.Visible = true;
            btnReprocesar.Visible = true;

            btnArchivar.Enabled = false;
            cmbPeriodo.Enabled = false;
            cmbPeriodo.SelectedIndex = 0;//EAG 18/12/2009
        }

        private void GUIForArchivamiento()
        {
            lblPendientedeProcesarTit.Visible = false;
            lblPendientedeProcesar.Visible = false;
            btnReprocesar.Visible = false;
            cmbPeriodo.SelectedIndex = 0;//EAG 18/12/2009
        }

        private void SetFechaDisponible()
        {
            BO_Consultas boConsultas = new BO_Consultas();

            String cultureName;

            DataTable dtFechaDisponible = boConsultas.CalculaFechaDisponible();
            if(dtFechaDisponible.Rows.Count<1)
            {
                lblFechaDisponible.Text = "NO DISPONIBLE";
                btnArchivar.Enabled = false;
                return;
            }

            rangoFechaInicial = dtFechaDisponible.Rows[0]["RangoFechaInicial"].ToString();
            rangoFechaFinal = dtFechaDisponible.Rows[0]["RangoFechaFinal"].ToString();

            if (String.IsNullOrEmpty(rangoFechaInicial) && String.IsNullOrEmpty(rangoFechaFinal))
            {
                lblFechaDisponibleTit.Text = "Fecha Disponible:";
                lblFechaDisponible.Visible = true;
                txtDiaInicial.Visible = false;
                calendar.Visible = false;

                lblFechaFinal.Visible = false;
                lblFechaFinalTit.Visible = false;

                lblPeriodoTit.Location = new Point(42, 90);
                cmbPeriodo.Location = new Point(208, 90);

                lblFechaDisponible.Text = "NO DISPONIBLE";
                btnArchivar.Enabled = false;
                return;
            }

            if(String.IsNullOrEmpty(rangoFechaInicial))
            {
                //Caso cuando no se posee ningun registro en la tabla TUA_Archivo
                lblFechaDisponibleTit.Text = "Fecha Inicial:";
                lblFechaDisponible.Visible = false;
                txtDiaInicial.Visible = true;
                calendar.Visible = false;
                //txtDiaInicial.Location = new Point(208, 45);

                calendar.Location = new Point(208, 65);

                //lblFechaDisponible.Text = "NO DISPONIBLE";
                //btnArchivar.Enabled = false;
                btnArchivar.Enabled = true;

                lblFechaFinal.Visible = true;
                lblFechaFinalTit.Visible = true;

                lblPeriodoTit.Location = new Point(42, 131);
                cmbPeriodo.Location = new Point(208, 126);

                cultureName = (String)Property.htProperty[Define.CULTURENAME_FOR_MONTHSABREVIATED];
                lblFechaFinal.Text = rangoFechaFinal.Substring(6, 2) + " " + Function.GetAbbreviatedMonthName(cultureName, Int32.Parse(rangoFechaFinal.Substring(4, 2))) + " " + rangoFechaFinal.Substring(0, 4);

                btnArchivar.Enabled = false;

                return;
            }
            else
            {
                lblFechaDisponibleTit.Text = "Fecha Disponible:";
                lblFechaDisponible.Visible = true;
                txtDiaInicial.Visible = false;
                calendar.Visible = false;

                lblFechaFinal.Visible = false;
                lblFechaFinalTit.Visible = false;

                lblPeriodoTit.Location = new Point(42, 90);
                cmbPeriodo.Location = new Point(208, 90);
            }
            if(String.Compare(rangoFechaInicial, rangoFechaFinal) > 0)
            {
                lblFechaDisponible.Text = "NO DISPONIBLE";
                btnArchivar.Enabled = false;
                return;                
            }
            cultureName = (String)Property.htProperty[Define.CULTURENAME_FOR_MONTHSABREVIATED];

            lblFechaDisponible.Text = rangoFechaInicial.Substring(6, 2) + " " + Function.GetAbbreviatedMonthName(cultureName, Int32.Parse(rangoFechaInicial.Substring(4, 2))) + " " + rangoFechaInicial.Substring(0, 4);
            lblFechaDisponible.Text += " - ";
            lblFechaDisponible.Text += rangoFechaFinal.Substring(6, 2) + " " + Function.GetAbbreviatedMonthName(cultureName, Int32.Parse(rangoFechaFinal.Substring(4, 2))) + " " + rangoFechaFinal.Substring(0, 4);
        }

        private void cargarHistorico()
        {
            BO_Consultas boConsultas = new BO_Consultas();


            DataTable dtHistoricoArchivamiento = boConsultas.ObtenerHistoricoArchivamiento();
            bindingSource1.DataSource = dtHistoricoArchivamiento;
            //gvwHistorico.DataSource = dtHistoricoArchivamiento;

            bool needReprocesamiento = false;
            for(int i=0; i < dtHistoricoArchivamiento.Rows.Count; i++)
            {
                if(!dtHistoricoArchivamiento.Rows[i]["Tip_Estado"].Equals("1"))
                {
                    String cultureName = (String)Property.htProperty[Define.CULTURENAME_FOR_MONTHSABREVIATED];

                    rangoFechaInicial = dtHistoricoArchivamiento.Rows[i]["Fch_Ini"].ToString();
                    rangoFechaFinal = dtHistoricoArchivamiento.Rows[i]["Fch_Fin"].ToString();

                    lblPendientedeProcesar.Text = dtHistoricoArchivamiento.Rows[i]["FechaInicial"].ToString() + " - " + dtHistoricoArchivamiento.Rows[i]["FechaFinal"].ToString();
                    Codigo_Archivo = dtHistoricoArchivamiento.Rows[i]["Cod_Archivo"].ToString();
                    Cod_Etapa = Int32.Parse(dtHistoricoArchivamiento.Rows[i]["Cod_Etapa"].ToString());

                    GUIForReprocesamiento();

                    needReprocesamiento = true;
                    break;
                }
            }
            if(!needReprocesamiento)
            {
                btnArchivar.Enabled = true;
                cmbPeriodo.Enabled = true;                
            }
        }

        private void cargaComboPeriodo()
        {
            cmbPeriodo.Items.Clear();

            DataTable dtListaPeriodos = objBOConfiguracion.ObtenerPeriodosAArchivar();

            dtListaPeriodos.DefaultView.Sort = "Cod_Relativo";

            //for (int i = 0; i < dtListaPeriodos.Rows.Count; i++)
            //{
            //    cmbPeriodo.Items.Add(dtListaPeriodos.Rows[i]["Dsc_Campo"].ToString());
            //}

            DataRow row = dtListaPeriodos.NewRow();
            row["Dsc_Campo"] = "- Seleccione -";
            row["Cod_Campo"] = "";
            dtListaPeriodos.Rows.InsertAt(row,0);
            //cmbPeriodo.Text = "-Seleccione - ";

            cmbPeriodo.DataSource = dtListaPeriodos;
            cmbPeriodo.DisplayMember = "Dsc_Campo";
            cmbPeriodo.ValueMember = "Cod_Campo";

            //cmbPeriodo.Items.Insert(0, "- Seleccionar -");

            //cmbPeriodo.Items.Add("- Seleccionar -");
            
            cmbPeriodo.SelectedIndex = 0;
        }

        private String getNombreCompletoUsuario()
        {
            return objUsuario.SNomUsuario + " " + objUsuario.SApeUsuario;
        }

        private void lnkSalir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show((string)LabelConfig.htLabels["archiving.msgConfirmExit"], "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            Close();
            loginForm.Close();
        }

        private void btnArchivar_Click(object sender, EventArgs e)
        {
            bdTUUAHistorica.Iniciar();
            if(cmbPeriodo.SelectedIndex>0)
            {
                try
                {
                    if (String.Compare(rangoFechaInicial, rangoFechaFinal) > 0)
                    {
                        MessageBox.Show((string)LabelConfig.htLabels["archiving.msgRangoFechaIncorrecta"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    String rangoInicialParseableDateTime = rangoFechaInicial.Substring(6, 2) + "/" + rangoFechaInicial.Substring(4, 2) + "/" + rangoFechaInicial.Substring(0, 4);
                    DateTime dtRangoInicial = (DateTime.Parse(rangoInicialParseableDateTime));

                    String rangoFinalParseableDateTime = rangoFechaFinal.Substring(6, 2) + "/" + rangoFechaFinal.Substring(4, 2) + "/" + rangoFechaFinal.Substring(0, 4);
                    DateTime dtRangoFinal = (DateTime.Parse(rangoFinalParseableDateTime));

                    int diffMonths = monthDifference(dtRangoFinal, dtRangoInicial) + 1;
                    int periodo = Int32.Parse(cmbPeriodo.SelectedValue.ToString());

                    if(periodo > diffMonths)
                    {
                        MessageBox.Show((string)LabelConfig.htLabels["archiving.msgSeleccionePeriodoValido"], "Advertencia", MessageBoxButtons.OK);
                        return;
                    }
                    else if(periodo < diffMonths)
                    {
                        for (int j = 0; j < (diffMonths - periodo); j++ )
                        {
                            dtRangoFinal = dtRangoFinal.AddDays(-dtRangoFinal.Day);  
                        }
                        //dtRangoFinal = dtRangoFinal.AddMonths(periodo - diffMonths);

                        rangoFechaFinal = dtRangoFinal.Year.ToString().PadLeft(4, '0') + dtRangoFinal.Month.ToString().PadLeft(2, '0') + dtRangoFinal.Day.ToString().PadLeft(2, '0') + "";
                    }


                    DataTable dtResult = objBOArchivamiento.CalculaCodigoArchivamiento();
                    if(dtResult.Rows.Count==0)
                    {

                        return;
                    }
                    //HoldOnWindow holdOnWindow = new HoldOnWindow();
                    //holdOnWindow.Show();

                    if (MessageBox.Show((string)LabelConfig.htLabels["archiving.msgConfirmArchiving"], "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    Cod_Periodo = periodo.ToString();
                    Codigo_Archivo = dtResult.Rows[0]["Cod_Archivo"].ToString();

                    //-- EAG 08/01/2010
                    holdOnWindow = new HoldOnWindow(objBOConfiguracion);
                    //-- EAG 08/01/2010

                    new Thread(new ThreadStart(RunProcesarArchiving)).Start();
                    holdOnWindow.ShowDialog(this);
                    //bdTUUAHistorica.ProcesarArchiving(Cod_Periodo, rangoFechaInicial, rangoFechaFinal, Codigo_Archivo, tablasCopy, tablasDepura);


                }
                catch (Exception ex)
                {
                    MessageBox.Show((string)LabelConfig.htLabels["archiving.msgErrorDesconocido"] + " - " +ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorHandler.Trace(Define.ERR_DEFAULT, "btnArchivar_Click: " + (string)LabelConfig.htLabels["archiving.msgErrorDesconocido"] + " - ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                    RefreshArchieving();
                }


            }
            else
            {
                MessageBox.Show((string)LabelConfig.htLabels["archiving.msgSeleccionePeriodo"], "Advertencia", MessageBoxButtons.OK);
            }

        }

        private void RunProcesarArchiving()
        {
            bool ret = false;
            try
            {
                ret = bdTUUAHistorica.ProcesarArchiving(Cod_Periodo, rangoFechaInicial, rangoFechaFinal, Codigo_Archivo,
                                                  tablasCopy, tablasDepura);
                SetBack_Archieving(1, null, ret);
            }
            catch(Exception ex)
            {
                ret = false;
                ErrorHandler.Trace(Define.ERR_DEFAULT, "RunProcesarArchiving: ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                SetBack_Archieving(0, ex, ret);
            }
        }

        private void RunReprocesarArchiving()
        {
            bool ret = false;
            try
            {
                ret = bdTUUAHistorica.ReprocesarArchiving(Cod_Etapa, rangoFechaInicial, rangoFechaFinal, Codigo_Archivo, tablasCopy, tablasDepura);
                SetBack_Archieving(1, null, ret);
            }
            catch(Exception ex)
            {
                ret = false;
                ErrorHandler.Trace(Define.ERR_DEFAULT, "RunReprocesarArchiving: ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                SetBack_Archieving(0, ex, ret);
            }
        }

        private delegate void BAHandler(int curso, Exception ex, bool ret);

        public void SetBack_Archieving(int curso, Exception ex, bool ret)
        {
            if (this.InvokeRequired)
                this.Invoke(new BAHandler(Back_Archieving), new object[] { curso, ex, ret });
            else
                Back_Archieving(curso, ex, ret);
        }

        private void Back_Archieving(int curso, Exception ex, bool ret)
        {
            switch (curso)
            {
                case 0:
                    holdOnWindow.Hide();
                    holdOnWindow.Close();
                    MessageBox.Show((string)LabelConfig.htLabels["archiving.msgErrorDesconocido"] + " - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //new ResultadoProceso(ret, Codigo_Archivo, objBOConsultas, objBOConfiguracion).ShowDialog(this);   //EAG 18/12/2009 Comentado
                    RefreshArchieving();
                    break;
                case 1:
                    holdOnWindow.Hide();
                    holdOnWindow.Close();
                    new ResultadoProceso(ret, Codigo_Archivo, objBOConsultas, objBOConfiguracion).ShowDialog(this);
                    RefreshArchieving();
                    break;
            }

        }

        private delegate void SPHOWHandler(int paso);

        public void SetSetPasoHoldOnWindow(int paso)
        {
            if (this.InvokeRequired)
                this.Invoke(new SPHOWHandler(SetPasoHoldOnWindow), new object[] { paso });
            else
                SetPasoHoldOnWindow(paso);
        }

        private void SetPasoHoldOnWindow(int paso)
        {
            holdOnWindow.SetPaso(paso);
        }

        private void btnReprocesar_Click(object sender, EventArgs e)
        {
            bdTUUAHistorica.Iniciar();

            //-- EAG 08/01/2010
            holdOnWindow = new HoldOnWindow(objBOConfiguracion);
            //-- EAG 08/01/2010

            new Thread(new ThreadStart(RunReprocesarArchiving)).Start();
            holdOnWindow.ShowDialog(this);
            

            //try
            //{
            //    bdTUUAHistorica.ReprocesarArchiving(Cod_Etapa, rangoFechaInicial, rangoFechaFinal, Codigo_Archivo, tablasCopy, tablasDepura);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show((string)LabelConfig.htLabels["archiving.msgErrorDesconocido"] + " - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    RefreshArchieving(); 
            //}
        }

        private static int monthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        private void gvwHistorico_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try
                {
                    String valor = gvwHistorico.Rows[e.RowIndex].Cells[0].Value.ToString();
                    DetalleArchivamiento detalleArchivamiento = new DetalleArchivamiento(valor, objBOConsultas,
                                                                                         objBOConfiguracion);
                    detalleArchivamiento.ShowDialog(this);
                }
                catch(Exception ex)
                {
                    MessageBox.Show((string)LabelConfig.htLabels["archiving.msgErrorDesconocido"] + " - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //RefreshArchieving();
                }
            }
        }

        private void calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            String fechaPeruanaInicial = e.Start.ToShortDateString();
            rangoFechaInicial = fechaPeruanaInicial.Substring(6, 4) + fechaPeruanaInicial.Substring(3, 2) + fechaPeruanaInicial.Substring(0, 2);

            String cultureName = (String)Property.htProperty[Define.CULTURENAME_FOR_MONTHSABREVIATED];
            String fechaFormateada = fechaPeruanaInicial.Substring(0, 2) + " " + Function.GetAbbreviatedMonthName(cultureName, Int32.Parse(fechaPeruanaInicial.Substring(3, 2))) + " " + fechaPeruanaInicial.Substring(6, 4);
            txtDiaInicial.Text = fechaFormateada;
            txtDiaInicial.SelectionStart = txtDiaInicial.Text.Length;
            cmbPeriodo.Focus();
            calendar.Visible = false;
            btnArchivar.Enabled = true;
        }

        private void calendar_Leave(object sender, EventArgs e)
        {
            calendar.Visible = false;
        }

        //private void txtDiaInicial_Enter(object sender, EventArgs e)
        //{
        //    calendar.Visible = true;
        //}

        private void txtDiaInicial_Leave(object sender, EventArgs e)
        {
            //calendar.Visible = false;
        }

        private void txtDiaInicial_Click(object sender, EventArgs e)
        {
            if(!calendar.Visible)
                calendar.Visible = true;
            else
                calendar.Visible = false;
        }

        private void lnkAyuda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Help.ShowHelp(this, AppDomain.CurrentDomain.BaseDirectory + "resources/AyudaArchivamiento.chm", HelpNavigator.TableOfContents);
        }

        private void Archieving_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        Help.ShowHelp(this, AppDomain.CurrentDomain.BaseDirectory + "resources/AyudaArchivamiento.chm", HelpNavigator.TableOfContents);
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string)LabelConfig.htLabels["archiving.msgErrorDesconocido"] + " - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void calendar_MouseLeave(object sender, EventArgs e)
        //{
        //    calendar.Visible = false;
        //}

    }
}
