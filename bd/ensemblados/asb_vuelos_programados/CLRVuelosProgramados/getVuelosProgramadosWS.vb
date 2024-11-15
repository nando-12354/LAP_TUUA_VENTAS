Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.SqlServer.Server

Partial Public Class UserDefinedFunctions
    <Microsoft.SqlServer.Server.SqlFunction()> _
    Public Shared Function getVuelosProgramadosWS(ByVal url_webservice As SqlString) As <SqlFacet(MaxSize:=-1)> SqlString
        Dim ws As New UWBS_TUUA.UWBS_TUUA
        ws.Url = url_webservice.ToString()
        Dim sql_str_result As New SqlString(ws.VueloProgramadoFTP.InnerXml.ToString)
        Return sql_str_result
    End Function
End Class
