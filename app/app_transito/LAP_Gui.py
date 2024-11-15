import datetime
import logging
import eel
from dao.LAP_Dao import Lap_Dao
#from controlador import controlador
import configuracionParametros
import xmlrpc.client as xmlrpclib
import time

#registrar logger
sformato = '%(asctime)s - %(levelname)s - %(message)s'
log_format=logging.Formatter(sformato)
logger = logging.getLogger(__name__)
logger.setLevel(logging.DEBUG)

#funciones para controlar puerta

direccion = "http://192.168.0.200:8081/"
proxycon = xmlrpclib.ServerProxy(direccion)

def abrir():
   proxycon.SetModeAEA("O")

def desbloqueo(): # se pone estado normal de la puerta
    abrir()
    data = proxycon.SetModeAEA("E")
    print(data)
    
def bloqueo():
    data = proxycon.SetModeAEA("L")
    print(data)
    
def getStatus():
    data = proxycon.GetSetAEA("SQ")
    print(data)
    return data

def pase():
    stat = getStatus()
    if stat[1] == "L":
        desbloqueo()
    time.sleep(3)
    data = proxycon.GetSetAEA("CC")
    print(data)
    
def restablecer():
    stat = getStatus()
    if stat[1] == "L":
        desbloqueo()
    if stat[1] == "O":
        bloqueo()
        time.sleep(2)
        desbloqueo()

#fin control puerta

#esto para que cuando cambie el dia se cree un nuevo archivo
def registrarLogHandler():
    fch_dia = datetime.datetime.today()
    nombre_log = 'log/LAP_Gui_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
    logging.basicConfig(filename=nombre_log,format=sformato)
# fin registro de logger

#parametros BD
BD = configuracionParametros.datosBD(logger)
_host = BD[0]
_bd = BD[3]
_user = BD[1]
_clave = BD[2]
_codMolinete = configuracionParametros.NombrePuerta(logger)

#ESTABLECER TRUE SI LA PUERTA ESTA CONECTADA
_puertaConectada = True

#fin parametros BD

dao = Lap_Dao(_host,_bd,_user,_clave)
#control = controlador(configuracionParametros.direccionControlador(logger))


@eel.expose
def suma(a, b):
    resp = int(a) + int(b)
    print(resp)
    return resp

@eel.expose
def obtenerVuelosLlegada():
    registrarLogHandler()
    resp = ""
    try:        
        query ="select * from lap_transito_vuelos_llegadas where Hor_Prog >=  date_sub(now(), interval 2 hour) and Hor_Prog <= now() and Dsc_Estado = 'ATERRIZO' order by Hor_Prog desc"
        resp = dao.EjecutarQueryJson(query)
        #logger.info(resp)
        logger.info("Consulta de vuelos ejecutada")
    except Exception as ex:
        print(ex)
        logger.error(ex)
    return resp

@eel.expose
def obtenerVuelosSalidaPorAerolinea(iata):
    registrarLogHandler()
    resp = ""
    try:        
        query = """select * from lap_transito_vuelos_salidas where Cod_Iata = '{0}'
        and Hor_Prog >=  now() and Hor_Prog <= date_add(now(), interval 8 hour) and Dsc_Estado <> 'FIN EMBARQ'
        order by Hor_Prog asc; 
        """.format(iata)
        resp = dao.EjecutarQueryJson(query)
        logger.info("Consulta de vuelos ejecutada")
    except Exception as ex:
        print(ex)
        logger.error(ex)
    return resp

@eel.expose
def obtenerAerolineasSalida():
    registrarLogHandler()
    resp = ""
    try:
        query = """select distinct Cod_Iata, Dsc_Aerolinea from lap_transito_vuelos_salidas 
        where Hor_Prog >= now() and Hor_Prog <= date_add(now(), interval 8 hour) and Dsc_Estado <> 'FIN EMBARQ'
        order by Dsc_Aerolinea"""
        resp = dao.EjecutarQueryJson(query)
    except Exception as ex:
        print(ex)
        logger.error(ex)
    return resp
@eel.expose
def insertarRegistroTransito(Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,Trama_Origen,Trama_Destino):
    registrarLogHandler()
    resp = None
    fecha = datetime.datetime.now()
    str_fecha = fecha.strftime('%Y-%m-%d %H:%M:%S')
    print('vuelo orig: {0} vuelo dest; {1}'.format(Num_Vuelo_Origen,Num_Vuelo_Destino))
    try:        
        query ="""INSERT INTO lap_transito_datos (Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,
        Cod_Molinete,Trama_Origen,Trama_Destino,Std_subida,fch_hora_intento) 
        VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');""".format(Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,_codMolinete,Trama_Origen,Trama_Destino,"NO",str_fecha)
        dao.Ejecutar(query)
        resp = "OK"   

        #abrir puerta
        if _puertaConectada:
            pase()
            #control.abrir(logger)
            #control.verifica_abertura_puerta(logger)

    except Exception as ex:
        print(ex)
        logger.error(ex)
    return resp

#reestablecer puerta
print("reestablecer puerta")
restablecer()
    
eel.init('gui')
#eel.start('inicio.html',mode="chrome", port=8880, cmdline_args=['--kiosk','--kiosk-printing', '--start-fullscreen','--chrome-frame'])
eel.start('inicio.html', mode="chrome", port=8880, cmdline_args=['--kiosk', '--kiosk-printing', '--start-fullscreen'])

