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

public partial class Modulo_Consultas_ReporteCNS_rptTicketxFecha : System.Web.UI.Page
{
    protected Hashtable htLabels;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Set Paramaters values
        string sDesde = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        string sHasta = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        string idHoraDesde = Fecha.convertToHoraSQL(Request.QueryString["idHoraDesde"]);
        string idHoraHasta = Fecha.convertToHoraSQL(Request.QueryString["idHoraHasta"]);
        string idCompania = Request.QueryString["idCompania"];
        string idEstadoTicket = Request.QueryString["idEstadoTicket"];
        string idPersona = Request.QueryString["idPersona"];
        string idVuelo = Request.QueryString["idVuelo"];
        string idTipoTicket = Request.QueryString["idTipoTicket"];
        string idFlgCobro = Request.QueryString["idFlgCobro"];  
        string idTipoDocumento = Request.QueryString["idTipoDoc"];
        string idMasiva = Request.QueryString["idChkMasiva"];
        string idEstadoTurno = Request.QueryString["idEstadoTurno"];
        string idCajero = Request.QueryString["idCajero"];
        string idMedioAnulacion = Request.QueryString["idMedioAnulacion"];
        //Descripciones
        string idDscT = Request.QueryString["idDscT"];
        string idDscC = Request.QueryString["idDscC"];
        string idDscE = Request.QueryString["idDscE"];
        string idDscP = Request.QueryString["idDscP"];
        string idDscK = Request.QueryString["idDscK"];
        string idDscV = Request.QueryString["idDscV"];
        string idDscET = Request.QueryString["idDscET"];
        string idDscCA = Request.QueryString["idDscCA"];
        
        //Web bussines Ticket x Fecha
        BO_Consultas objListarTicketxFecha = new BO_Consultas();
        DataTable dt_consulta = new DataTable();
        string strSortExp = Session["sortExpressionTicketBPXFecha"] == null ? null : Session["sortExpressionTicketBPXFecha"].ToString();


        DataTable dt_parametro = objListarTicketxFecha.ListarParametrosDefaultValue("LR");
        if (dt_parametro.Rows.Count > 0) 
        {
            DateTime fechaF = Convert.ToDateTime(Request.QueryString["sHasta"]);
            DateTime fechaI = Convert.ToDateTime(Request.QueryString["sDesde"]);

            TimeSpan ts = fechaF.Subtract(fechaI);
            int dias = ts.Days;
            int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

            if (dias > parametro)
            {
                lblmensaje.Text = "El periodo máximo de días a imprimir el reporte es " + parametro.ToString() + " días.";
            }

            else {

                if (Session["Print_TicketxFecha"] != null)
                {
                    dt_consulta = (DataTable)Session["Print_TicketxFecha"];
                }
                else
                {
                    string usuarios = "";
                    if (idEstadoTicket == "X")
                    {
                        DataTable dt_usuarios = new DataTable();
                        BO_Consultas objConsulta = new BO_Consultas();

                        if (idMedioAnulacion == "SUP")
                        {
                            dt_usuarios = objConsulta.ListarUsuarioxRol((string)Property.htProperty[Define.ID_ROL_SUPER]);
                        }
                        else if (idMedioAnulacion == "ADM")
                        {
                            dt_usuarios = objConsulta.ListarUsuarioxRol((string)Property.htProperty[Define.ID_ROL_ADMIN]);
                        }

                        if (dt_usuarios.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_usuarios.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    usuarios = dt_usuarios.Rows[i]["Cod_Usuario"].ToString();
                                }
                                else
                                {
                                    usuarios += "," + dt_usuarios.Rows[i]["Cod_Usuario"].ToString();
                                }
                            }
                        }
                        else if (idMedioAnulacion == "0")
                        {
                            usuarios = ""; //quiere decir Todos
                        }
                        else if (idMedioAnulacion == "AUT")
                        {
                            if (idTipoDocumento == "T")
                            {
                                usuarios = (string)Property.htProperty[Define.ID_ROL_AUTO];
                            }
                            else
                            {
                                usuarios = "";
                            }
                        }
                   
                    }
                    else {
                        usuarios = "";
                    }

                    dt_consulta = objListarTicketxFecha.ListarTicketxFechaPagin(idTipoDocumento,
                                                            sDesde,
                                                            sHasta,
                                                            idHoraDesde,
                                                            idHoraHasta,
                                                            idCompania,
                                                            idEstadoTicket,
                                                            idTipoTicket,
                                                            idPersona,
                                                            idVuelo,
                                                            null,
                                                            null,
                                                            idFlgCobro,
                                                            idMasiva,
                                                            idEstadoTurno,
                                                            idCajero,
                                                            usuarios,
                                                            strSortExp,
                                                            0,
                                                            0,
                                                            "0");
                }

                if (dt_consulta == null || dt_consulta.Rows.Count == 0)
                {
                    Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
                }
                else
                {
                    if (idTipoDocumento == "T" || idTipoDocumento == "B")
                    {
                        CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                        if (idTipoDocumento == "B")
                            obRpt.Load(Server.MapPath("").ToString() + @"\rptBoardingxFecha.rpt");
                        else if (idTipoDocumento == "T")
                            obRpt.Load(Server.MapPath("").ToString() + @"\rptTicketxFecha.rpt");

                        obRpt.SetDataSource(dt_consulta);
                        crptvTicketxFecha.ReportSource = obRpt;

                        obRpt.SetParameterValue("pFechaIni", Fecha.convertSQLToFecha(sDesde, idHoraDesde));
                        obRpt.SetParameterValue("pFechaFin", Fecha.convertSQLToFecha(sHasta, idHoraHasta));
                        obRpt.SetParameterValue("pCompania", (idDscC == null || idDscC.Length == 0) ? " -TODOS- " : idDscC);
                        obRpt.SetParameterValue("pEstado", (idDscE == null || idDscE.Length == 0) ? " -TODOS- " : idDscE);
                        obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                        obRpt.SetParameterValue("pTipoPersona", (idDscP == null || idDscP.Length == 0) ? " -TODOS- " : idDscP);
                        obRpt.SetParameterValue("pVuelo", (idDscV == null || idDscV.Length == 0) ? " -TODOS- " : idDscV);
                        obRpt.SetParameterValue("pTipoTicket", (idDscT == null || idDscT.Length == 0) ? " -TODOS- " : idDscT);
                        if (idTipoDocumento == "T")
                        {
                            obRpt.SetParameterValue("pTipoCobro", (idDscK == null || idDscK.Length == 0) ? " -TODOS- " : idDscK);
                            obRpt.SetParameterValue("pVentaMasiva", (idMasiva == null || idMasiva.Length == 0 || idMasiva == "0") ? "NO" : "SI");
                            obRpt.SetParameterValue("pEstadoTurno", (idDscET == null || idDscET.Length == 0) ? " - TODOS- " : idDscET);
                            obRpt.SetParameterValue("pCajero", (idDscCA == null || idDscCA.Length == 0) ? " -TODOS- " : idDscCA);
                        }
                    }
                }
            }    
            
            
            }
        
        }

    /*private string MedioAnulacion()
    {

        DataTable dt_usuarios = new DataTable();
        BO_Consultas objConsulta = new BO_Consultas();
        string usuarios = "";

        //obtener usuario por rol
        if (idMedioAnulacion == "SUP")
        {
            dt_usuarios = objConsulta.ListarUsuarioxRol((string)Property.htProperty[Define.ID_ROL_SUPER]);
        }
        else if (idMedioAnulacion == "ADM")
        {
            dt_usuarios = objConsulta.ListarUsuarioxRol((string)Property.htProperty[Define.ID_ROL_ADMIN]);
        }

        if (dt_usuarios.Rows.Count > 0)
        {
            for (int i = 0; i < dt_usuarios.Rows.Count; i++)
            {
                if (i == 0)
                {
                    usuarios = dt_usuarios.Rows[i]["Cod_Usuario"].ToString();
                }
                else
                {
                    usuarios += "," + dt_usuarios.Rows[i]["Cod_Usuario"].ToString();
                }
            }
        }
        else if (idMedioAnulacion == "0")
        {
            usuarios = ""; //quiere decir Todos
        }
        else if (idMedioAnulacion == "AUT")
        {
            if (ddlTipoDocumento.SelectedValue == "T")
            {
                usuarios = (string)Property.htProperty[Define.ID_ROL_AUTO];
            }
            else
            {
                usuarios = "";
            }
        }
        else
        {
            usuarios = "";
        }
        return usuarios;
    }*/

        //begin_kinzi
       
}
