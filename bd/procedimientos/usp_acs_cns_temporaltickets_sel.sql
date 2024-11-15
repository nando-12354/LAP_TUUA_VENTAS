-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	selecciona todos los tickets temporales por salida 
-- =============================================
CREATE PROCEDURE [dbo].[usp_acs_cns_temporaltickets_sel]
AS

SELECT 
TT.Cod_Numero_Ticket,
TT.Num_Serie,
TT.Fch_Creacion,
TT.Hor_Creacion,
TT.Fch_Registro
FROM TUA_Temporal_Tickets TT
GO


