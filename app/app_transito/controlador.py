#import xmlrpclib
import xmlrpc.client as xmlrpclib

import datetime
import configuracionParametros
import logging
import time
class controlador:

    def __init__(self, direccion):
        self.direccion = direccion
        self.proxycon = xmlrpclib.ServerProxy(direccion)

    def restart(self):
        data = self.proxycon.Sendrestart()
        return data

    #def grabadolog(self,temp):
    #    dia=datetime.datetime.utcnow() + datetime.timedelta(hours=configuracionParametros.desfazeUTC())
    #    nombre_log = 'traza_controlador_' + str(datetime.date(year=dia.year,month=dia.month,day=dia.day)) +'.log'
    #    logging.basicConfig(filename=nombre_log,level=logging.DEBUG)
    #    logging.info(str(generico.diahora()) + " Informacion del estado :     " + str(temp))

    def estadoObstaculo(self):
        std_obstaculo = self.proxycon.GetSetAEA()
        return std_obstaculo

    def desbloqueo(self,logger):
        logger.info("Info: "+str(datetime.datetime.today())+" Se cambia el estado de la puerta a abierto y despues a normal")
        data = self.proxycon.SetModeAEA(configuracionParametros.estadoAbrir(logger))
        data = self.proxycon.SetModeAEA(configuracionParametros.estadoNormal(logger))
        return data

    def abrir(self,logger):
        logger.info("Info: "+str(datetime.datetime.today())+" Se cambia el estado de la puerta a abierto")
        print("Apertura de Puerta")
        data = self.proxycon.SetModeAEA(configuracionParametros.estadoAbrir(logger))
        return data

    def normal(self,logger):
        logger.info("Info: "+str(datetime.datetime.today())+" Se cambia el estado de la puerta a normal ")
        data = self.proxycon.SetModeAEA(configuracionParametros.estadoNormal(logger))
        return data
    
    def bloqueo(self,logger):
        logger.info("Info: "+str(datetime.datetime.today())+" Se cambia el estado de la puerta a cerrado ")
        data = self.proxycon.SetModeAEA(configuracionParametros.estadoCerrado(logger))
        return data

    def estado_actual(self,logger):
        data = self.proxycon.GetSetAEA(configuracionParametros.statusQuery(logger))
        return data

    def presencia_puerta(self,logger):
        data = self.proxycon.GetSetAEA(configuracionParametros.statusQuery(logger))
        for _ in range(1, 8):
            time.sleep(configuracionParametros.muestreoPuerta(logger))
            data = self.proxycon.GetSetAEA(configuracionParametros.statusQuery(logger))
            print(data)
            if data[8] == 1 :
                msg = "presencia"
                break
            else :
                msg = "sin presencia"
        return msg


    def verifica_abertura_puerta(self,logger):
        print("********ENTRO EN VERIFICA ABERTURA PUERTA*************")
        logger.info("Info: "+str(datetime.datetime.today())+" Se verifica la abertura de puerta ")
        data = self.proxycon.GetSetAEA(configuracionParametros.autorizaPase(logger),"Se espera que abrir puerta")
        for i in range(1, configuracionParametros.rangoMuestreo(logger)):
            time.sleep(configuracionParametros.muestreoPuerta(logger))
            data = self.proxycon.GetSetAEA(configuracionParametros.statusQuery(logger))
            print(data)

            if data[0] == configuracionParametros.completoPase(logger):
                temp = data[0]
                break
            if data[0] == configuracionParametros.regreso(logger): 
                temp = data[0]
                break
            if data[0] == configuracionParametros.incompletoPase(logger):
                temp = data[0]
                break
            if data[0] == configuracionParametros.bloqueo(logger):
                temp = data[0]
                break
            if data[0] == configuracionParametros.pasoInocrrecto(logger):
                temp = data[0]
                break
            #if data[0] != configuracionParametros.pasando():
            #    temp = data[0]
            #    break
            if i == (configuracionParametros.rangoMuestreo(logger)-1):
                temp = "sobre paso tiempo"
                break

            try :
                temp
            except NameError:
                temp = None

            if temp is None:
                temp = "ERROR"

        if temp == configuracionParametros.completoPase(logger):
            pass
            #self.grabadolog(temp)
        else:
            #self.grabadolog(temp)
            if temp == configuracionParametros.bloqueo(logger):
                self.desbloqueo(logger)
            if temp == configuracionParametros.pasoInocrrecto(logger):
                self.desbloqueo(logger)
                
        self.proxycon("close")
        return temp

    def mantener_obstaculo(self,logger):
        logger.info("Info: "+str(datetime.datetime.today())+" Se mantuvo el obstaculo (mantener_obstaculo) ")
        return self.proxycon.GetSetAEA(configuracionParametros.statusQuery(logger))



