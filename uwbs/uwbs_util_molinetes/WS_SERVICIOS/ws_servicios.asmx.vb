Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.IO

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la siguiente línea.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ws_servicios
    Inherits System.Web.Services.WebService

    Dim ruta_log As String = System.AppDomain.CurrentDomain.BaseDirectory() & "log.txt"
    Dim ruta_log_stop As String = System.AppDomain.CurrentDomain.BaseDirectory() & "log_stop.txt"
    Dim ruta_log_start As String = System.AppDomain.CurrentDomain.BaseDirectory() & "log_start.txt"
    Dim salto_linea As String = "°"

    <WebMethod()> _
    Public Function ReiniciarServicio() As String
        Dim separador = "Echo ==================== Reiniciando Servicio========================= >> " & ruta_log & vbNewLine
        Dim cmdfecha As String = "Date /T" & ">>" & ruta_log & vbNewLine
        Dim cmdhora As String = "Time /T" & ">>" & ruta_log & vbNewLine
        Dim comando1 As String = "NET STOP " & My.Settings.p_nombreservicio & " >> " & ruta_log & vbNewLine
        Dim comando2 As String = "NET START " & My.Settings.p_nombreservicio & " >> " & ruta_log & vbNewLine
        Dim comando As String = separador & cmdfecha & cmdhora & comando1 & comando2
        crearArchivo(comando, "reinicio.bat")
        Dim archivo_bat As String = System.AppDomain.CurrentDomain.BaseDirectory() & "reinicio.bat"
        System.Diagnostics.Process.Start(archivo_bat)
        Return "Termino"
    End Function

    <WebMethod()> _
    Public Function ReiniciarPC() As String
        Dim separador = "Echo ======================== Reiniciando PC ========================= >> " & ruta_log & vbNewLine
        Dim cmdfecha As String = "Date /T" & ">>" & ruta_log & vbNewLine
        Dim cmdhora As String = "Time /T" & ">>" & ruta_log & vbNewLine
        Dim comando1 As String = "shutdown -r " & " & ruta_log & vbNewLine"
        Dim comando As String = separador & cmdfecha & cmdhora & comando1
        crearArchivo(comando, "reinicio.bat")
        Dim archivo_bat As String = System.AppDomain.CurrentDomain.BaseDirectory() & "reinicio.bat"
        System.Diagnostics.Process.Start(archivo_bat)
        Return "Termino"
    End Function

    <WebMethod()> _
    Public Function obtenerLog() As String
        Return leerArchivo()
    End Function


    Public Sub escribirArchivo(ByVal texto As String)
        'Lectura de archivo
        Dim str_contenido As String = ""
        Dim oSR As StreamReader = New StreamReader(ruta_log)
        Dim line As String
        Do
            line = oSR.ReadLine()
            str_contenido = str_contenido & line
        Loop Until line Is Nothing
        oSR.Close()
        'Escritura de archivo
        Dim oSW As New StreamWriter(ruta_log)
        oSW.WriteLine(str_contenido & texto & vbNewLine)
        oSW.Flush()
        oSW.Dispose()
    End Sub

    Public Sub crearArchivo(ByVal texto As String, ByVal nombre_archivo As String)
        Dim ruta As String = System.AppDomain.CurrentDomain.BaseDirectory() & nombre_archivo
        Dim oSW As New StreamWriter(ruta)
        oSW.WriteLine(texto)
        oSW.Flush()
        oSW.Dispose()
    End Sub

    Public Function leerArchivo() As String
        'Lectura de archivo
        Dim str_contenido As String = ""
        Dim oSR As StreamReader = New StreamReader(ruta_log)
        Dim line As String
        Do
            line = oSR.ReadLine()
            str_contenido = str_contenido & line & salto_linea
        Loop Until line Is Nothing
        oSR.Close()
        Return str_contenido
    End Function



End Class