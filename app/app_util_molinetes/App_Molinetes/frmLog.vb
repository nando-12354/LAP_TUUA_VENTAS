Public Class frmLog

    Public num_molinete As String
    Dim salto_linea As String = "°"

    Private Sub frmLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        obtenerLog()
    End Sub

    Private Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        obtenerLog()
    End Sub

    Public Sub obtenerLog()
        Try
            Select Case num_molinete
                Case "1"
                    Dim ws As New cnx1.ws_serviciosSoapClient
                    txtLog.Text = ws.obtenerLog()
                    txtLog.Text = txtLog.Text.Replace(salto_linea, vbNewLine)
                Case "2"
                    Dim ws As New cnx2.ws_serviciosSoapClient
                    txtLog.Text = ws.obtenerLog()
                Case "3"
                    Dim ws As New cnx3.ws_serviciosSoapClient
                    txtLog.Text = ws.obtenerLog()
                Case "4"
                    Dim ws As New cnx4.ws_serviciosSoapClient
                    txtLog.Text = ws.obtenerLog()
                Case "5"
                    Dim ws As New cnx5.ws_serviciosSoapClient
                    txtLog.Text = ws.obtenerLog()
                Case Else
                    Dim ws As New cnx6.ws_serviciosSoapClient
                    txtLog.Text = ws.obtenerLog()
            End Select
        Catch ex As Exception
            txtLog.Text = "No hay conexión con el molinete"
        End Try
    End Sub

End Class