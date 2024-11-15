
import requests
import mysql.connector
import datetime
import logging
import sys
import time
import socket
import math
import configuracionParametros
import serial
from urllib.request import urlopen
import os

def internet_on(logger):
	try:
		urlopen(configuracionParametros.urlVerificarRed(logger), timeout=1)
		return True
	except Exception as e: 
		logger.info("Error: "+str(datetime.datetime.today())+" Error en internet_on "+str(e))
		return False

def diahora():
	text = str(datetime.datetime.utcnow() + datetime.timedelta(hours=-5))
	return text.split(".")[0]

def listaVacia(str_lista):
	if not str_lista:
		str_result = "vacio"
	else:
		str_result = "no vacio"
	return str_result

def entraBD(logger):
	BD = configuracionParametros.datosBD(logger)
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

def lista_vuelos_salidas(lugar,logger):
	respuesta =[]
	try:
		con = entraBD(logger)
		my_cursor = con.cursor()
		my_cursor.execute("SELECT Num_Vuelo,Dsc_Proc_Destino,Fch_Real,Fch_Est,Hor_Prog,num FROM LAP_Transito_Vuelos_salidas WHERE Dsc_Proc_Destino != '"+lugar+"' ORDER BY Hor_Prog")
		results = my_cursor.fetchall()
		#print("PRINTEO DE LA BASE DE DATOS ")
		#print(results)
		con.close()
		listaL = []
		for resp in results:
			listaS = []
			listaS.append(resp[1])
			listaS.append(resp[0])
			if (resp[2]!=None):
				listaS.append(resp[2])
			elif(resp[3]!=None):
				listaS.append(resp[3])
			else:
				listaS.append(resp[4])
			listaL.append(listaS)

		respuesta.append(200)
		respuesta.append(listaL)

	except Exception as e:
		respuesta.append(999)
		respuesta.append("no hay servicio")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en lista_vuelos_salidas en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " +str(e))
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		respuesta.append(404)
		respuesta.append("no hay servicio")		
		logger.info("Error: "+str(datetime.datetime.today())+" Error en lista_vuelos_salidas en linea :     " + str(sys.exc_info()[2].tb_lineno))
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)	
	finally:
		return respuesta

def lista_vuelos_llegadas(logger):
	respuesta =[]
	try:
		con = entraBD(logger)
		my_cursor = con.cursor()
		my_cursor.execute("SELECT Num_Vuelo,Dsc_Proc_Destino,Fch_Real,Fch_Est,Hor_Prog,num FROM LAP_Transito_Vuelos_llegadas WHERE num != 322 ORDER BY Hor_Prog DESC")
		results = my_cursor.fetchall()
		con.close()
		listaL = []
		for resp in results:
			listaS = []
			listaS.append(resp[1])
			listaS.append(resp[0])
			if (resp[2]!=None):
				listaS.append(resp[2])
			elif(resp[3]!=None):
				listaS.append(resp[3])
			else:
				listaS.append(resp[4])
			listaL.append(listaS)

		respuesta.append(200)
		respuesta.append(listaL)

	except Exception as e:
		respuesta.append(999)
		respuesta.append("no hay servicio")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en lista_vuelos_llegadas en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " +str(e))
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		respuesta.append(404)
		respuesta.append("no hay servicio")		
		logger.info("Error: "+str(datetime.datetime.today())+" Error en lista_vuelos_llegadas en linea :     " + str(sys.exc_info()[2].tb_lineno))
	finally:
		return respuesta


def estado_vuelos_salidas(logger):
	respuesta = []
	try:
		print("antes del webservice de llaves")
		print(configuracionParametros.urlServiceVuelosSalidas(logger))
		print(configuracionParametros.urlServiceVuelosSalidas_timeout(logger))
		r = requests.get(configuracionParametros.urlServiceVuelosSalidas(logger), timeout=configuracionParametros.urlServiceVuelosSalidas_timeout(logger))  # guarda el valor que me da
		print("despues del request")
		if r.status_code == 200:
			print("despues del 200")
			respuestas = r.json()
			listaL = []
			for resp in respuestas:
				listaS = []
				listaS.append(resp['Num_Vuelo'])
				listaS.append(resp['Fch_Prog'])
				listaS.append(resp['Hor_Prog'])
				listaS.append(resp['Tip_Operacion'])
				listaS.append(resp['Cod_Compania'])
				listaS.append(resp['Tip_Vuelo'])
				listaS.append(resp['Fch_Real'])
				listaS.append(resp['Fch_Est'])
				listaS.append(resp['Cod_Proc_Dest'])
				listaS.append(resp['Cod_Escala'])
				listaS.append(resp['Cod_Gate'])
				listaS.append(resp['Tip_Gate_Terminal'])
				listaS.append(resp['Cod_Faja'])
				listaS.append(resp['Cod_Mostrador'])
				listaS.append(resp['Dsc_Aerolinea'])
				listaS.append(resp['Dsc_Proc_Destino'])
				listaS.append(resp['Log_Fch_Creacion'])
				listaS.append(resp['Log_Fch_Modificacion'])
				listaS.append(resp['Dsc_Estado'])
				listaS.append(resp['Cod_Iata'])
				listaS.append(resp['Nro_Vuelo'])
				listaL.append(listaS)

			respuesta.append(r.status_code)
			respuesta.append(listaL)
		else:
			respuesta.append(999)
			respuesta.append("no hay servicio")		
	except requests.exceptions.ConnectionError as e:
		respuesta = []
		respuesta.append(404)
		respuesta.append("no hay servicio")
	except Exception as e:
		respuesta = []
		respuesta.append(777)
		respuesta.append("no hay servicio")
	except:
		respuesta = []
		respuesta.append(888)
		respuesta.append("no hay servicio")

	finally:
		return respuesta

def estado_vuelos_llegadas(logger):
	respuesta = []
	try:
		r = requests.get(configuracionParametros.urlServiceVuelosLlegadas(logger), timeout=configuracionParametros.urlServiceVuelosLlegadas_timeout(logger))  # guarda el valor que me da
		if r.status_code == 200:
			respuestas = r.json()
			listaL = []
			for resp in respuestas:
				listaS = []
				listaS.append(resp['Num_Vuelo'])
				listaS.append(resp['Fch_Prog'])
				listaS.append(resp['Hor_Prog'])
				listaS.append(resp['Tip_Operacion'])
				listaS.append(resp['Cod_Compania'])
				listaS.append(resp['Tip_Vuelo'])
				listaS.append(resp['Fch_Real'])
				listaS.append(resp['Fch_Est'])
				listaS.append(resp['Cod_Proc_Dest'])
				listaS.append(resp['Cod_Escala'])
				listaS.append(resp['Cod_Gate'])
				listaS.append(resp['Tip_Gate_Terminal'])
				listaS.append(resp['Cod_Faja'])
				listaS.append(resp['Cod_Mostrador'])
				listaS.append(resp['Dsc_Aerolinea'])
				listaS.append(resp['Dsc_Proc_Destino'])
				listaS.append(resp['Log_Fch_Creacion'])
				listaS.append(resp['Log_Fch_Modificacion'])
				listaS.append(resp['Dsc_Estado'])
				listaS.append(resp['Cod_Iata'])
				listaS.append(resp['Nro_Vuelo'])
				listaL.append(listaS)

			respuesta.append(r.status_code)
			respuesta.append(listaL)
		else:
			respuesta.append(999)
			respuesta.append("no hay servicio")		
	except requests.exceptions.ConnectionError as e:
		respuesta = []
		respuesta.append(404)
		respuesta.append("no hay servicio")
		
	except Exception as e:
		respuesta = []
		respuesta.append(777)
		respuesta.append("no hay servicio")
		
	except:
		respuesta = []
		respuesta.append(888)
		respuesta.append("no hay servicio")
		

	finally:
		return respuesta


def Post_datos(Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,Cod_Molinete,Trama_Origen,Trama_Destino,logger):
	str_devolver = []
	payload = {'Num_Vuelo_Origen': Num_Vuelo_Origen, 'Fch_Vuelo_Origen': Fch_Vuelo_Origen, 'Num_Vuelo_Destino': Num_Vuelo_Destino, 'Fch_Vuelo_Destino': Fch_Vuelo_Destino, 'Cod_Molinete': Cod_Molinete, 'Trama_Origen': Trama_Origen,'Trama_Destino': Trama_Destino}
	try:
		r = requests.post(configuracionParametros.urlPostDatos(logger), json=payload,timeout=configuracionParametros.urlPostDatos_timeout(logger))
		if r.status_code == 200:  

			text = r.json()
			print("RESPUESTA DEL VERIFICACION WEBSERVICE")
			print(text)
			mensaje = text['respuesta']
			str_devolver.append(text['color'])
			str_devolver.append(text['mensaje'])


			if (mensaje == "ok"):
				str_devolver.append('pase tuua')

			else:
				str_devolver.append('no pase tuua')

		else:
			str_devolver.append('lightblue')
			str_devolver.append('ENTRO EN EL 1')
			str_devolver.append('error')
	except requests.exceptions.ConnectionError as e:
		str_devolver = []
		str_devolver.append('rojo')
		str_devolver.append('ENTRO EN EL 2')
		str_devolver.append('error')
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_datos "+str(e))
	except Exception as e:
		str_devolver = []
		str_devolver.append('rojo')
		str_devolver.append('Sucedio Timeout')
		str_devolver.append('error')
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_datos "+str(e))
		print("printeo del error")
		print(e)
	except:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_datos ")
		str_devolver = []
		str_devolver.append('rojo')
		str_devolver.append('ENTRO EN EL 4')
		str_devolver.append('error')

	finally:
		return str_devolver



def Post_vuelo_dato_salida(trama,logger):
	str_devolver = []
	payload = {'trama': trama}
	try:
		r = requests.post(configuracionParametros.urlPostVueloSalida(logger), json=payload,timeout=configuracionParametros.urlPostVueloSalida_timeout(logger))
		if r.status_code == 200:  
			text = r.json()
			print("RESPUESTA DEL VERIFICACION WEBSERVICE")
			print(text)
			mensaje = text['respuesta']
			str_devolver.append(text['Dsc_Proc_Destino'])
			str_devolver.append(text['Num_Vuelo'])
			if (text['Fch_Real'] != None):
				str_devolver.append(text['Fch_Real'])
			elif(text['Fch_Est'] != None):
				str_devolver.append(text['Fch_Est'])
			else:
				str_devolver.append(text['Hor_Prog'])
		else:
			str_devolver.append('error')
			str_devolver.append('ENTRO EN EL 1')
			str_devolver.append('error')
	except requests.exceptions.ConnectionError as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_vuelo_dato_salida "+str(e))
		str_devolver = []
		str_devolver.append('error')
		str_devolver.append('ENTRO EN EL 2')
		str_devolver.append('error')
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_vuelo_dato_salida "+str(e))
		str_devolver = []
		str_devolver.append('error')
		str_devolver.append('ENTRO EN EL 3')
		str_devolver.append('error')
		print("printeo del error")
		print(e)
	except:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_vuelo_dato_salida ")
		str_devolver = []
		str_devolver.append('error')
		str_devolver.append('ENTRO EN EL 4')
		str_devolver.append('error')
	finally:
		return str_devolver

def Post_vuelo_dato_llegada(trama,logger):
	str_devolver = []
	payload = {'trama': trama}
	try:
		print("DESPUES DEL TRY")
		r = requests.post(configuracionParametros.urlPostVueloLlegada(logger), json=payload,timeout=configuracionParametros.urlPostVueloLlegada_timeout(logger))
		print ("DESPUES DEL REQUESTS")
		print(r)
		if r.status_code == 200:  

			text = r.json()
			print("RESPUESTA DEL VERIFICACION WEBSERVICE")
			print(text)
			mensaje = text['respuesta']
			str_devolver.append(text['Dsc_Proc_Destino'])
			str_devolver.append(text['Num_Vuelo'])
			if (text['Fch_Real'] != None):
				str_devolver.append(text['Fch_Real'])
			elif(text['Fch_Est'] != None):
				str_devolver.append(text['Fch_Est'])
			else:
				str_devolver.append(text['Hor_Prog'])

		else:
			str_devolver.append('error')
			str_devolver.append('ENTRO EN EL 1')
			str_devolver.append('error')
	except requests.exceptions.ConnectionError as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_vuelo_dato_llegada "+str(e))
		str_devolver = []
		str_devolver.append('error')
		str_devolver.append('ENTRO EN EL 2')
		str_devolver.append('error')

	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_vuelo_dato_llegada "+str(e))
		str_devolver = []
		str_devolver.append('error')
		str_devolver.append('ENTRO EN EL 3')
		str_devolver.append('error')
		print("printeo del error")
		print(e)

	except:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Post_vuelo_dato_llegada ")
		str_devolver = []
		str_devolver.append('error')
		str_devolver.append('ENTRO EN EL 4')
		str_devolver.append('error')

	finally:
		return str_devolver		


def armar_cadena(hora,lugar,codigo,logger):
	try:
		lugarO = lugar.strip()

		num1 = lugarO.count('I')
		num2 = lugarO.count(' ')
		numSemTot1 = (num1 + num2 )*5
		#print("numSemTot1")
		#print(numSemTot1)

		num3 = lugarO.count('F')
		numSemTot2 = (num3)*11
		#print("numSemTot2")
		#print(numSemTot2)

		num4 = lugarO.count('U')
		num5 = lugarO.count('H')
		num6 = lugarO.count('J')
		num7 = lugarO.count('L')	
		num8 = lugarO.count('Z')	

		numSemTot3 = (num4 + num5 + num6 + num7 +num8)*12
		#print("numSemTot3")
		#print(numSemTot3)
		num9 = 	lugarO.count('Y')
		num10 = lugarO.count('T')
		num11 = lugarO.count('P')
		num12 = lugarO.count('S')
		num13 = lugarO.count('X')	
		num14 = lugarO.count('N')	
		num15 = lugarO.count('Ñ')	
		numSemTot4 = (num9 + num10 + num11 + num12 + num13 +num14 + num15)*13
		#print("numSemTot4")
		#print(numSemTot4)

		num16 = lugarO.count('E')
		num17 = lugarO.count('K')
		num18 = lugarO.count('C')
		num19 = lugarO.count('V')	
		num20 = lugarO.count('B')	
		num21 = lugarO.count('D')
		numSemTot5 = (num16 + num17 + num18 + num19 +num20 + num21)*14
		#print("numSemTot5")
		#print(numSemTot5)


		num22 = lugarO.count('Q')
		num23 = lugarO.count('R')
		num24 = lugarO.count('O')	
		num25 = lugarO.count('A')	
		num26 = lugarO.count('G')	
		numSemTot6 = (num22 + num23 + num24 +num25 + num26)*15
		#print("numSemTot6")
		#print(numSemTot6)

		num27 = lugarO.count('M')
		numSemTot7 = (num27)*16
		#print("numSemTot7")
		#print(numSemTot7)

		num28 = lugarO.count('W')
		numSemTot8 = (num28)*20
		#print("numSemTot8")
		#print(numSemTot8)


		numResto = (len(lugarO)-(num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10 + num11 + num12 + num13 + num14 + num15 + num16 + num17 + num18 + num19 + num20 + num21 + num22 + num23 + num24 + num25 + num26 + num27 + num28))*12
		#print("numResto")
		#print(numResto)


		verifica = numSemTot1 + numSemTot2 + numSemTot3 + numSemTot4 + numSemTot5 + numSemTot6 + numSemTot7 + numSemTot8 + numResto

		if (verifica>=148):
			f = '{:<18s}\t{:<8s}'	# “>” y “<”
		#elif (len(lugarO)<=3):
		#	f = '{:<18s}\t\t\t{:<8s}'
		else :
			f = '{:<18s}\t\t{:<8s}'
		#cadena1 = lugarO.ljust(35)
		#cadena2 = codigo.ljust(10)
		#cadena = cadena1 + cadena2
		cadena = f.format(lugarO.strip(),codigo.strip())
		cadena = cadena + "\t"
	except Exception as e :
		logger.info("Error: "+str(datetime.datetime.today())+" Error en armar_cadena "+str(e))
	finally:
		return cadena
def trama_recivida(logger):
	try:
		#s2=None
		try:
			print("dentro del try")
			s = serial.Serial(port=configuracionParametros.puertoscanner(logger),baudrate=115200)
			print("s",s)
			s2 = serial.Serial(port=configuracionParametros.luzscanner(logger),baudrate=115200)
			print("luz is opn",s.isOpen())
			print("scanner is open",s2.isOpen())
		#except IOError :
		#	print("error")
		#	s.close()
		#	s2.close()
		#	s.open()
		#	s2.open()
		except Exception as e:
			print(e)
		finally:
			print("s2",s2)
			s2.write(b'\x02RA;s=B\x03')
			str_t = s.read(500)
			str_trama = str_t.decode("utf-8")
			s2.write(b'\x02RA;s=F\x03')
			print(str_trama)
				
		
		try:
			print("ENTRO AL TRY 1 DEL SCANNER")
			prueba=int(str_trama[44:47]) ##############
			prueba2 = str_trama[57]
			devol=[]
			devol.append("boardingPass")
			devol.append(str_trama)

		except:
			print("ENTRO AL EXCEPT 1 DEL SCANNER")


			try:
				print("ENTRO AL TRY 2 DEL SCANNER")
				print(type(len(str_trama)))
				print(configuracionParametros.longitudTuua(logger))
				print(type(configuracionParametros.longitudTuua(logger)))
				prueba3 = int(str_trama)
				if (len(str_trama)==configuracionParametros.longitudTuua(logger)):
					print("PASO LA VERIFICACION DEL IF DEL TRY 2")
					devol=[]
					devol.append("tuua")
					devol.append(str_trama)
				else:
					print("NOOOOOOO PASO LA VERIFICACION DEL IF DEL TRY 2")
					devol=[]
					devol.append("error")
					devol.append(str_trama)
			except:
				print("ENTRO AL EXCEPT 2 DEL SCANNER")
				devol=[]
				devol.append("error")
				devol.append(str_trama)

	except:
		print("ENTRO AL EXCEPT 3 DEL SCANNER")
		devol=[]
		devol.append("error")
		devol.append("")
		logging.error(str(diahora()) + " Error en scanner en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
	finally:
		print("ENTRO AL FINALLY DEL SCANNER")
		return devol ##############
"""def trama_recivida(logger):
	try:
		try:
			s = serial.Serial(port=configuracionParametros.puertoscanner(logger),baudrate=115200)
			s2 = serial.Serial(port=configuracionParametros.luzscanner(logger),baudrate=115200)
			#s.isOpen()
			#s2.isOpen()
			#
		#except Exception as e :
		#	logger.info("Error: "+str(datetime.datetime.today())+" Error en trama_recivida "+str(e))
		#	print("error")
		#	s.close()
		#	s2.close()
		#	s.open()
		#	s2.open()
		except Exception as e:
			logger.info("Error: "+str(datetime.datetime.today())+" Error en trama_recivida "+str(e))
		finally:
			#
			s2.write(b'\x02RA;s=B\x03')
			str_t = s.read(500)
			str_trama = str_t.decode("utf-8")
			s2.write(b'\x02RA;s=F\x03')
			print(str_trama)
				
		
		try:
			print("ENTRO AL TRY 1 DEL SCANNER")
			prueba=int(str_trama[44:47]) ##############
			prueba2 = str_trama[57]
			devol=[]
			devol.append("boardingPass")
			devol.append(str_trama)

		except:
			print("ENTRO AL EXCEPT 1 DEL SCANNER")


			try:
				print("ENTRO AL TRY 2 DEL SCANNER")
				print(type(len(str_trama)))
				print(configuracionParametros.longitudTuua(logger))
				print(type(configuracionParametros.longitudTuua(logger)))
				prueba3 = int(str_trama)
				if (len(str_trama)==configuracionParametros.longitudTuua(logger)):
					print("PASO LA VERIFICACION DEL IF DEL TRY 2")
					devol=[]
					devol.append("tuua")
					devol.append(str_trama)
				else:
					print("NOOOOOOO PASO LA VERIFICACION DEL IF DEL TRY 2")
					devol=[]
					devol.append("error")
					devol.append(str_trama)
			except:
				print("ENTRO AL EXCEPT 2 DEL SCANNER")
				devol=[]
				devol.append("error")
				devol.append(str_trama)

	except:
		print("ENTRO AL EXCEPT 3 DEL SCANNER")
		devol=[]
		devol.append("error")
		devol.append("")
		logging.error(str(diahora()) + " Error en scanner en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
	finally:
		print("ENTRO AL FINALLY DEL SCANNER")
		return devol ##############"""

####################################################################################

def estado_webservice(logger):
	respuesta = []
	try:
		print("Este es el nombre del webservice &&&&&&&")
		print(configuracionParametros.urlVerificarRed(logger))
		r = requests.get(configuracionParametros.urlVerificarRed(logger), timeout=configuracionParametros.urlVerificarRed_timeout(logger))

		if r.status_code == 200:
			estado = r.json()

			print('ESTE ES EL ESTADO DEL WEBSERVICE ')
			print(estado)
			resp = estado['respuesta']
			respuesta.append(r.status_code)
			respuesta.append(str(resp))
		else:
			respuesta.append(999)
			respuesta.append("no hay servicio")
	except requests.exceptions.ConnectionError as e:
		print('error DE CONEXION')
		respuesta.append(404)
		respuesta.append("no hay servicio")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en estado_webservice en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args))
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except Exception as e:
		print('error 2')
		print(e)
		respuesta.append(777)
		respuesta.append("no hay servicio")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en estado_webservice en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args))
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		print('error 3')
		print(sys.exc_info()[0])
		respuesta.append(888)
		respuesta.append("no hay servicio")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en estado_webservice en linea :     " + str(sys.exc_info()[2].tb_lineno) )
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

	finally:
		return respuesta


def sincronizacionVueloSalida(logger):
	try:
		print("111111111111111111111111")
		con = entraBD(logger)
		my_cursor = con.cursor()
		my_cursor.execute("SELECT num FROM LAP_Transito_Vuelos_salidas WHERE num != 322")
		results = my_cursor.fetchall()

		lista = listaVacia(results)
		respuestas = estado_vuelos_salidas(logger)
		print(respuestas)

		if(lista == "no vacio"):
			print("22222222222222222222222222")
			impor = results[len(results)-1][0]
			print("PRINTEO DEL IMPOR$$$$$$$$$$$$$$$$$44")
			print(impor)
			
			if (respuestas[0] == 200):
				my_sql = "DELETE FROM LAP_Transito_Vuelos_salidas WHERE num = " + str(impor)
				my_cursor.execute(my_sql)
				con.commit()
				sql_stuff = "INSERT INTO LAP_Transito_Vuelos_salidas (Num_Vuelo,Fch_Prog,Hor_Prog,Tip_Operacion,Cod_Compania,Tip_Vuelo,Fch_Real,Fch_Est,Cod_Proc_Dest,Cod_Escala,Cod_Gate,Tip_Gate_Terminal,Cod_Faja,Cod_Mostrador,Dsc_Aerolinea,Dsc_Proc_Destino,Log_Fch_Creacion,Log_Fch_Modificacion,Dsc_Estado,Cod_Iata,Nro_Vuelo,num) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)"
				records = []
				for x in respuestas[1]:
					if (impor < 100): 
						x.append(impor+1)
						records.append(x)
					else:
						x.append(1)
						records.append(x)

				my_cursor.executemany(sql_stuff, records)
				con.commit()
		else:
			print("333333333333333333333333333")
			sql_stuff = "INSERT INTO LAP_Transito_Vuelos_salidas (Num_Vuelo,Fch_Prog,Hor_Prog,Tip_Operacion,Cod_Compania,Tip_Vuelo,Fch_Real,Fch_Est,Cod_Proc_Dest,Cod_Escala,Cod_Gate,Tip_Gate_Terminal,Cod_Faja,Cod_Mostrador,Dsc_Aerolinea,Dsc_Proc_Destino,Log_Fch_Creacion,Log_Fch_Modificacion,Dsc_Estado,Cod_Iata,Nro_Vuelo,num) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)"
			records = []
			for x in respuestas[1]:
				x.append(1)
				records.append(x)
			my_cursor.executemany(sql_stuff, records)
			con.commit()
		con.close()
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en sincronizacionVueloSalida "+str(e))
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en sincronizacionVueloSalida ")
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)			

def sincronizacionVuelollegada(logger):
	try:
		con = entraBD(logger)
		my_cursor = con.cursor()
		my_cursor.execute("SELECT num FROM LAP_Transito_Vuelos_llegadas WHERE num != 322")
		results = my_cursor.fetchall()

		lista = listaVacia(results)
		respuestas = estado_vuelos_llegadas(logger)
		if(lista == "no vacio"):
			impor = results[len(results)-1][0]
			print("PRINTEO DEL IMPOR$$$$$$$$$$$$$$$$$44")
			print(impor)
			
			if (respuestas[0] == 200):
				my_sql = "DELETE FROM LAP_Transito_Vuelos_llegadas WHERE num = " + str(impor)
				my_cursor.execute(my_sql)
				con.commit()
				sql_stuff = "INSERT INTO LAP_Transito_Vuelos_llegadas (Num_Vuelo,Fch_Prog,Hor_Prog,Tip_Operacion,Cod_Compania,Tip_Vuelo,Fch_Real,Fch_Est,Cod_Proc_Dest,Cod_Escala,Cod_Gate,Tip_Gate_Terminal,Cod_Faja,Cod_Mostrador,Dsc_Aerolinea,Dsc_Proc_Destino,Log_Fch_Creacion,Log_Fch_Modificacion,Dsc_Estado,Cod_Iata,Nro_Vuelo,num) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)"
				records = []
				for x in respuestas[1]:
					if (impor < 100): 
						x.append(impor+1)
						records.append(x)
					else:
						x.append(1)
						records.append(x)

				my_cursor.executemany(sql_stuff, records)
				con.commit()
		else:
			sql_stuff = "INSERT INTO LAP_Transito_Vuelos_llegadas (Num_Vuelo,Fch_Prog,Hor_Prog,Tip_Operacion,Cod_Compania,Tip_Vuelo,Fch_Real,Fch_Est,Cod_Proc_Dest,Cod_Escala,Cod_Gate,Tip_Gate_Terminal,Cod_Faja,Cod_Mostrador,Dsc_Aerolinea,Dsc_Proc_Destino,Log_Fch_Creacion,Log_Fch_Modificacion,Dsc_Estado,Cod_Iata,Nro_Vuelo,num) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)"
			records = []
			for x in respuestas[1]:
				x.append(1)
				records.append(x)
			my_cursor.executemany(sql_stuff, records)
			con.commit()
		con.close()
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en sincronizacionVuelollegada "+str(e))
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en sincronizacionVuelollegada ")
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)					

def Guardar_datos(Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,Cod_Molinete,Trama_Origen,Trama_Destino,std_subida,fecha,logger):
	try:
		con = entraBD(logger)
		my_cursor = con.cursor()

		sql_stuff = "INSERT INTO LAP_Transito_Datos (Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,Cod_Molinete,Trama_Origen,Trama_Destino,Std_subida,fch_hora_intento) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s)"
		#INSERT INTO LAP_llaves_de_destrabe (cod_usuario,cod_destrabe,cod_molinete,num) VALUES ('prueba','DES000167','prueba',322)
		records = [(Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,Cod_Molinete,Trama_Origen,Trama_Destino,std_subida,fecha),]
		print("ANTES DE GUARDAR DATOS")
		print(records)
		my_cursor.executemany(sql_stuff, records)
		con.commit()
		con.close()
		resp = ['verde','subio correctamente','ok']
	except Exception as e:
		resp = ['rojo','error al subir a la base de datos','error']
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Guardar_datos "+str(e))
	except:
		resp = ['rojo','error al subir a la base de datos','error']
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Guardar_datos ")
	finally:
		return resp


def verificacion_contingencia(trama,pantalla,logger):
	try:
		codigo_iata = trama[36:39]
		numero_vuelo = int(trama[39:44])
		fecha = trama[44:47]
		fecha_cal = datetime.date(year=datetime.date.today().year,month = 1 , day=1) + datetime.timedelta(days=(int(trama[44:47])-1))
		print('fecha_cal')
		print(fecha_cal)
		con = entraBD(logger)
		my_cursor = con.cursor()
		if (pantalla == "origen"):
			my_cursor.execute("SELECT * FROM LAP_Transito_Vuelos_llegadas WHERE Cod_Iata = '"+codigo_iata+"' and Nro_Vuelo='"+str(numero_vuelo)+"' ")
		else:
			my_cursor.execute("SELECT * FROM LAP_Transito_Vuelos_salidas WHERE Cod_Iata = '"+codigo_iata+"' and Nro_Vuelo='"+str(numero_vuelo)+"' ")
		results = my_cursor.fetchall()
		con.close()
		print(results)

		if(results[0][1].split("T")[0]==str(fecha_cal)):
			print("IIIIIIIIIIFFFFFFFFFFFFFF")
			temp = "PASO CONTINGENCIA"
			dato = results[0][0]
			if (results[0][6] != None):
				print("ENTRO EN EL 1")
				print(results[0][6])
				dato2=results[0][6]
			elif (results[0][7] != None):
				print("ENTRO EN EL 2")
				print(results[0][7])
				dato2=results[0][7]
			else:
				print("ENTRO EN EL 3")
				print(results[0][2])
				dato2=results[0][2]
		else:
			print("EEEEELLLLLLSSSSSEEEEEE")
			temp = "NO PASO CONTINGENCIA"
			dato = ""
			dato2 = ""

		lugar = results[0][15]
		codigo = results[0][0]
		dia = dato2
		print("PRINTEO DEL LEN DEL RESULT")
		print(results[0][15])
		print(results[0][0])
		print(dato2)
		print(len(results))
		print("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$")			
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en verificacion_contingencia ")
		temp = "NO PASO CONTINGENCIA"
		dato = ""
		dato2 = ""
		lugar = ""			
		codigo = ""			
		dia = ""			
	finally:
		return [temp,lugar,codigo,dia]

def estado_webservice_llave_destrabe(logger):
	respuesta = []
	try:
		print("antes del webservice de llaves")
		r = requests.get(configuracionParametros.urlVerificarLlavesDestrabe(logger), timeout=configuracionParametros.urlVerificarLlavesDestrabe_timeout(logger))  # guarda el valor que me da
		if r.status_code == 200:
			respuestas = r.json()
			listaL = []
			for resp in respuestas:
				listaS = []
				listaS.append(resp['Cod_Usuario'])
				listaS.append(resp['Cod_Destrabe'])
				listaS.append(resp['Cod_Molinete'])
				listaL.append(listaS)
			respuesta.append(r.status_code)
			respuesta.append(listaL)
		else:
			respuesta.append(999)
			respuesta.append("no hay servicio")		
	except requests.exceptions.ConnectionError as e:
		respuesta = []
		respuesta.append(404)
		respuesta.append("no hay servicio")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en estado_webservice_llave_destrabe "+str(e))
	except Exception as e:
		respuesta = []
		respuesta.append(777)
		respuesta.append("no hay servicio")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en estado_webservice_llave_destrabe "+str(e))
	except:
		respuesta = []
		respuesta.append(888)
		respuesta.append("no hay servicio")
		logger.info("Error: "+str(datetime.datetime.today())+" Error en estado_webservice_llave_destrabe ")
	finally:
		return respuesta


def sincronizacionLlaveDestrabe(logger):
	try:
		con = entraBD(logger)
		my_cursor = con.cursor()
		my_cursor.execute("SELECT num FROM LAP_llaves_de_destrabe WHERE num != 322")
		results = my_cursor.fetchall()

		lista = listaVacia(results)
		respuestas = estado_webservice_llave_destrabe(logger)
		if(lista == "no vacio"):
			impor = results[len(results)-1][0]
			if (respuestas[0] == 200):
				my_sql = "DELETE FROM LAP_llaves_de_destrabe WHERE num = " + str(impor)
				my_cursor.execute(my_sql)
				con.commit()
				sql_stuff = "INSERT INTO LAP_llaves_de_destrabe (cod_usuario,cod_destrabe,cod_molinete,num) VALUES (%s,%s,%s,%s)"
				records = []
				for x in respuestas[1]:
					if (impor < 100): 
						x.append(impor+1)
						records.append(x)
					else:
						x.append(1)
						records.append(x)

				my_cursor.executemany(sql_stuff, records)
				con.commit()
		else:
			sql_stuff = "INSERT INTO LAP_llaves_de_destrabe (cod_usuario,cod_destrabe,cod_molinete,num) VALUES (%s,%s,%s,%s)"
			records = []
			print("HOLOOOOOOO")
			print(respuestas)
			for x in respuestas[1]:

				x.append(1)
				records.append(x)
			my_cursor.executemany(sql_stuff, records)
			con.commit()
		con.close()
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en sincronizacionLlaveDestrabe "+str(e))
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
	except:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en sincronizacionLlaveDestrabe ")
		#logging.error(str(diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)		

def encontrar_llave(trama,logger):
	try: #U000167DES000167CML000167
		con = entraBD(logger)
		my_cursor = con.cursor()
		my_cursor.execute("SELECT COUNT(cod_destrabe) FROM LAP_llaves_de_destrabe WHERE cod_destrabe = '"+trama[7:16]+"'")
		results = my_cursor.fetchall()
		con.close()
		if (results[0][0]==0):
			devolver = ["no llave","no pase"]
		else :
			if (trama[7:10]=="DES"):
				devolver = ["pase","DES"]
			elif (trama[7:10]=="TRA"):
				devolver = ["pase","TRA"]
			elif (trama[7:10]=="APR"):
				devolver = ["pase","APR"]
			elif (trama[7:10]=="MUL"):
				devolver = ["pase","MUL"]
			elif (trama[7:10]=="MAS"):
				devolver = ["pase","MAS"]				
			else:
				devolver = ["error","no pase"]

	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en encontrar_llave "+str(e))
		devolver = ["error",str(e)]
	except:	
		logger.info("Error: "+str(datetime.datetime.today())+" Error en encontrar_llave ")
		devolver = ["error","no pase"]
	finally:
		return devolver	


def sincronizacionBD(logger):
	try:
		con = entraBD(logger)
		my_cursor = con.cursor()
		
		my_cursor.execute("SELECT Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,Cod_Molinete,Trama_Origen,Trama_Destino,Std_subida,fch_hora_intento FROM LAP_Transito_Datos WHERE  Std_subida = 'no subido' ") # quitar el ultimo and si se quiere subir todos hasta los que no entraron
		results = my_cursor.fetchall()
		for res in results:
			print(res)
		lista = listaVacia(results)
		if(lista == "no vacio"):
			respuestaSub = Post_datos(results[0][0], results[0][1], results[0][2], results[0][3],results[0][4],results[0][5],results[0][6],logger)

			print(respuestaSub)
			if (respuestaSub[0] == 'rojo'  or respuestaSub[0] == 'verde'):
				my_sql = "UPDATE LAP_Transito_Datos SET Std_subida = 'si subido' WHERE fch_hora_intento = '"+str(results[0][8])+"'"
				my_cursor.execute(my_sql)
				con.commit()
			elif(respuestaSub[1] == "error"):
				print("entro en error")
			else:
				print("entro en error")
				
		con.close()
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en sincronizacionBD "+str(e))
		print(e)
	except:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en sincronizacionBD ")
		print("ERRORRRR")


def Borrar_lista_caducados(logger):
	try:
		dia=datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
		dtm_fechaBorrado = datetime.date(year=dia.year,month=dia.month,day=dia.day) - configuracionParametros.limiteBorrado(logger)
		dtm_fechaBorradolog = datetime.date(year=dia.year,month=dia.month,day=dia.day) - configuracionParametros.limiteBorradoLog(logger)

		con = entraBD(logger)
		my_cursor = con.cursor()
		my_sql = "DELETE FROM LAP_Transito_Datos WHERE fch_hora_intento < '" + str(dtm_fechaBorrado) + "' AND std_subida ='si subido'"
		my_cursor.execute(my_sql)
		con.commit()
		variable = os.listdir(os.getcwd())
		#pantalla_transito_2020-02-17
		#traza_controlador_2020-02-20
		#traza_SV_2020-02-17
		#traza_SD_2020-02-17
		#traza_OSLL_2020-02-17
		for var in variable :
			if(var[0:9] == 'traza_SV_'):
				fechaZ=datetime.date(year=int(var[9:13]),month=int(var[14:16]),day=int(var[17:19]))
				if (fechaZ < dtm_fechaBorradolog):
					os.remove(var)
			elif(var[0:18] == 'traza_controlador_') :
				fechaZ=datetime.date(year=int(var[18:22]),month=int(var[23:25]),day=int(var[26:28]))
				if (fechaZ < dtm_fechaBorradolog):
					os.remove(var)
			elif(var[0:18] == 'pantalla_transito_') :
				fechaZ=datetime.date(year=int(var[18:22]),month=int(var[23:25]),day=int(var[26:28]))
				if (fechaZ < dtm_fechaBorradolog):
					os.remove(var)
			elif(var[0:9] == 'traza_SD_'):
				fechaZ=datetime.date(year=int(var[9:13]),month=int(var[14:16]),day=int(var[17:19]))
				if (fechaZ < dtm_fechaBorradolog):
					os.remove(var)
			elif(var[0:9] == 'traza_BD_'):
				fechaZ=datetime.date(year=int(var[9:13]),month=int(var[14:16]),day=int(var[17:19]))
				if (fechaZ < dtm_fechaBorradolog):
					os.remove(var)					
			elif(var[0:11] == 'traza_OSLL_'):
				fechaZ=datetime.date(year=int(var[11:15]),month=int(var[16:18]),day=int(var[19:21]))
				if (fechaZ < dtm_fechaBorradolog):
					os.remove(var)
		con.close()
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Borrar_lista_caducados "+str(e))
		print(e)
	except:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en Borrar_lista_caducados ")
		print("Error")



