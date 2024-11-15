
/****** Object:  Table [dbo].[TUA_Temporal_BoardingPass]    Script Date: 18/07/2018 04:29:31 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TUA_Temporal_BoardingPass](
	[Num_Secuencial_Bcbp] [bigint] NOT NULL,
	[Cod_Numero_Bcbp] [varchar](16) NULL,
	[Cod_Compania] [char](10) NOT NULL,
	[Num_Vuelo] [varchar](10) NOT NULL,
	[Fch_Vuelo] [char](8) NOT NULL,
	[Num_Asiento] [varchar](10) NOT NULL,
	[Nom_Pasajero] [varchar](50) NOT NULL,
	[Dsc_Trama_Bcbp] [varchar](500) NULL,
	[Fch_Creacion] [char](8) NULL,
	[Hor_Creacion] [char](6) NULL,
	[Fch_Registro] [datetime] NOT NULL,
	[Num_Checkin] [varchar](5) NULL,
 CONSTRAINT [PK_TUA_Temporal_BoardingPass] PRIMARY KEY CLUSTERED 
(
	[Num_Secuencial_Bcbp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


