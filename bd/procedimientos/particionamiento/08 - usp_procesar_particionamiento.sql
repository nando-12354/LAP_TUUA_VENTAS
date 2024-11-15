USE BD_TUUA_PRD
GO
IF OBJECT_ID ( 'usp_procesar_particionamiento', 'P' ) IS NOT NULL   
    DROP PROCEDURE usp_procesar_particionamiento;  
GO 
CREATE PROCEDURE usp_procesar_particionamiento --@year='2010'
	@year char(4)
/*
Creado por: Victor Arrunategui Panta
Creado el : 26/12/2016
Modificado: 18/01/2017
Historial :
			Version Inicial: Se crea el stored procedure para la creación automática de los DataFiles, FilesGroups 
			y asignación de los mismos en los PartitionFunction y PartitionScheme	 
*/
AS
DECLARE @time_ini datetime
DECLARE @time_fin datetime
DECLARE @time char (8)
BEGIN
--/*Creacion de File Groups y DataFiles */
--PRINT N'==> FileGroup y DataFiles del Periodo: '+@year;
--PRINT 'Hora de Inicio : ' + convert (varchar(30), getdate(), 8)
--SET @time_ini= getdate()
--	EXEC usp_create_files_partitions @year_part=@year
--SET @time_fin= getdate()
--SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
--RAISERROR ('Duración por la generacion de FileGroups y DataFiles: %s', 0, 1, @time) 

/*Particionamiento de la tabla TUA_BoardingBcbp */
PRINT N'==> Particionamiento de la tabla TUA_BoardingBcbp para el periodo: '+@year;
PRINT 'Hora de Inicio : ' + convert (varchar(30), getdate(), 8)
SET @time_ini= getdate()
	EXEC usp_partition_table_TUA_BoardingBcbp @year_part=@year	
SET @time_fin= getdate()
SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
RAISERROR ('Duración por el proceso de particionamiento: %s', 0, 1, @time) 

/*Particionamiento de la tabla TUA_BoardingBcbpErr */
PRINT N'==> Particionamiento de la tabla TUA_BoardingBcbpErr para el periodo: '+@year;
PRINT 'Hora de Inicio : ' + convert (varchar(30), getdate(), 8)
SET @time_ini= getdate()
	EXEC usp_partition_table_TUA_BoardingBcbpErr @year_part=@year	
SET @time_fin= getdate()
SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
RAISERROR ('Duración por el proceso de particionamiento: %s', 0, 1, @time) 

/*Particionamiento de la tabla TUA_BoardingBcbpEstHist */
PRINT N'==> Particionamiento de la tabla TUA_BoardingBcbpEstHist para el periodo: '+@year;
PRINT 'Hora de Inicio : ' + convert (varchar(30), getdate(), 8)
SET @time_ini= getdate()
	EXEC usp_partition_table_TUA_BoardingBcbpEstHist @year_part=@year	
SET @time_fin= getdate()
SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
RAISERROR ('Duración por el proceso de particionamiento: %s', 0, 1, @time) 

/*Particionamiento de la tabla TUA_Ticket */
PRINT N'==> Particionamiento de la tabla TUA_Ticket para el periodo: '+@year;
PRINT 'Hora de Inicio : ' + convert (varchar(30), getdate(), 8)
SET @time_ini= getdate()
	EXEC usp_partition_table_TUA_Ticket @year_part=@year	
SET @time_fin= getdate()
SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
RAISERROR ('Duración por el proceso de particionamiento: %s', 0, 1, @time) 

/*Particionamiento de la tabla TUA_TicketEstHist */
PRINT N'==> Particionamiento de la tabla TUA_TicketEstHist para el periodo: '+@year;
PRINT 'Hora de Inicio : ' + convert (varchar(30), getdate(), 8)
SET @time_ini= getdate()
	EXEC usp_partition_table_TUA_TicketEstHist @year_part=@year	
SET @time_fin= getdate()
SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
RAISERROR ('Duración por el proceso de particionamiento: %s', 0, 1, @time) 

END 