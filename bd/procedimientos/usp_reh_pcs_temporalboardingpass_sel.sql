
/****** Object:  StoredProcedure [dbo].[usp_reh_pcs_temporalboardingpass_sel]    Script Date: 18/07/2018 04:32:06 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	selecciona boardingpass temporales por salida por fecha
-- =============================================

CREATE PROCEDURE [dbo].[usp_reh_pcs_temporalboardingpass_sel]
@Cod_Compania CHAR(10),
@Fch_Vuelo CHAR(8),
@Num_Vuelo VARCHAR(10)
AS
BEGIN
SELECT Num_Secuencial_Bcbp,
Cod_Numero_Bcbp,
Num_Vuelo,
Fch_Vuelo,
Num_Asiento,
Nom_Pasajero,
Num_Checkin,
Fch_Creacion,
Hor_Creacion,
Fch_Registro 
FROM TUA_Temporal_BoardingPass WITH(NOLOCK)
WHERE Cod_Compania=@Cod_Compania
AND Fch_Vuelo=@Fch_Vuelo
AND (Num_Vuelo=@Num_Vuelo OR ISNULL(@Num_Vuelo,'')='')
END

GO


