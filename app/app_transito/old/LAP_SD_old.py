
import time
import logging
import sys ,os
import configuracionParametros
import datetime
import generico

fch_dia = datetime.datetime.today()
nombre_log = 'log/traza_SD_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
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
		nombre_log = 'traza_SD_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
		file_handler = logging.FileHandler(nombre_log)
		logger.addHandler(file_handler)	

		estado = generico.estado_webservice(logger)
		if (estado[1] == "ok"):
			#logger.info("Info: "+str(datetime.datetime.today())+" entro a la conexion de web service")
			print("ENTRO AQUI")
			time.sleep(1)
			generico.sincronizacionBD(logger)
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" LAP SD "+str(e)+"  "+str(sys.exc_info()[2].tb_lineno))
		print("Error")
	time.sleep(5)
