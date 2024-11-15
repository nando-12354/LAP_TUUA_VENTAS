import time
import generico
import logging
import sys , os
import datetime
import configuracionParametros

fch_dia = datetime.datetime.today()
nombre_log = 'log/traza_SV_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
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
		nombre_log = 'traza_SV_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
		file_handler = logging.FileHandler(nombre_log)
		logger.addHandler(file_handler)		
		#nombre_log = 'traza_SV_' + str(datetime.date(year=datetime.date.today().year,month=datetime.date.today().month,day=datetime.date.today().day)) +'.log'
		#logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
		rsp = generico.internet_on(logger)
		if (rsp):
			estado = generico.estado_webservice(logger)
			logger.info("Info: "+str(datetime.datetime.today())+" entro a la conexion con la red")
			if (estado[1] == "ok"):
				#logger.info("Info: "+str(datetime.datetime.today())+" entro a la conexion de web service")
				print("ENTRO EN EL OK")
				generico.sincronizacionVueloSalida(logger)
				generico.sincronizacionVuelollegada(logger)
				time.sleep(1)
		else:
			print("No hay conexion")
	except Exception as e:
		print(e.args)
		print(str(sys.exc_info()[2].tb_lineno))
		logger.info("Error: "+str(datetime.datetime.today())+" LAP SV "+str(e)+"  "+str(sys.exc_info()[2].tb_lineno))
	except:
		print("Error")
		logger.info("Error: "+str(datetime.datetime.today())+" LAP SV "+str(sys.exc_info()[2].tb_lineno))
	time.sleep(5)