USE BD_TUUA_PRD
GO
IF OBJECT_ID ( 'ufn_get_exist_partition', 'FN' ) IS NOT NULL   
    DROP PROCEDURE ufn_get_exist_partition;  
GO
/*
Creado por: Victor Arrunategui Panta
Creado el : 26/12/2016
Modificado: 18/01/2017
Historial :
			Version Inicial: Se crea el stored procedure para la creación automática de los DataFiles, FilesGroups 
			y asignación de los mismos en los PartitionFunction y PartitionScheme	 
*/
CREATE FUNCTION ufn_get_exist_partition 
	(@dbname varchar (11), @table varchar (25), @year_part char(4))
RETURNS TABLE  
AS  
RETURN
(
	SELECT i.name
	FROM sys.partitions p JOIN sys.indexes i 
	ON  P.OBJECT_ID=I.object_id and  p.index_id = i.index_id
	JOIN sys.partition_schemes ps
	ON ps.data_space_id = i.data_space_id
	JOIN sys.partition_functions f
	ON f.function_id =ps.function_id
	LEFT JOIN sys.partition_range_values rv
	ON f.function_id = rv.function_id
	AND p.partition_number =rv.boundary_id
	JOIN sys.destination_data_spaces dds
	ON dds.partition_scheme_id = ps.data_space_id
	AND dds.destination_id = p.partition_number
	JOIN sys.filegroups fg
	ON dds.data_space_id = fg.data_space_id
	JOIN sysfiles fs
	ON fg.data_space_id= fs.groupid
	where i.index_id <2 and OBJECT_NAME(i.object_id) =@table and fg.name=@dbname+'_'+@year_part
)
GO 


