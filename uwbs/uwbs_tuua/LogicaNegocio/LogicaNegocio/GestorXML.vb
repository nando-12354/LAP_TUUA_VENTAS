Imports System.Collections.Generic
Imports System.Xml

Public Class GestorXML

    Dim objFunciones As New Funciones

    Public Function VueloProgramado() As XmlDocument
        Dim doc As New XmlDocument()
        Dim declaration As XmlNode = doc.CreateNode(XmlNodeType.XmlDeclaration, Nothing, Nothing)
        doc.AppendChild(declaration)
        Dim root As XmlElement = doc.CreateElement("Vuelo_Programado")
        doc.AppendChild(root)

        Dim lvuelos As List(Of Vuelo) = objFunciones.getVuelosProgramados
        For Each obj_vuelo As Vuelo In lvuelos
            Dim xml_vuelo As XmlElement = doc.CreateElement("Vuelo")
            root.AppendChild(xml_vuelo)

            Dim CodEmpresa As XmlElement = doc.CreateElement("CodEmpresa")
            CodEmpresa.InnerText = obj_vuelo.CodEmpresa
            xml_vuelo.AppendChild(CodEmpresa)
            Dim NumeroVuelo As XmlElement = doc.CreateElement("NumeroVuelo")
            NumeroVuelo.InnerText = obj_vuelo.NumeroVuelo
            xml_vuelo.AppendChild(NumeroVuelo)
            Dim FechaVuelo As XmlElement = doc.CreateElement("FechaVuelo")
            FechaVuelo.InnerText = obj_vuelo.FechaVuelo
            xml_vuelo.AppendChild(FechaVuelo)
            Dim Descripcion As XmlElement = doc.CreateElement("Descripcion")
            Descripcion.InnerText = obj_vuelo.Descripcion
            xml_vuelo.AppendChild(Descripcion)
            Dim TipoVuelo As XmlElement = doc.CreateElement("TipoVuelo")
            TipoVuelo.InnerText = obj_vuelo.TipoVuelo
            xml_vuelo.AppendChild(TipoVuelo)
            Dim Estado As XmlElement = doc.CreateElement("Estado")
            Estado.InnerText = obj_vuelo.Estado
            xml_vuelo.AppendChild(Estado)
            Dim Destino As XmlElement = doc.CreateElement("Destino")
            Destino.InnerText = obj_vuelo.Destino
            xml_vuelo.AppendChild(Destino)
        Next

        Return doc
    End Function

    Public Function VueloTemporada() As XmlDocument
        Dim doc As New XmlDocument()
        Dim declaration As XmlNode = doc.CreateNode(XmlNodeType.XmlDeclaration, Nothing, Nothing)
        doc.AppendChild(declaration)
        Dim root As XmlElement = doc.CreateElement("Vuelo_Temporada")
        doc.AppendChild(root)

        Dim lvuelos As List(Of Vuelo) = objFunciones.getVuelosTemporada
        For Each obj_vuelo As Vuelo In lvuelos
            Dim xml_vuelo As XmlElement = doc.CreateElement("Vuelo")
            root.AppendChild(xml_vuelo)

            Dim CodEmpresa As XmlElement = doc.CreateElement("CodEmpresa")
            CodEmpresa.InnerText = obj_vuelo.CodEmpresa
            xml_vuelo.AppendChild(CodEmpresa)
            Dim NumeroVuelo As XmlElement = doc.CreateElement("NumeroVuelo")
            NumeroVuelo.InnerText = obj_vuelo.NumeroVuelo
            xml_vuelo.AppendChild(NumeroVuelo)
            Dim FechaVuelo As XmlElement = doc.CreateElement("FechaVuelo")
            FechaVuelo.InnerText = obj_vuelo.FechaVuelo
            xml_vuelo.AppendChild(FechaVuelo)
            Dim Descripcion As XmlElement = doc.CreateElement("Descripcion")
            Descripcion.InnerText = obj_vuelo.Descripcion
            xml_vuelo.AppendChild(Descripcion)
            Dim TipoVuelo As XmlElement = doc.CreateElement("TipoVuelo")
            TipoVuelo.InnerText = obj_vuelo.TipoVuelo
            xml_vuelo.AppendChild(TipoVuelo)
            Dim Estado As XmlElement = doc.CreateElement("Estado")
            Estado.InnerText = obj_vuelo.Estado
            xml_vuelo.AppendChild(Estado)
            Dim Destino As XmlElement = doc.CreateElement("Destino")
            Destino.InnerText = obj_vuelo.Destino
            xml_vuelo.AppendChild(Destino)
        Next

        Return doc
    End Function


    Public Function Compania() As XmlDocument
        Dim doc As New XmlDocument()
        Dim declaration As XmlNode = doc.CreateNode(XmlNodeType.XmlDeclaration, Nothing, Nothing)
        doc.AppendChild(declaration)
        Dim root As XmlElement = doc.CreateElement("Companias")
        doc.AppendChild(root)

        Dim lcompanias As List(Of Compania) = objFunciones.getCompania
        For Each obj_compania As Compania In lcompanias
            Dim xml_compania As XmlElement = doc.CreateElement("Compania")
            root.AppendChild(xml_compania)

            Dim CodigoAereolinea As XmlElement = doc.CreateElement("CodigoAerolinea")
            CodigoAereolinea.InnerText = obj_compania.CodigoAereolinea
            xml_compania.AppendChild(CodigoAereolinea)
            Dim CodigoInterno As XmlElement = doc.CreateElement("CodigoInterno")
            CodigoInterno.InnerText = obj_compania.CodigoInterno
            xml_compania.AppendChild(CodigoInterno)
            Dim CodigoIATA As XmlElement = doc.CreateElement("CodigoIATA")
            CodigoIATA.InnerText = obj_compania.CodigoIATA
            xml_compania.AppendChild(CodigoIATA)
            Dim CodigoOACI As XmlElement = doc.CreateElement("CodigoOACI")
            CodigoOACI.InnerText = obj_compania.CodigoOACI
            xml_compania.AppendChild(CodigoOACI)
            Dim CodigoSAP As XmlElement = doc.CreateElement("CodigoSAP")
            CodigoSAP.InnerText = obj_compania.CodigoSAP
            xml_compania.AppendChild(CodigoSAP)
            Dim Nombre As XmlElement = doc.CreateElement("Nombre")
            Nombre.InnerText = obj_compania.Nombre
            xml_compania.AppendChild(Nombre)
            Dim Ruc As XmlElement = doc.CreateElement("Ruc")
            Ruc.InnerText = obj_compania.RUC
            xml_compania.AppendChild(Ruc)
        Next

        Return doc
    End Function


    Public Function TasaCambio(ByVal cad_xml As String) As XmlDocument
        Dim doc As New XmlDocument()
        Dim declaration As XmlNode = doc.CreateNode(XmlNodeType.XmlDeclaration, Nothing, Nothing)
        doc.AppendChild(declaration)
        Dim root As XmlElement = doc.CreateElement("Tasa_Cambio_Moneda")
        doc.AppendChild(root)

        Dim ltipomoneda As List(Of Tipo_Moneda) = objFunciones.getTasaCambio(cad_xml)
        If Not (ltipomoneda.Count = 0) Then
            For Each obj_tipomoneda As Tipo_Moneda In ltipomoneda
                Dim xml_tipomoneda As XmlElement = doc.CreateElement("Tipo_Moneda")
                root.AppendChild(xml_tipomoneda)

                Dim Id As XmlElement = doc.CreateElement("Id")
                Id.InnerText = obj_tipomoneda.Id
                xml_tipomoneda.AppendChild(Id)
                Dim Compra As XmlElement = doc.CreateElement("Compra")
                Compra.InnerText = obj_tipomoneda.Compra
                xml_tipomoneda.AppendChild(Compra)
                Dim Venta As XmlElement = doc.CreateElement("Venta")
                Venta.InnerText = obj_tipomoneda.Venta
                xml_tipomoneda.AppendChild(Venta)
            Next
        Else
            doc = Nothing
        End If

        Return doc
    End Function


    Public Function VueloProgramadoFTP(ByVal lista As List(Of String)) As XmlDocument
        Dim doc As New XmlDocument()
        Dim declaration As XmlNode = doc.CreateNode(XmlNodeType.XmlDeclaration, Nothing, Nothing)
        doc.AppendChild(declaration)
        Dim root As XmlElement = doc.CreateElement("Vuelo_Programado")
        doc.AppendChild(root)

        Dim lvuelos As List(Of Vuelo) = objFunciones.getVuelosProgramadosFTP(lista)
        For Each obj_vuelo As Vuelo In lvuelos
            Dim xml_vuelo As XmlElement = doc.CreateElement("Vuelo")
            root.AppendChild(xml_vuelo)

            Dim CodEmpresa As XmlElement = doc.CreateElement("CodEmpresa")
            CodEmpresa.InnerText = obj_vuelo.CodEmpresa
            xml_vuelo.AppendChild(CodEmpresa)
            Dim NumeroVuelo As XmlElement = doc.CreateElement("NumeroVuelo")
            NumeroVuelo.InnerText = obj_vuelo.NumeroVuelo
            xml_vuelo.AppendChild(NumeroVuelo)
            Dim FechaVuelo As XmlElement = doc.CreateElement("FechaVuelo")
            FechaVuelo.InnerText = obj_vuelo.FechaVuelo
            xml_vuelo.AppendChild(FechaVuelo)
            Dim Descripcion As XmlElement = doc.CreateElement("Descripcion")
            Descripcion.InnerText = obj_vuelo.Descripcion
            xml_vuelo.AppendChild(Descripcion)
            Dim TipoVuelo As XmlElement = doc.CreateElement("TipoVuelo")
            TipoVuelo.InnerText = obj_vuelo.TipoVuelo
            xml_vuelo.AppendChild(TipoVuelo)
            Dim Estado As XmlElement = doc.CreateElement("Estado")
            Estado.InnerText = obj_vuelo.Estado
            xml_vuelo.AppendChild(Estado)
            Dim Destino As XmlElement = doc.CreateElement("Destino")
            Destino.InnerText = obj_vuelo.Destino
            xml_vuelo.AppendChild(Destino)
        Next

        Return doc
    End Function

End Class
