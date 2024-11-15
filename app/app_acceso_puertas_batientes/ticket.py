# -*- coding: utf-8 -*-

"""
ARCHIVO CLASE : ticket.py
Objetivo : Objeto ticket con metodos relacionados a la puerta
Autor : 
Fecha Creacion : 2019-05-21
Metodos :	-listaVacia(self, lista) verifica si una lista esta vacia
		****-armarcadenaotros(self, dato, numero) metodo momentaneo crea la trama , la trama solo se va a repetir
			-verificacion_webservice(self, str_tipoPuerta) verifica el ticket con el web service
			-subir_base_datos(self) sube los datos del ticket actual a la base de datos
			-VerificacionWS(self, str_tipopuerta) con la respuesta de la verificacion de web service se brinda una repsuesta
			-VerificacionContingencia(self, str_nombre_puerta) verificacion cuando no se tiene conexion con web service
			-Borrar_lista_caducados(self) Borra datos que ya pasaorn su tiempo de caducidad y ya fueron subidos al web service
			-sincronizacionBD(self) sincroniza los datos de la base de datos que aun no fueron subidos al web service
			-subida_webservice( str_trama, dtm_fechaHoraIntento, str_codPuerta, str_tipoPuerta) sube informacion
			del ticket al web service de sincronizacion al web service
"""
import json
import requests
import generico
import mysql.connector
import datetime
import configuracionParametros
import logging
import sys
import os

class ticket:
	dia_hora = datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())

	def __init__(self, flg_boarding_pass, dsc_nombrePAX, flg_ticket, cod_reserva, dsc_aero_origen, dsc_aero_destino, dsc_aerolinea, num_vuelo, fch_vuelo, dsc_clase, num_asiento, dsc_checkin, std_pasajero, std_subida, txt_trama):
		self.flg_boarding_pass = flg_boarding_pass
		self.str_dsc_nombrePAX = dsc_nombrePAX
		self.flg_ticket = flg_ticket
		self.str_cod_reserva = cod_reserva
		self.str_desc_aero_origen = dsc_aero_origen
		self.str_desc_aero_destino = dsc_aero_destino
		self.str_desc_aerolinea = dsc_aerolinea
		self.str_num_vuelo = num_vuelo
		self.dtm_fch_vuelo = fch_vuelo
		self.str_dsc_clase = dsc_clase
		self.str_num_asiento = num_asiento
		self.str_dsc_checkin = dsc_checkin
		self.str_std_pasajero = std_pasajero
		self.str_std_subida = std_subida
		self.int_num_intento = 0
		self.str_dsc_intento = "default"
		self.dtm_fch_hora_intento = datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
		self.int_num_pase = 0
		self.str_txt_trama = txt_trama

	def __del__(self):
		print("deleted")

	@staticmethod
	def listaVacia(str_lista):
		if not str_lista:
			str_result = "vacio"
		else:
			str_result = "no vacio"
		return str_result

	def entraBD(self):
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
			logging.error(str(datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except :
			logging.error(str(datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)


	"""
	PROCEDURE : subir_base _datos(self)
	Objetivo : sube los datos del ticket a la base de datos
	Autor : DSG
	Fecha Creacion : 2019-05-21
	Parametros : datos de la base de datos
				 datos del ticket
	Uso : ticket.subir_base_datos()
	"""
	def subir_base_datos(self):
		#BD = configuracionParametros.datosBD()
		try:
			#con = self.entraBD()
			con = generico.entraBD()
			my_cursor = con.cursor()
			my_cursor.execute("SELECT * FROM LAP_SP_informacion_de_ticket WHERE  flg_boarding_pass = '" + self.flg_boarding_pass + "' and txt_trama='" + self.str_txt_trama + "'")
			results = my_cursor.fetchall()
			self.intentos_entradas = len(results) + 1
			sql_stuff = "INSERT INTO LAP_SP_informacion_de_ticket (flg_boarding_pass,dsc_nombrePAX,flg_ticket,cod_reserva,dsc_aero_origen,dsc_aero_destino,dsc_aerolinea,num_vuelo,fch_vuelo,dsc_clase,num_asiento,dsc_checkin,std_pasajero,std_subida,num_intento,dsc_intento,fch_hora_intento,num_pase,txt_trama) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)"
			records = [	(self.flg_boarding_pass, self.str_dsc_nombrePAX, self.flg_ticket, self.str_cod_reserva, self.str_desc_aero_origen, self.str_desc_aero_destino, self.str_desc_aerolinea, self.str_num_vuelo, self.dtm_fch_vuelo, self.str_dsc_clase, self.str_num_asiento, self.str_dsc_checkin, self.str_std_pasajero, self.str_std_subida, self.int_num_intento, self.str_dsc_intento, self.dtm_fch_hora_intento, self.int_num_pase, self.str_txt_trama), ]
			my_cursor.executemany(sql_stuff, records)
			con.commit()
			con.close()
		except Exception as e:
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except :
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)

	def verificacionWS(self,str_tipopuerta):
		str_nombre = configuracionParametros.NombrePuerta() + " " + str(self.dtm_fch_hora_intento).split(' ')[0]
		#con = self.entraBD()
		con = generico.entraBD()
		my_cursor = con.cursor()
		my_cursor.execute("SELECT COUNT(txt_trama) FROM LAP_SP_informacion_de_ticket WHERE  txt_trama = '"+self.str_txt_trama+"' ")
		results = my_cursor.fetchall()
		self.int_num_intento = results[0][0]+1
		respuesta = []
		str_devolver = []
		str_tramacre = self.str_txt_trama
		dtm_fechare = self.dtm_fch_hora_intento - datetime.timedelta(0, 0, self.dtm_fch_hora_intento.microsecond)
		str_codpuer = configuracionParametros.NombrePuerta()
		str_tipovu = str(str_tipopuerta)

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
					str_devolver.append('pase')
					self.str_dsc_intento = "paso"

					my_cursor.execute("SELECT num_persona FROM LAP_SP_conteo_de_personas WHERE  dsc_puerta = '" + str_nombre + "' ")
					results = my_cursor.fetchall()
					lista = generico.listaVacia(results)
					if(lista == "no vacio"):
						self.int_num_pase = results[0][0]+1
					else:
						self.int_num_pase = 1
					self.str_std_subida = "si subido"
				else:
					str_devolver.append('no pase')
					self.str_dsc_intento = "no paso"
					self.int_num_pase = 0
					self.str_std_subida = "si subido"

				respuesta.append(r.status_code)
				respuesta.append(mensaje)
			else:
				str_devolver.append('lightblue')
				str_devolver.append('')
				str_devolver.append('error')
				self.str_dsc_intento = "error de exception2"
				self.int_num_pase = 0	
		except requests.exceptions.ConnectionError as e:
			str_devolver = []
			str_devolver.append('lightblue')
			str_devolver.append('')
			str_devolver.append('error')
			self.str_dsc_intento = "error de conexion"
			self.int_num_pase = 0
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except Exception as e:
			str_devolver = []
			str_devolver.append('lightblue')
			str_devolver.append('')
			str_devolver.append('error')
			self.str_dsc_intento = "error de exception1"
			self.int_num_pase = 0
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor" ,exc_info=True)
		except:
			str_devolver = []
			str_devolver.append('lightblue')
			str_devolver.append('')
			str_devolver.append('error')
			self.str_dsc_intento = "error de exception2"
			self.int_num_pase = 0
			logging.error(str(generico.diahora()) + " Error en puerta en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
		finally:
			con.close()
			return str_devolver



	"""
	PROCEDURE : VerificacionContingencia(self, numero_puerta, tipopuerta)
	Objetivo : guarda los datos de los tickets que pasan por la puerta cuando no se tiene conexion con el web service
	Autor : DSG
	Fecha Creacion : 2019-05-21
	Parametros : datos de la base de datos
				 datos del ticket
	Uso : ticket.VerificacionContingencia("L03")
	"""
	def VerificacionContingencia(self, str_nombre_puerta):
		try:

			str_diadp = str(self.dtm_fch_hora_intento).split(' ')
			print(configuracionParametros.horaContingenciatemprano())
			print(configuracionParametros.horaContingenciatarde())
			str_diada = str(self.dtm_fch_hora_intento+configuracionParametros.horaContingenciatemprano()).split(' ')
			str_diadd = str(self.dtm_fch_hora_intento-configuracionParametros.horaContingenciatarde()).split(' ')
			str_nombre = str_nombre_puerta + " " + str_diadp[0]
			str_respuesta = []

			#con = self.entraBD()
			con = generico.entraBD()
			my_cursor = con.cursor()
			print('ENTRO CONTINGENCIA PASO 3')
			my_cursor.execute("SELECT COUNT(txt_trama) FROM LAP_SP_informacion_de_ticket WHERE  txt_trama = '"+self.str_txt_trama+"' ")
			results = my_cursor.fetchall()
			self.int_num_intento = results[0][0]+1

			my_cursor.execute("SELECT num_pase FROM LAP_SP_informacion_de_ticket WHERE txt_trama = '"+self.str_txt_trama+"' and num_pase > 0")
			results = my_cursor.fetchall()
			lista = generico.listaVacia(results)
			if (lista == "vacio"):

				if (configuracionParametros.AeropuertoActual() == self.str_desc_aero_origen):

					if((str(self.dtm_fch_vuelo) == str_diadp[0]) or (str(self.dtm_fch_vuelo) == str_diadd[0]) or (str(self.dtm_fch_vuelo) == str_diada[0])):
						if (str(self.str_std_pasajero)!= '6'):
							str_respuesta.append('verde')
							str_respuesta.append(configuracionParametros.mnsjPase())
							str_respuesta.append('pase')
							my_cursor.execute("SELECT num_persona FROM LAP_SP_conteo_de_personas WHERE  dsc_puerta = '" + str_nombre + "' ")
							results = my_cursor.fetchall()
							lista = generico.listaVacia(results)
							if(lista == "no vacio"):
								self.int_num_pase = results[0][0]+1
							else:
								self.int_num_pase = 1
						else :
							self.str_dsc_intento = "no por pasajero de transito"
							str_respuesta.append('ambar')
							str_respuesta.append(configuracionParametros.mnsjTransito())
							str_respuesta.append('no pase')
						


						self.str_dsc_intento = "paso"
					else:
						self.str_dsc_intento = "no paso fuera rango de hora"
						str_respuesta.append('rojo')
						str_respuesta.append(configuracionParametros.mnsjNoPaseHora())
						str_respuesta.append('no pase')

				else:
					self.str_dsc_intento = "no paso aeropuerto actual equivocado"
					str_respuesta.append('rojo')
					str_respuesta.append(configuracionParametros.mnsjNoPaseAeropuerto())
					str_respuesta.append('no pase')
			else:
				self.str_dsc_intento = "boleto utilizado previamente"
				n = len(results)
				self.int_num_pase = results[n-1][0]
				str_respuesta.append('rojo')
				str_respuesta.append(configuracionParametros.mnsjNoPaseUtilizado())
				str_respuesta.append('no pase')

			con.close()
		except Exception as e:
			self.str_dsc_intento = "sucedio error"
			str_respuesta = []
			str_respuesta.append('lightblue')
			str_respuesta.append('')
			str_respuesta.append('error')
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except :
			self.str_dsc_intento = "sucedio error"
			str_respuesta = []
			str_respuesta.append('lightblue')
			str_respuesta.append('')
			str_respuesta.append('error')
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
		finally:
			return str_respuesta
	"""
	PROCEDURE : VerificacionError(self)
	Objetivo : guarda los datos de los tickets que pasan por la puerta cuando no se tiene conexion con el web service
	Autor : DSG
	Fecha Creacion : 2019-05-21
	Parametros : datos de la base de datos
				 datos del ticket
	Uso : ticket.VerificacionContingencia("L03")
	"""
	def VerificacionError(self,str_nombre_puerta):
		try:
			str_diadp = str(self.dtm_fch_hora_intento).split(' ')
			str_diada = str(self.dtm_fch_hora_intento-configuracionParametros.horaContingenciatemprano()).split(' ')
			str_diadd = str(self.dtm_fch_hora_intento+configuracionParametros.horaContingenciatarde()).split(' ')
			str_nombre = str_nombre_puerta + " " + str_diadp[0]
			#con = self.entraBD()
			con = generico.entraBD()
			listare = []
			my_cursor = con.cursor()

			my_cursor.execute("SELECT COUNT(txt_trama) FROM LAP_SP_informacion_de_ticket WHERE txt_trama = '"+self.str_txt_trama+"' and num_pase > 0 and std_subida = 'si subido'")
			results = my_cursor.fetchall()
			if (results[0][0] == 0):
				my_cursor.execute("SELECT dsc_intento FROM LAP_SP_informacion_de_ticket WHERE txt_trama = '"+self.str_txt_trama+"' and num_pase = 0 and std_subida = 'si subido'")
				results2 = my_cursor.fetchall()
				lista2 = generico.listaVacia(results2)

				if (lista2 == "vacio"):
					str_respuesta = "no pase"
				else :
					num = len(results2)
					estado_anterior = results2[num-1][0]
					if (estado_anterior == "no paso "+configuracionParametros.regreso()):
						str_respuesta = "pase"
					elif (estado_anterior == "no paso "+configuracionParametros.incompletoPase()):
						str_respuesta = "pase"
					elif (estado_anterior == "no paso "+configuracionParametros.bloqueo()):
						str_respuesta = "pase"
					elif (estado_anterior == "no paso "+configuracionParametros.pasoInocrrecto()):
						str_respuesta = "pase"
					else :
						str_respuesta = "no pase"
			else :
				str_respuesta = "no pase"
			con.close()
		except Exception as e:
			self.str_dsc_intento = "sucedio error"
			str_respuesta = 'error'
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except :
			self.str_dsc_intento = "sucedio error"
			str_respuesta = 'error'
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor",exc_info=True)
		finally:
			return str_respuesta
	"""
	PROCEDURE : Borrar_lista_caducados(self)
	Objetivo : borra los datos que ya pasaron el tiempo de guardado y ya fueron subidos
	Autor : DSG
	Fecha Creacion : 2019-05-21
	Parametros : datos de la base de datos
	Uso : ticket.Borrar_lista_caducados()
	"""
	@classmethod
	def Borrar_lista_caducados(self):
		try:
			dia=datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
			dtm_fechaBorrado = datetime.date(year=dia.year,month=dia.month,day=dia.day) - configuracionParametros.limiteBorrado()
			dtm_fechaBorradolog = datetime.date(year=dia.year,month=dia.month,day=dia.day) - configuracionParametros.limiteBorradoLog()

			con = generico.entraBD()
			my_cursor = con.cursor()
			my_sql = "DELETE FROM LAP_SP_informacion_de_ticket WHERE fch_hora_intento < '" + str(dtm_fechaBorrado) + "' AND std_subida ='si subido'"
			my_cursor.execute(my_sql)
			con.commit()
			my_sql = "DELETE FROM LAP_SP_informacion_de_ticket WHERE fch_hora_intento < '" + str(dtm_fechaBorrado) + "' AND std_subida ='no subido' AND num_pase = 0"
			my_cursor.execute(my_sql)
			con.commit()
			my_sql = "DELETE FROM LAP_SP_conteo_de_personas WHERE fch_creacion < '" + str(dtm_fechaBorrado) + "' "
			my_cursor.execute(my_sql)
			con.commit()
			my_sql = "DELETE FROM LAP_SP_estado_de_puerta WHERE fch_cambio < '" + str(dtm_fechaBorrado) + "' "
			my_cursor.execute(my_sql)
			con.commit()
			variable = os.listdir(os.getcwd())
			for var in variable :
				if((var[0:11] == 'traza_OVP__') or (var[0:11] == 'traza_OSD__') or (var[0:11] == 'traza_OSLL_') or (var[0:11] == 'traza_OBD__') or (var[0:11] == 'traza_OEIP_') or (var[0:11] == 'traza_ORIP_')):
					fechaZ=datetime.date(year=int(var[11:15]),month=int(var[16:18]),day=int(var[19:21]))
					if (fechaZ < dtm_fechaBorradolog):
						os.remove(var)
			con.close()

		except Exception as e:
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except:
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

	@staticmethod
	def subida_webservice( str_trama, dtm_fechaHoraIntento, str_codPuerta, str_tipoPuerta):
		try:
			respuesta = []
			dtm_fechare = dtm_fechaHoraIntento - datetime.timedelta(0, 0, dtm_fechaHoraIntento.microsecond)
			payload = {'Trama': str_trama, 'FechaRegistro': str(dtm_fechare), 'CodPuerta': str_codPuerta, 'TipoVuelo': str_tipoPuerta}

			r = requests.post(configuracionParametros.urlSubirBDaWS(), json=payload,timeout=configuracionParametros.urlSubirBDaWS_timeout())
			if r.status_code == 200:
				text = r.json()
				mensaje = text['respuesta']
				respuesta.append(r.status_code)
				respuesta.append(mensaje)
			else:
				text = r.json()
				print("printeo del texto")
				print(text)
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

	"""
	PROCEDURE : sincronizacionBD(self)
	Objetivo : cambia los datos de algunos tickets si son subidos al web service
	Autor : DSG
	Fecha Creacion : 2019-05-21
	Parametros : datos de la base de datos 
	Uso : ticket.sincronizacionBD() 
	"""
	@classmethod
	def sincronizacionBD(self):
		try:
			con = generico.entraBD()
			my_cursor = con.cursor()
			#my_cursor.execute("SELECT txt_trama,fch_hora_intento FROM LAP_SP_informacion_de_ticket WHERE  (std_subida = 'no subido' AND num_pase > 0 AND dsc_intento = 'paso') OR (std_subida = 'no subido' AND dsc_intento like 'no a pasado%')") # quitar el ultimo and si se quiere subir todos hasta los que no entraron
			my_cursor.execute("SELECT txt_trama,fch_hora_intento FROM LAP_SP_informacion_de_ticket WHERE  std_subida = 'no subido' ") # quitar el ultimo and si se quiere subir todos hasta los que no entraron
			results = my_cursor.fetchall()
			my_cursor.execute("SELECT Tip_Vuelo FROM LAP_SP_estado_de_puerta WHERE  Cod_Molinete = '"+configuracionParametros.NombrePuerta()+"'") # quitar el ultimo and si se quiere subir todos hasta los que no entraron
			results2 = my_cursor.fetchall()
			print(results2)
			for res in results:
				print(res)
			lista = generico.listaVacia(results)
			lista2 = generico.listaVacia(results2)
			if(lista == "no vacio"):
				if(lista2 == "no vacio"):
					#( str_trama, dtm_fechaHoraIntento, str_codPuerta, str_tipoPuerta)
					respuestaSub = self.subida_webservice(results[0][0], results[0][1], configuracionParametros.NombrePuerta(), results2[0][0])

					print(respuestaSub)

					if (respuestaSub[0] == 200):
						my_sql = "UPDATE LAP_SP_informacion_de_ticket SET std_subida = 'si subido' WHERE txt_trama = '"+str(results[0][0])+"' AND fch_hora_intento = '"+str(results[0][1])+"'"
						my_cursor.execute(my_sql)
						con.commit()
					elif(respuestaSub[1] == "error"):
						print("entro en error")
					else:
						print("entro en error")
				else:
					logging.error(str(generico.diahora()) + " Error en ticket en linea :      base de datos LAP_SP_estado_de_puerta esta vacia o no se encuentra nada con el nombre de la puerta" )
			con.close()
		except Exception as e:
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args),exc_info=True)
		except:
			logging.error(str(generico.diahora()) + " Error en ticket en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)

	def grabado_log_pasajero(self):
		logging.info(str(generico.diahora()) + " Nombre 		: " + self.str_dsc_nombrePAX)
		logging.info(str(generico.diahora()) + " Trama 		: " + self.str_txt_trama)
		logging.info(str(generico.diahora()) + " Intento 		: " + str(self.int_num_intento))
		logging.info(str(generico.diahora()) + " Hora Intento 	: " + str(self.dtm_fch_hora_intento))
		logging.info(str(generico.diahora()) + " Razon		: " + self.str_dsc_intento)
		logging.info(str(generico.diahora()) + " Numero de pase	: " + str(self.int_num_pase))

	def grabado_log(self,str_mensaje):
		logging.info(str(generico.diahora()) + " " + str_mensaje )




