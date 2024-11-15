USE BD_TUUA_PRD
GO
CREATE TABLE tbl_archiving_history(
id_his int identity,
fe_eje date,
bd_des char (50),
tb_des char (50),
period char (6),
cn_row int default (0),
hr_ini time,
hr_fin time,
ti_dur time 
)
go
