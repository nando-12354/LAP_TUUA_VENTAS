/*
Sistema		    :   TUUA
Aplicación		:   Ventas
Objetivo		:   Gestión del Menu principal de la aplicación de ventas.
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	:   11/07/2009	
Programador		:	JCISNEROS
Observaciones	:	
*/
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.PRINTER;
using LAP.TUUA.UTIL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OutlookStyleControls;
using System.Collections;
using System.Threading;
using System.Xml;
using LAP.TUUA.CONTROL;

namespace LAP.TUUA.VENTAS
{
    public partial class Principal : Form
    {
        internal static System.Windows.Forms.Timer timerIdle;
        public Usuario objUsuario;
        protected Turno objTurno;
        protected CajaIngreso formIngreso;
        protected CajaEgreso formEgreso;
        protected Cierre formCierre;
        protected Cuadre formCuadre;
        protected CompraVenta formCompraVenta;
        protected Logueo formLogueo;
        protected ClaveTurno formClave;
        protected Normal formNormal;
        protected Masivo formMasivo;
        protected List<ArbolModulo> listaPerfil;
        public bool Flg_Salir;
        public bool Flg_Bloqueado;

        private bool Flg_Operacion;
        private bool Flg_Turno;
        private bool Flg_Ingreso;
        private bool Flg_Egreso;
        private bool Flg_Cuadre;
        private bool Flg_Cierre;
        private bool Flg_CompraVenta;
        private bool Flg_Normal;
        private bool Flg_Masivo;
        private bool Flg_Clave;
        protected BO_Turno objBOTurno;

        public List<VueloProgramado> listaFavoritos;

        // parametros de impresion (GGarcia-20090907)
        // lista parametros de configuracion
        private Hashtable listaParamConfig;
        // lista parametros de impresion 
        private Hashtable listaParamImp;
        // xml
        private XmlDocument xml;
        // impresion
        private Print impresion;
        //enumerador
        public int cont_favoritos_nac;
        public int cont_favoritos_int;

        // se agrega parametros de impresion (GGarcia-20090907)
        public Principal(Usuario objUsuario, Turno objTurno, Logueo formLogueo, List<ArbolModulo> listaPerfil, Hashtable listaParamConfig, XmlDocument xml)
        {
            InitializeComponent();

            this.listaPerfil = listaPerfil;
            listaFavoritos = new List<VueloProgramado>();
            Flg_Bloqueado = false;
            Flg_Salir = false;
            Flg_Operacion = false;
            Flg_Turno = false;
            Flg_Ingreso = false;
            Flg_Egreso = false;
            Flg_Cuadre = false;
            Flg_Cierre = false;
            Flg_CompraVenta = false;
            Flg_Normal = false;
            Flg_Masivo = false;
            Flg_Clave = false;

            this.objUsuario = objUsuario;
            this.objTurno = objTurno;
            this.tsslPtoventa.Text = "Estación de Venta: [" + Property.htProperty[Define.IP_PTO_VENTA].ToString() + "]";
            this.Text = "TURNO: " + objTurno.SCodTurno + " - USUARIO: " + objUsuario.SNomUsuario + " " + objUsuario.SApeUsuario;
            this.formLogueo = formLogueo;
            this.tsslTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();

            VerificarPermisos();
            EjecutarPermisos();

            // inicializar parametros de impresion (GGarcia-20090907)
            // lista parametros de configuracion
            this.listaParamConfig = listaParamConfig;
            // xml
            this.xml = xml;
            // lista parametros de impresion
            this.listaParamImp = new Hashtable();
            // objeto 
            impresion = new Print();
            objBOTurno = new BO_Turno();

            Property.strUsuario = objUsuario.SCodUsuario;
            MostrarFooterTasas();
            ActualizarTasa();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowPanes("show initial options here", "show homepage here");
        }

        private void ShowPanes(string label1Text, string label2Text)
        {

        }

        private void outlookBar1_Click(object sender, OutlookStyleControls.OutlookBar.ButtonClickEventArgs e)
        {
            int idx = olkbVentas.Buttons.IndexOf(e.SelectedButton);
            switch (idx)
            {
                case 0: // operaciones
                    if (Flg_Operacion)
                    {
                        SeleccionarMenu(0);
                    }
                    break;
                case 1: // turno
                    if (Flg_Turno)
                    {
                        SeleccionarMenu(1);
                    }
                    break;
                case 2: // consultas
                    CargarAyuda();
                    //SeleccionarMenu(2);
                    break;
                default:
                    break;
            }
        }

        private void SeleccionarMenu(int intIndex)
        {
            switch (intIndex)
            {
                case 0:
                    trwOperacion.ExpandAll();
                    trwOperacion.Visible = true;
                    trwTurno.Visible = false;
                    trwConsulta.Visible = false;
                    break;
                case 1:
                    trwTurno.ExpandAll();
                    trwOperacion.Visible = false;
                    trwTurno.Visible = true;
                    trwConsulta.Visible = false;
                    break;
                case 2:
                    trwOperacion.Visible = false;
                    trwTurno.Visible = false;
                    trwConsulta.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void menuOlive_Click(object sender, EventArgs e)
        {
            olkbVentas.GradientButtonNormalDark = Color.FromArgb(178, 193, 140);
            olkbVentas.GradientButtonNormalLight = Color.FromArgb(234, 240, 207);
            olkbVentas.Invalidate();
        }

        private void menuBlue_Click(object sender, EventArgs e)
        {
            olkbVentas.GradientButtonNormalDark = Color.FromArgb(126, 166, 225);
            olkbVentas.GradientButtonNormalLight = Color.FromArgb(203, 225, 252);
            olkbVentas.Invalidate();

        }

        private void menuSilver_Click(object sender, EventArgs e)
        {
            olkbVentas.GradientButtonNormalDark = Color.FromArgb(150, 148, 178);
            olkbVentas.GradientButtonNormalLight = Color.FromArgb(225, 226, 236);
            olkbVentas.Invalidate();
        }

        public void CloseFormChildren(int ichild)
        {
            if (this.formIngreso != null)
            {
                this.formIngreso.Close();
            }
            if (this.formEgreso != null)
            {
                this.formEgreso.Close();
            }
            if (this.formCuadre != null)
            {
                this.formCuadre.Close();
            }
            if (this.formCierre != null)
            {
                this.formCierre.Close();
            }
            if (this.formCompraVenta != null)
            {
                this.formCompraVenta.Close();
            }
            if (this.formClave != null)
            {
                this.formClave.Close();
            }
            if (this.formNormal != null)
            {
                this.formNormal.Close();
            }
            if (this.formMasivo != null)
            {
                this.formMasivo.Close();
            }
        }

        private void trwOperacion_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strName = trwOperacion.SelectedNode.Name;
            Property.strModulo = (string)Property.htProperty[Define.VEN_OPERA];
            try
            {
                switch (strName)
                {
                    case "cndIgreso":
                        if (Flg_Ingreso)
                        {
                            CloseFormChildren(0);
                            // se agrega parametros de impresion (GGarcia-20090907)
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_ING].ToString();
                            formIngreso = new CajaIngreso(this.objUsuario, this.objTurno, listaParamConfig, listaParamImp, xml, impresion, this);
                            formIngreso.MdiParent = this;
                            formIngreso.Show();
                        }
                        break;
                    case "cndEgreso":
                        if (Flg_Egreso)
                        {
                            CloseFormChildren(1);
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_EGR].ToString();
                            // se agrega parametros de impresion (GGarcia-20090907)
                            formEgreso = new CajaEgreso(this.objUsuario, this.objTurno, listaParamConfig, listaParamImp, xml, impresion, this);
                            formEgreso.MdiParent = this;
                            formEgreso.Show();
                        }
                        break;
                    case "cndVenta":
                        if (Flg_Normal)
                        {
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_VTN].ToString();
                            CloseFormChildren(1);
                            // se agrega parametros de impresion (GGarcia-20090907)
                            formNormal = new Normal(this.objUsuario, this.objTurno, this, listaParamConfig, listaParamImp, xml, impresion);
                            formNormal.MdiParent = this;
                            if (!formNormal.IsDisposed)
                            {
                                formNormal.Show();
                            }
                        }
                        break;
                    case "cndCompraVenta":
                        if (Flg_CompraVenta)
                        {
                            CloseFormChildren(3);
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_CVM].ToString();
                            // se agrega parametros de impresion (GGarcia-20090907)
                            formCompraVenta = new CompraVenta(this.objUsuario, this.objTurno, listaParamConfig, listaParamImp, xml, impresion, this);
                            formCompraVenta.MdiParent = this;
                            if (!formCompraVenta.IsDisposed)
                            {
                                formCompraVenta.Show();
                            }
                        }
                        break;
                    case "cndMasivo":
                        if (Flg_Masivo)
                        {
                            CloseFormChildren(5);
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_VTM].ToString();
                            // se agrega parametros de impresion (GGarcia-20090907)
                            formMasivo = new Masivo(this.objUsuario, this.objTurno, this, listaParamConfig, listaParamImp, xml, impresion);
                            formMasivo.MdiParent = this;
                            if (!formMasivo.IsDisposed)
                            {
                                formMasivo.Show();
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                ShowErrorHandler();
            }
        }

        private void trwTurno_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strName = trwTurno.SelectedNode.Name;
            Property.strModulo = (string)Property.htProperty[Define.VEN_TURNO];
            try
            {
                switch (strName)
                {
                    case "cndCuadre":
                        if (Flg_Cuadre)
                        {
                            CloseFormChildren(4);
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_CUA].ToString();
                            formCuadre = new Cuadre(this.objUsuario, this.objTurno, this);
                            formCuadre.MdiParent = this;
                            formCuadre.Show();
                        }
                        break;
                    case "cndCierre":
                        if (Flg_Cierre)
                        {
                            CloseFormChildren(6);
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_CIE].ToString();
                            formCierre = new Cierre(this.objTurno, listaParamConfig, listaParamImp, xml, impresion, this, this.formLogueo);
                            formCierre.MdiParent = this;
                            formCierre.Show();
                        }
                        break;
                    case "cndClave":
                        if (Flg_Clave)
                        {
                            CloseFormChildren(7);
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_CLV].ToString();
                            formClave = new ClaveTurno(this.objUsuario, this);
                            formClave.MdiParent = this;
                            formClave.Show();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                ShowErrorHandler();
            }
        }


        public void ShowErrorHandler()
        {
            string strMessage = "";
            try
            {
                strMessage = (string)((Hashtable)ErrorHandler.htErrorType[ErrorHandler.Cod_Error])["MESSAGE"];
            }
            catch
            {
                strMessage = Define.ERR_DEFAULT;
            }
            ErrorHandler.Desc_Mensaje = strMessage;
            ErrorHandler.Trace(Define.VENTAS, strMessage);
            MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            this.tsslTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        private void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        CargarAyuda();
                        break;
                    case Keys.F2:
                        if (Flg_Ingreso)
                        {
                            Property.strModulo = (string)Property.htProperty[Define.VEN_OPERA];
                            Property.strSubModulo = Property.htProperty[Define.VEN_PROC_ING].ToString();
                            this.olkbVentas.SelectedButton = this.olkbVentas.Buttons[0];
                            CloseFormChildren(0);
                            SeleccionarMenu(0);
                            // se agrega parametros de impresion (GGarcia-20090907)
                            formIngreso = new CajaIngreso(this.objUsuario, this.objTurno, listaParamConfig, listaParamImp, xml, impresion, this);
                            formIngreso.MdiParent = this;
                            formIngreso.Show();
                        }
                        break;
                    case Keys.F3:
                        if (Flg_Egreso)
                        {
                            Property.strModulo = (string)Property.htProperty[Define.VEN_OPERA];
                            Property.strSubModulo = (string)Property.htProperty[Define.VEN_PROC_EGR];
                            this.olkbVentas.SelectedButton = this.olkbVentas.Buttons[0];
                            CloseFormChildren(1);
                            SeleccionarMenu(0);
                            // se agrega parametro de impresion (GGarcia-20090907)
                            formEgreso = new CajaEgreso(this.objUsuario, this.objTurno, listaParamConfig, listaParamImp, xml, impresion, this);
                            formEgreso.MdiParent = this;
                            formEgreso.Show();
                        }
                        break;
                    case Keys.F4:
                        if (Flg_Normal)
                        {
                            Property.strModulo = (string)Property.htProperty[Define.VEN_OPERA];
                            Property.strSubModulo = (string)Property.htProperty[Define.VEN_PROC_VTN];
                            this.olkbVentas.SelectedButton = this.olkbVentas.Buttons[0];
                            SeleccionarMenu(0);
                            CloseFormChildren(2);
                            // se agrega parametros de impresion (GGarcia-20090907)
                            formNormal = new Normal(this.objUsuario, this.objTurno, this, listaParamConfig, listaParamImp, xml, impresion);
                            formNormal.MdiParent = this;
                            if (!formNormal.IsDisposed)
                            {
                                formNormal.Show();
                            }
                        }
                        break;
                    case Keys.F5:
                        if (Flg_CompraVenta)
                        {
                            Property.strModulo = (string)Property.htProperty[Define.VEN_OPERA];
                            Property.strSubModulo = (string)Property.htProperty[Define.VEN_PROC_CVM];
                            this.olkbVentas.SelectedButton = this.olkbVentas.Buttons[0];
                            CloseFormChildren(3);
                            SeleccionarMenu(0);
                            // se agrega parametros de impresion (GGarcia-20090907)
                            formCompraVenta = new CompraVenta(this.objUsuario, this.objTurno, listaParamConfig, listaParamImp, xml, impresion, this);
                            formCompraVenta.MdiParent = this;
                            if (!formCompraVenta.IsDisposed)
                            {
                                formCompraVenta.Show();
                            }
                        }
                        break;
                    case Keys.F6:
                        if (Flg_Masivo)
                        {
                            Property.strModulo = (string)Property.htProperty[Define.VEN_OPERA];
                            Property.strSubModulo = (string)Property.htProperty[Define.VEN_PROC_VTM];
                            this.olkbVentas.SelectedButton = this.olkbVentas.Buttons[0];
                            CloseFormChildren(5);
                            SeleccionarMenu(0);
                            // se agrega parametros de impresion (GGarcia-20090907)
                            formMasivo = new Masivo(this.objUsuario, this.objTurno, this, listaParamConfig, listaParamImp, xml, impresion);
                            formMasivo.MdiParent = this;
                            if (!formMasivo.IsDisposed)
                            {
                                formMasivo.Show();
                            }
                        }
                        break;
                    case Keys.F7:
                        if (Flg_Cuadre)
                        {
                            Property.strModulo = (string)Property.htProperty[Define.VEN_TURNO];
                            Property.strSubModulo = (string)Property.htProperty[Define.VEN_PROC_CUA];
                            this.olkbVentas.SelectedButton = this.olkbVentas.Buttons[1];
                            CloseFormChildren(4);
                            SeleccionarMenu(1);
                            formCuadre = new Cuadre(this.objUsuario, this.objTurno, this);
                            formCuadre.MdiParent = this;
                            formCuadre.Show();
                        }
                        break;
                    case Keys.F8:
                        if (Flg_Cierre)
                        {
                            Property.strModulo = (string)Property.htProperty[Define.VEN_TURNO];
                            Property.strSubModulo = (string)Property.htProperty[Define.VEN_PROC_CIE];
                            this.olkbVentas.SelectedButton = this.olkbVentas.Buttons[1];
                            CloseFormChildren(6);
                            SeleccionarMenu(1);
                            formCierre = new Cierre(this.objTurno, listaParamConfig, listaParamImp, xml, impresion, this, this.formLogueo);
                            formCierre.MdiParent = this;
                            formCierre.Show();
                        }
                        break;
                    case Keys.F9:
                        if (Flg_Clave)
                        {
                            Property.strModulo = (string)Property.htProperty[Define.VEN_TURNO];
                            Property.strSubModulo = (string)Property.htProperty[Define.VEN_PROC_CLV];
                            this.olkbVentas.SelectedButton = this.olkbVentas.Buttons[1];
                            CloseFormChildren(7);
                            SeleccionarMenu(1);
                            formClave = new ClaveTurno(this.objUsuario, this);
                            formClave.MdiParent = this;
                            formClave.Show();
                        }
                        break;
                    default: break;
                }
            }
            catch
            {
                ShowErrorHandler();
            }

        }

        private void VerificarPermisos()
        {
            int intContOpera = 0;
            int intContTurno = 0;
            for (int i = 0; i < listaPerfil.Count; i++)
            {
                if (listaPerfil[i].SCodModulo == Property.htProperty[Define.VEN_OPERA].ToString())
                {

                    if (listaPerfil[i].SFlgPermitido == "1")
                    {
                        intContOpera++;
                    }
                    if (listaPerfil[i].SCodProceso == Property.htProperty[Define.VEN_PROC_CVM].ToString())
                    {
                        Flg_CompraVenta = listaPerfil[i].SFlgPermitido == "1" ? true : false;
                        continue;
                    }
                    if (listaPerfil[i].SCodProceso == Property.htProperty[Define.VEN_PROC_EGR].ToString())
                    {
                        Flg_Egreso = listaPerfil[i].SFlgPermitido == "1" ? true : false;
                        continue;
                    }
                    if (listaPerfil[i].SCodProceso == Property.htProperty[Define.VEN_PROC_ING].ToString())
                    {
                        Flg_Ingreso = listaPerfil[i].SFlgPermitido == "1" ? true : false;
                        continue;
                    }
                    if (listaPerfil[i].SCodProceso == Property.htProperty[Define.VEN_PROC_VTM].ToString())
                    {
                        Flg_Masivo = listaPerfil[i].SFlgPermitido == "1" ? true : false;
                        continue;
                    }
                    if (listaPerfil[i].SCodProceso == Property.htProperty[Define.VEN_PROC_VTN].ToString())
                    {
                        Flg_Normal = listaPerfil[i].SFlgPermitido == "1" ? true : false;
                        continue;
                    }
                }
                else if (listaPerfil[i].SCodModulo == Property.htProperty[Define.VEN_TURNO].ToString())
                {
                    if (listaPerfil[i].SFlgPermitido == "1")
                    {
                        intContTurno++;
                    }

                    if (listaPerfil[i].SCodProceso == Property.htProperty[Define.VEN_PROC_CIE].ToString())
                    {
                        Flg_Cierre = listaPerfil[i].SFlgPermitido == "1" ? true : false;
                        continue;
                    }
                    if (listaPerfil[i].SCodProceso == Property.htProperty[Define.VEN_PROC_CLV].ToString())
                    {
                        Flg_Clave = listaPerfil[i].SFlgPermitido == "1" ? true : false;
                        continue;
                    }
                    if (listaPerfil[i].SCodProceso == Property.htProperty[Define.VEN_PROC_CUA].ToString())
                    {
                        Flg_Cuadre = listaPerfil[i].SFlgPermitido == "1" ? true : false;
                        continue;
                    }

                }
            }
            if (intContTurno != 0)
            {
                trwTurno.ExpandAll();
                Flg_Turno = true;
            }
            if (intContOpera != 0)
            {
                trwOperacion.ExpandAll();
                Flg_Operacion = true;
            }
        }

        private void EjecutarPermisos()
        {
            if (!Flg_Operacion)
            {
                trwOperacion.Hide();
                trwOperacion.Enabled = false;

                this.olkbVentas.Buttons[0].Enabled = false;
            }
            else
            {
                TreeNode trnIngreso = trwOperacion.Nodes[0].Nodes[0];
                TreeNode trnEgreso = trwOperacion.Nodes[0].Nodes[1];
                TreeNode trnNormal = trwOperacion.Nodes[0].Nodes[2];
                TreeNode trnComVenta = trwOperacion.Nodes[0].Nodes[3];
                TreeNode trnMasivo = trwOperacion.Nodes[0].Nodes[4];
                if (!Flg_Ingreso)
                {
                    trwOperacion.Nodes.Remove(trnIngreso);
                }
                if (!Flg_Egreso)
                {
                    trwOperacion.Nodes.Remove(trnEgreso);
                }

                if (!Flg_Normal)
                {
                    trwOperacion.Nodes.Remove(trnNormal);
                }
                if (!Flg_CompraVenta)
                {
                    trwOperacion.Nodes.Remove(trnComVenta);
                }
                if (!Flg_Masivo)
                {
                    trwOperacion.Nodes.Remove(trnMasivo);
                }
            }

            if (!Flg_Turno)
            {
                trwTurno.Hide();
                trwTurno.Enabled = false;
                this.olkbVentas.Buttons[1].Enabled = false;
            }
            else
            {
                TreeNode trnCuadre = trwTurno.Nodes[0].Nodes[0];
                TreeNode trnCierre = trwTurno.Nodes[0].Nodes[1];
                TreeNode trnClave = trwTurno.Nodes[0].Nodes[2];
                if (!Flg_Cuadre)
                {
                    trwTurno.Nodes.Remove(trnCuadre);
                }
                if (!Flg_Cierre)
                {
                    trwTurno.Nodes.Remove(trnCierre);
                }
                if (!Flg_Clave)
                {
                    trwTurno.Nodes.Remove(trnClave);
                }
            }
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!Flg_Salir && Flg_Cierre)
            if (!Flg_Salir)
            {
                //if (!Flg_Bloqueado)
                //{
                MessageBox.Show("Para salir debe cerrar turno (F8).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                formLogueo.Limpiar();
                formLogueo.Show();
                //}
                //else { e.Cancel = false; }
            }
        }

        public void VerificarTurnoActivo()
        {
            if (!objBOTurno.EstaTurnoActivo(objUsuario.SCodUsuario))
            {
                MessageBox.Show((string)LabelConfig.htLabels["turno.msgCerrado"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flg_Salir = true;
                Close();
                formLogueo.Limpiar();
                formLogueo.Show();
            }
        }

        public void MostrarFooterTasas()
        {
            DataTable dtTasaCambio = objBOTurno.ListarTasaCambio();
            //ArrayList lista = new ArrayList();
            //string strTasas = "";
            //for (int i = 0; i < dtTasaCambio.Rows.Count; i++)
            //{
            //    strTasas += dtTasaCambio.Rows[i].ItemArray.GetValue(1).ToString();
            //    strTasas += "     ";
            //}
            //lista.Add(strTasas);
            //lbxTasa.DataSource = lista;
            lblComDol.Text = dtTasaCambio.Select("Cod_Moneda='DOL'").Length>0?dtTasaCambio.Select("Cod_Moneda='DOL'")[0].ItemArray.GetValue(1).ToString():"-";
            lblVenDol.Text = dtTasaCambio.Select("Cod_Moneda='DOL'").Length > 0 ? dtTasaCambio.Select("Cod_Moneda='DOL'")[0].ItemArray.GetValue(2).ToString() : "-";
            lblComEur.Text = dtTasaCambio.Select("Cod_Moneda='EUR'").Length > 0 ? dtTasaCambio.Select("Cod_Moneda='EUR'")[0].ItemArray.GetValue(1).ToString() : "-";
            lblVenEur.Text = dtTasaCambio.Select("Cod_Moneda='EUR'").Length > 0 ? dtTasaCambio.Select("Cod_Moneda='EUR'")[0].ItemArray.GetValue(2).ToString() : "-";
        }

        public void ActualizarTasa()
        {
            timerIdle = new System.Windows.Forms.Timer();
            timerIdle.Enabled = true;
            int intMaxInac = Int32.Parse(Property.htParametro["FTC"].ToString());
            timerIdle.Interval = intMaxInac * 1000;
            timerIdle.Tick += new EventHandler(timerIdle_Tick);
        }

        private void timerIdle_Tick(object sender, EventArgs e)
        {
            MostrarFooterTasas();
        }

        private void CargarAyuda()
        {
            Help.ShowHelp(this, AppDomain.CurrentDomain.BaseDirectory + "resources/AyudaVentas.chm", HelpNavigator.TableOfContents);
        }
    }
}