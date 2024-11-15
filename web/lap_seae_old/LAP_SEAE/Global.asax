<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        LAP.TUUA.UTIL.ErrorHandler.CargarErrorTypes(AppDomain.CurrentDomain.BaseDirectory + "resources");
        LAP.TUUA.UTIL.LabelConfig.LoadData();

        LAP.TUUA.UTIL.Property.CargarPropiedades(AppDomain.CurrentDomain.BaseDirectory + "resources/");
        //carga path de recursos
        if (!LAP.TUUA.UTIL.Property.htProperty.ContainsKey("PATHRECURSOS"))
        {
            LAP.TUUA.UTIL.Property.htProperty.Add("PATHRECURSOS", HttpContext.Current.Server.MapPath("."));
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
