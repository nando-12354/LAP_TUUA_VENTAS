#Operacion Sincronizado de Datos : sincroniza los datos de pasajeros que pasaron por la puerta cuando estaba en estado de contingencia
from puerta import puerta
from ticket import ticket
import time
import logging
import sys
import configuracionParametros
import datetime
import generico

dia=generico.diahora()
nombre_log = 'traza_OSD__' + str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) +'.log'
logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
logging.warning("Inicio LAP_SP_OSD :"+ str(dia));
logging.info(str(dia) + " Conectado LAP_SP_OSD");

while(1):
	try:
		dia=generico.diahora()
		nombre_log = 'traza_OSD__' + str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) +'.log'
		logging.basicConfig(filename=nombre_log,level=logging.DEBUG)

		estado = puerta.estado_webservice()
		if (estado[1] == "ok"):
			time.sleep(1)
			ticket.sincronizacionBD()
	except Exception as e:
		logging.error(str(dia) + " Error en LAP_SP_OSD en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		logging.error(str(dia) + " Error en LAP_SP_OVP en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
	time.sleep(180)
