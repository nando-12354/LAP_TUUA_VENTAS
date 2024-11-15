-- =============================================
-- Author:		Daniel Castillo
-- Create date: 2019-10-06
-- Description:	Se agrega la columna Num_Puerta
-- Update date: 2022-08-20
-- Description: se agrega columnas: @Fch_Est, @Fch_Prog, @Fch_Real y @Dsc_Estado
-- =============================================

alter proc [dbo].[usp_actualizar_tua_vuelo]
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
@Num_Puerta varchar(10),
@Fch_Est datetime,
@Fch_Prog datetime,
@Fch_Real datetime,
@Dsc_Estado varchar(50)
as  
  
begin  

    if convert(date,@Fch_Est) = convert(date,'19000101',112)
    begin
        set @Fch_Est = null
    end

    if convert(date,@Fch_Prog) = convert(date,'19000101',112)
    begin
        set @Fch_Prog = null
    end

    if convert(date,@Fch_Real) = convert(date,'19000101',112)
    begin
        set @Fch_Real = null
    end


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
	    ,Fch_Est = @Fch_Est
      ,Fch_Prog = @Fch_Prog
      ,Fch_Real = @Fch_Real
      ,Dsc_Estado = @Dsc_Estado
 WHERE [Cod_Compania] = @Cod_Compania and [Num_Vuelo] = @Num_Vuelo and [Fch_Vuelo] = @Fch_Vuelo  
 end
go

