# -*- coding: utf-8 -*-

# Operacio Verificacion Pase : Verifica el pase de una persona dependiendo de la trama captada por el scanner
import configuracionParametros
from puerta import puerta
import time
import generico
from controlador import controlador
import mysql.connector
import logging
import sys
import datetime
import scanner

fch_dia = generico.diahora()

nombre_log = 'traza_OVP__' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'

logging.basicConfig(filename=nombre_log,level=logging.DEBUG) #creacion del log
logging.warning("Inicio LAP_SP_OVP :"+ str(fch_dia))
logging.info(str(fch_dia) + " Conectado LAP_SP_OVP")

str_nombre_puerta = configuracionParametros.NombrePuerta()
obj_door = puerta('cerrado')
time.sleep(8) # se espera 8 segundos para que se levante la base de datos
try: # verificacion de parametros correctos para entrar a la base de datos
	con = generico.entraBD()
	my_cursor = con.cursor() 
	msg = "ok"	
except:
	msg = "error"

if (configuracionParametros.comprobarDatos()=='hay problema'): #mensaje de error de parametros 
	generico.enviaPantalla("errPAR","","blue","")
elif(msg == "error"): # mensaje de error de base de datos
	generico.enviaPantalla("errBD","","blue","")

else: # si no se encuentra algun error entonces se entra al ciclo de verificacion regular
	while(1):
		try:
			fch_dia = generico.diahora()
			nombre_log = 'traza_OVP__' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
			logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
			con.close()
			str_tipo , str_info_captada = scanner.trama_recivida() # se espera hasta recibir alguna trama del escaner
			std_estado = obj_door.estado_webservice()
			rsp = generico.internet_on()
			con = generico.entraBD()

			my_cursor = con.cursor()
			obj_controller = controlador(configuracionParametros.direccionControlador())

			status_control = obj_controller.estado_actual()
			my_cursor.execute("SELECT COUNT(cod_usuario) FROM LAP_SP_llaves_de_destrabe Where cod_destrabe ='"+ str_info_captada+"' OR cod_destrabe = '"+str_info_captada[7:16]+"'") # se busca si pertenece a alguna llave guardada en la base de datos
			results = my_cursor.fetchall()
			
			if (status_control == [configuracionParametros.emergencia(),configuracionParametros.estadoNormal(),1,0,0,0,0,0,0,0,0,0]): # caso la puerta este con el boton de emergencia
				generico.enviaPantalla("emergencia","","red","")
			elif (results[0][0] > 0): # en caso se tenga la llave guardada en la base de datos
				bol_flag = 0
				logging.info(str(generico.diahora())+" es una llave")
				if (str_info_captada[7:10]=="TRA"): # llave de bloqueo de puerta
					str_respuesta = "llave"
					generico.enviaPantalla("bloqueado","","ambar","")
					obj_controller.bloqueo()
					logging.info(str(generico.diahora())+" es una llave de trabe")
				elif (str_info_captada[7:10]=="MAS"): # llave de fin de programa
					str_respuesta = "llave"
					generico.enviaPantalla("reinicio","","white","")
					obj_controller.bloqueo()
					logging.info(str(generico.diahora())+" es una llave de maestra")
					break
				elif(str_info_captada[7:10]=="MUL"): # llave multiple
					str_respuesta = "llave"
					generico.enviaPantalla("mul","","lightblue","")
					logging.info(str(generico.diahora())+" es una llave multi")
					obj_controller.abrir()
					while(bol_flag == 0):
						tipo_pase,str_pases = scanner.trama_recivida()
						if (str_pases != str_info_captada):
							if (tipo_pase == "boardingPass"):		
								rsp = generico.internet_on()
								obj_ticket = obj_door.creacion_ticket(str_pases)
								if(rsp):
									std_estado = obj_door.estado_webservice()
									logging.info(str(generico.diahora())+ ' ESTE ES EL ESTADO DEL WEBSERVICE '+str(std_estado))
									logging.info(str(generico.diahora())+" es un ticket")
									if (std_estado[1] == "ok"):
										logging.info(str(generico.diahora())+" entro a verificacion con webservice")
										obj_door.actualizacionTipoPuerta()
										str_color,str_mensaje,str_respues = obj_ticket.verificacionWS(obj_door.str_tipoP)
										msj_estado = "    CON."
									else:
										logging.info(str(generico.diahora())+" entro en verificacion de contingencia")
										str_color,str_mensaje,str_respues = obj_ticket.VerificacionContingencia(str_nombre_puerta)
										msj_estado = "    DES."
								else:
									logging.info(str(generico.diahora())+" entro en verificacion de contingencia")
									str_color,str_mensaje,str_respues = obj_ticket.VerificacionContingencia(str_nombre_puerta)
									msj_estado = "    DES."
							elif (tipo_pase == "tuua"):
								rsp = generico.internet_on()
								if(rsp):
									logging.info(str(generico.diahora())+ ' ESTE ES EL ESTADO DEL WEBSERVICE '+str(std_estado))
									logging.info(str(generico.diahora())+" es un ticket")
									std_estado = obj_door.estado_webservice()
									
									if (std_estado[1] == "ok"):
										logging.info(str(generico.diahora())+" entro a verificacion con webservice")
										obj_door.actualizacionTipoPuerta()
										str_color,str_mensaje,str_respues = obj_door.verificacionWS(str_pases)
										std_tuua = "si subido"
										msj_estado = "    CON."
									else:
										str_color,str_mensaje,str_respues = generico.verificacionTuua(str_pases)
										std_tuua = "no subido"
										msj_estado = "    DES."
								else:
									str_color,str_mensaje,str_respues = generico.verificacionTuua(str_pases)
									std_tuua = "no subido"
									msj_estado = "    DES."
							else:
								rsp = generico.internet_on()
								if(rsp):
									logging.info(str(generico.diahora())+ ' ESTE ES EL ESTADO DEL WEBSERVICE '+str(std_estado))
									std_estado = obj_door.estado_webservice()
									if (std_estado[1] == "ok"):
										logging.info(str(generico.diahora())+" entro a verificacion con webservice")
										obj_door.actualizacionTipoPuerta()
										str_color,str_mensaje,str_respues = obj_door.verificacionWS(str_pases)
										std_tuua = "si subido"
										msj_estado = "    CON."
									else:
										cont = generico.verificacionDES(str_pases)
										if (cont == "no"):
											str_color = "rojo"
											str_mensaje = configuracionParametros.mnsjNoPase()
											str_respues ="no pase tuua"
										else:
											str_color = "verde"
											str_mensaje = configuracionParametros.mnsjPase()
											str_respues ="pase tuua"		
										std_tuua = "no subido"
										msj_estado = "    DES."
								else:
									cont = generico.verificacionDES(str_pases)
									if (cont == "no"):
										str_color = "rojo"
										str_mensaje = configuracionParametros.mnsjNoPase()
										str_respues ="no pase tuua"
									else:
										str_color = "verde"
										str_mensaje = configuracionParametros.mnsjPase()
										str_respues ="pase tuua"		
									std_tuua = "no subido"
									msj_estado = "    DES."

							if(str_respues=="pase tuua"):
								generico.enviaPantalla("pase mul",str_mensaje,str_color,msj_estado)
								fch_dia_tuua = generico.diahora()
								obj_door.contar_personas(str_nombre_puerta, fch_dia_tuua)
								obj_door.guardarTuua(str_info_captada,"paso",std_tuua,fch_dia_tuua)
								time.sleep(3)
								generico.enviaPantalla("siguiente mul","","lightblue","")
							elif(str_respues == "no pase tuua"):
								fch_dia_tuua = generico.diahora()
								generico.enviaPantalla("no pase mul",str_mensaje,str_color,msj_estado)
								obj_door.guardarTuua(str_info_captada,"no paso",std_tuua,fch_dia_tuua)
								time.sleep(3)
								generico.enviaPantalla("siguiente mul","","lightblue","")
							elif(str_respues=="pase"):
								generico.enviaPantalla("pase mul",str_mensaje,str_color,msj_estado)
								print("boleto ok")
								obj_ticket.str_dsc_intento = "paso con llave destrabe MULTI"+str(str_info_captada)
								obj_door.contar_personas(str_nombre_puerta, obj_ticket.dia_hora)
								time.sleep(3)
								generico.enviaPantalla("siguiente mul","","lightblue","")
								obj_ticket.grabado_log_pasajero()
								obj_ticket.subir_base_datos()
								del obj_ticket
							else:
								generico.enviaPantalla("no pase mul",str_mensaje,str_color,msj_estado)
								obj_ticket.str_dsc_intento += " destrabe MULTI"
								print(obj_ticket.str_dsc_intento)
								time.sleep(3)
								generico.enviaPantalla("siguiente mul","","lightblue","")
								obj_ticket.grabado_log_pasajero()
								obj_ticket.subir_base_datos()
								del obj_ticket
						else : 
							bol_flag = 1 
					obj_controller.normal()
					generico.enviaPantalla("bienvenido","","lightblue","")
				elif(str_info_captada[7:10]=="DES" or str_info_captada[0:3]=="DES"): #llave de destrabe
					rsp = generico.internet_on()
					if(rsp):
						std_estado = obj_door.estado_webservice()
						if (std_estado[1] == "ok"):
							obj_door.actualizacionTipoPuerta()
							str_color,str_mensaje,str_respuesta = obj_door.verificacionWS(str_info_captada)
							std_tuua = "si subido"
							msj_estado = "    CON."
						else:
							str_color = "verde"
							str_mensaje = configuracionParametros.mnsjPase()
							str_respuesta = "pase tuua"
							std_tuua = "no subido"
							msj_estado = "    DES."
					else:
						str_color = "verde"
						str_mensaje = configuracionParametros.mnsjPase()
						str_respuesta = "pase tuua"
						std_tuua = "no subido"
						msj_estado = "    DES."

				elif(str_info_captada[7:10]=="APR"): #llave de apertura
					str_respuesta = "llave"
					generico.enviaPantalla("desbloqueado","","ambar","")
					time.sleep(2)
					generico.enviaPantalla("bienvenido","","lightblue","")
					logging.info(str(generico.diahora())+"es llave de apertura")
					obj_controller.desbloqueo()
			elif(status_control[1] == configuracionParametros.estadoCerrado() or status_control[0] == configuracionParametros.bloqueo()):	
				generico.enviaPantalla("bloqueado","","ambar","")	
				str_respuesta = "bloqueado"
			elif (results[0][0] == 0):
				logging.info(str(generico.diahora())+" no es una llave de destrabe")

				if (str_tipo == "boardingPass"):
					rsp = generico.internet_on()
					obj_ticket = obj_door.creacion_ticket(str_info_captada)
					if(rsp):
						std_estado = obj_door.estado_webservice()
						logging.info(str(generico.diahora())+ ' ESTE ES EL ESTADO DEL WEBSERVICE '+str(std_estado))
						logging.info(str(generico.diahora())+" es un ticket")
						
						if (std_estado[1] == "ok"):
							logging.info(str(generico.diahora())+" entro a verificacion con webservice")
							obj_door.actualizacionTipoPuerta()
							str_color,str_mensaje,str_respuesta = obj_ticket.verificacionWS(obj_door.str_tipoP)
							msj_estado = "    CON."
						else:
							logging.info(str(generico.diahora())+" entro en verificacion de contingencia")
							str_color,str_mensaje,str_respuesta = obj_ticket.VerificacionContingencia(str_nombre_puerta)
							msj_estado = "    DES."
					else:
						logging.info(str(generico.diahora())+" entro en verificacion de contingencia")
						str_color,str_mensaje,str_respuesta = obj_ticket.VerificacionContingencia(str_nombre_puerta)
						msj_estado = "    DES."
				elif (str_tipo == "tuua"):
					rsp = generico.internet_on()
					if(rsp):
						logging.info(str(generico.diahora())+ ' ESTE ES EL ESTADO DEL WEBSERVICE '+str(std_estado))
						logging.info(str(generico.diahora())+" es un tuua")
						std_estado = obj_door.estado_webservice()
						
						if (std_estado[1] == "ok"):
							logging.info(str(generico.diahora())+" entro a verificacion con webservice")
							obj_door.actualizacionTipoPuerta()
							str_color,str_mensaje,str_respuesta = obj_door.verificacionWS(str_info_captada)
							std_tuua = "si subido"
							msj_estado = "    CON."
						else:
							str_color,str_mensaje,str_respuesta = generico.verificacionTuua(str_info_captada)
							std_tuua = "no subido"
							msj_estado = "    DES."
					else:
						str_color,str_mensaje,str_respuesta = generico.verificacionTuua(str_info_captada)
						std_tuua = "no subido"
						msj_estado = "    DES."
				else :
					str_respuesta = "Error"
			else:
				str_respuesta = "Error"			

			if (str_respuesta == "llave"):
				print("llave")
			elif (str_respuesta == "bloqueado"):
				print("bloqueado")			
			elif (str_respuesta == "pase"):
				generico.enviaPantalla("pase",str_mensaje,str_color,msj_estado)
				str_feedback = obj_controller.verifica_abertura_puerta()

				if str_feedback  == configuracionParametros.completoPase():
					generico.enviaPantalla("bienvenido","","lightblue","")
					obj_door.contar_personas(str_nombre_puerta, obj_ticket.dia_hora)
					obj_ticket.grabado_log_pasajero()
					obj_ticket.subir_base_datos()
				else:
					generico.enviaPantalla("bienvenido","","lightblue","")
					obj_ticket.str_dsc_intento = "no a pasado "+str_feedback
					obj_ticket.int_num_pase = 0
					obj_ticket.grabado_log_pasajero()
					obj_ticket.subir_base_datos()
			elif (str_respuesta == "pase tuua"):
				generico.enviaPantalla("pase",str_mensaje,str_color,msj_estado)
				str_feedback = obj_controller.verifica_abertura_puerta()

				fch_dia_tuua = generico.diahora()
				if str_feedback  == configuracionParametros.completoPase():
					generico.enviaPantalla("bienvenido","","lightblue","")
					obj_door.contar_personas(str_nombre_puerta, fch_dia_tuua)
					obj_door.guardarTuua(str_info_captada,"paso",std_tuua,fch_dia_tuua)
				else:
					generico.enviaPantalla("bienvenido","","lightblue","")
					obj_door.guardarTuua(str_info_captada,"no a pasado",std_tuua,fch_dia_tuua)
			elif (str_respuesta == "no pase tuua"):	
				str_feedback = obj_controller.mantener_obstaculo()
				fch_dia_tuua = generico.diahora()
				generico.enviaPantalla("no pase",str_mensaje,str_color,msj_estado)
				time.sleep(3)
				generico.enviaPantalla("bienvenido","","lightblue","")
				obj_door.guardarTuua(str_info_captada,"no paso",std_tuua,fch_dia_tuua)
			elif (str_respuesta == "no pase"):
				str_feedback = obj_controller.mantener_obstaculo()
				generico.enviaPantalla("no pase",str_mensaje,str_color,msj_estado)
				time.sleep(3)
				generico.enviaPantalla("bienvenido","","lightblue","")
				obj_ticket.grabado_log_pasajero()
				obj_ticket.subir_base_datos()
			elif(str_respuesta == "Error"):
				print("error")
			elif(str_respuesta == "error"):
				str_feedback = obj_controller.mantener_obstaculo()
				obj_ticket.grabado_log_pasajero()
				obj_ticket.subir_base_datos()
			else:
				str_feedback = obj_controller.mantener_obstaculo()
				obj_ticket.grabado_log_pasajero()
				obj_ticket.subir_base_datos()

		except Exception as e:
			print(e.args)
			print(sys.exc_info())
			logging.error(str(fch_dia) + " Error en LAP_SP_OVP en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "     " + str(e.args), exc_info=True)
		except:
			logging.error(str(fch_dia) + " Error en LAP_SP_OVP en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]), exc_info=True)
	con.close()