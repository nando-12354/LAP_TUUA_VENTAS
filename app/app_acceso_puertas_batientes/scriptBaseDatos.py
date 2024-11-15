#para crear la base de datos y las tablas a usar

import mysql.connector
import configuracionParametros

parametros = configuracionParametros.datosBD()
mydb =mysql.connector.connect(
	host =parametros[0],
	user = parametros[1],
	passwd = parametros[2],
	)


my_cursor = mydb.cursor()

my_cursor.execute( "DROP DATABASE " + str(parametros[3]))

my_cursor.execute("CREATE DATABASE "+ str(parametros[3]))

my_cursor.execute("SHOW DATABASES")

mydb.close()

mydb =mysql.connector.connect(
	host =parametros[0],
	user = parametros[1],
	passwd = parametros[2],
	database = parametros[3],
	)

my_cursor = mydb.cursor()

my_cursor.execute("""CREATE TABLE LAP_SP_informacion_de_ticket
(
	flg_boarding_pass varchar(5),
	dsc_nombrePAX varchar(45),
	flg_ticket varchar(10),
	cod_reserva varchar(10),
	dsc_aero_origen varchar(10),
	dsc_aero_destino varchar(10),
	dsc_aerolinea varchar(10),
	num_vuelo varchar(10),
	fch_vuelo date,
	dsc_clase varchar(10),
	num_asiento varchar(10),
	dsc_checkin varchar(10),
	std_pasajero varchar(10),
	std_subida varchar(10),
	num_intento int(5),#
	dsc_intento varchar(55),#
	fch_hora_intento datetime,#
	num_pase int(11),#
	txt_trama varchar(200)#
	) ENGINE = MYISAM
	""")

my_cursor.execute("""CREATE TABLE LAP_SP_llaves_de_destrabe
(
	cod_usuario varchar(10),
	cod_destrabe varchar(10),
	cod_molinete varchar(10),
	num int(10)
	) ENGINE = MYISAM
	""")

my_cursor.execute("""CREATE TABLE LAP_SP_conteo_de_personas
(
	dsc_puerta varchar(20),
	num_persona int(10),
	fch_creacion date
	) ENGINE = MYISAM
	""")

my_cursor.execute("""CREATE TABLE LAP_SP_estado_de_puerta
(

	Cod_Molinete varchar(20),
	Dsc_Ip varchar(20),
	Dsc_Molinete varchar(30),
	Tip_Documento varchar(10),
	Tip_Vuelo varchar(10),
	Tip_Acceso varchar(10),
	Tip_Estado varchar(10),
	Log_Usuario_Mod varchar(20),
	Log_Fecha_Mod varchar(20),
	Log_Hora_Mod varchar(20),
	Fch_Sincroniza varchar(30),
	Est_Master varchar(10),
	Dsc_DBName varchar(30),
	Dsc_DBUser varchar(30),
	Dsc_DBPassword varchar(30),
	Tip_Molinete varchar(30),
	Flg_Sincroniza varchar(10),
	cod_grupo varchar(10),
	dsc_estado varchar(20),
	fch_cambioDB datetime 

	) ENGINE = MYISAM
	""")

my_cursor.execute("""CREATE TABLE LAP_SP_enviar_pases
(
	dsc_puerta varchar(20),
	dsc_estado_pase varchar(20),
	fch_pase datetime,
	fch_unix float(25)
	) ENGINE = MYISAM
	""")

mydb.commit()
mydb.close()
########################################################33

