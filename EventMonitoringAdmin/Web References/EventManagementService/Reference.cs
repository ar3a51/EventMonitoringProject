﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.225.
// 
#pragma warning disable 1591

namespace EventMonitoringAdmin.EventManagementService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="EventSoap", Namespace="http://ICAA.EventMonitoringService.org/")]
    public partial class Event : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback testConnectionOperationCompleted;
        
        private System.Threading.SendOrPostCallback logEventOperationCompleted;
        
        private System.Threading.SendOrPostCallback getLogInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback addNewMachineOperationCompleted;
        
        private System.Threading.SendOrPostCallback removeMachineOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Event() {
            this.Url = global::EventMonitoringAdmin.Properties.Settings.Default.EventMonitoringAdmin_EventManagementService_Event;
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
        public event testConnectionCompletedEventHandler testConnectionCompleted;
        
        /// <remarks/>
        public event logEventCompletedEventHandler logEventCompleted;
        
        /// <remarks/>
        public event getLogInfoCompletedEventHandler getLogInfoCompleted;
        
        /// <remarks/>
        public event addNewMachineCompletedEventHandler addNewMachineCompleted;
        
        /// <remarks/>
        public event removeMachineCompletedEventHandler removeMachineCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ICAA.EventMonitoringService.org/testConnection", RequestNamespace="http://ICAA.EventMonitoringService.org/", ResponseNamespace="http://ICAA.EventMonitoringService.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string testConnection() {
            object[] results = this.Invoke("testConnection", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void testConnectionAsync() {
            this.testConnectionAsync(null);
        }
        
        /// <remarks/>
        public void testConnectionAsync(object userState) {
            if ((this.testConnectionOperationCompleted == null)) {
                this.testConnectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OntestConnectionOperationCompleted);
            }
            this.InvokeAsync("testConnection", new object[0], this.testConnectionOperationCompleted, userState);
        }
        
        private void OntestConnectionOperationCompleted(object arg) {
            if ((this.testConnectionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.testConnectionCompleted(this, new testConnectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ICAA.EventMonitoringService.org/logEvent", RequestNamespace="http://ICAA.EventMonitoringService.org/", ResponseNamespace="http://ICAA.EventMonitoringService.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ResultSet logEvent(long eventId, string strEventType, string strEventMessage, string strMachineName, string strUsername, string strSource, string dtDateTime) {
            object[] results = this.Invoke("logEvent", new object[] {
                        eventId,
                        strEventType,
                        strEventMessage,
                        strMachineName,
                        strUsername,
                        strSource,
                        dtDateTime});
            return ((ResultSet)(results[0]));
        }
        
        /// <remarks/>
        public void logEventAsync(long eventId, string strEventType, string strEventMessage, string strMachineName, string strUsername, string strSource, string dtDateTime) {
            this.logEventAsync(eventId, strEventType, strEventMessage, strMachineName, strUsername, strSource, dtDateTime, null);
        }
        
        /// <remarks/>
        public void logEventAsync(long eventId, string strEventType, string strEventMessage, string strMachineName, string strUsername, string strSource, string dtDateTime, object userState) {
            if ((this.logEventOperationCompleted == null)) {
                this.logEventOperationCompleted = new System.Threading.SendOrPostCallback(this.OnlogEventOperationCompleted);
            }
            this.InvokeAsync("logEvent", new object[] {
                        eventId,
                        strEventType,
                        strEventMessage,
                        strMachineName,
                        strUsername,
                        strSource,
                        dtDateTime}, this.logEventOperationCompleted, userState);
        }
        
        private void OnlogEventOperationCompleted(object arg) {
            if ((this.logEventCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.logEventCompleted(this, new logEventCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ICAA.EventMonitoringService.org/getLogInfo", RequestNamespace="http://ICAA.EventMonitoringService.org/", ResponseNamespace="http://ICAA.EventMonitoringService.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ResultLog getLogInfo(string machineName) {
            object[] results = this.Invoke("getLogInfo", new object[] {
                        machineName});
            return ((ResultLog)(results[0]));
        }
        
        /// <remarks/>
        public void getLogInfoAsync(string machineName) {
            this.getLogInfoAsync(machineName, null);
        }
        
        /// <remarks/>
        public void getLogInfoAsync(string machineName, object userState) {
            if ((this.getLogInfoOperationCompleted == null)) {
                this.getLogInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetLogInfoOperationCompleted);
            }
            this.InvokeAsync("getLogInfo", new object[] {
                        machineName}, this.getLogInfoOperationCompleted, userState);
        }
        
        private void OngetLogInfoOperationCompleted(object arg) {
            if ((this.getLogInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getLogInfoCompleted(this, new getLogInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ICAA.EventMonitoringService.org/addNewMachine", RequestNamespace="http://ICAA.EventMonitoringService.org/", ResponseNamespace="http://ICAA.EventMonitoringService.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ResultSet addNewMachine(string type, string machineName, string environment) {
            object[] results = this.Invoke("addNewMachine", new object[] {
                        type,
                        machineName,
                        environment});
            return ((ResultSet)(results[0]));
        }
        
        /// <remarks/>
        public void addNewMachineAsync(string type, string machineName, string environment) {
            this.addNewMachineAsync(type, machineName, environment, null);
        }
        
        /// <remarks/>
        public void addNewMachineAsync(string type, string machineName, string environment, object userState) {
            if ((this.addNewMachineOperationCompleted == null)) {
                this.addNewMachineOperationCompleted = new System.Threading.SendOrPostCallback(this.OnaddNewMachineOperationCompleted);
            }
            this.InvokeAsync("addNewMachine", new object[] {
                        type,
                        machineName,
                        environment}, this.addNewMachineOperationCompleted, userState);
        }
        
        private void OnaddNewMachineOperationCompleted(object arg) {
            if ((this.addNewMachineCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.addNewMachineCompleted(this, new addNewMachineCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ICAA.EventMonitoringService.org/removeMachine", RequestNamespace="http://ICAA.EventMonitoringService.org/", ResponseNamespace="http://ICAA.EventMonitoringService.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ResultSet removeMachine(string machineName) {
            object[] results = this.Invoke("removeMachine", new object[] {
                        machineName});
            return ((ResultSet)(results[0]));
        }
        
        /// <remarks/>
        public void removeMachineAsync(string machineName) {
            this.removeMachineAsync(machineName, null);
        }
        
        /// <remarks/>
        public void removeMachineAsync(string machineName, object userState) {
            if ((this.removeMachineOperationCompleted == null)) {
                this.removeMachineOperationCompleted = new System.Threading.SendOrPostCallback(this.OnremoveMachineOperationCompleted);
            }
            this.InvokeAsync("removeMachine", new object[] {
                        machineName}, this.removeMachineOperationCompleted, userState);
        }
        
        private void OnremoveMachineOperationCompleted(object arg) {
            if ((this.removeMachineCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.removeMachineCompleted(this, new removeMachineCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResultLog))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ICAA.EventMonitoringService.org/")]
    public partial class ResultSet {
        
        private string stringResultTypeField;
        
        private string stringResultMessageField;
        
        private string stringResultDataField;
        
        /// <remarks/>
        public string stringResultType {
            get {
                return this.stringResultTypeField;
            }
            set {
                this.stringResultTypeField = value;
            }
        }
        
        /// <remarks/>
        public string stringResultMessage {
            get {
                return this.stringResultMessageField;
            }
            set {
                this.stringResultMessageField = value;
            }
        }
        
        /// <remarks/>
        public string stringResultData {
            get {
                return this.stringResultDataField;
            }
            set {
                this.stringResultDataField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ICAA.EventMonitoringService.org/")]
    public partial class ResultLog : ResultSet {
        
        private System.Data.DataTable logListField;
        
        private string numberOfRecordsField;
        
        /// <remarks/>
        public System.Data.DataTable logList {
            get {
                return this.logListField;
            }
            set {
                this.logListField = value;
            }
        }
        
        /// <remarks/>
        public string numberOfRecords {
            get {
                return this.numberOfRecordsField;
            }
            set {
                this.numberOfRecordsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void testConnectionCompletedEventHandler(object sender, testConnectionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class testConnectionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal testConnectionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void logEventCompletedEventHandler(object sender, logEventCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class logEventCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal logEventCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ResultSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getLogInfoCompletedEventHandler(object sender, getLogInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getLogInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getLogInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ResultLog Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultLog)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void addNewMachineCompletedEventHandler(object sender, addNewMachineCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class addNewMachineCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal addNewMachineCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ResultSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void removeMachineCompletedEventHandler(object sender, removeMachineCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class removeMachineCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal removeMachineCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ResultSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultSet)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591