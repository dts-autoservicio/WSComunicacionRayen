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

namespace ComunicacionRayen.WSFarmaciaTotem1 {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_IFarmaciaTotem", Namespace="http://tempuri.org/")]
    public partial class FarmaciaTotem : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GenerarTicketOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public FarmaciaTotem() {
            this.Url = global::ComunicacionRayen.Properties.Settings.Default.ComunicacionRayen_WSFarmaciaTotem1_FarmaciaTotem;
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
        public event GenerarTicketCompletedEventHandler GenerarTicketCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFarmaciaTotem/GenerarTicket", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ParametroRespuestaGenerarTicket GenerarTicket([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] ParametroGenerarTicket request) {
            object[] results = this.Invoke("GenerarTicket", new object[] {
                        request});
            return ((ParametroRespuestaGenerarTicket)(results[0]));
        }
        
        /// <remarks/>
        public void GenerarTicketAsync(ParametroGenerarTicket request) {
            this.GenerarTicketAsync(request, null);
        }
        
        /// <remarks/>
        public void GenerarTicketAsync(ParametroGenerarTicket request, object userState) {
            if ((this.GenerarTicketOperationCompleted == null)) {
                this.GenerarTicketOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerarTicketOperationCompleted);
            }
            this.InvokeAsync("GenerarTicket", new object[] {
                        request}, this.GenerarTicketOperationCompleted, userState);
        }
        
        private void OnGenerarTicketOperationCompleted(object arg) {
            if ((this.GenerarTicketCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerarTicketCompleted(this, new GenerarTicketCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/RayenSalud.Integraciones.TotemFarmacia.We" +
        "bService")]
    public partial class ParametroGenerarTicket {
        
        private int diasToleranciaField;
        
        private string idCentroField;
        
        private string rutField;
        
        /// <remarks/>
        public int DiasTolerancia {
            get {
                return this.diasToleranciaField;
            }
            set {
                this.diasToleranciaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string IdCentro {
            get {
                return this.idCentroField;
            }
            set {
                this.idCentroField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Rut {
            get {
                return this.rutField;
            }
            set {
                this.rutField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/RayenSalud.Integraciones.TotemFarmacia.We" +
        "bService")]
    public partial class TypeRespuestaBase {
        
        private int codigoField;
        
        private bool codigoFieldSpecified;
        
        private string descripcionField;
        
        /// <remarks/>
        public int Codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CodigoSpecified {
            get {
                return this.codigoFieldSpecified;
            }
            set {
                this.codigoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/RayenSalud.Integraciones.TotemFarmacia.We" +
        "bService")]
    public partial class ParametroRespuestaGenerarTicket {
        
        private string entregaTicketField;
        
        private string nombresPacienteField;
        
        private string primerApellidoPacienteField;
        
        private string segundoApellidoPacienteField;
        
        private string edadField;
        
        private string embarazoField;
        
        private string discapacidadField;
        
        private string atencionPreferenteCuidadorField;
        
        private TypeRespuestaBase respuestaBaseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string EntregaTicket {
            get {
                return this.entregaTicketField;
            }
            set {
                this.entregaTicketField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string NombresPaciente {
            get {
                return this.nombresPacienteField;
            }
            set {
                this.nombresPacienteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string PrimerApellidoPaciente {
            get {
                return this.primerApellidoPacienteField;
            }
            set {
                this.primerApellidoPacienteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SegundoApellidoPaciente {
            get {
                return this.segundoApellidoPacienteField;
            }
            set {
                this.segundoApellidoPacienteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Edad {
            get {
                return this.edadField;
            }
            set {
                this.edadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Embarazo {
            get {
                return this.embarazoField;
            }
            set {
                this.embarazoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Discapacidad {
            get {
                return this.discapacidadField;
            }
            set {
                this.discapacidadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string AtencionPreferenteCuidador {
            get {
                return this.atencionPreferenteCuidadorField;
            }
            set {
                this.atencionPreferenteCuidadorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public TypeRespuestaBase RespuestaBase {
            get {
                return this.respuestaBaseField;
            }
            set {
                this.respuestaBaseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void GenerarTicketCompletedEventHandler(object sender, GenerarTicketCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerarTicketCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GenerarTicketCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ParametroRespuestaGenerarTicket Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ParametroRespuestaGenerarTicket)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591