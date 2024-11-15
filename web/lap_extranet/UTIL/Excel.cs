using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LAP.EXTRANET.UTIL
{
    /// <summary>
    /// Clase para la exportación a Excel
    /// </summary>
    public class Excel
    {
        private Worksheet[] worksheet;

        //Clase anidada Worksheet
        public class Worksheet
        {
            private string _name;
            private DataTable _dtSource;
            private int[] widthColumns;
            private string[] columns;
            private string[] dataFields;

            /// <summary>
            /// Crea una hoja excel
            /// </summary>
            public Worksheet()
            {
            }
            /// <summary>
            /// Nombre de la hoja 
            /// </summary>
            /// <param name="nombre"></param>
            public Worksheet(string nombre)
            {
                this.Name = nombre;
            }

            //METODOS
            #region Atributos {get;set}
            /// <summary>
            /// Nombre de la Hoja
            /// </summary>
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            /// <summary>
            /// DataTable para poblar el reporte
            /// </summary>
            public DataTable Source
            {
                get { return _dtSource; }
                set { _dtSource = value; }
            }

            /// <summary>
            /// Tamaño de las columnas
            /// </summary>
            public int[] WidthColumns
            {
                get { return widthColumns; }
                set { widthColumns = value; }
            }

            /// <summary>
            /// Columnas que se mostrarán en la exportación
            /// </summary>
            public string[] Columns
            {
                get { return columns; }
                set { columns = value; }
            }

            /// <summary>
            /// Campos a mostrar del DataTable. 
            /// En caso de dar formato de fechas usar '@' 
            /// e.g. "@Fch_Vuelo" 
            /// </summary>
            public string[] DataFields
            {
                get { return dataFields; }
                set { dataFields = value; }
            }
            #endregion


            public StringBuilder obtenerDatosXML()
            {
                StringBuilder worksheetXML = new StringBuilder();

                worksheetXML.Append("<Worksheet ss:Name=\"" + Name + "\">\n");
                worksheetXML.Append("<Table>\n");

                //Columnas
                #region ANCHO DE LAS COLUMNAS

                if (WidthColumns != null)
                {
                    foreach (int width in widthColumns)
                    {
                        worksheetXML.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"" + width + "\"/>\n");
                    }
                }
                #endregion

                #region CABECERA
                if (Columns != null)
                {
                    worksheetXML.Append("<Row ss:AutoFitHeight=\"0\" ss:Height=\"30\">\n");

                    foreach (string column in columns)
                    {
                        worksheetXML.Append("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">" + column + "</Data></Cell>\n");
                    }

                    worksheetXML.Append("</Row>\n");
                }
                #endregion

                //DATA
                string estilo = "";
                string estiloNum = "";
                string estiloNumReal = "";
                int fila = 0;

                if (DataFields != null)
                {
                    foreach (DataRow row in Source.Rows)
                    {
                        worksheetXML.Append("<Row ss:AutoFitHeight=\"0\">\n");

                        if (fila % 2 != 0)
                        {
                            estilo = "s78";
                            estiloNum = "numero";
                            estiloNumReal = "real";
                        }
                        else
                        {
                            estilo = "s77";
                            estiloNum = "numeroInt";
                            estiloNumReal = "realInt";
                        }

                        foreach (string dataField in dataFields)
                        {
                            //Validamos el formato del campo @ para fechas
                            if (dataField.Contains("@"))
                            {
                                string field = dataField.Replace('@', ' ').Trim();
                                worksheetXML.Append("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + Fecha.formatoFechaExcel(row[field].ToString()) + "</Data></Cell>\n");
                            }
                            else
                            {
                                //Validacion para los campos tipo numericos
                                string tipo = row[dataField].GetType().ToString();
                                if (tipo == "System.String" || tipo == "System.DBNull")
                                {
                                    worksheetXML.Append("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + row[dataField] + "</Data></Cell>\n");
                                }
                                else
                                {
                                    if (tipo == "System.Int32" || tipo == "System.Int64") //entero
                                        worksheetXML.Append("<Cell ss:StyleID=\"" + estiloNum + "\"><Data ss:Type=\"Number\">" + row[dataField] + "</Data></Cell>\n");
                                    else
                                    {
                                        if (tipo == "System.Decimal" || tipo == "System.Double") //real
                                            worksheetXML.Append("<Cell ss:StyleID=\"" + estiloNumReal + "\"><Data ss:Type=\"Number\">" + row[dataField] + "</Data></Cell>\n");
                                    }
                                }
                            }
                        }

                        worksheetXML.Append("</Row>\n");
                        fila++;
                    }
                }
                else
                {
                    foreach (DataRow row in Source.Rows)
                    {
                        worksheetXML.Append("<Row ss:AutoFitHeight=\"0\">\n");

                        if (fila % 2 != 0)
                            estilo = "s78";
                        else
                            estilo = "s77";

                        foreach (DataColumn column in Source.Columns)
                        {
                            worksheetXML.Append("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + row[column.ColumnName] + "</Data></Cell>\n");
                        }

                        worksheetXML.Append("</Row>\n");
                        fila++;
                    }
                }

                worksheetXML.Append("</Table>\n");
                worksheetXML.Append("</Worksheet>\n");

                return worksheetXML;
            }
        }

        public Excel()
        {

        }

        public Worksheet[] Worksheets
        {
            get { return worksheet; }
            set { worksheet = value; }
        }

        public System.IO.StringWriter Save()
        {
            StringBuilder ExcelXML = new StringBuilder();

            //CABECERA DEL ARCHIVO
            #region CABECERA DEL ARCHIVO
            ExcelXML.Append("<?xml version=\"1.0\"?>\n");
            ExcelXML.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
            ExcelXML.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\n");
            ExcelXML.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\n");
            ExcelXML.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\n");
            ExcelXML.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\n");
            ExcelXML.Append(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n");
            ExcelXML.Append("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">\n");
            ExcelXML.Append("<Author>Hiper S.A</Author>\n");
            ExcelXML.Append("<LastAuthor>Hiper S.A</LastAuthor>\n");
            ExcelXML.Append("<Created>2011-01-10T16:34:47Z</Created>\n");
            ExcelXML.Append("<Version>12.00</Version>\n");
            ExcelXML.Append(" </DocumentProperties>\n");
            ExcelXML.Append(" <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
            ExcelXML.Append("<WindowHeight>7935</WindowHeight>\n");
            ExcelXML.Append("<WindowWidth>20055</WindowWidth>\n");
            ExcelXML.Append("<WindowTopX>240</WindowTopX>\n");
            ExcelXML.Append("<WindowTopY>75</WindowTopY>\n");
            ExcelXML.Append(" <ProtectStructure>False</ProtectStructure>\n");
            ExcelXML.Append("<ProtectWindows>False</ProtectWindows>\n");
            ExcelXML.Append("</ExcelWorkbook>\n");
            ExcelXML.Append(" <Styles>\n");
            ExcelXML.Append("<Style ss:ID=\"s66\">\n");
            ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"\n");
            ExcelXML.Append(" ss:Underline=\"Single\"/>\n");
            ExcelXML.Append("</Style>\n");
            ExcelXML.Append("<Style ss:ID=\"s76\">\n");
            ExcelXML.Append("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>\n");
            ExcelXML.Append("<Borders>\n");
            ExcelXML.Append("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
            ExcelXML.Append("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
            ExcelXML.Append("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
            ExcelXML.Append("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
            ExcelXML.Append("</Borders>\n");
            ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"\n");
            ExcelXML.Append(" ss:Bold=\"1\" ss:Underline=\"Single\"/>\n");
            ExcelXML.Append("<Interior ss:Color=\"#D8D8D8\" ss:Pattern=\"Solid\"/>\n");
            ExcelXML.Append("</Style>\n");
            ExcelXML.Append("<Style ss:ID=\"s77\">\n");
            ExcelXML.Append("<Alignment ss:Vertical=\"Bottom\"/>\n");
            ExcelXML.Append("<Borders/>\n");
            ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>\n");
            ExcelXML.Append("<Interior ss:Color=\"#F2F2F2\" ss:Pattern=\"Solid\"/>\n");
            ExcelXML.Append("<NumberFormat/>\n");
            ExcelXML.Append("<Protection/>\n");
            ExcelXML.Append(" </Style>\n");
            ExcelXML.Append("<Style ss:ID=\"s78\">\n");
            ExcelXML.Append("<Alignment ss:Vertical=\"Bottom\"/>\n");
            ExcelXML.Append("<Borders/>\n");
            ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>\n");
            ExcelXML.Append("<Interior />\n");
            ExcelXML.Append("<NumberFormat/>\n");
            ExcelXML.Append("<Protection/>\n");
            ExcelXML.Append(" </Style>\n");
            ExcelXML.Append("<Style ss:ID=\"numero\">");
            ExcelXML.Append("<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:Indent=\"1\"/>");
            ExcelXML.Append("<Borders/>");
            ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>");
            ExcelXML.Append("<Interior/>");
            ExcelXML.Append("<NumberFormat ss:Format=\"0\"/>");
            ExcelXML.Append("<Protection/>");
            ExcelXML.Append("</Style>");
            ExcelXML.Append("<Style ss:ID=\"real\">");
            ExcelXML.Append("<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:Indent=\"1\"/>");
            ExcelXML.Append("<Borders/>");
            ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>");
            ExcelXML.Append("<Interior/>");
            ExcelXML.Append("<NumberFormat ss:Format=\"0.00\"/>");
            ExcelXML.Append("<Protection/>");
            ExcelXML.Append("</Style>");
            ExcelXML.Append("<Style ss:ID=\"numeroInt\">");
            ExcelXML.Append("<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:Indent=\"1\"/>");
            ExcelXML.Append("<Borders/>");
            ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>");
            ExcelXML.Append("<Interior ss:Color=\"#F2F2F2\" ss:Pattern=\"Solid\"/>");
            ExcelXML.Append("<NumberFormat ss:Format=\"0\"/>");
            ExcelXML.Append("<Protection/>");
            ExcelXML.Append("</Style>");
            ExcelXML.Append("<Style ss:ID=\"realInt\">");
            ExcelXML.Append("<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:Indent=\"1\"/>");
            ExcelXML.Append("<Borders/>");
            ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>");
            ExcelXML.Append("<Interior ss:Color=\"#F2F2F2\" ss:Pattern=\"Solid\"/>");
            ExcelXML.Append("<NumberFormat ss:Format=\"0.00\"/>");
            ExcelXML.Append("<Protection/>");
            ExcelXML.Append("</Style>");
            ExcelXML.Append(" </Styles>\n");
            #endregion
            string startExcelXML = ExcelXML.ToString();
            System.IO.StringWriter Archivo = new System.IO.StringWriter();

            Archivo.Write(startExcelXML);

            //DATA POR CADA WORKSHEET
            foreach (Worksheet sheet in Worksheets)
            {
                Archivo.Write(sheet.obtenerDatosXML());
            }

            //FIN DE ARCHIVO
            Archivo.Write("</Workbook>\n");

            return Archivo;
        }
    }

}
