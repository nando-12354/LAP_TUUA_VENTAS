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

namespace LAP.Tuua.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //int intCodigo = 1145;
                String cod_usuario = Request.QueryString["codigo"];
                //String cod_usuario = "3";
                LAP.Comun.Control.Auditoria objAuditoria = new LAP.Comun.Control.Auditoria();
                int intCodigo = int.Parse(objAuditoria.Desencriptar(cod_usuario, "saa"));
                this.AutenticarUsuarioExtranet(intCodigo);
                LAP.Seguridad.Entidades.Usuario objData = (LAP.Seguridad.Entidades.Usuario)Session["UsuarioActual"];
                string iata = objData.Apellidos.Replace("(", "");
                iata = iata.Replace(")", "");
                HttpContext.Current.Session["Iata"] = iata;
                //if (intTipo == 0)
                    Response.Redirect("~/pages/Rpt_BcbpDiario.aspx");
                //else
                //  Response.Redirect("~/pages/Rpt_BcbpMensual.aspx");
            }
        }

        #region Metodos Privados

        /// <summary>
        /// Autentica al Usuario en la Extranet
        /// </summary>
        private void AutenticarUsuarioExtranet(int codigo)
        {
            bool blnRpta = false;
            string strRutaDefecto = string.Empty;

            try
            {
                lap.comun.DAO.web objTemporal = new lap.comun.DAO.web();
                LAP.Seguridad.Entidades.Usuario objUsuario = new LAP.Seguridad.Entidades.Usuario();

                objUsuario.Codigo = codigo;
                objUsuario = objTemporal.LoginExtranet(objUsuario);
                HttpContext.Current.Session["UsuarioActual"] = objUsuario;                

                blnRpta = LAP.Seguridad.Util.Web.AutenticarUsuarioWindows(objUsuario, out strRutaDefecto);

                string strQuery = string.Empty;

                for (int intCount = 0; intCount < Request.QueryString.Keys.Count; intCount++)
                {
                    strQuery = strQuery + Request.QueryString.Keys.Get(intCount) + "=" + Request.QueryString[intCount] + "&";
                }

                if (strQuery.Length > 0) strQuery = "?" + strQuery;
                strRutaDefecto = strRutaDefecto + strQuery;
            }
            catch (Exception ex)
            {
                string ss = ex.Message;                
            }

            //if (blnRpta) Response.Redirect(strRutaDefecto);
        }

        #endregion
    }
}
