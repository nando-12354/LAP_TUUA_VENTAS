# -*- coding: utf-8 -*-

import datetime
import configuracionParametros
import logging
import socket
import mysql.connector
import sys
import netifaces
from urllib.request import urlopen


def diahora():
	return datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())

def listaVacia(str_lista):
	if not str_lista:
		str_result = "vacio"
	else:
		str_result = "no vacio"
	return str_result

def limpiaApostrofe(cadena):
	#cadena = cadena.encode("ascii")
	cadena = cadena.replace(chr(39)," ")
	#cadena = cadena.encode("ascii","ignore")
	return cadena

def entraBD():
	BD = configuracionParametros.datosBD()
	try:
		mydb = mysql.connector.connect(
		host=BD[0],
		user=BD[1],
		passwd=BD[2],
		database=BD[3],
		)
		return mydb
	except Exception as e:
		logging.error(str(diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		logging.error(str(diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)

def archivo_log(mensaje):
	logging.info(str(diahora())+ mensaje)

def nullDetector(str_mensaje):
	respuesta =  (str_mensaje if str_mensaje else "")
	return respuesta

def internet_on():
    try:
        urlopen(configuracionParametros.urlVerificarRed(), timeout=1)
        return True
    except : 
        return False

def checkConnection():
	a =  netifaces.interfaces()
	for b in a:
		c = netifaces.ifaddresses(b)  #lee los datos de la interface de red
		for d in c: #lee dato a dato la configuracion
			for e in c[d]:
				text = e['addr'].split('.')

				if (text[0]=="10"):
					print("		ok encontro inter")
					print("***********************************")

def enviaPantalla(msg,msg2,msg3,msg4):
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    server_address = ('localhost', 10000)
    print('connecting to {} port {}'.format(*server_address))

    sock.connect(server_address)


    try:
        x = msg+'|'+msg2+'|'+msg3+'|'+msg4
        message = str.encode(x)#b x  
        print('sending {!r}'.format(message))
        sock.sendall(message)

    finally:
        sock.close()



def verificacionTuua(str_txt_trama):
	try:
		con = entraBD()
		my_cursor = con.cursor()
		my_cursor.execute("SELECT COUNT(txt_trama) FROM LAP_SP_informacion_de_ticket WHERE  txt_trama = '" + str(str_txt_trama) + "' and num_pase > 0")
		results = my_cursor.fetchall()
		print(results)

		if(results[0][0]==0):
			lista = []
			lista.append("verde")
			lista.append(configuracionParametros.mnsjPase())
			lista.append("pase tuua")
		else:
			lista = []
			lista.append("rojo")
			lista.append(configuracionParametros.mnsjNoPaseUtilizado())
			lista.append("no pase tuua")

		con.close()

	
	except Exception as e:
		lista = []
		lista.append("rojo")
		lista.append(configuracionParametros.mnsjNoPase())
		lista.append("no pase tuua")
		logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		lista = []
		lista.append("rojo")
		lista.append(configuracionParametros.mnsjNoPase())
		lista.append("no pase tuua")
		logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return lista

def verificacionDES(str_txt_trama):
	try:
		con = entraBD()
		my_cursor = con.cursor()
		my_cursor.execute("SELECT COUNT(cod_destrabe) FROM LAP_SP_llaves_de_destrabe WHERE  cod_destrabe = '" + str(str_txt_trama) + "'")
		results = my_cursor.fetchall()
		print(results)

		if(results[0][0]==0):
			resp = "ok"
		else:
			resp = "no"

		con.close()

	
	except Exception as e:
		resp = "no"
		logging.error(str(generico.diahora()) + " Error en generico en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except :
		resp = "no"
		logging.error(str(generico.diahora()) + " Error en generico en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
	finally:
		return resp