"""
ARCHIVO CLASE : puerta.py
Objetivo : Objeto puerta con metodos relacionados a la puerta
Autor : 
Fecha Creacion : 2019-05-21
Metodos :	-creacion_ticket(self) crea el ticket con los datos captados de la trama
			-listaVacia(self, str_lista) verifica si una str_lista esta vacia
		****-armarcadenaotros(self, dato, numero) metodo momentaneo crea la trama , la trama solo se va a repetir
			-abrir_puerta(self) metodo encargado de abrir puerta
			-cerrar_puerta(self) metodo encargado de cerrar puerta
			-mostrar_mensaje(self, mensaje) muestra mensaje de situacion del ticket
			-estado_webservice(self) verifica la conexion con el web esrvice con el metodo get
			-estado_webservice_llave_destrabe(self) recibe todas las llaves de destrabe con el metodo get
			-sincronizacionLlaveDestrabe(self) sincroniza los datos captados del web service a la base de datos
			-sincronizacionPuerta(self) sincroniza el tipo de puerta 
			-actualizacionTipoPuerta(self) actualiza el estado de la puerta
			-contar_personas(self, str_numero_puerta, str_llegadaTK) cuenta las personas que pasan por dicha puerta cada dia
			-cuantas_personas(self, str_numero_puerta, str_llegadaTK) devuelve el numero de personas que pasaron por la puerta en ese dia
"""
import requests
import mysql.connector
import datetime
import configuracionParametros
from ticket import ticket
import logging
import sys
import time
import socket
import generico

class puerta:
	str_tipoP = 'O'
	def __init__(self, estado):
		self.estado = estado

	def creacion_ticket(self,x):
		
		str_txt_trama = x
		fecha_cal = datetime.date(year=datetime.date.today().year,month = 1 , day=1) + datetime.timedelta(days=(int(str_txt_trama[44:47])-1))
		str_std_subida = 'no subido'

		flg_boarding_pass=str_txt_trama[0:2]
		dsc_nombrePAX=generico.limpiaApostrofe(str_txt_trama[2:22])
		flg_ticket=str_txt_trama[22]
		cod_reserva=str_txt_trama[23:30]
		dsc_aero_origen=str_txt_trama[30:33]
		dsc_aero_destino=str_txt_trama[33:36]
		dsc_aerolinea=str_txt_trama[36:39]
		num_vuelo=str_txt_trama[39:44]
		fch_vuelo=str(fecha_cal)
		dsc_clase=str_txt_trama[47]
		num_asiento=str_txt_trama[48:52]
		dsc_checkin=str_txt_trama[52:57]
		std_pasajero=str_txt_trama[57]
		std_subida=str_std_subida
		txt_trama=generico.limpiaApostrofe(str_txt_trama)
		respuesta = ticket(flg_boarding_pass, dsc_nombrePAX, flg_ticket, cod_reserva, dsc_aero_origen, dsc_aero_destino, dsc_aerolinea, num_vuelo, fch_vuelo, dsc_clase, num_asiento, dsc_checkin, std_pasajero, std_subida, txt_trama)
		return respuesta
	
	@classmethod
	def estado_webservice(self):
		respuesta = []
		try:
			print("Este es el nombre del webservice &&&&&&&")
			print(configuracionParametros.urlVerificarRed)
			r = requests.get(configuracionParametros.urlVerificarRed(), timeout=configuracionParametros.urlVerificarRed_timeout())

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
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except Exception as e:
			print('error 2')
			print(e)
			respuesta.append(777)
			respuesta.append("no hay servicio")
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except:
			print('error 3')
			print(sys.exc_info()[0])
			respuesta.append(888)
			respuesta.append("no hay servicio")
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

		finally:
			return respuesta

	def estado_webservice_llave_destrabe(self):
		respuesta = []
		try:
			print("antes del webservice de llaves")
			r = requests.get(configuracionParametros.urlVerificarLlavesDestrabe(), timeout=configuracionParametros.urlVerificarLlavesDestrabe_timeout())  # guarda el valor que me da
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
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except Exception as e:
			respuesta = []
			respuesta.append(777)
			respuesta.append("no hay servicio")
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except:
			respuesta = []
			respuesta.append(888)
			respuesta.append("no hay servicio")
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

		finally:
			return respuesta

	"""
	PROCEDURE : sincronizacionLlaveDestrabe(self)
	Objetivo : llama a los datos de la llave de destrabe mediante el metodo estado_webservice_llave_destrabe() y
				borra los datos antiguos , para poner los datos actuales
	Autor : DSG
	Fecha Creacion : 2019-05-21
	Parametros : datos de la base de datos 
	Uso : puerta.sincronizacionLlaveDestrabe() 
	"""
	def sincronizacionLlaveDestrabe(self):
		try:
			con = generico.entraBD()
			my_cursor = con.cursor()
			my_cursor.execute("SELECT num FROM LAP_SP_llaves_de_destrabe WHERE num != 322")
			results = my_cursor.fetchall()

			lista = generico.listaVacia(results)
			respuestas = self.estado_webservice_llave_destrabe()
			#lista2 = self.listaVacia(respuestas)
			if(lista == "no vacio"):
				impor = results[len(results)-1][0]
				#if (lista2 == "no vacio"):
				if (respuestas[0] == 200):
					my_sql = "DELETE FROM LAP_SP_llaves_de_destrabe WHERE num = " + str(impor)
					my_cursor.execute(my_sql)
					con.commit()
					sql_stuff = "INSERT INTO LAP_SP_llaves_de_destrabe (cod_usuario,cod_destrabe,cod_molinete,num) VALUES (%s,%s,%s,%s)"
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
				sql_stuff = "INSERT INTO LAP_SP_llaves_de_destrabe (cod_usuario,cod_destrabe,cod_molinete,num) VALUES (%s,%s,%s,%s)"
				records = []
				for x in respuestas[1]:
					x.append(1)
					records.append(x)
				my_cursor.executemany(sql_stuff, records)
				con.commit()
			con.close()
		except Exception as e:
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except:
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

	def sincronizacionPuerta(self):
		respuesta = []
		try:
			r = requests.get(configuracionParametros.urlVerificarEstadoPuerta(), timeout=configuracionParametros.urlVerificarEstadoPuerta_timeout())
			if r.status_code == 200:
				estado = r.json()
				estado2 = estado['Tip_Vuelo']
				respuesta.append(r.status_code)
				respuesta.append(estado2)
			else:
				respuesta.append(999)
				respuesta.append("no hay servicio")
		except requests.exceptions.ConnectionError as e:
			respuesta.append(404)
			respuesta.append("no hay servicio")
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except Exception as e:
			respuesta.append(777)
			respuesta.append("no hay servicio")
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except:
			respuesta.append(888)
			respuesta.append("no hay servicio")
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

		finally:
			return respuesta

	def actualizacionTipoPuerta(self):
		try:
			#con = self.entraBD()
			con = generico.entraBD()
			my_cursor = con.cursor()

			try:
				r = requests.get(configuracionParametros.urlVerificarEstadoPuerta(), timeout=configuracionParametros.urlVerificarEstadoPuerta_timeout())
				if r.status_code == 200:
					estado = r.json()
					respuesta = "ok" 
				else:
					respuesta = 'Error404'
			except:
				respuesta = "Error404"
				logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

			finally:
				#respuesta = self.sincronizacionPuerta()
			
				print("ACTUALIZACION DE TIPO DE PUERTA")
				print(respuesta)
				
				if (respuesta == "ok"): # si hay servicio
					#print("*****SE IMPRIME EL ESTADO DEL WEB SERVICE ESTADO PUERTA*****")
					#print(estado)

					datos = configuracionParametros.datosBD()
					if(datos!="Error404"):
						if (datos[1]!=estado['Dsc_DBUser']):#16
							print("Error en el usuario")
						if (datos[2]!=estado['Dsc_DBPassword']):#16
							print("Error en el password")
						if (datos[3]!=estado['Dsc_DBName']):#16
							print("Error en el nombre de BD")
					else:
						print("ERROR de captado de dato")

					str_mensaje = "hay conexion"

					my_cursor.execute("SELECT COUNT(Cod_Molinete) FROM LAP_SP_estado_de_puerta WHERE  Cod_Molinete = '"+ configuracionParametros.NombrePuerta()+"' ")
					results = my_cursor.fetchall()
					self.str_tipoP = estado['Tip_Vuelo']
					

					if(results[0][0] != 0):
						my_sql = "UPDATE LAP_SP_estado_de_puerta SET Cod_Molinete = '"+generico.nullDetector(estado['Cod_Molinete'])+ "', Dsc_Ip ='"+generico.nullDetector(estado['Dsc_Ip'])+"', Dsc_Molinete ='"+generico.nullDetector(estado['Dsc_Molinete'])
						my_sql += "', Tip_Documento ='"+generico.nullDetector(estado['Tip_Documento'])+ "', Tip_Vuelo ='"+generico.nullDetector(estado['Tip_Vuelo'])+ "', Tip_Acceso ='"+generico.nullDetector(estado['Tip_Acceso'])+ "', Tip_Estado ='"+generico.nullDetector(estado['Tip_Estado'])
						my_sql += "', Log_Usuario_Mod ='"+generico.nullDetector(estado['Log_Usuario_Mod'])+ "', Log_Fecha_Mod ='"+generico.nullDetector(estado['Log_Fecha_Mod'])+ "', Log_Hora_Mod ='"+generico.nullDetector(estado['Log_Hora_Mod'])
						my_sql += "', Fch_Sincroniza ='"+generico.nullDetector(estado['Fch_Sincroniza'])+ "', Est_Master ='"+generico.nullDetector(estado['Est_Master'])+ "', Dsc_DBName ='"+generico.nullDetector(estado['Dsc_DBName'])+ "', Dsc_DBUser ='"+generico.nullDetector(estado['Dsc_DBUser'])
						my_sql += "', Dsc_DBPassword ='"+generico.nullDetector(estado['Dsc_DBPassword'])+ "', Tip_Molinete ='"+generico.nullDetector(estado['Tip_Molinete'])+ "', Flg_Sincroniza ='"+generico.nullDetector(estado['Flg_Sincroniza'])+ "', cod_grupo ='"+generico.nullDetector(estado['cod_grupo'])
						my_sql += "', dsc_estado ='"+str_mensaje+"', fch_cambioDB ='"+ str(generico.diahora())+"' WHERE Cod_Molinete = '"+configuracionParametros.NombrePuerta()+"' " 
						my_cursor.execute(my_sql)
						con.commit()

					elif(results[0][0] == 0):
						sql_stuff = "INSERT INTO LAP_SP_estado_de_puerta (Cod_Molinete,Dsc_Ip,Dsc_Molinete,Tip_Documento,Tip_Vuelo,Tip_Acceso,Tip_Estado,Log_Usuario_Mod,Log_Fecha_Mod,Log_Hora_Mod,Fch_Sincroniza"
						sql_stuff += ",Est_Master,Dsc_DBName,Dsc_DBUser,Dsc_DBPassword,Tip_Molinete,Flg_Sincroniza,cod_grupo,dsc_estado,fch_cambioDB) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)"
						records = [(generico.nullDetector(estado['Cod_Molinete']),generico.nullDetector(estado['Dsc_Ip']),generico.nullDetector(estado['Dsc_Molinete']),generico.nullDetector(estado['Tip_Documento']),
									generico.nullDetector(estado['Tip_Vuelo']),generico.nullDetector(estado['Tip_Acceso']),generico.nullDetector(estado['Tip_Estado']),generico.nullDetector(estado['Log_Usuario_Mod']), #8
									 generico.nullDetector(estado['Log_Fecha_Mod']),generico.nullDetector(estado['Log_Hora_Mod']),generico.nullDetector(estado['Fch_Sincroniza']),generico.nullDetector(estado['Est_Master']),
									 generico.nullDetector(estado['Dsc_DBName']),generico.nullDetector(estado['Dsc_DBUser']),generico.nullDetector(estado['Dsc_DBPassword']), #7
									 generico.nullDetector(estado['Tip_Molinete']),generico.nullDetector(estado['Flg_Sincroniza']),generico.nullDetector(estado['cod_grupo']),generico.nullDetector(str_mensaje),generico.diahora())] #5
						my_cursor.executemany(sql_stuff, records)
						con.commit()

				else: #no hay servicio
					str_mensaje = "no hay conexion"

					my_cursor.execute("SELECT Tip_Vuelo FROM LAP_SP_estado_de_puerta WHERE  Cod_Molinete = '"+ configuracionParametros.NombrePuerta()+"' ")
					results = my_cursor.fetchall()
					lista = generico.listaVacia(results)
					if(lista == "no vacio"):
						self.str_tipoP = results[0][0]
						my_sql = "UPDATE LAP_SP_estado_de_puerta SET fch_cambioDB = '"+ str(generico.diahora())+"' , dsc_estado = '"+str_mensaje+"' WHERE Cod_Molinete = '"+configuracionParametros.NombrePuerta()+"' "
						my_cursor.execute(my_sql)
						con.commit()
					else:

						sql_stuff = "INSERT INTO LAP_SP_estado_de_puerta (Cod_Molinete,Dsc_Ip,Dsc_Molinete,Tip_Documento,Tip_Vuelo,Tip_Acceso,Tip_Estado,Log_Usuario_Mod,Log_Fecha_Mod,Log_Hora_Mod,Fch_Sincroniza"
						sql_stuff = ",Est_Master,Dsc_DBName,Dsc_DBUser,Dsc_DBPassword,Tip_Molinete,Flg_Sincroniza,cod_grupo,dsc_estado,fch_cambioDB) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)"
						
						records = [(configuracionParametros.NombrePuerta(),'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-',str_mensaje,generico.diahora())]
						my_cursor.executemany(sql_stuff, records)
						con.commit()
				con.close()
		except Exception as e:
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except:
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

	"""
	PROCEDURE : contar_personas(self, numero_puerta, llegadaTK)
	Objetivo : cuenta las persona que pasan por la puerta en un dia
	Autor : DSG
	Fecha Creacion : 2019-05-21
	Parametros : numero_puerta es lo que diferencia una puerta de las otras
				 llegadaTK es la hora a la cual el ticket paso por la puerta
	Uso : puerta.contar_personas("L03", "2019-05-15 12:53:12")
	"""
	def contar_personas(self, str_numero_puerta, fch_llegadaTK):
		try:
			str_dia = str(fch_llegadaTK).split(' ')
			str_nombre = str_numero_puerta +" "+ str(str_dia[0])
			unixtime = time.mktime(fch_llegadaTK.timetuple())

			#con = self.entraBD()
			con = generico.entraBD()
			my_cursor = con.cursor()
			#sql_stuff = "INSERT INTO LAP_SP_enviar_pases (dsc_puerta,dsc_estado_pase,fch_pase,fch_unix) VALUES (%s,%s,%s,%s)"
			#records = [	(configuracionParametros.NombrePuerta(),"no enviado",fch_llegadaTK,unixtime), ]
			#my_cursor.executemany(sql_stuff, records)
			#con.commit()

			my_cursor.execute("SELECT num_persona FROM LAP_SP_conteo_de_personas WHERE  dsc_puerta = '" + str_nombre + "' ")
			results = my_cursor.fetchall()
			lista = generico.listaVacia(results)
			if(lista == "no vacio"):
				my_sql = "UPDATE LAP_SP_conteo_de_personas SET num_persona = " + str(results[0][0]+1) + " WHERE dsc_puerta = '" + str_nombre + "'"
				my_cursor.execute(my_sql)
				con.commit()
			else:
				sql_stuff = "INSERT INTO LAP_SP_conteo_de_personas (dsc_puerta,num_persona,fch_creacion) VALUES (%s,%s,%s)"
				records = [	(str_nombre, 1,str_dia[0]), ]
				my_cursor.executemany(sql_stuff, records)
				con.commit()
			con.close()
		except Exception as e:
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except:
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

	"""
	PROCEDURE : cuantas_personas(self, numero_puerta, llegadaTK)
	Objetivo : devuelve cuantas personas pasaron por la puerta en el dia que se consulta
	Autor : DSG
	Fecha Creacion : 2019-05-21
	Parametros : numero_puerta es lo que diferencia una puerta de las otras
				 llegadaTK es la hora a la cual el ticket paso por la puerta
	Uso : puerta.contar_personas("L03", "2019-05-15 12:53:12")
	"""
	def cuantas_personas(self, str_numero_puerta, str_llegadaTK):
		try:
			str_nombre = str_numero_puerta + " " + str(str_llegadaTK.split(' ')[0])

			#con = self.entraBD()
			con = generico.entraBD()
			my_cursor = con.cursor()
			my_cursor.execute("SELECT num_persona FROM LAP_SP_conteo_de_personas WHERE  dsc_puerta = '" + str_nombre +"' ")
			results = my_cursor.fetchall()
			lista = generico.listaVacia(results)
			if(lista == "no vacio"):
				respuesta = results[0][0]
			else:
				respuesta = 0
			con.close()
		except Exception as e:
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
			respuesta = 0
		except:
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
			respuesta = 0
		finally:
			return respuesta

	def verificacionWS(self,trama):
		#str_nombre = configuracionParametros.NombrePuerta() + " " + str(self.dtm_fch_hora_intento).split(' ')[0]
		#con = self.entraBD()

		#self.int_num_intento = results[0][0]+1
		respuesta = []
		str_devolver = []
		str_tramacre = trama
		dtm_fechare = generico.diahora()
		str_codpuer = configuracionParametros.NombrePuerta()
		str_tipovu = str(self.str_tipoP)

		payload = {'Trama': str_tramacre, 'FechaRegistro': str(dtm_fechare), 'CodPuerta': str_codpuer, 'TipoVuelo': str_tipovu}
		try:
			r = requests.post(configuracionParametros.urlVerificarWebservice(), json=payload,timeout=configuracionParametros.urlVerificarWebservice_timeout())
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


				respuesta.append(r.status_code)
				respuesta.append(mensaje)
			else:
				str_devolver.append('lightblue')
				str_devolver.append('')
				str_devolver.append('error')
				#self.str_dsc_intento = "error de exception2"
				#self.int_num_pase = 0	
		except requests.exceptions.ConnectionError as e:
			str_devolver = []
			str_devolver.append('lightblue')
			str_devolver.append('')
			str_devolver.append('error')
			#self.str_dsc_intento = "error de conexion"
			#self.int_num_pase = 0
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except Exception as e:
			str_devolver = []
			str_devolver.append('lightblue')
			str_devolver.append('')
			str_devolver.append('error')
			#self.str_dsc_intento = "error de exception1"
			#self.int_num_pase = 0
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor" ,exc_info=True)
		except:
			str_devolver = []
			str_devolver.append('lightblue')
			str_devolver.append('')
			str_devolver.append('error')
			#self.str_dsc_intento = "error de exception2"
			#self.int_num_pase = 0
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
		finally:
			return str_devolver

	def guardarTuua(self,str_txt_trama,dsc_intento,std_subida,dtm_fch_hora_intento):
		try:
			con = generico.entraBD()
			my_cursor = con.cursor()
			str_nombre = configuracionParametros.NombrePuerta() + " " + str(dtm_fch_hora_intento).split(' ')[0]

			if (dsc_intento == "paso"):
				my_cursor.execute("SELECT num_persona FROM LAP_SP_conteo_de_personas WHERE  dsc_puerta = '" + str_nombre + "' ")
				results = my_cursor.fetchall()
				lista = generico.listaVacia(results)
				if(lista == "no vacio"):
					num_pase = results[0][0]+1
				else:
					num_pase = 1
			else:
				num_pase = 0


			my_cursor.execute("SELECT * FROM LAP_SP_informacion_de_ticket WHERE txt_trama='" + str_txt_trama + "'")
			results = my_cursor.fetchall()
			intentos_entradas = len(results) + 1
			sql_stuff = "INSERT INTO LAP_SP_informacion_de_ticket (std_subida,num_intento,dsc_intento,fch_hora_intento,num_pase,txt_trama) VALUES (%s,%s,%s,%s,%s,%s)"
			records = [	(std_subida,intentos_entradas, dsc_intento, dtm_fch_hora_intento, num_pase, str_txt_trama), ]
			my_cursor.executemany(sql_stuff, records)
			con.commit()
			con.close()
		except Exception as e:
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except :
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)

