#!/usr/bin/python3

import tkinter as tk
from PIL import ImageTk,Image  
import _thread
import socket
from controlador import controlador # clase creada, usa a configuracion parametros y generico
import sys
import time
import generico # clase creada , usa a configuracion parametros
import math 
import configuracionParametros # clase creada , no usa a nadie
import datetime
import logging

fch_dia = datetime.datetime.today()
nombre_log = 'log/pantalla_transito_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
logger = logging.getLogger(__name__)
logger.setLevel(logging.DEBUG)
file_handler = logging.FileHandler(nombre_log)
logger.addHandler(file_handler)
logger.warning("Warning: Inicio  :"+ str(fch_dia))
logger.info("Info: "+str(fch_dia) + " ")

listaLL =[]
listaS =[]
respuesta = ['rojo','','']
flag1 = "actualiza"
flag2 = "actualiza"
flag3 = "actualizado"
flag1S = "actualizado"
flag2S = "actualizado"
flag3S = "actualizado"
stdOri = 1
stdDes = 1
tiempo = configuracionParametros.tiempoRefreshSegundos(logger)
datoOri = ["","",""]
datoDes = ["","",""]
str_data = "default"
flag_fin = False



class ventana :
	def __init__(self):
		self.root = tk.Tk()
		self.root.attributes('-fullscreen',True)
		self.root.configure(background='white')
		try:

			self.imgGo = ImageTk.PhotoImage(Image.open("img/go.png"))
			self.imgError = ImageTk.PhotoImage(Image.open("img/imgError.png"))
			self.imgReini = ImageTk.PhotoImage(Image.open("img/reinicio.png"))
			self.imgEncendido = ImageTk.PhotoImage(Image.open("img/encendido.png"))
			self.LAPw = ImageTk.PhotoImage(Image.open("img/cabecera.png"))
			self.izquierda = ImageTk.PhotoImage(Image.open("img/izquierda.png"))
			self.derecha = ImageTk.PhotoImage(Image.open("img/derecha.png"))
			self.BgVuelos = ImageTk.PhotoImage(Image.open("img/BgVuelos.png"))
			self.regresarBL = ImageTk.PhotoImage(Image.open("img/regresarBlanco.png"))
			self.regresarRO = ImageTk.PhotoImage(Image.open("img/regresarRojo.png"))
			self.blanco60x60 = ImageTk.PhotoImage(Image.open("img/blanco60x60.png"))
			self.blanco36x60 = ImageTk.PhotoImage(Image.open("img/blanco36x60.png"))
			self.rojo160x45 =  ImageTk.PhotoImage(Image.open("img/rojo160x45.png"))
			self.verde160x45 = ImageTk.PhotoImage(Image.open("img/verde160x45.png"))
			self.ambar160x45 = ImageTk.PhotoImage(Image.open("img/ambar160x45.png"))
			self.candado = ImageTk.PhotoImage(Image.open("img/candado.png"))
			self.cabeza1 =  ImageTk.PhotoImage(Image.open("img/cabeza1.png"))
			self.cabeza2 =  ImageTk.PhotoImage(Image.open("img/cabeza2.png"))
			self.reloj =  ImageTk.PhotoImage(Image.open("img/clock.png"))

		except:
			print("No se encontro la imagen")

		self.conteo = 10
		self.conteo2 = 0
		self.num = 0
		self.obj_controller = controlador(configuracionParametros.direccionControlador(logger))

		self.L1 = tk.Label(text="", fg="#636363",compound= tk.CENTER, font=("Helvetica", 10,"bold"),bg='white',wraplength="18c")
		self.L2= tk.Label(image = "",compound= tk.CENTER,bg='white')
		self.L3 = tk.Label(image="", fg="#0050AF", font=("Helvetica", 20,"bold"),bg='white',wraplength="18c")

		self.L5 = tk.Label(text="", fg="#0050AF", compound= tk.CENTER, font=("Helvetica", 12,"bold"),bg='white',wraplength="18c")
		self.L6 = tk.Label(text="", fg="#636363", font=("Helvetica", 10,"bold"),bg='white',wraplength="18c")
		self.L7 = tk.Label(image=self.blanco60x60, fg="#FFFFFF", font=("Helvetica", 12),bg='white',wraplength="14c",height=18,width=455)
		self.L9 = tk.Label(image=self.blanco60x60, fg="#FFFFFF", font=("Helvetica", 12),bg='white',wraplength="14c",height=18,width=455)
		self.L8 = tk.Label(text="", fg="#0050AF", font=("Helvetica", 12),bg='white',wraplength="14c",)

		self.boton1= tk.Button(image = self.BgVuelos,text="",bg="white",font=(configuracionParametros.tipoLetraBotones(logger), 16,"bold"), compound= tk.CENTER,relief="flat",state="disabled",height=70,width=455,command=self.funcionBoton1)
		self.boton2= tk.Button(image = self.BgVuelos,text="",bg="white",font=(configuracionParametros.tipoLetraBotones(logger), 16,"bold"), compound= tk.CENTER,relief="flat",state="disabled",height=70,width=455,command=self.funcionBoton2)
		self.boton3= tk.Button(image = self.BgVuelos,text="",bg="white",font=(configuracionParametros.tipoLetraBotones(logger), 16,"bold"), compound= tk.CENTER,relief="flat",state="disabled",height=70,width=455,command=self.funcionBoton3)
		self.boton4= tk.Button(image = self.BgVuelos,text="",bg="white",font=(configuracionParametros.tipoLetraBotones(logger), 16,"bold"), compound= tk.CENTER,relief="flat",state="disabled",height=70,width=455,command=self.funcionBoton4)
		self.boton5= tk.Button(image = self.BgVuelos,text="",bg="white",font=(configuracionParametros.tipoLetraBotones(logger), 16,"bold"), compound= tk.CENTER,relief="flat",state="disabled",height=70,width=455,command=self.funcionBoton5)
		self.boton6= tk.Button(image = self.BgVuelos,text="",bg="white",font=(configuracionParametros.tipoLetraBotones(logger), 16,"bold"), compound= tk.CENTER,relief="flat",state="disabled",height=70,width=455,command=self.funcionBoton6)
		self.boton7= tk.Button(image = self.BgVuelos,text="",bg="white",font=(configuracionParametros.tipoLetraBotones(logger), 16,"bold"), compound= tk.CENTER,relief="flat",state="disabled",height=70,width=455,command=self.funcionBoton7)
		self.boton8= tk.Button(image = self.BgVuelos,text="",bg="white",font=(configuracionParametros.tipoLetraBotones(logger), 16,"bold"), compound= tk.CENTER,relief="flat",state="disabled",height=70,width=455,command=self.funcionBoton8)

		self.boton13= tk.Button(image=self.blanco60x60,bg="white", compound= tk.CENTER,relief="flat",state="disabled",font="bold",height=60,width=60,command=self.helloCallBack2)
		self.boton14= tk.Button(image=self.blanco36x60, compound= tk.CENTER,bg="white",relief="flat",state="disabled",font="bold",height=36,width=60,command=self.helloCallBack3)
		self.boton15= tk.Button(image=self.blanco36x60, compound= tk.CENTER,bg="white",relief="flat",state="disabled",font="bold",height=36,width=60,command=self.helloCallBack4)
																											# 2         10
		self.listBotVuel = [self.boton1,self.boton2,self.boton3,self.boton4,self.boton5,self.boton6,self.boton7,self.boton8]#,self.boton9,self.boton10]

		self.L2.grid(sticky="WE",row=0 ,column= 0) #imagen de cabeza
		self.L1.grid(row=0 ,column= 1)
		self.L3.grid(row=1 ,column= 0,columnspan=2)
		
		self.L5.grid(sticky="WE",row=2 ,column= 0,columnspan=2)
		self.L6.grid(sticky="WE",row=3 ,column= 0,columnspan=2)
		self.boton1.grid(row=4 ,column= 0 ,columnspan=2)
		self.boton2.grid(row=5 ,column= 0 ,columnspan=2)
		self.boton3.grid(row=6 ,column= 0 ,columnspan=2)
		self.boton4.grid(row=7 ,column= 0 ,columnspan=2)
		self.boton5.grid(row=8 ,column= 0 ,columnspan=2)
		self.boton6.grid(row=9 ,column= 0 ,columnspan=2)
		self.boton7.grid(row=10 ,column= 0 ,columnspan=2)
		self.boton8.grid(row=11 ,column= 0 ,columnspan=2)
		self.L7.grid(row=12,column=0,columnspan=2) # espacio
		self.L8.grid(row=13,column=0,columnspan=2) # texto de pagina
		self.boton14.grid(row=13 ,column= 0 )
		self.boton15.grid(row=13 ,column= 1 )
		self.L9.grid(row=14,column=0,columnspan=2) # espacio
		self.boton13.grid(row=15 ,column= 1 ) # boton de regresar

	def funcionBoton1(self):
		global str_data
		global str_data_anti
		global flag2
		global flag2S
		global datoOri
		global datoDes
		global flag3
		global flag3S
		if (str_data == "bienvenido"):
			datoOri = [listaLL[1][self.num][0],listaLL[1][self.num][1],listaLL[1][self.num][2],""]
			str_data_anti = True
			str_data = "pase2"
			flag2 = "actualiza"
			flag2S = "actualiza"
			print(datoOri)
		elif (str_data  == "pase2"):
			datoDes = [listaS[1][self.num][0],listaS[1][self.num][1],listaS[1][self.num][2],""]
			str_data_anti = True
			str_data = "espera"
			flag3 = "actualiza"
			flag3S = "actualiza"
			print(datoDes)
		else :
			print("OK")
		time.sleep(0.2)

	def funcionBoton2(self):
		global str_data
		global str_data_anti
		global flag2
		global flag2S
		global datoOri
		global datoDes
		global flag3
		global flag3S
		if (str_data == "bienvenido"):
			datoOri = [listaLL[1][self.num+1][0],listaLL[1][self.num+1][1],listaLL[1][self.num+1][2],""]
			str_data_anti = True
			str_data = "pase2"
			flag2 = "actualiza"
			flag2S = "actualiza"
			print(datoOri)
		elif (str_data  == "pase2"):
			datoDes = [listaS[1][self.num+1][0],listaS[1][self.num+1][1],listaS[1][self.num+1][2],""]
			str_data_anti = True
			str_data = "espera"
			flag3 = "actualiza"
			flag3S = "actualiza"
			print(datoDes)
		else :
			print("OK")
		time.sleep(0.2)

	def funcionBoton3(self):
		global str_data
		global str_data_anti
		global flag2
		global flag2S
		global datoOri
		global datoDes
		global flag3
		global flag3S
		if (str_data == "bienvenido"):
			datoOri = [listaLL[1][self.num+2][0],listaLL[1][self.num+2][1],listaLL[1][self.num+2][2],""]
			str_data_anti = True
			str_data = "pase2"
			flag2 = "actualiza"
			flag2S = "actualiza"
			print(datoOri)
		elif (str_data  == "pase2"):
			datoDes = [listaS[1][self.num+2][0],listaS[1][self.num+2][1],listaS[1][self.num+2][2],""]
			str_data_anti = True
			str_data = "espera"
			flag3 = "actualiza"
			flag3S = "actualiza"
			print(datoDes)
		else :
			print("OK")
		time.sleep(0.2)

	def funcionBoton4(self):
		global str_data
		global str_data_anti
		global flag2
		global flag2S
		global datoOri
		global datoDes
		global flag3
		global flag3S
		if (str_data == "bienvenido"):
			datoOri = [listaLL[1][self.num+3][0],listaLL[1][self.num+3][1],listaLL[1][self.num+3][2],""]
			str_data_anti = True
			str_data = "pase2"
			flag2 = "actualiza"
			flag2S = "actualiza"
			print(datoOri)
		elif (str_data  == "pase2"):
			datoDes = [listaS[1][self.num+3][0],listaS[1][self.num+3][1],listaS[1][self.num+3][2],""]
			str_data_anti = True
			str_data = "espera"
			flag3 = "actualiza"
			flag3S = "actualiza"
			print(datoDes)
		else :
			print("OK")
			time.sleep(0.2)

	def funcionBoton5(self):
		global str_data
		global str_data_anti
		global flag2
		global flag2S
		global datoOri
		global datoDes
		global flag3
		global flag3S
		if (str_data == "bienvenido"):
			datoOri = [listaLL[1][self.num+4][0],listaLL[1][self.num+4][1],listaLL[1][self.num+4][2],""]
			str_data_anti = True
			str_data = "pase2"
			flag2 = "actualiza"
			flag2S = "actualiza"
			print(datoOri)
		elif (str_data  == "pase2"):
			datoDes = [listaS[1][self.num+4][0],listaS[1][self.num+4][1],listaS[1][self.num+4][2],""]
			str_data_anti = True
			str_data = "espera"
			flag3 = "actualiza"
			flag3S = "actualiza"
			print(datoDes)
		else :
			print("OK")
		time.sleep(0.2)

	def funcionBoton6(self):
		global str_data
		global str_data_anti
		global flag2
		global flag2S
		global datoOri
		global datoDes
		global flag3
		global flag3S
		if (str_data == "bienvenido"):
			datoOri = [listaLL[1][self.num+5][0],listaLL[1][self.num+5][1],listaLL[1][self.num+5][2],""]
			str_data_anti = True
			str_data = "pase2"
			flag2 = "actualiza"
			flag2S = "actualiza"
			print(datoOri)
		elif (str_data  == "pase2"):
			datoDes = [listaS[1][self.num+5][0],listaS[1][self.num+5][1],listaS[1][self.num+5][2],""]
			str_data_anti = True
			str_data = "espera"
			flag3 = "actualiza"
			flag3S = "actualiza"
			print(datoDes)
		else :
			print("OK")#asesores contables tributarios
		time.sleep(0.2)
#que puede hacer david 
	def funcionBoton7(self):
		global str_data
		global str_data_anti
		global flag2
		global flag2S
		global datoOri
		global datoDes
		global flag3
		global flag3S
		if (str_data == "bienvenido"):
			datoOri = [listaLL[1][self.num+6][0],listaLL[1][self.num+6][1],listaLL[1][self.num+6][2],""]
			str_data_anti = True
			str_data = "pase2"
			flag2 = "actualiza"
			flag2S = "actualiza"
			print(datoOri)
		elif (str_data  == "pase2"):
			datoDes = [listaS[1][self.num+6][0],listaS[1][self.num+6][1],listaS[1][self.num+6][2],""]
			str_data_anti = True
			str_data = "espera"
			flag3 = "actualiza"
			flag3S = "actualiza"
			print(datoDes)
		else :
			print("OK")
		time.sleep(0.2)

	def funcionBoton8(self):
		global str_data
		global str_data_anti
		global flag2
		global flag2S
		global datoOri
		global datoDes
		global flag3
		global flag3S
		if (str_data == "bienvenido"):
			datoOri = [listaLL[1][self.num+7][0],listaLL[1][self.num+7][1],listaLL[1][self.num+7][2],""]
			str_data_anti = True
			str_data = "pase2"
			flag2 = "actualiza"
			flag2S = "actualiza"
			print(datoOri)
		elif (str_data  == "pase2"):
			datoDes = [listaS[1][self.num+7][0],listaS[1][self.num+7][1],listaS[1][self.num+7][2],""]
			str_data_anti = True
			str_data = "espera"
			flag3 = "actualiza"
			flag3S = "actualiza"
			print(datoDes)
			print("clicked at")
		else :
			print("OK")
		time.sleep(0.2)

	def helloCallBack2(self):
		global str_data
		global str_data_anti
		global stdOri
		global stdDes
		global flag1
		global flag1S
		if (str_data == "pase2"):
			str_data_anti = True
			flag1 = "actualiza"
			flag1S = "actualiza"
			str_data = "bienvenido"
			stdOri = 1
			stdDes = 1
		else:
			print("no pasa nada 1")
			#str_data_anti = True
			#str_data = "pase2"
		print("clicked at")

	def helloCallBack3(self):
		global str_data
		global stdOri
		global stdDes
		global flag1S
		flag1S = "actualiza"
		if (str_data == "bienvenido"):
			stdOri = stdOri - 1
		elif (str_data == "pase2"):
			stdDes = stdDes - 1
		else :
			print("no pasa nada 2")
		print("clicked at")

	def helloCallBack4(self):
		global str_data
		global stdDes
		global stdOri
		global flag1S
		flag1S = "actualiza"
		if (str_data == "bienvenido"):
			stdOri = stdOri + 1
		elif (str_data == "pase2"):
			stdDes = stdDes + 1
		else :
			print("no pasa nada 3")
		print("clicked at")
 
	def inicio(self):
		self.root.after(900, self.update)
		self.root.mainloop()

	def update(self):
		try:
			global str_data
			global listaLL
			global listaS
			global respuesta
			global flag1
			global flag2
			global flag3
			global flag1S
			global flag2S
			global flag3S
			global stdOri
			global stdDes
			global tiempo
			global datoOri
			global datoDes
			global str_data
			global str_data_anti
			global flag_fin

			if (str_data == "reinicio"):

				if (str_data_anti):
					logger.info("Info: "+str(datetime.datetime.today())+" Se reinicio el programa")
					str_data_anti = False
				if (self.conteo > -1 ):
					self.root.configure(background='white')
					self.L1.config(image="",text="",bg='white')
					self.L2.config(image="",bg='white')
					self.L3.config(image=self.imgReini,text="",fg="white",bg='white')
					self.L5.config(image="",text="",bg='white')
					self.L6.config(text=self.conteo ,font=("Helvetica", 20,"bold"),fg="#636363",bg='white')
					self.L7.config(image=self.blanco60x60,text="",bg='white')
					self.L9.config(image=self.blanco60x60,text="",bg='white')
					self.L8.config(text="",bg='white')
					for i in range(8):
						self.listBotVuel[i].config(image=self.blanco60x60,text="",bg='white',height=1,width=1,relief="flat",state="normal")

					self.boton14.config(image=self.blanco60x60,bg='white',height=1,width=1,relief="flat",state="normal")
					self.boton15.config(image=self.blanco60x60,bg='white',height=1,width=1,relief="flat",state="normal")
					self.boton13.config(image=self.blanco60x60,bg='white',height=1,width=1,relief="flat",state="normal")
					#self.L7.config(image=blanco36x60 , fg = "black",bg="white")
					#self.L8.config(text="" , fg = "black",bg="white")
					self.conteo = self.conteo - 1
					time.sleep(0.5)
				elif (self.conteo == -1):
					time.sleep(0.5)
					self.root.destroy()

			elif (str_data == "pase2"):
				if (str_data_anti):
					logger.info("Info: "+str(datetime.datetime.today())+" entro en la segunda pantalla")
					str_data_anti = False
				if(listaS[0] == 200):
					pags = math.ceil(len(listaS[1])/8)
				else:
					pags = 0
				self.L1.config(image = self.reloj, text=generico.diahora(),bg="white")
				self.L2.config(image=self.LAPw , bg ="white")
				self.L3.config(image=self.cabeza2, fg="#0050AF",bg="white")

				self.L5.config(image=self.BgVuelos,text="Vuelo de origen : "+datoOri[0]+" "+datoOri[1]+" "+datoOri[2].split("T")[1] ,bg="white")
				self.L6.config(text="Destino/Destination  \t\t\t Nro.Vuelo/Flight", fg="#636363", font=("Helvetica", 10,"bold"),bg="white")
				self.L7.config(image=self.blanco36x60,bg="white")
				self.L9.config(image=self.blanco36x60,bg="white")
				self.L8.config(text="Página "+str(stdDes)+" de "+str(pags),bg="white")
				self.boton13.config(image=self.regresarBL,relief="flat",height=60,width=60,state="normal",fg="white",bg="white")

				if (stdDes == pags):
					if(stdDes == 1):
						self.num=0 
						self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
					else:
						self.num=(stdDes-1)*8
						self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
				elif(stdDes < pags):
					if(stdDes == 1):
						self.num=0
						self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
					elif(stdDes < 1):
						self.num=0
						stdDes = 1
						self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
					else:
						self.num=(stdDes-1)*8
						self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
				else :
					stdDes = pags
					self.num=(stdDes-1)*8
					self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
					self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")

				self.root.configure(background="white")
				for i in range(8):
					try:
						temp = listaS[1][self.num+i][0]
						temp1 = listaS[1][self.num+i][2].split("T")[1]
						temp2 = listaS[1][self.num+i][1]
					except:
						self.listBotVuel[i].config(image=self.BgVuelos,text= "",height=45,width=455,state="disabled",fg="#1DA0D7",bg="white")
					else:
						self.listBotVuel[i].config(image=self.BgVuelos,height=45,width=455, text=generico.armar_cadena(listaS[1][self.num+i][2].split("T")[1],listaS[1][self.num+i][0],listaS[1][self.num+i][1],logger),state="normal",fg="#1DA0D7",bg="white")

			elif (str_data == "espera"):
				stdDes = 1
				stdOri = 1
				tempcolor= "#EDB200"
				tempImg=self.ambar160x45
				tempBotones = self.ambar160x45
				tempRegreso = ""
				msjMostrar = configuracionParametros.mnsjEspera(logger)# "Espere ... "
				self.root.configure(background=tempcolor)
				self.L1.config(image="",text="",bg=tempcolor)
				self.L2.config(image="",bg=tempcolor)
				#self.L3.config(image=tempImg,text="",fg="white",bg=tempcolor)
				self.L3.config(image="",text="",fg="white",bg=tempcolor)
				self.L5.config(image="",text="",bg=tempcolor)
				self.L6.config(text=msjMostrar ,font=("Helvetica", 20,"bold"),fg="white",bg=tempcolor)
				self.L7.config(image=tempBotones,text="",bg=tempcolor)
				self.L9.config(image=tempBotones,text="",bg=tempcolor)
				self.L8.config(text="",bg=tempcolor)
				self.boton14.config(image="",text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")
				self.boton15.config(image="",text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")
				for i in range(8):
					self.listBotVuel[i].config(image=tempBotones,text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")

				self.boton13.config(image=tempRegreso,bg=tempcolor,height=1,width=1,relief="flat",state="normal")
				if flag_fin:
					flag_fin = False
					str_data = "pase3"

			elif (str_data == "pase3" or str_data == "pase3V2"):

				stdDes = 1
				stdOri = 1
				#time.sleep(0.7)
				print("&&&&6PRINTEO ANTES DEL PANTALLAZO")
				print(respuesta)
				if (respuesta[0]=="rojo"):
					if (str_data_anti):
						logger.info("Info: "+str(datetime.datetime.today())+" pantalla de error de pase")
						str_data_anti = False
					tempImg=self.imgError
					tempcolor= "#ED1B24"
					tempRegreso = ""
					tempBotones = self.rojo160x45
					msjMostrar = respuesta[1]
				else:
					if (str_data_anti):
						logger.info("Info: "+str(datetime.datetime.today())+" pantalla de pase")
						str_data_anti = False
					tempImg=self.imgGo
					tempcolor= "#008700"
					tempBotones = self.verde160x45
					tempRegreso = ""
					msjMostrar = respuesta[1]
				self.root.configure(background=tempcolor)
				self.L1.config(image="",text="",bg=tempcolor)
				self.L2.config(image="",bg=tempcolor)
				self.L3.config(image=tempImg,text="",fg="white",bg=tempcolor)
				self.L5.config(image="",text="",bg=tempcolor)
				self.L6.config(text=msjMostrar ,font=("Helvetica", 20,"bold"),fg="white",bg=tempcolor)
				self.L7.config(image=tempBotones,text="",bg=tempcolor)
				self.L9.config(image=tempBotones,text="",bg=tempcolor)
				self.L8.config(text="",bg=tempcolor)
				self.boton14.config(image="",text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")
				self.boton15.config(image="",text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")

				for i in range(8):
					self.listBotVuel[i].config(image=tempBotones,text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")

				self.boton13.config(image=tempRegreso,bg=tempcolor,height=1,width=1,relief="flat",state="normal")
				if(str_data=="pase3V2"):
					str_data_anti = True
					str_data = "bienvenido"
					flag1 = "actualiza"
					flag1S = "actualiza"

					if (respuesta[0]=="rojo"):
						time.sleep(1)
						print("MANTENER OBSTACULO")
						if (respuesta[2]!="llave"):
							str_feedback = self.obj_controller.mantener_obstaculo(logger)
							print(str_feedback)
						else:
							if (respuesta[1]=="Llave traba"):
								str_feedback = self.obj_controller.bloqueo(logger)
								print(str_feedback)
							else:
								time.sleep(1)
					else :
						print("APERTURA DE PUERTA")
						#time.sleep(1)
						if (respuesta[2]=="llave"):
							if (respuesta[1]=="Llave destrabe"):
								str_feedback = self.obj_controller.verifica_abertura_puerta(logger)
								print(str_feedback)
							elif (respuesta[1]=="Llave multiple"):
								datoC = self.obj_controller.estadoObstaculo()[1]
								if ("E"==datoC or "L"==datoC):
									str_feedback = self.obj_controller.abrir(logger)
									print(str_feedback)
								else:
									str_feedback = self.obj_controller.normal(logger)
									print(str_feedback)
							elif (respuesta[1]=="Llave apertura"):
								str_feedback = self.obj_controller.normal(logger)
								print(str_feedback)
							else:
								str_feedback = self.obj_controller.mantener_obstaculo(logger)
								print(str_feedback)
						else:
							datoC = self.obj_controller.estadoObstaculo()[1]
							if ("O"==datoC):
								time.sleep(1)
							else:
								str_feedback = self.obj_controller.verifica_abertura_puerta(logger)
								print(str_feedback)
				else :
					str_data_anti = True
					str_data="pase3V2"

			elif (str_data == "trabado"):
				stdDes = 1
				stdOri = 1
				tempcolor= "#EDB200"
				tempImg=self.candado
				tempBotones = self.ambar160x45
				tempRegreso = ""
				msjMostrar = "Bloqueado"
				str_feedback = self.obj_controller.estadoObstaculo()
				print(str_feedback)
				if (str_feedback[1] != "L"):
					str_feedback = self.obj_controller.bloqueo(logger)
				else:
					pass
				
				self.root.configure(background=tempcolor)
				self.L1.config(image="",text="",bg=tempcolor)
				self.L2.config(image="",bg=tempcolor)
				self.L3.config(image=tempImg,text="",fg="white",bg=tempcolor)
				self.L5.config(image="",text="",bg=tempcolor)
				self.L6.config(text=msjMostrar ,font=("Helvetica", 20,"bold"),fg="white",bg=tempcolor)
				self.L7.config(image=tempBotones,text="",bg=tempcolor)
				self.L9.config(image=tempBotones,text="",bg=tempcolor)
				self.L8.config(text="",bg=tempcolor)
				self.boton14.config(image="",text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")
				self.boton15.config(image="",text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")
				for i in range(8):
					self.listBotVuel[i].config(image=tempBotones,text="",bg=tempcolor,height=1,width=1,relief="flat",state="normal")

				self.boton13.config(image=tempRegreso,bg=tempcolor,height=1,width=1,relief="flat",state="normal")

			elif (str_data == "bienvenido"):
				if (str_data_anti):
					logger.info("Info: "+str(datetime.datetime.today())+" entro en la primera pantalla")
					str_data_anti = False
				if(listaLL[0] == 200):
					pags = math.ceil(len(listaLL[1])/8)
				else:
					pags = 0
				self.L1.config(image = self.reloj,text=generico.diahora(),bg="white")
				self.L2.config(image=self.LAPw , bg ="white")
				self.L3.config(image = self.cabeza1, fg="#0050AF",bg="white")
				self.L5.config(image="",text="",bg="white")
				self.L6.config(text="Origen/Origin        \t\t\t Nro.Vuelo/Flight" , fg="#636363",font=("Helvetica", 10,"bold"),bg="white")
				self.L7.config(image=self.blanco36x60,text="",bg="white")
				self.L9.config(image=self.blanco36x60,text="",bg="white")
				self.L8.config(text="Página "+str(stdOri)+" de "+str(pags),bg="white")

				if (stdOri == pags):
					if(stdOri == 1):
						self.num=0
						self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
					else:
						self.num=(stdOri-1)*8
						self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")
				elif(stdOri < pags):
					if(stdOri == 1):
						self.num=0
						self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
					elif(stdOri < 1):
						self.num=0
						stdOri = 1
						self.boton14.config(image=self.izquierda,height=36,width=60,state="disabled",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
					else:
						self.num=(stdOri-1)*8
						self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
						self.boton15.config(image=self.derecha,height=36,width=60,state="normal",fg="black",bg="white")
				else :
					stdOri = pags
					self.num=(stdOri-1)*8
					self.boton14.config(image=self.izquierda,height=36,width=60,state="normal",fg="black",bg="white")
					self.boton15.config(image=self.derecha,height=36,width=60,state="disabled",fg="black",bg="white")

				self.root.configure(background="white")
				for i in range(8):
					try:
						temp = listaLL[1][self.num+i][0]
						temp2 = listaLL[1][self.num+i][2].split("T")[1]
						temp3 = listaLL[1][self.num+i][1]
					except:
						self.listBotVuel[i].config(image=self.BgVuelos,text= "",state="disabled",height=45,width=455,fg="#0050AF",bg="white")
					else:
						self.listBotVuel[i].config(image=self.BgVuelos,height=45,width=455, text=generico.armar_cadena(listaLL[1][self.num+i][2].split("T")[1],listaLL[1][self.num+i][0],listaLL[1][self.num+i][1],logger),state="normal",fg="#0050AF",bg="white")
					
				self.boton13.config(image=self.blanco60x60,relief="flat",height=60,width=60,state="disabled",fg="white",bg="white")

			elif (str_data == "default"):
				if (self.conteo2 < 11 ):
					self.root.configure(background="white")
					self.L2.config(image=self.imgEncendido,bg="white")
					self.L1.config(image="",text="",bg="white")

					self.L3.config(text=self.conteo2 ,bg="white")
					self.conteo2 = self.conteo2 +1
					time.sleep(0.5)
				elif (self.conteo2 == 11):
					self.obj_controller.desbloqueo(logger)
					time.sleep(0.5)
					str_data_anti = True
					str_data = "bienvenido"
			else :
				self.root.configure(background="white")
				self.L2.config(image=self.imgError,bg="white")

				self.L8.config(text="" , fg = "white",bg="white")

			self.root.after(500, self.update)
		except Exception as e :
			logger.info("Error: "+str(datetime.datetime.today())+" Error en update en pantalla "+str(e))


#procedimientos

def proceso_llenado_listas():
	try:
		while True :
			global listaLL
			global listaS
			global flag1
			global flag2
			global flag3
			global respuesta
			global flag_fin
			global str_data
			if str_data == "reinicio":
				break
			if (flag1 == "actualiza"):
				print("entro en el flag1")
				listaLL = generico.lista_vuelos_llegadas(logger)
				print("#######################")
				print("LLEGADAS")
				#print(listaLL)
				#listaLL = [200,[("CALI","LA2428","2019-12-03T10:41:00"),("4","5","6T1"),("7","8","9T1"),("10","11","12T1"),("13","14","15T1"),("16","17","18T1"),("19","20","21T1"),("22","23","24T1"),("25","26","27T1"),("28","29","30T1"),("31","32","33T1"),("34","35","36T1"),("37","38","39T1"),("40","41","42T1"),("43","44","45T1"),("46","47","48T1"),("49","50","51T1"),("52","53","54T1"),("55","56","57T1"),("58","59","60T1"),("61","62","63T1"),("64","65","66T1"),("67","68","69T1"),("70","71","72T1")]]

				flag1 = "actualizado"
			#print("antes del flag2")
			if (flag2 == "actualiza"):
				print("entro en el flag2")
				listaS = generico.lista_vuelos_salidas(datoOri[0],logger)
				print("#######################")
				print("SALIDAS")
				#print(listaS)
				#listaS = [200,[("1","2","3T1"),("4","5","6T1"),("7","8","9T1"),("10","11","12T1"),("13","14","15T1"),("16","17","18T1"),("19","20","21T1"),("22","23","24T1"),("25","26","27T1"),("28","29","30T1"),("31","32","33T1"),("34","35","36T1"),("37","38","39T1"),("40","41","42T1"),("43","44","45T1"),("46","47","48T1"),("49","50","51T1"),("52","53","54T1"),("55","56","57T1"),("58","59","60T1"),("61","62","63T1"),("64","65","66T1"),("67","68","69T1"),("70","71","72T1")]]
				flag2 = "actualizado"
			if (flag3 == "actualiza"):
				print("entro en el flag3")
				print("1***************************************")
				internet = generico.internet_on(logger)
				if (internet):
					print("ENTRO EN EL INTERNET")
					estado = generico.estado_webservice(logger)
					if (estado[1] == "ok"):
						respuesta = generico.Post_datos(datoOri[1],datoOri[2],datoDes[1],datoDes[2],configuracionParametros.NombrePuerta(logger),datoOri[3],datoDes[3],logger)
						generico.Guardar_datos(datoOri[1],datoOri[2],datoDes[1],datoDes[2],configuracionParametros.NombrePuerta(logger),datoOri[3],datoDes[3],"si subido",datetime.datetime.today(),logger)
						flag_fin = True
					else:
						generico.Guardar_datos(datoOri[1],datoOri[2],datoDes[1],datoDes[2],configuracionParametros.NombrePuerta(logger),datoOri[3],datoDes[3],"no subido",datetime.datetime.today(),logger)
						respuesta = ['verde','','']
						flag_fin = True
					print("2**************************************")
				else:
					generico.Guardar_datos(datoOri[1],datoOri[2],datoDes[1],datoDes[2],configuracionParametros.NombrePuerta(logger),datoOri[3],datoDes[3],"no subido",datetime.datetime.today(),logger)
					respuesta = ['verde','',''] 
					flag_fin = True
				print(respuesta)
				flag3 = "actualizado"
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en proceso_llenado_listas de pantalla "+str(e))

def timer_actualizacion():
	try:
		global tiempo
		global str_data
		global flag1S
		global flag2S
		global flag3S
		global flag1
		global flag2
		global flag3
		global stdOri
		global stdDes

		while True :
			n = 0
			while n != tiempo :
				if str_data == "reinicio":
					break
				print(n)
				time.sleep(1)
				n = n + 1
				if (flag1S == "actualiza" or flag2S == "actualiza" or flag3S == "actualiza"):
					flag1S = "actualizado"
					flag2S = "actualizado"
					flag3S = "actualizado"
					n = 0
			flag1 = "actualiza"
			flag2 = "actualiza"
			stdOri = 1
			stdDes = 1
			if (str_data == "pase2"):
				str_data_anti = True
				str_data = "bienvenido"
			if str_data == "reinicio":
				break
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en timer_actualizacion pantalla "+str(e))

def escaneo():
	try:
		global str_data
		global datoOri
		global datoDes
		global respuesta

		while True:
			if(str_data != "pase3" or str_data != "pase3V2" or str_data != "espera"):
				str_tipo , str_info_captada = generico.trama_recivida(logger)
				print('INFORMACION DEL SCANEO '+str(str_info_captada))
				#str_info_captada = input("PON AQUI LA TRAMA ")
				encontrar = generico.encontrar_llave(str_info_captada,logger)
				if (encontrar[0] == "pase" and encontrar[1]=="DES"):
					if (str_data != "trabado"):
						str_data_anti = True
						str_data = "pase3"
						respuesta = ['verde','Llave destrabe','']
					else :
						print("no se puede hacer nada")
				elif(encontrar[0] == "pase" and encontrar[1]=="MUL"):
					str_data_anti = True
					str_data = "pase3"
					respuesta = ['verde','Llave multiple','llave']
				elif(encontrar[0] == "pase" and encontrar[1]=="TRA"):
					str_data_anti = True
					#str_data = "pase3"
					if str_data != "trabado" :
						str_data = "trabado"
					respuesta = ['rojo','Llave traba','llave']
				elif(encontrar[0] == "pase" and encontrar[1]=="APR"):
					str_data_anti = True
					str_data = "pase3"
					respuesta = ['verde','Llave apertura','llave']
				elif(encontrar[0] == "pase" and encontrar[1]=="MAS"):
					str_data_anti = True
					str_data = "reinicio"
					break
					#respuesta = ['verde','Llave apertura','llave']
				else:
					if (str_data == "bienvenido"):
						internet = generico.internet_on(logger)
						if (internet):
							estado = generico.estado_webservice(logger)
							if (estado[1] == "ok"):
								rsp = generico.Post_vuelo_dato_llegada(str_info_captada,logger)
								if (rsp[0] != "error" ):
									datoOri = [rsp[0],rsp[1],rsp[2],str_info_captada]
									str_data_anti = True
									str_data = "pase2"
									flag2 = "actualiza"
									flag2S = "actualiza"
							else:
								rsp = generico.verificacion_contingencia(str_info_captada,"origen",logger)
								if (rsp[0] != "NO PASO CONTINGENCIA" ):
									datoOri = [rsp[0],rsp[1],rsp[2],str_info_captada]
									str_data_anti = True
									str_data = "pase2"
									flag2 = "actualiza"
									flag2S = "actualiza"
						else:
							rsp = generico.verificacion_contingencia(str_info_captada,"origen",logger)
							if (rsp[0] != "NO PASO CONTINGENCIA" ):
								datoOri = [rsp[0],rsp[1],rsp[2],str_info_captada]
								str_data_anti = True
								str_data = "pase2"
								flag2 = "actualiza"
								flag2S = "actualiza"

					elif(str_data == "pase2"):
						internet = generico.internet_on(logger)
						if (internet):
							estado = generico.estado_webservice(logger)
							if (estado[1] == "ok"):
								rsp = generico.Post_vuelo_dato_salida(str_info_captada,logger)
								if (rsp[0] != "error" ):
									datoDes = [rsp[0],rsp[1],rsp[2],str_info_captada]
									respuesta = generico.Post_datos(datoOri[1],datoOri[2],datoDes[1],datoDes[2],configuracionParametros.NombrePuerta(logger),datoOri[3],datoDes[3],logger)
									flag_fin = True
									flag3 = "actualiza"
									flag3S = "actualiza"
									str_data_anti = True
									str_data = "pase3"

							else:
								rsp = generico.verificacion_contingencia(str_info_captada,"origen",logger)
								if (rsp[0] != "NO PASO CONTINGENCIA" ):
									datoOri = [rsp[0],rsp[1],rsp[2],str_info_captada]
									str_data_anti = True
									str_data = "pase3"
									flag3 = "actualiza"
									flag3S = "actualiza"
						else:
							rsp = generico.verificacion_contingencia(str_info_captada,"origen",logger)
							if (rsp[0] != "NO PASO CONTINGENCIA" ):
								datoOri = [rsp[0],rsp[1],rsp[2],str_info_captada]
								str_data_anti = True
								str_data = "pase3"
								flag3 = "actualiza"
								flag3S = "actualiza"
					print("PRINTEO DEL RSP")
					print(rsp)
	except Exception as e:
		logger.info("Error: "+str(datetime.datetime.today())+" Error en escaneo pantalla "+str(e))


try:
	win = ventana()
	#_thread.start_new_thread( escaneo, () )
	_thread.start_new_thread( proceso_llenado_listas, () )
	_thread.start_new_thread( timer_actualizacion, () )
	_thread.start_new_thread( win.inicio(), () )

except Exception as e:
	logger.info("Error: "+str(datetime.datetime.today())+" Error en pantalla "+str(e))
	print(e.args)
	print(sys.exc_info())
	print ("Error: unable to start thread")

while 1:
	handler2 = logger.handlers[0]
	logger.removeHandler(handler2)
	fch_dia = datetime.datetime.today()
	nombre_log = 'pantalla_transito_' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
	file_handler = logging.FileHandler(nombre_log)
	logger.addHandler(file_handler)
	time.sleep(20)
	pass