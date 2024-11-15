
if not exists (select * from sysobjects where name='TUA_Transito' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[TUA_Transito](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Cod_Compania_Origen] [char](10) NULL,
		[Num_Vuelo_Origen] [varchar](10) NULL,
		[Fch_Vuelo_Origen] [date] NULL,
		[Hor_Vuelo_Origen] [datetime] NULL,
		[Tip_Vuelo_Origen] [char](1) NULL,
		[Tip_Operacion_Origen] [char](1) NULL,
		[Cod_Origen] [varchar](6) NULL,
		[Dsc_Origen] [varchar](50) NULL,
		[Cod_Compania_Destino] [char](10) NULL,
		[Num_Vuelo_Destino] [varchar](10) NULL,
		[Fch_Vuelo_Destino] [date] NULL,
		[Hor_Vuelo_Destino] [datetime] NULL,
		[Tip_Vuelo_Destino] [char](1) NULL,
		[Tip_Operacion_Destino] [char](1) NULL,
		[Cod_Destino] [varchar](6) NULL,
		[Dsc_Destino] [varchar](50) NULL,
		[Trama_Origen] [varchar](500) NULL,
		[Trama_Destino] [varchar](500) NULL,
		[Log_Fch_Registro] [datetime] NULL,
		[Log_Fch_Mod] [datetime] NULL,
		[Cod_Molinete] [varchar](4) NULL,
	 CONSTRAINT [PK_TUA_Transito] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END


