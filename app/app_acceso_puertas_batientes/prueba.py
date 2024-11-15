#!/usr/bin/

'''import serial

with serial.Serial('/dev/ttyS1', 19200, timeout=1) as ser:
	x = ser.read()          # read one byte
	s = ser.read(10)        # read up to ten bytes (timeout)
	line = ser.readline()   # read a  terminated line
	print line
	'''
'''
import configuracionParametros
import generico
import datetime
import mysql.connector'''
'''
import datetime
def armarcadenaotros( dato, numero):
	espacio = ""
	if (len(dato) == numero):
		cadena = dato
	elif (len(dato) < numero):
		for x in range(0, (numero-len(dato))):
			espacio = espacio+" "
		cadena = dato+espacio
	else:
		cadena = dato[0:numero]
	return cadena

flg_boarding_pass = "M1"
str_dsc_nombrePAX = "CARLOS SOCA GUERAA "
flg_ticket = "E"
str_cod_reserva = "RINFNG"
str_desc_aero_origen = "LIM"
str_desc_aero_destino = "GYE"
str_desc_aerolinea = "PL"
str_num_vuelo = "109"
dtm_fch_vuelo = datetime.date(2019, 9, 19)
str_dsc_clase = "E"
str_num_asiento = "009F"
str_dsc_checkin = "0173"
str_std_pasajero = "1"
str_std_subida = "no subido"
fechajul = dtm_fch_vuelo.timetuple()

str_txt_trama = armarcadenaotros(flg_boarding_pass, 2)+armarcadenaotros(str_dsc_nombrePAX, 20)+armarcadenaotros(flg_ticket, 1)+armarcadenaotros(str_cod_reserva, 7)+armarcadenaotros(str_desc_aero_origen, 3)+armarcadenaotros(str_desc_aero_destino, 3)+armarcadenaotros(str_desc_aerolinea, 3)+armarcadenaotros(str(str_num_vuelo), 5)+armarcadenaotros(str(fechajul.tm_yday), 3)+armarcadenaotros(str_dsc_clase, 1)+armarcadenaotros(str_num_asiento, 4)+armarcadenaotros(str_dsc_checkin, 5)+armarcadenaotros(str_std_pasajero, 3)

print(str_txt_trama)'''


'''
mydb = mysql.connector.connect(
	host="localhost",
	user="root",
 	passwd= "root",
	database="lap",
)
my_cursor = mydb.cursor()
#my_cursor.execute("SELECT * FROM LAP_SP_llaves_de_destrabe Where cod_usuario ='"+ x[0:7]+"' and cod_destrabe = '"+x[7:17]+"' and cod_molinete ='"+x[17:27]+"'")
#results = my_cursor.fetchall()
#print results
my_cursor.execute("SELECT cod_destrabe,cod_usuario,cod_molinete FROM LAP_SP_llaves_de_destrabe WHERE  cod_usuario = 'U000207' ")
results = my_cursor.fetchall()
print "RESULTADO"
print type(results)
print results
print results[0][0]
print type(results[0][0])
'''

#sql_stuff = "INSERT INTO LAP_SP_llaves_de_destrabe (cod_usuario,cod_destrabe,cod_molinete,num) VALUES (%s,%s,%s,%s)"
#INSERT INTO LAP_SP_llaves_de_destrabe (cod_usuario,cod_destrabe,cod_molinete,num) VALUES ('U000167','APR000167','CML000167',322)
#records = [	("U000'159", "TRA000159 ", "CML000159", 12), ]
#my_sql = "UPDATE LAP_SP_informacion_de_ticket SET std_subida = 'si subido' WHERE txt_trama = '"+str(results[0][18])+"' AND fch_hora_intento = '"+str(results[0][16])+"'"
#my_cursor.execute(my_sql)
#my_cursor.executemany(sql_stuff, records)
#mydb.commit()
#mydb.close()
#U000159DES000159 CML000159 


#print configuracionParametros.urlVerificarLlavesDestrabe_timeout()

#print configuracionParametros.AeropuertoActual()
#print configuracionParametros.urlVerificarRed()
#print configuracionParametros.comprobarDatos()



#M1DANIELO ROJAUS MATNEERINFNG LIMGYEPL 109  220E009F0173 1



#1	M1GUTIERREZ FIESTAS-JOEEGMJGM LIMTRULA 2206 221Y024L0092 13D:10B1RO9199BLA 29045          00                          8
#2	M1CACERES-GLADYS      EHTGEEX LIMTRULA 2204 221Y024C0072 63D:10B2RR9199BLA 29045          00
#3	M1CERVANTES-DANIEL    ERXPLZN LIMAQPLA 2147 221Y001A0011 13D:10B1RR9199BLA 29045          00   LA 51408893430
#4	M1ALEJO YUPANQUI-KARELEZMTAWC LIMTRULA 2224 221Y027C0095 13D:10B2RO9199BLA 29045          00                          8
#5	M1AMAYA-DEIBI         ETODMIH LIMTRULA 2206 221Y007L0111 13D:10B2RO9199BLA 29045          00                          8
#6	M1CHANCAFE-JOEL ALEXANEXPCHCF LIMCIXLA 2282 221Y020C0013 13D:10B1RO9199BLA 29045          00                          8
#7	M1SUAREZ-ARNOLD       EGWJTWW LIMPIULA 2308 221Y024K0013 13D:10B1RO9199BLA 29045          00   LA 5141256607K         8




'''
from Tkinter import *
import datetime
import time
class ChangeTime(Frame):
    def __init__(self, master=None):
        master.geometry("100x50+5+5")
        Frame.__init__(self, master)
        self.pack()
        self.timestr = StringVar()
        lbl = Label(master, textvariable=self.timestr)
        lbl.pack()
        # register callback
        self.listenID = self.after(1000, self.newtime)
    def newtime(self):
        timenow = datetime.datetime.now()
        self.timestr.set("%d:%02d:%02d" % (timenow.hour, timenow.minute, timenow.second))
        self.listenID = self.after(1000, self.newtime)
root=Tk()
CT = ChangeTime(root)
CT.mainloop()'''

'''
import socket
from Tkinter import *

HOST= 'localhost'
PORT = 8000
s= socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((HOST, PORT))
s.listen(1) #I'm aiming for this to eventually be 24.
conn, addr = s.accept()

oldvalue = 60
interval = 500

def data():
    global oldvalue
    newvalue = str(value.get())
    if newvalue != oldvalue:
        print(newvalue)
        oldvalue = newvalue
    root.after(interval, data)
    #Value = str(value.get())
    #conn.sendall(str(Value))

root = Tk()
value = Scale(root, from_=255, to=0)
value.set(oldvalue)
value.pack() #not using pack but grid. Pack is just easier for simple example.

root.after(1, data)
root.mainloop()

'''
'''
from Tkinter import *

oldvalue = 60
interval = 500

def data():
    global oldvalue
    newvalue = str(value.get())
    if newvalue != oldvalue:
        print(newvalue)
        oldvalue = newvalue
    root.after(interval, data)

root = Tk()
value = Scale(root, from_=255, to=0)
value.set(oldvalue)
value.pack() #not using pack but grid. Pack is just easier for simple example.

root.after(1, data)
root.mainloop()'''
'''
import json

with open('parametros.json', 'r+') as f:
    data = json.load(f)
    data['id'] = 134 # <--- add `id` value.
    f.seek(0)        # <--- should reset file position to the beginning.
    json.dump(data, f, indent=4)
    f.truncate()'''
'''
from tkinter import *  
from PIL import ImageTk,Image  
root = Tk()  
canvas = Canvas(root, width = 300, height = 300)  
canvas.pack()  
img = ImageTk.PhotoImage(Image.open("bienvenido.jpg"))  
canvas.create_image(20, 20, anchor=NW, image=img) 
root.mainloop()  '''
'''
import configuracionParametros

resp = configuracionParametros.longitudTuua()

print(resp)

print("//////////////////////////////////////////////////")
print(configuracionParametros.urlVerificarRed())
print(configuracionParametros.urlVerificarRed_timeout() )
print(configuracionParametros.urlVerificarWebservice() )
print(configuracionParametros.urlVerificarWebservice_timeout() )
print(configuracionParametros.urlSubirBDaWS() )
print(configuracionParametros.urlSubirBDaWS_timeout() )
print(configuracionParametros.urlVerificarEstadoPuerta() )
print(configuracionParametros.urlVerificarEstadoPuerta_timeout() )
print(configuracionParametros.urlVerificarLlavesDestrabe() )
print(configuracionParametros.urlVerificarLlavesDestrabe_timeout() )
print(configuracionParametros.horaContingenciatemprano() )
print(configuracionParametros.horaContingenciatarde() )
print(configuracionParametros.limiteBorrado() )
print(configuracionParametros.limiteBorradoLog() )
print(configuracionParametros.horaBorradoCaducados() )
print(configuracionParametros.datosBD() )
print(configuracionParametros.AeropuertoActual() )
print(configuracionParametros.NombrePuerta() )
print(configuracionParametros.luzscanner() )
print(configuracionParametros.puertoscanner() )
print(configuracionParametros.longitudTuua() )
print(configuracionParametros.direccionControlador() )
print(configuracionParametros.estadoCerrado() )
print(configuracionParametros.estadoAbrir() )
print(configuracionParametros.estadoNormal() )
print(configuracionParametros.rangoMuestreo() )
print(configuracionParametros.muestreoPuerta() )
print(configuracionParametros.statusQuery() )
print(configuracionParametros.autorizaPase() )
print(configuracionParametros.intentoFraude() )
print(configuracionParametros.completoPase() )
print(configuracionParametros.regreso() )
print(configuracionParametros.incompletoPase() )
print(configuracionParametros.bloqueo() )
print(configuracionParametros.pasoInocrrecto() )
print(configuracionParametros.emergencia() )
print(configuracionParametros.bloqueadoEsp() )
print(configuracionParametros.bloqueadoEn() )
print(configuracionParametros.desbloqueadoEsp() )
print(configuracionParametros.desbloqueadoEn() )
print(configuracionParametros.mulEsp() )
print(configuracionParametros.mulEn() )
print(configuracionParametros.mnsjPase() )
print(configuracionParametros.mnsjNoPaseHora() )
print(configuracionParametros.mnsjNoPaseAeropuerto() )
print(configuracionParametros.mnsjNoPaseUtilizado() )
print(configuracionParametros.mnsjTransito() )'''

