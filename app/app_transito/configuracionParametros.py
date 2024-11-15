"""
ARCHIVO CLASE : configuracionParametros.py
Objetivo : Archivo que tiene los parametros que se puedan cambiar
Autor : 
Fecha Creacion : 2019-12-04
Metodos :    -captarDatos() capta los datos del archivo de texto y busca especificamente la informacion que pide 

"""
import datetime
import os
import logging
import sys
import time
import mysql.connector

try:
    variable = os.listdir(os.getcwd())
    std = 0
    for var in variable :
        if(var == 'parametros.txt'):
            std = std + 1

    if (std == 0):
        f= open("parametros.txt","w+")
        f.write("COMENTARIO: Escoje el tipo de separador el cual estara entre los datos->(#)  No cambiar el orden de los parametros\n")
        f.write("1 web service verificar red:    #https://4f4fe15d.ngrok.io/api/TuuaAccesos/getstatus#\n")
        f.write("2 web service vuelos salidas:    #https://4f4fe15d.ngrok.io/api/TuuaAccesos/GetVuelosProgramados?tipo_operacion=S&tipo_vuelo=I#\n")
        f.write("3 web service vuelos llegadas:    #https://4f4fe15d.ngrok.io/api/TuuaAccesos/GetVuelosProgramados?tipo_operacion=L&tipo_vuelo=I\n")
        f.write("4 web post vuelo salida:    #https://4f4fe15d.ngrok.io/api/tuuaaccesos/PostObtenerVueloTrama?tip_operacion=S# \n")
        f.write("5 web post vuelo llegada:    #https://4f4fe15d.ngrok.io/api/tuuaaccesos/PostObtenerVueloTrama?tip_operacion=L#\n")
        f.write("6 web service post de datos:    #https://4f4fe15d.ngrok.io/api/tuuaaccesos/PostRegistrarPaxTransito#\n")
        f.write("7 Timeout verficar red:        #0.7#\n")
        f.write("8 Timeout vuelos salidas:    #3#\n")
        f.write("9 Timeout vuelos llegadas:    #3#\n")
        f.write("10 Timeout post vuelo salida:    #3#\n")
        f.write("11 Timeout post vuelo llegada:    #3#\n")
        f.write("12 Timeout post de datos:    #3#\n")
        f.write("13 tiempo de reinicio segundos:    #300#\n")
        f.write("14 tipo de letra botones:    #Helvetica#\n")
        f.write("15 Nombre puerta:        #L21#\n")
        f.write("16 Base de datos(l/u/p/n):    #localhost,root,root,lapTransito#\n")
        f.write("17 Hora de borrado:        #13:06#\n")
        f.write("18 Borrado base de datos:    #30#\n")
        f.write("19 Borrado log:            #10#\n")
        f.write("20 Aeropuerto origen:        #LIM#\n")
        f.write("21 luz scanner:            #COM20#\n")
        f.write("22 puerto scanner:        #COM19#\n")
        f.write("23 longitud tuua :        #16#\n")
        f.write("24 direccion controlador:    #http://192.168.0.200:8081/#\n")
        f.write("25 estado cerrado puerta:    #L#\n")
        f.write("26 estado abierto puerta:    #O#\n")
        f.write("27 estado normal puerta:    #E#\n")
        f.write("28 rango de muestreo:        #40#\n")        
        f.write("29 duracion de muestreo:    #0.5#\n")
        f.write("30 status query:        #SQ#\n")
        f.write("31 autorizar pase:        #CC#\n")
        f.write("32 intento de fraude:        #CR#\n")
        f.write("33 Completo pase:        #QBOK#\n")
        f.write("34 regreso:            #CNXB#\n")
        f.write("35 incompleto pase:        #TODT#\n")
        f.write("36 bloqueo:            #FRXO#\n")
        f.write("37 paso incorrecto:        #FR2X#\n")
        f.write("38 emergencia:            #EXOK#\n")
        f.write("39 mnsj bloqueado esp :        #bloqueado#\n")
        f.write("40 mnsj bloqueado en :        #locked#\n")
        f.write("41 mnsj desbloqueado esp :    #desbloqueado#\n")
        f.write("42 mnsj desbloqueado en :    #unlocked#\n")
        f.write("43 mnsj de pase :        #Registro Satisfactorio#\n")
        f.write("44 mnsj de no pase :        #No pase#\n")
        f.write("45 mnsj bienvenido esp :    #Bienvenido#\n")
        f.write("46 mnsj bienvenido en :        #Welcome#\n")
        f.write("47 pasajero pasando :        #PROK#\n")
        f.write("48 web service llaves destrabe:    #https://4f4fe15d.ngrok.io/api/TuuaAccesos/GetLlavesDestrabe#\n")
        f.write("49 Timeout llaves de destrabe:    #3#\n")
        f.write("50 mnsj de espera:        #Espere...#\n")
        f.close()


except Exception as e:
    print("ERROR")
except :
    print("ERROR EXCEPT")
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
            separador = captarSeparador()
            partes = contents.split(separador)
        f.close
        string = partes[int_numero]

    except :
        string = 'Error404'
    finally:
        return string

def buscarDatoxLinea(numero_de_linea,string):
    try:

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
        elif (indice2 == -1):
            devol = 'hay un error'
        return devol
    except :
        devol = 'hay un error'
        return devol


def urlVerificarRed(logger):
    try:
        msg = captarDatos(1)
        feedback = buscarDatoxLinea(1,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlVerificarRed "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlVerificarRed ")
        msg = 'Error404'
    finally:
        return msg

def urlServiceVuelosSalidas(logger):
    try:
        msg = captarDatos(2)
        feedback = buscarDatoxLinea(2,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlServiceVuelosSalidas ")
        msg = 'Error404'
    finally:
        return msg

def urlServiceVuelosLlegadas(logger):
    try:
        msg = captarDatos(3)
        feedback = buscarDatoxLinea(3,msg) 
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlServiceVuelosLlegadas "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlServiceVuelosLlegadas ")
        msg = 'Error404'
    finally:
        return msg

def urlPostVueloSalida(logger):
    try:
        msg = captarDatos(4)
        feedback = buscarDatoxLinea(4,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostVueloSalida "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostVueloSalida ")
        msg = 'Error404'
    finally:
        return msg

def urlPostVueloLlegada(logger):
    try:
        msg = captarDatos(5)
        feedback = buscarDatoxLinea(5,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostVueloLlegada "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostVueloLlegada ")
        msg = 'Error404'
    finally:
        return msg

def urlPostDatos(logger):
    try:
        msg = captarDatos(6)
        feedback = buscarDatoxLinea(6,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostDatos "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en mnsjEspera ")
        msg = 'Error404'
    finally:
        return msg

def urlVerificarRed_timeout(logger):
    try:
        try:
            msg = int(captarDatos(7))
        except:        
            msg = float(captarDatos(7))
        feedback = buscarDatoxLinea(7,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlVerificarRed_timeout "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlVerificarRed_timeout ")
        msg = 'Error404'
    finally:
        return msg

def urlServiceVuelosSalidas_timeout(logger):
    try:
        try:
            msg = int(captarDatos(8))
        except:
            print("entro al except")
            msg = float(captarDatos(8))
        print("MSGGGG")
        print(msg)
        feedback = buscarDatoxLinea(8,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlServiceVuelosSalidas_timeout "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlServiceVuelosSalidas_timeout ")
        msg = 'Error404'
    finally:
        return msg

def urlServiceVuelosLlegadas_timeout(logger):
    try:
        try:
            msg = int(captarDatos(9))
        except:        
            msg = float(captarDatos(9))
        feedback = buscarDatoxLinea(9,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlServiceVuelosLlegadas_timeout "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlServiceVuelosLlegadas_timeout ")
        msg = 'Error404'
    finally:
        return msg

def urlPostVueloSalida_timeout(logger):
    try:
        try:
            msg = int(captarDatos(10))
        except:        
            msg = float(captarDatos(10))
        feedback = buscarDatoxLinea(10,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostVueloSalida_timeout ")
        msg = 'Error404'
    finally:
        return msg

def urlPostVueloLlegada_timeout(logger):
    try:
        try:
            msg = int(captarDatos(11))
        except:        
            msg = float(captarDatos(11))
        feedback = buscarDatoxLinea(11,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostVueloLlegada_timeout "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostVueloLlegada_timeout ")
        msg = 'Error404'
    finally:
        return msg

def urlPostDatos_timeout(logger):
    try:
        try:
            msg = int(captarDatos(12))
        except:        
            msg = float(captarDatos(12))
        feedback = buscarDatoxLinea(12,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostDatos_timeout "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlPostDatos_timeout ")
        msg = 'Error404'
    finally:
        return msg

def tiempoRefreshSegundos(logger):
    try:
        msg = int(captarDatos(13))
        feedback = buscarDatoxLinea(13,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en tiempoRefreshSegundos "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en tiempoRefreshSegundos ")
        msg = 'Error404'
    finally:
        return msg

def tipoLetraBotones(logger):
    try:
        msg = captarDatos(14)
        feedback = buscarDatoxLinea(14,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en tipoLetraBotones "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en tipoLetraBotones ")
        msg = 'Error404'
    finally:
        return msg

def NombrePuerta(logger):
    try:
        msg = captarDatos(15)
        feedback = buscarDatoxLinea(15,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en NombrePuerta "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en NombrePuerta ")
        msg = 'Error404'
    finally:
        return msg

def datosBD(logger):
    try:
        msg = captarDatos(16).split(',')
        for x in msg :
            feedback = buscarDatoxLinea(16,x)
            if (feedback == 'hay un error'):
                msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en datosBD "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en datosBD ")
        msg = 'Error404'
    finally:
        return msg

def horaBorradoCaducados(logger):
    try:
        msg = captarDatos(17).split(':')
        for x in msg:
            feedback = buscarDatoxLinea(17,x)
            if (feedback == 'hay un error'):
                msg= "Error404"

        fch = datetime.time(int(msg[0]),int(msg[1]), 0)
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en horaBorradoCaducados "+str(e))
        fch = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en horaBorradoCaducados ")
        fch = 'Error404'
    finally:
        return fch

def limiteBorrado(logger):
    try:
        msg = int(captarDatos(18))
        feedback = buscarDatoxLinea(18,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
        fch = datetime.timedelta(days=msg)
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en limiteBorrado "+str(e))
        fch = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en limiteBorrado ")
        fch = 'Error404'
    finally:
        return fch

def limiteBorradoLog(logger):
    try:
        msg = int(captarDatos(19))
        feedback = buscarDatoxLinea(19,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
        fch = datetime.timedelta(days=msg)
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en limiteBorradoLog "+str(e))
        fch = 'Error404'
    finally:
        return fch

def AeropuertoActual():
    try:
        msg = captarDatos(20)
        feedback = buscarDatoxLinea(20,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except:
        msg = 'Error404'
    finally:
        return msg

def luzscanner(logger):
    try:
        msg = captarDatos(21)
        feedback = buscarDatoxLinea(21,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en luzscanner "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en luzscanner ")
        msg = 'Error404'
    finally:
        return msg

def puertoscanner(logger):
    try:
        msg = captarDatos(22)
        feedback = buscarDatoxLinea(22,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en puertoscanner "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en puertoscanner ")
        msg = 'Error404'
    finally:
        return msg

def longitudTuua(logger):
    try:
        msg = int(captarDatos(23))
        feedback = buscarDatoxLinea(23,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en longitudTuua "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en longitudTuua ")
        msg = 'Error404'
    finally:
        return msg

def direccionControlador(logger):
    try:
        msg = captarDatos(24)
        feedback = buscarDatoxLinea(24,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en direccionControlador "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en direccionControlador ")
        msg = 'Error404'
    finally:
        return msg

def desfazeUTC():
    return -5

def estadoCerrado(logger):
    try:
        msg = captarDatos(25)
        feedback = buscarDatoxLinea(25,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en estadoCerrado "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en estadoCerrado ")
        msg = 'Error404'
    finally:
        return msg

def estadoAbrir(logger):
    try:
        msg = captarDatos(26)
        feedback = buscarDatoxLinea(26,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en estadoAbrir "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en estadoAbrir ")
        msg = 'Error404'
    finally:
        return msg

def estadoNormal(logger):
    try:
        msg = captarDatos(27)
        feedback = buscarDatoxLinea(27,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en estadoNormal "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en estadoNormal ")
        msg = 'Error404'
    finally:
        return msg

def rangoMuestreo(logger):
    try:
        msg = int(captarDatos(28))
        feedback = buscarDatoxLinea(28,str(msg))
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en rangoMuestreo "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en rangoMuestreo ")
        msg = 'Error404'
    finally:
        return msg

def muestreoPuerta(logger):
    try:    
        msg = float(captarDatos(29))
        feedback = buscarDatoxLinea(29,str(msg))
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en muestreoPuerta "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en muestreoPuerta ")
        msg = 'Error404'
    finally:
        return msg

def statusQuery(logger):
    try:
        msg = captarDatos(30)
        feedback = buscarDatoxLinea(30,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en statusQuery "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en statusQuery ")
        msg = 'Error404'
    finally:
        return msg

def autorizaPase(logger):
    try:
        msg = captarDatos(31)
        feedback = buscarDatoxLinea(31,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en autorizaPase ")
        msg = 'Error404'
    finally:
        return msg

def intentoFraude():
    try:
        msg = captarDatos(32)
        feedback = buscarDatoxLinea(32,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except:
        msg = 'Error404'
    finally:
        return msg

def completoPase(logger):
    try:
        msg = captarDatos(33)
        feedback = buscarDatoxLinea(33,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en completoPase "+str(e))
        msg = 'Error404'
    finally:
        return msg

def regreso(logger):
    try:
        msg = captarDatos(34)
        feedback = buscarDatoxLinea(34,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en regreso "+str(e))
        msg = 'Error404'
    finally:
        return msg
#
def incompletoPase(logger):
    try:
        msg = captarDatos(35)
        feedback = buscarDatoxLinea(35,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en incompletoPase "+str(e))
        msg = 'Error404'
    finally:
        return msg

def bloqueo(logger):
    try:
        msg = captarDatos(36)
        feedback = buscarDatoxLinea(36,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en bloqueo "+str(e))
        msg = 'Error404'

    finally:
        return msg

def pasoInocrrecto(logger):
    try:
        msg = captarDatos(37)
        feedback = buscarDatoxLinea(37,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en pasoInocrrecto "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en pasoInocrrecto ")
        msg = 'Error404'
        
    finally:
        return msg

def emergencia():
    try:
        msg = captarDatos(38)
        feedback = buscarDatoxLinea(38,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def bloqueadoEsp():
    try:
        msg = captarDatos(39)
        feedback = buscarDatoxLinea(39,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def bloqueadoEn():
    try:
        msg = captarDatos(40)
        feedback = buscarDatoxLinea(40,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def desbloqueadoEsp():
    try:
        msg = captarDatos(41)
        feedback = buscarDatoxLinea(41,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def desbloqueadoEn():
    try:
        msg = captarDatos(42)
        feedback = buscarDatoxLinea(42,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def mnsjPase():
    try:
        msg = captarDatos(43)
        feedback = buscarDatoxLinea(43,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def mnsjNoPase():
    try:
        msg = captarDatos(44)
        feedback = buscarDatoxLinea(44,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def mnsjBienvenidoEsp():
    try:
        msg = captarDatos(45)
        feedback = buscarDatoxLinea(45,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def mnsjBienvenidoEng():
    try:
        msg = captarDatos(46)
        feedback = buscarDatoxLinea(46,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def pasando():
    try:
        msg = captarDatos(47)
        feedback = buscarDatoxLinea(47,msg)
        if (feedback == 'hay un error'):
            msg = 'Error404'
    except :
        msg = 'Error404'
    finally:
        return msg

def urlVerificarLlavesDestrabe(logger):
    try:
        msg = captarDatos(48)
        feedback = buscarDatoxLinea(48,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlVerificarLlavesDestrabe "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlVerificarLlavesDestrabe ")
        msg = 'Error404'
    finally:
        return msg
        
def urlVerificarLlavesDestrabe_timeout(logger):
    try:
        try:
            msg = int(captarDatos(49))
        except:        
            msg = float(captarDatos(49))
        feedback = buscarDatoxLinea(49,str(msg))
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlVerificarLlavesDestrabe_timeout "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en urlVerificarLlavesDestrabe_timeout ")
        msg = 'Error404'
    finally:
        return msg

def  mnsjEspera(logger):
    try:
        msg = captarDatos(50)
        feedback = buscarDatoxLinea(50,msg)
        if (feedback == 'hay un error'):
            msg= "Error404"
    except Exception as e:
        logger.info("Error: "+str(datetime.datetime.today())+" Error en mnsjEspera "+str(e))
        msg = 'Error404'
    except :
        logger.info("Error: "+str(datetime.datetime.today())+" Error en mnsjEspera ")
        msg = 'Error404'
    finally:
        return msg

def comprobarDatos(logger):
    try:
        lista = []
        lista.append(urlVerificarRed(logger))
        lista.append(urlServiceVuelosSalidas(logger))#
        lista.append(urlServiceVuelosLlegadas(logger))#
        lista.append(urlPostVueloSalida(logger))#
        lista.append(urlPostVueloLlegada(logger))#
        lista.append(urlPostDatos(logger))#
        lista.append(urlVerificarRed_timeout(logger))#
        lista.append(urlVerificarWebservice_timeout())#
        lista.append(urlSubirBDaWS_timeout())#
        lista.append(urlVerificarEstadoPuerta_timeout())#
        lista.append(urlVerificarLlavesDestrabe_timeout(logger))#
        lista.append(horaContingenciatemprano())#
        lista.append(horaContingenciatarde())#
        lista.append(limiteBorrado(logger))#
        lista.append(limiteBorradoLog(logger))#
        lista.append(horaBorradoCaducados(logger))#
        lista.append(datosBD(logger))#
        lista.append(AeropuertoActual())#
        lista.append(NombrePuerta(logger))#
        lista.append(luzscanner(logger))
        lista.append(puertoscanner(logger))
        lista.append(direccionControlador(logger))
        lista.append(desfazeUTC())
        lista.append(estadoCerrado(logger))
        lista.append(estadoAbrir(logger))
        lista.append(estadoNormal(logger))
        lista.append(rangoMuestreo(logger))
        lista.append(muestreoPuerta(logger))
        lista.append(statusQuery(logger))
        lista.append(autorizaPase(logger))
        lista.append(intentoFraude())
        lista.append(completoPase(logger))
        lista.append(regreso(logger))
        lista.append(incompletoPase(logger))
        lista.append(bloqueo(logger))
        lista.append(pasoInocrrecto(logger))
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
    finally:
        return flag