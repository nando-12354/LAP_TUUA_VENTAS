

if not exists (select * from sysobjects where name='TUA_Vuelo_Transito' and xtype='U')
BEGIN
CREATE TABLE [dbo].[TUA_Vuelo_Transito](
	[Num_Vuelo] [varchar](10) NOT NULL,
	[Fch_Prog] [date] NOT NULL,
	[Tip_Operacion] [char](1) NOT NULL,
	[Hor_Prog] [datetime] NULL,
	[Cod_Compania] [char](10) NULL,
	[Tip_Vuelo] [char](1) NULL,
	[Fch_Real] [datetime] NULL,
	[Fch_Est] [datetime] NULL,
	[Cod_Proc_Dest] [varchar](50) NULL,
	[Cod_Escala] [varchar](50) NULL,
	[Cod_Gate] [varchar](50) NULL,
	[Tip_Gate_Terminal] [char](1) NULL,
	[Cod_Faja] [varchar](50) NULL,
	[Cod_Mostrador] [varchar](50) NULL,
	[Dsc_Aerolinea] [varchar](50) NULL,
	[Dsc_Proc_Destino] [varchar](50) NULL,
	[Log_Fch_Creacion] [datetime] NULL,
	[Log_Fch_Modificacion] [datetime] NULL,
	[Dsc_Estado] [varchar](50) NULL,
	[Flg_Eliminado] [bit] NOT NULL,
	[Cod_Iata] [varchar](5) NULL,
	[Nro_Vuelo] [varchar](5) NULL,
 CONSTRAINT [PK_TUA_Vuelo_Transito] PRIMARY KEY CLUSTERED 
(
	[Num_Vuelo] ASC,
	[Fch_Prog] ASC,
	[Tip_Operacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[TUA_Vuelo_Transito] ADD  CONSTRAINT [DF_TUA_Vuelo_Transito_Flg_Eliminado]  DEFAULT ((0)) FOR [Flg_Eliminado]


END


