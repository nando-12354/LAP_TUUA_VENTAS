Imports AccesoDatos

Public Class Compania
    Public CodigoAereolinea As String
    Public CodigoInterno As String
    Public CodigoIATA As String
    Public CodigoOACI As String
    Public CodigoSAP As String
    Public Nombre As String
    Public RUC As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal compania As lap_entidad)
        Me.CodigoAereolinea = compania.cod_iata
        Me.CodigoInterno = compania.cod_entidad
        Me.Nombre = compania.dsc_raz_social
        Me.RUC = compania.dsc_doc_identif
        Me.CodigoSAP = compania.cod_entidad
        Me.CodigoIATA = compania.cod_iata
        Me.CodigoOACI = compania.cod_oaci
    End Sub

End Class
