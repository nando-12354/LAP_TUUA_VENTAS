--insertar opciones de menú

INSERT INTO [dbo].[TUA_ArbolModulo]
           ([Cod_Modulo]
           ,[Cod_Proceso]
           ,[Id_Proceso]
           ,[Dsc_Proceso]
           ,[Cod_Proceso_Padre]
           ,[Tip_Nivel]
           ,[Tip_Estado]
           ,[Flg_Permitido]
           ,[Dsc_Archivo]
           ,[Dsc_Icono]
           ,[Dsc_Texto_Ayuda]
           ,[Dsc_Ind_Critic]
           ,[Dsc_Color_Critic]
           ,[Dsc_Tab_Filtro]
           ,[Dsc_Licencia]
           ,[Num_Posicion])
     VALUES
           ('005'
           ,'E0469'
           ,''
           ,'Tickets Por Salida'
           ,'E0400'
           ,2
           ,1
           ,1
           ,'Reh_TicketsPorSalida.aspx'
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,3);


INSERT INTO [dbo].[TUA_ArbolModulo]
           ([Cod_Modulo]
           ,[Cod_Proceso]
           ,[Id_Proceso]
           ,[Dsc_Proceso]
           ,[Cod_Proceso_Padre]
           ,[Tip_Nivel]
           ,[Tip_Estado]
           ,[Flg_Permitido]
           ,[Dsc_Archivo]
           ,[Dsc_Icono]
           ,[Dsc_Texto_Ayuda]
           ,[Dsc_Ind_Critic]
           ,[Dsc_Color_Critic]
           ,[Dsc_Tab_Filtro]
           ,[Dsc_Licencia]
           ,[Num_Posicion])
     VALUES
           ('005'
           ,'E0471'
           ,''
           ,'Boarding Pass Por Salida'
           ,'E0450'
           ,2
           ,1
           ,1
           ,'Reh_BcbpPorSalida.aspx'
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,3);

--insertar opciones de rehabilitación a Administrador y a Dutty Office

--admin
INSERT INTO [dbo].[TUA_PerfilRol]
           ([Cod_Proceso]
           ,[Cod_Modulo]
           ,[Cod_Rol]
           ,[Flg_Permitido]
           ,[Log_Usuario_Mod]
           ,[Log_Fecha_Mod]
           ,[Log_Hora_Mod])
     VALUES
           ('E0469'
           ,'005'
           ,'R0001'
           ,1
           ,'U000001'
           ,'20180724'
           ,'120000');

INSERT INTO [dbo].[TUA_PerfilRol]
           ([Cod_Proceso]
           ,[Cod_Modulo]
           ,[Cod_Rol]
           ,[Flg_Permitido]
           ,[Log_Usuario_Mod]
           ,[Log_Fecha_Mod]
           ,[Log_Hora_Mod])
     VALUES
           ('E0471'
           ,'005'
           ,'R0001'
           ,1
           ,'U000001'
           ,'20180724'
           ,'120000');

--duty office

INSERT INTO [dbo].[TUA_PerfilRol]
           ([Cod_Proceso]
           ,[Cod_Modulo]
           ,[Cod_Rol]
           ,[Flg_Permitido]
           ,[Log_Usuario_Mod]
           ,[Log_Fecha_Mod]
           ,[Log_Hora_Mod])
     VALUES
           ('E0469'
           ,'005'
           ,'R0006'
           ,1
           ,'U000001'
           ,'20180724'
           ,'120000');

INSERT INTO [dbo].[TUA_PerfilRol]
           ([Cod_Proceso]
           ,[Cod_Modulo]
           ,[Cod_Rol]
           ,[Flg_Permitido]
           ,[Log_Usuario_Mod]
           ,[Log_Fecha_Mod]
           ,[Log_Hora_Mod])
     VALUES
           ('E0471'
           ,'005'
           ,'R0006'
           ,1
           ,'U000001'
           ,'20180724'
           ,'120000');





