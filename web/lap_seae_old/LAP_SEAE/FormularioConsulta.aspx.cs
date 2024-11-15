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
using System.Xml;
using LAP.TUUA.ALARMAS;
using System.IO;
using LAP.TUUA.CONVERSOR;

public partial class FormularioConsulta : System.Web.UI.Page
{

    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    protected BO_Seguridad objBOSeguridad = new BO_Seguridad();
    protected BO_Consultas objBOConsultas = new BO_Consultas();
    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt;

    bool Flg_Error;
    DataTable dt_parametro;


    /*public FormularioConsulta()
    {
        try
        {
            //carga path de recursos
            if (!Property.htProperty.ContainsKey("PATHRECURSOS"))
            {
                Property.htProperty.Add("PATHRECURSOS", HttpContext.Current.Server.MapPath("."));
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            string msgErr = (string)((Hashtable)ErrorHandler.htErrorType[LAP.TUUA.UTIL.Define.ERR_008])["MESSAGE"];
            ErrorHandler.Trace(msgErr, ex.Message, ex.StackTrace);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PagError.aspx?cod=" + LAP.TUUA.UTIL.Define.ERR_008);
            }

        }
        objBOSeguridad = new BO_Seguridad();
        objBOConsultas = new BO_Consultas();
    }*/


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
                {
                #region Llenando combos de busqueda: Lleno las companias
                DataTable dtCompanias = objBOConsultas.ConsultaCompaniaxFiltro("0","1","","ASC");
                Session["dtCompanias"] = dtCompanias;
                cboCompanias.DataSource = dtCompanias;
                cboCompanias.DataTextField = "Dsc_Compania";
                cboCompanias.DataValueField = "Cod_Compania";
                cboCompanias.DataBind();
                cboCompanias.Items.Insert(0, new ListItem("Seleccionar", string.Empty));
                //cboVuelo.Items.Insert(0, "Seleccionar");
                #endregion
            }
             catch (Exception ex)
             {
                 Flg_Error = true;
                 string msgErr = (string)((Hashtable)ErrorHandler.htErrorType[LAP.TUUA.UTIL.Define.ERR_006])["MESSAGE"];
                 ErrorHandler.Trace(msgErr, ex.Message, ex.StackTrace);
             }
             finally
             {
                 if (Flg_Error)
                 {
                     Response.Redirect("PagError.aspx?cod=" + LAP.TUUA.UTIL.Define.ERR_006);
                 }
             }
        }

    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        #region Codigo Depreciado
        //string sCompania = cboCompanias.SelectedValue;
        //string sNumVuelo = txtNroVuelo.Text;
        //string sNumAsiento = txtAsiento.Text;
        //string sFchVuelo = txtFechaVuelo.Text;
        //string sPasajero = txtPasajero.Text;

        //DataTable dt_consulta = objBOConsultas.obtenerDetalleWebBCBP(sCompania, sNumVuelo, sFchVuelo, sNumAsiento, sPasajero);

        //if (dt_consulta == null || dt_consulta.Rows.Count == 0)
        //{
        //    //Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
        //    //lblmensaje.Text = "La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro";
        //}
        //else
        //{
        //    string sETicket = string.Empty;
        //    DataRow drRegistro = dt_consulta.Rows[0];
        //    string sTrama = drRegistro["Dsc_Trama_Bcbp"].ToString();
        //    Reader2 reader = new Reader2();
        //    Hashtable ht = reader.ParseString_Boarding(sTrama);
        //    if (sTrama.Length >= 88)
        //    {
        //        sETicket = (String)ht["eticket"];
        //    }

        //    //Pintar el reporte                 
        //    obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //    obRpt.Load(Server.MapPath("").ToString() + @"\rptConsulta.rpt");
        //    //rptDetalleVentaCompania rptRecaudacionMensual
        //    obRpt.SetDataSource(dt_consulta);
        //    obRpt.SetParameterValue("pETicket", sETicket);
        //    //CrystalReportViewer1.ReportSource = obRpt;
        //    ExpotacionReport("0");
        //}
        #endregion

        String script =
            "<script language=\"javascript\">" +
            "consultar();" +            
            "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);



    }

    public void ExpotacionReport(string expExcel)
    {
        if (expExcel != "1")
        {

            MemoryStream oStream; // using System.IO 

            oStream = (MemoryStream)obRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            Response.Clear();

            Response.Buffer = true;

            Response.ContentType = "application/pdf";

            Response.AddHeader("Content-Disposition", "attachment;filename=consulta.pdf");

            Response.BinaryWrite(oStream.ToArray());

            //Response.End();

            //crvClienAtenOperador.ReportSource = obRpt;

            //crvClienAtenOperador.DataBind();

        }

        else
        {

            MemoryStream oStream1; // using System.IO 

            oStream1 = (MemoryStream)obRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);

            Response.Clear();

            Response.Buffer = true;

            Response.ContentType = "application/vnd.ms-excel";

            Response.BinaryWrite(oStream1.ToArray());

            //Response.End();

        }


    }
}
