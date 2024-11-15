"""
Autor: Daniel Castillo
Permite sincronizar la informacion del registro de pasajeros en transito, ademas la información de vuelos
Permite sincronizar la informacion de llaves de destrabe
"""
import mysql.connector
from dao.LAP_Dao import Lap_Dao
import logging
import configuracionParametros
import datetime
import time
import json
from util.restClient import RestClient
from model.registroTransito import RegistroTransito

#configurar log
sformato = '%(asctime)s - %(levelname)s - %(message)s'
log_format=logging.Formatter(sformato)

logger = logging.getLogger(__name__)
logger.setLevel(logging.DEBUG)

#esto para que cuando cambie el dia se cree un nuevo archivo
def registrarLogHandler():
    fch_dia = datetime.datetime.today()
    nombre_log = 'log/LAP_Sync_' + str(datetime.date(year=fch_dia.year, month=fch_dia.month, day=fch_dia.day)) +'.log'
    logging.basicConfig(filename=nombre_log, format=sformato)

#parametros BD
BD = configuracionParametros.datosBD(logger)
_host = BD[0]
_bd = BD[3]
_user = BD[1]
_clave = BD[2]
_codMolinete = configuracionParametros.NombrePuerta(logger)

#url web services
url_registrar_transito = configuracionParametros.urlPostDatos(logger)
url_obtener_vuelos_llegada = configuracionParametros.urlServiceVuelosLlegadas(logger)
url_obtener_vuelos_salida = configuracionParametros.urlServiceVuelosSalidas(logger)

dao = Lap_Dao(_host, _bd, _user, _clave)

restCLient = RestClient("",None,None)

def sincronizarDatosTransito():
    print("Iniciando sincronizacion de datos de transito")
    #1. obtener registros no sinconizados
    listaRegistros = dao.EjecutarQueryDict("select * from lap_transito_datos where Std_subida = 'NO'")
    #2. sincronizar registros
    for reg in listaRegistros:
        try:
            if reg!=None:
                tran = RegistroTransito(reg['Num_Vuelo_Origen'],reg['Fch_Vuelo_Origen'],reg['Num_Vuelo_Destino'],reg['Fch_Vuelo_Destino'],reg['Cod_Molinete'],reg['Trama_Origen'],reg['Trama_Destino'])
                body = json.dumps(tran.__dict__)
                #print("enviar a {0} el valor {1}".format(url_registrar_transito, body))
                resp=restCLient.post(url_registrar_transito,body)
                if resp.status_code ==200 or resp.status_code == 201:
                    bodyResp = json.loads(resp.text)
                    if(bodyResp['respuesta']=='ok'):
                        #actualizar en base de datos
                        strFecha = reg['fch_hora_intento'].strftime('%Y-%m-%d %H:%M:%S')
                        dao.Ejecutar("update lap_transito_datos set std_subida = 'SI' where fch_hora_intento = '{0}'".format(strFecha))
                        print("OK")
                        logger.info("info: se sincronizo el registro:{0}".format(body))
                    elif(bodyResp['respuesta']=='error'):
                        logger.error(resp.text)
                        print(bodyResp['mensaje'])
                else:
                    logger.error("No se pudo enviar el registro")

        except Exception as ex:
            logger.error(ex)
            print("ocurrio un error inesperado")

        #3. eliminar registros ya sincronizados
    print("Eliminar Datos Antiguos")
    dao.Ejecutar("delete from lap_transito_datos where fch_hora_intento < curdate() and Std_subida = 'SI'")

def sincronizarVuelosLlegada():
    #1. obtener vuelos de servicio web
    try:
        logger.info("Iniciando Sincronización de vuelos de llegada")
        resp = restCLient.get(url_obtener_vuelos_llegada,None)       
        if resp.status_code ==200 or resp.status_code == 201:
            lsVuelosLlegada = json.loads(resp.text)
            if lsVuelosLlegada != None and len(lsVuelosLlegada)>0:
                for vuelo in lsVuelosLlegada:
                    try:
                        #2. insertar / actualizar vuelos
                        #print(vuelo["Num_Vuelo"]+" " +vuelo["Fch_Prog"])
                        vl_aux = dao.EjecutarQueryDict("select * from lap_transito_vuelos_llegadas where Num_Vuelo = '{0}' and Fch_Prog = '{1}';".format(vuelo["Num_Vuelo"],vuelo["Fch_Prog"]))
                        if vl_aux!= None and len(vl_aux)>0:
                            #actualizar
                            queryUpdate = """
                            UPDATE lap_transito_vuelos_llegadas
                            SET
                            Hor_Prog = '{0}',Cod_Compania = '{1}',Tip_Vuelo = '{2}',Fch_Real = '{3}',
                            Fch_Est = '{4}',Cod_Proc_Dest = '{5}',Cod_Escala = '{6}',Dsc_Aerolinea = '{7}',
                            Dsc_Proc_Destino = '{8}',Dsc_Estado = '{9}',Cod_Iata = '{10}'
                            WHERE Num_Vuelo = '{11}' AND Fch_Prog = '{12}';
                            """.format(vuelo["Hor_Prog"],vuelo["Cod_Compania"],vuelo["Tip_Vuelo"],vuelo["Fch_Real"],
                            vuelo["Fch_Est"],vuelo["Cod_Proc_Dest"],vuelo["Cod_Escala"],vuelo["Dsc_Aerolinea"],vuelo["Dsc_Proc_Destino"],
                            vuelo["Dsc_Estado"],vuelo["Cod_Iata"],vuelo["Num_Vuelo"],vuelo["Fch_Prog"])
                            #logger.info(queryUpdate)
                            dao.Ejecutar(queryUpdate)
                        else:
                            #Insertar
                            query = """
                            INSERT INTO lap_transito_vuelos_llegadas
                            (Num_Vuelo,Fch_Prog,Hor_Prog,Tip_Operacion,Cod_Compania,
                            Tip_Vuelo,Fch_Real,Fch_Est,Cod_Proc_Dest,Cod_Escala,Cod_Gate,
                            Tip_Gate_Terminal,Cod_Faja,Cod_Mostrador,Dsc_Aerolinea,
                            Dsc_Proc_Destino,Log_Fch_Creacion,Log_Fch_Modificacion,Dsc_Estado,Cod_Iata,Nro_Vuelo,num)
                            VALUES
                            ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')
                            """.format(vuelo["Num_Vuelo"],vuelo["Fch_Prog"],vuelo["Hor_Prog"],vuelo["Tip_Operacion"],vuelo["Cod_Compania"],
                            vuelo["Tip_Vuelo"],vuelo["Fch_Real"],vuelo["Fch_Est"],vuelo["Cod_Proc_Dest"],vuelo["Cod_Escala"],
                            vuelo["Cod_Gate"],vuelo["Tip_Gate_Terminal"],vuelo["Cod_Faja"],vuelo["Cod_Mostrador"],vuelo["Dsc_Aerolinea"],
                            vuelo["Dsc_Proc_Destino"],vuelo["Log_Fch_Creacion"],vuelo["Log_Fch_Modificacion"],vuelo["Dsc_Estado"],vuelo["Cod_Iata"],vuelo["Nro_Vuelo"],"1")
                            #logger.info(query)
                            dao.Ejecutar(query)
                        #3. eliminar vuelos antiguos
                        dao.Ejecutar("delete from lap_transito_vuelos_llegadas where Hor_Prog < curdate()")  
                    except Exception as ex:
                        logger.error(ex)

        else:
            logger.error("no se pudo obtener la informacion del servicio web: {0}".format(url_obtener_vuelos_llegada))
        logger.info("Fin de sincronización de vuelos de llegada")
    except Exception as ex:
        logger.error(ex)
    
def sincronizarVuelosSalida():
    #1. obtener vuelos de servicio web
    try:
        logger.info("Iniciando Sincronización de vuelos de Salida")
        resp = restCLient.get(url_obtener_vuelos_salida,None)       
        if resp.status_code ==200 or resp.status_code == 201:
            lsVuelosSalida = json.loads(resp.text)
            if lsVuelosSalida != None and len(lsVuelosSalida)>0:
                for vuelo in lsVuelosSalida:
                    try:
                        #2. insertar / actualizar vuelos
                        #print(vuelo["Num_Vuelo"]+" " +vuelo["Fch_Prog"])
                        vl_aux = dao.EjecutarQueryDict("select * from lap_transito_vuelos_salidas where Num_Vuelo = '{0}' and Fch_Prog = '{1}';".format(vuelo["Num_Vuelo"],vuelo["Fch_Prog"]))
                        if vl_aux!= None and len(vl_aux)>0:
                            #actualizar
                            queryUpdate = """
                            UPDATE lap_transito_vuelos_salidas
                            SET
                            Hor_Prog = '{0}',Cod_Compania = '{1}',Tip_Vuelo = '{2}',Fch_Real = '{3}',
                            Fch_Est = '{4}',Cod_Proc_Dest = '{5}',Cod_Escala = '{6}',Dsc_Aerolinea = '{7}',
                            Dsc_Proc_Destino = '{8}',Dsc_Estado = '{9}',Cod_Iata = '{10}'
                            WHERE Num_Vuelo = '{11}' AND Fch_Prog = '{12}';
                            """.format(vuelo["Hor_Prog"],vuelo["Cod_Compania"],vuelo["Tip_Vuelo"],vuelo["Fch_Real"],
                            vuelo["Fch_Est"],vuelo["Cod_Proc_Dest"],vuelo["Cod_Escala"],vuelo["Dsc_Aerolinea"],vuelo["Dsc_Proc_Destino"],
                            vuelo["Dsc_Estado"],vuelo["Cod_Iata"],vuelo["Num_Vuelo"],vuelo["Fch_Prog"])
                            #logger.info(queryUpdate)
                            dao.Ejecutar(queryUpdate)
                        else:
                            #Insertar
                            query = """
                            INSERT INTO lap_transito_vuelos_salidas
                            (Num_Vuelo,Fch_Prog,Hor_Prog,Tip_Operacion,Cod_Compania,
                            Tip_Vuelo,Fch_Real,Fch_Est,Cod_Proc_Dest,Cod_Escala,Cod_Gate,
                            Tip_Gate_Terminal,Cod_Faja,Cod_Mostrador,Dsc_Aerolinea,
                            Dsc_Proc_Destino,Log_Fch_Creacion,Log_Fch_Modificacion,Dsc_Estado,Cod_Iata,Nro_Vuelo,num)
                            VALUES
                            ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')
                            """.format(vuelo["Num_Vuelo"],vuelo["Fch_Prog"],vuelo["Hor_Prog"],vuelo["Tip_Operacion"],vuelo["Cod_Compania"],
                            vuelo["Tip_Vuelo"],vuelo["Fch_Real"],vuelo["Fch_Est"],vuelo["Cod_Proc_Dest"],vuelo["Cod_Escala"],
                            vuelo["Cod_Gate"],vuelo["Tip_Gate_Terminal"],vuelo["Cod_Faja"],vuelo["Cod_Mostrador"],vuelo["Dsc_Aerolinea"],
                            vuelo["Dsc_Proc_Destino"],vuelo["Log_Fch_Creacion"],vuelo["Log_Fch_Modificacion"],vuelo["Dsc_Estado"],vuelo["Cod_Iata"],vuelo["Nro_Vuelo"],"1")
                            #logger.info(query)
                            dao.Ejecutar(query)
                        #3. eliminar vuelos antiguos
                        dao.Ejecutar("delete from lap_transito_vuelos_salidas where Hor_Prog < curdate()")  
                    except Exception as ex:
                        logger.error(ex)

        else:
            logger.error("no se pudo obtener la informacion del servicio web: {0}".format(url_obtener_vuelos_llegada))
        logger.info("Fin de sincronización de vuelos de Salida")
    except Exception as ex:
        logger.error(ex)

def sincronizarLlavesDestrabe():
    #obtener llaves de destrabe
    #eliminar llaves e insertar nuevas    
    pass

while True:
    try:
        registrarLogHandler()
        #1. sincronizar registros de vuelos
        sincronizarVuelosLlegada()  
        sincronizarVuelosSalida()
        #2. sincronizar llaves de destrabe
        sincronizarLlavesDestrabe()
        #3. sincronizar registros de transito
        sincronizarDatosTransito()
    
    except Exception as ex:
        logger.error(ex)
    finally:
        time.sleep(30)
        