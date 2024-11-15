using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using LAP.TUUA.ALARMASCLR; 

public partial class ProcesoAlarma
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_alr_pcs_send(string sCodAlarma, string sCodModulo, string sDesEquipo, string sTipoImport, string sAsunto, string sCuerpo, string sUser,  string sCodSubModulo)
    {
        SqlConnection conn = new SqlConnection("context connection = true");
        conn.Open();
        GestionAlarma.Registrar("", sCodAlarma, sCodModulo, sDesEquipo, sTipoImport, sAsunto, sCuerpo, sUser, sCodSubModulo, conn);
        conn.Close();
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_alr_pcs_send_mail(string sIdAlarma)
    {
        SqlConnection conn = new SqlConnection("context connection = true");
        conn.Open();
        GestionAlarma.EnviarAlarma(sIdAlarma, conn);  
        conn.Close();
    }
};
