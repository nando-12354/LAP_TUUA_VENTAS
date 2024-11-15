Imports System.Collections.Generic
Imports System.Linq

Public Class AccesoDatos

    Dim dt As New DBTUUADataContext

    Public Sub registrarVuelo(ByVal objVuelo As TUA_VueloProgramado)
        dt.TUA_VueloProgramados.InsertOnSubmit(objVuelo)
        dt.SubmitChanges()
    End Sub

    Public Function getVuelo(ByVal cod_compania As String, ByVal num_vuelo As String, ByVal fch_vuelo As String) As TUA_VueloProgramado
        Try
            Return dt.TUA_VueloProgramados.Single(Function(ts) ts.Cod_Compania = cod_compania And ts.Num_Vuelo = num_vuelo And ts.Fch_Vuelo = fch_vuelo)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub actualizarVuelo(ByVal objVuelo As TUA_VueloProgramado)
        dt.SubmitChanges()
    End Sub

    Public Function getCompania(ByVal cod_iata As String) As TUA_Compania
        Try
            Return dt.TUA_Companias.Single(Function(ts) ts.Cod_IATA = cod_iata)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
