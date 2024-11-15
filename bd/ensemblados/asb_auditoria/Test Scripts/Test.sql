-- Examples for queries that exercise different SQL objects implemented by this assembly

-----------------------------------------------------------------------------------------
-- Stored procedure
-----------------------------------------------------------------------------------------
-- exec StoredProcedureName


-----------------------------------------------------------------------------------------
-- User defined function
-----------------------------------------------------------------------------------------
-- select dbo.FunctionName()


-----------------------------------------------------------------------------------------
-- User defined type
-----------------------------------------------------------------------------------------
-- CREATE TABLE test_table (col1 UserType)
-- go
--
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 1'))
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 2'))
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 3'))
--
-- select col1::method1() from test_table



-----------------------------------------------------------------------------------------
-- User defined type
-----------------------------------------------------------------------------------------
-- select dbo.AggregateName(Column1) from Table1


--select 'To run your project, please edit the Test.sql file in your project. This file is located in the Test Scripts folder in the Solution Explorer.'
--Exec usp_audit_tabla_sel 'Num_Secuencial_Bcbp©404','TUA_BoardingBcbp',null
--Exec usp_audit_tabla_ins 'm1','s1','U1','r1','2','TUA_BoardingBcbp','1','1'

--strModulo, strSubModulo, strUsuario, strRol, strTipOperacion, strTabla,strRegNuevo,strRegAnterior                               

--Exec usp_audit_tabla_ins_multiple 'M01', 'S01', 'U01','R01','1', 'TUA_ParamGeneralDetalle','Identificador©LN|Cod_Tipo_Operacion©CM|Cod_Moneda©DOL~Identificador©LN|Cod_Tipo_Operacion©CT|Cod_Moneda©DOL~Identificador©LN|Cod_Tipo_Operacion©EC|Cod_Moneda©DOL~Identificador©LN|Cod_Tipo_Operacion©IC|Cod_Moneda©DOL~Identificador©LN|Cod_Tipo_Operacion©IT|Cod_Moneda©DOL~Identificador©LN|Cod_Tipo_Operacion©MC|Cod_Moneda©DOL~Identificador©LN|Cod_Tipo_Operacion©VM|Cod_Moneda©DOL'
Exec usp_audit_tabla_del_multiple 'DM1', 'DS1', 'DU1','DR1','4','TUA_PerfilRol','Cod_Proceso©06050|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E0000|Cod_Modulo©001|Cod_Rol©R0001~Cod_Proceso©E0001|Cod_Modulo©001|Cod_Rol©R0001~Cod_Proceso©E0002|Cod_Modulo©001|Cod_Rol©R0001~Cod_Proceso©E0003|Cod_Modulo©001|Cod_Rol©R0001~Cod_Proceso©E0020|Cod_Modulo©001|Cod_Rol©R0001~Cod_Proceso©E0021|Cod_Modulo©001|Cod_Rol©R0001~Cod_Proceso©E0022|Cod_Modulo©001|Cod_Rol©R0001~Cod_Proceso©E0023|Cod_Modulo©001|Cod_Rol©R0001~Cod_Proceso©E00X1|Cod_Modulo©009|Cod_Rol©R0001~Cod_Proceso©E00X2|Cod_Modulo©009|Cod_Rol©R0001~Cod_Proceso©E0100|Cod_Modulo©002|Cod_Rol©R0001~Cod_Proceso©E0101|Cod_Modulo©002|Cod_Rol©R0001~Cod_Proceso©E0120|Cod_Modulo©002|Cod_Rol©R0001~Cod_Proceso©E0121|Cod_Modulo©002|Cod_Rol©R0001~Cod_Proceso©E0122|Cod_Modulo©002|Cod_Rol©R0001~Cod_Proceso©E0141|Cod_Modulo©002|Cod_Rol©R0001~Cod_Proceso©E0200|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0201|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0202|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0210|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0211|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0212|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0220|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0221|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0222|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0230|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0231|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0232|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0240|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0241|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0242|Cod_Modulo©003|Cod_Rol©R0001~Cod_Proceso©E0300|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0301|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0310|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0311|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0320|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0321|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0326|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0330|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0331|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0336|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0340|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0341|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0344|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0347|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0350|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0351|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0356|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0360|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0370|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0375|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0380|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0390|Cod_Modulo©004|Cod_Rol©R0001~Cod_Proceso©E0400|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0401|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0406|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0411|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0416|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0421|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0450|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0451|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0456|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0461|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E0466|Cod_Modulo©005|Cod_Rol©R0001~Cod_Proceso©E4000|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4010|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4020|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4030|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4040|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4050|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4060|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4080|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4090|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4100|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E4110|Cod_Modulo©006|Cod_Rol©R0001~Cod_Proceso©E5000|Cod_Modulo©008|Cod_Rol©R0001~Cod_Proceso©E5001|Cod_Modulo©008|Cod_Rol©R0001~Cod_Proceso©E5002|Cod_Modulo©008|Cod_Rol©R0001~Cod_Proceso©E5003|Cod_Modulo©008|Cod_Rol©R0001~Cod_Proceso©E5010|Cod_Modulo©008|Cod_Rol©R0001~Cod_Proceso©E5020|Cod_Modulo©008|Cod_Rol©R0001~Cod_Proceso©E6001|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6010|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6020|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6030|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6040|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6050|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6060|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6070|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6080|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6090|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6100|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6110|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6120|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6130|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E6140|Cod_Modulo©007|Cod_Rol©R0001~Cod_Proceso©E7000|Cod_Modulo©I20|Cod_Rol©R0001~Cod_Proceso©E7010|Cod_Modulo©I20|Cod_Rol©R0001~Cod_Proceso©E7020|Cod_Modulo©I20|Cod_Rol©R0001~Cod_Proceso©E7030|Cod_Modulo©I20|Cod_Rol©R0001~Cod_Proceso©E7040|Cod_Modulo©I20|Cod_Rol©R0001~Cod_Proceso©E8000|Cod_Modulo©I21|Cod_Rol©R0001~Cod_Proceso©E8010|Cod_Modulo©I21|Cod_Rol©R0001~Cod_Proceso©E8020|Cod_Modulo©I21|Cod_Rol©R0001'
