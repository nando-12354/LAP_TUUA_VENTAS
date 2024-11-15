/*----------------------------------------------------------------------------------------------------------------
Insertar registro de transito
------------------------------------------------------------------------------------------------------------------
PROCEDURE : usp_insertar_tuua_transito : 
AUTHOR    : Daniel Castillo
DATE      : 22-Sept-2020
USE       : TUUA
----------------------------------------------------------------------------------------------------*/


CREATE OR ALTER proc usp_insertar_tuua_transito  
  
@Num_Vuelo_Origen varchar(10),  
@Fch_Vuelo_Origen date,  
@Num_Vuelo_Destino varchar(10),  
@Fch_Vuelo_Destino date,  
@Trama_Origen varchar(500),  
@Trama_Destino varchar(500),  
@Cod_Molinete varchar(4)  
  
as  
begin  
  
 declare @Cod_Compania_Origen char(10)  
 declare @Cod_Origen varchar(6)  
 declare @Dsc_Origen varchar(50)  
 declare @Hor_Vuelo_Origen datetime  
 declare @Tip_Vuelo_Origen char(1)  
 declare @Tip_Operacion_Origen char(1)  
 declare @Tip_Operacion_Destino char(1)  
  
  
 declare @Cod_Compania_Destino char(10)  
 declare @Cod_Destino varchar(6)  
 declare @Dsc_Destino varchar(50)  
 declare @Hor_Vuelo_Destino datetime  
 declare @Tip_Vuelo_Destino char(1)  
  
  
 declare @Log_Fch_Registro as datetime  
 declare @Log_Fch_Mod as datetime  
  
 set @Log_Fch_Registro = GETDATE()  
 set @Log_Fch_Mod = @Log_Fch_Registro  
  
 set @Tip_Operacion_Origen = 'L'  
 set @Tip_Operacion_Destino = 'S'  
  
  
 --obtener info de vuelo de origen y destino  
 --origen  
 select @Cod_Compania_Origen=Cod_Compania, @Cod_Origen=Cod_Proc_Dest, @Dsc_Origen=Dsc_Proc_Destino, @Hor_Vuelo_Origen=Hor_Prog, @Tip_Vuelo_Origen=Tip_Vuelo   
 from TUA_Vuelo_Transito  
 where Num_Vuelo = @Num_Vuelo_Origen and Fch_Prog = @Fch_Vuelo_Origen and Tip_Operacion = @Tip_Operacion_Origen  
  
 --destino  
 select @Cod_Compania_Destino=Cod_Compania, @Cod_Destino=Cod_Proc_Dest, @Dsc_Destino=Dsc_Proc_Destino, @Hor_Vuelo_Destino=Hor_Prog, @Tip_Vuelo_Destino=Tip_Vuelo   
 from TUA_Vuelo_Transito  
 where Num_Vuelo = @Num_Vuelo_Destino and Fch_Prog = @Fch_Vuelo_Destino and Tip_Operacion = @Tip_Operacion_Destino  
  
  
 if @Cod_Compania_Origen is null  
 begin  
  RAISERROR('Vuelo de origen no encontrado',16,1)  
 end  
  
 if @Cod_Compania_Destino is null  
 begin  
  RAISERROR('Vuelo de destino no encontrado',16,1)  
 end  
  
  
 INSERT INTO [dbo].[TUA_Transito]  
      ([Cod_Compania_Origen]  
      ,[Num_Vuelo_Origen]  
      ,[Fch_Vuelo_Origen]  
      ,[Hor_Vuelo_Origen]  
      ,[Tip_Vuelo_Origen]  
      ,[Tip_Operacion_Origen]  
      ,[Cod_Origen]  
      ,[Dsc_Origen]  
      ,[Cod_Compania_Destino]  
      ,[Num_Vuelo_Destino]  
      ,[Fch_Vuelo_Destino]  
      ,[Hor_Vuelo_Destino]  
      ,[Tip_Vuelo_Destino]  
      ,[Tip_Operacion_Destino]  
      ,[Cod_Destino]  
      ,[Dsc_Destino]  
      ,[Trama_Origen]  
      ,[Trama_Destino]  
      ,[Log_Fch_Registro]  
      ,[Log_Fch_Mod]  
      ,[Cod_Molinete])  
   VALUES  
      (@Cod_Compania_Origen  
      ,@Num_Vuelo_Origen  
      ,@Fch_Vuelo_Origen  
      ,@Hor_Vuelo_Origen  
      ,@Tip_Vuelo_Origen  
      ,@Tip_Operacion_Origen  
      ,@Cod_Origen  
      ,@Dsc_Origen  
      ,@Cod_Compania_Destino  
      ,@Num_Vuelo_Destino  
      ,@Fch_Vuelo_Destino  
      ,@Hor_Vuelo_Destino  
      ,@Tip_Vuelo_Destino  
      ,@Tip_Operacion_Destino  
      ,@Cod_Destino  
      ,@Dsc_Destino  
      ,@Trama_Origen  
      ,@Trama_Destino  
      ,@Log_Fch_Registro  
      ,@Log_Fch_Mod  
      ,@Cod_Molinete)  
  
  
  
end  
  
  
  