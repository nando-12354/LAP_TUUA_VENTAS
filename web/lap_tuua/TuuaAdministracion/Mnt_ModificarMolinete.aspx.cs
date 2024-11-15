using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;

public partial class Mnt_ModificarMolinete : System.Web.UI.Page
{
    protected bool Flg_Error;
    protected Hashtable htLabels;
    BO_Consultas objConsultas = new BO_Consultas();
    BO_Operacion objOperacion = new BO_Operacion();
    BO_Administracion objWBAdministracion = new BO_Administracion();
    UIControles objCargaCombo = new UIControles();
    DataTable dt_consultaIp = new DataTable();
    String strCodMolinete;
    bool flgValidIp;    
    string IPBLoque1, IPBLoque2, IPBLoque3, IPBLoque4, IPFinal;

    string FrmCodMolinete = "";
    string FrmDescripcion = "";
    string FrmIp = "";
    string FrmTipoDocumento = "";
    string FrmTipoVuelo = "";
    string FrmTipoMolinete = "";
    string FrmTipoAcceso = "";
    string FrmEstado = "";
    string FrmDBName = "";
    string FrmDBUser = "";
    string FrmDBPassword = "";
    string FrmEstaMaster = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Flg_Error = false;
        
        if (!IsPostBack)
        {
            strCodMolinete = Convert.ToString(Request.QueryString["Cod_Molinete"]);
            htLabels = LabelConfig.htLabels;

            try
            {
                this.btnGrabar.Text = htLabels["madmEditarPuntoVenta.btnActualizar.Text"].ToString();
                this.btnCancelar.Text = htLabels["madmEditarPuntoVenta.btnCancelar.Text"].ToString();

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
                this.lblTipoMolinete1.Text = htLabels["moperacionMolinete.lblTipoMolinete"].ToString();
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
            CargarFormulario(strCodMolinete);
        }

        if (ViewState["password"] != null)
        {
            txtDBPassword1.Attributes.Add("value", (String)ViewState["password"]);
        }
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

        DataTable dt_tipomolinete = new DataTable();
        dt_tipomolinete = objConsultas.ListaCamposxNombre("TipoMolinete");
        objCargaCombo.LlenarCombo(ddlTipoMolinete1, dt_tipomolinete, "Cod_Campo", "Dsc_Campo", false, false);
    }


    public void CargarFormulario(String strCodMolinete)
    {
        Flg_Error = false;
        try
        {            
            DataTable dt_molinet = objOperacion.obtenerMolinete(strCodMolinete);

            lblCodMolinete1.Text = dt_molinet.Rows[0].ItemArray.GetValue(0).ToString();
            string[] bip = dt_molinet.Rows[0].ItemArray.GetValue(1).ToString().Split('.');
            txtDscIP11.Text = bip[0];
            txtDscIP12.Text = bip[1];
            txtDscIP13.Text = bip[2];
            txtDscIP14.Text = bip[3];
            txtDescripcion1.Text = dt_molinet.Rows[0].ItemArray.GetValue(2).ToString();
            ddlTipoDocumento1.SelectedValue = dt_molinet.Rows[0].ItemArray.GetValue(3).ToString();
            ddlTipoVuelo1.SelectedValue = dt_molinet.Rows[0].ItemArray.GetValue(4).ToString();
            ddlTipoAcceso1.SelectedValue = dt_molinet.Rows[0].ItemArray.GetValue(5).ToString();
            ddlTipoMolinete1.SelectedValue = dt_molinet.Rows[0].ItemArray.GetValue(6).ToString();

            ddlEstado1.SelectedValue = dt_molinet.Rows[0].ItemArray.GetValue(7).ToString();
            rbtEstMaster1.Checked = dt_molinet.Rows[0].ItemArray.GetValue(12).ToString() == "1" ? true : false;
            txtDBName1.Text = dt_molinet.Rows[0].ItemArray.GetValue(13).ToString();
            txtDBUser1.Text = dt_molinet.Rows[0].ItemArray.GetValue(14).ToString();
            txtDBPassword1.Text = dt_molinet.Rows[0].ItemArray.GetValue(15).ToString();
            ViewState["password"] = dt_molinet.Rows[0].ItemArray.GetValue(15).ToString();

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

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        try
        {
            
            ValidarIp(txtDscIP11.Text, txtDscIP12.Text, txtDscIP13.Text, txtDscIP14.Text, strCodMolinete);
            if (flgValidIp == true){
                FrmCodMolinete = lblCodMolinete1.Text;
                FrmDescripcion = txtDescripcion1.Text;
                FrmIp = IPFinal;
                FrmTipoDocumento = ddlTipoDocumento1.SelectedValue;
                FrmTipoVuelo = ddlTipoVuelo1.SelectedValue;
                FrmTipoAcceso = ddlTipoAcceso1.SelectedValue;
                FrmTipoMolinete = ddlTipoMolinete1.SelectedValue;
                FrmEstado = ddlEstado1.SelectedValue;
                FrmDBName = txtDBName1.Text;
                FrmDBUser = txtDBUser1.Text;
                FrmDBPassword = (String)ViewState["password"];
                string sMaster1 = rbtEstMaster1.Checked == true ? "1" : "0";
                FrmEstaMaster = sMaster1;
            }
            else{
                flgValidIp = false;
                lblMensaje.Text = "";                
                lblMensajeError1.Text = "Direccion de IP incorrecta";
            }

            if (flgValidIp == true){
                string[] NumIp = FrmIp.Split('|');
                bool bFlag = false;
                for (int k = 0; k < NumIp.Length - 1; k++){
                    if (bFlag == false){
                        for (int m = 0; m < NumIp.Length - 1; m++){
                            if (k == m){ 
                                m = m + 1; 
                            }
                            if (NumIp[k] == NumIp[m]){
                                bFlag = true;
                                k = (k == (NumIp.Length - 1)) ? (k = k + 2) : (k = m + 1);
                                lblMensaje.Text = "La IP del molinete " + k + " se encuentra duplicada";
                                lblMensajeError1.Text = "";
                                break;
                            }
                            else{
                                bFlag = false;
                            }
                        }
                    }
                }
                
                if (bFlag == false) {
                    Molinete objActualizarMolinete = new Molinete(FrmCodMolinete, FrmIp, FrmDescripcion, FrmTipoDocumento,
                                                                  FrmTipoVuelo, FrmTipoAcceso, FrmEstado, Session["Cod_Usuario"].ToString(),
                                                                  null, null, FrmEstaMaster, FrmDBName,
                                                                  FrmDBUser, FrmDBPassword, 0, FrmTipoMolinete);


                    objOperacion = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

                    try{
                        if (objOperacion.actualizarUnMolinete(objActualizarMolinete) == true){
                            lblMensaje.Text = "";
                            //GeneraAlarma
                            string IpClient = Request.UserHostAddress;
                            GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000035", "004", IpClient, "1", "Alerta W0000035", "Actualizacion de la configuracion del Molinete, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                            omb.ShowMessage("Molinete actualizado correctamente", "Actualización de Molinete","Mnt_Molinete.aspx");
                        }
                    }
                    catch (Exception exec){

                        lblMensaje.Text = "Error en actualizar molinete.";
                    }
                }

            }

        }
        catch (Exception ex)
        {

        }
    }


    private void ValidarIp(string sIpB1, string sIPB2, string sIPB3, string sIPB4, string sCodMolinete)
    {
        if (sIpB1 == ""){
            lblMensaje.Text = "Ip incorrecta (Rango de 1 a 255)";
            IPBLoque1 = "";
        }
        else{
            if (Convert.ToInt32(sIpB1) > 0 && Convert.ToInt32(sIpB1) < 256){
                IPBLoque1 = Convert.ToString(Convert.ToInt32(sIpB1));
            }
            else{
                lblMensaje.Text = "Ip incorrecta (Rango de 1 a 255)";
                IPBLoque1 = "";
            }
        }
        
        if (sIPB2 == ""){
            lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            IPBLoque2 = "";
        }
        else{
            if (Convert.ToInt32(sIPB2) >= 0 && Convert.ToInt32(sIPB2) < 256){
                IPBLoque2 = Convert.ToString(Convert.ToInt32(sIPB2));
            }
            else{
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                IPBLoque2 = "";
            }
        }
        
        if (sIPB3 == ""){
            lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            IPBLoque3 = "";
        }
        else{
            if (Convert.ToInt32(sIPB3) >= 0 && Convert.ToInt32(sIPB3) < 256){
                IPBLoque3 = Convert.ToString(Convert.ToInt32(sIPB3));
            }
            else{
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                IPBLoque3 = "";
            }
        }
        
        if (sIPB4 == ""){
            lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            IPBLoque4 = "";
        }
        else{
            if (Convert.ToInt32(sIPB4) >= 0 && Convert.ToInt32(sIPB4) < 256){
                IPBLoque4 = Convert.ToString(Convert.ToInt32(sIPB4));
            }
            else{
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                IPBLoque4 = "";
            }
        }

        if (IPBLoque1 != "" && IPBLoque2 != "" && IPBLoque3 != "" && IPBLoque4 != ""){
            IPFinal = IPBLoque1 + "." + IPBLoque2 + "." + IPBLoque3 + "." + IPBLoque4;
            this.lblMensaje.Text = "";
            flgValidIp = true;
        }
        else{
            flgValidIp = false;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mnt_Molinete.aspx");
    }
}

