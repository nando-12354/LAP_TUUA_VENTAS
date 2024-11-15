/*----------------------------------------------------------------------------------------------------------------
Obtener vuelo de transito por fecha programada
------------------------------------------------------------------------------------------------------------------
PROCEDURE : usp_tua_obtener_vuelo_transito_dia : 
AUTHOR    : Daniel Castillo
DATE      : 22-Sept-2020
USE       : TUUA
----------------------------------------------------------------------------------------------------*/

CREATE or alter proc usp_tua_obtener_vuelo_transito_dia    
@fecha_programada datetime    
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
 WHERE Fch_Prog = @fecha_programada and Flg_Eliminado = 0    
    
end