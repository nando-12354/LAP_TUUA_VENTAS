#import xmlrpclib
import xmlrpc.client as xmlrpclib

import datetime
import configuracionParametros
import logging
import time
import generico
class controlador:

	def __init__(self, direccion):
		self.direccion = direccion
		self.proxycon = xmlrpclib.ServerProxy(direccion)

	def restart(self):
		data = self.proxy.Sendrestart()

	def grabadolog(self,temp):
		dia=datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
		nombre_log = 'traza_controlador_' + str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) +'.log'
		logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
		logging.info(str(generico.diahora()) + " Informacion del estado :     " + str(temp))

	def estadoObstaculo(self):
		std_obstaculo = self.proxycon.GetSetAEA()
		return std_obstaculo

	def desbloqueo(self):
		data = self.proxycon.SetModeAEA(configuracionParametros.estadoAbrir())
		data = self.proxycon.SetModeAEA(configuracionParametros.estadoNormal())

	def abrir(self):
		data = self.proxycon.SetModeAEA(configuracionParametros.estadoAbrir())

	def normal(self):
		data = self.proxycon.SetModeAEA(configuracionParametros.estadoNormal())
	
	def bloqueo(self):
		data = self.proxycon.SetModeAEA(configuracionParametros.estadoCerrado())

	def estado_actual(self):
		data = self.proxycon.GetSetAEA(configuracionParametros.statusQuery())
		return data

	def presencia_puerta(self):
		data = self.proxycon.GetSetAEA(configuracionParametros.statusQuery())
		for i in range(1, 8):
			time.sleep(configuracionParametros.muestreoPuerta())
			data = self.proxycon.GetSetAEA(configuracionParametros.statusQuery())
			print(data)
			if data[8] == 1 :
				msg = "presencia"
				break
			else :
				msg = "sin presencia"
		return msg


	def verifica_abertura_puerta(self):

		data = self.proxycon.GetSetAEA(configuracionParametros.autorizaPase(),"Se espera que abrir puerta")
		for i in range(1, configuracionParametros.rangoMuestreo()):
			time.sleep(configuracionParametros.muestreoPuerta())
			data = self.proxycon.GetSetAEA(configuracionParametros.statusQuery())
			print(data)

			if data[0] == configuracionParametros.completoPase():
				temp = data[0]
				break
			if data[0] == configuracionParametros.regreso(): 
				temp = data[0]
				break
			if data[0] == configuracionParametros.incompletoPase():
				temp = data[0]
				break
			if data[0] == configuracionParametros.bloqueo():
				temp = data[0]
				break
			if data[0] == configuracionParametros.pasoInocrrecto():
				temp = data[0]
				break
			if data[0] != configuracionParametros.pasando():
				temp = data[0]
				break
			if i == (configuracionParametros.rangoMuestreo()-1):
				temp = "sobre paso tiempo"
				break

			try :
				temp
			except NameError:
				temp = None

			if temp is None:
				temp = "ERROR"

		if temp == configuracionParametros.completoPase():
			self.grabadolog(temp)
		else:
			self.grabadolog(temp)
			if temp == configuracionParametros.bloqueo():
				self.desbloqueo()
			if temp == configuracionParametros.pasoInocrrecto():
				self.desbloqueo()
				
		self.proxycon("close")
		return temp

	def mantener_obstaculo(self):
		self.proxycon.GetSetAEA(configuracionParametros.statusQuery())



