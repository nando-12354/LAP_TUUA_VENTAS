# Operacion Borrado de Dato : borra los datos de la base de datos que ya pasaron un tiempo especifico
import time
import datetime
import generico
import configuracionParametros
from ticket import ticket
import logging
import sys
import os

dia=datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
nombre_log = 'traza_OBD__' + str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) +'.log'
logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
logging.warning("Inicio LAP_SP_OBD :"+ str(datetime.date(year=dia.year,month=dia.month,day=dia.day)));
logging.info(str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) + " Conectado LAP_SP_OBD");
while(1):
	try:
		fch_dia = generico.diahora()
		nombre_log = 'traza_OBD__' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
		logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
		dtm_dia = configuracionParametros.horaBorradoCaducados()
		for x in range(2):
			dtm_hora_actual = datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
			dtm_hora_actual2 = datetime.time(dtm_hora_actual.hour, dtm_hora_actual.minute, 0)
			if (dtm_dia != dtm_hora_actual2):
				ticket.Borrar_lista_caducados()
			time.sleep(1)
	except Exception as e:
		logging.error(str(generico.diahora()) + " Error en LAP_SP_OBD en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		logging.error(str(generico.diahora()) + " Error en LAP_SP_OBD en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
	time.sleep(10)