import xmlrpc.client as xmlrpclib
import time
import configuracionParametros

proxycon = xmlrpclib.ServerProxy(configuracionParametros.direccionControlador())



data= proxycon.SetMotorSpeed('Entry',70,70,5,50,80,65)
data= proxycon.SetMotorSpeed('Exit',70,70,5,50,80,65)
data = proxycon.SendRestart()

print(data)
