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
using System.Net;

public partial class Ope_ArchivoVenta : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    protected TipoTicket objTipoTicket;
    protected bool Flg_Error;
    protected Hashtable htConfigura;
    protected Hashtable htParametro;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        htParametro = (Hashtable)Session["htParametro"];
        tvwModalidad.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
        try
        {
            objBOOpera = new BO_Operacion();
            if (!IsPostBack)
            {
                btnGenerar.Text = htLabels["archventas.btnGenerar"].ToString();
                //btnGenerar_ConfirmButtonExtender.ConfirmText = htLabels["archventas.msgConfirm"].ToString();
                lblFecIni.Text = htLabels["archventas.lblFecIni"].ToString();
                lblFecFin.Text = htLabels["archventas.lblFecFin"].ToString();
                lblArchivo.Text = htLabels["archventas.lblArchivo"].ToString();
                lblFormato.Text = htLabels["archventas.lblFormato"].ToString();
                rbExcel.Text = htLabels["archventas.rbExcel"].ToString();
                rbTexto.Text = htLabels["archventas.rbTexto"].ToString();
                txtFecIni.Text = DateTime.Now.ToShortDateString();
                txtFecFin.Text = DateTime.Now.ToShortDateString();
                CargarModalidades();
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
        if (strModVenta == (string)Property.htProperty[Define.MOD_VENTA_NORMAL] || strModVenta == (string)Property.htProperty[Define.MOD_VENTA_MAS_CONT] || strModVenta ==null)
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

    private bool GenerarArchivo()
    {
        Hashtable htSelected = ObtenerSeleccionados();
        if (htSelected.Count == 0)
        {
            lblMensajeError.Text = htLabels["archVentas.msgSelect"].ToString();
            return false;
        }
        ArrayList htCaja = (ArrayList)htSelected[Define.TIP_VENTA_CAJA];
        ArrayList htCredito = (ArrayList)htSelected[Define.TIP_VENTA_CREDITO];
        ArrayList htUso = (ArrayList)htSelected[Define.TIP_VENTA_USO];
        ArrayList htATM = (ArrayList)htSelected[Define.TIP_VENTA_ATM];
        ArrayList htBoarding = (ArrayList)htSelected[Define.TIP_VENTA_BCBP];
        DataTable dtVentaTicket;
        ArrayList listaDTVenta = new ArrayList();
        string strFlgAero = "";
        string strFecIni = txtFecIni.Text.Substring(6, 4) + txtFecIni.Text.Substring(3, 2) + txtFecIni.Text.Substring(0, 2);
        string strFecFin = txtFecFin.Text.Substring(6, 4) + txtFecFin.Text.Substring(3, 2) + txtFecFin.Text.Substring(0, 2);
        int intSecuencial=0;
        bool boRetorno = false;

        if (htCaja != null)
        {
            strFlgAero = htCaja.Count > 0 ? "1" : "0";
            dtVentaTicket = objBOOpera.ObtenerVentaTicket(strFecIni, strFecFin, Define.TIP_VENTA_CAJA, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htCaja, dtVentaTicket);
            }
            if (dtVentaTicket.Rows.Count > 0)
            {
                listaDTVenta.Add(dtVentaTicket);
            }
        }
        if (htCredito != null)
        {
            strFlgAero = htCredito.Count > 0 ? "1" : "0";
            dtVentaTicket = objBOOpera.ObtenerVentaTicket(strFecIni, strFecFin, Define.TIP_VENTA_CREDITO, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htCredito, dtVentaTicket);
            }
            if (dtVentaTicket.Rows.Count > 0)
            {
                listaDTVenta.Add(dtVentaTicket);
            }
        }
        if (htUso != null)
        {
            strFlgAero = htUso.Count > 0 ? "1" : "0";
            dtVentaTicket = objBOOpera.ObtenerVentaTicket(strFecIni, strFecFin, Define.TIP_VENTA_USO, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htUso, dtVentaTicket);
            }
            if (dtVentaTicket.Rows.Count > 0)
            {
                listaDTVenta.Add(dtVentaTicket);
            }
        }
        if (htATM != null)
        {
            strFlgAero = htATM.Count > 0 ? "1" : "0";
            dtVentaTicket = objBOOpera.ObtenerVentaTicket(strFecIni, strFecFin, Define.TIP_VENTA_ATM, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htATM, dtVentaTicket);
            }
            if (dtVentaTicket.Rows.Count > 0)
            {
                listaDTVenta.Add(dtVentaTicket);
            }
        }
        if (htBoarding != null)
        {
            strFlgAero = htBoarding.Count > 0 ? "1" : "0";
            dtVentaTicket = objBOOpera.ObtenerVentaTicket(strFecIni, strFecFin, Define.TIP_VENTA_BCBP, strFlgAero);
            if (strFlgAero == "1")
            {
                DepurarXAerolinea(htBoarding, dtVentaTicket);
            }
            if (dtVentaTicket.Rows.Count > 0)
            {
                listaDTVenta.Add(dtVentaTicket);
            }
        }
        if (listaDTVenta.Count == 0)
        {
            lblMensajeError.Text = (string)htLabels["archVentas.msgData"];
            return false;
        }

        if (rbExcel.Checked)
        {
            boRetorno= GenerarExcel(listaDTVenta,ref intSecuencial);
        }
        else
        {
            boRetorno= GenerarTexto(listaDTVenta, ref intSecuencial);
        }
        if (boRetorno)
        {
            objBOOpera.ObtenerVentaTicket(intSecuencial.ToString(), strFecFin, "", "");
        }
        return boRetorno;
    }

    private bool GenerarExcel(ArrayList listaDTVenta, ref int intSecuencial)
    {
        DataTable dtVentaTicket;
        string strFullPath = "";
        string strNroMaterial = "";
        string strFile = (string)htParametro[Define.ID_ARCHIVO_VENTA];
        string strFecActual = objBOOpera.ObtenerFechaActual();
        bool boIncremento=false;
        int intMaxSecuencial =Int32.Parse(htParametro["MSAV"].ToString());
        //try
        //{
        strFile = strFile + strFecActual + ".xls";
        
        
        //SFTP o FTP
        string modoEnvio = CargarModoEnvioArchivos();
        string codParametro = Define.ID_PARAM_PATH_VENTAS; // FTP

        if (modoEnvio == "SFTP")
        {
            codParametro = Define.ID_PARAM_PATH_VENTAS_SFTP;
        }
        
        htConfigura = CargarConfiguracionUpload(codParametro);

        if ((string)htConfigura["modo"] == "1")
        {
            //File.Delete((string)htConfigura["path"] + "/" + strFile);
            strFullPath = HttpContext.Current.Server.MapPath(".") + "/" + strFile;
        }
        else if ((string)htConfigura["modo"] == "2")
        {
            File.Delete(strFile);
           // strFullPath = (string)htConfigura["path"] + "/" + strFile;
            strFullPath = "D:\\" + strFile;
        }
        try
        {
            System.IO.StreamWriter excelDoc;

            excelDoc = new System.IO.StreamWriter(strFullPath);
            const string startExcelXML = "<xml version>\r\n<Workbook " +
                  "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                  " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                  "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                  "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                  "office:spreadsheet\">\r\n <Styles>\r\n " +
                  "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                  "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                  "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                  "\r\n <Protection/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                  "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                  "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                  " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                  "ss:Format=\"0.000000\"/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                  "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                  "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                  "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";
            int sheetCount = 1;
            excelDoc.Write(startExcelXML);
            excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            excelDoc.Write("<Table>");

            for (int k = 0; k < listaDTVenta.Count; k++)
            {
                dtVentaTicket = (DataTable)listaDTVenta[k];
                intSecuencial=intSecuencial==intMaxSecuencial?1:++intSecuencial;
                boIncremento = k>0;
                for (int i = 0; i < dtVentaTicket.Rows.Count; i++)
                {
                    DataRow x = dtVentaTicket.Rows[i];
                    excelDoc.Write("<Row>");
                    for (int j = 0; j < dtVentaTicket.Columns.Count - 3; j++)
                    {
                        System.Type rowType;
                        rowType = x[j].GetType();

                        if (j == 3)//campo secuencial
                        {
                            if (strNroMaterial != x[15].ToString())
                            {
                                if (!boIncremento)
                                {
                                    intSecuencial = strNroMaterial != "" ? (intSecuencial == intMaxSecuencial ? 1 : ++intSecuencial) : Int32.Parse(x[j].ToString());
                                }
                                boIncremento = false;
                                strNroMaterial = x[15].ToString();
                            }
                            x[j] = intSecuencial.ToString();
                        }

                        switch (rowType.ToString())
                        {
                            case "System.String":
                                string XMLstring = x[j].ToString();
                                XMLstring = XMLstring.Trim();
                                XMLstring = XMLstring.Replace("&", "&");
                                XMLstring = XMLstring.Replace(">", ">");
                                XMLstring = XMLstring.Replace("<", "<");
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(XMLstring);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DateTime":
                                DateTime XMLDate = (DateTime)x[j];
                                string XMLDatetoString = ""; //Excel Converted Date

                                XMLDatetoString = XMLDate.Year.ToString() +
                                     "-" +
                                     (XMLDate.Month < 10 ? "0" +
                                     XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                     "-" +
                                     (XMLDate.Day < 10 ? "0" +
                                     XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                     "T" +
                                     (XMLDate.Hour < 10 ? "0" +
                                     XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                     ":" +
                                     (XMLDate.Minute < 10 ? "0" +
                                     XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                     ":" +
                                     (XMLDate.Second < 10 ? "0" +
                                     XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                     ".000";
                                excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                             "<Data ss:Type=\"DateTime\">");
                                excelDoc.Write(XMLDatetoString);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Boolean":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                            "<Data ss:Type=\"String\">");
                                excelDoc.Write(x[j].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                        "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[j].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Decimal":
                            case "System.Double":
                                excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                      "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[j].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DBNull":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                      "<Data ss:Type=\"String\">");
                                excelDoc.Write("");
                                excelDoc.Write("</Data></Cell>");
                                break;
                            default:
                                throw (new Exception(rowType.ToString() + " not handled."));
                        }
                    }
                    excelDoc.Write("</Row>");
                }
            }
            excelDoc.Write("</Table>");
            excelDoc.Write(" </Worksheet>");
            excelDoc.Write(endExcelXML);
            excelDoc.Close();

            if ((string)htConfigura["modo"] == "1")
            {
                
                //SFTP
                if(modoEnvio == "SFTP")  return UploadSFtp(strFile);
                //FTP
                return UploadFtp(strFile);

            }
        }
        catch (Exception ex)
        {
            //ErrorHandler.Trace("",ex);
            lblMensajeError.Text = (string)htLabels["archVentas.msgRuta"];
            return false;
        }
        return true;
    }

    private bool GenerarTexto(ArrayList listaDTVenta, ref int intSecuencial)
    {
        try
        {
            htParametro = (Hashtable)Session["htParametro"];

            
            //SFTP o FTP
           
            string modoEnvio = CargarModoEnvioArchivos();
            string codParametro = Define.ID_PARAM_PATH_VENTAS; // FTP

            if (modoEnvio == "SFTP")
            {
                codParametro = Define.ID_PARAM_PATH_VENTAS_SFTP;
            }
            
            htConfigura = CargarConfiguracionUpload(codParametro);


            string strFile = (string)htParametro[Define.ID_ARCHIVO_VENTA];
            string strFecActual = objBOOpera.ObtenerFechaActual();
            StreamWriter swTrace = null;
            string strNroMaterial = "";
            bool boIncremento = false;
            int intMaxSecuencial = Int32.Parse(htParametro["MSAV"].ToString());
            strFile = strFile + strFecActual + ".txt";

            if ((string)htConfigura["modo"] == "1")
            {
                //File.Delete(strFile);
                swTrace = File.AppendText(HttpContext.Current.Server.MapPath(".") + "/" + strFile);
            }
            else if ((string)htConfigura["modo"] == "2")
            {
                //File.Delete((string)htConfigura["path"] + "/" + strFile);
                //swTrace = File.AppendText((string)htConfigura["path"] + "/" + strFile);

                File.Delete("D:\\" + strFile);
                swTrace = File.AppendText("D:\\" + strFile);                

            }

            string strLinea = "";
            string strSeparador = SeparadorEspecial();
            DataTable dtVentaTicket;

            for (int k = 0; k < listaDTVenta.Count; k++)
            {
                dtVentaTicket = (DataTable)listaDTVenta[k];
                intSecuencial = intSecuencial == intMaxSecuencial ? 1 : ++intSecuencial;
                boIncremento = k > 0;
                for (int i = 0; i < dtVentaTicket.Rows.Count; i++)
                {
                    strLinea = "";
                    for (int j = 0; j < dtVentaTicket.Columns.Count - 3; j++)
                    {
                        if (j == 3)//campo secuencial
                        {
                            if (strNroMaterial != dtVentaTicket.Rows[i].ItemArray.GetValue(15).ToString())
                            {
                                if (!boIncremento)
                                {
                                    intSecuencial = strNroMaterial != "" ? (intSecuencial == intMaxSecuencial ? 1 : ++intSecuencial) : Int32.Parse(dtVentaTicket.Rows[i].ItemArray.GetValue(j).ToString());
                                }
                                boIncremento = false;
                                strNroMaterial = dtVentaTicket.Rows[i].ItemArray.GetValue(15).ToString();
                            }
                            strLinea += intSecuencial.ToString() + strSeparador;
                        }
                        else
                        {
                            strLinea += dtVentaTicket.Rows[i].ItemArray.GetValue(j).ToString() + strSeparador;
                        }
                    }
                    swTrace.WriteLine(strLinea);
                }
            }
            swTrace.Close();
            if ((string)htConfigura["modo"] == "1")
            {
                //SFTP
                if (modoEnvio == "SFTP") return UploadSFtp(strFile);
                //FTP
                return UploadFtp(strFile);
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.Trace("", ex);
            return false;
        }
        return true;
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
                }
                htModCia.Add(trnModalidad.Value, arrCia);
            }
        }
        return htModCia;
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        Hashtable htSelected = ObtenerSeleccionados();
        lblMensajeError.Text = "";
        if (htSelected.Count <= 0)
        {
            lblMensajeError.Text = (string)htLabels["archventas.msgModalidad"];
            return;
        }
        if (GenerarArchivo())
        {
            System.Threading.Thread.Sleep(500);
            Response.Redirect("Ope_PosArchVenta.aspx");
        }
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

    /*private bool Upload(string strFile)
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
                lblMensajeError.Text = (string)htLabels["archventas.msgFTP"];
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
    }*/

    /// <summary>
    /// Metodo enviado por Daniel Castillo de LAP el 12/10/2015
    /// </summary>
    /// <param name="strFile"></param>
    /// <returns></returns>
    private bool UploadFtp(string strFile)
    {
        try
        {
           
            //FTP
            string codParametro = Define.ID_PARAM_PATH_VENTAS;
           

            htConfigura = CargarConfiguracionUpload(codParametro);

            //DCASTILLO
            string localFilePath = HttpContext.Current.Server.MapPath(".") + "\\" + strFile;
            using (WebClient client = new WebClient())
            {

                client.Credentials = new NetworkCredential((string)htConfigura["user"], (string)htConfigura["password"]);
                client.UploadFile("ftp://" + (string)htConfigura["host"] + "/" + (string)htConfigura["directorio"] + "/" + strFile, "STOR", localFilePath);

            }
            File.Delete(localFilePath);
            return true;

        }
        catch (Exception ex)
        {
            string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_010])["MESSAGE"];
            lblMensajeError.Text = strMessage;
            ErrorHandler.Trace(strMessage, ex);
            return false;
        }
    }
    //2022-08-22
    //DCASTILLO
    //Envío de archivos utilizando el protocolo SFTP
    private bool UploadSFtp(string strFile)
    {
        try
        {
          
            //SFTP 
            string codParametro = Define.ID_PARAM_PATH_VENTAS_SFTP;
            htConfigura = CargarConfiguracionUpload(codParametro);

            string localFilePath = HttpContext.Current.Server.MapPath(".") + "\\" + strFile;

            byte[] archivoVenta = File.ReadAllBytes(localFilePath);

            SftpUtil sftpUtil = new SftpUtil((string)htConfigura["host"], int.Parse((string)htConfigura["puerto"]), 
                (string)htConfigura["user"], (string)htConfigura["password"], (string)htConfigura["directorio"]);


            sftpUtil.EnviarArchivo(archivoVenta,strFile);
            
            File.Delete(localFilePath);
            return true;

        }
        catch (Exception ex)
        {
            string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_010])["MESSAGE"];
            lblMensajeError.Text = strMessage;
            ErrorHandler.Trace(strMessage, ex);
            return false;
        }
    }




    private Hashtable CargarConfiguracionUpload(string parametro)
    {
        //Define.ID_PARAM_PATH_VENTAS_SFTP
        BO_Consultas objBOConfigura = new BO_Consultas();
        DataTable dtConfigura = objBOConfigura.ListarParametros((string)Property.htProperty[parametro]);
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

    private string CargarModoEnvioArchivos()
    {
        //Define.ID_PARAM_PATH_VENTAS_SFTP
        BO_Consultas objBOConfigura = new BO_Consultas();
        DataTable dtConfigura = objBOConfigura.ListarParametros(Define.ID_PARAM_MODO_ENVIO);
        string strConfigura = dtConfigura.Rows[0].ItemArray.GetValue(4).ToString();

        return strConfigura;
    }



    private void DepurarXAerolinea(ArrayList ArrCia, DataTable dtVentas)
    {
        //for (int k = 0; k < ArrCia.Count; k++)
        //{
            for (int i = 0; i < dtVentas.Rows.Count; i++)
            {
                if (!ArrCia.Contains(dtVentas.Rows[i].ItemArray.GetValue(30).ToString().Trim()))
                {
                    dtVentas.Rows.RemoveAt(i--);
                }
            }
        //}
    }

    private string SeparadorEspecial()
    {
        string strSeparador = Convert.ToString((char)(Int32.Parse((string)htParametro[Define.ID_PARAM_SEP_VENTAS])));
        return strSeparador;
    }


    
}
