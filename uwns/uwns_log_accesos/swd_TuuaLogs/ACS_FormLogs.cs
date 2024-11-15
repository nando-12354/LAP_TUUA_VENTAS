using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONVERSOR;
using System.Collections;
using System.Security.Permissions;

namespace LAP.TUUA.LOGS
{
    public partial class ACS_FormLogs : Form
    {
        public enum TipoDeArchivoPlano { Delimited, Fixed }
        public DataTable dtRegistros;
        ACS_Util Obj_Util = new ACS_Util();
        private ACS_Resolver Obj_Resolver;
        
        public DataTable dtUltimoregistro;

        public ACS_FormLogs(ACS_Resolver resolver)
        {
            InitializeComponent();

            try
            {
                this.Obj_Resolver = resolver;

                Obj_Resolver.CrearDAOBoardingBcbpErr();
                Obj_Resolver.CrearDAOLogProcesados();

                //creamos la tabla con los registros
                dtRegistros = new DataTable();
                dtRegistros.Columns.Add("Fech_Error");
                dtRegistros.Columns.Add("Cod_Molinete");
                dtRegistros.Columns.Add("Dsc_Error");
                dtRegistros.Columns.Add("Tip_Error");
                dtRegistros.Columns.Add("Tip_Boarding");
                dtRegistros.Columns.Add("Cod_Compania");
                dtRegistros.Columns.Add("Num_Vuelo");
                dtRegistros.Columns.Add("Fech_Vuelo");
                dtRegistros.Columns.Add("Num_Asiento");
                dtRegistros.Columns.Add("Nom_Pasajero");
                dtRegistros.Columns.Add("Seccion_Error");

                string directory = @"" + (string)Property.htProperty["DIRECTORIO_LOG"] + "";
                FileInfo directorio = new FileInfo(directory);
                directorio.Directory.GetFiles();

                //BUSCAMOS EL ULTIMO ARCHIVO PROCESADO
                dtUltimoregistro = new DataTable();
                dtUltimoregistro = Obj_Resolver.UltimoArchivoProcesado((string)Property.htProperty["COD_MOLINETE"]);

                if (dtUltimoregistro != null)
                {
                    string sNomArchivo = dtUltimoregistro.Rows[0]["Nom_Archivo"].ToString();
                    string sUtlimoRegistro = dtUltimoregistro.Rows[0]["Log_Fecha_Mod"].ToString() + dtUltimoregistro.Rows[0]["Log_Hora_Mod"].ToString();

                    LeerArchivoPlano(new FileInfo(directory + sNomArchivo), false, TipoDeArchivoPlano.Delimited, sUtlimoRegistro);
                }

                //CONTINUAMOS CON TODOS LOS DEMAS LOGS
                foreach (FileInfo file in directorio.Directory.GetFiles())
                {
                    //VALIDAMOS QUE EL ARCHIVO NO ESTE PROCESADO
                    bool procesado;
                    procesado = Obj_Resolver.ArchivoProcesado((string)Property.htProperty["COD_MOLINETE"], file.Name);

                    //PROCESAMOS LOS DATOS
                    if (!procesado)
                        LeerArchivoPlano(new FileInfo(directory + file.Name), false, TipoDeArchivoPlano.Delimited, "");
                }

                //MENSAJE CARGA MANUAL
                bool carga_manual = Convert.ToBoolean((string)Property.htProperty["CARGA_MANUAL"]);
                if (carga_manual)
                {
                    Obj_Util.EscribirLog("LAP.TUUA.LOGS.ACS_Controlador", "Fin Carga Manual");
                }

                dataGridView1.DataSource = dtRegistros;
            }
            catch(Exception ex)
            {
                Obj_Util.EscribirLog("LAP.TUUA.LOGS.ACS_FormLogs", ex.Message);
            }

        }

        public DataTable LeerArchivoPlano(FileInfo archivo, bool tieneEncabezado, TipoDeArchivoPlano tipoDeArchivo, string strUltimoRegistro)
        {
            try
            {
                Obj_Util.EscribirLog("LAP.TUUA.LOGS.ACS_FormLogs", "Procesando Archivo: " + archivo.Name);

                //VALIDA QUE EL ARCHIVO TMP.TXT NO EXISTA
                FileInfo tmp = new FileInfo(@"" + (string)Property.htProperty["DIRECTORIO_TEMPORAL"] + "tmp.txt");
                if (tmp.Exists)
                    tmp.Delete();

                //CREAMOS LA COPIA DEL ARCHIVO PARA PROCESAR
                if (archivo.Exists)
                {
                    archivo.CopyTo(@"" + (string)Property.htProperty["DIRECTORIO_TEMPORAL"] + "tmp.txt");
                    FileInfo arch = new FileInfo(@"" + (string)Property.htProperty["DIRECTORIO_TEMPORAL"] + "tmp.txt");

                    DataTable dt_procesos = new DataTable();

                    //PROCESAMOS EL ARCHIVO LINEA POR LINEA
                    int counter = 0;
                    string sline;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("F1");
                    System.IO.StreamReader file =
                       new System.IO.StreamReader(@"" + (string)Property.htProperty["DIRECTORIO_TEMPORAL"] + "tmp.txt");
                    while ((sline = file.ReadLine()) != null)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[0] = sline;
                        dt.Rows.Add(newRow);

                        counter++;
                    }

                    file.Close();


                    //INSERTAMOS A LA TABLA dt_procesos SOLO LOS LOGS QUE TENGAN DESDE INICIA FLUJO - FINALIZA FLUJO 
                    dt_procesos.Columns.Add("F1");
                    int i = 0;
                    string inicial = "INICIA FLUJO";
                    string fin = "FINALIZA FLUJO";
                    bool recuperar = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        string linea = row.ItemArray[0].ToString();

                        if (!linea.Contains("Conexion Remota Activa") && !linea.Contains("Conexion Remota Inactiva"))
                        {
                            if (linea.Contains(inicial))
                            {
                                dt_procesos.Rows.Add();
                                dt_procesos.Rows[i][0] = row[0];
                                recuperar = true;
                                i++;
                            }

                            if (linea.Contains(fin))
                            {
                                dt_procesos.Rows.Add();
                                dt_procesos.Rows[i][0] = row[0];
                                recuperar = false;
                                i++;
                            }

                            if (recuperar && !linea.Contains(inicial))
                            {
                                dt_procesos.Rows.Add();
                                dt_procesos.Rows[i][0] = row[0];
                                recuperar = true;
                                i++;
                            }
                        }
                    }


                    //CREAMOS EL TREEVIEW QUE TENDRA TODOS LOS LOGS EN NODOS
                    int num = 0;
                    TreeView tvElementos = new TreeView();
                    tvElementos.Nodes.Add(num.ToString());

                    int numFilas = dt_procesos.Select("F1 like '%INICIA FLUJO%'").Count();
                    string[,] registros = new string[numFilas, 15];

                    for (int fila = 0; fila < dt_procesos.Rows.Count; fila++)
                    {
                        string line = dt_procesos.Rows[fila].ItemArray.GetValue(0).ToString();
                        if (line.Contains("INICIA FLUJO"))
                        {
                            fila++;

                            line = dt_procesos.Rows[fila].ItemArray.GetValue(0).ToString();

                            while (!line.Contains("FINALIZA FLUJO") && !line.Contains("INICIA FLUJO"))
                            {
                                //VALIDACION PARA EL FINAL DEL ARCHIVO (CASO NO TENGA 'FINALIZA FLUJO')
                                if (fila >= dt_procesos.Rows.Count)
                                    line = "FINALIZA FLUJO";
                                else
                                {
                                    line = dt_procesos.Rows[fila].ItemArray.GetValue(0).ToString();
                                    if (!line.Contains("FINALIZA FLUJO") && !line.Contains("INICIA FLUJO"))
                                    {
                                        tvElementos.Nodes[num].Nodes.Add(dt_procesos.Rows[fila].ItemArray.GetValue(0).ToString());
                                        fila++;
                                    }
                                }
                            }
                            num++;
                            tvElementos.Nodes.Add(num.ToString());
                            fila--;
                        }
                    }

                    arch.Delete();

                    TreeView tvPrueba = new TreeView();
                    //ELIMINAMOS LOS TICKET DEL LOG
                    for (int j = 0; j < tvElementos.Nodes.Count; j++)
                    {
                        foreach (TreeNode child in tvElementos.Nodes[j].Nodes)
                        {
                            if (child.Text.Contains("ACS_Ticket"))
                            {
                                tvElementos.Nodes[j].Remove();
                                j--;
                                break;
                            }
                        }
                    }
                    //END

                    //ELIMINAMOS LOS REGISTROS PROCESADOS (CASO ULTIMO ARCHIVO PROCESADO)
                    if (strUltimoRegistro != "")
                    {
                        for (int j = 0; j < tvElementos.Nodes.Count; j++)
                        {
                            if (existeErrorNodo(tvElementos.Nodes[j]))
                            {
                                string[] fecha = tvElementos.Nodes[j].Nodes[0].Text.Split(' ');
                                string FechaLog = Function.convertToFechaSQL(fecha[0]) + Function.convertToHoraSQL(fecha[1]);

                                if (String.Compare(FechaLog, strUltimoRegistro) <= 0)
                                {
                                    tvElementos.Nodes[j].Remove();
                                    j--;
                                }
                            }
                        }
                    }
                    //END

                    bool existe = false;
                    for (int j = 0; j < tvElementos.Nodes.Count; j++)
                    {
                        string error = "";
                        foreach (TreeNode child in tvElementos.Nodes[j].Nodes)
                        {
                            if (child.Text.Contains(ACS_Define.ERROR_TIME_OUT) ||
                                child.Text.Contains(ACS_Define.ERROR_TIME_OUT_) ||
                                child.Text.Contains(ACS_Define.ERROR_SERVICIO_ACCESOS) ||
                                child.Text.Contains(ACS_Define.ERROR_SERVICIO_ACCESOS_) ||
                                child.Text.Contains(ACS_Define.ERROR_SQL_EXCEPTION) ||
                                child.Text.Contains(ACS_Define.ERROR_LECTURA) ||
                                child.Text.Contains(ACS_Define.ERROR_TRAMA_VACIA) ||
                                child.Text.Contains(ACS_Define.DESTRABE_MOLINETE) ||
                                child.Text.Contains(ACS_Define.ERROR_NO_IDENTIFICADO))
                            {
                                error = child.Text;
                                existe = true;
                                break;
                            }
                            else
                                existe = false;
                        }

                        if (existe)
                            insertarRegistro(tvElementos.Nodes[j], error, archivo.Name);
                    }

                    //INSERTAMOS REGISTRO DEL ARCHIVO COMO PROCESADO SI NO EXISTE NINGUN ERRROR EN EL LOG
                    //if (!existe)
                    //{
                    //    if (strUltimoRegistro == "")
                    //    {
                    //        string[] fechaLog = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss:fff").Split(' ');
                    //        LogProcesados Obj_LogProcesados = new LogProcesados();
                    //        Obj_LogProcesados.StrCodEquipoMod = (string)Property.htProperty["COD_MOLINETE"];
                    //        Obj_LogProcesados.StrNomArchivo = archivo.Name;
                    //        Obj_LogProcesados.StrLogFechaMod = Function.convertToFechaSQL(fechaLog[0]);
                    //        Obj_LogProcesados.StrLogHoraMod = Function.convertToHoraSQL(fechaLog[1]);

                    //        Obj_Resolver.InsertarLogProcesados(Obj_LogProcesados);
                    //    }
                    //}

                    return dt_procesos;
                }
                else
                {
                    Obj_Util.EscribirLog(this, "Archivo " + archivo.Name + " no existe en el directorio");
                    return null;
                }
            }
            catch (Exception ex)
            {
                //Obj_Util.EscribirLog(this, ex.Message);
                throw ex; 
            }
        }

        //VALIDA SI EXISTE ALGUN TIPO DE ERROR EN EL NODO
        public bool existeErrorNodo(TreeNode nodoPadre)
        {
            bool existe = false;
            foreach (TreeNode child in nodoPadre.Nodes)
            {
                if (child.Text.Contains(ACS_Define.ERROR_TIME_OUT) ||
                         child.Text.Contains(ACS_Define.ERROR_TIME_OUT_) ||
                         child.Text.Contains(ACS_Define.ERROR_SERVICIO_ACCESOS) ||
                         child.Text.Contains(ACS_Define.ERROR_SERVICIO_ACCESOS_) ||
                         child.Text.Contains(ACS_Define.ERROR_SQL_EXCEPTION) ||
                         child.Text.Contains(ACS_Define.ERROR_LECTURA) ||
                         child.Text.Contains(ACS_Define.ERROR_TRAMA_VACIA) ||
                         child.Text.Contains(ACS_Define.DESTRABE_MOLINETE) ||
                         child.Text.Contains(ACS_Define.ERROR_NO_IDENTIFICADO))
                    existe = true;
            }

            return existe;
        }
        

        public void insertarRegistro(TreeNode nodo, string error, string sNomArchivo)
        {
            try
            {
                BoardingBcbpErr Obj_BoardingBcbpErr = new BoardingBcbpErr();
                LogProcesados Obj_LogProcesados = new LogProcesados();
                Compania Obj_Compania = new Compania();

                string Fecha_Error = "";
                string Cod_Molinete = (string)Property.htProperty["COD_MOLINETE"];
                string Dsc_Error = "";
                string Tip_Error = "";
                string Tip_Boarding = "";
                string Cod_Compania = "";
                string Num_Vuelo = "";
                string Fech_Vuelo = "";
                string Num_Asiento = "";
                string Nom_Pasajero = "";
                string Seccion_Error = "";

                //CONCATENAMOS EL LOG DE ERROR PARA EL CAMPO DE LA TABLA Tua_BoardingBcbpErr(Log_Error)
                StringBuilder sb = new StringBuilder();
                foreach (TreeNode node in nodo.Nodes)
                {
                    sb.Append(node.Text + "|\n");
                }

                //INSERTAMOS LOS NODOS QUES TENGAN EL TIPO ERROR_LECTURA - ERROR_NO_IDENTIFICADO
                if (error.Contains(ACS_Define.ERROR_LECTURA) || error.Contains(ACS_Define.ERROR_NO_IDENTIFICADO))
                {
                    string[] cadenas = nodo.Nodes[0].Text.Split(' ');
                    Fecha_Error = cadenas[0] + " " + cadenas[1];

                    if (error.Contains(ACS_Define.ERROR_LECTURA))
                    {
                        Dsc_Error = ACS_Define.ERROR_LECTURA;
                        Tip_Error = "E4";
                    }
                    else
                    {
                        if (error.Contains(ACS_Define.ERROR_NO_IDENTIFICADO))
                        {
                            bool errorPinpad = false;
                            foreach (TreeNode child in nodo.Nodes)
                            {
                                if (child.Text.Contains(ACS_Define.ERROR_PINPAD))
                                {
                                    errorPinpad = true;
                                    break;
                                }
                            }

                            if (!errorPinpad)
                            {
                                Dsc_Error = ACS_Define.ERROR_NO_IDENTIFICADO;
                                Tip_Error = "E8";
                            }
                            else
                            {
                                Dsc_Error = "Error en PinPad";
                                Tip_Error = "E5";
                            }
                        }
                        
                    }


                    Tip_Boarding = "-";
                    Cod_Compania = "-";
                    Num_Vuelo = "-";
                    Fech_Vuelo = "-";
                    Num_Asiento = "-";
                    Nom_Pasajero = "-";
                    Seccion_Error = sb.ToString();

                    DataRow row = dtRegistros.NewRow();
                    row["Fech_Error"] = Fecha_Error;
                    row["Cod_Molinete"] = Cod_Molinete;
                    row["Dsc_Error"] = Dsc_Error;
                    row["Tip_Error"] = Tip_Error;
                    row["Tip_Boarding"] = Tip_Boarding;
                    row["Cod_Compania"] = Cod_Compania;
                    row["Num_Vuelo"] = Num_Vuelo;
                    row["Fech_Vuelo"] = Fech_Vuelo;
                    row["Num_Asiento"] = Num_Asiento;
                    row["Nom_Pasajero"] = Nom_Pasajero;
                    row["Seccion_Error"] = Seccion_Error;

                    dtRegistros.Rows.Add(row);

                    //INSERTAMOS EL REGISTRO
                    Obj_BoardingBcbpErr.SDscTramaBcbp = Dsc_Error;
                    Obj_BoardingBcbpErr.Cod_Tip_Error = Tip_Error;
                    Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso;
                    Obj_BoardingBcbpErr.StrLogFechaMod = cadenas[0];
                    Obj_BoardingBcbpErr.StrLogHoraMod = cadenas[1];
                    Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngAuto;
                    Obj_BoardingBcbpErr.StrCodEquipoMod = Cod_Molinete;
                    Obj_BoardingBcbpErr.StrTipBoarding = Tip_Boarding;
                    Obj_BoardingBcbpErr.StrCodCompania = Cod_Compania;
                    Obj_BoardingBcbpErr.StrNumVuelo = Num_Vuelo;
                    Obj_BoardingBcbpErr.StrFchVuelo = Fech_Vuelo;
                    Obj_BoardingBcbpErr.StrNumAsiento = Num_Asiento;
                    Obj_BoardingBcbpErr.StrNomPasajero = Nom_Pasajero;
                    Obj_BoardingBcbpErr.StrLogError = Seccion_Error;

                    //INSERTAMOS EL REGISTRO 
                    Obj_Resolver.InsertarBoardingBcbpErr(Obj_BoardingBcbpErr);

                    //ACTUALIZAMOS EL REGISTRO EN Tua_LogProcesado
                    Obj_LogProcesados.StrCodEquipoMod = Cod_Molinete;
                    Obj_LogProcesados.StrNomArchivo = sNomArchivo;
                    Obj_LogProcesados.StrLogFechaMod = Function.convertToFechaSQL(cadenas[0]);
                    Obj_LogProcesados.StrLogHoraMod = Function.convertToHoraSQL(cadenas[1]);

                    Obj_Resolver.InsertarLogProcesados(Obj_LogProcesados);

                }
                else
                {
                    bool existeError = false;
                    if (error.Contains(ACS_Define.ERROR_TIME_OUT) || error.Contains(ACS_Define.ERROR_TIME_OUT_))
                    {
                        Dsc_Error = ACS_Define.ERROR_TIME_OUT;
                        Tip_Error = "E1";
                        existeError = true;
                    }
                    else
                    {
                        if (error.Contains(ACS_Define.ERROR_SQL_EXCEPTION))
                        {
                            Dsc_Error = ACS_Define.ERROR_SQL_EXCEPTION;
                            Tip_Error = "E3";
                            existeError = true;
                        }
                        else
                        {
                            if (error.Contains(ACS_Define.ERROR_TRAMA_VACIA))
                            {
                                Dsc_Error = ACS_Define.ERROR_TRAMA_VACIA;
                                Tip_Error = "E6";
                                existeError = true;
                            }
                            else
                            {
                                if (error.Contains(ACS_Define.ERROR_SERVICIO_ACCESOS) || error.Contains(ACS_Define.ERROR_SERVICIO_ACCESOS_))
                                {
                                    Dsc_Error = "Error de Servicio Accesos";
                                    Tip_Error = "E2";
                                    existeError = true;
                                }
                                else
                                {
                                    if (error.Contains(ACS_Define.DESTRABE_MOLINETE))
                                    {
                                        Dsc_Error = "Destrabe de Molinete";
                                        Tip_Error = "E7";
                                        existeError = true;
                                    }
                                }
                            }
                        }
                    }

                    if (existeError)
                    {
                        string[] cadenas = nodo.Nodes[0].Text.Split(' ');
                        Fecha_Error = cadenas[0] + " " + cadenas[1];

                        //VALIDACION EN CASO NO EXISTA LA TRAMA
                        if (nodo.Nodes[0].Text.LastIndexOf("Trama Recibida:") != -1)
                        {
                            int indice = nodo.Nodes[0].Text.LastIndexOf("Trama Recibida:") + 17;

                            //VALIDACION (CASO TRAMA VACIA)
                            string sTrama = "";
                            if (indice >= nodo.Nodes[0].Text.Length)
                            {
                                Tip_Boarding = "-";
                            }
                            else
                            {
                                int longitud = nodo.Nodes[0].Text.Substring(indice).TrimStart(' ').Length;

                                //IDENTIFICAMOS EL TIPO DE BOARDING 1D-2D
                                if (longitud == 17 || longitud == 18)
                                    Tip_Boarding = "1D";
                                else
                                    Tip_Boarding = "2D";

                                //RECUPERAMOS LA TRAMA
                                sTrama = nodo.Nodes[0].Text.Substring(indice) + "";

                                //ELIMINAMOS CARACTERES EXTRAÑOS AL INICIO DE LA TRAMA
                                sTrama = sTrama.Replace("", "");

                                if (nodo.Nodes[0].Text.Contains(""))
                                    sTrama = " " + sTrama.TrimStart(' ');
                                else
                                    sTrama = "  " + sTrama.TrimStart(' ');
                            }
                            Reader reader = new Reader();

                            Hashtable ht = reader.ParseString_Boarding(sTrama);

                            //VERIFICAMOS SI LA TRAMA ES CORRECTA
                            if (ht == null)
                            {
                                Tip_Boarding = "-";
                                Cod_Compania = "-";
                                Num_Vuelo = "-";
                                Fech_Vuelo = "-";
                                Num_Asiento = "-";
                                Nom_Pasajero = "-";
                                Seccion_Error = sb.ToString();

                                DataRow row = dtRegistros.NewRow();
                                row["Fech_Error"] = Fecha_Error;
                                row["Cod_Molinete"] = Cod_Molinete;
                                row["Dsc_Error"] = Dsc_Error;
                                row["Tip_Error"] = Tip_Error;
                                row["Tip_Boarding"] = Tip_Boarding;
                                row["Cod_Compania"] = Cod_Compania;
                                row["Num_Vuelo"] = Num_Vuelo;
                                row["Fech_Vuelo"] = Fech_Vuelo;
                                row["Num_Asiento"] = Num_Asiento;
                                row["Nom_Pasajero"] = Nom_Pasajero;
                                row["Seccion_Error"] = Seccion_Error;

                                dtRegistros.Rows.Add(row);

                                //INSERTAMOS EL REGISTRO
                                Obj_BoardingBcbpErr.SDscTramaBcbp = Dsc_Error;
                                Obj_BoardingBcbpErr.Cod_Tip_Error = Tip_Error;
                                Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso;
                                Obj_BoardingBcbpErr.StrLogFechaMod = cadenas[0];
                                Obj_BoardingBcbpErr.StrLogHoraMod = cadenas[1];
                                Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngAuto;
                                Obj_BoardingBcbpErr.StrCodEquipoMod = Cod_Molinete;
                                Obj_BoardingBcbpErr.StrTipBoarding = Tip_Boarding;
                                Obj_BoardingBcbpErr.StrCodCompania = Cod_Compania;
                                Obj_BoardingBcbpErr.StrNumVuelo = Num_Vuelo;
                                Obj_BoardingBcbpErr.StrFchVuelo = Fech_Vuelo;
                                Obj_BoardingBcbpErr.StrNumAsiento = Num_Asiento;
                                Obj_BoardingBcbpErr.StrNomPasajero = Nom_Pasajero;
                                Obj_BoardingBcbpErr.StrLogError = Seccion_Error;

                                //INSERTAMOS EL REGISTRO 
                                Obj_Resolver.InsertarBoardingBcbpErr(Obj_BoardingBcbpErr);

                                //ACTUALIZAMOS EL REGISTRO EN Tua_LogProcesado
                                Obj_LogProcesados.StrCodEquipoMod = Cod_Molinete;
                                Obj_LogProcesados.StrNomArchivo = sNomArchivo;
                                Obj_LogProcesados.StrLogFechaMod = Function.convertToFechaSQL(cadenas[0]);
                                Obj_LogProcesados.StrLogHoraMod = Function.convertToHoraSQL(cadenas[1]);

                                Obj_Resolver.InsertarLogProcesados(Obj_LogProcesados);
                            }
                            else
                            {
                                try
                                {
                                    reader.ParseHashtable(ht);
                                    string strAirDesig = (String)ht[LAP.TUUA.CONVERSOR.Define.Compania];

                                    Obj_Resolver.CrearDAOCompania();

                                    foreach (Compania objCompania in Obj_Resolver.listarCompania())
                                    {
                                        if (strAirDesig != null && (objCompania.SCodEspecialCompania.Trim().Equals(strAirDesig.Trim()) || objCompania.SCodCompania.Trim().Equals(strAirDesig.Trim())))
                                        {
                                            Cod_Compania = objCompania.SCodCompania;
                                            break;
                                        }
                                    }

                                    String fechaVueloAux = (String)ht[LAP.TUUA.CONVERSOR.Define.FechaVuelo];
                                    Num_Vuelo = (String)ht[LAP.TUUA.CONVERSOR.Define.NroVuelo];
                                    Num_Asiento = (String)ht[LAP.TUUA.CONVERSOR.Define.Asiento];
                                    Nom_Pasajero = (String)ht[LAP.TUUA.CONVERSOR.Define.Persona];

                                    Fech_Vuelo = Obj_Util.ConvertirJulianoCalendario(Int32.Parse(fechaVueloAux));
                                    Seccion_Error = sb.ToString();

                                    DataRow row = dtRegistros.NewRow();
                                    row["Fech_Error"] = Fecha_Error;
                                    row["Cod_Molinete"] = Cod_Molinete;
                                    row["Dsc_Error"] = Dsc_Error;
                                    row["Tip_Error"] = Tip_Error;
                                    row["Tip_Boarding"] = Tip_Boarding;
                                    row["Cod_Compania"] = Cod_Compania;
                                    row["Num_Vuelo"] = Num_Vuelo;
                                    row["Fech_Vuelo"] = Fech_Vuelo;
                                    row["Num_Asiento"] = Num_Asiento;
                                    row["Nom_Pasajero"] = Nom_Pasajero;
                                    row["Seccion_Error"] = Seccion_Error;

                                    dtRegistros.Rows.Add(row);
                                }
                                catch
                                {
                                    //SOLO SI EXISTE ERRORES EN TRAMA (TRAMA ILEGIBLE)
                                    Tip_Boarding = "-";
                                    Cod_Compania = "-";
                                    Num_Vuelo = "-";
                                    Fech_Vuelo = "-";
                                    Num_Asiento = "-";
                                    Nom_Pasajero = "-";
                                    Seccion_Error = sb.ToString();

                                    DataRow row = dtRegistros.NewRow();
                                    row["Fech_Error"] = Fecha_Error;
                                    row["Cod_Molinete"] = Cod_Molinete;
                                    row["Dsc_Error"] = Dsc_Error;
                                    row["Tip_Error"] = Tip_Error;
                                    row["Tip_Boarding"] = Tip_Boarding;
                                    row["Cod_Compania"] = Cod_Compania;
                                    row["Num_Vuelo"] = Num_Vuelo;
                                    row["Fech_Vuelo"] = Fech_Vuelo;
                                    row["Num_Asiento"] = Num_Asiento;
                                    row["Nom_Pasajero"] = Nom_Pasajero;
                                    row["Seccion_Error"] = Seccion_Error;

                                    dtRegistros.Rows.Add(row);
                                }

                                //INSERTAMOS EL REGISTRO
                                Obj_BoardingBcbpErr.SDscTramaBcbp = Dsc_Error;
                                Obj_BoardingBcbpErr.Cod_Tip_Error = Tip_Error;
                                Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso;
                                Obj_BoardingBcbpErr.StrLogFechaMod = cadenas[0];
                                Obj_BoardingBcbpErr.StrLogHoraMod = cadenas[1];
                                Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngAuto;
                                Obj_BoardingBcbpErr.StrCodEquipoMod = Cod_Molinete;
                                Obj_BoardingBcbpErr.StrTipBoarding = Tip_Boarding;
                                Obj_BoardingBcbpErr.StrCodCompania = Cod_Compania;
                                Obj_BoardingBcbpErr.StrNumVuelo = Num_Vuelo;
                                Obj_BoardingBcbpErr.StrFchVuelo = Fech_Vuelo;
                                Obj_BoardingBcbpErr.StrNumAsiento = Num_Asiento;
                                Obj_BoardingBcbpErr.StrNomPasajero = Nom_Pasajero;
                                Obj_BoardingBcbpErr.StrLogError = Seccion_Error;

                                //INSERTAMOS EL REGISTRO 
                                Obj_Resolver.InsertarBoardingBcbpErr(Obj_BoardingBcbpErr);

                                //ACTUALIZAMOS EL REGISTRO EN Tua_LogProcesado
                                Obj_LogProcesados.StrCodEquipoMod = Cod_Molinete;
                                Obj_LogProcesados.StrNomArchivo = sNomArchivo;
                                Obj_LogProcesados.StrLogFechaMod = Function.convertToFechaSQL(cadenas[0]);
                                Obj_LogProcesados.StrLogHoraMod = Function.convertToHoraSQL(cadenas[1]);

                                Obj_Resolver.InsertarLogProcesados(Obj_LogProcesados);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Obj_Util.EscribirLog(this, ex.Message);
                throw ex;
            }
        }
    }
}

