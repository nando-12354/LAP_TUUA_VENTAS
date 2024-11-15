-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	selecciona tickets temporales por salida por numero de ticket
-- =============================================
CREATE PROCEDURE [dbo].[usp_acs_cns_getticket_sel]  
(  
@Cod_Numero_Ticket VARCHAR(16)
)  
AS  
  
SET NOCOUNT ON 

SELECT T.Cod_Numero_Ticket,
T.Num_Serie,
T.Fch_Creacion,
T.Hor_Creacion,
T.Fch_Registro
FROM dbo.TUA_Temporal_Tickets T WITH(NOLOCK)
WHERE T.Cod_Numero_Ticket=@Cod_Numero_Ticket

SET NOCOUNT OFF
GO


