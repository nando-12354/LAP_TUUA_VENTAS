#!/usr/bin/python
import json
from os import listdir

from os.path import isfile, join
from os import path
from os import makedirs

import shutil
import unicodedata
import pypyodbc
import datetime

#configuracion
with open("config.json") as json_file:
        cfg = json.load(json_file)

rutaCarpeta = cfg["rutaCarpeta"]
server = cfg["server"]
database = cfg["database"]
user = cfg["user"]
password = cfg["password"]
molinete = cfg["molinete"]

#script que lee los archivos de log de cada molinete del tuua para contar la cantidad de pasajeros en tr�nsito
class BoardingPass:
    def __init__(self, fecha, tipoVuelo, trama, nombreArchivo,nombrePax, codReserva, origen, destino, iataAerolinea, nroVuelo, fecVueloNum, nroAsiento, checkinSequence, statusPax):
        self.fecha = fecha
        self.tipoVuelo = tipoVuelo
        self.trama = trama
        self.nombreArchivo = nombreArchivo
        self.nombrePax = nombrePax
        self.codReserva = codReserva
        self.origen = origen
        self.destino = destino
        self.iataAerolinea = iataAerolinea
        self.nroVuelo = nroVuelo
        self.fecVueloNum = fecVueloNum
        self.nroAsiento = nroAsiento
        self.checkinSequence = checkinSequence
        self.statusPax = statusPax

def registrarLog(mensaje, tipo):
    t = datetime.datetime.now()
    #crear carpeta de log si no existe
    if not path.exists("log"):
            makedirs("log")

    std = t.strftime('%Y-%m-%d %H:%M:%S')
    linea = std+" \t "+tipo+" \t "+mensaje +"\n"
    #registrar mensaje en archivo de log de ejecucion
    with open('log/capturaTransit-'+t.strftime('%Y%m%d')+'.log','a') as logFile:
        logFile.write(linea)
    return

def remove_control_characters(s):
    return "".join(ch for ch in s if unicodedata.category(ch)[0]!="C")

def guardarBPTransito(bp: BoardingPass):
    connectionStr = "Driver={SQL Server};Server="+server+";Database="+database+";uid="+user+";pwd="+password
    connection = pypyodbc.connect(connectionStr)
    cursor = connection.cursor()
    SqlCommand =  "EXECUTE [dbo].[usp_tuua_insertar_bp_transito] '"+ bp.nroVuelo+"'  ,'"+bp.fecVueloNum+"'  ,'"+bp.nroAsiento+"'  ,'"+bp.checkinSequence+"'  ,'"+bp.nombrePax+"'  ,'"+bp.trama+"'  ,'"+bp.destino+"'  ,'"+bp.iataAerolinea+"'  ,'"+bp.fecha+"'  ,'"+bp.tipoVuelo+"'  ,'"+bp.statusPax+"', '"+molinete+"','"+bp.nombreArchivo+"','USR_SVC'"
    cursor.execdirect(SqlCommand)
    cursor.commit()

    return


def main():
    registrarLog('Transit pax capture started','Info')
    try:
        #listar los archivos en la carpeta
        archivos = [f for f in listdir(rutaCarpeta) if isfile(join(rutaCarpeta,f))]
        #print(archivos)
        #crear carpeta logs procesados 
        if not path.exists(rutaCarpeta+"procesados"):
            makedirs(rutaCarpeta+"procesados")

        boardingsList = []

        #iterar sobre lista de archivos que sean distintos a accesos.log
        for f in archivos:
            #leer todos los archivos menos accesos.log
            if f.lower()!="accesos.log" :
                #print(f)
                with open(rutaCarpeta+f,"r") as archivo:
                    lineas = archivo.readlines()
                    for idx, l in enumerate(lineas):
                        if "TRANSITO" in l:
                            if "VUELO:N" in lineas[idx-3]:
                                tipoVuelo = "N"
                            elif "VUELO:I" in lineas[idx-3]:
                                tipoVuelo = "I"
                                                                                            
                            trama = remove_control_characters(lineas[idx-11]).split(":")[4].strip().replace("/"," ")
                            if trama[0:1] != "M":
                                trama = trama[1:]
                            fecha = l[0:24].strip()
                            fec = fecha[0:10]
                            hor = fecha[11:]
                            arrFec = fec.split("/")
                            fecha = arrFec[2]+"-"+arrFec[1]+"-"+arrFec[0]+" "+hor
                            nombrePax = trama[2:22].strip()
                            codReserva = trama[23:30].strip()
                            origen = trama[30:33].strip()
                            destino = trama[33:36].strip()
                            iataAerolinea = trama[36:39].strip()
                            nroVuelo = trama[39:44].strip()
                            fecVueloNum = trama[44:47].strip()
                            nroAsiento = trama[48:52].strip()
                            checkinSequence = trama[52:57].strip()
                            statusPax = trama[57:58].strip()
                        
                            bp = BoardingPass(fecha,tipoVuelo,trama,f,nombrePax,codReserva,origen,destino,iataAerolinea,nroVuelo,fecVueloNum,nroAsiento,checkinSequence,statusPax)

                            boardingsList.append(bp)
                              
        #Iterar sobre lista de boardings y guardarlos en la base de datos
        for bp in boardingsList:
            print(bp.fecha+" - "+bp.tipoVuelo+" - "+bp.trama+" - "+bp.nombrePax+" - "+bp.codReserva+" - "+bp.origen+" - "+bp.destino+" - "+bp.iataAerolinea+" - "+bp.nroVuelo+" - "+bp.fecVueloNum+" - "+bp.nroAsiento+" - "+bp.checkinSequence+" - "+bp.statusPax)
            #guardar boarding en tabla de pax en transito
            guardarBPTransito(bp)

        #una vez concluida la carga de todos los boardinpass se debe mover los archivos a la carpeta de procesados
        for arc in archivos:
            #mover archivos
            if arc.lower()!="accesos.log" :
                shutil.move(rutaCarpeta+arc,rutaCarpeta+"procesados/"+arc)

        registrarLog("Transit pax capture ended", "Info")
    except Exception as err:

        registrarLog(str(err),"Error")

    return

#ejecutar funcion main
main()