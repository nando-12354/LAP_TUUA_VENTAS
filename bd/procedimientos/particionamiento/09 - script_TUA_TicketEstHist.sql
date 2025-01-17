USE [BD_TUUA_PRD]
GO
BEGIN TRANSACTION
CREATE PARTITION FUNCTION [PF_TUA_TicketEstHist](char(16)) AS RANGE LEFT FOR VALUES (N'9000600304142320', N'9000600308645450')

CREATE PARTITION SCHEME [PS_TUA_TicketEstHist] AS PARTITION [PF_TUA_TicketEstHist] TO ([BD_TUUA_PRD_2010], [BD_TUUA_PRD_2011], [PRIMARY])

ALTER TABLE [dbo].[TUA_TicketEstHist] DROP CONSTRAINT [PK_TUA_TicketEstHist]

SET ANSI_PADDING ON

ALTER TABLE [dbo].[TUA_TicketEstHist] ADD  CONSTRAINT [PK_TUA_TicketEstHist] PRIMARY KEY CLUSTERED 
(
	[Cod_Numero_Ticket] ASC,
	[Num_Secuencial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PS_TUA_TicketEstHist]([Cod_Numero_Ticket])

COMMIT TRANSACTION



