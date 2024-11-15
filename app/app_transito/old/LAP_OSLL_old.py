# Operacion Sincronizacion de LLave : sincroniza las llaves de destrabe dadas , para tener de manera constante la ifnormacion de llaves activadas
import time
import generico
import logging
import sys , os
import datetime
import configuracionParametros

fch_dia = datetime.datetime.today()
nombre_log = 'log/traza_OSLL_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
logger = logging.getLogger(__name__)
logger.setLevel(logging.DEBUG)
file_handler = logging.FileHandler(nombre_log)
logger.addHandler(file_handler)
logger.warning("Warning: Inicio  :"+ str(fch_dia))
logger.info("Info: "+str(fch_dia) + " ")

while(1):
	try:
		handler2 = logger.handlers[0]
		logger.removeHandler(handler2)
		fch_dia = datetime.datetime.today()
		nombre_log = 'traza_OSLL_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
		file_handler = logging.FileHandler(nombre_log)
		logger.addHandler(file_handler)	

		estado = generico.estado_webservice(logger)
		if (estado[1] == "ok"):
			#logger.info("Info: "+str(datetime.datetime.today())+" entro a la conexion de web service")
			generico.sincronizacionLlaveDestrabe(logger)
			time.sleep(5)
	except Exception as e:
		print(e.args)
		print(str(sys.exc_info()[2].tb_lineno))
		logger.info("Error: "+str(datetime.datetime.today())+"Error en LAP_OSLL "+str(e)+" "+str(sys.exc_info()[2].tb_lineno))
	except:
		print("error")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en LAP_OSLL "+str(sys.exc_info()[2].tb_lineno))
	time.sleep(2)