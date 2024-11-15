/****** Object:  Table [dbo].[TUA_BP_TRANSITO]    Script Date: 28/02/2018 03:39:40 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TUA_BP_TRANSITO](
	[num_vuelo] [varchar](10) NOT NULL,
	[fch_vuelo] [datetime] NOT NULL,
	[num_asiento] [varchar](10) NOT NULL,
	[num_checkin] [varchar](5) NOT NULL,
	[nom_pasajero] [varchar](50) NOT NULL,
	[dsc_trama_bcbp] [varchar](500) NULL,
	[dsc_destino] [varchar](3) NULL,
	[cod_iata] [char](2) NULL,
	[fch_registro] [datetime] NULL,
	[tip_vuelo] [char](1) NULL,
	[tip_status_pax] [char](1) NULL,
	[cod_molinete] [varchar](5) NULL,
	[dsc_archivo] [varchar](50) NULL,
	[log_usr_cre] [varchar](20) NULL,
	[log_usr_mod] [varchar](20) NULL,
	[log_fch_cre] [datetime] NULL,
	[log_fch_mod] [datetime] NULL,
 CONSTRAINT [PK_TUA_BP_TRANSITO] PRIMARY KEY CLUSTERED 
(
	[num_vuelo] ASC,
	[fch_vuelo] ASC,
	[num_asiento] ASC,
	[num_checkin] ASC,
	[nom_pasajero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


