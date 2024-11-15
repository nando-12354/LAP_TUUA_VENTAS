
#!/usr/bin/python3

import tkinter as tk
from PIL import ImageTk,Image  
import configuracionParametros
import _thread
import socket
import sys
import time
# strdata es mensajes cortos strdata2 es razon strdata3 es color
str_data = "default"
str_data2 = ""
str_data3 = "#d3d3d3"
str_data4 = ""

class ventana :
    def __init__(self):
        global str_data2
        global str_data3
        global str_data4
        self.root = tk.Tk()
        self.root.attributes('-fullscreen',True)
        self.root.configure(background='#d3d3d3',cursor="none")
        self.imgGo = ImageTk.PhotoImage(Image.open("go.png"))
        self.imgStopA = ImageTk.PhotoImage(Image.open("stopA.png"))        
        self.imgStop = ImageTk.PhotoImage(Image.open("stop.png"))
        self.imgScan = ImageTk.PhotoImage(Image.open("scan.png"))
        self.imgError = ImageTk.PhotoImage(Image.open("error.png"))
        self.imgReini = ImageTk.PhotoImage(Image.open("reinicio.png"))
        self.imgEncendido = ImageTk.PhotoImage(Image.open("encendido.png"))
        self.LAPl = ImageTk.PhotoImage(Image.open("LAPl.png"))
        self.LAPw = ImageTk.PhotoImage(Image.open("LAPw.png"))
        self.conteo = 10
        self.conteo2 = 0
  
        self.L2= tk.Label(image = '',bg='#d3d3d3') 
        self.L1= tk.Label(text='', fg="white", font=("Helvetica", 20),bg='#d3d3d3') 
        self.L3= tk.Label(text='', fg="white", font=("Helvetica", 20),bg='#d3d3d3')
        self.L4 = tk.Label(text='Encendido', fg="black", font=("Helvetica", 45),bg='#d3d3d3',wraplength="14c")
        self.L5 = tk.Label(text=str_data2, fg="black", font=("Helvetica", 45),bg='#d3d3d3',wraplength="14c")
        self.L6 = tk.Label(text='On', fg="black", font=("Helvetica", 45),bg='#d3d3d3',wraplength="14c")
        self.L7 = tk.Label(image= self.LAPl,bg='#d3d3d3')
        self.L8 = tk.Label(text=str_data4, fg="black", font=("Helvetica", 25),bg='#d3d3d3',wraplength="14c")

        self.L1.pack()
        self.L2.pack()
        self.L3.pack()
        self.L4.pack()
        self.L5.pack()
        self.L6.pack()
        self.L7.pack()
        self.L8.pack(side='left')
        
    def inicio(self):
        self.root.after(1000, self.update)
        self.root.mainloop()

    def update(self):
        global str_data
        global str_data2
        global str_data3
        global str_data4

        if (str_data == "pase"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgGo,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text="" , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text="" , fg = "white",bg=str_data3)
            self.L7.config(image='',bg=str_data3)
            self.L8.config(text=str_data4, fg = "white",bg=str_data3)
        elif (str_data == "no pase"):

            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text="" , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text="" , fg = "white",bg=str_data3)
            self.L8.config(text=str_data4, fg = "white",bg=str_data3)
            if(str_data3 == "red"):
                self.L2.config(image=self.imgStop,bg=str_data3)
                self.L7.config(image='',bg=str_data3)
            else:
                self.L2.config(image=self.imgStopA,bg=str_data3)
                self.L7.config(image='',bg=str_data3)

        elif (str_data == "reinicio"):
            if (self.conteo > -1 ):
                self.root.configure(background='white')
                self.L1.config( bg = "white")
                self.L2.config(image=self.imgReini,bg='white')
                self.L3.config( bg = "white")
                self.L4.config(text= str(self.conteo) , fg = "black",bg='white')
                self.L5.config(text= "", fg = "black",bg='white')
                self.L6.config(text="" , fg = "black",bg='white')
                self.L7.config(image=self.LAPw,bg='white')
                self.L8.config(text=str_data4 , fg = "black",bg=str_data3)
                self.conteo = self.conteo - 1
                time.sleep(0.5)
            elif (self.conteo == -1):
                time.sleep(0.5)
                self.root.destroy()
        elif (str_data == "errPAR"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgError,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text="Error de parametros" , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text="Parameter error" , fg = "white",bg=str_data3)
            self.L7.config(image='',bg=str_data3)
            self.L8.config(text=str_data4 , fg = "white",bg=str_data3)
        elif (str_data == "errBD"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgError,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text="Error BD" , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text="BD Error" , fg = "white",bg=str_data3)
            self.L7.config(image='',bg=str_data3)
            self.L8.config(text=str_data4 , fg = "white",bg=str_data3)
        elif (str_data == "bloqueado"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgScan,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text=configuracionParametros.bloqueadoEsp() , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text=configuracionParametros.bloqueadoEn() , fg = "white",bg=str_data3)
            self.L7.config(image='',bg=str_data3)
            self.L8.config(text=str_data4 , fg = "white",bg=str_data3)
        elif (str_data == "desbloqueado"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgScan,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text=configuracionParametros.desbloqueadoEsp() , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text=configuracionParametros.desbloqueadoEn() , fg = "white",bg=str_data3)
            self.L7.config(image='',bg=str_data3)
            self.L8.config(text=str_data4 , fg = "white",bg=str_data3)
        elif (str_data == "mul"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgScan,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text=configuracionParametros.mulEsp() , fg = "#024896",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "#024896",bg=str_data3)
            self.L6.config(text=configuracionParametros.mulEn() , fg = "#024896",bg=str_data3)
            self.L7.config(image='',bg=str_data3)
            self.L8.config(text=str_data4 , fg = "#024896",bg=str_data3)
        elif (str_data == "no pase mul"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text="" , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text="" , fg = "white",bg=str_data3)
            self.L8.config(text=str_data4 , fg = "white",bg=str_data3)
            if(str_data3 == "red"):
                self.L2.config(image=self.imgStop,bg=str_data3)
                self.L7.config(image='',bg=str_data3)
            else:
                self.L2.config(image=self.imgStopA,bg=str_data3)
                self.L7.config(image='',bg=str_data3)			

        elif (str_data == "pase mul"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgGo,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text="" , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text="" , fg = "white",bg=str_data3)
            self.L7.config(image='',bg=str_data3)
            self.L8.config(text=str_data4 , fg = "white",bg=str_data3)
        elif (str_data == "siguiente mul"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgScan,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text=configuracionParametros.mnsjBienvenidoEsp() , fg = "#024896",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "#024896",bg=str_data3)
            self.L6.config(text=configuracionParametros.mnsjBienvenidoEng()+"\n" , fg = "#024896",bg=str_data3)
            self.L7.config(image=self.LAPl,bg=str_data3)
            self.L8.config(text=str_data4 , fg = "#024896",bg=str_data3)
        elif (str_data == "bienvenido"):
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image='',bg =str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text=configuracionParametros.mnsjBienvenidoEsp() , fg = "#024896",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "#024896",bg=str_data3)
            self.L6.config(text=configuracionParametros.mnsjBienvenidoEng()+"\n" , fg = "#024896",bg=str_data3)
            self.L7.config(image=self.LAPl, fg = "black",bg=str_data3)
            self.L8.config(text=str_data4 , fg = "#024896",bg=str_data3)

        elif (str_data == "default"):
            if (self.conteo2 < 11 ):
                self.root.configure(background=str_data3)
                self.L2.config(image=self.imgEncendido,bg=str_data3)
                self.L4.config(text= str(self.conteo2) , fg = "#024896",bg=str_data3)
                self.L5.config(text= str_data2 , fg = "#024896",bg=str_data3)
                self.L6.config(text="" , fg = "#024896",bg=str_data3)
                self.L7.config(image=self.LAPl , fg = "black",bg=str_data3)
                self.L8.config(text=str_data4 , fg = "black",bg=str_data3)
                self.conteo2 = self.conteo2 +1
                time.sleep(0.5)
            elif (self.conteo2 == 11):
                time.sleep(0.5)
                str_data = "bienvenido"

        else :
            self.root.configure(background=str_data3)
            self.L1.config( bg = str_data3)
            self.L2.config(image=self.imgStop,bg=str_data3)
            self.L3.config( bg = str_data3)
            self.L4.config(text="Error" , fg = "white",bg=str_data3)
            self.L5.config(text=str_data2 , fg = "white",bg=str_data3)
            self.L6.config(text="Error" , fg = "white",bg=str_data3)
            self.L7.config(image=self.LAPw,bg=str_data3)
            self.L8.config(text=str_data4 , fg = "white",bg=str_data3)

        self.root.after(500, self.update)

def conexion_serial():
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_address = ('', 10000)
    sock.bind(server_address)
    print('starting up on {} port {}'.format(*sock.getsockname()))
    sock.listen(1)
    while True:
        connection, client_address = sock.accept()
        print('client connected:', client_address)

        while True :
            dat = connection.recv(100)
            data = dat.decode("utf-8")
            global str_data
            global str_data2
            global str_data3
            global str_data4
            print("***********")
            if data:
                print(data)                
                str_data,str_data2,color,str_data4 = data.split('|')
                if(color == "lightblue"):
                    str_data3 = "#D3D3D3"
                elif(color == "rojo"):
                    str_data3 = "red"
                elif(color == "verde"):
                    str_data3 = "#008700"
                elif(color == "ambar"):
                    str_data3 = "#EDB200"
                else:
                    str_data3 = "blue"

                print(str_data)
                print(str_data2)
                print(str_data3)
            else:
                print("fin")
                break
        connection.close()

try:
    win = ventana()
    _thread.start_new_thread( conexion_serial, () )
    _thread.start_new_thread( win.inicio(), () )
    
except:
    print(sys.exc_info())
    print ("Error: unable to start thread")

while 1:
    pass


