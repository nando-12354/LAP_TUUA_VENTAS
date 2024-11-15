
'''
import generico

print("prueba2")

trama = input("escanea el codigo de origen: ")
respuesta =generico.Post_vuelo_dato_llegada(trama)

if (respuesta[0] != "error"):
	print(respuesta)
	trama2 = input("escanea el codigo de destino: ")
	respuesta2 =generico.Post_vuelo_dato_salida(trama)
	if (respuesta2[0] != "error"):
		print(respuesta2)
		respuesta3 = generico.Post_datos(respuesta[1],respuesta[2],respuesta2[1],respuesta2[2],"L02",trama,trama2)
		#(Num_Vuelo_Origen,Fch_Vuelo_Origen,Num_Vuelo_Destino,Fch_Vuelo_Destino,Cod_Molinete,Trama_Origen,Trama_Destino):
		print("respuesta de los datos escaneados")
		print(respuesta3)
	else:
		print("error en destino")
else:
	print("error en origen")
	print(respuesta)
'''
from controlador import controlador
import configuracionParametros
import generico

#ejemplo = generico.estado_webservice()
#ejemplo = generico.verificacion_contingencia("M1HARVEY/DAVID        EFYIJUE LIMCUZLA 2457 338Y001L0149 13D>10B1KR9287BLA 29045          00                 ","origen")

#print("PRINTEO PRUEBA")
#print(ejemplo)
import datetime
generico.Guardar_datos('dato1','dato2','dato3','dato4','dato5','dato6','dato7','no subido',str(datetime.datetime.today()))



#import generico
#print(len("1234"))
#print(len("Hora/time       Destino/Destination Nro.                        Vuelo/Flight"))
#print(len("Destino/Destination Nro.                        "))
#print(len("Vuelo/Flight"))
'''
print("1234567890")
print("1\t23456789012345678901234567890")
print("12\t3456789012345678901234567890")
print("123\t456789012345678901234567890")
print("1234\t56789012345678901234567890")
print("12345\t6789012345678901234567890")
print("123456\t789012345678901234567890")
print("1234567\t89012345678901234567890")#####
print(len("1234567"))
print("12345678\t9012345678901234567890")
print("123456789\t012345678901234567890")
print("1234567890\t12345678901234567890")
print("12345678901\t2345678901234567890")
print("123456789012\t345678901234567890")
print("1234567890123\t45678901234567890")
print("12345678901234\t5678901234567890")
print("123456789012345\t678901234567890")######7 - 15 - 23 - 
print(len("123456789012345"))
print("1234567890123456\t78901234567890")
print("12345678901234567\t8901234567890")
print("123456789012345678\t901234567890")
print("1234567890123456789\t01234567890")
print("12345678901234567890\t1234567890")
print("123456789012345678901\t234567890")
print("1234567890123456789012\t34567890")
print("12345678901234567890123\t4567890")#####
print(len("12345678901234567890123"))
print("123456789012345678901234\t567890")
print("1234567890123456789012345\t67890")
print("12345678901234567890123456\t7890")
print("123456789012345678901234567\t890")
print("1234567890123456789012345678\t90")
print("12345678901234567890123456789\t0")
print("123456789012345678901234567890\t")'''
#trama = generico.armar_cadena("12:39:00","Quito","LA1453")
#print(trama)
#print(len(trama))

'''
# python 3
import tkinter as tk
from tkinter.font import Font

root = tk.Tk()
text = tk.Text(root)
...
myFont = Font(family="Times New Roman", size=12)
text.configure(font=myFont)
'''

'''
from tkinter import *
import time
root = Tk()
clock = Label(root, font=('times', 20, 'bold'), bg='green')
clock.pack(fill=BOTH, expand=1)
def tick():
# get the current local time from the PC
    time2 = time.strftime('%H:%M:%S')
    print(time2)
    print(type(time2))
# if time string has changed, update it
    clock.config(text=time2)
# calls itself every 200 milliseconds
# to update the time display as needed
# could use >200 ms, but display gets jerky
    clock.after(200, tick)
tick()
root.mainloop( )
'''

#if((stdOri == 1) and (stdOri == pags)):
#    self.num=0
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 1) and (stdOri < pags)):
#    self.num=0
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
#elif((stdOri == 2) and (stdOri == pags)):
#    self.num=8
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 2) and (stdOri < pags)):
#    self.num=8
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
#elif((stdOri == 3) and (stdOri == pags)):
#    self.num=16
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 3) and (stdOri < pags)):
#    self.num=16
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
#elif((stdOri == 4) and (stdOri == pags)):
#    self.num=24
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 4) and (stdOri < pags)):
#    self.num=24
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
#elif((stdOri == 5) and (stdOri == pags)):
#    self.num=32
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 5) and (stdOri < pags)):
#    self.num=32
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
#elif((stdOri == 6) and (stdOri == pags)):
#    self.num=40
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 6) and (stdOri < pags)):
#    self.num=40
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
#elif((stdOri == 7) and (stdOri == pags)):
#    self.num=48
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 7) and (stdOri < pags)):
#    self.num=48
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
#elif((stdOri == 8) and (stdOri == pags)):
#    self.num=56
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 8) and (stdOri < pags)):
#    self.num=56
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white") 
#elif((stdOri == 9) and (stdOri == pags)):
#    self.num=64
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
#elif((stdOri == 9) and (stdOri < pags)):
#    self.num=64
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
#else:
#    self.num=0
#    self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
#    self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
