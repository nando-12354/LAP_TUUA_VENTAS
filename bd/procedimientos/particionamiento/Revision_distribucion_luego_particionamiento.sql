USE BD_TUUA_PRD
GO

SELECT OBJECT_NAME(i.object_id) AS OBJECT_NAME,
--p.partition_number, 
fg.NAME as FILEGROUP_NAME, 
fs.filename,
ROWS, --au.total_pages,
--CASE boundary_value_on_right 
--WHEN 1 THEN 'Less Than'
--ELSE 'Less or equal than' END AS 'Comparition', 
ISNULL(VALUE, convert(date,getdate())) as 'Less or equal than'
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
--JOIN (select container_id, sum (total_pages) as total_pages
--from sys.allocation_units
--group by container_id ) as au
--ON au.container_id = p.partition_id
where i.index_id <2   
order by 1





--SELECT * FROM SYS.Partition_schemes
--SELECT * FROM SYS.Partition_functions
--SELECT * FROM SYS.filegroups