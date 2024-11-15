/*----------------------------------------------------------------------------------------------------------------
Obtener vuelo de transito en un intervalo de horas
------------------------------------------------------------------------------------------------------------------
PROCEDURE : usp_obtener_vuelos_transito_ventana_horas : 
AUTHOR    : Daniel Castillo
DATE      : 22-Sept-2020
USE       : TUUA
----------------------------------------------------------------------------------------------------*/

CREATE OR ALTER PROCEDURE usp_obtener_vuelos_transito_ventana_horas      
      
@fecha_actual datetime,      
@Tip_Vuelo char(1),      
@Tip_Operacion char(1),      
@ventana_horas int      
      
as      
begin      
      
 if @Tip_Operacion = 'L'      
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
  where Flg_Eliminado = 0 and       
  (      
        
     (Hor_Prog >= DATEADD(hour,-1*@ventana_horas,@fecha_actual)  and Hor_Prog <= @fecha_actual)      
  or (Fch_Real >= DATEADD(hour,-1*@ventana_horas,@fecha_actual)  and Fch_Real <= @fecha_actual)      
  or (Fch_Est  >= DATEADD(hour,-1*@ventana_horas,@fecha_actual)  and Fch_Est  <= @fecha_actual)      
        
  )      
  and Tip_Vuelo = @Tip_Vuelo and Tip_Operacion = @Tip_Operacion  and Dsc_Estado = 'ATERRIZO'     
  order by Hor_Prog desc      
      
 end      
 else       
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
  where Flg_Eliminado = 0       
  and       
  (      
     (Hor_Prog >= @fecha_actual and Hor_Prog <=DATEADD(hour,@ventana_horas,@fecha_actual))      
  or (Fch_Real >= @fecha_actual and Fch_Real <=DATEADD(hour,@ventana_horas,@fecha_actual))      
  or (Fch_Est  >= @fecha_actual and Fch_Est  <=DATEADD(hour,@ventana_horas,@fecha_actual))      
      
  )      
  and Tip_Vuelo = @Tip_Vuelo and Tip_Operacion = @Tip_Operacion and Dsc_Estado <> 'FIN EMBARQ'  
  order by Hor_Prog asc      
      
 end      
      
end