Imports AccesoDatos

Public Class Vuelo
    Public CodEmpresa As String
    Public NumeroVuelo As String
    Public FechaVuelo As String
    Public Descripcion As String
    Public TipoVuelo As String
    Public Estado As Integer
    Public Destino As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal vuelo_dia As lap_vuelo_dia)
        Dim obj_entidad_AD As New lap_entidad_AD
        Try
            Dim obj_entidad As lap_entidad = obj_entidad_AD.getEntidad(vuelo_dia.cod_entidad)
            Me.CodEmpresa = obj_entidad.cod_iata
        Catch ex As Exception
            Me.CodEmpresa = Left(vuelo_dia.cod_vuelo, 2)
        End Try
        Me.NumeroVuelo = vuelo_dia.cod_vuelo
        Me.FechaVuelo = Format(vuelo_dia.fch_prog, "yyyy-MM-dd HH:mm:ss.fffK")
        Me.Descripcion = vuelo_dia.dsc_proc_dest
        Me.TipoVuelo = vuelo_dia.tip_origen
        Me.Estado = 1 'Estado es clase vuelo?
        Me.Destino = vuelo_dia.dsc_proc_dest_fin
    End Sub

    Public Sub New(ByVal linea_vuelo As String)
        Me.CodEmpresa = linea_vuelo.Substring(18, 3).Trim
        Me.NumeroVuelo = linea_vuelo.Substring(0, 3).Trim & linea_vuelo.Substring(3, 5).Trim
        Dim fecha_vuelo As Date = Date.Parse(linea_vuelo.Substring(54, 10) & " " & linea_vuelo.Substring(65, 5))
        Me.FechaVuelo = Format(fecha_vuelo, "yyyy-MM-dd HH:mm:ss.fffK")
        Dim str_descripcion As String = linea_vuelo.Substring(82, 20).Trim
        If linea_vuelo.Substring(118, 20).Trim.Length <> 0 Then
            str_descripcion = str_descripcion & " - " & linea_vuelo.Substring(118, 20).Trim
        End If
        Me.Descripcion = str_descripcion
        Me.TipoVuelo = linea_vuelo.Substring(9, 1)
        Me.Estado = "1"
        If linea_vuelo.Substring(161, 20).Trim().ToUpper = Constantes.demorado.ToUpper Then ' Agregado por POLARTE.
            Me.Estado = "2"
        End If
        Dim str_destino As String = linea_vuelo.Substring(118, 20).Trim
        If str_destino.Length = 0 Then
            str_destino = linea_vuelo.Substring(82, 20).Trim
        End If
        Me.Destino = str_destino
    End Sub

    Public Sub New(ByVal vuelo_temp As lap_vuelo_temporada)
        Dim obj_entidad_AD As New lap_entidad_AD
        Try
            Dim obj_entidad As lap_entidad = obj_entidad_AD.getEntidad(vuelo_temp.cod_entidad)
            Me.CodEmpresa = obj_entidad.cod_iata
        Catch ex As Exception
            Me.CodEmpresa = Left(vuelo_temp.cod_vuelo, 2)
        End Try
        Me.NumeroVuelo = vuelo_temp.cod_vuelo
        Me.FechaVuelo = Format(vuelo_temp.fch_fin_temporada, "yyyy-MM-dd HH:mm:ss.fffK")
        Me.Descripcion = vuelo_temp.dsc_proc_dest
        Me.TipoVuelo = vuelo_temp.tip_origen
        Me.Estado = 1 'Estado es clase vuelo?
        Me.Destino = vuelo_temp.dsc_proc_dest
    End Sub

End Class
