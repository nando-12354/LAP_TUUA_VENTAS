Imports System.Collections.Generic
Imports System.Linq

Public Class lap_entidad_AD
    Dim dtl As New DB_LAP_VUELOSDataContext

    Public Function getCompanias() As List(Of lap_entidad)
        Return (From entidad In dtl.lap_entidads _
                Where entidad.cod_clase_entidad = Constantes.cod_clase_entidad _
                And entidad.flg_activo = Constantes.flg_activo _
                And entidad.cod_iata IsNot Nothing _
                ).ToList
    End Function

    Public Function getEntidad(ByVal id As String) As lap_entidad
        Return dtl.lap_entidads.Single(Function(ts) ts.cod_entidad = id)
    End Function

End Class
