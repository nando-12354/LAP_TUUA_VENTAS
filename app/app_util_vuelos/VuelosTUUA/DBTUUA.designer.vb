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

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection


<System.Data.Linq.Mapping.DatabaseAttribute(Name:="BD_TUUA_PRD")>  _
Partial Public Class DBTUUADataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  Partial Private Sub InsertTUA_Compania(instance As TUA_Compania)
    End Sub
  Partial Private Sub UpdateTUA_Compania(instance As TUA_Compania)
    End Sub
  Partial Private Sub DeleteTUA_Compania(instance As TUA_Compania)
    End Sub
  Partial Private Sub InsertTUA_VueloProgramado(instance As TUA_VueloProgramado)
    End Sub
  Partial Private Sub UpdateTUA_VueloProgramado(instance As TUA_VueloProgramado)
    End Sub
  Partial Private Sub DeleteTUA_VueloProgramado(instance As TUA_VueloProgramado)
    End Sub
  #End Region
	
	Public Sub New()
		MyBase.New(Global.VuelosTUUA.My.MySettings.Default.BD_TUUA_PRDConnectionString, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public ReadOnly Property TUA_Companias() As System.Data.Linq.Table(Of TUA_Compania)
		Get
			Return Me.GetTable(Of TUA_Compania)
		End Get
	End Property
	
	Public ReadOnly Property TUA_VueloProgramados() As System.Data.Linq.Table(Of TUA_VueloProgramado)
		Get
			Return Me.GetTable(Of TUA_VueloProgramado)
		End Get
	End Property
End Class

<Table(Name:="dbo.TUA_Compania")>  _
Partial Public Class TUA_Compania
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _Cod_Compania As String
	
	Private _Tip_Compania As System.Nullable(Of Char)
	
	Private _Dsc_Compania As String
	
	Private _Fch_Creacion As System.Nullable(Of Date)
	
	Private _Cod_Especial_Compania As String
	
	Private _Cod_Aerolinea As String
	
	Private _Cod_SAP As String
	
	Private _Cod_OACI As String
	
	Private _Cod_IATA As String
	
	Private _Dsc_Ruc As String
	
	Private _Tip_Estado As System.Nullable(Of Char)
	
	Private _Log_Usuario_Mod As String
	
	Private _Log_Fecha_Mod As String
	
	Private _Log_Hora_Mod As String
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnCod_CompaniaChanging(value As String)
    End Sub
    Partial Private Sub OnCod_CompaniaChanged()
    End Sub
    Partial Private Sub OnTip_CompaniaChanging(value As System.Nullable(Of Char))
    End Sub
    Partial Private Sub OnTip_CompaniaChanged()
    End Sub
    Partial Private Sub OnDsc_CompaniaChanging(value As String)
    End Sub
    Partial Private Sub OnDsc_CompaniaChanged()
    End Sub
    Partial Private Sub OnFch_CreacionChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnFch_CreacionChanged()
    End Sub
    Partial Private Sub OnCod_Especial_CompaniaChanging(value As String)
    End Sub
    Partial Private Sub OnCod_Especial_CompaniaChanged()
    End Sub
    Partial Private Sub OnCod_AerolineaChanging(value As String)
    End Sub
    Partial Private Sub OnCod_AerolineaChanged()
    End Sub
    Partial Private Sub OnCod_SAPChanging(value As String)
    End Sub
    Partial Private Sub OnCod_SAPChanged()
    End Sub
    Partial Private Sub OnCod_OACIChanging(value As String)
    End Sub
    Partial Private Sub OnCod_OACIChanged()
    End Sub
    Partial Private Sub OnCod_IATAChanging(value As String)
    End Sub
    Partial Private Sub OnCod_IATAChanged()
    End Sub
    Partial Private Sub OnDsc_RucChanging(value As String)
    End Sub
    Partial Private Sub OnDsc_RucChanged()
    End Sub
    Partial Private Sub OnTip_EstadoChanging(value As System.Nullable(Of Char))
    End Sub
    Partial Private Sub OnTip_EstadoChanged()
    End Sub
    Partial Private Sub OnLog_Usuario_ModChanging(value As String)
    End Sub
    Partial Private Sub OnLog_Usuario_ModChanged()
    End Sub
    Partial Private Sub OnLog_Fecha_ModChanging(value As String)
    End Sub
    Partial Private Sub OnLog_Fecha_ModChanged()
    End Sub
    Partial Private Sub OnLog_Hora_ModChanging(value As String)
    End Sub
    Partial Private Sub OnLog_Hora_ModChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		OnCreated
	End Sub
	
	<Column(Storage:="_Cod_Compania", DbType:="Char(10) NOT NULL", CanBeNull:=false, IsPrimaryKey:=true)>  _
	Public Property Cod_Compania() As String
		Get
			Return Me._Cod_Compania
		End Get
		Set
			If (String.Equals(Me._Cod_Compania, value) = false) Then
				Me.OnCod_CompaniaChanging(value)
				Me.SendPropertyChanging
				Me._Cod_Compania = value
				Me.SendPropertyChanged("Cod_Compania")
				Me.OnCod_CompaniaChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Tip_Compania", DbType:="Char(1)")>  _
	Public Property Tip_Compania() As System.Nullable(Of Char)
		Get
			Return Me._Tip_Compania
		End Get
		Set
			If (Me._Tip_Compania.Equals(value) = false) Then
				Me.OnTip_CompaniaChanging(value)
				Me.SendPropertyChanging
				Me._Tip_Compania = value
				Me.SendPropertyChanged("Tip_Compania")
				Me.OnTip_CompaniaChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Dsc_Compania", DbType:="VarChar(50)")>  _
	Public Property Dsc_Compania() As String
		Get
			Return Me._Dsc_Compania
		End Get
		Set
			If (String.Equals(Me._Dsc_Compania, value) = false) Then
				Me.OnDsc_CompaniaChanging(value)
				Me.SendPropertyChanging
				Me._Dsc_Compania = value
				Me.SendPropertyChanged("Dsc_Compania")
				Me.OnDsc_CompaniaChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Fch_Creacion", DbType:="DateTime")>  _
	Public Property Fch_Creacion() As System.Nullable(Of Date)
		Get
			Return Me._Fch_Creacion
		End Get
		Set
			If (Me._Fch_Creacion.Equals(value) = false) Then
				Me.OnFch_CreacionChanging(value)
				Me.SendPropertyChanging
				Me._Fch_Creacion = value
				Me.SendPropertyChanged("Fch_Creacion")
				Me.OnFch_CreacionChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Cod_Especial_Compania", DbType:="Char(3)")>  _
	Public Property Cod_Especial_Compania() As String
		Get
			Return Me._Cod_Especial_Compania
		End Get
		Set
			If (String.Equals(Me._Cod_Especial_Compania, value) = false) Then
				Me.OnCod_Especial_CompaniaChanging(value)
				Me.SendPropertyChanging
				Me._Cod_Especial_Compania = value
				Me.SendPropertyChanged("Cod_Especial_Compania")
				Me.OnCod_Especial_CompaniaChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Cod_Aerolinea", DbType:="Char(10)")>  _
	Public Property Cod_Aerolinea() As String
		Get
			Return Me._Cod_Aerolinea
		End Get
		Set
			If (String.Equals(Me._Cod_Aerolinea, value) = false) Then
				Me.OnCod_AerolineaChanging(value)
				Me.SendPropertyChanging
				Me._Cod_Aerolinea = value
				Me.SendPropertyChanged("Cod_Aerolinea")
				Me.OnCod_AerolineaChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Cod_SAP", DbType:="Char(16)")>  _
	Public Property Cod_SAP() As String
		Get
			Return Me._Cod_SAP
		End Get
		Set
			If (String.Equals(Me._Cod_SAP, value) = false) Then
				Me.OnCod_SAPChanging(value)
				Me.SendPropertyChanging
				Me._Cod_SAP = value
				Me.SendPropertyChanged("Cod_SAP")
				Me.OnCod_SAPChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Cod_OACI", DbType:="VarChar(5)")>  _
	Public Property Cod_OACI() As String
		Get
			Return Me._Cod_OACI
		End Get
		Set
			If (String.Equals(Me._Cod_OACI, value) = false) Then
				Me.OnCod_OACIChanging(value)
				Me.SendPropertyChanging
				Me._Cod_OACI = value
				Me.SendPropertyChanged("Cod_OACI")
				Me.OnCod_OACIChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Cod_IATA", DbType:="VarChar(5)")>  _
	Public Property Cod_IATA() As String
		Get
			Return Me._Cod_IATA
		End Get
		Set
			If (String.Equals(Me._Cod_IATA, value) = false) Then
				Me.OnCod_IATAChanging(value)
				Me.SendPropertyChanging
				Me._Cod_IATA = value
				Me.SendPropertyChanged("Cod_IATA")
				Me.OnCod_IATAChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Dsc_Ruc", DbType:="Char(11)")>  _
	Public Property Dsc_Ruc() As String
		Get
			Return Me._Dsc_Ruc
		End Get
		Set
			If (String.Equals(Me._Dsc_Ruc, value) = false) Then
				Me.OnDsc_RucChanging(value)
				Me.SendPropertyChanging
				Me._Dsc_Ruc = value
				Me.SendPropertyChanged("Dsc_Ruc")
				Me.OnDsc_RucChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Tip_Estado", DbType:="Char(1)")>  _
	Public Property Tip_Estado() As System.Nullable(Of Char)
		Get
			Return Me._Tip_Estado
		End Get
		Set
			If (Me._Tip_Estado.Equals(value) = false) Then
				Me.OnTip_EstadoChanging(value)
				Me.SendPropertyChanging
				Me._Tip_Estado = value
				Me.SendPropertyChanged("Tip_Estado")
				Me.OnTip_EstadoChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Log_Usuario_Mod", DbType:="Char(7)")>  _
	Public Property Log_Usuario_Mod() As String
		Get
			Return Me._Log_Usuario_Mod
		End Get
		Set
			If (String.Equals(Me._Log_Usuario_Mod, value) = false) Then
				Me.OnLog_Usuario_ModChanging(value)
				Me.SendPropertyChanging
				Me._Log_Usuario_Mod = value
				Me.SendPropertyChanged("Log_Usuario_Mod")
				Me.OnLog_Usuario_ModChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Log_Fecha_Mod", DbType:="Char(8) NOT NULL", CanBeNull:=false)>  _
	Public Property Log_Fecha_Mod() As String
		Get
			Return Me._Log_Fecha_Mod
		End Get
		Set
			If (String.Equals(Me._Log_Fecha_Mod, value) = false) Then
				Me.OnLog_Fecha_ModChanging(value)
				Me.SendPropertyChanging
				Me._Log_Fecha_Mod = value
				Me.SendPropertyChanged("Log_Fecha_Mod")
				Me.OnLog_Fecha_ModChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Log_Hora_Mod", DbType:="Char(6) NOT NULL", CanBeNull:=false)>  _
	Public Property Log_Hora_Mod() As String
		Get
			Return Me._Log_Hora_Mod
		End Get
		Set
			If (String.Equals(Me._Log_Hora_Mod, value) = false) Then
				Me.OnLog_Hora_ModChanging(value)
				Me.SendPropertyChanging
				Me._Log_Hora_Mod = value
				Me.SendPropertyChanged("Log_Hora_Mod")
				Me.OnLog_Hora_ModChanged
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class

<Table(Name:="dbo.TUA_VueloProgramado")>  _
Partial Public Class TUA_VueloProgramado
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _Cod_Compania As String
	
	Private _Num_Vuelo As String
	
	Private _Fch_Vuelo As String
	
	Private _Hor_Vuelo As String
	
	Private _Dsc_Vuelo As String
	
	Private _Tip_Vuelo As Char
	
	Private _Tip_Estado As Char
	
	Private _Dsc_Destino As String
	
	Private _Log_Usuario_Mod As String
	
	Private _Log_Fecha_Mod As String
	
	Private _Log_Hora_Mod As String
	
	Private _Flg_Programado As System.Nullable(Of Char)
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnCod_CompaniaChanging(value As String)
    End Sub
    Partial Private Sub OnCod_CompaniaChanged()
    End Sub
    Partial Private Sub OnNum_VueloChanging(value As String)
    End Sub
    Partial Private Sub OnNum_VueloChanged()
    End Sub
    Partial Private Sub OnFch_VueloChanging(value As String)
    End Sub
    Partial Private Sub OnFch_VueloChanged()
    End Sub
    Partial Private Sub OnHor_VueloChanging(value As String)
    End Sub
    Partial Private Sub OnHor_VueloChanged()
    End Sub
    Partial Private Sub OnDsc_VueloChanging(value As String)
    End Sub
    Partial Private Sub OnDsc_VueloChanged()
    End Sub
    Partial Private Sub OnTip_VueloChanging(value As Char)
    End Sub
    Partial Private Sub OnTip_VueloChanged()
    End Sub
    Partial Private Sub OnTip_EstadoChanging(value As Char)
    End Sub
    Partial Private Sub OnTip_EstadoChanged()
    End Sub
    Partial Private Sub OnDsc_DestinoChanging(value As String)
    End Sub
    Partial Private Sub OnDsc_DestinoChanged()
    End Sub
    Partial Private Sub OnLog_Usuario_ModChanging(value As String)
    End Sub
    Partial Private Sub OnLog_Usuario_ModChanged()
    End Sub
    Partial Private Sub OnLog_Fecha_ModChanging(value As String)
    End Sub
    Partial Private Sub OnLog_Fecha_ModChanged()
    End Sub
    Partial Private Sub OnLog_Hora_ModChanging(value As String)
    End Sub
    Partial Private Sub OnLog_Hora_ModChanged()
    End Sub
    Partial Private Sub OnFlg_ProgramadoChanging(value As System.Nullable(Of Char))
    End Sub
    Partial Private Sub OnFlg_ProgramadoChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		OnCreated
	End Sub
	
	<Column(Storage:="_Cod_Compania", DbType:="Char(10) NOT NULL", CanBeNull:=false, IsPrimaryKey:=true)>  _
	Public Property Cod_Compania() As String
		Get
			Return Me._Cod_Compania
		End Get
		Set
			If (String.Equals(Me._Cod_Compania, value) = false) Then
				Me.OnCod_CompaniaChanging(value)
				Me.SendPropertyChanging
				Me._Cod_Compania = value
				Me.SendPropertyChanged("Cod_Compania")
				Me.OnCod_CompaniaChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Num_Vuelo", DbType:="Char(10) NOT NULL", CanBeNull:=false, IsPrimaryKey:=true)>  _
	Public Property Num_Vuelo() As String
		Get
			Return Me._Num_Vuelo
		End Get
		Set
			If (String.Equals(Me._Num_Vuelo, value) = false) Then
				Me.OnNum_VueloChanging(value)
				Me.SendPropertyChanging
				Me._Num_Vuelo = value
				Me.SendPropertyChanged("Num_Vuelo")
				Me.OnNum_VueloChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Fch_Vuelo", DbType:="Char(8) NOT NULL", CanBeNull:=false, IsPrimaryKey:=true)>  _
	Public Property Fch_Vuelo() As String
		Get
			Return Me._Fch_Vuelo
		End Get
		Set
			If (String.Equals(Me._Fch_Vuelo, value) = false) Then
				Me.OnFch_VueloChanging(value)
				Me.SendPropertyChanging
				Me._Fch_Vuelo = value
				Me.SendPropertyChanged("Fch_Vuelo")
				Me.OnFch_VueloChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Hor_Vuelo", DbType:="Char(6) NOT NULL", CanBeNull:=false, IsPrimaryKey:=true)>  _
	Public Property Hor_Vuelo() As String
		Get
			Return Me._Hor_Vuelo
		End Get
		Set
			If (String.Equals(Me._Hor_Vuelo, value) = false) Then
				Me.OnHor_VueloChanging(value)
				Me.SendPropertyChanging
				Me._Hor_Vuelo = value
				Me.SendPropertyChanged("Hor_Vuelo")
				Me.OnHor_VueloChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Dsc_Vuelo", DbType:="VarChar(50)")>  _
	Public Property Dsc_Vuelo() As String
		Get
			Return Me._Dsc_Vuelo
		End Get
		Set
			If (String.Equals(Me._Dsc_Vuelo, value) = false) Then
				Me.OnDsc_VueloChanging(value)
				Me.SendPropertyChanging
				Me._Dsc_Vuelo = value
				Me.SendPropertyChanged("Dsc_Vuelo")
				Me.OnDsc_VueloChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Tip_Vuelo", DbType:="Char(1) NOT NULL")>  _
	Public Property Tip_Vuelo() As Char
		Get
			Return Me._Tip_Vuelo
		End Get
		Set
			If ((Me._Tip_Vuelo = value)  _
						= false) Then
				Me.OnTip_VueloChanging(value)
				Me.SendPropertyChanging
				Me._Tip_Vuelo = value
				Me.SendPropertyChanged("Tip_Vuelo")
				Me.OnTip_VueloChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Tip_Estado", DbType:="Char(1) NOT NULL")>  _
	Public Property Tip_Estado() As Char
		Get
			Return Me._Tip_Estado
		End Get
		Set
			If ((Me._Tip_Estado = value)  _
						= false) Then
				Me.OnTip_EstadoChanging(value)
				Me.SendPropertyChanging
				Me._Tip_Estado = value
				Me.SendPropertyChanged("Tip_Estado")
				Me.OnTip_EstadoChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Dsc_Destino", DbType:="VarChar(60) NOT NULL", CanBeNull:=false)>  _
	Public Property Dsc_Destino() As String
		Get
			Return Me._Dsc_Destino
		End Get
		Set
			If (String.Equals(Me._Dsc_Destino, value) = false) Then
				Me.OnDsc_DestinoChanging(value)
				Me.SendPropertyChanging
				Me._Dsc_Destino = value
				Me.SendPropertyChanged("Dsc_Destino")
				Me.OnDsc_DestinoChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Log_Usuario_Mod", DbType:="Char(7)")>  _
	Public Property Log_Usuario_Mod() As String
		Get
			Return Me._Log_Usuario_Mod
		End Get
		Set
			If (String.Equals(Me._Log_Usuario_Mod, value) = false) Then
				Me.OnLog_Usuario_ModChanging(value)
				Me.SendPropertyChanging
				Me._Log_Usuario_Mod = value
				Me.SendPropertyChanged("Log_Usuario_Mod")
				Me.OnLog_Usuario_ModChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Log_Fecha_Mod", DbType:="Char(8)")>  _
	Public Property Log_Fecha_Mod() As String
		Get
			Return Me._Log_Fecha_Mod
		End Get
		Set
			If (String.Equals(Me._Log_Fecha_Mod, value) = false) Then
				Me.OnLog_Fecha_ModChanging(value)
				Me.SendPropertyChanging
				Me._Log_Fecha_Mod = value
				Me.SendPropertyChanged("Log_Fecha_Mod")
				Me.OnLog_Fecha_ModChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Log_Hora_Mod", DbType:="Char(6)")>  _
	Public Property Log_Hora_Mod() As String
		Get
			Return Me._Log_Hora_Mod
		End Get
		Set
			If (String.Equals(Me._Log_Hora_Mod, value) = false) Then
				Me.OnLog_Hora_ModChanging(value)
				Me.SendPropertyChanging
				Me._Log_Hora_Mod = value
				Me.SendPropertyChanged("Log_Hora_Mod")
				Me.OnLog_Hora_ModChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Flg_Programado", DbType:="Char(1)")>  _
	Public Property Flg_Programado() As System.Nullable(Of Char)
		Get
			Return Me._Flg_Programado
		End Get
		Set
			If (Me._Flg_Programado.Equals(value) = false) Then
				Me.OnFlg_ProgramadoChanging(value)
				Me.SendPropertyChanging
				Me._Flg_Programado = value
				Me.SendPropertyChanged("Flg_Programado")
				Me.OnFlg_ProgramadoChanged
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class
