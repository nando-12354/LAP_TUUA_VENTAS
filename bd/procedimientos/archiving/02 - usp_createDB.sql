
USE BD_TUUA_PRD
GO
IF OBJECT_ID ( 'USP_CREATEDB', 'P' ) IS NOT NULL   
    DROP PROCEDURE USP_CREATEDB;  
GO 
CREATE PROCEDURE USP_CREATEDB 
	@yearcont int
AS

/*
PROYECTO: Archiving BD TUUA
CREADO POR: Victor Arrunategui (GMD)
MODIFICADO: 16/01/2017
MOTIVO: EL siguiente procedimiento crea una BD nueva segun el formato de la BD de PRD TUUA, según el año seleccionado.
*/

SET NOCOUNT ON
DECLARE @DBNAME varchar (50)
--DECLARE @yearcont int
DECLARE @tsql varchar (max)

SET @DBNAME='BD_TUUA_ARC'
--SET @yearcont=YEAR(GETDATE())-4

SET @DBNAME=UPPER(@DBNAME+'_'+CONVERT(char(4),@yearcont))

--SET @tsql='
--USE MASTER

--CREATE DATABASE ['+@DBNAME+']

-- ON  PRIMARY 
--( NAME = '''+@DBNAME+''', FILENAME = ''X:\DATA\'+@DBNAME+'.mdf'' ,MAXSIZE = UNLIMITED, FILEGROWTH = 512MB)
-- LOG ON 
--( NAME = '''+@DBNAME+'_LOG'', FILENAME = ''Y:\LOG\'+@DBNAME+'_log.ldf'' , FILEGROWTH = 512MB)
--'

SET @tsql='
USE MASTER

CREATE DATABASE ['+@DBNAME+']

 ON  PRIMARY 
( NAME = '''+@DBNAME+''', FILENAME = ''X:\DATA\'+@DBNAME+'.mdf'' ,MAXSIZE = UNLIMITED, FILEGROWTH = 512MB)
 LOG ON 
( NAME = '''+@DBNAME+'_LOG'', FILENAME = ''Y:\LOG\'+@DBNAME+'_log.ldf'' , FILEGROWTH = 512MB)
'


IF NOT EXISTS (SELECT name from sys.databases where name =@DBNAME)
BEGIN

	EXEC (@tsql)
	PRINT '*** Se creo satisfactoriamente la BD: '+@DBNAME+'.' 

END
ELSE 
	BEGIN PRINT '*** Ya existe la BD: '+@DBNAME+', favor de revisar.' END

SET @TSQL='
USE ['+@DBNAME+']
IF NOT EXISTS (SELECT name from sys.objects where name =''TUA_BoardingBcbp'')
BEGIN
CREATE TABLE [dbo].[TUA_BoardingBcbp](
	[Num_Secuencial_Bcbp] [bigint] NOT NULL,
	[Cod_Compania] [char](10) NOT NULL,
	[Num_Vuelo] [varchar](10) NOT NULL,
	[Fch_Vuelo] [char](8) NOT NULL,
	[Num_Asiento] [varchar](10) NOT NULL,
	[Nom_Pasajero] [varchar](50) NOT NULL,
	[Dsc_Trama_Bcbp] [varchar](500) NULL,
	[Log_Usuario_Mod] [char](7) NOT NULL,
	[Log_Fecha_Mod] [char](8) NOT NULL,
	[Log_Hora_Mod] [char](6) NOT NULL,
	[Tip_Ingreso] [char](1) NULL,
	[Tip_Estado] [char](1) NOT NULL,
	[Num_Rehabilitaciones] [int] NOT NULL,
	[Cod_Unico_Bcbp] [varchar](20) NULL,
	[Fch_Vencimiento] [datetime] NULL,
	[Fch_Creacion] [char](8) NULL,
	[Hor_Creacion] [char](6) NULL,
	[Cod_Unico_Bcbp_Rel] [varchar](20) NULL,
	[Flg_Sincroniza] [char](1) NOT NULL,
	[Tip_Pasajero] [char](1) NULL,
	[Tip_Vuelo] [char](1) NULL,
	[Tip_Trasbordo] [char](1) NOT NULL,
	[Tip_Anulacion] [char](1) NULL,
	[Cod_Numero_Bcbp] [char](16) NULL,
	[Num_Serie] [char](3) NULL,
	[Num_Secuencial] [char](9) NULL,
	[Flg_Tipo_Bcbp] [char](1) NULL,
	[Num_Secuencial_Bcbp_Rel] [bigint] NOT NULL,
	[Num_Secuencial_Bcbp_Rel_Sec] [bigint] NOT NULL,
	[Nro_Boarding] [char](5) NULL,
	[Dsc_Destino] [char](3) NULL,
	[Cod_Eticket] [varchar](20) NULL,
	[Cod_Moneda] [char](3) NULL,
	[Imp_Precio] [numeric](12, 2) NULL,
	[Imp_Tasa_Compra] [numeric](8, 4) NULL,
	[Imp_Tasa_Venta] [numeric](8, 4) NULL,
	[Num_Proceso_Rehab] [varchar](20) NULL,
	[Flg_Bloqueado] [char](1) NOT NULL,
	[Flg_WSError] [char](1) NULL,
	[Flg_Incluye_Tuua] [char](1) NULL,
	[Fch_Rehabilitacion] [char](14) NULL,
	[Num_Airline_Code] [char](3) NULL,
	[Num_Document_Form] [char](10) NULL,
	[Num_Checkin] [varchar](5) NULL,
 CONSTRAINT [PK_TUA_BoardingBcbp] PRIMARY KEY CLUSTERED 
(
	[Num_Secuencial_Bcbp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
PRINT ''- Se creo la tabla BoardingBcbp'' END
ELSE BEGIN PRINT ''Ya existe la tabla BoardingBcbp '' END

IF NOT EXISTS (SELECT name from sys.objects where name =''TUA_BoardingBcbpErr'')
BEGIN
CREATE TABLE [dbo].[TUA_BoardingBcbpErr](
	[Num_Secuencial] [bigint] NOT NULL,
	[Dsc_Trama_Bcbp] [varchar](200) NOT NULL,
	[Tip_Error] [varchar](2) NULL,
	[Log_Usuario_Mod] [char](7) NULL,
	[Log_Fecha_Mod] [char](8) NULL,
	[Log_Hora_Mod] [char](6) NULL,
	[Tip_Ingreso] [char](1) NULL,
	[Cod_Equipo_Mod] [char](3) NULL,
	[Tip_Boarding] [char](2) NULL,
	[Cod_Compania] [char](10) NULL,
	[Num_Vuelo] [varchar](10) NULL,
	[Fch_Vuelo] [char](8) NULL,
	[Num_Asiento] [varchar](10) NULL,
	[Nom_Pasajero] [varchar](50) NULL,
	[Log_Error] [nvarchar](max) NULL,
 CONSTRAINT [PK_BoardingBcbpError] PRIMARY KEY NONCLUSTERED 
(
	[Num_Secuencial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
PRINT ''- Se creo la tabla BoardingBcbpError'' END
ELSE BEGIN PRINT ''Ya existe la tabla BoardingBcbpError '' END

IF NOT EXISTS (SELECT name from sys.objects where name =''TUA_BoardingBcbpEstHist'')
BEGIN
CREATE TABLE [dbo].[TUA_BoardingBcbpEstHist](
	[Num_Secuencial_Bcbp] [bigint] NOT NULL,
	[Num_Secuencial] [bigint] NOT NULL,
	[Cod_Compania] [char](10) NOT NULL,
	[Num_Vuelo] [varchar](10) NOT NULL,
	[Fch_Vuelo] [char](8) NOT NULL,
	[Num_Asiento] [varchar](10) NOT NULL,
	[Nom_Pasajero] [varchar](50) NOT NULL,
	[Tip_Estado] [char](1) NOT NULL,
	[Dsc_Boarding_Estado] [varchar](20) NOT NULL,
	[Cod_Equipo_Mod] [varchar](20) NULL,
	[Log_Usuario_Mod] [char](7) NULL,
	[Log_Fecha_Mod] [char](8) NULL,
	[Log_Hora_Mod] [char](6) NULL,
	[Causal_Rehabilitacion] [varchar](5) NULL,
	[Nro_Operacion] [varchar](7) NULL,
	[Dsc_Motivo] [varchar](255) NULL,
	[Num_Checkin] [varchar](5) NULL,
 CONSTRAINT [PK_TUA_BoardingBcbpEstHist_1] PRIMARY KEY CLUSTERED 
(
	[Num_Secuencial_Bcbp] ASC,
	[Num_Secuencial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
PRINT ''- Se creo la tabla BoardingBcbpEstHist'' END
ELSE BEGIN PRINT ''Ya existe la tabla BoardingBcbpEstHist '' END

IF NOT EXISTS (SELECT name from sys.objects where name =''TUA_Ticket'')
BEGIN
CREATE TABLE [dbo].[TUA_Ticket](
	[Cod_Numero_Ticket] [char](16) NOT NULL,
	[Num_Serie] [char](3) NULL,
	[Num_Secuencial] [char](9) NULL,
	[Cod_Compania] [char](10) NULL,
	[Cod_Venta_Masiva] [char](5) NULL,
	[Dsc_Num_Vuelo] [char](10) NULL,
	[Fch_Vuelo] [char](8) NULL,
	[Tip_Estado_Actual] [char](1) NOT NULL,
	[Tip_Anulacion] [char](1) NULL,
	[Fch_Creacion] [char](8) NOT NULL,
	[Hor_Creacion] [char](6) NOT NULL,
	[Cod_Turno] [char](6) NULL,
	[Imp_Precio] [numeric](12, 2) NOT NULL,
	[Cod_Precio_Ticket] [char](10) NULL,
	[Cod_Moneda] [char](3) NULL,
	[Cod_Tasa_Cambio] [char](10) NULL,
	[Imp_Tasa_Cambio] [numeric](8, 4) NULL,
	[Cod_Tasa_Venta] [char](10) NULL,
	[Imp_Tasa_Venta] [numeric](8, 4) NULL,
	[Fch_Vencimiento] [char](8) NOT NULL,
	[Cod_Modalidad_Venta] [char](5) NULL,
	[Num_Rehabilitaciones] [int] NOT NULL,
	[Cod_Tipo_Ticket] [char](3) NOT NULL,
	[Num_Referencia] [char](10) NULL,
	[Flg_Contingencia] [char](1) NOT NULL,
	[Num_Extensiones] [int] NOT NULL,
	[Tip_Pago] [char](2) NULL,
	[Flg_Cobro] [char](1) NOT NULL,
	[Log_Usuario_Mod] [char](8) NULL,
	[Log_Fecha_Mod] [char](8) NOT NULL,
	[Log_Hora_Mod] [char](6) NOT NULL,
	[Flg_Sincroniza] [char](1) NULL,
	[Fch_Uso] [char](14) NULL,
	[Fch_Rehabilitacion] [char](14) NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY NONCLUSTERED 
(
	[Cod_Numero_Ticket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
PRINT ''- Se creo la tabla TUA_Ticket'' END
ELSE BEGIN PRINT ''Ya existe la tabla TUA_Ticket '' END

IF NOT EXISTS (SELECT name from sys.objects where name =''TUA_TicketEstHist'')
BEGIN
CREATE TABLE [dbo].[TUA_TicketEstHist](
	[Cod_Numero_Ticket] [char](16) NOT NULL,
	[Num_Secuencial] [int] NOT NULL,
	[Tip_Estado] [char](1) NOT NULL,
	[Dsc_Ticket_Estado] [varchar](20) NULL,
	[Cod_Equipo_Mod] [varchar](20) NULL,
	[Dsc_Num_Vuelo] [char](10) NULL,
	[Log_Usuario_Mod] [char](7) NULL,
	[Log_Fecha_Mod] [char](8) NULL,
	[Log_Hora_Mod] [char](6) NULL,
	[Causal_Rehabilitacion] [varchar](5) NULL,
	[Dsc_Motivo] [varchar](255) NULL,
	[Nro_Operacion] [varchar](7) NULL,
	[Cod_Tasa_Cambio] [char](10) NULL,
	[Imp_Tasa_Cambio] [decimal](8, 4) NULL,
	[Cod_Tasa_Venta] [char](10) NULL,
	[Imp_Tasa_Venta] [decimal](8, 4) NULL,
 CONSTRAINT [PK_TUA_TicketEstHist] PRIMARY KEY CLUSTERED 
(
	[Cod_Numero_Ticket] ASC,
	[Num_Secuencial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
PRINT ''- Se creo la tabla TUA_TicketEstHist'' END
ELSE BEGIN PRINT ''Ya existe la tabla TUA_TicketEstHist '' END
'
EXEC (@tsql)
PRINT '*** Se crearon satisfactoriamente las tablas sobre la BD: '+@DBNAME+', evidencia:'
SET @tsql='
USE ['+@DBNAME+']
SELECT @@SERVERNAME AS [Server], DB_NAME() as [DataBase], so.type_desc, sc.name [schema], so.name [object_name], so.create_date, so.modify_date
FROM SYS.objects so
INNER JOIN SYS.schemas sc ON so.schema_id=sc.schema_id
WHERE so.NAME IN (''TUA_BoardingBcbp'',''TUA_BoardingBcbpErr'',''TUA_BoardingBcbpEstHist'',''TUA_Ticket'',''TUA_TicketEstHist'')
'
EXEC (@tsql)

SET NOCOUNT OFF




