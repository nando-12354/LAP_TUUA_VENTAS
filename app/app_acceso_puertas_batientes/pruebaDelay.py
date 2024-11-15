import xmlrpc.client as xmlrpclib
import time
import configuracionParametros
proxycon = xmlrpclib.ServerProxy('http://192.168.0.200:8081/')

#result = proxycon.GetSetTempo(10000,-1,10000,-1,10000,-1,15000,9000,20000)
#print(result)
#data = proxycon.SendRestart()
#data= proxycon.SetMotorSpeed('Entry',75,75,5,50,80,65)
#data= proxycon.SetMotorSpeed('Exit',75,75,5,50,80,65)
data= proxycon.SetMotorSpeed('Entry',60,60,5,50,80,65)
data= proxycon.SetMotorSpeed('Exit',60,60,5,50,80,65)
data = proxycon.SendRestart()
data= proxycon.GetMotorSpeed()
#data = proxycon.GetSetTempo(-1,-1,-1,-1,-1,-1,-1,-1,-1)
#print(result)
print(data)
#data = proxycon.SetModeAEA(configuracionParametros.estadoNormal())
#data = proxycon.GetSetAEA(configuracionParametros.statusQuery())
