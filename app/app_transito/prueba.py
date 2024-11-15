import xmlrpc.client as xmlrpclib
import time
direccion = "http://192.168.0.200:8081/"
proxycon = xmlrpclib.ServerProxy(direccion)

def abrir():
   proxycon.SetModeAEA("O")

def desbloqueo(): # se pone estado normal de la puerta
    abrir()
    data = proxycon.SetModeAEA("E")
    print(data)
    
def bloqueo():
    data = proxycon.SetModeAEA("L")
    print(data)
    
def getStatus():
    data = proxycon.GetSetAEA("SQ")
    print(data)
    return data

def pase():
    stat = getStatus()
    if stat[1] == "L":
        desbloqueo()
    data = proxycon.GetSetAEA("CC")
    print(data)
    
def restablecer():
    stat = getStatus()
    if stat[1] == "L":
        desbloqueo()
    if stat[1] == "O":
        bloqueo()
        time.sleep(2)
        desbloqueo()
     
 

#desbloqueo()
#abrir()
#time.sleep(5)
#bloqueo()
#abrir()
#pase()
restablecer()
getStatus()
