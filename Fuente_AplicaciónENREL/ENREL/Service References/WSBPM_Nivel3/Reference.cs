﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ENREL.WSBPM_Nivel3 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://xmlns.oracle.com/bpmn/bpmnProcess/ProcessNivel3", ConfigurationName="WSBPM_Nivel3.ProcessNivel3PortType")]
    public interface ProcessNivel3PortType {
        
        // CODEGEN: Generating message contract since element name Idproyecto from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="receiveTask")]
        void receiveTask(ENREL.WSBPM_Nivel3.receiveTask request);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="receiveTask")]
        System.Threading.Tasks.Task receiveTaskAsync(ENREL.WSBPM_Nivel3.receiveTask request);
        
        // CODEGEN: Generating message contract since element name Idproyecto from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="start")]
        void start(ENREL.WSBPM_Nivel3.start request);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="start")]
        System.Threading.Tasks.Task startAsync(ENREL.WSBPM_Nivel3.start request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class receiveTask {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="receiveTask", Namespace="http://xmlns.oracle.com/bpmn/bpmnProcess/ProcessNivel3", Order=0)]
        public ENREL.WSBPM_Nivel3.receiveTaskBody Body;
        
        public receiveTask() {
        }
        
        public receiveTask(ENREL.WSBPM_Nivel3.receiveTaskBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class receiveTaskBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Idproyecto;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Homoclave;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Estatus;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public int Prorroga;
        
        public receiveTaskBody() {
        }
        
        public receiveTaskBody(string Idproyecto, string Homoclave, string Estatus, int Prorroga) {
            this.Idproyecto = Idproyecto;
            this.Homoclave = Homoclave;
            this.Estatus = Estatus;
            this.Prorroga = Prorroga;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class start {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="start", Namespace="http://xmlns.oracle.com/bpmn/bpmnProcess/ProcessNivel3", Order=0)]
        public ENREL.WSBPM_Nivel3.startBody Body;
        
        public start() {
        }
        
        public start(ENREL.WSBPM_Nivel3.startBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class startBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Idproyecto;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Homoclave;
        
        public startBody() {
        }
        
        public startBody(string Idproyecto, string Homoclave) {
            this.Idproyecto = Idproyecto;
            this.Homoclave = Homoclave;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ProcessNivel3PortTypeChannel : ENREL.WSBPM_Nivel3.ProcessNivel3PortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProcessNivel3PortTypeClient : System.ServiceModel.ClientBase<ENREL.WSBPM_Nivel3.ProcessNivel3PortType>, ENREL.WSBPM_Nivel3.ProcessNivel3PortType {
        
        public ProcessNivel3PortTypeClient() {
        }
        
        public ProcessNivel3PortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProcessNivel3PortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessNivel3PortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessNivel3PortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void ENREL.WSBPM_Nivel3.ProcessNivel3PortType.receiveTask(ENREL.WSBPM_Nivel3.receiveTask request) {
            base.Channel.receiveTask(request);
        }
        
        public void receiveTask(string Idproyecto, string Homoclave, string Estatus, int Prorroga) {
            ENREL.WSBPM_Nivel3.receiveTask inValue = new ENREL.WSBPM_Nivel3.receiveTask();
            inValue.Body = new ENREL.WSBPM_Nivel3.receiveTaskBody();
            inValue.Body.Idproyecto = Idproyecto;
            inValue.Body.Homoclave = Homoclave;
            inValue.Body.Estatus = Estatus;
            inValue.Body.Prorroga = Prorroga;
            ((ENREL.WSBPM_Nivel3.ProcessNivel3PortType)(this)).receiveTask(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task ENREL.WSBPM_Nivel3.ProcessNivel3PortType.receiveTaskAsync(ENREL.WSBPM_Nivel3.receiveTask request) {
            return base.Channel.receiveTaskAsync(request);
        }
        
        public System.Threading.Tasks.Task receiveTaskAsync(string Idproyecto, string Homoclave, string Estatus, int Prorroga) {
            ENREL.WSBPM_Nivel3.receiveTask inValue = new ENREL.WSBPM_Nivel3.receiveTask();
            inValue.Body = new ENREL.WSBPM_Nivel3.receiveTaskBody();
            inValue.Body.Idproyecto = Idproyecto;
            inValue.Body.Homoclave = Homoclave;
            inValue.Body.Estatus = Estatus;
            inValue.Body.Prorroga = Prorroga;
            return ((ENREL.WSBPM_Nivel3.ProcessNivel3PortType)(this)).receiveTaskAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void ENREL.WSBPM_Nivel3.ProcessNivel3PortType.start(ENREL.WSBPM_Nivel3.start request) {
            base.Channel.start(request);
        }
        
        public void start(string Idproyecto, string Homoclave) {
            ENREL.WSBPM_Nivel3.start inValue = new ENREL.WSBPM_Nivel3.start();
            inValue.Body = new ENREL.WSBPM_Nivel3.startBody();
            inValue.Body.Idproyecto = Idproyecto;
            inValue.Body.Homoclave = Homoclave;
            ((ENREL.WSBPM_Nivel3.ProcessNivel3PortType)(this)).start(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task ENREL.WSBPM_Nivel3.ProcessNivel3PortType.startAsync(ENREL.WSBPM_Nivel3.start request) {
            return base.Channel.startAsync(request);
        }
        
        public System.Threading.Tasks.Task startAsync(string Idproyecto, string Homoclave) {
            ENREL.WSBPM_Nivel3.start inValue = new ENREL.WSBPM_Nivel3.start();
            inValue.Body = new ENREL.WSBPM_Nivel3.startBody();
            inValue.Body.Idproyecto = Idproyecto;
            inValue.Body.Homoclave = Homoclave;
            return ((ENREL.WSBPM_Nivel3.ProcessNivel3PortType)(this)).startAsync(inValue);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://xmlns.oracle.com/bpmn/bpmnProcess/ProcessNivel3", ConfigurationName="WSBPM_Nivel3.ProcessNivel3PortTypeCallBack")]
    public interface ProcessNivel3PortTypeCallBack {
        
        // CODEGEN: Generating message contract since the wrapper name (endResponse) of message end does not match the default value (end)
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="end")]
        void end(ENREL.WSBPM_Nivel3.end request);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="end")]
        System.Threading.Tasks.Task endAsync(ENREL.WSBPM_Nivel3.end request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="endResponse", WrapperNamespace="http://xmlns.oracle.com/bpmn/bpmnProcess/ProcessNivel3", IsWrapped=true)]
    public partial class end {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string Idproyecto;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=1)]
        public string Homoclave;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=2)]
        public string Estatus;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=3)]
        public int Prorroga;
        
        public end() {
        }
        
        public end(string Idproyecto, string Homoclave, string Estatus, int Prorroga) {
            this.Idproyecto = Idproyecto;
            this.Homoclave = Homoclave;
            this.Estatus = Estatus;
            this.Prorroga = Prorroga;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ProcessNivel3PortTypeCallBackChannel : ENREL.WSBPM_Nivel3.ProcessNivel3PortTypeCallBack, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProcessNivel3PortTypeCallBackClient : System.ServiceModel.ClientBase<ENREL.WSBPM_Nivel3.ProcessNivel3PortTypeCallBack>, ENREL.WSBPM_Nivel3.ProcessNivel3PortTypeCallBack {
        
        public ProcessNivel3PortTypeCallBackClient() {
        }
        
        public ProcessNivel3PortTypeCallBackClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProcessNivel3PortTypeCallBackClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessNivel3PortTypeCallBackClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessNivel3PortTypeCallBackClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void ENREL.WSBPM_Nivel3.ProcessNivel3PortTypeCallBack.end(ENREL.WSBPM_Nivel3.end request) {
            base.Channel.end(request);
        }
        
        public void end(string Idproyecto, string Homoclave, string Estatus, int Prorroga) {
            ENREL.WSBPM_Nivel3.end inValue = new ENREL.WSBPM_Nivel3.end();
            inValue.Idproyecto = Idproyecto;
            inValue.Homoclave = Homoclave;
            inValue.Estatus = Estatus;
            inValue.Prorroga = Prorroga;
            ((ENREL.WSBPM_Nivel3.ProcessNivel3PortTypeCallBack)(this)).end(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task ENREL.WSBPM_Nivel3.ProcessNivel3PortTypeCallBack.endAsync(ENREL.WSBPM_Nivel3.end request) {
            return base.Channel.endAsync(request);
        }
        
        public System.Threading.Tasks.Task endAsync(string Idproyecto, string Homoclave, string Estatus, int Prorroga) {
            ENREL.WSBPM_Nivel3.end inValue = new ENREL.WSBPM_Nivel3.end();
            inValue.Idproyecto = Idproyecto;
            inValue.Homoclave = Homoclave;
            inValue.Estatus = Estatus;
            inValue.Prorroga = Prorroga;
            return ((ENREL.WSBPM_Nivel3.ProcessNivel3PortTypeCallBack)(this)).endAsync(inValue);
        }
    }
}
