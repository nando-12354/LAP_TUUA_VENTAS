
/****** Object:  Table [dbo].[TUA_GrupoPuerta]    Script Date: 10/6/2019 10:13:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TUA_GrupoPuerta](
	[cod_grupo] [varchar](10) NOT NULL,
	[dsc_descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_TUA_Grupo_Gate] PRIMARY KEY CLUSTERED 
(
	[cod_grupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[TUA_Puerta_Grupo](
	[cod_grupo] [varchar](10) NOT NULL,
	[num_puerta] [varchar](10) NOT NULL,
 CONSTRAINT [PK_TUA_Puerta_Grupo] PRIMARY KEY CLUSTERED 
(
	[cod_grupo] ASC,
	[num_puerta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TUA_Puerta_Grupo]  WITH CHECK ADD  CONSTRAINT [FK_TUA_Puerta_Grupo_TUA_GrupoPuerta] FOREIGN KEY([cod_grupo])
REFERENCES [dbo].[TUA_GrupoPuerta] ([cod_grupo])
GO

ALTER TABLE [dbo].[TUA_Puerta_Grupo] CHECK CONSTRAINT [FK_TUA_Puerta_Grupo_TUA_GrupoPuerta]
GO


ALTER TABLE [dbo].[TUA_Molinete] ADD  [cod_grupo] [varchar](10)
GO

ALTER TABLE [dbo].[TUA_Molinete]  WITH CHECK ADD  CONSTRAINT [FK_TUA_Molinete_TUA_GrupoPuerta] FOREIGN KEY([cod_grupo])
REFERENCES [dbo].[TUA_GrupoPuerta] ([cod_grupo])
GO

ALTER TABLE [dbo].[TUA_Molinete] CHECK CONSTRAINT [FK_TUA_Molinete_TUA_GrupoPuerta]


ALTER TABLE [dbo].[TUA_VueloProgramado] ADD [Num_Puerta] [varchar](10)

GO