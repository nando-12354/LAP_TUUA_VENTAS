-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	Elimina tickets temporales rehabilitados
-- =============================================
CREATE PROCEDURE [dbo].[usp_reh_pcs_temporalticket_del]
@Cod_Numero_Ticket VARCHAR(16)
AS
BEGIN
DELETE FROM TUA_Temporal_Tickets
WHERE Cod_Numero_Ticket=@Cod_Numero_Ticket
END

GO


