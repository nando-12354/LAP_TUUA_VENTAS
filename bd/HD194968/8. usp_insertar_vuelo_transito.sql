/*----------------------------------------------------------------------------------------------------------------
Insertar vuelo de transito
------------------------------------------------------------------------------------------------------------------
PROCEDURE : usp_obtener_vuelos_transito_ventana_horas : 
AUTHOR    : Daniel Castillo
DATE      : 22-Sept-2020
USE       : TUUA
----------------------------------------------------------------------------------------------------*/


CREATE OR ALTER proc usp_insertar_vuelo_transito    
    
 @Num_Vuelo varchar(10)    
,@Fch_Prog datetime    
,@Hor_Prog datetime    
,@Tip_Operacion char(1)    
,@Cod_Compania char(10)    
,@Tip_Vuelo char(1)    
,@Fch_Real datetime    
,@Fch_Est datetime    
,@Cod_Proc_Dest varchar(50)    
,@Cod_Escala varchar(50)    
,@Cod_Gate varchar(50)    
,@Tip_Gate_Terminal char(1)    
,@Cod_Faja varchar(50)    
,@Cod_Mostrador varchar(50)    
,@Dsc_Aerolinea varchar(50)    
,@Dsc_Proc_Destino varchar(50)    
,@Dsc_Estado varchar(50)  
,@Cod_Iata varchar(5)  
,@Nro_Vuelo varchar(5)    
    
as    
begin    
    
declare @Log_Fch_Creacion datetime    
declare @Log_Fch_Modificacion datetime    
    
set @Log_Fch_Creacion = GETDATE()    
set @Log_Fch_Modificacion = @Log_Fch_Creacion    
    
declare @cont as int     
    
select @cont = COUNT(*) from TUA_Vuelo_Transito    
where Num_Vuelo = @Num_Vuelo and Fch_Prog = @Fch_Prog and Tip_Vuelo = @Tip_Vuelo    
    
if @cont is null    
begin    
 set @cont = 0    
end    
    
if @cont > 0     
begin    
--actualizar    
    
    
UPDATE [dbo].[TUA_Vuelo_Transito]    
   SET     
       [Cod_Compania] = @Cod_Compania    
      ,[Tip_Vuelo] = @Tip_Vuelo    
      ,[Hor_Prog] = @Hor_Prog    
      ,[Fch_Real] = @Fch_Real    
      ,[Fch_Est] = @Fch_Est    
      ,[Cod_Proc_Dest] = @Cod_Proc_Dest    
      ,[Cod_Escala] = @Cod_Escala    
      ,[Cod_Gate] = @Cod_Gate    
      ,[Tip_Gate_Terminal] = @Tip_Gate_Terminal    
      ,[Cod_Faja] = @Cod_Faja    
      ,[Cod_Mostrador] = @Cod_Mostrador    
      ,[Dsc_Aerolinea] = @Dsc_Aerolinea    
      ,[Dsc_Proc_Destino] = @Dsc_Proc_Destino    
      ,[Log_Fch_Modificacion] = @Log_Fch_Modificacion    
      ,[Dsc_Estado] = @Dsc_Estado    
 WHERE Num_Vuelo = @Num_Vuelo and Fch_Prog = @Fch_Prog and Tip_Vuelo = @Tip_Vuelo    
    
end    
    
else    
begin    
 --insertar    
    
 INSERT INTO [dbo].[TUA_Vuelo_Transito]    
      ([Num_Vuelo]    
      ,[Fch_Prog]    
      ,[Hor_Prog]    
      ,[Tip_Operacion]    
      ,[Cod_Compania]    
      ,[Tip_Vuelo]    
      ,[Fch_Real]    
      ,[Fch_Est]    
      ,[Cod_Proc_Dest]    
      ,[Cod_Escala]    
      ,[Cod_Gate]    
      ,[Tip_Gate_Terminal]    
      ,[Cod_Faja]    
      ,[Cod_Mostrador]    
      ,[Dsc_Aerolinea]    
      ,[Dsc_Proc_Destino]    
      ,[Log_Fch_Creacion]    
      ,[Log_Fch_Modificacion]    
      ,[Dsc_Estado]  
   ,[Cod_Iata]  
   ,[Nro_Vuelo])    
   VALUES    
      (@Num_Vuelo    
      ,@Fch_Prog    
      ,@Hor_Prog    
      ,@Tip_Operacion    
      ,@Cod_Compania    
      ,@Tip_Vuelo    
      ,@Fch_Real    
      ,@Fch_Est    
      ,@Cod_Proc_Dest    
      ,@Cod_Escala    
      ,@Cod_Gate    
      ,@Tip_Gate_Terminal    
      ,@Cod_Faja    
      ,@Cod_Mostrador    
      ,@Dsc_Aerolinea    
      ,@Dsc_Proc_Destino    
      ,@Log_Fch_Creacion    
      ,@Log_Fch_Modificacion    
      ,@Dsc_Estado  
   ,@Cod_Iata  
   ,@Nro_Vuelo)    
    
end    
    
end 