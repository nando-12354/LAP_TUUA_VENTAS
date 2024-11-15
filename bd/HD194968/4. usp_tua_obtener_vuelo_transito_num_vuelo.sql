/*----------------------------------------------------------------------------------------------------------------
Obtener vuelo de transito por numero de vuelo
------------------------------------------------------------------------------------------------------------------
PROCEDURE : usp_tua_obtener_vuelo_transito_num_vuelo : 
AUTHOR    : Daniel Castillo
DATE      : 22-Sept-2020
USE       : TUUA
----------------------------------------------------------------------------------------------------*/
create or alter proc usp_tua_obtener_vuelo_transito_num_vuelo    
@Num_Vuelo varchar(10),    
@Fch_Prog Date,    
@Tip_Operacion char(1)    
as    
begin    
SELECT [Num_Vuelo]    
      ,[Fch_Prog]    
      ,[Tip_Operacion]    
      ,[Hor_Prog]    
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
      ,[Flg_Eliminado]  
   ,[Cod_Iata]  
   ,[Nro_Vuelo]    
  FROM [dbo].[TUA_Vuelo_Transito]    
  Where Num_Vuelo = @Num_Vuelo and Fch_Prog = @Fch_Prog and Tip_Operacion = @Tip_Operacion and Flg_Eliminado = 0    
end