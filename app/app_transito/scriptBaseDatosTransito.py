#para crear la base de datos y las tablas a usar

import mysql.connector
import configuracionParametros
import logging
import datetime

fch_dia = datetime.datetime.today()
nombre_log = 'log/script_base_datos' + str(datetime.date(year=fch_dia.year,month=fch_dia.month,day=fch_dia.day)) +'.log'
logger = logging.getLogger(__name__)
logger.setLevel(logging.DEBUG)
file_handler = logging.FileHandler(nombre_log)
logger.addHandler(file_handler)

parametros = configuracionParametros.datosBD(logger)
mydb =mysql.connector.connect(
    host =parametros[0],
    user = parametros[1],
    passwd = parametros[2],
    )


my_cursor = mydb.cursor()
try :
    my_cursor.execute( "DROP DATABASE " + str(parametros[3]))
    my_cursor.execute("CREATE DATABASE "+ str(parametros[3]))
except:
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

my_cursor.execute("""CREATE TABLE LAP_Transito_Vuelos_llegadas
(
    Num_Vuelo varchar(40),
    Fch_Prog varchar(40),
    Hor_Prog varchar(40),
    Tip_Operacion varchar(40),
    Cod_Compania varchar(40),
    Tip_Vuelo varchar(40),
    Fch_Real varchar(40),
    Fch_Est varchar(40),
    Cod_Proc_Dest varchar(40),
    Cod_Escala varchar(40),
    Cod_Gate varchar(40),
    Tip_Gate_Terminal varchar(40),
    Cod_Faja varchar(40),
    Cod_Mostrador varchar(40),
    Dsc_Aerolinea varchar(40),
    Dsc_Proc_Destino varchar(40),
    Log_Fch_Creacion varchar(40),
    Log_Fch_Modificacion varchar(40),
    Dsc_Estado varchar(40),
    #Flg_Eliminado varchar(40),
    Cod_Iata varchar(40),
    Nro_Vuelo varchar(40),
    num int(10)

    ) ENGINE = MYISAM;

    ALTER TABLE lap_transito_vuelos_llegadas
    CHANGE COLUMN Num_Vuelo Num_Vuelo VARCHAR(40) NOT NULL ,
    CHANGE COLUMN Fch_Prog Fch_Prog VARCHAR(40) NOT NULL ,
    ADD PRIMARY KEY (Num_Vuelo, Fch_Prog);
    """)

my_cursor.execute("""CREATE TABLE LAP_Transito_Vuelos_salidas
(
    Num_Vuelo varchar(40),
    Fch_Prog varchar(40),
    Hor_Prog varchar(40),
    Tip_Operacion varchar(40),
    Cod_Compania varchar(40),
    Tip_Vuelo varchar(40),
    Fch_Real varchar(40),
    Fch_Est varchar(40),
    Cod_Proc_Dest varchar(40),
    Cod_Escala varchar(40),
    Cod_Gate varchar(40),
    Tip_Gate_Terminal varchar(40),
    Cod_Faja varchar(40),
    Cod_Mostrador varchar(40),
    Dsc_Aerolinea varchar(40),
    Dsc_Proc_Destino varchar(40),
    Log_Fch_Creacion varchar(40),
    Log_Fch_Modificacion varchar(40),
    Dsc_Estado varchar(40),
    #Flg_Eliminado varchar(40),
    Cod_Iata varchar(40),
    Nro_Vuelo varchar(40),
    num int(10)

    ) ENGINE = MYISAM;
    ALTER TABLE lap_transito_vuelos_salidas
    CHANGE COLUMN Num_Vuelo Num_Vuelo VARCHAR(40) NOT NULL ,
    CHANGE COLUMN Fch_Prog Fch_Prog VARCHAR(40) NOT NULL ,
    ADD PRIMARY KEY (Num_Vuelo, Fch_Prog);
    """)

my_cursor.execute("""CREATE TABLE LAP_llaves_de_destrabe
(
    cod_usuario varchar(10),
    cod_destrabe varchar(10),
    cod_molinete varchar(10),
    num int(10)
    ) ENGINE = MYISAM
    """)

my_cursor.execute("""CREATE TABLE LAP_Transito_Datos
(

    Num_Vuelo_Origen varchar(30),
    Fch_Vuelo_Origen varchar(30),
    Num_Vuelo_Destino varchar(30),
    Fch_Vuelo_Destino varchar(30),
    Cod_Molinete varchar(20),
    Trama_Origen varchar(100),
    Trama_Destino varchar(100),
    Std_subida varchar(30),
    fch_hora_intento datetime

    ) ENGINE = MYISAM;

    ALTER TABLE lap_transito_datos
    CHANGE COLUMN fch_hora_intento fch_hora_intento DATETIME NOT NULL ,
    ADD PRIMARY KEY (fch_hora_intento);

    """)


mydb.commit()
mydb.close()
########################################################33