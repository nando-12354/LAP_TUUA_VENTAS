Public Class frmMolinete

    Private Sub frmMolinete_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtTitulo1.Text = My.Settings.txtTitulo1
        txtTitulo2.Text = My.Settings.txtTitulo2
        txtTitulo3.Text = My.Settings.txtTitulo3
        txtTitulo4.Text = My.Settings.txtTitulo4
        txtTitulo5.Text = My.Settings.txtTitulo5
        txtTitulo6.Text = My.Settings.txtTitulo6
    End Sub

    Private Sub AbrirVentanaLog(ByVal num_molinete As String)
        Dim ventana As New frmLog
        ventana.num_molinete = num_molinete
        ventana.Show()
    End Sub

#Region "Reinicio Forzoso"
    Private Sub btnReiniciarPC1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarPC1.Click
        Dim result As DialogResult
        result = MessageBox.Show("¿Esta opción debe usarse en caso la opción de reinicar servicio no funcione?¿Esta seguro de realizarla?", "Reinicio Forzoso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        If result = DialogResult.OK Then
            Try
                'Reinicio de PC
                'Se instancia el webservice cnx1
                Dim ws As New cnx1.ws_serviciosSoapClient
                ws.ReiniciarPC()
                AbrirVentanaLog("1")
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
            End Try
        End If
    End Sub

    Private Sub btnReiniciarPC2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarPC2.Click
        Dim result As DialogResult
        result = MessageBox.Show("¿Esta opción debe usarse en caso la opción de reinicar servicio no funcione?¿Esta seguro de realizarla?", "Reinicio Forzoso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        If result = DialogResult.OK Then
            Try
                'Reinicio de PC
                Dim ws As New cnx2.ws_serviciosSoapClient
                ws.ReiniciarPC()
                AbrirVentanaLog("2")
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
            End Try
        End If
    End Sub

    Private Sub btnReiniciarPC3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarPC3.Click
        Dim result As DialogResult
        result = MessageBox.Show("¿Esta opción debe usarse en caso la opción de reinicar servicio no funcione?¿Esta seguro de realizarla?", "Reinicio Forzoso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        If result = DialogResult.OK Then
            Try
                'Reinicio de PC
                Dim ws As New cnx3.ws_serviciosSoapClient
                ws.ReiniciarPC()
                AbrirVentanaLog("3")
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
            End Try
        End If
    End Sub

    Private Sub btnReiniciarPC4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarPC4.Click
        Dim result As DialogResult
        result = MessageBox.Show("¿Esta opción debe usarse en caso la opción de reinicar servicio no funcione?¿Esta seguro de realizarla?", "Reinicio Forzoso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        If result = DialogResult.OK Then
            Try
                'Reinicio de PC
                Dim ws As New cnx4.ws_serviciosSoapClient
                ws.ReiniciarPC()
                AbrirVentanaLog("4")
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
            End Try
        End If
    End Sub

    Private Sub btnReiniciarPC5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarPC5.Click
        Dim result As DialogResult
        result = MessageBox.Show("¿Esta opción debe usarse en caso la opción de reinicar servicio no funcione?¿Esta seguro de realizarla?", "Reinicio Forzoso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        If result = DialogResult.OK Then
            Try
                'Reinicio de PC
                Dim ws As New cnx5.ws_serviciosSoapClient
                ws.ReiniciarPC()
                AbrirVentanaLog("5")
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
            End Try
        End If
    End Sub

    Private Sub btnReiniciarPC6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarPC6.Click
        Dim result As DialogResult
        result = MessageBox.Show("¿Esta opción debe usarse en caso la opción de reinicar servicio no funcione?¿Esta seguro de realizarla?", "Reinicio Forzoso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        If result = DialogResult.OK Then
            Try
                'Reinicio de PC
                Dim ws As New cnx6.ws_serviciosSoapClient
                ws.ReiniciarPC()
                AbrirVentanaLog("6")
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
            End Try
        End If
    End Sub
#End Region


#Region "Reinicio de Servicio"
    Private Sub btnReiniciarServ1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarServ1.Click
        Try
            Dim ws As New cnx1.ws_serviciosSoapClient
            ws.ReiniciarServicio()
            AbrirVentanaLog("1")
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        End Try
    End Sub

    Private Sub btnReiniciarServ2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarServ2.Click
        Try
            Dim ws As New cnx2.ws_serviciosSoapClient
            ws.ReiniciarServicio()
            AbrirVentanaLog("2")
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        End Try
    End Sub

    Private Sub btnReiniciarServ3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarServ3.Click
        Try
            Dim ws As New cnx3.ws_serviciosSoapClient
            ws.ReiniciarServicio()
            AbrirVentanaLog("3")
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        End Try
    End Sub

    Private Sub btnReiniciarServ4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarServ4.Click
        Try
            Dim ws As New cnx4.ws_serviciosSoapClient
            ws.ReiniciarServicio()
            AbrirVentanaLog("4")
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        End Try
    End Sub

    Private Sub btnReiniciarServ5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarServ5.Click
        Try
            Dim ws As New cnx5.ws_serviciosSoapClient
            ws.ReiniciarServicio()
            AbrirVentanaLog("5")
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        End Try
    End Sub

    Private Sub btnReiniciarServ6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciarServ6.Click
        Try
            Dim ws As New cnx6.ws_serviciosSoapClient
            ws.ReiniciarServicio()
            AbrirVentanaLog("6")
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con el Molinete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
        End Try
    End Sub
#End Region

#Region "Abrir Ventana Log"
    Private Sub btnLog1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLog1.Click
        AbrirVentanaLog("1")
    End Sub

    Private Sub btnLog2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLog2.Click
        AbrirVentanaLog("2")
    End Sub

    Private Sub btnLog3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLog3.Click
        AbrirVentanaLog("3")
    End Sub

    Private Sub btnLog4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLog4.Click
        AbrirVentanaLog("4")
    End Sub

    Private Sub btnLog5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLog5.Click
        AbrirVentanaLog("5")
    End Sub

    Private Sub btnLog6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLog6.Click
        AbrirVentanaLog("6")
    End Sub
#End Region

End Class
