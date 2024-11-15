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
using System.Globalization;


public partial class Modulo_Consultas_ConsultaAuditoria : System.Web.UI.Page
{
    protected Hashtable htLabels;
    public string orden = "";
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    UIControles objControles = new UIControles();
    bool flagError;
    protected int currentPageNumber = 1;
    public int count;
    public int valorMaxGrilla;

    string sTipoOperacion;
    string sTabla;
    string sCodModulo;
    string sCodSubModulo;
    string sCodUsuario;
    string sFchDesde;
    string sFchHasta;
    string sHraDesde;
    string sHraHasta;


    protected void Page_Load(object sender, EventArgs e)
    {
        CultureInfo culturaPeru = new CultureInfo("es-ES");
        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;

        Flg_Error = false;
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblOperacion.Text = htLabels["mconsultaAuditoria.lblOperacion.Text"].ToString();
                this.lblTabla.Text = htLabels["mconsultaAuditoria.lblTabla.Text"].ToString();
                this.lblModulo.Text = htLabels["mconsultaAuditoria.lblModulo.Text"].ToString();
                this.lblSubModulo.Text = htLabels["mconsultaAuditoria.lblSubModulo.Text"].ToString();
                this.lblUsuario.Text = htLabels["mconsultaAuditoria.lblUsuario.Text"].ToString();
                this.lblDesde.Text = htLabels["mconsultaAuditoria.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["mconsultaAuditoria.lblHasta.Text"].ToString();
                this.btnConsultar.Text = htLabels["mconsultaAuditoria.btnConsultar.Text"].ToString();  
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
            this.txtDesde.Text = DateTime.Now.ToShortDateString();
            this.txtHasta.Text = DateTime.Now.ToShortDateString();

            grvAuditoriaPagin.VirtualItemCount = GetRowCount();
            CargarGrilla();

        }
    }

    public void CargarCombos()
    {
        Flg_Error = false;
        try
        {
            //Establecer datatable TipoOperacion
            DataTable dt_operacion = new DataTable();
            dt_operacion = objConsulta.ListaCamposxNombre("TipoOperacion");
            objControles.LlenarCombo(ddlOperacion, dt_operacion, "Cod_Campo", "Dsc_Campo", true, false);

            //Establecer datatable Tablas de auditoria 
            DataTable dt_tabla = new DataTable();
            dt_tabla = objConsulta.FiltrosAuditorias(null,null,"1");
            objControles.LlenarCombo(ddlTabla, dt_tabla, "Codigo", "TablaXml", true, false);

            //Establecer datatable Modulo 
            DataTable dt_modulo = new DataTable();
            dt_modulo = objConsulta.FiltrosAuditorias(null,"0",null);
            objControles.LlenarCombo(ddlModulo, dt_modulo, "Cod_Modulo", "Dsc_Modulo", true, false);


            //Carga los sub modulos     
            DataTable dt_submodulo = new DataTable();
            dt_submodulo = objConsulta.FiltrosAuditorias(ddlModulo.SelectedValue, "1", null);
            objControles.LlenarCombo(ddlSubModulo, dt_submodulo, "Cod_Proceso", "Dsc_Proceso", true, false);

            //Carga todos los usuarios
            DataTable dt_usuario = new DataTable();
            dt_usuario = objConsulta.ListarAllUsuario();
            objControles.LlenarCombo(ddlUsuario, dt_usuario, "Cod_Usuario", "Usuario", true, false);

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


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        CargarGrilla();
    }


    void cargarDataTable()
    {
        try
        {
             sTipoOperacion = ddlOperacion.SelectedValue;
             sTabla = ddlTabla.SelectedValue;
             sCodModulo = ddlModulo.SelectedValue;
             sCodSubModulo = ddlSubModulo.SelectedValue;
             sCodUsuario = ddlUsuario.SelectedValue;
             sFchDesde = txtDesde.Text;
             sFchHasta = txtHasta.Text;
             sHraDesde = txtHoraDesde.Text;
             sHraHasta = txtHoraHasta.Text;


            if (sHraDesde == "__:__:__") { sHraDesde = ""; }
            if (sHraHasta == "__:__:__") { sHraHasta = ""; }

            if (sFchDesde != "")
            {
                string[] wordsFechaDesde = sFchDesde.Split('/');
                sFchDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
            }
            else { sFchDesde = ""; }



            if (sFchHasta != "")
            {
                string[] wordsFechaHasta = sFchHasta.Split('/');
                sFchHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
            }
            else { sFchHasta = ""; }


            if (sHraDesde != "")
            {
                string[] wordsDesde = sHraDesde.Split(':');
                sHraDesde = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
            }
            else { sHraDesde = ""; }



            if (sHraHasta != "")
            {
                string[] wordsHasta = sHraHasta.Split(':');
                sHraHasta = wordsHasta[0] + "" + wordsHasta[1] + "" + wordsHasta[2];
            }
            else { sHraHasta = ""; }


            
            //dt_consulta = objConsulta.obtenerconsultaAuditorias(sTipoOperacion,sTabla, sCodModulo, sCodSubModulo,sCodUsuario,sFchDesde,sFchHasta,sHraDesde,sHraHasta);
           
        
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

    void formatearValores()
    {
        try
        {
             sTipoOperacion = ddlOperacion.SelectedValue;
             sTabla = ddlTabla.SelectedValue;
             sCodModulo = ddlModulo.SelectedValue;
             sCodSubModulo = ddlSubModulo.SelectedValue;
             sCodUsuario = ddlUsuario.SelectedValue;
             sFchDesde = txtDesde.Text;
             sFchHasta = txtHasta.Text;
             sHraDesde = txtHoraDesde.Text;
             sHraHasta = txtHoraHasta.Text;


            if (sHraDesde == "__:__:__") { sHraDesde = ""; }
            if (sHraHasta == "__:__:__") { sHraHasta = ""; }

            if (sFchDesde != "")
            {
                string[] wordsFechaDesde = sFchDesde.Split('/');
                sFchDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
            }
            else { sFchDesde = ""; }



            if (sFchHasta != "")
            {
                string[] wordsFechaHasta = sFchHasta.Split('/');
                sFchHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
            }
            else { sFchHasta = ""; }


            if (sHraDesde != "")
            {
                string[] wordsDesde = sHraDesde.Split(':');
                sHraDesde = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
            }
            else { sHraDesde = ""; }



            if (sHraHasta != "")
            {
                string[] wordsHasta = sHraHasta.Split(':');
                sHraHasta = wordsHasta[0] + "" + wordsHasta[1] + "" + wordsHasta[2];
            }
            else { sHraHasta = ""; }



            //dt_consulta = objConsulta.obtenerconsultaAuditorias(sTipoOperacion,sTabla, sCodModulo, sCodSubModulo,sCodUsuario,sFchDesde,sFchHasta,sHraDesde,sHraHasta);


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

    protected void CargarGrilla()
    {
        try
        {
            grvAuditoriaPagin.VirtualItemCount = GetRowCount();
            //BindPagingGrid();
           
            //cargarDataTable();

            dt_consulta = GetDataPage(grvAuditoriaPagin.PageIndex, grvAuditoriaPagin.PageSize,
                                          grvAuditoriaPagin.OrderBy);

            htLabels = LabelConfig.htLabels;
            if (dt_consulta.Rows.Count < 1)
            {

                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
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
                lblTotal.Text = "";
                grvAuditoria.DataSource = null;
                grvAuditoria.DataBind();

                grvAuditoriaPagin.DataSource = null;
                grvAuditoriaPagin.DataBind();
            }
            else
            {

                CargarTamanoGrilla();

                this.lblMensajeError.Text = "";
                this.lblMensajeErrorData.Text = "";


                /**/
                //ObjectDataSource tmpDataSource = null;
                //Parameter tmpParametro = null;



                //tmpIConsultarData = dt_consulta;
                //tmpDataSource = new ObjectDataSource();
                //tmpDataSource.EnablePaging = true;
                //tmpDataSource.TypeName = "ConsultarDatosBL";
                //tmpDataSource.SelectMethod = "obtenerDatos";
                //tmpDataSource.SelectCountMethod = "obtenerNumRegistros";
                //tmpDataSource.StartRowIndexParameterName = "startRowIndex";
                //tmpDataSource.MaximumRowsParameterName = "maximumRows";
                //tmpParametro = new Parameter();
                //tmpParametro.Name = "tmpIConsultarData";
                //tmpParametro.Type = TypeCode.Object;
                //tmpDataSource.SelectParameters.Add(tmpParametro);
                //tmpDataSource.Selecting += new ObjectDataSourceSelectingEventHandler(tmpDataSource_Selecting);

                //datosGV.DataSource = tmpDataSource;
                //datosGV.DataBind();

 
                /**/



                //Cargar datos en la grilla      
                ViewState["tablaAuditoria"] = dt_consulta;
                //grvAuditoria.DataSource = dt_consulta;
                //grvAuditoria.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
                //grvAuditoria.PageIndex = 0;
                //grvAuditoria.DataBind();

                grvAuditoriaPagin.DataSource = dt_consulta;
                grvAuditoriaPagin.PageSize = valorMaxGrilla;
                grvAuditoriaPagin.DataBind();


                lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + count;
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



    public void CargarTamanoGrilla()
    {
        BO_Consultas objParametro = new BO_Consultas();
        DataTable dt_parametro = new DataTable();
        dt_parametro = objParametro.ListarParametros("LG");

        if (dt_parametro.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }

        return dv;
    }



    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["SortDirection"];

        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }


    private void SortGridView(string sortExpression, String direction)
    {
        try
        {
            cargarDataTable();
            ViewState["tablaAuditoria"] = objControles.ConvertDataTable(dwConsulta(dt_consulta, sortExpression, direction));
            this.grvAuditoria.DataSource = dwConsulta((DataTable)ViewState["tablaAuditoria"], sortExpression, direction);
            this.grvAuditoria.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvAuditoria.DataBind();
        }
        catch (Exception ex)
        {
            flagError = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (flagError)
                Response.Redirect("PaginaError.aspx");

        }

    }


    protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Carga los sub modulos     
        DataTable dt_submodulo = new DataTable();
        dt_submodulo = objConsulta.FiltrosAuditorias(ddlModulo.SelectedValue, "1", null);
        objControles.LlenarCombo(ddlSubModulo, dt_submodulo, "Cod_Proceso", "Dsc_Proceso", true, false);
    }


    protected void grvAuditoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //cargarDataTable();
        this.grvAuditoria.DataSource = dwConsulta((DataTable)ViewState["tablaAuditoria"], this.txtColumna.Text, txtOrdenacion.Text);
        this.grvAuditoria.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvAuditoria.PageIndex = e.NewPageIndex;
        grvAuditoria.DataBind();
    }

    protected void grvAuditoria_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;

            this.txtOrdenacion.Text = "DESC";
            SortGridView(sortExpression, "DESC");

        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;

            this.txtOrdenacion.Text = "ASC";
            SortGridView(sortExpression, "ASC");

        }
        this.txtColumna.Text = sortExpression;

    }



    protected void grvAuditoria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetalleAuditoria")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleCodTurno = (DataTable)ViewState["tablaAuditoria"];
            string NomUsuario = dtDetalleCodTurno.Rows[rowIndex]["Cta_Usuario"].ToString();
            string NomRoles = dtDetalleCodTurno.Rows[rowIndex]["NomRoles"].ToString();
            string NomTabla = dtDetalleCodTurno.Rows[rowIndex]["Log_Tabla_Mod"].ToString();
            string CodContador = dtDetalleCodTurno.Rows[rowIndex]["Cod_Contador"].ToString();
            
            DetalleAuditoria1.CargarDetalle(NomUsuario,NomRoles,NomTabla,CodContador);
 
        }
    }


    #region Dynamic data query
    private int GetRowCount()
    {
        formatearValores();
        dt_consulta = objConsulta.obtenerconsultaAuditoriasPagin(sTipoOperacion, sTabla, sCodModulo, sCodSubModulo, sCodUsuario, sFchDesde, sFchHasta, sHraDesde, sHraHasta, null, 0, 0, "1");
        //dt_consulta = objWBConsultas.ConsultaDetalleTicketPagin(this.txtNumTicket.Text, "", "", null, 0, 0, "1");
        count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
       
        return count;

    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        //ValidarTamanoGrilla();
        formatearValores();
        CargarTamanoGrilla();
        dt_consulta = objConsulta.obtenerconsultaAuditoriasPagin(sTipoOperacion, sTabla, sCodModulo, sCodSubModulo, sCodUsuario, sFchDesde, sFchHasta, sHraDesde, sHraHasta, sortExpression, pageIndex, valorMaxGrilla, "0");
        //dt_consulta = objWBConsultas.ConsultaDetalleTicketPagin(this.txtNumTicket.Text, "", "", sortExpression, pageIndex, valorMaxGrilla, "0");

        return dt_consulta;
    }
    #endregion


    protected void grvAuditoriaPagin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAuditoriaPagin.PageIndex = e.NewPageIndex;
        CargarGrilla();
    }

    protected void grvAuditoriaPagin_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetalleAuditoria")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleCodTurno = (DataTable)ViewState["tablaAuditoria"];
            string NomUsuario = dtDetalleCodTurno.Rows[rowIndex]["Cta_Usuario"].ToString();
            string NomRoles = dtDetalleCodTurno.Rows[rowIndex]["NomRoles"].ToString();
            string NomTabla = dtDetalleCodTurno.Rows[rowIndex]["Log_Tabla_Mod"].ToString();
            string CodContador = dtDetalleCodTurno.Rows[rowIndex]["Cod_Contador"].ToString();

            DetalleAuditoria1.CargarDetalle(NomUsuario, NomRoles, NomTabla, CodContador);

        }
    }

    protected void grvAuditoriaPagin_Sorting(object sender, GridViewSortEventArgs e)
    {
        CargarGrilla();
    }
}
