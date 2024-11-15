USE BD_TUUA_PRD 
GO
IF OBJECT_ID ( 'usp_partition_table_TUA_BoardingBcbpErr', 'P' ) IS NOT NULL   
    DROP PROCEDURE usp_partition_table_TUA_BoardingBcbpErr;  
GO 
CREATE PROCEDURE usp_partition_table_TUA_BoardingBcbpErr --@year_part='2011'
	@year_part char (4)
/*
Creado por: Victor Arrunategui Panta
Creado el : 26/12/2016
Modificado: 18/01/2017
Historial :
			Version Inicial: Se crea el stored procedure para la creación automática de los DataFiles, FilesGroups 
			y asignación de los mismos en los PartitionFunction y PartitionScheme	 
*/
AS
BEGIN

DECLARE @TSQL VARCHAR (MAX)
DECLARE @boundary int
DECLARE @dbname CHAR (11)
--DECLARE @count int
SET @dbname='BD_TUUA_PRD'

SELECT @boundary= Max(Num_Secuencial) FROM TUA_BoardingBcbpErr WHERE left(Log_Fecha_Mod,4)=@year_part

IF @boundary IS NULL
BEGIN 
	PRINT '** No existe información a particionar para el periodo seleccionado'
END
ELSE
BEGIN
IF EXISTS (SELECT * from  ufn_get_exist_partition (@dbname,'TUA_BoardingBcbpErr',@year_part))
	BEGIN
		PRINT '** Ya se tiene actualizada la partición para el periodo: '+@year_part 
	END
	ELSE 
	BEGIN
		SET @TSQL ='
		USE '+@dbname+'
		ALTER PARTITION SCHEME [PS_TUA_BoardingBcbpErr] NEXT USED '+@dbname+'_'+CONVERT(CHAR(4),@year_part)+'

		ALTER PARTITION FUNCTION PF_TUA_BoardingBcbpErr() SPLIT RANGE ('''+CONVERT(VARCHAR(15),@boundary)+''')'

		EXEC (@TSQL)
		PRINT '** Se actualizo la partición de la tabla TUA_BoardingBcbpErr para el periodo: '+@year_part
	END
END
END























