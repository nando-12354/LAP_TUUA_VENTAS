///V.1.4.6.0
///Luz Huaman
///Copyright ( Copyright © HIPER S.A. )

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
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class ReporteCNS_rptTicketBoardingUsados : System.Web.UI.Page
{
    BO_Consultas objBOConsultas = new BO_Consultas();

    protected void Page_Load(object sender, EventArgs e)
    {
        string FechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        string FechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        string HoraDesde = Fecha.convertToHoraSQL(Request.QueryString["idHoraDesde"]);
        string HoraHasta = Fecha.convertToHoraSQL(Request.QueryString["idHoraHasta"]);

        string idCompania = Request.QueryString["idCompania"];
        string idTipoVuelo = Request.QueryString["idTipoVuelo"];
        string idNumVuelo = Request.QueryString["idNumVuelo"];
        string idTipoPasajero = Request.QueryString["idTipoPasajero"];
        string idTipoDocumento = Request.QueryString["idTipoDocumento"];
        string idTipoTrasbordo = Request.QueryString["idTipoTrasbordo"];
        string idFechaVuelo = Request.QueryString["idFechaVuelo"];
        string sEstado = Request.QueryString["idEstado"];

        ///<sumarry>Descripciones</sumarry>
        string idDscD = Request.QueryString["idDscD"];///<see>Documento<see>
        string idDscC = Request.QueryString["idDscC"];///<see>Compania<see>
        string idDscE = Request.QueryString["idDscE"];///<see>Estado<see>
        string idDscP = Request.QueryString["idDscP"];///<see>Persona<see>
        string idDscV = Request.QueryString["idDscV"];///<see>Vuelo<see>
        string idDscT = Request.QueryString["idDscT"];///<see>Trasbordo<see>

        if (idFechaVuelo != "")
        {
            idFechaVuelo = idFechaVuelo.Substring(6, 4) + idFechaVuelo.Substring(3, 2) + idFechaVuelo.Substring(0, 2);
        }
        ///<sumarry> Validacion Limite de Rango de Fechas en Reportes <sumarry>
        DataTable dt_parametro = objBOConsultas.ListarParametrosDefaultValue("LR");
        if (dt_parametro.Rows.Count > 0)
        {
            ///<sumarry>RecuperarFiltros();
            DateTime fechaF = Convert.ToDateTime(Request.QueryString["sHasta"]);
            DateTime fechaI = Convert.ToDateTime(Request.QueryString["sDesde"]);
            TimeSpan ts = fechaF.Subtract(fechaI);
            int dias = ts.Days;
            int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

            if (dias > parametro)
            {
                Response.Write("El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.");
            }
            else
            {
                DataTable dt_consulta = new DataTable();

                
                dt_consulta = objBOConsultas.ListarTicketBoardingUsadosPagin(FechaDesde, FechaHasta, HoraDesde, HoraHasta,
                    idCompania, idTipoVuelo, idNumVuelo, idTipoPasajero, idTipoDocumento, idTipoTrasbordo, idFechaVuelo, sEstado,
                    null, 0, 0, "0");
              
                if (dt_consulta.Rows.Count == 0)
                {
                    Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
                }
                else
                {
                    ///<sumarry>Pintar el reporte 
                    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    obRpt.Load(Server.MapPath("").ToString() + @"\rptTicketUsados.rpt");
                    ///<sumarry>Poblar el reporte con el datatable
                    obRpt.SetDataSource(dt_consulta);
                    crvTicketUsados.ReportSource = obRpt;

                    obRpt.SetParameterValue("pFechaIni", Fecha.convertSQLToFecha(FechaDesde, HoraDesde));
                    obRpt.SetParameterValue("pFechaFin", Fecha.convertSQLToFecha(FechaHasta, HoraHasta));
                    obRpt.SetParameterValue("pCompania", (idDscC == null || idDscC.Length == 0) ? " -TODOS- " : idDscC);
                    obRpt.SetParameterValue("pEstado", (idDscE == null || idDscE.Length == 0) ? " -TODOS- " : idDscE);
                    obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                    obRpt.SetParameterValue("pPersona", (idDscP == null || idDscP.Length == 0) ? " -TODOS- " : idDscP);
                    obRpt.SetParameterValue("pVuelo", (idDscV == null || idDscV.Length == 0) ? " -TODOS- " : idDscV);
                    obRpt.SetParameterValue("pDocumento", (idDscD == null || idDscD.Length == 0) ? " -TODOS- " : idDscD);
                    obRpt.SetParameterValue("pFchVuelo", Fecha.convertSQLToFecha(idFechaVuelo, null));
                    obRpt.SetParameterValue("pTrasbordo", (idDscT == null || idDscT.Length == 0) ? " -TODOS- " : idDscT);
                }
            }
        }
    }
    protected void crvTicketUsados_Unload(object sender, EventArgs e)
    {
        
        
    }
}
