/*
Creado por: Victor Arrunategui Panta
Creado el : 26/12/2016
Modificado: 18/01/2017
Historial :
			Version Inicial: Se crea el stored procedure para la creación automática de los DataFiles, FilesGroups 
			y asignación de los mismos en los PartitionFunction y PartitionScheme	 
*/
IF OBJECT_ID ( 'usp_create_files_partitions', 'P' ) IS NOT NULL   
    DROP PROCEDURE usp_create_files_partitions;  
GO
CREATE PROCEDURE usp_create_files_partitions 
	@year_part char (4)
AS
BEGIN

DECLARE @tsql varchar (max)
DECLARE @dbname varchar(11)
DECLARE @ruta varchar (500)

SET @dbname ='BD_TUUA_PRD'
SET @ruta ='X:\DATA\'

IF EXISTS (select Name from sysfiles where name in (@dbname+'_'+@year_part) )
	BEGIN 
		PRINT '** Ya existe un DataFile y un FileGroup para el periodo '+@year_part+'**' 
	END
ELSE 
	BEGIN

	SET @tsql ='
		ALTER DATABASE '+@dbname+'
		ADD FILEGROUP ['+@dbname+'_'+@year_part+']

		ALTER DATABASE '+@dbname+'
		ADD FILE
		(NAME = '''+@dbname+'_'+@year_part+''',
		FILENAME = '''+@ruta+@dbname+'_'+@year_part+'.ndf'',
		MAXSIZE = UNLIMITED)
		TO FILEGROUP ['+@dbname+'_'+@year_part+']'
	EXEC (@tsql)
	PRINT '** Se creo el DataFile y el Filegroup para el periodo seleccionado '+@year_part+'.'
END
END

GO

--exec usp_create_files_partitions 2010
--exec usp_create_files_partitions 2011
--exec usp_create_files_partitions 2012
--exec usp_create_files_partitions 2013
--exec usp_create_files_partitions 2014
--exec usp_create_files_partitions 2015
--exec usp_create_files_partitions 2016



