﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3053
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace cnx1
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="cnx1.ws_serviciosSoap")>  _
    Public Interface ws_serviciosSoap
        
        'CODEGEN: Generating message contract since element name ReiniciarServicioResult from namespace http://tempuri.org/ is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ReiniciarServicio", ReplyAction:="*")>  _
        Function ReiniciarServicio(ByVal request As cnx1.ReiniciarServicioRequest) As cnx1.ReiniciarServicioResponse
        
        'CODEGEN: Generating message contract since element name ReiniciarPCResult from namespace http://tempuri.org/ is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ReiniciarPC", ReplyAction:="*")>  _
        Function ReiniciarPC(ByVal request As cnx1.ReiniciarPCRequest) As cnx1.ReiniciarPCResponse
        
        'CODEGEN: Generating message contract since element name obtenerLogResult from namespace http://tempuri.org/ is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/obtenerLog", ReplyAction:="*")>  _
        Function obtenerLog(ByVal request As cnx1.obtenerLogRequest) As cnx1.obtenerLogResponse
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ReiniciarServicioRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ReiniciarServicio", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As cnx1.ReiniciarServicioRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As cnx1.ReiniciarServicioRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute()>  _
    Partial Public Class ReiniciarServicioRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ReiniciarServicioResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ReiniciarServicioResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As cnx1.ReiniciarServicioResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As cnx1.ReiniciarServicioResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class ReiniciarServicioResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public ReiniciarServicioResult As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal ReiniciarServicioResult As String)
            MyBase.New
            Me.ReiniciarServicioResult = ReiniciarServicioResult
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ReiniciarPCRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ReiniciarPC", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As cnx1.ReiniciarPCRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As cnx1.ReiniciarPCRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute()>  _
    Partial Public Class ReiniciarPCRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ReiniciarPCResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ReiniciarPCResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As cnx1.ReiniciarPCResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As cnx1.ReiniciarPCResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class ReiniciarPCResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public ReiniciarPCResult As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal ReiniciarPCResult As String)
            MyBase.New
            Me.ReiniciarPCResult = ReiniciarPCResult
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class obtenerLogRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="obtenerLog", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As cnx1.obtenerLogRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As cnx1.obtenerLogRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute()>  _
    Partial Public Class obtenerLogRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class obtenerLogResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="obtenerLogResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As cnx1.obtenerLogResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As cnx1.obtenerLogResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class obtenerLogResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public obtenerLogResult As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal obtenerLogResult As String)
            MyBase.New
            Me.obtenerLogResult = obtenerLogResult
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface ws_serviciosSoapChannel
        Inherits cnx1.ws_serviciosSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Partial Public Class ws_serviciosSoapClient
        Inherits System.ServiceModel.ClientBase(Of cnx1.ws_serviciosSoap)
        Implements cnx1.ws_serviciosSoap
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function cnx1_ws_serviciosSoap_ReiniciarServicio(ByVal request As cnx1.ReiniciarServicioRequest) As cnx1.ReiniciarServicioResponse Implements cnx1.ws_serviciosSoap.ReiniciarServicio
            Return MyBase.Channel.ReiniciarServicio(request)
        End Function
        
        Public Function ReiniciarServicio() As String
            Dim inValue As cnx1.ReiniciarServicioRequest = New cnx1.ReiniciarServicioRequest
            inValue.Body = New cnx1.ReiniciarServicioRequestBody
            Dim retVal As cnx1.ReiniciarServicioResponse = CType(Me,cnx1.ws_serviciosSoap).ReiniciarServicio(inValue)
            Return retVal.Body.ReiniciarServicioResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function cnx1_ws_serviciosSoap_ReiniciarPC(ByVal request As cnx1.ReiniciarPCRequest) As cnx1.ReiniciarPCResponse Implements cnx1.ws_serviciosSoap.ReiniciarPC
            Return MyBase.Channel.ReiniciarPC(request)
        End Function
        
        Public Function ReiniciarPC() As String
            Dim inValue As cnx1.ReiniciarPCRequest = New cnx1.ReiniciarPCRequest
            inValue.Body = New cnx1.ReiniciarPCRequestBody
            Dim retVal As cnx1.ReiniciarPCResponse = CType(Me,cnx1.ws_serviciosSoap).ReiniciarPC(inValue)
            Return retVal.Body.ReiniciarPCResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function cnx1_ws_serviciosSoap_obtenerLog(ByVal request As cnx1.obtenerLogRequest) As cnx1.obtenerLogResponse Implements cnx1.ws_serviciosSoap.obtenerLog
            Return MyBase.Channel.obtenerLog(request)
        End Function
        
        Public Function obtenerLog() As String
            Dim inValue As cnx1.obtenerLogRequest = New cnx1.obtenerLogRequest
            inValue.Body = New cnx1.obtenerLogRequestBody
            Dim retVal As cnx1.obtenerLogResponse = CType(Me,cnx1.ws_serviciosSoap).obtenerLog(inValue)
            Return retVal.Body.obtenerLogResult
        End Function
    End Class
End Namespace
