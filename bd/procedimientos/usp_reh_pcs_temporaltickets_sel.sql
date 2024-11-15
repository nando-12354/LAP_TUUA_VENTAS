-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	selecciona tickets temporales por salida por fecha
-- =============================================
CREATE PROCEDURE [dbo].[usp_reh_pcs_temporaltickets_sel]
@Fch_Vuelo CHAR(8)
AS
BEGIN
SELECT Cod_Numero_Ticket,
Num_Vuelo,
Fch_Vuelo,
Num_Serie,
Fch_Creacion,
Hor_Creacion,
Fch_Registro
FROM TUA_Temporal_Tickets WITH(NOLOCK)
WHERE Fch_Vuelo=@Fch_Vuelo
END

GO


