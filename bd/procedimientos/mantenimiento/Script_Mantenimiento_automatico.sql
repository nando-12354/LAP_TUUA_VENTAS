
USE BD_TUUA_PRD
go
/*
Creado por: Victor Arrunategui Panta
Creado el : 26/12/2016
Modificado: 18/01/2017
Historial :
			Version Inicial: Se crea el store procedure para realizar mantenimiento de base de datos 	 
*/

CREATE PROCEDURE usp_maintenance_database 
AS
BEGIN
	DECLARE @tableindex table (
	nameschema varchar (30),
	nametable varchar(155), 
	nameindex varchar(max), 
	indexdpeth int, 
	porc_frag int, page_count int, 
	recomendacion char(15))
	DECLARE @dbid INT 
	SET @dbid = db_id()
	SET NOCOUNT ON

	/*Identificamos la recomendacion de los Indices por las tablas*/
	INSERT INTO @tableindex
	select  t4.name,
	t3.name ,
	t2.name ,
	t1.index_depth,
	convert (decimal,t1.avg_fragmentation_in_percent),
	t1.page_count,
	(CASE 
		   WHEN t1.avg_fragmentation_in_percent between 5 and 29 THEN 'REORGANIZE' 
		   WHEN t1.avg_fragmentation_in_percent >=30 THEN 'REBUILD' ELSE '-' END) AS RECOMENDACION
	from sys.dm_db_index_physical_stats(@dbid,NULL,NULL,NULL,'LIMITED' ) t1
						inner join sys.objects t3 on (t1.object_id = t3.object_id)
						inner join sys.schemas t4 on (t3.schema_id = t4.schema_id)
						inner join sys.indexes t2 on (t1.object_id = t2.object_id and  t1.index_id = t2.index_id )
						where index_type_desc <> 'HEAP' --and t3.name='tottrib'
						order by t4.name,t3.name,t2.name,partition_number

	--SELECT * FROM @tableindex 

	PRINT ' '
	PRINT '========================================================= '
	PRINT '== Inicio de proceso de mantenimiento de indices - REBUILD'
	PRINT '========================================================= '
	PRINT ' '
	/*Procedemos con las tablas cuyos indices necesitan REBUILD*/
	DECLARE @tableName varchar(155), @schemaName varchar(155), @tsql varchar(max)

	DECLARE @time char(8)
	DECLARE @time_ini datetime 
	DECLARE @time_fin datetime 
	DECLARE @SQL nvarchar(MAX)

	DECLARE cur_index_rebuild CURSOR FOR
		   SELECT distinct nameschema, nametable FROM @tableindex where recomendacion='REBUILD' or nametable in ('TUA_Ticket')
	OPEN cur_index_rebuild
	FETCH cur_index_rebuild INTO @schemaName, @tableName 
	WHILE @@FETCH_STATUS=0

	BEGIN 
	PRINT N'==> Tabla: '+@TableName;
	PRINT 'Hora de Inicio REBUILD Index: ' + convert (varchar(30), getdate(), 8)
	SET @time_ini= getdate()
	BEGIN
		   SET @TSQL=('ALTER INDEX ALL ON ['+@schemaName+'].['+@tableName+'] REBUILD WITH (FILLFACTOR = 80)')
		   EXEC (@tsql) 
		   --PRINT 'ALTER INDEX ALL ON ['+@schemaName+'].['+@tableName+'] REBUILD WITH (FILLFACTOR = 80)'
		   --PRINT 'UPDATE STATISTICS ['+@schemaName+'].['+@tableName+']  WITH FULLSCAN' 
	END;
	SET @time_fin= getdate()
	SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
	RAISERROR ('Duracion por la generacion de REBUILD Index: %s', 0, 1, @time) 
       
	FETCH cur_index_rebuild INTO  @schemaName, @tableName
	END
	CLOSE cur_index_rebuild
	DEALLOCATE cur_index_rebuild

	--Procedemos con las tablas cuyos indices necesitan REORGANIZE*/
	PRINT ' '
	PRINT '========================================================= '
	PRINT '== Inicio de proceso de mantenimiento de indices - REORGANIZE'
	PRINT '========================================================= '
	PRINT ' '

	DECLARE cur_index_reorganize CURSOR FOR

		   SELECT distinct nameschema, nametable FROM @tableindex where recomendacion='REORGANIZE' and nametable not in ('TUA_Ticket')
		   OPEN cur_index_reorganize
	FETCH cur_index_reorganize INTO @schemaName, @tableName 
	WHILE @@FETCH_STATUS=0

	BEGIN 
	PRINT N'==> Tabla: '+@TableName;
	PRINT 'Hora de Inicio REORGANIZE Index: ' + convert (varchar(30), getdate(), 8)
	SET @time_ini= getdate()
	BEGIN
		   SET @TSQL=('ALTER INDEX ALL ON ['+@schemaName+'].['+@tableName+'] REORGANIZE')
		   EXEC (@tsql) 

	END;
	SET @time_fin= getdate()
	SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
	RAISERROR ('Duracion por la generacion de REORGANIZE Index: %s', 0, 1, @time) 

	PRINT 'Hora de Inicio ESTADISTICA: ' + convert (varchar(30), getdate(), 8)
	SET @time_ini= getdate()
	BEGIN
		   SET @TSQL=('UPDATE STATISTICS ['+@schemaName+'].['+@tableName+']  WITH FULLSCAN')
		   EXEC (@tsql) 
	----   PRINT 'ALTER INDEX ALL ON ['+@schemaName+'].['+@tableName+'] REBUILD WITH (FILLFACTOR = 80)'
	----   PRINT 'UPDATE STATISTICS ['+@schemaName+'].['+@tableName+']  WITH FULLSCAN' 
	END;
	SET @time_fin= getdate()
	SET @time= convert (varchar(30), @time_fin-@time_ini, 8)
	RAISERROR ('Duracion por la generacion de Estadisticas: %s', 0, 1, @time) 
       
	FETCH cur_index_reorganize INTO  @schemaName, @tableName
	END
	CLOSE cur_index_reorganize
	DEALLOCATE cur_index_reorganize

	--PRINT ' '
	--PRINT '===================================================== '
	--PRINT '== Inicio de proceso de mantenimiento de estadisticas '
	--PRINT '===================================================== '
	--PRINT ' '

	--EXEC sp_updatestats


	SET NOCOUNT OFF
END