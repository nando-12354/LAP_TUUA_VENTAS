using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;

public partial class Ope_GestionMolinete : System.Web.UI.Page
{
    protected bool Flg_Error;
    protected Hashtable htLabels;
    BO_Consultas objConsultas = new BO_Consultas();
    BO_Operacion objOperacion = new BO_Operacion();
    BO_Administracion objWBAdministracion = new BO_Administracion();
    UIControles objCargaCombo = new UIControles();
    DataTable dt_consultaIp = new DataTable();
    bool flgValidIp;
    int ActMolinete1, ActMolinete2, ActMolinete3, ActMolinete4, ActMolinete5, ActMolinete6;
    string IPBLoque1, IPBLoque2, IPBLoque3, IPBLoque4, IPFinal;
    string FrmCodMolinete="";
    string FrmDescripcion="";
    string FrmIp="";
    string FrmTipoDocumento="";
    string FrmTipoVuelo="";
    string FrmTipoAcceso="";
    string FrmEstado="";
    string FrmDBName="";
    string FrmDBUser="";
    string FrmDBPassword = "";
    string FrmEstaMaster = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Flg_Error = false;
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                this.btnGrabar.Text = htLabels["madmEditarPuntoVenta.btnActualizar.Text"].ToString();

                this.lblCodigo1.Text = htLabels["moperacionMolinete.lblCodigo"].ToString();
                this.lblDescripcion1.Text = htLabels["moperacionMolinete.lblDescripcion"].ToString();
                this.lblDscIp1.Text = htLabels["moperacionMolinete.lblDscIp1"].ToString();
                this.lblTipoDocumento1.Text = htLabels["moperacionMolinete.lblTipoDocumento"].ToString();
                this.lblTipoVuelo1.Text = htLabels["moperacionMolinete.lblTipoVuelo"].ToString();
                this.lblTipoAcceso1.Text = htLabels["moperacionMolinete.lblTipoAcceso"].ToString();
                this.lblEstado1.Text = htLabels["moperacionMolinete.lblEstado"].ToString();
                this.lblEstMaster1.Text = htLabels["moperacionMolinete.lblEstMaster"].ToString();
                this.lblDBName1.Text = htLabels["moperacionMolinete.lblDBName"].ToString();
                this.lblDBUser1.Text = htLabels["moperacionMolinete.lblDBUser"].ToString();
                this.lblDBPassword1.Text = htLabels["moperacionMolinete.lblDBPassword"].ToString();


                this.lblCodigo2.Text = htLabels["moperacionMolinete.lblCodigo"].ToString();
                this.lblDescripcion2.Text = htLabels["moperacionMolinete.lblDescripcion"].ToString();
                this.lblDscIp2.Text = htLabels["moperacionMolinete.lblDscIp1"].ToString();
                this.lblTipoDocumento2.Text = htLabels["moperacionMolinete.lblTipoDocumento"].ToString();
                this.lblTipoVuelo2.Text = htLabels["moperacionMolinete.lblTipoVuelo"].ToString();
                this.lblTipoAcceso2.Text = htLabels["moperacionMolinete.lblTipoAcceso"].ToString();
                this.lblEstado2.Text = htLabels["moperacionMolinete.lblEstado"].ToString();
                this.lblEstMaster2.Text = htLabels["moperacionMolinete.lblEstMaster"].ToString();
                this.lblDBName2.Text = htLabels["moperacionMolinete.lblDBName"].ToString();
                this.lblDBUser2.Text = htLabels["moperacionMolinete.lblDBUser"].ToString();
                this.lblDBPassword2.Text = htLabels["moperacionMolinete.lblDBPassword"].ToString();


                this.lblCodigo3.Text = htLabels["moperacionMolinete.lblCodigo"].ToString();
                this.lblDescripcion3.Text = htLabels["moperacionMolinete.lblDescripcion"].ToString();
                this.lblDscIp3.Text = htLabels["moperacionMolinete.lblDscIp1"].ToString();
                this.lblTipoDocumento3.Text = htLabels["moperacionMolinete.lblTipoDocumento"].ToString();
                this.lblTipoVuelo3.Text = htLabels["moperacionMolinete.lblTipoVuelo"].ToString();
                this.lblTipoAcceso3.Text = htLabels["moperacionMolinete.lblTipoAcceso"].ToString();
                this.lblEstado3.Text = htLabels["moperacionMolinete.lblEstado"].ToString();
                this.lblEstMaster3.Text = htLabels["moperacionMolinete.lblEstMaster"].ToString();
                this.lblDBName3.Text = htLabels["moperacionMolinete.lblDBName"].ToString();
                this.lblDBUser3.Text = htLabels["moperacionMolinete.lblDBUser"].ToString();
                this.lblDBPassword3.Text = htLabels["moperacionMolinete.lblDBPassword"].ToString();

                this.lblCodigo4.Text = htLabels["moperacionMolinete.lblCodigo"].ToString();
                this.lblDescripcion4.Text = htLabels["moperacionMolinete.lblDescripcion"].ToString();
                this.lblDscIp4.Text = htLabels["moperacionMolinete.lblDscIp1"].ToString();
                this.lblTipoDocumento4.Text = htLabels["moperacionMolinete.lblTipoDocumento"].ToString();
                this.lblTipoVuelo4.Text = htLabels["moperacionMolinete.lblTipoVuelo"].ToString();
                this.lblTipoAcceso4.Text = htLabels["moperacionMolinete.lblTipoAcceso"].ToString();
                this.lblEstado4.Text = htLabels["moperacionMolinete.lblEstado"].ToString();
                this.lblEstMaster4.Text = htLabels["moperacionMolinete.lblEstMaster"].ToString();
                this.lblDBName4.Text = htLabels["moperacionMolinete.lblDBName"].ToString();
                this.lblDBUser4.Text = htLabels["moperacionMolinete.lblDBUser"].ToString();
                this.lblDBPassword4.Text = htLabels["moperacionMolinete.lblDBPassword"].ToString();
                
                this.lblCodigo5.Text = htLabels["moperacionMolinete.lblCodigo"].ToString();
                this.lblDescripcion5.Text = htLabels["moperacionMolinete.lblDescripcion"].ToString();
                this.lblDscIp5.Text = htLabels["moperacionMolinete.lblDscIp1"].ToString();
                this.lblTipoDocumento5.Text = htLabels["moperacionMolinete.lblTipoDocumento"].ToString();
                this.lblTipoVuelo5.Text = htLabels["moperacionMolinete.lblTipoVuelo"].ToString();
                this.lblTipoAcceso5.Text = htLabels["moperacionMolinete.lblTipoAcceso"].ToString();
                this.lblEstado5.Text = htLabels["moperacionMolinete.lblEstado"].ToString();
                this.lblEstMaster5.Text = htLabels["moperacionMolinete.lblEstMaster"].ToString();
                this.lblDBName5.Text = htLabels["moperacionMolinete.lblDBName"].ToString();
                this.lblDBUser5.Text = htLabels["moperacionMolinete.lblDBUser"].ToString();
                this.lblDBPassword5.Text = htLabels["moperacionMolinete.lblDBPassword"].ToString();

                this.lblCodigo6.Text = htLabels["moperacionMolinete.lblCodigo"].ToString();
                this.lblDescripcion6.Text = htLabels["moperacionMolinete.lblDescripcion"].ToString();
                this.lblDscIp6.Text = htLabels["moperacionMolinete.lblDscIp1"].ToString();
                this.lblTipoDocumento6.Text = htLabels["moperacionMolinete.lblTipoDocumento"].ToString();
                this.lblTipoVuelo6.Text = htLabels["moperacionMolinete.lblTipoVuelo"].ToString();
                this.lblTipoAcceso6.Text = htLabels["moperacionMolinete.lblTipoAcceso"].ToString();
                this.lblEstado6.Text = htLabels["moperacionMolinete.lblEstado"].ToString();
                this.lblEstMaster6.Text = htLabels["moperacionMolinete.lblEstMaster"].ToString();
                this.lblDBName6.Text = htLabels["moperacionMolinete.lblDBName"].ToString();
                this.lblDBUser6.Text = htLabels["moperacionMolinete.lblDBUser"].ToString();
                this.lblDBPassword6.Text = htLabels["moperacionMolinete.lblDBPassword"].ToString();

            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            CargarCombos();
            CargarFormulario();
            /*txtDBPassword1.Attributes.Add("value",txtDBPassword1.Text);
            txtDBPassword2.Attributes.Add("value",txtDBPassword2.Text);
            txtDBPassword3.Attributes.Add("value",txtDBPassword3.Text);
            txtDBPassword4.Attributes.Add("value",txtDBPassword4.Text);
            txtDBPassword5.Attributes.Add("value",txtDBPassword5.Text);
            txtDBPassword6.Attributes.Add("value",txtDBPassword6.Text);*/

        }
        txtDBPassword1.Attributes.Add("value", txtDBPassword1.Text);
        txtDBPassword2.Attributes.Add("value", txtDBPassword2.Text);
        txtDBPassword3.Attributes.Add("value", txtDBPassword3.Text);
        txtDBPassword4.Attributes.Add("value", txtDBPassword4.Text);
        txtDBPassword5.Attributes.Add("value", txtDBPassword5.Text);
        txtDBPassword6.Attributes.Add("value", txtDBPassword6.Text);
    }


    public void CargarCombos()
    {
        /*----------------------------- Molinete 1 ------------------------------------------------*/
        DataTable dt_tipodocumento = new DataTable();
        dt_tipodocumento = objConsultas.ListaCamposxNombre("TipoDocumento");
        objCargaCombo.LlenarCombo(ddlTipoDocumento1, dt_tipodocumento, "Cod_Campo", "Dsc_Campo", true, false);

        DataTable dt_tipovuelo = new DataTable();
        dt_tipovuelo = objConsultas.ListaCamposxNombre("TipoVuelo");
        objCargaCombo.LlenarCombo(ddlTipoVuelo1, dt_tipovuelo, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_tipoacceso = new DataTable();
        dt_tipoacceso = objConsultas.ListaCamposxNombre("TipAccesoMolinete");
        objCargaCombo.LlenarCombo(ddlTipoAcceso1, dt_tipoacceso, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_estado = new DataTable();
        dt_estado = objConsultas.ListaCamposxNombre("EstadoMolinete");
        objCargaCombo.LlenarCombo(ddlEstado1, dt_estado, "Cod_Campo", "Dsc_Campo", false, false);


        /*----------------------------- Molinete 2 ------------------------------------------------*/
        DataTable dt_tipodocumento2 = new DataTable();
        dt_tipodocumento2 = objConsultas.ListaCamposxNombre("TipoDocumento");
        objCargaCombo.LlenarCombo(ddlTipoDocumento2, dt_tipodocumento2, "Cod_Campo", "Dsc_Campo", true, false);

        DataTable dt_tipovuelo2 = new DataTable();
        dt_tipovuelo2 = objConsultas.ListaCamposxNombre("TipoVuelo");
        objCargaCombo.LlenarCombo(ddlTipoVuelo2, dt_tipovuelo2, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_tipoacceso2 = new DataTable();
        dt_tipoacceso2 = objConsultas.ListaCamposxNombre("TipAccesoMolinete");
        objCargaCombo.LlenarCombo(ddlTipoAcceso2, dt_tipoacceso2, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_estado2 = new DataTable();
        dt_estado2 = objConsultas.ListaCamposxNombre("EstadoMolinete");
        objCargaCombo.LlenarCombo(ddlEstado2, dt_estado2, "Cod_Campo", "Dsc_Campo", false, false);



        /*----------------------------- Molinete 3 ------------------------------------------------*/
        DataTable dt_tipodocumento3 = new DataTable();
        dt_tipodocumento3 = objConsultas.ListaCamposxNombre("TipoDocumento");
        objCargaCombo.LlenarCombo(ddlTipoDocumento3, dt_tipodocumento3, "Cod_Campo", "Dsc_Campo", true, false);

        DataTable dt_tipovuelo3 = new DataTable();
        dt_tipovuelo3 = objConsultas.ListaCamposxNombre("TipoVuelo");
        objCargaCombo.LlenarCombo(ddlTipoVuelo3, dt_tipovuelo3, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_tipoacceso3 = new DataTable();
        dt_tipoacceso3 = objConsultas.ListaCamposxNombre("TipAccesoMolinete");
        objCargaCombo.LlenarCombo(ddlTipoAcceso3, dt_tipoacceso3, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_estado3 = new DataTable();
        dt_estado3 = objConsultas.ListaCamposxNombre("EstadoMolinete");
        objCargaCombo.LlenarCombo(ddlEstado3, dt_estado3, "Cod_Campo", "Dsc_Campo", false, false);



        /*----------------------------- Molinete 4 ------------------------------------------------*/
        DataTable dt_tipodocumento4 = new DataTable();
        dt_tipodocumento4 = objConsultas.ListaCamposxNombre("TipoDocumento");
        objCargaCombo.LlenarCombo(ddlTipoDocumento4, dt_tipodocumento4, "Cod_Campo", "Dsc_Campo", true, false);

        DataTable dt_tipovuelo4 = new DataTable();
        dt_tipovuelo4 = objConsultas.ListaCamposxNombre("TipoVuelo");
        objCargaCombo.LlenarCombo(ddlTipoVuelo4, dt_tipovuelo4, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_tipoacceso4 = new DataTable();
        dt_tipoacceso4 = objConsultas.ListaCamposxNombre("TipAccesoMolinete");
        objCargaCombo.LlenarCombo(ddlTipoAcceso4, dt_tipoacceso4, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_estado4 = new DataTable();
        dt_estado4 = objConsultas.ListaCamposxNombre("EstadoMolinete");
        objCargaCombo.LlenarCombo(ddlEstado4, dt_estado4, "Cod_Campo", "Dsc_Campo", false, false);


        /*----------------------------- Molinete 5 ------------------------------------------------*/
        DataTable dt_tipodocumento5 = new DataTable();
        dt_tipodocumento5 = objConsultas.ListaCamposxNombre("TipoDocumento");
        objCargaCombo.LlenarCombo(ddlTipoDocumento5, dt_tipodocumento5, "Cod_Campo", "Dsc_Campo", true, false);

        DataTable dt_tipovuelo5 = new DataTable();
        dt_tipovuelo5 = objConsultas.ListaCamposxNombre("TipoVuelo");
        objCargaCombo.LlenarCombo(ddlTipoVuelo5, dt_tipovuelo5, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_tipoacceso5 = new DataTable();
        dt_tipoacceso5 = objConsultas.ListaCamposxNombre("TipAccesoMolinete");
        objCargaCombo.LlenarCombo(ddlTipoAcceso5, dt_tipoacceso5, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_estado5 = new DataTable();
        dt_estado5 = objConsultas.ListaCamposxNombre("EstadoMolinete");
        objCargaCombo.LlenarCombo(ddlEstado5, dt_estado5, "Cod_Campo", "Dsc_Campo", false, false);



        /*----------------------------- Molinete 6 ------------------------------------------------*/
        DataTable dt_tipodocumento6 = new DataTable();
        dt_tipodocumento6 = objConsultas.ListaCamposxNombre("TipoDocumento");
        objCargaCombo.LlenarCombo(ddlTipoDocumento6, dt_tipodocumento6, "Cod_Campo", "Dsc_Campo", true, false);

        DataTable dt_tipovuelo6 = new DataTable();
        dt_tipovuelo6 = objConsultas.ListaCamposxNombre("TipoVuelo");
        objCargaCombo.LlenarCombo(ddlTipoVuelo6, dt_tipovuelo6, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_tipoacceso6 = new DataTable();
        dt_tipoacceso6 = objConsultas.ListaCamposxNombre("TipAccesoMolinete");
        objCargaCombo.LlenarCombo(ddlTipoAcceso6, dt_tipoacceso6, "Cod_Campo", "Dsc_Campo", false, false);

        DataTable dt_estado6 = new DataTable();
        dt_estado6 = objConsultas.ListaCamposxNombre("EstadoMolinete");
        objCargaCombo.LlenarCombo(ddlEstado6, dt_estado6, "Cod_Campo", "Dsc_Campo", false, false);
    }



    public void CargarFormulario()
    {
        Flg_Error = false;
        try
        { 

            DataTable dt_molinetes = objOperacion.ListarMolinetes(null,null);

            for (int i = 0; i <= dt_molinetes.Rows.Count; i++)
            {
                if (i == 0)
                {
                    lblCodMolinete1.Text = dt_molinetes.Rows[i].ItemArray.GetValue(0).ToString();
                    string[] bip = dt_molinetes.Rows[i].ItemArray.GetValue(1).ToString().Split('.');
                    txtDscIP11.Text= bip[0];
                    txtDscIP12.Text= bip[1];
                    txtDscIP13.Text= bip[2];
                    txtDscIP14.Text= bip[3];                 
                    txtDescripcion1.Text = dt_molinetes.Rows[i].ItemArray.GetValue(2).ToString();
                    ddlTipoDocumento1.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(3).ToString();
                    ddlTipoVuelo1.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(4).ToString();
                    ddlTipoAcceso1.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(5).ToString();
                    ddlEstado1.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(6).ToString();
                    rbtEstMaster1.Checked = dt_molinetes.Rows[i].ItemArray.GetValue(11).ToString() == "1" ? true : false;
                    txtDBName1.Text = dt_molinetes.Rows[i].ItemArray.GetValue(12).ToString();
                    txtDBUser1.Text = dt_molinetes.Rows[i].ItemArray.GetValue(13).ToString();
                    txtDBPassword1.Text = dt_molinetes.Rows[i].ItemArray.GetValue(14).ToString();
                }

                if (i == 1)
                {
                    lblCodMolinete2.Text = dt_molinetes.Rows[i].ItemArray.GetValue(0).ToString();
                    string[] bip = dt_molinetes.Rows[i].ItemArray.GetValue(1).ToString().Split('.');
                    txtDscIP21.Text = bip[0];
                    txtDscIP22.Text = bip[1];
                    txtDscIP23.Text = bip[2];
                    txtDscIP24.Text = bip[3];                    
                    txtDescripcion2.Text = dt_molinetes.Rows[i].ItemArray.GetValue(2).ToString();
                    ddlTipoDocumento2.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(3).ToString();
                    ddlTipoVuelo2.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(4).ToString();
                    ddlTipoAcceso2.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(5).ToString();
                    ddlEstado2.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(6).ToString();
                    rbtEstMaster2.Checked = dt_molinetes.Rows[i].ItemArray.GetValue(11).ToString() == "1" ? true : false;
                    txtDBName2.Text = dt_molinetes.Rows[i].ItemArray.GetValue(12).ToString();
                    txtDBUser2.Text = dt_molinetes.Rows[i].ItemArray.GetValue(13).ToString();
                    txtDBPassword2.Text = dt_molinetes.Rows[i].ItemArray.GetValue(14).ToString();
                }

                if (i == 2)
                {
                    lblCodMolinete3.Text = dt_molinetes.Rows[i].ItemArray.GetValue(0).ToString();
                    string[] bip = dt_molinetes.Rows[i].ItemArray.GetValue(1).ToString().Split('.');
                    txtDscIP31.Text = bip[0];
                    txtDscIP32.Text = bip[1];
                    txtDscIP33.Text = bip[2];
                    txtDscIP34.Text = bip[3];                    
                    txtDescripcion3.Text = dt_molinetes.Rows[i].ItemArray.GetValue(2).ToString();
                    ddlTipoDocumento3.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(3).ToString();
                    ddlTipoVuelo3.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(4).ToString();
                    ddlTipoAcceso3.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(5).ToString();
                    ddlEstado3.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(6).ToString();
                    rbtEstMaster3.Checked = dt_molinetes.Rows[i].ItemArray.GetValue(11).ToString() == "1" ? true : false;
                    txtDBName3.Text = dt_molinetes.Rows[i].ItemArray.GetValue(12).ToString();
                    txtDBUser3.Text = dt_molinetes.Rows[i].ItemArray.GetValue(13).ToString();
                    txtDBPassword3.Text = dt_molinetes.Rows[i].ItemArray.GetValue(14).ToString();
                }

                if (i == 3)
                {
                    lblCodMolinete4.Text = dt_molinetes.Rows[i].ItemArray.GetValue(0).ToString();
                    string[] bip = dt_molinetes.Rows[i].ItemArray.GetValue(1).ToString().Split('.');
                    txtDscIP41.Text = bip[0];
                    txtDscIP42.Text = bip[1];
                    txtDscIP43.Text = bip[2];
                    txtDscIP44.Text = bip[3];                    
                    txtDescripcion4.Text = dt_molinetes.Rows[i].ItemArray.GetValue(2).ToString();
                    ddlTipoDocumento4.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(3).ToString();
                    ddlTipoVuelo4.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(4).ToString();
                    ddlTipoAcceso4.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(5).ToString();
                    ddlEstado4.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(6).ToString();
                    rbtEstMaster4.Checked = dt_molinetes.Rows[i].ItemArray.GetValue(11).ToString() == "1" ? true : false;
                    txtDBName4.Text = dt_molinetes.Rows[i].ItemArray.GetValue(12).ToString();
                    txtDBUser4.Text = dt_molinetes.Rows[i].ItemArray.GetValue(13).ToString();
                    txtDBPassword4.Text = dt_molinetes.Rows[i].ItemArray.GetValue(14).ToString();
                }

                if (i == 4)
                {
                    lblCodMolinete5.Text = dt_molinetes.Rows[i].ItemArray.GetValue(0).ToString();
                    string[] bip = dt_molinetes.Rows[i].ItemArray.GetValue(1).ToString().Split('.');
                    txtDscIP51.Text = bip[0];
                    txtDscIP52.Text = bip[1];
                    txtDscIP53.Text = bip[2];
                    txtDscIP54.Text = bip[3];                    
                    txtDescripcion5.Text = dt_molinetes.Rows[i].ItemArray.GetValue(2).ToString();
                    ddlTipoDocumento5.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(3).ToString();
                    ddlTipoVuelo5.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(4).ToString();
                    ddlTipoAcceso5.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(5).ToString();
                    ddlEstado5.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(6).ToString();
                    rbtEstMaster5.Checked = dt_molinetes.Rows[i].ItemArray.GetValue(11).ToString() == "1" ? true : false;
                    txtDBName5.Text = dt_molinetes.Rows[i].ItemArray.GetValue(12).ToString();
                    txtDBUser5.Text = dt_molinetes.Rows[i].ItemArray.GetValue(13).ToString();
                    txtDBPassword5.Text = dt_molinetes.Rows[i].ItemArray.GetValue(14).ToString();
                }

                if (i == 5)
                {
                    lblCodMolinete6.Text = dt_molinetes.Rows[i].ItemArray.GetValue(0).ToString();
                    string[] bip = dt_molinetes.Rows[i].ItemArray.GetValue(1).ToString().Split('.');
                    txtDscIP61.Text = bip[0];
                    txtDscIP62.Text = bip[1];
                    txtDscIP63.Text = bip[2];
                    txtDscIP64.Text = bip[3];                    
                    txtDescripcion6.Text = dt_molinetes.Rows[i].ItemArray.GetValue(2).ToString();
                    ddlTipoDocumento6.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(3).ToString();
                    ddlTipoVuelo6.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(4).ToString();
                    ddlTipoAcceso6.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(5).ToString();
                    ddlEstado6.SelectedValue = dt_molinetes.Rows[i].ItemArray.GetValue(6).ToString();
                    rbtEstMaster6.Checked = dt_molinetes.Rows[i].ItemArray.GetValue(11).ToString() == "1" ? true : false;
                    txtDBName6.Text = dt_molinetes.Rows[i].ItemArray.GetValue(12).ToString();
                    txtDBUser6.Text = dt_molinetes.Rows[i].ItemArray.GetValue(13).ToString();
                    txtDBPassword6.Text = dt_molinetes.Rows[i].ItemArray.GetValue(14).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }


    private void ValidarIp(string sIpB1, string sIPB2, string sIPB3, string sIPB4,string sCodMolinete)
    {

        if (sIpB1 == "")
        {
            lblMensaje.Text = "Ip incorrecta (Rango de 1 a 255)";
            IPBLoque1 = "";
        }
        else
        {
            if (Convert.ToInt32(sIpB1) > 0 && Convert.ToInt32(sIpB1) < 256)
            {
                IPBLoque1 = Convert.ToString(Convert.ToInt32(sIpB1));
            }
            else
            {

                lblMensaje.Text = "Ip incorrecta (Rango de 1 a 255)";
                IPBLoque1 = "";
            }
        }



        if (sIPB2 == "")
        {
            lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            IPBLoque2 = "";
        }
        else
        {
            if (Convert.ToInt32(sIPB2) >= 0 && Convert.ToInt32(sIPB2) < 256)
            {
                IPBLoque2 = Convert.ToString(Convert.ToInt32(sIPB2));
            }
            else
            {
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                IPBLoque2 = "";
            }
        }


        if (sIPB3 == "")
        {
            lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            IPBLoque3 = "";
        }
        else
        {
            if (Convert.ToInt32(sIPB3) >= 0 && Convert.ToInt32(sIPB3) < 256)
            {
                IPBLoque3 = Convert.ToString(Convert.ToInt32(sIPB3));
            }
            else
            {
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                IPBLoque3 = "";
            }
        }



        if (sIPB4 == "")
        {
            lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            IPBLoque4 = "";
        }
        else
        {
            if (Convert.ToInt32(sIPB4) >= 0 && Convert.ToInt32(sIPB4) < 256)
            {
                IPBLoque4 = Convert.ToString(Convert.ToInt32(sIPB4));
            }
            else
            {
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                IPBLoque4 = "";
            }
        }

        if (IPBLoque1 != "" && IPBLoque2 != "" && IPBLoque3 != "" && IPBLoque4 != "")
        {
            IPFinal = IPBLoque1 + "." + IPBLoque2 + "." + IPBLoque3 + "." + IPBLoque4;

            //dt_consultaIp = objOperacion.ListarMolinetes(sCodMolinete,null);  //Poner esto para validar IP
            //dt_consultaIp = objWBAdministracion.obtenerDetallePuntoVenta(null, IPFinal);

            //if (dt_consultaIp.Rows.Count < 1)
            //{
                this.lblMensaje.Text = "";
                flgValidIp = true;
            //}
            //else
            //{
            //    this.lblMensaje.Text = "La dirección IP ya se encuentra registrada";
            //    flgValidIp = false;
            //}
        }
        else
        {
            flgValidIp = false;
        }
    }


    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt_NumMolinetes = objOperacion.ListarMolinetes(null,null);            
            for (int j = 0; j <= dt_NumMolinetes.Rows.Count; j++)
            {
                if (j == 0)
                {
                        ValidarIp(txtDscIP11.Text, txtDscIP12.Text, txtDscIP13.Text, txtDscIP14.Text, dt_NumMolinetes.Rows[j].ItemArray.GetValue(0).ToString());
                        if (flgValidIp == true)
                        {
                            FrmCodMolinete = FrmCodMolinete + lblCodMolinete1.Text + "|";
                            FrmDescripcion = FrmDescripcion + txtDescripcion1.Text + "|";
                            FrmIp = FrmIp + IPFinal + "|";
                            FrmTipoDocumento = FrmTipoDocumento + ddlTipoDocumento1.SelectedValue + "|";
                            FrmTipoVuelo = FrmTipoVuelo + ddlTipoVuelo1.SelectedValue + "|";
                            FrmTipoAcceso = FrmTipoAcceso + ddlTipoAcceso1.SelectedValue + "|";
                            FrmEstado = FrmEstado + ddlEstado1.SelectedValue + "|";
                            FrmDBName = FrmDBName + txtDBName1.Text + "|";
                            FrmDBUser = FrmDBUser + txtDBUser1.Text + "|";
                            FrmDBPassword = FrmDBPassword + txtDBPassword1.Text + "|";
                            string sMaster1 = rbtEstMaster1.Checked == true ? "1" : "0";
                            FrmEstaMaster = FrmEstaMaster + sMaster1 + "|";

                        }
                        else
                        {
                            flgValidIp = false;
                            lblMensaje.Text = "";
                            j = dt_NumMolinetes.Rows.Count + 2;
                            lblMensajeError1.Text = "Direccion de IP incorrecta";
                        }
                    
                }

                if (j == 1)
                {

                        ValidarIp(txtDscIP21.Text, txtDscIP22.Text, txtDscIP23.Text, txtDscIP24.Text, dt_NumMolinetes.Rows[j].ItemArray.GetValue(0).ToString());
                        if (flgValidIp == true)
                        {
                            FrmCodMolinete = FrmCodMolinete + lblCodMolinete2.Text + "|";
                            FrmDescripcion = FrmDescripcion + txtDescripcion2.Text + "|";
                            FrmIp = FrmIp + IPFinal + "|";
                            FrmTipoDocumento = FrmTipoDocumento + ddlTipoDocumento2.SelectedValue + "|";
                            FrmTipoVuelo = FrmTipoVuelo + ddlTipoVuelo2.SelectedValue + "|";
                            FrmTipoAcceso = FrmTipoAcceso + ddlTipoAcceso2.SelectedValue + "|";
                            FrmEstado = FrmEstado + ddlEstado2.SelectedValue + "|";
                            FrmDBName = FrmDBName + txtDBName2.Text + "|";
                            FrmDBUser = FrmDBUser + txtDBUser2.Text + "|";
                            FrmDBPassword = FrmDBPassword + txtDBPassword2.Text + "|";
                            string sMaster2 = rbtEstMaster2.Checked == true ? "1" : "0";
                            FrmEstaMaster = FrmEstaMaster + sMaster2 + "|";

                        }
                        else
                        {
                            flgValidIp = false;
                            lblMensaje.Text = "";
                            j = dt_NumMolinetes.Rows.Count + 2;
                            lblMensajeError2.Text = "Direccion de IP incorrecta";
                        }
                    
                }

                if (j == 2)
                {

                        ValidarIp(txtDscIP31.Text, txtDscIP32.Text, txtDscIP33.Text, txtDscIP34.Text, dt_NumMolinetes.Rows[j].ItemArray.GetValue(0).ToString());
                        if (flgValidIp == true)
                        {
                            FrmCodMolinete = FrmCodMolinete + lblCodMolinete3.Text + "|";
                            FrmDescripcion = FrmDescripcion + txtDescripcion3.Text + "|";
                            FrmIp = FrmIp + IPFinal + "|";
                            FrmTipoDocumento = FrmTipoDocumento + ddlTipoDocumento3.SelectedValue + "|";
                            FrmTipoVuelo = FrmTipoVuelo + ddlTipoVuelo3.SelectedValue + "|";
                            FrmTipoAcceso = FrmTipoAcceso + ddlTipoAcceso3.SelectedValue + "|";
                            FrmEstado = FrmEstado + ddlEstado3.SelectedValue + "|";
                            FrmDBName = FrmDBName + txtDBName3.Text + "|";
                            FrmDBUser = FrmDBUser + txtDBUser3.Text + "|";
                            FrmDBPassword = FrmDBPassword + txtDBPassword3.Text + "|";
                            string sMaster3 = rbtEstMaster3.Checked == true ? "1" : "0";
                            FrmEstaMaster = FrmEstaMaster + sMaster3 + "|";

                        }
                        else
                        {
                            flgValidIp = false;
                            lblMensaje.Text = "";
                            j = dt_NumMolinetes.Rows.Count + 2;
                            lblMensajeError3.Text = "Direccion de IP incorrecta";
                        }
                    
                }

                if (j == 3)
                {

                        ValidarIp(txtDscIP41.Text, txtDscIP42.Text, txtDscIP43.Text, txtDscIP44.Text, dt_NumMolinetes.Rows[j].ItemArray.GetValue(0).ToString());
                        if (flgValidIp == true)
                        {
                            FrmCodMolinete = FrmCodMolinete + lblCodMolinete4.Text + "|";
                            FrmDescripcion = FrmDescripcion + txtDescripcion4.Text + "|";
                            FrmIp = FrmIp + IPFinal + "|";
                            FrmTipoDocumento = FrmTipoDocumento + ddlTipoDocumento4.SelectedValue + "|";
                            FrmTipoVuelo = FrmTipoVuelo + ddlTipoVuelo4.SelectedValue + "|";
                            FrmTipoAcceso = FrmTipoAcceso + ddlTipoAcceso4.SelectedValue + "|";
                            FrmEstado = FrmEstado + ddlEstado4.SelectedValue + "|";
                            FrmDBName = FrmDBName + txtDBName4.Text + "|";
                            FrmDBUser = FrmDBUser + txtDBUser4.Text + "|";
                            FrmDBPassword = FrmDBPassword + txtDBPassword4.Text + "|";
                            string sMaster4 = rbtEstMaster4.Checked == true ? "1" : "0";
                            FrmEstaMaster = FrmEstaMaster + sMaster4 + "|";

                        }
                        else
                        {
                            flgValidIp = false;
                            lblMensaje.Text = "";
                            j = dt_NumMolinetes.Rows.Count + 2;
                            lblMensajeError4.Text = "Direccion de IP incorrecta";
                        }
                    
                }

                if (j == 4)
                {

                        ValidarIp(txtDscIP51.Text, txtDscIP52.Text, txtDscIP53.Text, txtDscIP54.Text, dt_NumMolinetes.Rows[j].ItemArray.GetValue(0).ToString());
                        if (flgValidIp == true)
                        {
                            FrmCodMolinete = FrmCodMolinete + lblCodMolinete5.Text + "|";
                            FrmDescripcion = FrmDescripcion + txtDescripcion5.Text + "|";
                            FrmIp = FrmIp + IPFinal + "|";
                            FrmTipoDocumento = FrmTipoDocumento + ddlTipoDocumento5.SelectedValue + "|";
                            FrmTipoVuelo = FrmTipoVuelo + ddlTipoVuelo5.SelectedValue + "|";
                            FrmTipoAcceso = FrmTipoAcceso + ddlTipoAcceso5.SelectedValue + "|";
                            FrmEstado = FrmEstado + ddlEstado5.SelectedValue + "|";
                            FrmDBName = FrmDBName + txtDBName5.Text + "|";
                            FrmDBUser = FrmDBUser + txtDBUser5.Text + "|";
                            FrmDBPassword = FrmDBPassword + txtDBPassword5.Text + "|";
                            string sMaster5 = rbtEstMaster5.Checked == true ? "1" : "0";
                            FrmEstaMaster = FrmEstaMaster + sMaster5 + "|";

                        }
                        else
                        {
                            flgValidIp = false;
                            lblMensaje.Text = "";
                            j = dt_NumMolinetes.Rows.Count + 2;
                            lblMensajeError5.Text = "Direccion de IP incorrecta";
                        }
                    
                }

                if (j == 5)
                {
                        ValidarIp(txtDscIP61.Text, txtDscIP62.Text, txtDscIP63.Text, txtDscIP64.Text, dt_NumMolinetes.Rows[j].ItemArray.GetValue(0).ToString());
                        if (flgValidIp == true)
                        {
                            FrmCodMolinete = FrmCodMolinete + lblCodMolinete6.Text + "|";
                            FrmDescripcion = FrmDescripcion + txtDescripcion6.Text + "|";
                            FrmIp = FrmIp + IPFinal + "|";
                            FrmTipoDocumento = FrmTipoDocumento + ddlTipoDocumento6.SelectedValue + "|";
                            FrmTipoVuelo = FrmTipoVuelo + ddlTipoVuelo6.SelectedValue + "|";
                            FrmTipoAcceso = FrmTipoAcceso + ddlTipoAcceso6.SelectedValue + "|";
                            FrmEstado = FrmEstado + ddlEstado6.SelectedValue + "|";
                            FrmDBName = FrmDBName + txtDBName6.Text + "|";
                            FrmDBUser = FrmDBUser + txtDBUser6.Text + "|";
                            FrmDBPassword = FrmDBPassword + txtDBPassword6.Text + "|";
                            string sMaster6 = rbtEstMaster6.Checked == true ? "1" : "0";
                            FrmEstaMaster = FrmEstaMaster + sMaster6 + "|";

                        }
                        else
                        {
                            flgValidIp = false;
                            lblMensaje.Text = "";
                            j = dt_NumMolinetes.Rows.Count + 2;
                            lblMensajeError6.Text = "Direccion de IP incorrecta";
                        }
                    
                }
            }


            if (flgValidIp == true)
            {
                string[] NumIp = FrmIp.Split('|');
                bool bFlag = false;
                for (int k = 0; k < NumIp.Length - 1; k++)
                {
                    if (bFlag == false)
                    {
                        for (int m = 0; m < NumIp.Length - 1; m++)
                        {
                            if (k == m)
                            { m = m + 1; }
                            if (NumIp[k] == NumIp[m])
                            {
                                bFlag = true;
                                k = (k == (NumIp.Length - 1)) ? (k = k + 2) : (k = m + 1);
                                lblMensaje.Text = "La IP del molinete " + k + " se encuentra duplicada";
                                lblMensajeError1.Text = "";
                                lblMensajeError2.Text = "";
                                lblMensajeError3.Text = "";
                                lblMensajeError4.Text = "";
                                lblMensajeError5.Text = "";
                                lblMensajeError6.Text = "";
                                break;
                            }
                            else
                            {
                                bFlag = false;
                            }
                        }
                    }
                }


                if (bFlag == false)
                {
                    //SETEADO EN DURO EL TIPO DE MOLINETE EN F
                    Molinete objActualizarAllMolinetes = new Molinete(FrmCodMolinete, FrmIp, FrmDescripcion, FrmTipoDocumento, FrmTipoVuelo, FrmTipoAcceso, FrmEstado, Session["Cod_Usuario"].ToString(), null, null, FrmEstaMaster, FrmDBName, FrmDBUser, FrmDBPassword, dt_NumMolinetes.Rows.Count, "F");
                    objOperacion = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

                    try
                    {
                        if (objOperacion.actualizarMolinete(objActualizarAllMolinetes) == true)
                        {
                            lblMensaje.Text = "";

                            //GeneraAlarma
                            string IpClient = Request.UserHostAddress;
                            GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000035", "004", IpClient, "1", "Alerta W0000035", "Actualizacion de la configuracion del Molinete, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                            omb.ShowMessage("Molinete actualizado correctamente", "Actualización de Molinete");
                        }

                    }
                    catch (Exception exec)
                    {
                        lblMensaje.Text = "Las direcciones de IP deben pertenece a la red";
                    }
                }

            }

        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }
    

}