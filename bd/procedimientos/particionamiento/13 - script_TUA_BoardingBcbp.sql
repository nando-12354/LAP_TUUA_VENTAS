USE [BD_TUUA_PRD]
GO
BEGIN TRANSACTION
	CREATE PARTITION FUNCTION [PF_TUA_BoardingBcbp](bigint) AS RANGE LEFT FOR VALUES (N'3535926')

	CREATE PARTITION SCHEME [PS_TUA_BoardingBcbp] AS PARTITION [PF_TUA_BoardingBcbp] TO ([BD_TUUA_PRD_2011], [PRIMARY])
COMMIT TRANSACTION
PRINT '1'
GO
BEGIN TRANSACTION
	ALTER TABLE [dbo].[TUA_BoardingBcbp] DROP CONSTRAINT [PK_TUA_BoardingBcbp]

	ALTER TABLE [dbo].[TUA_BoardingBcbp] ADD  CONSTRAINT [PK_TUA_BoardingBcbp] PRIMARY KEY CLUSTERED 
	(
		[Num_Secuencial_Bcbp] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PS_TUA_BoardingBcbp]([Num_Secuencial_Bcbp])
COMMIT TRANSACTION
PRINT '2'
GO
BEGIN TRANSACTION
	CREATE NONCLUSTERED INDEX [IDX_UNICIDAD_BP] ON [dbo].[TUA_BoardingBcbp]
	(
		[Fch_Vuelo] ASC,
		[Cod_Compania] ASC,
		[Num_Vuelo] ASC,
		[Num_Asiento] ASC,
		[Nom_Pasajero] ASC,
		[Num_Checkin] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = ON, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PS_TUA_BoardingBcbp]([Num_Secuencial_Bcbp])
COMMIT TRANSACTION
PRINT '3'


--dbcc shrinkdatabase (BD_TUUA_QAS)