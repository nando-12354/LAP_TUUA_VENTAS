-- =============================================
-- Author:		Daniel Castillo
-- Create date: 2019-10-06
-- Description:	Se agrega la columna Num_Puerta
-- =============================================

ALTER PROCEDURE [dbo].[usp_acs_cns_vueloprogramado_sel]    
(    
 @Fch_Vuelo varchar(8)=NULL,    
 @Num_Vuelo  varchar(10)    
)    
AS    
  
SELECT     
Cod_Compania,    
Num_Vuelo,    
Fch_Vuelo,    
Hor_Vuelo,    
Dsc_Vuelo,    
Tip_Vuelo,    
Tip_Estado,    
Dsc_Destino,    
Log_Usuario_Mod,    
Log_Fecha_Mod,    
Log_Hora_Mod,
Num_Puerta    
FROM TUA_VueloProgramado (NOLOCK)    
WHERE rtrim(@Fch_Vuelo)=rtrim(Fch_Vuelo) AND rtrim(@Num_Vuelo)=rtrim(Num_Vuelo)    