USE BD_TUUA_PRD
GO
IF OBJECT_ID ( 'usp_process_archiving', 'P' ) IS NOT NULL   
    DROP PROCEDURE usp_process_archiving;  
GO 
CREATE PROCEDURE usp_process_archiving 
	@periodoanio int
AS

/*
PROYECTO: Archiving BD TUUA
CREADO POR: Victor Arrunategui (GMD)
MODIFICADO: 16/01/2017
MOTIVO: El siguiente procedimiento procede con archivar la información, según el año seleccionado.
*/

DECLARE @periodomm int
--DECLARE @periodoanio int
DECLARE @periodo char(6)
DECLARE @dbname char (11)
DECLARE @tsql varchar (MAX)
DECLARE @tsqllog varchar (300)
DECLARE @cnt_rows int
DECLARE @time datetime
DECLARE @time2 datetime
DECLARE @duration char(8)

SET @dbname='BD_TUUA_PRD'
--SET @periodoanio = year(getdate())-4
SET @periodomm=01

SET @periodo= CONVERT(VARCHAR(4),@periodoanio) + RIGHT('00'+CONVERT(varchar(2),@periodomm),2)
SET @tsql=''

CREATE TABLE #Tbl_Archiving
(Tabla varchar (50), periodo char(6), cn_row int)

EXEC USP_CREATEDB @periodoanio

SET @tsqllog='
use master 
alter database '+@dbname+' set recovery simple
use '+@dbname+'
dbcc shrinkfile (BD_TUUA_PRD_log) WITH NO_INFOMSGS
dbcc shrinkdatabase ('+@dbname+') WITH NO_INFOMSGS
use master 
alter database '+@dbname+' set recovery full'

/*Se procede con el proceso de archivamiento masivo*/
WHILE @periodomm <=12
BEGIN
	SET @periodo= CONVERT(VARCHAR(4),@periodoanio) + RIGHT('00'+CONVERT(varchar(2),@periodomm),2)
	SET @time = GETDATE()
	SET @tsql=@tsql+'
		USE [BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+']
		
		SET NOCOUNT ON
		
		PRINT ''*** Archivando registros de la tabla [TUA_BoardingBcbp] ***''
		
		PRINT ''- Periodo: '+@periodo+'''
		PRINT ''- Hora inicio: ''+CONVERT(varchar(8), getdate(),8)

		INSERT  INTO ['+@dbname+'].[dbo].[tbl_archiving_history] (fe_eje,bd_des,tb_des,period,hr_ini)
		VALUES (GETDATE(),''BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+''',''TUA_BoardingBcbp'','''+@periodo+''',CONVERT(varchar(8),getdate(),8))
		
		INSERT INTO #Tbl_Archiving 
		SELECT ''TUA_BoardingBcbp'','''+@periodo+''', count(*) FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbp] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('+@periodo+')	

		INSERT INTO TUA_BoardingBcbp
		SELECT * FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbp] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('''+@periodo+''')	

		DELETE FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbp] 
		WHERE LEFT(Log_Fecha_Mod,6) = ('''+@periodo+''')
		
		PRINT ''- Hora Fin: ''+CONVERT(VARCHAR(8),GETDATE(),8)
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] SET hr_fin = CONVERT(varchar(8), GETDATE(),8)  WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_BoardingBcbp''

		DBCC SHRINKFILE (BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+'_LOG) WITH NO_INFOMSGS
		DBCC SHRINKDATABASE (BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+') WITH NO_INFOMSGS
		
		--PRINT ''- Duración: ''+CONVERT (varchar(30), getdate()-'''+CONVERT(varchar(8), @time,8)+''', 8)

		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] 
		SET ti_dur = 
				RIGHT(''0'' + cast(datediff(hour, hr_ini, hr_fin) as varchar),2) + '':'' + 
				RIGHT(''0'' + cast(datediff(minute, hr_ini, hr_fin) % 60 as varchar), 2)+'':''+
				RIGHT(''0'' + cast(datediff(SECOND, hr_ini, hr_fin) % 60 as varchar), 2)  
		WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_BoardingBcbp''
				
		PRINT ''***********************************************************''
		
		PRINT ''*** Archivando registros de la tabla [TUA_BoardingBcbpErr] ***''
		
		PRINT ''- Periodo: '+@periodo+'''
		PRINT ''- Hora inicio: '+CONVERT(varchar(30), @time)+'''
		
		INSERT INTO TUA_BoardingBcbpErr
		SELECT * FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbpErr] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('''+@periodo+''')	
				
		INSERT INTO ['+@dbname+'].[dbo].[tbl_archiving_history] (fe_eje,bd_des,tb_des,period,hr_ini)
		VALUES (GETDATE(),''BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+''',''TUA_BoardingBcbpErr'','''+@periodo+''',CONVERT(varchar(8),getdate(),8))

		INSERT INTO #Tbl_Archiving 
		SELECT ''TUA_BoardingBcbpErr'','''+@periodo+''', count(*) FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbpErr] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('+@periodo+')	
																
		DELETE FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbpErr] 
		WHERE LEFT(Log_Fecha_Mod,6) = ('''+@periodo+''')
		
		DBCC SHRINKFILE (BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+'_LOG) WITH NO_INFOMSGS
		DBCC SHRINKDATABASE (BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+') WITH NO_INFOMSGS

		PRINT ''- Hora Fin: ''+CONVERT(VARCHAR(30),GETDATE())
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] SET hr_fin = CONVERT(varchar(8), GETDATE(),8)  WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_BoardingBcbpErr''
		
		--PRINT ''- Duración: ''+CONVERT (varchar(30), getdate()-'''+CONVERT(varchar(8), @time,8)+''', 8)
		
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] 
		SET ti_dur = 
				RIGHT(''0'' + cast(datediff(hour, hr_ini, hr_fin) as varchar),2) + '':'' + 
				RIGHT(''0'' + cast(datediff(minute, hr_ini, hr_fin) % 60 as varchar), 2)+'':''+
				RIGHT(''0'' + cast(datediff(SECOND, hr_ini, hr_fin) % 60 as varchar), 2)  
		WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_BoardingBcbpErr''
				
		PRINT ''***********************************************************''
		
		PRINT ''*** Archivando registros de la tabla [TUA_BoardingBcbpEstHist] ***''
		
		PRINT ''- Periodo: '+@periodo+'''
		PRINT ''- Hora inicio: '+CONVERT(varchar(30), @time)+'''

		INSERT INTO TUA_BoardingBcbpEstHist
		SELECT * FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbpEstHist] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('''+@periodo+''')	

		INSERT INTO ['+@dbname+'].[dbo].[tbl_archiving_history] (fe_eje,bd_des,tb_des,period,hr_ini)
		VALUES (GETDATE(),''BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+''',''TUA_BoardingBcbpEstHist'','''+@periodo+''',CONVERT(varchar(8),getdate(),8))

		INSERT INTO #Tbl_Archiving 
		SELECT ''TUA_BoardingBcbpEstHist'','''+@periodo+''', count(*) FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbpEstHist] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('+@periodo+')	
																
		DELETE FROM ['+@dbname+'].[dbo].[TUA_BoardingBcbpEstHist] 
		WHERE LEFT(Log_Fecha_Mod,6) = ('''+@periodo+''')
	
		DBCC SHRINKFILE (BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+'_LOG) WITH NO_INFOMSGS	

		PRINT ''- Hora Fin: ''+CONVERT(VARCHAR(30),GETDATE())
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] SET hr_fin = CONVERT(varchar(8), GETDATE(),8)  WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_BoardingBcbpEstHist''
		
		--PRINT ''- Duración: ''+CONVERT (varchar(30), getdate()-'''+CONVERT(varchar(8), @time,8)+''', 8)
		
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] 
		SET ti_dur = 
				RIGHT(''0'' + cast(datediff(hour, hr_ini, hr_fin) as varchar),2) + '':'' + 
				RIGHT(''0'' + cast(datediff(minute, hr_ini, hr_fin) % 60 as varchar), 2)+'':''+
				RIGHT(''0'' + cast(datediff(SECOND, hr_ini, hr_fin) % 60 as varchar), 2)  
		WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_BoardingBcbpEstHist''
				
		PRINT ''***********************************************************''
		
		PRINT ''*** Archivando registros de la tabla [TUA_Ticket] ***''
		
		PRINT ''- Periodo: '+@periodo+'''
		PRINT ''- Hora inicio: '+CONVERT(varchar(30), @time)+'''

		INSERT INTO TUA_Ticket
		SELECT * FROM ['+@dbname+'].[dbo].[TUA_Ticket] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('''+@periodo+''')	

		INSERT INTO ['+@dbname+'].[dbo].[tbl_archiving_history] (fe_eje,bd_des,tb_des,period,hr_ini)
		VALUES (GETDATE(),''BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+''',''TUA_Ticket'','''+@periodo+''',CONVERT(varchar(8),getdate(),8))
		
		INSERT INTO #Tbl_Archiving 
		SELECT ''TUA_Ticket'','''+@periodo+''', count(*) FROM ['+@dbname+'].[dbo].[TUA_Ticket] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('+@periodo+')	
																		
		DELETE FROM ['+@dbname+'].[dbo].[TUA_Ticket] 
		WHERE LEFT(Log_Fecha_Mod,6) = ('''+@periodo+''')
		
		DBCC SHRINKFILE (BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+'_LOG) WITH NO_INFOMSGS

		PRINT ''- Hora Fin: ''+CONVERT(VARCHAR(30),GETDATE())
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] SET hr_fin = CONVERT(varchar(8), GETDATE(),8)  WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_Ticket''
		
		--PRINT ''- Duración: ''+CONVERT (varchar(30), getdate()-'''+CONVERT(varchar(8), @time,8)+''', 8)
		
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] 
		SET ti_dur = 
				RIGHT(''0'' + cast(datediff(hour, hr_ini, hr_fin) as varchar),2) + '':'' + 
				RIGHT(''0'' + cast(datediff(minute, hr_ini, hr_fin) % 60 as varchar), 2)+'':''+
				RIGHT(''0'' + cast(datediff(SECOND, hr_ini, hr_fin) % 60 as varchar), 2)  
		WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_Ticket''
		
		PRINT ''***********************************************************''
		
		PRINT ''*** Archivando registros de la tabla [TUA_TicketEstHist] ***''
		
		PRINT ''- Periodo: '+@periodo+'''
		PRINT ''- Hora inicio: '+CONVERT(varchar(30), @time)+'''

		INSERT INTO TUA_TicketEstHist
		SELECT * FROM ['+@dbname+'].[dbo].[TUA_TicketEstHist] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('''+@periodo+''')	

		INSERT INTO ['+@dbname+'].[dbo].[tbl_archiving_history] (fe_eje,bd_des,tb_des,period,hr_ini)
		VALUES (GETDATE(),''BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+''',''TUA_TicketEstHist'','''+@periodo+''',CONVERT(varchar(8),getdate(),8))
		
		INSERT INTO #Tbl_Archiving 
		SELECT ''TUA_TicketEstHist'','''+@periodo+''', count(*) FROM ['+@dbname+'].[dbo].[TUA_TicketEstHist] 
		WHERE LEFT (Log_Fecha_Mod,6) = ('+@periodo+')	
																		
		DELETE FROM ['+@dbname+'].[dbo].[TUA_TicketEstHist] 
		WHERE LEFT(Log_Fecha_Mod,6) = ('''+@periodo+''')
		
		DBCC SHRINKFILE (BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+'_LOG) WITH NO_INFOMSGS

		PRINT ''- Hora Fin: ''+CONVERT(VARCHAR(30),GETDATE())
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] SET hr_fin = CONVERT(varchar(8), GETDATE(),8)  WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_TicketEstHist''
		
		--PRINT ''- Duración: ''+CONVERT (varchar(30), getdate()-'''+CONVERT(varchar(8), @time,8)+''', 8)
		
		UPDATE ['+@dbname+'].[dbo].[tbl_archiving_history] 
		SET ti_dur = 
				RIGHT(''0'' + cast(datediff(hour, hr_ini, hr_fin) as varchar),2) + '':'' + 
				RIGHT(''0'' + cast(datediff(minute, hr_ini, hr_fin) % 60 as varchar), 2)+'':''+
				RIGHT(''0'' + cast(datediff(SECOND, hr_ini, hr_fin) % 60 as varchar), 2)  
		WHERE period  = ('''+@periodo+''') AND tb_des=''TUA_TicketEstHist''
		
		PRINT ''***********************************************************''

		DBCC SHRINKDATABASE (BD_TUUA_ARC_'+CONVERT(VARCHAR(4),@periodoanio)+') WITH NO_INFOMSGS
	'
	exec (@tsql)
	SET @periodomm=@periodomm+1
	SET @tsql=''
END

UPDATE [dbo].[tbl_archiving_history] 
SET cn_row= tt.cn_row
FROM [dbo].[tbl_archiving_history] ta
INNER JOIN #Tbl_Archiving tt ON tt.periodo=ta.period and tt.Tabla=ta.tb_des

exec (@tsqllog)
DROP TABLE #Tbl_Archiving