using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.Tuua.Web.Dao;
using LAP.Tuua.Web.Model;

namespace LAP.Tuua.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                string url_lap_linea = ConfigurationManager.AppSettings["url_lap_en_linea"];
                string cod_usuario;


                if (Session["cod_usr"] == null)
                {
                    string[] valoresToken = ValidarToken();
                    //0. codusuario|1. login|2. fechahora|3. IP|4. nombreApp|5. ruc|6. entidad
                    cod_usuario = valoresToken[0];
                    //guardar código en sesion
                    Session["cod_usr"] = cod_usuario;
                }
                else
                {
                    cod_usuario = (string)Session["cod_usr"];
                }

                int intCodigo = int.Parse(cod_usuario);

                
                this.AutenticarUsuarioExtranet(intCodigo);
                LAP.Seguridad.Entidades.Usuario objData = (LAP.Seguridad.Entidades.Usuario)Session["UsuarioActual"];
                string iata = objData.Apellidos.Replace("(", "");
                iata = iata.Replace(")", "");
                HttpContext.Current.Session["Iata"] = iata;
                Response.Redirect("~/pages/Rpt_BcbpDiario.aspx");
                
            }
        }

        #region Metodos Privados

        private string[] ValidarToken()
        {
            Saa_TokenDAO dao = new Saa_TokenDAO();
            string[] valores;
            string token = Request.QueryString["token"];
            string url_lap_linea = ConfigurationManager.AppSettings["url_lap_en_linea"];
            string llaveAuth = ConfigurationManager.AppSettings["llave_auth"];
            //verificar si existe token
            if (string.IsNullOrWhiteSpace(token))
            {
                Response.Redirect(url_lap_linea);
            }
            //desencriptar token
            string cadena = string.Empty;
            var objAuditoria = new LAP.Comun.Control.Auditoria();
            try
            {
                cadena = objAuditoria.Desencriptar(token, llaveAuth);
            }
            catch (Exception)
            {
                //token invalido
                Response.Redirect(url_lap_linea);
            }
            //cadena vacía
            if (string.IsNullOrWhiteSpace(cadena))
            {
                Response.Redirect(url_lap_linea);
            }

            valores = cadena.Split('|');

            //verificar vigencia de token (30')
            DateTime fechaToken = DateTime.ParseExact(valores[2], "yyyyMMddHHmm", CultureInfo.InvariantCulture);

            if (fechaToken.AddMinutes(30) < DateTime.Now)
            {
                //token expirado
                Response.Redirect(url_lap_linea);
            }

            //validar unicidad de token
            try
            {
                var auxToken = dao.ObtenerToken(token);
                if (auxToken != null)
                {
                    //token ya utilizado
                    Response.Redirect(url_lap_linea);
                }
            }
            catch (Exception ex)
            {

                lblMensaje.Text = ex.Message;
            }

            Saa_Token objToken = new Saa_Token();
            objToken.cod_usuario = int.Parse(valores[0]);
            objToken.dsc_token = token;
            objToken.fch_acceso = DateTime.Now;

            try
            {
                //guardar token
                dao.InsertarToken(objToken);
            }
            catch (Exception ex)
            {

                lblMensaje.Text = ex.Message;
            }

            return valores;

        }





        /// <summary>
        /// Autentica al Usuario en la Extranet
        /// </summary>
        private void AutenticarUsuarioExtranet(int codigo)
        {
            bool blnRpta = false;
            string strRutaDefecto = string.Empty;

            Saa_TokenDAO dao = new Saa_TokenDAO();

            LAP.Seguridad.Entidades.Usuario objUsuario = new LAP.Seguridad.Entidades.Usuario();

            objUsuario.Codigo = codigo;
            objUsuario = dao.LoginExtranet(objUsuario);
            HttpContext.Current.Session["UsuarioActual"] = objUsuario;
            string url_lap_linea = ConfigurationManager.AppSettings["url_lap_en_linea"];

            if (objUsuario == null)
            {
                Response.Redirect(url_lap_linea);
            }

        }

        #endregion
    }
}
