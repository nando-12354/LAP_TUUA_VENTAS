
/****** Object:  Table [dbo].[TUA_Temporal_Tickets]    Script Date: 18/07/2018 04:30:24 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TUA_Temporal_Tickets](
	[Cod_Numero_Ticket] [char](16) NOT NULL,
	[Num_Vuelo] [varchar](10) NULL,
	[Fch_Vuelo] [char](8) NULL,
	[Num_Serie] [char](3) NULL,
	[Fch_Creacion] [char](8) NOT NULL,
	[Hor_Creacion] [char](6) NOT NULL,
	[Fch_Registro] [datetime] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


