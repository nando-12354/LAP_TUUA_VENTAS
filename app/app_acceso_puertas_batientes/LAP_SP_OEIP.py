# Opereacion envia informacion de puertas : envia la informacion del estado actual de las puertas hacia un terminal el cual
# mostrara la informacion de manera grafica
import json
from websocket import create_connection
import configuracionParametros
import time
import mysql.connector
import logging
import datetime
import sys
dia=datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
nombre_log = 'traza_OEIP_' + str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) +'.log'
logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
logging.warning("Inicio LAP_SP_OEIP :"+ str(dia));
logging.info(str(dia) + " Conectado LAP_SP_OEIP");
logging.info(str(dia) + " >>>>>>>>>>>>> Conectado LAP_SP_OEIP");

try:
	dtm_hora=time.time()
	str_nombre_puerta = configuracionParametros.NombrePuerta()

	BD = configuracionParametros.datosBD()
	mydb = mysql.connector.connect(
		host=BD[0],
		user=BD[1],
		passwd=BD[2],
		database=BD[3],
	)
	my_cursor = mydb.cursor()
	my_cursor.execute("SELECT * FROM LAP_SP_estado_de_puerta WHERE  dsc_puerta = '"+ configuracionParametros.NombrePuerta()+"' ")
	results = my_cursor.fetchall()

	if not results:
		respuesta = "vacio"
	else:
		respuesta = "no vacio"

	if (respuesta == "no vacio"):
		status = results[0][1]
		tipo = results[0][2]
	else:
		status = "no creado"
		tipo = "no creado"

	msg ={
	  "data": {
	    "id_puerta": str_nombre_puerta,
	    "status": status,
	    "tipo": tipo, # o False
	    "timestamp": dtm_hora
	  },
	}

	my_cursor = mydb.cursor()
	my_cursor.execute("SELECT * FROM LAP_SP_enviar_pases WHERE  dsc_puerta = '"+ configuracionParametros.NombrePuerta()+"' AND dsc_estado_pase = 'no enviado'")
	results2 = my_cursor.fetchall()
	#print("ESTO ES LO QUE SE CAPTA")
	#print(results2)
	msg2 = []
	for resp in results2:
		temp={
	    "id_puerta": str_nombre_puerta,
	    "timestamp": str(resp[3])
	  }
		msg2.append(temp)

	MSG = []
	MSG.append(msg)
	MSG.append(msg2)
	#print("se imprime todo lo que se quiere mandar")
	#print(MSG)

	y = json.dumps(MSG)

	ws = create_connection("ws://echo.websocket.org/")
	#print("Sending 'Hello, World'...")

	try:
		ws.send(y)
	except Exception as e:
		#print(e.args)
		#print(str(sys.exc_info()[2].tb_lineno))
		logging.error(str(generico.diahora()) + " Error en LAP_SP_OEIP en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		logging.error(str(generico.diahora()) + " Error en LAP_SP_OEIP en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
	else :
		for resp in results2:
			my_sql = "UPDATE LAP_SP_enviar_pases SET dsc_estado_pase = 'enviado' WHERE fch_pase = '" + str(resp[2]) + "'"
			my_cursor.execute(my_sql)
			mydb.commit()

	print("Sent")
	print("Receiving...")
	result =  ws.recv()
	print("Received '%s'" % result)
	ws.close()
except Exception as e:
	#print(e.args)
	#print(str(sys.exc_info()[2].tb_lineno))
	logging.error(str(generico.diahora()) + " Error en LAP_SP_OEIP en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
except:
	logging.error(str(generico.diahora()) + " Error en LAP_SP_OEIP en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
time.sleep(2)