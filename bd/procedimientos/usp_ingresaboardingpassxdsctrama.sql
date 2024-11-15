-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	registra boardingpass por salida en tabla temporal
-- =============================================

CREATE PROCEDURE [dbo].[usp_ingresaboardingpassxdsctrama]
@Num_Checkin VARCHAR(5),
@Num_Vuelo VARCHAR(10),
@Num_Asiento VARCHAR(10),
@Nom_Pasajero VARCHAR(50),
@Fch_Vuelo CHAR(8),
@RetVal INT OUTPUT
AS  
DECLARE @varNum_Checkin VARCHAR(5),@varNum_Vuelo VARCHAR(10),@varNum_Asiento VARCHAR(10),@varNom_Pasajero VARCHAR(50),@varFch_Vuelo CHAR(8)
SET @varNum_Checkin=@Num_Checkin
SET @varNum_Vuelo=@Num_Vuelo
SET @varNum_Asiento=@Num_Asiento
SET @varNom_Pasajero=@Nom_Pasajero
SET @varFch_Vuelo=@Fch_Vuelo

SET @RetVal=0

IF EXISTS(SELECT 1 
FROM TUA_Temporal_BoardingPass WITH(NOLOCK)  
WHERE Num_Vuelo=@varNum_Vuelo
AND Num_Asiento=@varNum_Asiento
AND Nom_Pasajero=@varNom_Pasajero
AND Fch_Vuelo=@varFch_Vuelo)
--AND Num_Checkin=@varNum_Checkin
BEGIN
	SET @RetVal=1
	RETURN	
END

IF NOT EXISTS(SELECT 1
FROM TUA_BoardingBcbp WITH(NOLOCK)  
WHERE Num_Vuelo=@varNum_Vuelo
AND Num_Asiento=@varNum_Asiento
AND Nom_Pasajero=@varNom_Pasajero
AND Fch_Vuelo=@varFch_Vuelo)
BEGIN
	SET @RetVal=2
	RETURN	
END
INSERT INTO TUA_Temporal_BoardingPass(
Num_Secuencial_Bcbp,
Cod_Numero_Bcbp,
Cod_Compania,
Num_Vuelo,
Fch_Vuelo,
Num_Asiento,
Nom_Pasajero,
Dsc_Trama_Bcbp,
Fch_Creacion,
Hor_Creacion,
Fch_Registro,
Num_Checkin
)  
SELECT 
Num_Secuencial_Bcbp,
Cod_Numero_Bcbp,  
Cod_Compania,   
Num_Vuelo,  
Fch_Vuelo,   
Num_Asiento,   
Nom_Pasajero,   
Dsc_Trama_Bcbp,   
Fch_Creacion,
Hor_Creacion,
GETDATE() AS Fch_Registro,
@varNum_Checkin
FROM TUA_BoardingBcbp WITH(NOLOCK)  
WHERE Num_Vuelo=@varNum_Vuelo
AND Num_Asiento=@varNum_Asiento
AND Nom_Pasajero=@varNom_Pasajero 
AND Fch_Vuelo=@varFch_Vuelo
--AND Num_Checkin=@varNum_Checkin
  
  

GO


