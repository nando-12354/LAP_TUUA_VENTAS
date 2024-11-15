USE BD_TUUA_PRD
GO
IF OBJECT_ID ( 'usp_partition_table_TUA_TicketEstHist', 'P' ) IS NOT NULL   
    DROP PROCEDURE usp_partition_table_TUA_TicketEstHist;  
GO 
CREATE PROCEDURE usp_partition_table_TUA_TicketEstHist  --@year_part='2012'
	@year_part char (4)
/*
Creado por: Victor Arrunategui Panta
Creado el : 26/12/2016
Modificado: 18/01/2017
Historial :
			Version Inicial: Se crea el stored procedure para la creaci�n autom�tica de los DataFiles, FilesGroups 
			y asignaci�n de los mismos en los PartitionFunction y PartitionScheme	 
*/
AS
BEGIN

DECLARE @TSQL VARCHAR (MAX)
DECLARE @boundary bigint
DECLARE @dbname CHAR (11)
SET @dbname='BD_TUUA_PRD'

SELECT @boundary= Max(Cod_Numero_Ticket) FROM TUA_TicketEstHist WHERE left(Log_Fecha_Mod,4)=@year_part

IF @boundary IS NULL
BEGIN 
	PRINT '** No existe informaci�n a particionar para el periodo seleccionado'
END
ELSE
BEGIN
IF EXISTS (SELECT * from  ufn_get_exist_partition (@dbname,'TUA_TicketEstHist',@year_part))
	BEGIN
		PRINT '** Ya se tiene actualizada la partici�n para el periodo: '+@year_part 
	END
	ELSE 
	BEGIN
		SET @TSQL ='
		USE '+@dbname+'
		ALTER PARTITION SCHEME [PS_TUA_TicketEstHist] NEXT USED '+@dbname+'_'+CONVERT(CHAR(4),@year_part)+'

		ALTER PARTITION FUNCTION PF_TUA_TicketEstHist() SPLIT RANGE ('''+CONVERT(VARCHAR(50),@boundary)+''')'

		EXEC (@TSQL)
		PRINT '** Se actualizo la partici�n de la tabla TUA_TicketEstHist para el periodo: '+@year_part
	END
END
END