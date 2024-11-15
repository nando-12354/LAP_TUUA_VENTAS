# Operacion Sincronizacion de LLave : sincroniza las llaves de destrabe dadas , para tener de manera constante la ifnormacion de llaves activadas
from puerta import puerta
import time
import generico
import logging
import sys
import datetime
import configuracionParametros

obj_door = puerta('cerrado')

dia=generico.diahora()
nombre_log = 'traza_OSLL_' + str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) +'.log'
logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
logging.warning("Inicio LAP_SP_OSLL :"+ str(dia));
logging.info(str(dia) + " Conectado LAP_SP_OSLL");

while(1):
	try:
		dia=generico.diahora()
		nombre_log = 'traza_OSLL_' + str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) +'.log'
		logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
		estado = obj_door.estado_webservice()
		if (estado[1] == "ok"):
			obj_door.sincronizacionLlaveDestrabe()
			time.sleep(5)
	except Exception as e:
		print(e.args)
		print(str(sys.exc_info()[2].tb_lineno))
		logging.error(str(dia) + " Error en LAP_SP_OSLL en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		logging.error(str(dia) + " Error en LAP_SP_OVP en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
	time.sleep(180)