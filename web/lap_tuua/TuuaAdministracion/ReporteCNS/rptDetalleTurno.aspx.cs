using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class ReporteCNS_rptDetalleTurno : System.Web.UI.Page
{
    protected bool Flg_Error;
    protected BO_Consultas objBOConsulta = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    
    protected Hashtable htLabels;
    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt;

    protected bool Flg_Detalle;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get Parameter
        string sTurno = Request.QueryString["idTurno"];        
        //Consulta
        consultarDatos(sTurno);
        //Reporte
        generarReporte(sTurno);
    }

    private DataTable ResultTable(DataTable dtMonedaTurno, string idTurno) {
        DataTable dest = new DataTable();
        DataColumn dc;

        dc = new DataColumn("Col_Titulo");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Efectivo_Titulo");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Efectivo_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_E_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_E_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_T_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_T_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_C_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_C_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_D_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_D_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_Q_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Inter_Q_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_E_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_E_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_T_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_T_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_C_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_C_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_D_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_D_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_Q_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Nac_Q_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Compra_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Compra_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Venta_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Venta_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Ingreso_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Ingreso_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Egreso_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Egreso_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Efectivo_F_Titulo");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Efectivo_F_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Recaudacion_Titulo");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Recaudacion_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Datos_Titulo");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Extorno_I_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Extorno_I_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Extorno_N_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Extorno_N_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Anul_I_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Anul_I_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Anul_N_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Anul_N_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Infante_I_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Infante_I_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Infante_N_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Infante_N_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Cod_Dsc_Moneda");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Efectivo_SF_Titulo");
        dc.DefaultValue = "Efectivo Sobrante o Faltante";
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Efectivo_SF_Monto");
        dc.DefaultValue = "-";
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Efectivo_CAJ_Titulo");
        dc.DefaultValue = "Efectivo Final Cajero";
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_Efectivo_CAJ_Monto");
        dc.DefaultValue = "-";
        dest.Columns.Add(dc);
        //icano 25-06-10 agregando columnas al datatable
        dc = new DataColumn("Col_SubTot_E_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_E_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_T_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_T_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_D_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_D_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_C_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_C_Monto");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_Q_Num");
        dest.Columns.Add(dc);
        dc = new DataColumn("Col_SubTot_Q_Monto");
        dest.Columns.Add(dc);
        //fin
        bool bConDetalle = false;

        if (dtMonedaTurno.Rows.Count > 0)
        {
            DataTable dt_consulta_tmp = new DataTable();
            for (int i = 0; i < dtMonedaTurno.Rows.Count; i++)
            {
                dest.Rows.Add(dest.NewRow());
                string strTipMoneda = dtMonedaTurno.Rows[i]["Cod_Moneda"].ToString();
                dt_consulta_tmp = objBOConsulta.DetalleMonedasTurno(idTurno, strTipMoneda, null);
                DataRow[] foundRowTipoMoneda = dt_consulta_tmp.Select();
                if (foundRowTipoMoneda != null && foundRowTipoMoneda.Length > 0) {
                    
                    dest.Rows[i]["Cod_Dsc_Moneda"] = dtMonedaTurno.Rows[i]["Dsc_Moneda"].ToString();
                    foreach (DataRow r in foundRowTipoMoneda)
                    {
                        switch (r["IdDetalle"].ToString()) {
                            case "0": dest.Rows[i]["Col_Titulo"] = r["Descripcion"].ToString(); break;
                            case "1": dest.Rows[i]["Col_Efectivo_Titulo"] = r["Descripcion"].ToString();
                                      dest.Rows[i]["Col_Efectivo_Monto"] = (r["Monto"] != null)?r["Monto"].ToString():"";break;
                            //Cobro Tasa Internacional
                            case "2": dest.Rows[i]["Col_Inter_Num"] = r["Cantidad"].ToString();
                                      dest.Rows[i]["Col_Inter_Monto"] = (r["Monto"] != null)?r["Monto"].ToString():""; break;
                            case "21": dest.Rows[i]["Col_Inter_E_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Inter_E_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true;  break;
                            case "22": dest.Rows[i]["Col_Inter_T_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Inter_T_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "23": dest.Rows[i]["Col_Inter_C_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Inter_C_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "24": dest.Rows[i]["Col_Inter_D_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Inter_D_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "25": dest.Rows[i]["Col_Inter_Q_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Inter_Q_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            //Cobro Tasa Nacional
                            case "3": dest.Rows[i]["Col_Nac_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Nac_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            case "31": dest.Rows[i]["Col_Nac_E_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Nac_E_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "32": dest.Rows[i]["Col_Nac_T_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Nac_T_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "33": dest.Rows[i]["Col_Nac_C_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Nac_C_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "34": dest.Rows[i]["Col_Nac_D_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Nac_D_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "35": dest.Rows[i]["Col_Nac_Q_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Nac_Q_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            // Subtotales
                            //icano 25-06-10 nuevos campos para mostrar en el reporte
                            case "71": dest.Rows[i]["Col_SubTot_E_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_SubTot_E_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "72": dest.Rows[i]["Col_SubTot_T_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_SubTot_T_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "73": dest.Rows[i]["Col_SubTot_D_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_SubTot_D_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "74": dest.Rows[i]["Col_SubTot_C_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_SubTot_C_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            case "75": dest.Rows[i]["Col_SubTot_Q_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_SubTot_Q_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; bConDetalle = true; break;
                            //fin

                            //Compra ME
                            case "4": dest.Rows[i]["Col_Compra_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Compra_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            //Venta ME
                            case "5": dest.Rows[i]["Col_Venta_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Venta_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            //Ingresos
                            case "6": dest.Rows[i]["Col_Ingreso_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Ingreso_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            //Egresos
                            case "7": dest.Rows[i]["Col_Egreso_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Egreso_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            //Efectivo Final
                            case "8": dest.Rows[i]["Col_Efectivo_F_Titulo"] = r["Descripcion"].ToString();
                                dest.Rows[i]["Col_Efectivo_F_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            //Recaudacion Final
                            case "81": dest.Rows[i]["Col_Recaudacion_Titulo"] = r["Descripcion"].ToString();
                                dest.Rows[i]["Col_Recaudacion_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            //Datos Adicionales
                            case "90": dest.Rows[i]["Col_Datos_Titulo"] = r["Descripcion"].ToString(); break;
                            //Extorno
                            case "9": dest.Rows[i]["Col_Extorno_I_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Extorno_I_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            case "10": dest.Rows[i]["Col_Extorno_N_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Extorno_N_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            //Anulacion
                            case "91": dest.Rows[i]["Col_Anul_I_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Anul_I_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            case "92": dest.Rows[i]["Col_Anul_N_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Anul_N_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            //Infantes
                            case "11": dest.Rows[i]["Col_Infante_I_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Infante_I_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            case "12": dest.Rows[i]["Col_Infante_N_Num"] = r["Cantidad"].ToString();
                                dest.Rows[i]["Col_Infante_N_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            case "82": dest.Rows[i]["Col_Efectivo_SF_Titulo"] = r["Descripcion"].ToString();
                                dest.Rows[i]["Col_Efectivo_SF_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                            case "83": dest.Rows[i]["Col_Efectivo_CAJ_Titulo"] = r["Descripcion"].ToString();
                                dest.Rows[i]["Col_Efectivo_CAJ_Monto"] = (r["Monto"] != null) ? r["Monto"].ToString() : ""; break;
                        }
                    }
                    //dest.Rows[i]["Col_Est_Detalle"] = (bConDetalle)?"1":"0";                    
                }
            }
        }        
        Flg_Detalle = bConDetalle?true:false;
        dest.AcceptChanges();
        return dest;
    }

    public void consultarDatos(string sTurno) {
        DataTable dt_cantidadmonedaturno = new DataTable();
        dt_cantidadmonedaturno = objBOConsulta.CantidadMonedasTurno(sTurno);

        dt_consulta = ResultTable(dt_cantidadmonedaturno, sTurno);
        
    }

    public void generarReporte(string sTurno)
    { 
        try
        {
            
            //Pintar el reporte                 
            obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            
            if (Flg_Detalle)
                obRpt.Load(Server.MapPath("").ToString() + @"\rptDetalleTurno.rpt");
            else
                obRpt.Load(Server.MapPath("").ToString() + @"\rptDetalleTurnoSimple.rpt");

            DataSet dtset = new DataSet();
            dt_consulta.TableName = "dtDetalleTurno";
            dtset.Tables.Add(dt_consulta.Copy());
            obRpt.SetDataSource(dtset);

            obRpt.SetParameterValue("pTitulo", "Reporte Detalle de Turno");
            obRpt.SetParameterValue("pFiltro", "Turno: ");
            obRpt.SetParameterValue("pTurno", sTurno);
            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
            crptvDetTurno.ReportSource = obRpt;        
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
