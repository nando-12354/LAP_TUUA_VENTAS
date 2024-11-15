"""
ARCHIVO CLASE : configuracionParametros.py
Objetivo : Archivo que tiene los parametros que se puedan cambiar
Autor : 
Fecha Creacion : 2019-05-21
Metodos :	-captarDatos() capta los datos del archivo de texto y busca especificamente la informacion que pide 
			-urlVerificarRed() brinda el url para la verificacion de la conexion con metodo de get
			-urlVerificarRed_timeout() el timeout que se le da para el metodo get con el primer url
			-urlVerificarWebservice() brinda el url de la verificacion via web service con metodo post
			-urlVerificarWebservice_timeout() el timeout que se le da para el metodo post con el segundo url
			-urlSubirBDaWS() brinda el url del metodo post para subir datos de la base de datos al web service
			-urlSubirBDaWS_timeout() el timeout que se le da para el metodo post con el tercer url
			-urlVerificarEstadoPuerta() brinda el url del metodo de get para actualizar la puerta
			-urlVerificarEstadoPuerta_timeout() el timeout que se le da para el metodo get con el cuarto url
			-urlVerificarLlavesDestrabe() brinda el url del metodo get para actualizar de las llaves de destrabe
			-urlVerificarLlavesDestrabe_timeout() el timeout que se le da para el metodo get con el quinto url
			-horaContingenciatemprano() parametro con horas que el pasajero puede estar antes de su hora de vuelo
			-horaContingenciatarde() parametro con horas que el pasajero puede estar despues de la hora de su vuelo
			-limiteBorrado() duracion de vida de los datos en la base de datos
			-limiteBorradoLog() duracion de vida de los archivos de log
			-horaBorradoCaducados() hora en la que se realizara el borrado de datos 
			-datosBD() son los parametros de la base de datos mysql
			-AeropuertoActual() es el codigo del aeropuerto actual
			-NombrePuerta() es el nombre de la puerta actual
			-luzscanner() es el COM en el cual se encuentra las luces del scanner
			-puertoscanner() es el puerto del scanner
			-longitudTuua() es la longitud que deberia tener un codigo tuua
			-direccionControlador() es el string con la direccion del controlador
			-computadorGateway() es el string del gateway del computador
			-desfazeUTC() es el desface horario que tiene el local
			-estadoCerrado() es el estado de la puerta cuando esta bloqueada
			-estadoAbrir() es el estado de la puerta cuando esta abierta
			-estadoNormal() es el estado de la puerta cuando se mantiene cerrada a menos que se le de la orden de abrirse
			-rangoMuestreo() es la cantidad de veces que se muestraria para que la puerta se bloquee
			-muestreoPuerta() es que tan seguido se realizaran el muestreo
			-statusQuery() comando del status query
			-autorizaPase() comando de autorizar pase
			-intentoFraude() comando de intento de fraude
			-completoPase() codigo de pase completado
			-regreso() codigo de regreso de pasajero
			-incompletoPase() codigo de pase incompletado
			-bloqueo() codigo de bloqueo
			-pasoInocrrecto() codigo de paso incorrecto
			-emergencia() codigo de emergencia
			-bloqueadoEsp() mensaje de bloqueado en espanol
			-bloqueadoEn() mensaje de bloqueado en ingles
			-desbloqueadoEsp() mensaje de desbloqueado en espanol
			-desbloqueadoEn() mensaje de desbloqueado en ingles
			-mulEsp() mensaje de llave multiple espanol
			-mulEn() mensaje de llave multiple ingles
			-mnsjPase() mensaje de pase satisfactorio
			-mnsjNoPaseHora() mensaje de no pase por hora
			-mnsjNoPaseAeropuerto() mensaje de no pase por aeropuerto equivocado
			-mnsjNoPaseUtilizado() mensaje de no pase por que ya fue utilizado
			-mnsjNoPase() mensaje de no pase
			-mnsjTransito() mensaje de pasajero en transito
			-mnsjBienvenidoEsp() mensaje de bienvenido en espanol
			-mnsjBienvenidoEng() mensaje de bienvenido en ingles
			-comprobarDatos() comprueba si todos los parametros son correctos
"""
import datetime
import os
import logging
import sys
import time
import mysql.connector
import generico

try:
	variable = os.listdir(os.getcwd())
	std = 0
	for var in variable :
		if(var == 'parametros.txt'):
			std = std + 1

	if (std == 0):
		f= open("parametros.txt","w+")
		f.write("COMENTARIO: Escoje el tipo de separador el cual estara entre los datos->(#)  No cambiar el orden de los parametros\n")
		f.write("1 web service verificar red:	#http://172.20.0.125/tuua_api/api/TuuaAccesos/getstatus#\n")
		f.write("2 web service registrar trama:	#http://172.20.0.125/tuua_api/api/TuuaAccesos/PostRegistrarTrama#\n")
		f.write("3 web service registro contin:	#http://172.20.0.125/tuua_api/api/TuuaAccesos/PostRegistrarTramaContingencia#\n")
		f.write("4 web service estado puerta:	#http://172.20.0.125/tuua_api/api/TuuaAccesos/GetMolinetePorCodigo?cod_molinete=#\n")
		f.write("5 web service llaves destrabe:	#http://172.20.0.125/tuua_api/api/TuuaAccesos/GetLlavesDestrabe#\n")
		f.write("6 Timeout verficar red:		#3#\n")
		f.write("7 Timeout registrar trama:	#3#\n")
		f.write("8 Timeout registro contin:	#3#\n")
		f.write("9 Timeout estado puerta:	#3#\n")
		f.write("10 Timeout llaves de destrabe:	#3#\n")
		f.write("11 Horas antes:			#12#\n")
		f.write("12 Horas despues:		#12#\n")
		f.write("13 Borrado base de datos:	#30#\n")
		f.write("14 Borrado log:			#10#\n")
		f.write("15 Hora de borrado:		#13:06#\n")
		f.write("16 Datos base de datos:		#localhost,root,,lap#\n")
		f.write("17 Aeropuerto origen:		#LIM#\n")
		f.write("18 Nombre puerta:		#L03#\n")
		f.write("19 luz scanner:			#COM20#\n")
		f.write("20 puerto scanner:		#COM19#\n")
		f.write("21 longitud tuua :		#16#\n")		
		f.write("22 direccion controlador:	#http://192.168.0.200:8081/#\n")
		f.write("23 estado cerrado puerta:	#L#\n")
		f.write("24 estado abierto puerta:	#O#\n")
		f.write("25 estado normal puerta:	#E#\n")
		f.write("26 rango de muestreo:		#40#\n")
		f.write("27 duracion de muestreo:	#0.5#\n")
		f.write("28 status query:		#SQ#\n")
		f.write("29 autorizar pase:		#CC#\n")
		f.write("30 intento de fraude:		#CR#\n")
		f.write("31 Completo pase:		#QBOK#\n")
		f.write("32 regreso:			#CNXB#\n")
		f.write("33 incompleto pase:		#TODT#\n")
		f.write("34 bloqueo:			#FRXO#\n")
		f.write("35 paso incorrecto:		#FR2X#\n")
		f.write("36 emergencia:			#EXOK#\n")
		f.write("37 mensaje bloqueado esp :	#bloqueado#\n")
		f.write("38 mensaje bloqueado en :	#locked#\n")
		f.write("39 mensaje desbloqueado esp :	#desbloqueado#\n")
		f.write("40 mensaje desbloqueado en :	#unlocked#\n")
		f.write("41 mensaje mul esp :		#pase ticket (mul)#\n")
		f.write("42 mensaje mul en :		#pass ticket (mul)#\n")
		f.write("43 mnsj de pase :		#Registro Satisfactorio#\n")
		f.write("44 mnsj de no pase por hora:	#Se encuentra fuera del rango de hora#\n")
		f.write("45 mnsj de no pase aeropuerto:	#Aeropuerto equivocado#\n")
		f.write("46 mnsj de no pase ya utilizado:#Boleto utilizado previamente#\n")
		f.write("47 mnsj de no pase :		#No pase#\n")
		f.write("48 mnsj de transito :		#PASAJERO EN TRANSITO#\n")
		f.write("49 mnsj bienvenido esp :	#Bienvenido#\n")
		f.write("50 mnsj bienvenido en :		#Welcome#\n")
		f.close()


except Exception as e:
	logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
except :
	logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)

def captarSeparador():
	f = open("parametros.txt","r")
	if f.mode == "r":
		contents = f.read()
		partes = contents.split('\n')
	f.close
	numI = partes[0].find("(")
	numF = partes[0].find(")")
	return partes[0][numI+1:numF]

def captarDatos(num):
	try:
		int_numero = (num*2)

		f = open("parametros.txt","r")
		if f.mode == "r":
			contents = f.read()
			#print "captar datos $$$$$$$$$$$"
			separador = captarSeparador()
			#print separador
			partes = contents.split(separador)
		f.close
		string = partes[int_numero]

	except :
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
		string = 'Error404'
	finally:
	#print "antes del string"
	#print string
		return string

def buscarDatoxLinea(numero_de_linea,string):
	try:
		#print 'muestra los datos'
		#print numero_de_linea
		#print string

		f = open("parametros.txt","r")
		if f.mode == "r":
			contents = f.read()
			partes = contents.split('\n')
		f.close
		cadena = partes[numero_de_linea]
		indice = cadena.find(string)
		indice2 = cadena.find(str(numero_de_linea))
		devol = 'ok'	
		if(indice == -1):
			devol = 'hay un error'
			#raise Exception('error', 'error')
		elif (indice2 == -1):
			devol = 'hay un error'
			#raise Exception('error', 'error')
		return devol

	except :
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
		devol = 'hay un error'
		return devol
	#finally:
	#	return devol


def urlVerificarRed():
	try:
		msg = captarDatos(1)
		feedback = buscarDatoxLinea(1,msg)
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlVerificarRed_timeout():
	try:
		msg = int(captarDatos(6))
		feedback = buscarDatoxLinea(6,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlVerificarWebservice():
	try:
		msg = captarDatos(2)
		feedback = buscarDatoxLinea(2,msg)
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlVerificarWebservice_timeout():
	try:
		msg = int(captarDatos(7))
		feedback = buscarDatoxLinea(7,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlSubirBDaWS():
	try:
		msg = captarDatos(3)
		feedback = buscarDatoxLinea(3,msg)
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlSubirBDaWS_timeout():
	try:
		msg = int(captarDatos(8))
		feedback = buscarDatoxLinea(8,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlVerificarEstadoPuerta():
	try:
		msg = captarDatos(4)
		feedback = buscarDatoxLinea(4,msg)
		msg = msg + NombrePuerta()
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlVerificarEstadoPuerta_timeout():
	try:
		msg = int(captarDatos(9))
		feedback = buscarDatoxLinea(9,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlVerificarLlavesDestrabe():
	try:
		msg = captarDatos(5)
		feedback = buscarDatoxLinea(5,msg)
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def urlVerificarLlavesDestrabe_timeout():
	try:
		msg = int(captarDatos(10))
		feedback = buscarDatoxLinea(10,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def horaContingenciatemprano():
	try:
		msg = int(captarDatos(11))
		feedback = buscarDatoxLinea(11,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return datetime.timedelta(hours=msg)

def horaContingenciatarde():
	try:
		msg = int(captarDatos(12))
		feedback = buscarDatoxLinea(12,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return datetime.timedelta(hours=msg)

def limiteBorrado():
	try:
		msg = int(captarDatos(13))
		feedback = buscarDatoxLinea(13,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
		fch = datetime.timedelta(days=msg)
	except Exception as e:
		fch = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		fch = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return fch

def limiteBorradoLog():
	try:
		msg = int(captarDatos(14))
		feedback = buscarDatoxLinea(14,str(msg))
		if (feedback == 'hay un error'):
			msg= "Error404"
			#raise Exception('error', 'error')
		fch = datetime.timedelta(days=msg)
	except Exception as e:
		fch = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		fch = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return fch

def horaBorradoCaducados():
	try:
		msg = captarDatos(15).split(':')
		for x in msg:
			feedback = buscarDatoxLinea(15,x)
			if (feedback == 'hay un error'):
				msg= "Error404"
				#raise Exception('error', 'error')

		fch = datetime.time(int(msg[0]),int(msg[1]), 0)
	except Exception as e:
		fch = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		fch = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return fch

def datosBD():
	try:
		msg = captarDatos(16).split(',')
		#print msg
		for x in msg :
		#	print x
			feedback = buscarDatoxLinea(16,x)
		#	print feedback
			if (feedback == 'hay un error'):
				msg = 'Error404'
		#		raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def AeropuertoActual():
	try:
		msg = captarDatos(17)
		feedback = buscarDatoxLinea(17,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def NombrePuerta():
	try:
		msg = captarDatos(18)
		feedback = buscarDatoxLinea(18,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def luzscanner():
	try:
		msg = captarDatos(19)
		feedback = buscarDatoxLinea(19,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def puertoscanner():
	try:
		msg = captarDatos(20)
		feedback = buscarDatoxLinea(20,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def longitudTuua():
	try:
		msg = int(captarDatos(21))
		feedback = buscarDatoxLinea(21,str(msg))
		if (feedback == 'hay un error'):
			msg = 'Error404'
	#		raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg


def direccionControlador():
	try:
		msg = captarDatos(22)
		feedback = buscarDatoxLinea(22,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
	#		raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg


def desfazeUTC():
	return -5

def estadoCerrado():
	try:
		msg = captarDatos(23)
		feedback = buscarDatoxLinea(23,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def estadoAbrir():
	try:
		msg = captarDatos(24)
		feedback = buscarDatoxLinea(24,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def estadoNormal():
	try:
		msg = captarDatos(25)
		feedback = buscarDatoxLinea(25,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def rangoMuestreo():
	try:
		msg = int(captarDatos(26))
		feedback = buscarDatoxLinea(26,str(msg))
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def muestreoPuerta():
	try:
		msg = float(captarDatos(27))
		feedback = buscarDatoxLinea(27,str(msg))
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def statusQuery():
	try:
		msg = captarDatos(28)
		feedback = buscarDatoxLinea(28,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def autorizaPase():
	try:
		msg = captarDatos(29)
		feedback = buscarDatoxLinea(29,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def intentoFraude():
	try:
		msg = captarDatos(30)
		feedback = buscarDatoxLinea(30,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def completoPase():
	try:
		msg = captarDatos(31)
		feedback = buscarDatoxLinea(31,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def regreso():
	try:
		msg = captarDatos(32)
		feedback = buscarDatoxLinea(32,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg
#
def incompletoPase():
	try:
		msg = captarDatos(33)
		feedback = buscarDatoxLinea(33,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def bloqueo():
	try:
		msg = captarDatos(34)
		feedback = buscarDatoxLinea(34,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def pasoInocrrecto():
	try:
		msg = captarDatos(35)
		feedback = buscarDatoxLinea(35,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def emergencia():
	try:
		msg = captarDatos(36)
		feedback = buscarDatoxLinea(36,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def bloqueadoEsp():
	try:
		msg = captarDatos(37)
		feedback = buscarDatoxLinea(37,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def bloqueadoEn():
	try:
		msg = captarDatos(38)
		feedback = buscarDatoxLinea(38,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def desbloqueadoEsp():
	try:
		msg = captarDatos(39)
		feedback = buscarDatoxLinea(39,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def desbloqueadoEn():
	try:
		msg = captarDatos(40)
		feedback = buscarDatoxLinea(40,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mulEsp():
	try:
		msg = captarDatos(41)
		feedback = buscarDatoxLinea(41,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mulEn():
	try:
		msg = captarDatos(42)
		feedback = buscarDatoxLinea(42,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mnsjPase():
	try:
		msg = captarDatos(43)
		feedback = buscarDatoxLinea(43,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mnsjNoPaseHora():
	try:
		msg = captarDatos(44)
		feedback = buscarDatoxLinea(44,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mnsjNoPaseAeropuerto():
	try:
		msg = captarDatos(45)
		feedback = buscarDatoxLinea(45,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mnsjNoPaseUtilizado():
	try:
		msg = captarDatos(46)
		feedback = buscarDatoxLinea(46,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mnsjNoPase():
	try:
		msg = captarDatos(47)
		feedback = buscarDatoxLinea(47,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mnsjTransito():
	try:
		msg = captarDatos(48)
		feedback = buscarDatoxLinea(48,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mnsjBienvenidoEsp():
	try:
		msg = captarDatos(49)
		feedback = buscarDatoxLinea(49,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def mnsjBienvenidoEng():
	try:
		msg = captarDatos(50)
		feedback = buscarDatoxLinea(50,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
			#raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def pasando():
	try:
		msg = captarDatos(51)
		feedback = buscarDatoxLinea(51,msg)
		if (feedback == 'hay un error'):
			msg = 'Error404'
		#	raise Exception('error', 'error')
	except Exception as e:
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		msg = 'Error404'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return msg

def comprobarDatos():
	try:
		lista = []
		lista.append(urlVerificarRed())
		lista.append(urlVerificarWebservice())
		lista.append(urlSubirBDaWS())
		lista.append(urlVerificarEstadoPuerta())
		lista.append(urlVerificarLlavesDestrabe())
		lista.append(urlVerificarRed_timeout())
		lista.append(urlVerificarWebservice_timeout())
		lista.append(urlSubirBDaWS_timeout())
		lista.append(urlVerificarEstadoPuerta_timeout())
		lista.append(urlVerificarLlavesDestrabe_timeout())
		lista.append(horaContingenciatemprano())
		lista.append(horaContingenciatarde())
		lista.append(limiteBorrado())
		lista.append(limiteBorradoLog())
		lista.append(horaBorradoCaducados())
		lista.append(datosBD())
		lista.append(AeropuertoActual())
		lista.append(NombrePuerta())
		lista.append(luzscanner())
		lista.append(puertoscanner())
		lista.append(direccionControlador())
		lista.append(desfazeUTC())
		lista.append(estadoCerrado())
		lista.append(estadoAbrir())
		lista.append(estadoNormal())
		lista.append(rangoMuestreo())
		lista.append(muestreoPuerta())
		lista.append(statusQuery())
		lista.append(autorizaPase())
		lista.append(intentoFraude())
		lista.append(completoPase())
		lista.append(regreso())
		lista.append(incompletoPase())
		lista.append(bloqueo())
		lista.append(pasoInocrrecto())
		lista.append(emergencia())
		lista.append(bloqueadoEsp())
		lista.append(bloqueadoEn())
		lista.append(desbloqueadoEsp())
		lista.append(desbloqueadoEn())
		lista.append(mulEsp())
		lista.append(mulEn())
		lista.append(mnsjPase())
		lista.append(mnsjNoPaseHora())
		lista.append(mnsjNoPaseAeropuerto())
		lista.append(mnsjNoPaseUtilizado())
		lista.append(mnsjNoPase())
		lista.append(mnsjTransito())
		lista.append(mnsjBienvenidoEsp())
		lista.append(mnsjBienvenidoEng())
		lista.append(pasando())

		flag = 'no hay problema'
		i=0
		for cosa in lista :
			i=i+1
			if (cosa == 'Error404'):
				print(i)
				flag = 'hay problema'
				break
	except :
		flag = 'hay problema'
		logging.error(str(generico.diahora()) + " Error en configuracionParametros en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	finally:
		return flag