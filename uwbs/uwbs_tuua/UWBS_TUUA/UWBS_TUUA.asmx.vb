Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports LogicaNegocio
Imports System.Xml
Imports System.IO
Imports System.Net

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class UWBS_TUUA
    Inherits System.Web.Services.WebService

    Dim objGestorXML As New GestorXML

    <WebMethod()> _
    Public Function VueloProgramado() As XmlDocument
        Return objGestorXML.VueloProgramado
    End Function

    <WebMethod()> _
    Public Function VueloTemporada() As XmlDocument
        Return objGestorXML.VueloTemporada
    End Function

    <WebMethod()> _
    Public Function Compania() As XmlDocument
        Return objGestorXML.Compania
    End Function

    <WebMethod()> _
    Public Function TasaCambio() As XmlDocument
        Dim objCredential As New System.Net.NetworkCredential()
        objCredential.UserName = My.Settings.Usuario_WSInterbank
        objCredential.Password = My.Settings.Password_WSInterbank
        Dim ws As New WSInterbank.wsTipoCambioLAP
        ws.Credentials = objCredential
        Dim cad_xml_interbank As String = ws.fCargaTipoDeCambioTodos
        Return objGestorXML.TasaCambio(cad_xml_interbank)
    End Function

    <WebMethod()> _
    Public Function VueloProgramadoFTP() As XmlDocument
        'Obteniendo FTP
        Dim request As FtpWebRequest = DirectCast(WebRequest.Create(My.Settings.url_ftp), FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.DownloadFile
        request.Credentials = New NetworkCredential(My.Settings.usuario_ftp, My.Settings.clave_ftp)
        Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
        Dim responseStream As Stream = response.GetResponseStream()
        Dim reader As New StreamReader(responseStream)
        'Fin de FTP
        Dim lineas As New List(Of String)
        Using sr As StreamReader = reader
            Dim linea As String = ""
            Do
                linea = sr.ReadLine()
                lineas.Add(linea)
            Loop Until linea Is Nothing
            lineas.RemoveAt(lineas.Count - 1)
            sr.Close()
        End Using
        reader.Close()
        response.Close()
        Return objGestorXML.VueloProgramadoFTP(lineas)
    End Function

End Class