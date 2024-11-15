/****** Object:  StoredProcedure [dbo].[usp_reh_pcs_temporalboardingpass_del]    Script Date: 18/07/2018 04:33:53 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	elimina boardingpass temporales por salida 
-- =============================================

CREATE PROCEDURE [dbo].[usp_reh_pcs_temporalboardingpass_del]
@Cod_Numero_Bcbp VARCHAR(16)
AS
BEGIN
DELETE FROM TUA_Temporal_BoardingPass
WHERE Cod_Numero_Bcbp=@Cod_Numero_Bcbp
END

GO


