Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cargarCmbTipoVuelo()
        lblMensaje.Text = ""
    End Sub

    Private Sub cargarCmbTipoVuelo()
        cmbTipoVuelo.Items.Add("Internacional")
        cmbTipoVuelo.Items.Add("Nacional")
        cmbTipoVuelo.SelectedIndex = 1
    End Sub


    Private Sub txtCodigoAerolinea_TextAlignChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigoAerolinea.TextAlignChanged
        txtCodigoAerolinea.Text = txtCodigoAerolinea.Text.ToUpper()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objAcceso As New AccesoDatos
        Dim objCompania As TUA_Compania = objAcceso.getCompania(txtCodigoAerolinea.Text.Trim())
        If Not objCompania Is Nothing Then
            Dim objVuelo As New TUA_VueloProgramado
            Dim sfechaActual As String = DateTime.Now.ToShortDateString()
            sfechaActual = sfechaActual.Split("/")(2) + sfechaActual.Split("/")(1) + sfechaActual.Split("/")(0)
            Dim dhoraActual As DateTime = DateTime.Now
            Dim shoraActual As String = dhoraActual.Hour.ToString().PadLeft(2, "0") + dhoraActual.Minute.ToString().PadLeft(2, "0") + dhoraActual.Second.ToString().PadLeft(2, "0")
            Dim sfechaVuelo As String = dtFechaVuelo.Value.ToShortDateString
            sfechaVuelo = sfechaVuelo.Split("/")(2) + sfechaVuelo.Split("/")(1) + sfechaVuelo.Split("/")(0)
            objVuelo.Cod_Compania = objCompania.Cod_Compania
            objVuelo.Fch_Vuelo = sfechaVuelo
            objVuelo.Tip_Estado = "1"
            objVuelo.Num_Vuelo = txtCodigoAerolinea.Text.ToUpper.Trim() & txtNumeroVuelo.Text.Trim()
            objVuelo.Hor_Vuelo = ""
            objVuelo.Dsc_Destino = ""
            objVuelo.Flg_Programado = "1"
            objVuelo.Log_Fecha_Mod = sfechaActual
            objVuelo.Log_Hora_Mod = shoraActual


            If cmbTipoVuelo.SelectedItem.ToString = "Internacional" Then
                objVuelo.Tip_Vuelo = "I"
            Else
                objVuelo.Tip_Vuelo = "N"
            End If
            Try
                Dim objVueloActual As TUA_VueloProgramado = objAcceso.getVuelo(objVuelo.Cod_Compania, objVuelo.Num_Vuelo, objVuelo.Fch_Vuelo)
                If objVueloActual Is Nothing Then
                    objAcceso.registrarVuelo(objVuelo)
                    lblMensaje.Text = "Se ingreso el vuelo en la aerolinea " & objCompania.Dsc_Compania
                Else
                    objVueloActual.Tip_Vuelo = objVuelo.Tip_Vuelo
                    objAcceso.actualizarVuelo(objVueloActual)
                    lblMensaje.Text = "Se actualizó el vuelo de la aerolinea " & objCompania.Dsc_Compania
                End If
                limpiar()
            Catch ex As Exception
                lblMensaje.Text = "Error al ingresar Vuelo"
            End Try
        Else
            lblMensaje.Text = "Aerolinea no existe"
        End If

    End Sub

    Private Sub limpiar()
        txtCodigoAerolinea.Text = ""
        txtNumeroVuelo.Text = ""
        cmbTipoVuelo.SelectedIndex = 1
    End Sub

End Class
