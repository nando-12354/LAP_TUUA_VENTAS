using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using LAP.TUUA.UTIL;

/// <summary>
/// Summary description for WSError
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSError : System.Web.Services.WebService
{

    public WSError()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetErrorCode()
    {
        return ErrorHandler.Cod_Error;
    }
    [WebMethod]
    public bool IsError()
    {
        return ErrorHandler.Flg_Error;
    }

}

