Imports System.Collections.Generic
Imports System.Linq

Public Class lap_vuelo_AD

    Dim dtl As New DB_LAP_VUELOSDataContext

    Public Function getVuelosProgBD() As List(Of lap_vuelo_dia)
        Return (From vuelo In dtl.lap_vuelo_dias _
                Where vuelo.tip_operacion = Constantes.salidas _
                ).ToList
    End Function

    Public Function getVuelosTempBD() As List(Of lap_vuelo_temporada)
        Dim hoy As Date = Format(Now, "Short Date")
        Return (From vuelo In dtl.lap_vuelo_temporadas _
                Where vuelo.fch_ini_temporada <= hoy _
                And vuelo.fch_ini_temporada >= hoy _
                ).ToList
    End Function

End Class
