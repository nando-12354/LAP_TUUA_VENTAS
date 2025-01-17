USE [BD_TUUA_PRD]
GO
/****** Object:  StoredProcedure [dbo].[usp_ope_pcs_compSEAE_sel]    Script Date: 07/05/2013 10:47:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[usp_ope_pcs_compSEAE_sel] '2012','09','B','','0'
--[usp_ope_pcs_compSEAE_sel] '2011','01','B','','1'
--[usp_ope_pcs_compSEAE_sel] '2011','01','T','CON','0'

ALTER PROCEDURE [dbo].[usp_ope_pcs_compSEAE_sel](
@Anio char(4),
@Mes char(2),
@Tipo_Documento char(1),
@Tip_Venta char(3),
@Flg_Aero char(1)
)
as 

BEGIN

IF(@Tipo_Documento = 'T')
BEGIN

	IF(@Tip_Venta = 'CON')
	BEGIN
		IF(@Flg_Aero = '0') 
		BEGIN
		
		SELECT '20501577252' RucLap,@Anio + @Mes Periodo,Fch_Emision,SUM(Nacionales) T_Nac,SUM(Internacionales) T_Int,sum(Imp_Nac) Imp_Nac,sum(Imp_Int) Imp_Int
		FROM (
		SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,count(T.Cod_Numero_ticket) Nacionales,0 Internacionales,
		CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
		ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
		ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Nac,0 Imp_Int
		from tua_ticket T WITH (NOLOCK)
		where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes  AND
		(T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' 
		OR (T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND 
		(Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3')) 
		and T.Cod_Tipo_Ticket in ('T02','T03')
		group by T.COD_MONEDA,T.Fch_creacion

		UNION ALL
				
		select  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,0 Nacionales,count(T.Cod_Numero_ticket) Internacionales,
		0 Imp_Nac,
		CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
		ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
		ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Int
		from tua_ticket T (NOLOCK)
		where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes  AND
		(T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' 
		OR (T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND 
		(Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3')) 
		and T.Cod_Tipo_Ticket in ('T01','T04')
		group by T.COD_MONEDA,T.Fch_creacion

		) TABLA
		GROUP BY RucLap,Fch_Emision
		ORDER BY Fch_Emision

		END ELSE IF(@Flg_Aero = '1')
		BEGIN
		
		SELECT '20501577252' RucLap, @Anio + @Mes Periodo,Fch_Emision,SUM(Nacionales) T_Nac,SUM(Internacionales) T_Int,sum(Imp_Nac) Imp_Nac,sum(Imp_Int) Imp_Int,COD_COMPANIA
		FROM(

		SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,count(T.Cod_Numero_ticket) Nacionales,0 Internacionales,
		CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
		ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
		ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Nac,0 Imp_Int,COD_COMPANIA
		from tua_ticket T (NOLOCK)
		where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes AND
		(T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' OR 
		(T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND 
		(Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))	
		and T.Cod_Tipo_Ticket in ('T02','T03')	
		group by T.COD_MONEDA,COD_COMPANIA,T.Fch_Creacion

		UNION ALL

		SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,0 Nacionales,count(T.Cod_Numero_ticket) Internacionales,
		0 Imp_Nac,
		CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
		ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
		ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Int,COD_COMPANIA
		from tua_ticket T (NOLOCK)
		where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes AND
		(T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' OR 
		(T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND 
		(Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))	
		and T.Cod_Tipo_Ticket in ('T01','T04')	
		group by T.COD_MONEDA,COD_COMPANIA,T.Fch_Creacion

		) TABLA
		GROUP BY COD_COMPANIA,FCH_EMISION
		ORDER BY FCH_EMISION
		END
	END 
	ELSE IF(@Tip_Venta = 'CRE') BEGIN
			IF (@Flg_Aero = '0')
			BEGIN
			
			SELECT '20501577252' RucLap,@Anio + @Mes Periodo,Fch_Emision,SUM(Nacionales) T_Nac,SUM(Internacionales) T_Int,sum(Imp_Nac) Imp_Nac,sum(Imp_Int) Imp_Int
			FROM(
			select  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,count(T.Cod_Numero_ticket) Nacionales,0 Internacionales,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Nac,0 Imp_Int
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes  AND
			(T.Cod_Modalidad_Venta='M0004') AND  T.Flg_Cobro='0' 
			AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' 
			AND Tip_Anulacion='3'))
			and T.Cod_Tipo_Ticket in ('T02','T03')
			group by T.COD_MONEDA,T.Fch_creacion

			UNION ALL
				
			select  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,0 Nacionales,count(T.Cod_Numero_ticket) Internacionales,
			0 Imp_Nac,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Int
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes  AND
			(T.Cod_Modalidad_Venta='M0004') AND  T.Flg_Cobro='0' 
			AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' 
			AND Tip_Anulacion='3'))
			and T.Cod_Tipo_Ticket in ('T01','T04')
			group by T.COD_MONEDA,T.Fch_creacion
			) TABLA
			GROUP BY RUCLAP,FCH_EMISION
			ORDER BY FCH_EMISION

			END ELSE IF (@Flg_Aero = '1')
			BEGIN
				
			SELECT '20501577252' RucLap, @Anio + @Mes Periodo,Fch_Emision,SUM(Nacionales) T_Nac,SUM(Internacionales) T_Int,sum(Imp_Nac) Imp_Nac,sum(Imp_Int) Imp_Int,COD_COMPANIA
			FROM(

			SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,count(T.Cod_Numero_ticket) Nacionales,0 Internacionales,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Nac,0 Imp_Int,COD_COMPANIA
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes AND
			(T.Cod_Modalidad_Venta='M0004') AND  T.Flg_Cobro='0' 
			AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' 
			AND Tip_Anulacion='3'))
			and T.Cod_Tipo_Ticket in ('T02','T03')	
			group by T.COD_MONEDA,COD_COMPANIA,T.FCH_CREACION

			UNION ALL

			SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,0 Nacionales,count(T.Cod_Numero_ticket) Internacionales,
			0 Imp_Nac,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Int,COD_COMPANIA
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes AND
			(T.Cod_Modalidad_Venta='M0004') AND  T.Flg_Cobro='0' 
			AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' 
			AND Tip_Anulacion='3'))
			and T.Cod_Tipo_Ticket in ('T01','T04')	
			group by T.COD_MONEDA,COD_COMPANIA,T.FCH_CREACION

			) TABLA
			GROUP BY COD_COMPANIA,FCH_EMISION
			ORDER BY FCH_EMISION

			END
	END
	ELSE IF (@Tip_Venta='CRU') BEGIN
			IF(@Flg_Aero = '0')
			BEGIN
			
			SELECT '20501577252' RucLap,@Anio + @Mes Periodo,Fch_Emision,SUM(Nacionales) T_Nac,SUM(Internacionales) T_Int,sum(Imp_Nac) Imp_Nac,sum(Imp_Int) Imp_Int
			FROM(
			select  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,count(T.Cod_Numero_ticket) Nacionales,0 Internacionales,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Nac,0 Imp_Int
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes  AND
			T.Cod_Modalidad_Venta='M0004' AND  T.Flg_Cobro='1'
			and T.Cod_Tipo_Ticket in ('T02','T03')
			group by T.COD_MONEDA,T.FCH_CREACION

			UNION ALL
				
			select  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,0 Nacionales,count(T.Cod_Numero_ticket) Internacionales,
			0 Imp_Nac,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Int
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes  AND
			T.Cod_Modalidad_Venta='M0004' AND  T.Flg_Cobro='1'
			and T.Cod_Tipo_Ticket in ('T01','T04')
			group by T.COD_MONEDA,T.FCH_CREACION
			) TABLA
			GROUP BY RUCLAP,FCH_EMISION
			ORDER BY FCH_EMISION

			END ELSE IF (@Flg_Aero = '1')
			BEGIN

			SELECT '20501577252' RucLap, @Anio + @Mes Periodo,Fch_Emision,SUM(Nacionales) T_Nac,SUM(Internacionales) T_Int,sum(Imp_Nac) Imp_Nac,sum(Imp_Int) Imp_Int,COD_COMPANIA
			FROM(

			SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,count(T.Cod_Numero_ticket) Nacionales,0 Internacionales,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Nac,0 Imp_Int,COD_COMPANIA
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes AND
			T.Cod_Modalidad_Venta='M0004' AND  T.Flg_Cobro='1'
			and T.Cod_Tipo_Ticket in ('T02','T03')	
			group by T.COD_MONEDA,COD_COMPANIA,T.FCH_CREACION

			UNION ALL

			SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,0 Nacionales,count(T.Cod_Numero_ticket) Internacionales,
			0 Imp_Nac,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Int,COD_COMPANIA
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes AND
			T.Cod_Modalidad_Venta='M0004' AND  T.Flg_Cobro='1'
			and T.Cod_Tipo_Ticket in ('T01','T04')	
			group by T.COD_MONEDA,COD_COMPANIA,T.FCH_CREACION

			) TABLA
			GROUP BY COD_COMPANIA,FCH_EMISION
			ORDER BY FCH_EMISION

			END

	END
	ELSE IF (@Tip_Venta='ATM') BEGIN
			IF(@Flg_Aero = '0')
			BEGIN

			SELECT '20501577252' RucLap,@Anio + @Mes Periodo,Fch_Emision,SUM(Nacionales) T_Nac,SUM(Internacionales) T_Int,sum(Imp_Nac) Imp_Nac,sum(Imp_Int) Imp_Int
			FROM(
			select  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,count(T.Cod_Numero_ticket) Nacionales,0 Internacionales,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Nac,0 Imp_Int
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes  AND
			T.Cod_Modalidad_Venta='M0005' AND (Tip_Estado_Actual<>'X' 
			OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
			and T.Cod_Tipo_Ticket in ('T02','T03')
			group by T.COD_MONEDA,T.FCH_CREACION

			UNION ALL
				
			select  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,0 Nacionales,count(T.Cod_Numero_ticket) Internacionales,
			0 Imp_Nac,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Int
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes  AND
			T.Cod_Modalidad_Venta='M0005' AND (Tip_Estado_Actual<>'X' 
			OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
			and T.Cod_Tipo_Ticket in ('T01','T04')
			group by T.COD_MONEDA,T.FCH_CREACION
			) TABLA
			GROUP BY RUCLAP,FCH_EMISION
			ORDER BY FCH_EMISION

			END ELSE IF(@Flg_Aero = '1')
			BEGIN

			SELECT '20501577252' RucLap, @Anio + @Mes Periodo,Fch_Emision,SUM(Nacionales) T_Nac,SUM(Internacionales) T_Int,sum(Imp_Nac) Imp_Nac,sum(Imp_Int) Imp_Int,COD_COMPANIA
			FROM(

			SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,count(T.Cod_Numero_ticket) Nacionales,0 Internacionales,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Nac,0 Imp_Int,COD_COMPANIA
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes AND
			T.Cod_Modalidad_Venta='M0005' AND (Tip_Estado_Actual<>'X' 
			OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
			and T.Cod_Tipo_Ticket in ('T02','T03')	
			group by T.COD_MONEDA,COD_COMPANIA,T.FCH_CREACION

			UNION ALL

			SELECT  '20501577252' RucLap,@Anio + @Mes Periodo,[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](T.Fch_creacion) Fch_Emision,0 Nacionales,count(T.Cod_Numero_ticket) Internacionales,
			0 Imp_Nac,
			CASE WHEN T.COD_MONEDA='DOL' THEN SUM(T.Imp_Precio)
			ELSE CASE WHEN T.COD_MONEDA='SOL' THEN SUM(T.Imp_Precio/T.Imp_Tasa_Venta)
			ELSE SUM(T.Imp_Precio*T.Imp_Tasa_Cambio/T.Imp_Tasa_Venta) END END Imp_Int,COD_COMPANIA
			from tua_ticket T WITH (NOLOCK)
			where SUBSTRING(T.fch_creacion,0,7) = @Anio + @Mes AND
			T.Cod_Modalidad_Venta='M0005' AND (Tip_Estado_Actual<>'X' 
			OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
			and T.Cod_Tipo_Ticket in ('T01','T04')	
			group by T.COD_MONEDA,COD_COMPANIA,T.FCH_CREACION

			) TABLA
			GROUP BY COD_COMPANIA,FCH_EMISION
			ORDER BY FCH_EMISION
			END
	END

END ELSE IF (@Tipo_Documento = 'B') BEGIN
		IF(@Flg_Aero = '0')
		BEGIN
		select '20501577252' RucLap,@Anio + @Mes Periodo,
		[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](BP.fch_creacion) Fecha,num_serie,
		substring(num_secuencial,3,9) Correlativo,
		case when (BP.Tip_Estado = 'X' AND BP.Tip_Anulacion = '1') then 0 else BP.Imp_Precio end Imp_Precio,
		case BP.Cod_Moneda when 'DOL'then 'DA'
		when 'SOL' then 'NS'
		else BP.Cod_Moneda end Moneda,
		case when TC.Cod_IATA <> '' or (BP.Tip_Estado = 'X' and BP.Tip_Anulacion = '1') then '1'
		else '0' end RetCIA,
		case when (BP.Tip_Ingreso = 'A' OR BP.Tip_Ingreso = 'M') then BP.Cod_Numero_Bcbp
		else case when (BP.Tip_estado = 'X' and BP.Tip_Anulacion = '1') then '1' 
		else '' end end NroTarjeta,
		case when (BP.Tip_estado = 'X' and BP.Tip_Anulacion = '1') then ''
		else TC.Dsc_Ruc end Dsc_Ruc,
		case when BP.Tip_estado = 'X' then '' 
		else TC.Cod_IATA end IATA,
		case when (BP.Tip_Estado = 'X' AND BP.Tip_Anulacion = '1') then [dbo].[fdb_TUA_FORMATO_FECHA_HORA4](BP.fch_creacion) else [dbo].[fdb_TUA_FORMATO_FECHA_HORA4](BP.Fch_Vuelo) end Fch_Vuelo,
		RTRIM(BP.Num_Vuelo) AS Num_Vuelo,

		case when LEN(Num_Asiento) = 3 then Num_Asiento
		when Num_Asiento is null then '0'
		when Num_Asiento = '' then '0'
		else substring(Num_Asiento,2,3) end Num_Asiento,

		case when (BP.Tip_Estado = 'X' AND BP.Tip_Anulacion = '1') then 'ANULADO'  
		else replace(LEFT(TC.Dsc_Compania,25),'Ñ','N') end Dsc_Compania    
     
		from tua_boardingbcbp BP WITH (NOLOCK)  
		inner join tua_compania TC on BP.Cod_Compania = TC.Cod_Compania 
		where substring(BP.fch_creacion,0,7) = @Anio + @Mes --and BP.Tip_estado = 'U'
		END ELSE IF (@Flg_Aero = '1') BEGIN
		
		select '20501577252' RucLap,@Anio + @Mes Periodo,
		[dbo].[fdb_TUA_FORMATO_FECHA_HORA4](BP.fch_creacion) Fecha,num_serie,
		substring(num_secuencial,3,9) Correlativo,
		case when (BP.Tip_Estado = 'X' AND BP.Tip_Anulacion = '1') then 0 else BP.Imp_Precio end Imp_Precio,
		case BP.Cod_Moneda when 'DOL'then 'DA'
		when 'SOL' then 'NS'
		else BP.Cod_Moneda end Moneda,
		case when TC.Cod_IATA <> '' or (BP.Tip_Estado = 'X' AND BP.Tip_Anulacion = '1') then '1'
		else '0' end RetCIA,
		case when (BP.Tip_Ingreso = 'A' OR BP.Tip_Ingreso = 'M') then BP.Cod_Numero_Bcbp
		else case when (BP.Tip_estado = 'X' AND BP.Tip_Anulacion = '1') then '1'
		else '' 
		end end NroTarjeta,
		case when (BP.Tip_estado = 'X' AND BP.Tip_Anulacion = '1') then ''
		else TC.Dsc_Ruc
		end Dsc_Ruc,
		case when (BP.Tip_estado = 'X' AND BP.Tip_Anulacion = '1') then ''
		else TC.Cod_IATA 
		end IATA,
		case when (BP.Tip_Estado = 'X' AND BP.Tip_Anulacion = '1') then [dbo].[fdb_TUA_FORMATO_FECHA_HORA4](BP.fch_creacion) else [dbo].[fdb_TUA_FORMATO_FECHA_HORA4](BP.Fch_Vuelo) end Fch_Vuelo,
		RTRIM(BP.Num_Vuelo) AS Num_Vuelo,

--		case when LEN(BP.Num_Asiento) = 3 then BP.Num_Asiento
--		else substring(BP.Num_Asiento,2,3) end Num_Asiento,

		case when Num_Asiento is null then '0'
		when Num_Asiento = '' then '0'
		when LEN(Num_Asiento) = 3 then Num_Asiento
		else substring(Num_Asiento,2,3) end Num_Asiento,

		--TC.Dsc_Compania,

--		case when (BP.Tip_Estado = 'X' AND BP.Tip_Anulacion = '1') then 'ANULADO' else LEFT(TC.Dsc_Compania,25) end Dsc_Compania
--		,BP.Cod_Compania 

		case when (BP.Tip_Estado = 'X' AND BP.Tip_Anulacion = '1') then 'ANULADO'  
		else replace(LEFT(TC.Dsc_Compania,25),'Ñ','N') end Dsc_Compania ,
		BP.Cod_Compania 

		from tua_boardingbcbp BP WITH (NOLOCK)

		inner join tua_compania TC on BP.Cod_Compania = TC.Cod_Compania 
		where substring(BP.fch_creacion,0,7) = @Anio + @Mes --and BP.Tip_estado = 'U'
		
		END
	END
END
