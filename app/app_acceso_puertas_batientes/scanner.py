import generico
import sys
import configuracionParametros
import logging
import time
import serial

def trama_recivida():
	try:
	
		try:
			s = serial.Serial(port=configuracionParametros.puertoscanner(),baudrate=115200)
			s2 = serial.Serial(port=configuracionParametros.luzscanner(),baudrate=115200)
			s.isOpen()
			s2.isOpen()
			#print("hola")
		except IOError :
			print("error")
			s.close()
			s2.close()
			s.open()
			s2.open()
		finally:
			#str_trama = input("pon el mensaje que quieras: ")
			s2.write(b'\x02RA;s=B\x03')
			str_t = s.read(200)
			str_trama = str_t.decode("utf-8")
			s2.write(b'\x02RA;s=F\x03')
			print(str_trama)
				
		
		try:
			print("ENTRO AL TRY 1 DEL SCANNER")
			prueba=int(str_trama[44:47]) ##############
			prueba2 = str_trama[57]
			devol=[]
			devol.append("boardingPass")
			devol.append(str_trama)

		except:
			print("ENTRO AL EXCEPT 1 DEL SCANNER")


			try:
				print("ENTRO AL TRY 2 DEL SCANNER")
				print(type(len(str_trama)))
				print(configuracionParametros.longitudTuua())
				print(type(configuracionParametros.longitudTuua()))
				prueba3 = int(str_trama)
				if (len(str_trama)==configuracionParametros.longitudTuua()):
					print("PASO LA VERIFICACION DEL IF DEL TRY 2")
					devol=[]
					devol.append("tuua")
					devol.append(str_trama)
				else:
					print("NOOOOOOO PASO LA VERIFICACION DEL IF DEL TRY 2")
					devol=[]
					devol.append("error")
					devol.append(str_trama)
			except:
				print("ENTRO AL EXCEPT 2 DEL SCANNER")
				devol=[]
				devol.append("error")
				devol.append(str_trama)

	except:
		print("ENTRO AL EXCEPT 3 DEL SCANNER")
		devol=[]
		devol.append("error")
		devol.append("")
		logging.error(str(generico.diahora()) + " Error en scanner en linea :     " + str(sys.exc_info()[2].tb_lineno)  + "      Error mayor"+str(sys.exc_info()[0]),exc_info=True)
	finally:
		print("ENTRO AL FINALLY DEL SCANNER")
		return devol ##############

	#print(str_trama)