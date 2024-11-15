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
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using System.Text;

///<summary> lhuaman 24/08/2012<sumarry> 
public partial class Cns_Depuracion : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;

    BO_Operacion objOperacion = new BO_Operacion();
    BO_Consultas objBOConsultas = new BO_Consultas();
    UIControles objCargaCombo = new UIControles();
    BO_Consultas objParametro = new BO_Consultas();
    BO_Consultas objListaParametro = new BO_Consultas();
    BO_Configuracion objBOConfiguracion = new BO_Configuracion();
    UIControles objControles = new UIControles();
    DataTable dt_consulta = new DataTable();
    DataTable dt_parametroTurno = new DataTable();

    ///<see>Filtros<see>
    string sMolinete;
    string sEstado; 
    string sTablaSincronizacion;
    string sFiltro;
    string sOrdenacion;
    string sFchDesde;
    string sFchHasta;
    string sHraDesde;
    string sHraHasta;

    


    #region Event - Handlers
    /// <sumarry> Durante la carga, si la solicitud actual es una devolución de datos, 
    /// <sumarry> las propiedades del control se cargan con información recuperada del estado de vista y del estado del control.
    ///<summary> lhuaman 24/08/2012<sumarry> 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblDesde.Text = htLabels["mconsultaTicketFecha.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["mconsultaTicketFecha.lblHasta.Text"].ToString();
                this.lblTablaSincronizacion.Text = htLabels["mconsultaDepuracion.lblTablaSincronizacion.Text"].ToString();            
                this.lblMolinete.Text = htLabels["mconsultaSincroniza.lblMolinete.text"].ToString();
                this.lblEstado.Text = htLabels["mconsultaBoardingUsados.lblEstado.Text"].ToString();
                this.btnConsultar.Text = htLabels["mconsultaTicketFecha.btnConsultar.Text"].ToString();
               

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

            this.txtDesde.Text = DateTime.Now.ToShortDateString();
            this.txtHasta.Text = DateTime.Now.ToShortDateString();

            


            CargarFiltros();

        }

    }
    ///<summary> lhuaman 24/08/2012<sumarry> 
    protected void cmdConsultar_Click(object sender, EventArgs e)
    {
        if (IsFechaValida())
        {
            SaveFiltros();
            CargarGrilla();
        }

    }

    ///<summary> lhuaman 24/08/2012<sumarry> 

    # region Cargar Grilla
    protected void CargarGrilla()
    {
        try
        {
            htLabels = LabelConfig.htLabels;
            formatearValores();
            CargarDataTableGrilla();


            if (dt_consulta.Rows.Count < 1)
            {
                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                    grvDepuracion.DataSource = null;
                    grvDepuracion.DataBind();
                    lblTotal.Text = "";

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
            }
            else
            {

                ///<see>Traer valor de tamaño de la grilla desde parametro general<see>

                dt_parametroTurno = objParametro.ListarParametros("LG");
                if (dt_parametroTurno.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString();
                }
                ///<see>Cargar datos en la grilla<see>
                this.lblMensajeErrorData.Text = "";
                this.grvDepuracion.DataSource = dt_consulta;
                this.grvDepuracion.AllowPaging = true;
                this.grvDepuracion.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
                this.grvDepuracion.DataBind();
                lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dt_consulta.Rows.Count;


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

    #endregion

    ///<summary> lhuaman 24/08/2012<sumarry> 

    void CargarDataTableGrilla()
    {
        try
        {

            sMolinete = ddlTipoDocumento.SelectedValue;
            sEstado = ddlEstado.SelectedValue;
            
            sTablaSincronizacion = ddlCompania.SelectedValue;

            sFchDesde = txtDesde.Text;
            sFchHasta = txtHasta.Text;
            sHraDesde = txtHoraDesde.Text;
            sHraHasta = txtHoraHasta.Text;

           

            sFiltro = null;
            sOrdenacion = null;

            formatearValores();


            dt_consulta = objBOConsultas.ListarFiltroDepuracion(sMolinete, sEstado, sTablaSincronizacion, sFchDesde, sFchHasta, sHraDesde,
                                                                        sHraHasta, sFiltro, sOrdenacion);



            if (dt_consulta != null)
            {
                dt_consulta.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            }

            Session["tabla"] = dt_consulta;
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
    ///<summary> lhuaman 24/08/2012<sumarry> 

    # region Formatear Horas
    void formatearValores()
    {
        try
        {

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

    # endregion

    ///<summary> lhuaman 24/08/2012<sumarry> 

    # region Fecha Valida
    private bool IsFechaValida()
    {
        int iValiFechas;
        if (txtHoraDesde.Text != "" && txtHoraHasta.Text == "")
        {
            string pNewHraDesde = txtHoraDesde.Text;
            string pNewHraHasta = txtHoraHasta.Text;



            pNewHraDesde = "23:59:59";
            pNewHraHasta = "23:59:59";


            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + pNewHraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + pNewHraHasta));

        }
        else
        {
            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + sHraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + sHraHasta));

        }

        if (iValiFechas == 1)
        {
            lblMensajeErrorData.Text = "Filtro de fecha invalido";
            lblMensajeErrorData.Text = "Filtro de fecha invalido";
            grvDepuracion.Visible = false;
            lblTotal.Text = "";

            return false;
        }
        else
        {
            this.lblMensajeErrorData.Text = "Filtro de fecha invalido";
            return true;
        }
    }
    //<sumarry> lhuaman - cambios <sumarry> 
  

    # endregion

    ///<summary> lhuaman 24/08/2012<sumarry> 


    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sTablaSincronizacion = newFilterList[0].Valor;
        sMolinete = newFilterList[1].Valor;
        sEstado = newFilterList[2].Valor;
       
        sFchDesde = newFilterList[3].Valor;
        sFchHasta = newFilterList[4].Valor;
        sHraDesde = newFilterList[5].Valor;
        sHraHasta = newFilterList[6].Valor;

     

    }
    ///<summary> lhuaman 24/08/2012<sumarry> 
    private void SaveFiltros()
    {

        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sTablaSincronizacion", ddlCompania.SelectedValue));
        filterList.Add(new Filtros("sMolinete", ddlTipoDocumento.SelectedValue));
        filterList.Add(new Filtros("sEstado", ddlEstado.SelectedValue));
     
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));

        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(txtHoraHasta.Text)));


       
        ViewState.Add("Filtros", filterList);
    }


    /// <see>Se produce cuando se hace clic en el control Button<see>

    #endregion

    ///<summary> lhuaman 24/08/2012<sumarry> 

    #region Cargar/Guardas Filtros de Consulta
    public void CargarFiltros()
    {
        try
        {
            //<see>Carga filtro Tabla de Sincronizacon            
            DataTable dt_allcompania = objBOConsultas.ListaCamposxNombre("TablaDepuracion");
            objCargaCombo.LlenarCombo(ddlCompania, dt_allcompania, "Cod_Campo", "Dsc_Campo", true, false);
          
            //<see>Carga filtro Molinete                        
            DataTable dt_allMolinete = new DataTable();
            dt_allMolinete = objOperacion.ListarMolinetes(null, null);
            UIControles objCargaComboMolinete = new UIControles();
            objCargaComboMolinete.LlenarCombo(ddlTipoDocumento, dt_allMolinete, "Cod_Molinete", "Dsc_Molinete", true, false);

            //<see>Carga filtro Tipo Estado Sincronización                   
            DataTable dt_tiposincronizacion = objBOConsultas.ListaCamposxNombre("EstadoDepuracion");
            objCargaCombo.LlenarCombo(ddlEstado, dt_tiposincronizacion, "Cod_Campo", "Dsc_Campo", true, false);

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


    #endregion


    #region Paginacion
    ///<summary> lhuaman 24/08/2012<sumarry> 

    protected void grvDepuracion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtSincronizacion = (DataTable)Session["tabla"];

        int pageIndex = grvDepuracion.PageIndex;
        int pageSize = grvDepuracion.PageSize;
        int pageCount = grvDepuracion.PageCount;

        ///<summary> lhuaman 24/08/2012<sumarry> 

        #region Guardo las selecciones del combo y checkbox
        pageIndex = grvDepuracion.PageIndex;
        int limite;

        if ((pageIndex + 1) < grvDepuracion.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtSincronizacion.Rows.Count - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
        }
      
        #endregion

        ///<summary> lhuaman 24/08/2012<sumarry> 
        #region Define el PageIndex
        if (dtSincronizacion.Rows.Count == (pageIndex * pageSize) && pageIndex != 0)//esta ultima condicion por el eliminar el unico elemento
        {
            grvDepuracion.PageIndex = pageIndex - 1;
        }
        #endregion

        if (dtSincronizacion.Rows.Count == 0)
        {
            ViewState["tabla"] = null;
            #region Agregando fila vacia a la grilla por default
            dtSincronizacion.Rows.Add(dtSincronizacion.NewRow());

            grvDepuracion.DataSource = dtSincronizacion;
            grvDepuracion.DataBind();

            grvDepuracion.Rows[0].FindControl("chkSeleccionar").Visible = false;

            #endregion
        }
        else
        {
            grvDepuracion.DataSource = dtSincronizacion;
            grvDepuracion.DataBind();


            #region  actualizo la seleccion del check.


            pageIndex = grvDepuracion.PageIndex;

            if ((pageIndex + 1) < grvDepuracion.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtSincronizacion.Rows.Count - (pageIndex * pageSize);

            }

           
            #endregion

        }



    }
    ///<summary> lhuaman 24/08/2012<sumarry> 
    protected void grvDepuracion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {


        DataTable dtSincronizacion = (DataTable)Session["tabla"];


        int pageIndex = grvDepuracion.PageIndex;
        int pageSize = grvDepuracion.PageSize;
        int pageCount = grvDepuracion.PageCount;
        ///<summary> lhuaman 24/08/2012<sumarry> 

        #region Guardo las seleccion checkbox

        int limite;

        if ((pageIndex + 1) < grvDepuracion.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtSincronizacion.Rows.Count - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
        }
    
        #endregion

        grvDepuracion.PageIndex = e.NewPageIndex;
        grvDepuracion.DataSource = dtSincronizacion;
        grvDepuracion.DataBind();

        repaintGrilla(); // Para el chekc durante la paginación

    }
    ///<summary> lhuaman 24/08/2012<sumarry> 
    #region Para que los check se mantengan en la Paginacion
    public void repaintGrilla()
    {
        DataTable dtSincronizacion = (DataTable)Session["tabla"];


        int pageIndex = grvDepuracion.PageIndex;
        int pageSize = grvDepuracion.PageSize;
        int pageCount = grvDepuracion.PageCount;




        int limite;

        if ((pageIndex + 1) < grvDepuracion.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtSincronizacion.Rows.Count - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
        }
       

    }

    #endregion


    ///<summary> lhuaman 24/08/2012<sumarry> 
    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            //dv.Sort = sortExpression + " " + direction;
            //Soporte Ordenacion con varios campos
            string[] vec = sortExpression.Split(',');
            string sortExpressionMultiple = "";
            for (int i = 0; i <= vec.Length - 1; i++)
            {
                sortExpressionMultiple += vec[i].Trim() + " " + direction;
                if (i + 1 <= vec.Length - 1)
                    sortExpressionMultiple += ",";
            }
            dv.Sort = sortExpressionMultiple;
        }

        return dv;
    }


    #endregion

    ///<summary> lhuaman 24/08/2012<sumarry> 
    #region Ordenacion
    //<sumarry> lhuaman - cambios <sumarry> 
    // <see> se produce cuando se hace clic en el hipervínculo para ordenar una columna, pero antes de que el control GridView se ocupe de la operación de ordenación.
    protected void grvDepuracion_Sorting(object sender, GridViewSortEventArgs e)
    {

        metodo();

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

    ////<sumarry> lhuaman - cambios <sumarry> 

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

    //<sumarry> lhuaman - cambios <sumarry> 

    private void SortGridView(string sortExpression, String direction)
    {
        try
        {
            CargarDataTableGrilla();
            Session["dt_consultaSincronizacion"] = objControles.ConvertDataTable(dwConsulta(dt_consulta, sortExpression, direction));
            this.grvDepuracion.DataSource = dwConsulta(dt_consulta, sortExpression, direction);
            this.grvDepuracion.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvDepuracion.DataBind();
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
                Response.Redirect("PaginaError.aspx");

        }

    }
    #endregion



    ///<summary> lhuaman 24/08/2012<sumarry> 

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=Depuracion.xls");
        this.EnableViewState = false;
        Response.Charset = string.Empty;
        System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
        myTextWriter = exportarExcel();
        Response.Write(myTextWriter.ToString());
        Response.End();
    }

    ///<summary> lhuaman 24/08/2012<sumarry> 

    public System.IO.StringWriter exportarExcel()
    {
        DataTable dt_consulta = new DataTable();

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta = objBOConsultas.ListarFiltroDepuracion
                (sMolinete, sEstado,sTablaSincronizacion, sFchDesde, sFchHasta, sHraDesde,
                sHraHasta, sFiltro, sOrdenacion);

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

        #endregion

        Excel Workbook = new Excel();

        #region Consulta de Sincronizacion
        Excel.Worksheet Consulta = new Excel.Worksheet("Sincronizacion");
        Consulta.Columns = new string[] { "Código", "Tabla Depuración", "Código Molinete", "Tipo de Estado", "Descripción Estado", "Fecha Inicio", "Fecha Fin","Tipo Depuración", "Número Registro" };
        Consulta.WidthColumns = new int[] { 80, 120, 80, 80, 120, 120, 120,80,120 };
        Consulta.DataFields = new string[] { "Cod_Sincronizacion", "Sincronizacion", "Cod_Molinete", "Tip_Estado", "Dsc_Estado", "Fecha_inicio_Excel", "Fecha_Fin", "Tip_Sincronizacion", "Num_Registro" };
        Consulta.Source = dt_consulta;
        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { Consulta };

        return Workbook.Save();
    }

    ///<summary> lhuaman 24/08/2012<sumarry> 
    #region metodo
    public void metodo()
    {

        DataTable dtSincronizacion = (DataTable)Session["tabla"];

        int pageIndex = grvDepuracion.PageIndex;
        int pageSize = grvDepuracion.PageSize;
        int pageCount = grvDepuracion.PageCount;


        #region Guardo las selecciones del combo y checkbox
        int limite;
        if ((pageIndex + 1) < pageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtSincronizacion.Rows.Count - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
        }
       
        #endregion

        Session["tabla"] = dtSincronizacion;


    }

    #endregion

}


