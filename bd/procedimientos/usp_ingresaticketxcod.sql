-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	inserta tickets en tabla temporal
-- =============================================

CREATE PROCEDURE [dbo].[usp_ingresaticketxcod] 
@Cod_Numero_Ticket VARCHAR(16),
@RetVal INT OUTPUT
AS

DECLARE @varCod_Numero_Ticket VARCHAR(16)
SET @varCod_Numero_Ticket=@Cod_Numero_Ticket

SET @RetVal=0

IF EXISTS(SELECT 1 FROM TUA_Temporal_Tickets WITH(NOLOCK)
WHERE Cod_Numero_Ticket=@varCod_Numero_Ticket)
BEGIN
	SET @RetVal=1
	RETURN	
END

IF NOT EXISTS(SELECT 1 FROM TUA_Ticket WITH(NOLOCK)
WHERE Cod_Numero_Ticket=@varCod_Numero_Ticket)
BEGIN
	SET @RetVal=2
	RETURN	
END

INSERT INTO TUA_Temporal_Tickets(
Cod_Numero_Ticket,
Num_Vuelo,
Fch_Vuelo,
Num_Serie,
Fch_Creacion,
Hor_Creacion,
Fch_Registro
)
SELECT 
Cod_Numero_Ticket,
Dsc_Num_Vuelo,
Fch_Vuelo,
Num_Serie,
Fch_Creacion,
Hor_Creacion,
GETDATE() AS Fch_Registro
FROM TUA_Ticket WITH(NOLOCK)
WHERE Cod_Numero_Ticket=@varCod_Numero_Ticket

GO


