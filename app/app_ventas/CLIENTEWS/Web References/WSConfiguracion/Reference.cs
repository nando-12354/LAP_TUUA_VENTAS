﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace LAP.TUUA.CLIENTEWS.WSConfiguracion {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WSConfiguracionSoap", Namespace="http://tempuri.org/")]
    public partial class WSConfiguracion : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ListarAllParametrosGeneralesOperationCompleted;
        
        private System.Threading.SendOrPostCallback DetalleParametroGeneralxIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback GrabarParametroGeneralOperationCompleted;
        
        private System.Threading.SendOrPostCallback obtenerParametroGeneralOperationCompleted;
        
        private System.Threading.SendOrPostCallback RegistrarListaDeCampoOperationCompleted;
        
        private System.Threading.SendOrPostCallback ObtenerListaDeCampoOperationCompleted;
        
        private System.Threading.SendOrPostCallback EliminarListaDeCampoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WSConfiguracion() {
            this.Url = global::LAP.TUUA.CLIENTEWS.Properties.Settings.Default.CLIENTEWS_WSConfiguracion_WSConfiguracion;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ListarAllParametrosGeneralesCompletedEventHandler ListarAllParametrosGeneralesCompleted;
        
        /// <remarks/>
        public event DetalleParametroGeneralxIdCompletedEventHandler DetalleParametroGeneralxIdCompleted;
        
        /// <remarks/>
        public event GrabarParametroGeneralCompletedEventHandler GrabarParametroGeneralCompleted;
        
        /// <remarks/>
        public event obtenerParametroGeneralCompletedEventHandler obtenerParametroGeneralCompleted;
        
        /// <remarks/>
        public event RegistrarListaDeCampoCompletedEventHandler RegistrarListaDeCampoCompleted;
        
        /// <remarks/>
        public event ObtenerListaDeCampoCompletedEventHandler ObtenerListaDeCampoCompleted;
        
        /// <remarks/>
        public event EliminarListaDeCampoCompletedEventHandler EliminarListaDeCampoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ListarAllParametrosGenerales", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable ListarAllParametrosGenerales(string strParametro) {
            object[] results = this.Invoke("ListarAllParametrosGenerales", new object[] {
                        strParametro});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void ListarAllParametrosGeneralesAsync(string strParametro) {
            this.ListarAllParametrosGeneralesAsync(strParametro, null);
        }
        
        /// <remarks/>
        public void ListarAllParametrosGeneralesAsync(string strParametro, object userState) {
            if ((this.ListarAllParametrosGeneralesOperationCompleted == null)) {
                this.ListarAllParametrosGeneralesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListarAllParametrosGeneralesOperationCompleted);
            }
            this.InvokeAsync("ListarAllParametrosGenerales", new object[] {
                        strParametro}, this.ListarAllParametrosGeneralesOperationCompleted, userState);
        }
        
        private void OnListarAllParametrosGeneralesOperationCompleted(object arg) {
            if ((this.ListarAllParametrosGeneralesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListarAllParametrosGeneralesCompleted(this, new ListarAllParametrosGeneralesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DetalleParametroGeneralxId", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable DetalleParametroGeneralxId(string sIdentificador) {
            object[] results = this.Invoke("DetalleParametroGeneralxId", new object[] {
                        sIdentificador});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void DetalleParametroGeneralxIdAsync(string sIdentificador) {
            this.DetalleParametroGeneralxIdAsync(sIdentificador, null);
        }
        
        /// <remarks/>
        public void DetalleParametroGeneralxIdAsync(string sIdentificador, object userState) {
            if ((this.DetalleParametroGeneralxIdOperationCompleted == null)) {
                this.DetalleParametroGeneralxIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDetalleParametroGeneralxIdOperationCompleted);
            }
            this.InvokeAsync("DetalleParametroGeneralxId", new object[] {
                        sIdentificador}, this.DetalleParametroGeneralxIdOperationCompleted, userState);
        }
        
        private void OnDetalleParametroGeneralxIdOperationCompleted(object arg) {
            if ((this.DetalleParametroGeneralxIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DetalleParametroGeneralxIdCompleted(this, new DetalleParametroGeneralxIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GrabarParametroGeneral", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta) {
            object[] results = this.Invoke("GrabarParametroGeneral", new object[] {
                        sValoresFormulario,
                        sValoresGrilla,
                        sParametroVenta});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void GrabarParametroGeneralAsync(string sValoresFormulario, string sValoresGrilla, string sParametroVenta) {
            this.GrabarParametroGeneralAsync(sValoresFormulario, sValoresGrilla, sParametroVenta, null);
        }
        
        /// <remarks/>
        public void GrabarParametroGeneralAsync(string sValoresFormulario, string sValoresGrilla, string sParametroVenta, object userState) {
            if ((this.GrabarParametroGeneralOperationCompleted == null)) {
                this.GrabarParametroGeneralOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGrabarParametroGeneralOperationCompleted);
            }
            this.InvokeAsync("GrabarParametroGeneral", new object[] {
                        sValoresFormulario,
                        sValoresGrilla,
                        sParametroVenta}, this.GrabarParametroGeneralOperationCompleted, userState);
        }
        
        private void OnGrabarParametroGeneralOperationCompleted(object arg) {
            if ((this.GrabarParametroGeneralCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GrabarParametroGeneralCompleted(this, new GrabarParametroGeneralCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/obtenerParametroGeneral", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ParameGeneral obtenerParametroGeneral(string sCodParam) {
            object[] results = this.Invoke("obtenerParametroGeneral", new object[] {
                        sCodParam});
            return ((ParameGeneral)(results[0]));
        }
        
        /// <remarks/>
        public void obtenerParametroGeneralAsync(string sCodParam) {
            this.obtenerParametroGeneralAsync(sCodParam, null);
        }
        
        /// <remarks/>
        public void obtenerParametroGeneralAsync(string sCodParam, object userState) {
            if ((this.obtenerParametroGeneralOperationCompleted == null)) {
                this.obtenerParametroGeneralOperationCompleted = new System.Threading.SendOrPostCallback(this.OnobtenerParametroGeneralOperationCompleted);
            }
            this.InvokeAsync("obtenerParametroGeneral", new object[] {
                        sCodParam}, this.obtenerParametroGeneralOperationCompleted, userState);
        }
        
        private void OnobtenerParametroGeneralOperationCompleted(object arg) {
            if ((this.obtenerParametroGeneralCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.obtenerParametroGeneralCompleted(this, new obtenerParametroGeneralCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RegistrarListaDeCampo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool RegistrarListaDeCampo(ListaDeCampo objListaDeCampo, int intTipo) {
            object[] results = this.Invoke("RegistrarListaDeCampo", new object[] {
                        objListaDeCampo,
                        intTipo});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void RegistrarListaDeCampoAsync(ListaDeCampo objListaDeCampo, int intTipo) {
            this.RegistrarListaDeCampoAsync(objListaDeCampo, intTipo, null);
        }
        
        /// <remarks/>
        public void RegistrarListaDeCampoAsync(ListaDeCampo objListaDeCampo, int intTipo, object userState) {
            if ((this.RegistrarListaDeCampoOperationCompleted == null)) {
                this.RegistrarListaDeCampoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegistrarListaDeCampoOperationCompleted);
            }
            this.InvokeAsync("RegistrarListaDeCampo", new object[] {
                        objListaDeCampo,
                        intTipo}, this.RegistrarListaDeCampoOperationCompleted, userState);
        }
        
        private void OnRegistrarListaDeCampoOperationCompleted(object arg) {
            if ((this.RegistrarListaDeCampoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegistrarListaDeCampoCompleted(this, new RegistrarListaDeCampoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObtenerListaDeCampo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo) {
            object[] results = this.Invoke("ObtenerListaDeCampo", new object[] {
                        strNomCampo,
                        strCodCampo});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void ObtenerListaDeCampoAsync(string strNomCampo, string strCodCampo) {
            this.ObtenerListaDeCampoAsync(strNomCampo, strCodCampo, null);
        }
        
        /// <remarks/>
        public void ObtenerListaDeCampoAsync(string strNomCampo, string strCodCampo, object userState) {
            if ((this.ObtenerListaDeCampoOperationCompleted == null)) {
                this.ObtenerListaDeCampoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnObtenerListaDeCampoOperationCompleted);
            }
            this.InvokeAsync("ObtenerListaDeCampo", new object[] {
                        strNomCampo,
                        strCodCampo}, this.ObtenerListaDeCampoOperationCompleted, userState);
        }
        
        private void OnObtenerListaDeCampoOperationCompleted(object arg) {
            if ((this.ObtenerListaDeCampoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ObtenerListaDeCampoCompleted(this, new ObtenerListaDeCampoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EliminarListaDeCampo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool EliminarListaDeCampo(string strNomCampo, string strCodCampo) {
            object[] results = this.Invoke("EliminarListaDeCampo", new object[] {
                        strNomCampo,
                        strCodCampo});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void EliminarListaDeCampoAsync(string strNomCampo, string strCodCampo) {
            this.EliminarListaDeCampoAsync(strNomCampo, strCodCampo, null);
        }
        
        /// <remarks/>
        public void EliminarListaDeCampoAsync(string strNomCampo, string strCodCampo, object userState) {
            if ((this.EliminarListaDeCampoOperationCompleted == null)) {
                this.EliminarListaDeCampoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEliminarListaDeCampoOperationCompleted);
            }
            this.InvokeAsync("EliminarListaDeCampo", new object[] {
                        strNomCampo,
                        strCodCampo}, this.EliminarListaDeCampoOperationCompleted, userState);
        }
        
        private void OnEliminarListaDeCampoOperationCompleted(object arg) {
            if ((this.EliminarListaDeCampoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EliminarListaDeCampoCompleted(this, new EliminarListaDeCampoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ParameGeneral {
        
        private string sIdentificadorField;
        
        private string sDescripcionField;
        
        private string sTipoParametroField;
        
        private string sTipoParametroDetField;
        
        private string sTipoValorField;
        
        private string sValorField;
        
        private string sCampoListaField;
        
        private string sIdentificadorPadreField;
        
        private string sLog_Usuario_ModField;
        
        private string sLog_Fecha_ModField;
        
        private string sLog_Hora_ModField;
        
        private bool bFlagField;
        
        /// <remarks/>
        public string SIdentificador {
            get {
                return this.sIdentificadorField;
            }
            set {
                this.sIdentificadorField = value;
            }
        }
        
        /// <remarks/>
        public string SDescripcion {
            get {
                return this.sDescripcionField;
            }
            set {
                this.sDescripcionField = value;
            }
        }
        
        /// <remarks/>
        public string STipoParametro {
            get {
                return this.sTipoParametroField;
            }
            set {
                this.sTipoParametroField = value;
            }
        }
        
        /// <remarks/>
        public string STipoParametroDet {
            get {
                return this.sTipoParametroDetField;
            }
            set {
                this.sTipoParametroDetField = value;
            }
        }
        
        /// <remarks/>
        public string STipoValor {
            get {
                return this.sTipoValorField;
            }
            set {
                this.sTipoValorField = value;
            }
        }
        
        /// <remarks/>
        public string SValor {
            get {
                return this.sValorField;
            }
            set {
                this.sValorField = value;
            }
        }
        
        /// <remarks/>
        public string SCampoLista {
            get {
                return this.sCampoListaField;
            }
            set {
                this.sCampoListaField = value;
            }
        }
        
        /// <remarks/>
        public string SIdentificadorPadre {
            get {
                return this.sIdentificadorPadreField;
            }
            set {
                this.sIdentificadorPadreField = value;
            }
        }
        
        /// <remarks/>
        public string SLog_Usuario_Mod {
            get {
                return this.sLog_Usuario_ModField;
            }
            set {
                this.sLog_Usuario_ModField = value;
            }
        }
        
        /// <remarks/>
        public string SLog_Fecha_Mod {
            get {
                return this.sLog_Fecha_ModField;
            }
            set {
                this.sLog_Fecha_ModField = value;
            }
        }
        
        /// <remarks/>
        public string SLog_Hora_Mod {
            get {
                return this.sLog_Hora_ModField;
            }
            set {
                this.sLog_Hora_ModField = value;
            }
        }
        
        /// <remarks/>
        public bool BFlag {
            get {
                return this.bFlagField;
            }
            set {
                this.bFlagField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ListaDeCampo {
        
        private string sNomCampoField;
        
        private string sCodCampoField;
        
        private string sCodRelativoField;
        
        private string sDscCampoField;
        
        private string sLogUsuarioModField;
        
        private string sLogFechaModField;
        
        private string sLogHoraModField;
        
        /// <remarks/>
        public string SNomCampo {
            get {
                return this.sNomCampoField;
            }
            set {
                this.sNomCampoField = value;
            }
        }
        
        /// <remarks/>
        public string SCodCampo {
            get {
                return this.sCodCampoField;
            }
            set {
                this.sCodCampoField = value;
            }
        }
        
        /// <remarks/>
        public string SCodRelativo {
            get {
                return this.sCodRelativoField;
            }
            set {
                this.sCodRelativoField = value;
            }
        }
        
        /// <remarks/>
        public string SDscCampo {
            get {
                return this.sDscCampoField;
            }
            set {
                this.sDscCampoField = value;
            }
        }
        
        /// <remarks/>
        public string SLogUsuarioMod {
            get {
                return this.sLogUsuarioModField;
            }
            set {
                this.sLogUsuarioModField = value;
            }
        }
        
        /// <remarks/>
        public string SLogFechaMod {
            get {
                return this.sLogFechaModField;
            }
            set {
                this.sLogFechaModField = value;
            }
        }
        
        /// <remarks/>
        public string SLogHoraMod {
            get {
                return this.sLogHoraModField;
            }
            set {
                this.sLogHoraModField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void ListarAllParametrosGeneralesCompletedEventHandler(object sender, ListarAllParametrosGeneralesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListarAllParametrosGeneralesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListarAllParametrosGeneralesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void DetalleParametroGeneralxIdCompletedEventHandler(object sender, DetalleParametroGeneralxIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DetalleParametroGeneralxIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DetalleParametroGeneralxIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void GrabarParametroGeneralCompletedEventHandler(object sender, GrabarParametroGeneralCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GrabarParametroGeneralCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GrabarParametroGeneralCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void obtenerParametroGeneralCompletedEventHandler(object sender, obtenerParametroGeneralCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class obtenerParametroGeneralCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal obtenerParametroGeneralCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ParameGeneral Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ParameGeneral)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void RegistrarListaDeCampoCompletedEventHandler(object sender, RegistrarListaDeCampoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegistrarListaDeCampoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegistrarListaDeCampoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void ObtenerListaDeCampoCompletedEventHandler(object sender, ObtenerListaDeCampoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ObtenerListaDeCampoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ObtenerListaDeCampoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void EliminarListaDeCampoCompletedEventHandler(object sender, EliminarListaDeCampoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EliminarListaDeCampoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EliminarListaDeCampoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591