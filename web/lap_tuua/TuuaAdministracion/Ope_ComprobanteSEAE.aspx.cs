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
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;
using LAP.TUUA.FTP;

public partial class Ope_ComprobanteSEAE : System.Web.UI.Page
{

    protected Hashtable htLabels;
    protected Hashtable htParametro;
    protected Hashtable htConfigura;
    protected bool Flg_Error;
    protected BO_Operacion objBOOpera;

    string sMes;
    string sAnio;
    string sTDocumento;

   
    protected void Page_Load(object sender, EventArgs e)
    {
        htParametro = (Hashtable)Session["htParametro"];
        htLabels = LabelConfig.htLabels;
        tvwModalidad.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
        try
        {
            objBOOpera = new BO_Operacion();
            if (!IsPostBack)
            {
                btnGenerar.Text = htLabels["opeComprobanteSEAE.btnGenerar"].ToString();
                lblFecha.Text = htLabels["opeComprobanteSEAE.lblFecha"].ToString();
                lblTipoDocumento.Text = htLabels["opeComprobanteSEAE.lblTipoDocumento"].ToString();
                lblArchivo.Text = htLabels["opeComprobanteSEAE.lblArchivo"].ToString();
                rbTicket.Text = htLabels["opeComprobanteSEAE.rbTicket"].ToString();
                rbBP.Text = htLabels["opeComprobanteSEAE.rbBP"].ToString();
                CargarModalidades();

                int mes = DateTime.Now.Month - 1;


                hfAnnio.Value = DateTime.Now.Year.ToString();

                string descripMes = "";



                switch (mes)
                {

                    case 1: descripMes = "Enero";
                        break;
                    case 2: descripMes = "Febrero";
                        break;
                    case 3: descripMes = "Marzo";
                        break;
                    case 4: descripMes = "Abril";
                        break;
                    case 5: descripMes = "Mayo";
                        break;
                    case 6: descripMes = "Junio";
                        break;
                    case 7: descripMes = "Julio";
                        break;
                    case 8: descripMes = "Agosto";
                        break;
                    case 9: descripMes = "Septiembre";
                        break;
                    case 10: descripMes = "Octubre";
                        break;
                    case 11: descripMes = "Noviembre";
                        break;
                    case 12: descripMes = "Diciembre";
                        break;

                }

                txtMes.Text = descripMes + " - " + hfAnnio.Value;
                hfMes.Value = mes < 10 ? "0" + mes.ToString() : mes.ToString();

            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex);

        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }

       
        DeshabilitarNodos();
        SaveFiltros();

    }

    private void SaveFiltros() {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sAnio", this.hfAnnio.Value));
        filterList.Add(new Filtros("sMes", this.hfMes.Value));
        

        sTDocumento = "";
        if (this.rbTicket.Checked == true && this.rbBP.Checked == false)
            sTDocumento = "T";
        else if (this.rbTicket.Checked == false && this.rbBP.Checked == true)
            sTDocumento = "B";
        filterList.Add(new Filtros("sTDocumento", sTDocumento));
        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros() {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sAnio = newFilterList[0].Valor;
        sMes =  newFilterList[1].Valor;
        sTDocumento = newFilterList[2].Valor;
    }

    private void CargarModalidades()
    {
        tvwModalidad.Nodes.Clear();
        string sHijo = "";
        string sCodPadre = "";
        string sPadre = "";
        TreeNode TPadre = null;
        TreeNode THijo = null;
        bool boAgregar = false;
        List<ModVentaComp> listaModCia = objBOOpera.ListarCompaniaxModVenta(null, null);
        string[] arrCodTipVenta = new string[5];
        string[] arrNomTipVenta = new string[5];
        Hashtable htCompany;
        CargarArregloTipVenta(arrCodTipVenta, arrNomTipVenta);

        for (int i = 0; i < arrCodTipVenta.Length; i++)
        {
            sCodPadre = arrCodTipVenta[i];
            sPadre = arrNomTipVenta[i];
            TPadre = new TreeNode(sPadre);
            TPadre.Value = arrCodTipVenta[i];
            TPadre.ShowCheckBox = true;
            htCompany = new Hashtable();
            for (int j = 0; j < listaModCia.Count; j++)
            {
                switch (i)
                {
                    //case 0:
                    //    boAgregar = IsTipoCaja(listaModCia[j].SCodModalidadVenta);
                    //    break;
                    case 1:
                        boAgregar = IsTipoCredito(listaModCia[j].SCodModalidadVenta);
                        break;
                    case 2:
                        boAgregar = IsTipoUso(listaModCia[j].SCodModalidadVenta);
                        break;
                    case 3:
                        boAgregar = IsTipoATM(listaModCia[j].SCodModalidadVenta);
                        break;
                    case 4:
                        boAgregar = IsTipoBoarding(listaModCia[j].SCodModalidadVenta);
                        break;
                    default: boAgregar = false;
                        break;
                }
                if (boAgregar)
                {
                    sHijo = listaModCia[j].Dsc_Compania;
                    if (!htCompany.Contains(sHijo))
                    {
                        THijo = new TreeNode(sHijo);
                        THijo.Value = listaModCia[j].SCodCompania;
                        THijo.ShowCheckBox = true;

                        htCompany.Add(sHijo, sHijo);
                        TPadre.ChildNodes.Add(THijo);
                    }
                }
            }
            tvwModalidad.Nodes.Add(TPadre);
        }
        tvwModalidad.ExpandAll();
        tvwModalidad.ShowExpandCollapse = false;
    }

    private bool IsTipoCaja(string strModVenta)
    {
        if (strModVenta == (string)Property.htProperty[Define.MOD_VENTA_NORMAL] || strModVenta == (string)Property.htProperty[Define.MOD_VENTA_MAS_CONT] || strModVenta == null)
        {
            return true;
        }
        return false;
    }

    private bool IsTipoCredito(string strModVenta)
    {
        if (strModVenta == (string)Property.htProperty[Define.MOD_VENTA_MAS_CRED])
        {
            return true;
        }
        return false;
    }

    private bool IsTipoUso(string strModVenta)
    {
        if (strModVenta == (string)Property.htProperty[Define.MOD_VENTA_MAS_CRED])
        {
            return true;
        }
        return false;
    }

    private bool IsTipoBoarding(string strModVenta)
    {
        if (strModVenta == (string)Property.htProperty[Define.MOD_VENTA_BOARDING])
        {
            return true;
        }
        return false;
    }

    private bool IsTipoATM(string strModVenta)
    {
        if (strModVenta == (string)Property.htProperty[Define.MOD_VENTA_ATM])
        {
            return true;
        }
        return false;
    }

    private void CargarArregloTipVenta(string[] arrCodTipVenta, string[] arrNomTipVenta)
    {
        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    arrCodTipVenta[i] = Define.TIP_VENTA_CAJA;
                    arrNomTipVenta[i] = (string)htLabels["archventas.nodoCaja"];
                    break;
                case 1:
                    arrCodTipVenta[i] = Define.TIP_VENTA_CREDITO;
                    arrNomTipVenta[i] = (string)htLabels["archventas.nodoCredito"];
                    break;
                case 2:
                    arrCodTipVenta[i] = Define.TIP_VENTA_USO;
                    arrNomTipVenta[i] = (string)htLabels["archventas.nodoUso"];
                    break;
                case 3:
                    arrCodTipVenta[i] = Define.TIP_VENTA_ATM;
                    arrNomTipVenta[i] = (string)htLabels["archventas.nodoAtm"];
                    break;
                case 4:
                    arrCodTipVenta[i] = Define.TIP_VENTA_BCBP;
                    arrNomTipVenta[i] = (string)htLabels["archventas.nodoBoarding"];
                    break;
                default:
                    break;
            }
        }
    }

    private void DesseleccionarNodos() {

            for (int i = 0; i < tvwModalidad.Nodes.Count; i++)
            {

                for (int j = 0; j < tvwModalidad.Nodes[i].ChildNodes.Count; j++)
                {

                    tvwModalidad.Nodes[i].ChildNodes[j].Checked = false;

                }

                tvwModalidad.Nodes[i].Checked = false;

            }

    }

    private void DeshabilitarNodos() {

        if (tvwModalidad.Nodes.Count > 0)
        {
            string sPadre = "";
            

            if (rbTicket.Checked == true) {

                tvwModalidad.ExpandAll();

                for (int i = 0; i < tvwModalidad.Nodes.Count; i++)
                {
                    sPadre = tvwModalidad.Nodes[i].Value.ToString();

                    if (sPadre == "BRD")
                    {
                        tvwModalidad.Nodes[i].ShowCheckBox = false;
                        for (int j = 0; j < tvwModalidad.Nodes[i].ChildNodes.Count; j++)
                        {
                            tvwModalidad.Nodes[i].ChildNodes[j].ShowCheckBox = false;

                        }
                    }
                    else {
                        tvwModalidad.Nodes[i].ShowCheckBox = true;

                        for (int j = 0; j < tvwModalidad.Nodes[i].ChildNodes.Count; j++)
                        {
                            tvwModalidad.Nodes[i].ChildNodes[j].ShowCheckBox = true;

                        }
                    }
                }
            }else 
            {
                tvwModalidad.ExpandAll();

                for (int i = 0; i < tvwModalidad.Nodes.Count; i++)
                {
                    sPadre = tvwModalidad.Nodes[i].Value.ToString();

                    if (sPadre != "BRD")
                    {
                        tvwModalidad.Nodes[i].ShowCheckBox = false;
                        for (int j = 0; j < tvwModalidad.Nodes[i].ChildNodes.Count; j++)
                        {
                            tvwModalidad.Nodes[i].ChildNodes[j].ShowCheckBox = false;

                        }
                    }
                    else
                    {
                        tvwModalidad.Nodes[i].ShowCheckBox = true;

                        for (int j = 0; j < tvwModalidad.Nodes[i].ChildNodes.Count; j++)
                        {
                            tvwModalidad.Nodes[i].ChildNodes[j].ShowCheckBox = true;

                        }
                    }
                }
            
            }
            
        }
    
    }
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        Hashtable htSelected = ObtenerSeleccionados();
        lblMensajeError.Text = "";
        if (htSelected.Count <= 0)
        {
            lblMensajeError.Text = (string)htLabels["opeComprobanteSEAE.msgModalidad"];
            return;
        }

        if (GenerarArchivo())
        {
            System.Threading.Thread.Sleep(500);
            Response.Redirect("Ope_PosCompSEAE.aspx");
        }
    }
    protected Hashtable ObtenerSeleccionados()
    {
        TreeNode trnModalidad;
        Hashtable htModCia = new Hashtable();
        ArrayList arrCia;

        for (int i = 0; i < tvwModalidad.Nodes.Count; i++)
        {
            if (tvwModalidad.Nodes[i].Checked)
            {
                trnModalidad = tvwModalidad.Nodes[i];
                arrCia = new ArrayList();
                for (int j = 0; j < trnModalidad.ChildNodes.Count; j++)
                {
                    if (trnModalidad.ChildNodes[j].Checked)
                    {
                        arrCia.Add(trnModalidad.ChildNodes[j].Value);
                    }
                    //else 
                    //{
                    //    lblMensajeError.Text = "Seleccione como mínimo una aerolínea";
                    //}
                }
                htModCia.Add(trnModalidad.Value, arrCia);
            }
        }
        return htModCia;
    }

    private bool GenerarArchivo() 
    {
        Hashtable htSelected = ObtenerSeleccionados();
        if (htSelected.Count == 0)
        {
            lblMensajeError.Text = htLabels["opeComprobanteSEAE.msgSelect"].ToString();
            
        }

        RecuperarFiltros();

        ArrayList htCaja = (ArrayList)htSelected[Define.TIP_VENTA_CAJA];
        ArrayList htCredito = (ArrayList)htSelected[Define.TIP_VENTA_CREDITO];
        ArrayList htUso = (ArrayList)htSelected[Define.TIP_VENTA_USO];
        ArrayList htATM = (ArrayList)htSelected[Define.TIP_VENTA_ATM];
        ArrayList htBoarding = (ArrayList)htSelected[Define.TIP_VENTA_BCBP];
        DataTable dtComprobante;
        ArrayList listaDTVenta = new ArrayList();
        string strFlgAero = "";
        

        if (htCaja != null)
        {
            strFlgAero = htCaja.Count > 0 ? "1" : "0";
            dtComprobante = objBOOpera.ObtenerComprobanteSEAE(sAnio, sMes, sTDocumento, Define.TIP_VENTA_CAJA, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htCaja, dtComprobante);
            }
            if (dtComprobante.Rows.Count > 0)
            {
               
                listaDTVenta.Add(dtComprobante);
                
            }
        }

        if (htCredito != null)
        {
            strFlgAero = htCredito.Count > 0 ? "1" : "0";
            dtComprobante = objBOOpera.ObtenerComprobanteSEAE(sAnio, sMes,sTDocumento, Define.TIP_VENTA_CREDITO, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htCredito, dtComprobante);
            }
            if (dtComprobante.Rows.Count > 0)
            {
               
                listaDTVenta.Add(dtComprobante);
               
            }
        }

        if (htUso != null)
        {
            strFlgAero = htUso.Count > 0 ? "1" : "0";
            dtComprobante = objBOOpera.ObtenerComprobanteSEAE(sAnio, sMes, sTDocumento, Define.TIP_VENTA_USO, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htUso, dtComprobante);
            }
            if (dtComprobante.Rows.Count > 0)
            {
              
                listaDTVenta.Add(dtComprobante);
            }
        }

        if (htATM != null)
        {
            strFlgAero = htATM.Count > 0 ? "1" : "0";
            dtComprobante = objBOOpera.ObtenerComprobanteSEAE(sAnio, sMes, sTDocumento, Define.TIP_VENTA_ATM, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htATM, dtComprobante);
            }
            if (dtComprobante.Rows.Count > 0)
            {
                
                listaDTVenta.Add(dtComprobante);
            }
        }

        if (htBoarding != null)
        {
            strFlgAero = htBoarding.Count > 0 ? "1" : "0";
            dtComprobante = objBOOpera.ObtenerComprobanteSEAE(sAnio, sMes,sTDocumento, Define.TIP_VENTA_BCBP, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htBoarding, dtComprobante);
            }
            if (dtComprobante.Rows.Count > 0)
            {
                 
                listaDTVenta.Add(dtComprobante);
            }
        }

        if (listaDTVenta.Count == 0)
        {
            lblMensajeError.Text = (string)htLabels["opeComprobanteSEAE.msgData"];
            return false;
        }

        return GenerarTexto(listaDTVenta);

    }

    private bool GenerarTexto(ArrayList listaDTVenta)
    {

        try {
            htParametro = (Hashtable)Session["htParametro"];
            htConfigura = CargarConfiguracionUpload();
            StreamWriter swTrace = null;
            
            string ruc = "20501577252";
            string TD = "";
            if (sTDocumento == "T")
            {
                TD = "002";
            }
            else {
                TD = "001";
            }

            string strFile = ruc + TD + sAnio + sMes + "." + "01.txt";


            if ((string)htConfigura["modo"] == "1")
            {
                File.Delete(strFile);
                swTrace = File.AppendText(HttpContext.Current.Server.MapPath(".") + "/" + strFile);
            }
            else if ((string)htConfigura["modo"] == "2")
            {
                File.Delete((string)htConfigura["path"] + "/" + strFile);
                swTrace = File.AppendText((string)htConfigura["path"] + "/" + strFile);

                //File.Delete("D:\\" + strFile);
                //swTrace = File.AppendText("D:\\" + strFile);   
            }

                       
            DataTable dtArchivo;

            string concat;
            

            if (sTDocumento == "T")
            {

                for (int k = 0; k < listaDTVenta.Count; k++)
                {
                    dtArchivo = (DataTable)listaDTVenta[k];

                    if (dtArchivo.Columns.Contains("COD_COMPANIA"))
                    { 

                        foreach (DataRow fila in dtArchivo.Rows)
                        {
                            concat = "";
                             

                            for (int i = 0; i < dtArchivo.Columns.Count; i++)
                            {
                                concat += fila[i] + "|";
                            }
                            concat = concat.Substring(0, concat.Length - 12);
                            swTrace.WriteLine(concat);
                        }
                    }
                    
                    else
                    {
                         foreach (DataRow fila in dtArchivo.Rows)
                        {
                            concat = "";

                            for (int i = 0; i < dtArchivo.Columns.Count; i++)
                            {
                                concat += fila[i] + "|";
                            }
                            concat = concat.Substring(0, concat.Length - 1);
                            swTrace.WriteLine(concat);
                        }
                    }
                        
                }

            }
            else {

                for (int k = 0; k < listaDTVenta.Count; k++)
                {
                    dtArchivo = (DataTable)listaDTVenta[k];

                    if (dtArchivo.Columns.Contains("COD_COMPANIA"))
                    {
                        foreach (DataRow fila in dtArchivo.Rows)
                        {
                            concat = "";

                            for (int i = 0; i < dtArchivo.Columns.Count; i++)
                            {
                                concat += fila[i] + "|";
                            }
                            concat = concat.Substring(0, concat.Length - 12);
                            swTrace.WriteLine(concat);
                        }
                        
                      
                    }
                    else {

                        foreach (DataRow fila in dtArchivo.Rows)
                        {
                            concat = "";

                            for (int i = 0; i < dtArchivo.Columns.Count; i++)
                            {
                                concat += fila[i] + "|";
                            }
                            concat = concat.Substring(0, concat.Length - 1);
                            swTrace.WriteLine(concat);
                        }
                    
                    }

                }
            
            }

        swTrace.Close();

        if ((string)htConfigura["modo"] == "1")
        {
         return Upload(strFile);
        }
        }
        catch (Exception ex)
        {
            ErrorHandler.Trace("", ex);
            return false;  
        }

        return true;

    }

    private Hashtable CargarConfiguracionUpload()
    {
        BO_Consultas objBOConfigura = new BO_Consultas();
        DataTable dtConfigura = objBOConfigura.ListarParametros((string)Property.htProperty[Define.ID_PARAM_PATH_SEAE]);
        string strConfigura = dtConfigura.Rows[0].ItemArray.GetValue(4).ToString();
        char[] sep1 = { ';' };
        char[] sep2 = { '=' };
        string[] arrConfigura = strConfigura.Split(sep1);
        string[] arrCampo;
        Hashtable htConfigura = new Hashtable();
        for (int i = 0; i < arrConfigura.Length; i++)
        {
            arrCampo = arrConfigura[i].Split(sep2);
            htConfigura.Add(arrCampo[0], arrCampo[1]);
        }
        return htConfigura;
    }


    private FTPFactory ModoFTP()
    {
        FTPFactory ff = new FTPFactory();
        //ff.setDebug(false);
        ff.setRemoteHost((string)htConfigura["host"]);
        ff.setRemoteUser((string)htConfigura["user"]);
        ff.setRemotePass((string)htConfigura["password"]);
        //ff.setRemotePort((string)htConfigura["puerto"]);
        if (!ff.login())
        {
            return null;
        }
        ff.chdir((string)htConfigura["directorio"]);
        return ff;
    }

    private bool Upload(string strFile)
    {
        try
        {
            htConfigura = CargarConfiguracionUpload();
            FTPFactory ff = ModoFTP();
            if (ff != null)
            {
                ff.upload(HttpContext.Current.Server.MapPath(".") + "\\" + strFile);
                //System.Threading.Thread.Sleep(2000);
                ff.close();
                File.Delete(HttpContext.Current.Server.MapPath(".") + "\\" + strFile);
                return true;
            }
            else
            {
                lblMensajeError.Text = (string)htLabels["opeComprobanteSEAE.msgFTP"];
                return false;
            }
        }
        catch (Exception ex)
        {
            string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_010])["MESSAGE"];
            lblMensajeError.Text = strMessage;
            ErrorHandler.Trace(strMessage, ex);
            return false;
        }
    }

    protected void rbTicket_CheckedChanged(object sender, EventArgs e)
    {
        DeshabilitarNodos();
        DesseleccionarNodos();
    }
    protected void rbBP_CheckedChanged(object sender, EventArgs e)
    {
        DeshabilitarNodos();
        DesseleccionarNodos();
    }

    private void DepurarXAerolinea(ArrayList ArrCia, DataTable dtComprobante)
    {
       
        if (sTDocumento == "T")
        {
            for (int i = 0; i < dtComprobante.Rows.Count; i++)
            {
                if (!ArrCia.Contains(dtComprobante.Rows[i].ItemArray.GetValue(7).ToString().Trim()))
                {
                    dtComprobante.Rows.RemoveAt(i--);
                }
            }

        }
        else {
            for (int i = 0; i < dtComprobante.Rows.Count; i++)
            {
                if (!ArrCia.Contains(dtComprobante.Rows[i].ItemArray.GetValue(15).ToString().Trim()))
                {
                    dtComprobante.Rows.RemoveAt(i--);
                }
            }
        }
    }
    
}