Imports System.Collections.Generic
Imports AccesoDatos
Imports System.Xml

Public Class Funciones

    Dim obj_vuelo_dia_AD As New lap_vuelo_AD
    Dim obj_entidad_AD As New lap_entidad_AD

    Public Function getVuelosProgramados() As List(Of Vuelo)
        Dim lvuelos As New List(Of Vuelo)
        Dim lvuelos_bd As List(Of lap_vuelo_dia) = obj_vuelo_dia_AD.getVuelosProgBD()

        For Each vuelo_bd As lap_vuelo_dia In lvuelos_bd
            Dim obj_vuelo As New Vuelo(vuelo_bd)
            lvuelos.Add(obj_vuelo)
        Next
        Return lvuelos
    End Function

    Public Function getVuelosTemporada() As List(Of Vuelo)
        Dim lvuelos As New List(Of Vuelo)
        Dim lvuelos_bd As List(Of lap_vuelo_temporada) = obj_vuelo_dia_AD.getVuelosTempBD()

        For Each vuelo_bd As lap_vuelo_temporada In lvuelos_bd
            Dim obj_vuelo As New Vuelo(vuelo_bd)
            lvuelos.Add(obj_vuelo)
        Next
        Return lvuelos
    End Function

    Public Function getCompania() As List(Of Compania)
        Dim lcompania As New List(Of Compania)
        Dim lcompania_bd As List(Of lap_entidad) = obj_entidad_AD.getCompanias

        For Each compania_bd As lap_entidad In lcompania_bd
            Dim obj_compania As New Compania(compania_bd)
            lcompania.Add(obj_compania)
        Next
        Return lcompania
    End Function


    Public Function getTasaCambio(ByVal cad_xml As String) As List(Of Tipo_Moneda)
        Dim ltipo_moneda As New List(Of Tipo_Moneda)
        Dim xml_doc As New XmlDocument()
        Dim NodeList As XmlNodeList
        Dim Node As XmlNode

        xml_doc.InnerXml = cad_xml
        NodeList = xml_doc.SelectNodes("/Tasa_Cambio/Operacion")

        For Each Node In NodeList
            Dim cod_mon_ini As String = Node.ChildNodes.ItemOf(0).InnerText
            Dim cod_mon_fin As String = Node.ChildNodes.ItemOf(1).InnerText
            Dim tipo_ope As String = Node.ChildNodes.ItemOf(2).InnerText
            Dim valor As String = Node.ChildNodes.ItemOf(3).InnerText

            Dim obj_tipo_moneda As Tipo_Moneda
            Dim existe_registro As Boolean = False
            For Each obj_moneda In ltipo_moneda
                If obj_moneda.Id = cod_mon_ini Or obj_moneda.Id = cod_mon_fin Then
                    existe_registro = True
                    obj_tipo_moneda = obj_moneda
                End If
            Next

            If existe_registro Then
                If cod_mon_ini = Constantes.Soles Then
                    obj_tipo_moneda.Venta = valor
                End If
                If cod_mon_fin = Constantes.Soles Then
                    obj_tipo_moneda.Compra = valor
                End If
            Else
                If cod_mon_ini = Constantes.Soles Then
                    obj_tipo_moneda = New Tipo_Moneda(cod_mon_fin, 0, valor)
                    ltipo_moneda.Add(obj_tipo_moneda)
                End If
                If cod_mon_fin = Constantes.Soles Then
                    obj_tipo_moneda = New Tipo_Moneda(cod_mon_ini, valor, 0)
                    ltipo_moneda.Add(obj_tipo_moneda)
                End If
            End If
        Next

        Return ltipo_moneda

    End Function


    Public Function getVuelosProgramadosFTP(ByVal lista As List(Of String)) As List(Of Vuelo)
        Dim lvuelos As New List(Of Vuelo)
        For Each vuelo_str As String In lista
            If vuelo_str.Substring(11, 1) = "S" Then
                Dim obj_vuelo As New Vuelo(vuelo_str)
                lvuelos.Add(obj_vuelo)
            End If
        Next
        Return lvuelos
    End Function

End Class
