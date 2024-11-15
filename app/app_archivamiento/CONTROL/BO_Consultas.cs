using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;

namespace LAP.TUUA.CONTROL
{
    public class BO_Consultas
    {
        private DAO_Archivo dao_Archivo;

        public BO_Consultas()
        {
            dao_Archivo = new DAO_Archivo(Property.htSPConfig);
        }

        public DataTable ObtenerHistoricoArchivamiento()
        {
            DataTable dtObtenerHistoricoArchivamiento = SelectHistoricoArchivamiento();
            dtObtenerHistoricoArchivamiento.Columns.Add("FechaInicial", System.Type.GetType("System.String"));
            dtObtenerHistoricoArchivamiento.Columns.Add("FechaFinal", System.Type.GetType("System.String"));
            dtObtenerHistoricoArchivamiento.Columns.Add("FechaProceso", System.Type.GetType("System.String"));
            dtObtenerHistoricoArchivamiento.Columns.Add("EstadoDescripcion", System.Type.GetType("System.String"));
            String cultureName = (String)Property.htProperty[Define.CULTURENAME_FOR_MONTHSABREVIATED];
            for (int i = 0; i < dtObtenerHistoricoArchivamiento.Rows.Count; i++ )
            {
                String Fch_Ini = dtObtenerHistoricoArchivamiento.Rows[i]["Fch_Ini"].ToString();
                String Fch_Fin = dtObtenerHistoricoArchivamiento.Rows[i]["Fch_Fin"].ToString();
                Fch_Ini = Fch_Ini.Substring(6, 2) + " " + Function.GetAbbreviatedMonthName(cultureName, Int32.Parse(Fch_Ini.Substring(4, 2))) + " " + Fch_Ini.Substring(0, 4);
                Fch_Fin = Fch_Fin.Substring(6, 2) + " " + Function.GetAbbreviatedMonthName(cultureName, Int32.Parse(Fch_Fin.Substring(4, 2))) + " " + Fch_Fin.Substring(0, 4);
                dtObtenerHistoricoArchivamiento.Rows[i]["FechaInicial"] = Fch_Ini;
                dtObtenerHistoricoArchivamiento.Rows[i]["FechaFinal"] = Fch_Fin;

                if (dtObtenerHistoricoArchivamiento.Rows[i]["Tip_Estado"].Equals("1"))
                {
                    dtObtenerHistoricoArchivamiento.Rows[i]["EstadoDescripcion"] = dtObtenerHistoricoArchivamiento.Rows[i]["DscEstado"];    
                }
                else
                {
                    dtObtenerHistoricoArchivamiento.Rows[i]["EstadoDescripcion"] = dtObtenerHistoricoArchivamiento.Rows[i]["DscEstado"] + " ( Paso " + Int32.Parse(dtObtenerHistoricoArchivamiento.Rows[i]["Cod_Etapa"].ToString()) + " - " + dtObtenerHistoricoArchivamiento.Rows[i]["DscEtapa"] + ")";
                }
                String log_Fecha_Mod = dtObtenerHistoricoArchivamiento.Rows[i]["Log_Fecha_Mod"].ToString();
                String log_Hora_Mod = dtObtenerHistoricoArchivamiento.Rows[i]["Log_Hora_Mod"].ToString();

                log_Fecha_Mod = log_Fecha_Mod.Substring(6, 2) + "/" + log_Fecha_Mod.Substring(4, 2) + "/" + log_Fecha_Mod.Substring(0, 4);
                int hora = Int32.Parse(log_Hora_Mod.Substring(0, 2));
                String turno;
                if(hora>=12)
                {
                    turno = "p.m.";
                }
                else
                {
                    turno = "a.m.";
                }
                log_Hora_Mod = log_Hora_Mod.Substring(0, 2) + ":" + log_Hora_Mod.Substring(2, 2) + ":" + log_Hora_Mod.Substring(4, 2);

                dtObtenerHistoricoArchivamiento.Rows[i]["FechaProceso"] = log_Fecha_Mod + " " + log_Hora_Mod + " " + turno;
            }
            return dtObtenerHistoricoArchivamiento;
        }

        public DataTable CalculaFechaDisponible()
        {
            DataTable dtReturn = dao_Archivo.calculaFechaDisponible((string)Property.htProperty[Define.ID_DELAY_MESES_ARCHIVAMIENTO]);
            return dtReturn;
        }

        public DataTable ObtenerDetalleArchivamiento(String Cod_Archivo)
        {
            return SelectDetalleArchivamiento(Cod_Archivo);
            /*
            DataTable dtReturn = SelectDetalleArchivamiento(Cod_Archivo);
            if(dtReturn.Rows.Count == 0)
            {
                return null;
            }
            String xmlStringDocument = dtReturn.Rows[0]["Xml_Rango"].ToString();
            Tip_Estado = dtReturn.Rows[0]["Tip_Estado"].ToString();
            Cod_Etapa = dtReturn.Rows[0]["Cod_Etapa"].ToString();
            */
        }

        public ArrayList ObtenerXmlRangoArchivamiento(String xmlStringDocument)
        {
            DAO_ModalidadVenta daoModalidadVenta = new DAO_ModalidadVenta(Property.htSPConfig);
            DAO_Compania daoCompania = new DAO_Compania(Property.htSPConfig);

            System.Xml.XmlDocument xDoc;
            try
            {
                xDoc = new XmlDocument();
                xDoc.LoadXml(xmlStringDocument);

            }
            catch (Exception ex)
            {
                return null;
                //throw ex;
            }
            XmlNodeList xmlRangos = xDoc.GetElementsByTagName("rangos");
            XmlNodeList nodeHijos = ((XmlElement)xmlRangos[0]).ChildNodes;
            if (nodeHijos.Count == 0)
            {
                return null;
            }
            String valor_RangoIni;
            String valor_RangoFin;
            String cantidad;
            CampoValor campoValor;
            ArrayList arrDetalle = new ArrayList();
            foreach (XmlElement nodo1 in nodeHijos)
            {
                if (nodo1.Name.ToLower().Equals("ticket"))
                {
                    nodeHijos = nodo1.GetElementsByTagName("modalidad");
                    if (nodeHijos.Count == 0)
                    {
                        //OJO EN DUDA:
                        campoValor = new CampoValor();
                        campoValor.Campo = "ticket";
                        campoValor.Valor = "    (0)";
                        arrDetalle.Add(campoValor);
                        //

                        continue;
                    }
                    String id = "";
                    ArrayList arrDetalle2 = new ArrayList();
                    String modalidad;
                    ModalidadVenta modalidadVenta;
                    Compania compania;
                    foreach (XmlElement nodo2 in nodeHijos)
                    {
                        try
                        {
                            id = nodo2.GetAttribute("id");
                            modalidadVenta = daoModalidadVenta.obtenerxCodigo(id);
                            modalidad = modalidadVenta.SNomModalidad;
                        }
                        catch(Exception ex)
                        {
                            ex = new Exception("Armado del TreeView (Ticket) - Error al intentar obtener la modalidad (id=" + id + ") - Detalle Tecnico: " + ex.Message);
                            throw ex;
                        }

                        nodeHijos = nodo2.GetElementsByTagName("compania");
                        if (nodeHijos.Count == 0)
                        {
                            if (nodo2["rango_ini"] == null || nodo2["rango_fin"] == null || nodo2["nro"] == null)
                            {
                                continue;
                            }
                            valor_RangoIni = nodo2["rango_ini"].InnerText;
                            valor_RangoFin = nodo2["rango_fin"].InnerText;
                            cantidad = nodo2["nro"].InnerText;

                            campoValor = new CampoValor();
                            campoValor.Campo = modalidad;
                            campoValor.Valor = valor_RangoIni + "   -   " + valor_RangoFin + "    (" + cantidad + ")";
                            arrDetalle2.Add(campoValor);
                        }
                        else
                        {
                            ArrayList arrDetalle3 = new ArrayList();
                            String cia;
                            foreach (XmlElement nodo3 in nodeHijos)
                            {
                                try
                                {
                                    id = nodo3.GetAttribute("id");
                                    compania = daoCompania.obtenerxcodigo(id);
                                    cia = compania.SDscCompania;
                                }
                                catch(Exception ex)
                                {
                                    ex = new Exception("Armado del TreeView (Ticket) - Error al intentar obtener la compania (id=" + id + ") - Detalle Tecnico: " + ex.Message);
                                    throw ex;
                                }

                                if (nodo3["rango_ini"] == null || nodo3["rango_fin"] == null || nodo3["nro"] == null)
                                {
                                    continue;
                                }
                                valor_RangoIni = nodo3["rango_ini"].InnerText;
                                valor_RangoFin = nodo3["rango_fin"].InnerText;
                                cantidad = nodo3["nro"].InnerText;

                                campoValor = new CampoValor();
                                campoValor.Campo = cia;
                                campoValor.Valor = valor_RangoIni + "   -   " + valor_RangoFin + "    (" + cantidad + ")";
                                arrDetalle3.Add(campoValor);

                            }
                            campoValor = new CampoValor();
                            campoValor.Campo = modalidad;
                            campoValor.Valor = arrDetalle3;
                            arrDetalle2.Add(campoValor);
                        }
                    }

                    campoValor = new CampoValor();
                    campoValor.Campo = "ticket";
                    campoValor.Valor = arrDetalle2;
                    arrDetalle.Add(campoValor);
                }
                else if (nodo1.Name.ToLower().Equals("boarding"))
                {
                    //Primera version...
                    /*
                    if (nodo1["rango_ini"] == null || nodo1["rango_fin"] == null)
                    {
                        continue;
                    }
                    valor_RangoIni = nodo1["rango_ini"].InnerText;
                    valor_RangoFin = nodo1["rango_fin"].InnerText;

                    campoValor = new CampoValor();
                    campoValor.Campo = "boarding";
                    campoValor.Valor = valor_RangoIni + " " + valor_RangoFin;
                    arrDetalle.Add(campoValor);
                    */

                    nodeHijos = nodo1.GetElementsByTagName("modalidad");
                    if (nodeHijos.Count == 0)
                    {
                        //OJO EN DUDA:
                        campoValor = new CampoValor();
                        campoValor.Campo = "boarding";
                        campoValor.Valor = "    (0)";
                        arrDetalle.Add(campoValor);
                        //


                        continue;
                    }
                    String id = "";
                    ArrayList arrDetalle2 = new ArrayList();
                    String modalidad;
                    ModalidadVenta modalidadVenta;

                    //Compania compania;

                    foreach (XmlElement nodo2 in nodeHijos)
                    {
                        try
                        {
                            id = nodo2.GetAttribute("id");
                            modalidadVenta = daoModalidadVenta.obtenerxCodigo(id);
                            modalidad = modalidadVenta.SNomModalidad;
                        }
                        catch(Exception ex)
                        {
                            ex = new Exception("Armado del TreeView (Boarding) - Error al intentar obtener la modalidad (id=" + id + ") - Detalle Tecnico: " + ex.Message);
                            throw ex;                            
                        }

                        //EAG - 18/12/2009  - Lineas comentadas por que ya no se dividen por Compania

                        //nodeHijos = nodo2.GetElementsByTagName("compania");
                        //if (nodeHijos.Count == 0)
                        //{
                            if (nodo2["rango_ini"] == null || nodo2["rango_fin"] == null || nodo2["nro"] == null)
                            {
                                continue;
                            }
                            valor_RangoIni = nodo2["rango_ini"].InnerText;
                            valor_RangoFin = nodo2["rango_fin"].InnerText;
                            cantidad = nodo2["nro"].InnerText;

                            campoValor = new CampoValor();
                            campoValor.Campo = modalidad;
                            campoValor.Valor = valor_RangoIni + "   -   " + valor_RangoFin + "    (" + cantidad + ")";
                            arrDetalle2.Add(campoValor);
                        //}
                        //else
                        //{
                        //    ArrayList arrDetalle3 = new ArrayList();
                        //    String cia;
                        //    foreach (XmlElement nodo3 in nodeHijos)
                        //    {
                        //        id = nodo3.GetAttribute("id");
                        //        compania = daoCompania.obtenerxcodigo(id);
                        //        cia = compania.SDscCompania;

                        //        if (nodo3["rango_ini"] == null || nodo3["rango_fin"] == null || nodo3["nro"] == null)
                        //        {
                        //            continue;
                        //        }
                        //        valor_RangoIni = nodo3["rango_ini"].InnerText;
                        //        valor_RangoFin = nodo3["rango_fin"].InnerText;
                        //        cantidad = nodo3["nro"].InnerText;

                        //        campoValor = new CampoValor();
                        //        campoValor.Campo = cia;
                        //        campoValor.Valor = valor_RangoIni + "   -   " + valor_RangoFin + "    (" + cantidad + ")";
                        //        arrDetalle3.Add(campoValor);

                        //    }
                        //    campoValor = new CampoValor();
                        //    campoValor.Campo = modalidad;
                        //    campoValor.Valor = arrDetalle3;
                        //    arrDetalle2.Add(campoValor);
                        //}
                    }

                    campoValor = new CampoValor();
                    campoValor.Campo = "boarding";
                    campoValor.Valor = arrDetalle2;
                    arrDetalle.Add(campoValor);

                }
                else if (nodo1.Name.ToLower().Equals("operaciones"))
                {
                    if (nodo1["rango_ini"] == null || nodo1["rango_fin"] == null || nodo1["nro"] == null)
                    {
                        //OJO EN DUDA:
                        campoValor = new CampoValor();
                        campoValor.Campo = "operaciones";
                        campoValor.Valor = "    (0)";
                        arrDetalle.Add(campoValor);
                        //


                        continue;
                    }
                    valor_RangoIni = nodo1["rango_ini"].InnerText;
                    valor_RangoFin = nodo1["rango_fin"].InnerText;
                    cantidad = nodo1["nro"].InnerText;

                    campoValor = new CampoValor();
                    campoValor.Campo = "operaciones";
                    campoValor.Valor = valor_RangoIni + "   -   " + valor_RangoFin + "    (" + cantidad + ")";
                    arrDetalle.Add(campoValor);
                }
            }

            return arrDetalle;            
        }




        private DataTable SelectDetalleArchivamiento(String Cod_Archivo)
        {
            DataTable dtReturn = dao_Archivo.SelectDetalleArchivamiento(Cod_Archivo);
            return dtReturn;
        }

        private DataTable SelectHistoricoArchivamiento()
        {
            DataTable dtHistoricoArchivamiento = dao_Archivo.obtenerHistoricoArchivamiento();
            return dtHistoricoArchivamiento;
        }



    }

    public class CampoValor
    {
        private String campo;
        private Object valor;

        public string Campo
        {
            get { return campo; }
            set { campo = value; }
        }

        public Object Valor
        {
            get { return valor; }
            set { valor = value; }
        }
    }

}
