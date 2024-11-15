/*----------------------------------------------------------------------------------------------------------------
Eliminar vuelo de transito
------------------------------------------------------------------------------------------------------------------
PROCEDURE : usp_eliminar_vuelo_transito : 
AUTHOR    : Daniel Castillo
DATE      : 22-Sept-2020
USE       : TUUA
----------------------------------------------------------------------------------------------------*/


create or alter proc usp_eliminar_vuelo_transito  
@Num_Vuelo varchar(10),  
@Fch_Prog date,  
@Tip_Operacion  char(1)  
  
as  
begin  
UPDATE [dbo].[TUA_Vuelo_Transito]  
   SET   
      [Flg_Eliminado] = 1  
 WHERE Num_Vuelo = @Num_Vuelo and Fch_Prog = @Fch_Prog and Tip_Operacion = @Tip_Operacion  
  
 end  
  
  