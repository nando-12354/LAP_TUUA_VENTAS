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
    public partial class tuua : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.CargarInfoUsuario();
                this.CargarFechaActual();
                this.CargarMenu();
                LAP.Seguridad.Util.Web.SeleccionarOpcionActual(barMenu);
            }
        }

        #region Metodos Privados

        /// <summary>
        /// Cargar las Opciones de Menu
        /// </summary>
        private void CargarMenu()
        {
            try
            {
                LAP.Seguridad.Util.Web.CargarMenu(barMenu);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;                
            }
        }

        /// <summary>
        /// Muesta a la izquierda de la pantalla el Login del Usuario actual
        /// </summary>
        private void CargarInfoUsuario()
        {
            try
            {
                LAP.Seguridad.Entidades.Usuario objData = (LAP.Seguridad.Entidades.Usuario)Session["UsuarioActual"];
                lblUsuarioDesc.Text = objData.Nombres + " " + objData.Apellidos;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;                
            }
        }

        /// <summary>
        /// Muestra a la derecha de la pantalla la fecha actual
        /// </summary>
        private void CargarFechaActual()
        {
            try
            {
                lblFechaDesc.Text = DateTime.Today.ToString("dddd, dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-PE"));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;                
            }
        }

        #endregion

        #region Metodos de Controles

        protected void barMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            LAP.Seguridad.Util.Web.AbrirPagina(e);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            LAP.Seguridad.Util.Web.CerrarSesion(false);
        }

        #endregion
    }
}
