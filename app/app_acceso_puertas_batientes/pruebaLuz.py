'''import serial
#   |
while(1):
	try:
		s = serial.Serial(port="COM",baudrate=115200)
		s2 = serial.Serial(port="COM",baudrate=115200)
		s.isOpen()
		s2.isOpen()

	except IOError :
		s.close()
		s.open()
		s2.close()
		s2.open()
	finally :
		s2.write(b'x02RA;s=Bx03') #☻RA;s=B♥
		str_t = s.read(200)
		str_trama = str_t.decode("utf-8")
		s2.write(b'x02RA;s=Fx03') #☻RA;s=F♥
		print(str_trama)

	#STX = ☻  0x02
	#ETX = ♥  0x03'''

import serial
import time
from controlador import controlador
import configuracionParametros

obj_controller = controlador(configuracionParametros.direccionControlador())
status_control = obj_controller.estado_actual()
try:
	s = serial.Serial(port="COM20",baudrate=115200,timeout=6)
	s2 = serial.Serial(port="COM19",baudrate=115200)
	s.isOpen()
	s2.isOpen()

except IOError :
	s.close()
	s2.close()
	s.open()
	s2.open()

finally:
	while (True):
		presencia = obj_controller.presencia_puerta()
		print("RESPUESTA DE PRESENCIA")
		print(presencia)
		if (presencia == "presencia"):
			s2.write(b'\x02RA;s=B\x03')
			str_t = s.read(200)
			str_trama = str_t.decode("utf-8")
			if (str_trama != ""):	
				print(str_trama)
				time.sleep(2)
				s2.write(b'\x02RA;s=F\x03')
				break

		else :
			s2.write(b'\x02RA;s=F\x03')


		#if (str_t != ""):	
		#	str_trama = str_t.decode("utf-8")
		#	s2.write(b'\x02RA;s=F\x03')
		#	break


