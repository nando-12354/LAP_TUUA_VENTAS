-- =============================================
-- Author:		Daniel Castillo
-- Create date: 2019-10-06
-- Description:	Se agrega la columna Num_Puerta
-- =============================================

ALTER proc [dbo].[usp_actualizar_tua_vuelo]  
@Cod_Compania char(10),  
@Num_Vuelo char(10),  
@Fch_Vuelo char(8),  
@Hor_Vuelo char(6),  
@Dsc_Vuelo varchar(50),  
@Tip_Vuelo char(1),  
@Tip_Estado char(1),  
@Dsc_Destino varchar(60),  
@Log_Usuario_Mod char(7),  
@Log_Fecha_Mod char(8),  
@Log_Hora_Mod char(6),  
@Flg_Programado char(1),
@Num_Puerta varchar(10)  
as  
  
begin  
UPDATE [dbo].[TUA_VueloProgramado]  
   SET   
    [Hor_Vuelo] = @Hor_Vuelo  
      ,[Dsc_Vuelo] = @Dsc_Vuelo  
      ,[Tip_Vuelo] = @Tip_Vuelo  
      ,[Tip_Estado] = @Tip_Estado  
      ,[Dsc_Destino] = @Dsc_Destino  
      ,[Log_Usuario_Mod] =@Log_Usuario_Mod  
      ,[Log_Fecha_Mod] = @Log_Fecha_Mod  
      ,[Log_Hora_Mod] = @Log_Hora_Mod  
      ,[Flg_Programado] = @Flg_Programado
	  ,Num_Puerta =   @Num_Puerta
 WHERE [Cod_Compania] = @Cod_Compania and [Num_Vuelo] = @Num_Vuelo and [Fch_Vuelo] = @Fch_Vuelo  
 end