
Public Class Tipo_Moneda
    Public Id As String
    Public Compra As Double
    Public Venta As Double

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As String, ByVal compra As String, ByVal venta As String)
        Me.Id = id
        Me.Compra = compra
        Me.Venta = venta
    End Sub

End Class
