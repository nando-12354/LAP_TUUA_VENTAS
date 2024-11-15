# Operacion Borrado de Dato : borra los datos de la base de datos que ya pasaron un tiempo especifico
import time
import datetime
import generico
import configuracionParametros
import logging
import sys , os

fch_dia = datetime.datetime.today()
nombre_log = 'log/traza_BD_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
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
		nombre_log = 'traza_BD_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
		file_handler = logging.FileHandler(nombre_log)
		logger.addHandler(file_handler)	

		dtm_dia = configuracionParametros.horaBorradoCaducados(logger)
		for x in range(2):
			dtm_hora_actual = datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
			dtm_hora_actual2 = datetime.time(dtm_hora_actual.hour, dtm_hora_actual.minute, 0)
			if (dtm_dia != dtm_hora_actual2):
				generico.Borrar_lista_caducados(logger)
				#logger.info("Info: "+str(datetime.datetime.today())+" Se borro la informacion caducada")
			time.sleep(1)
	except Exception as e:
		print(e)
		logger.info("Error: "+str(datetime.datetime.today())+" LAP BD "+str(e)+"  "+str(sys.exc_info()[2].tb_lineno))
	except:
		print("Error")
		logger.info("Error: "+str(datetime.datetime.today())+" LAP BD "+str(sys.exc_info()[2].tb_lineno))
	time.sleep(10)