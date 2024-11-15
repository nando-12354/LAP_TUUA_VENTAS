USE [BD_TUUA_PRD]
GO

/****** Object:  StoredProcedure [dbo].[usp_ope_pcs_archventa_sel]    Script Date: 23/05/2019 15:07:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





--[usp_ope_pcs_archventa_sel] '20110104','20110104','BRD','0'
--[usp_ope_pcs_archventa_sel] '20110105','20110105','BRD','0'
--[usp_ope_pcs_archventa_sel] '20110104','20110105','BRD','0'


--[usp_ope_pcs_archventa_sel] '20190509','20190512','BRD','1'


--[usp_ope_pcs_archventa_sel] '20101201','20101231','CON','0'
ALTER PROCEDURE [dbo].[usp_ope_pcs_archventa_sel](
@Fch_Inicio char(8),
@Fch_Fin char(8),
@Tip_Venta char(3),
@Flg_Aerolinea char(1)
)
as
declare @campo1 varchar(20),
@secuencia int,
@orga varchar(20),
@canal varchar(20),
@sector varchar(20),
@cliente varchar(20),
@ruc varchar(20),
@clasedoc varchar(20),
@fecha char(8),
@tipdoc varchar(20),
@nroserie varchar(20),
@nrodoc varchar(20),
@maxSec int,
@nomCliente varchar(30),
@IGV numeric(8,4),
@estado varchar(20),
@clientePago varchar(40)


set @IGV=(SELECT CONVERT(DECIMAL(18, 2),Valor) FROM TUA_ParameGeneral (NOLOCK) WHERE Identificador='IGV')
set @maxSec=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='MSAV')
set @secuencia=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='SAAV')

if(@secuencia>=@maxSec)
set @secuencia=1
else set @secuencia=@secuencia+1

--update TUA_ParameGeneral set Valor=@secuencia where Identificador='SAAV'

set @campo1=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A100')
set @orga=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A120')
--set @secuencia=(select Valor from TUA_ParameGeneral where Identificador='SCAV')
set @canal=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A125')
set @sector=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A130')
set @cliente=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A135')
set @ruc=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A145')
set @clasedoc=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A150')
set @fecha=Substring([dbo].NTPFunction(),1,8)
set @tipdoc=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A160')
set @nroserie=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A165')
set @nrodoc=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A170')
set @estado=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A215')
set @clientePago=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A315')

if(@Tip_Venta='' and @Flg_Aerolinea='' and @Fch_Inicio is not null)
begin
	-- @Fch_Inicio contiene el valor secuencial para guardar en parametros.
	update TUA_ParameGeneral set Valor=@Fch_Inicio where Identificador='SAAV'
	select '',''
	return 0
end

if(@Tip_Venta='CON')
BEGIN
	set @nomCliente='CONTADO'
	IF(@Flg_Aerolinea='0')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',@clientePago,'','',''
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE  T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin and Fch_Vencimiento<>'' and (T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' OR (T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
			   AND T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial'  AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo
		ORDER BY PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
--		UNION		
--
--		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,@fecha,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),-CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,'',T.Cod_Moneda,@estado,'','','',''
--		from TUA_ListaDeCampos PG,TUA_TipoTicket TT,TUA_Compania C,TUA_TICKET T  INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
--		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' OR (T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND T.Tip_Estado_Actual='X' AND T.Tip_Anulacion='2'
--		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin
--		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,T.Cod_Moneda,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo	

	END ELSE IF(@Flg_Aerolinea='1')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente+' - '+C.Dsc_Compania,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),(T.Imp_Precio/@IGV)),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',C.Cod_SAP,'','',T.Flg_Cobro,C.Cod_Compania
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' OR (T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin and Fch_Vencimiento<>'' AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,C.Dsc_Compania,T.Flg_Cobro,C.Cod_IATA,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania
		ORDER BY PG.Dsc_Campo DESC, T.Fch_Creacion --ordenamiento 

	END
END 
ELSE IF(@Tip_Venta='CRE')BEGIN
	set @nomCliente='CREDITO EMISION'
	IF(@Flg_Aerolinea='0')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',@clientePago,'','',''
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Cod_Modalidad_Venta='M0004') AND  T.Flg_Cobro='0' AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin  AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
	END ELSE IF(@Flg_Aerolinea='1')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente+' - '+C.Dsc_Compania,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',C.Cod_SAP,'','',T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_Compania
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Cod_Modalidad_Venta='M0004') AND  T.Flg_Cobro='0' AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,C.Dsc_Compania,T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_IATA,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
	END
END
ELSE IF(@Tip_Venta='CRU') BEGIN
	set @nomCliente='CREDITO USO'
	IF(@Flg_Aerolinea='0')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',@clientePago,'','',''
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Cod_Modalidad_Venta='M0004' AND  T.Flg_Cobro='1'
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin and T.Fch_Uso IS NOT NULL AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION

	END ELSE IF(@Flg_Aerolinea='1')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente+' - '+C.Dsc_Compania,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',C.Cod_SAP,'','',T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_Compania
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Cod_Modalidad_Venta='M0004' AND  T.Flg_Cobro='1' 
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin and T.Fch_Uso IS NOT NULL AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,C.Dsc_Compania,T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_IATA,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
	END
END ELSE IF(@Tip_Venta='ATM')BEGIN
	set @nomCliente='CREDITO ATM'
	IF(@Flg_Aerolinea='0')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',@clientePago,'','',''
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Cod_Modalidad_Venta='M0005' AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin  AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION

	END ELSE IF(@Flg_Aerolinea='1')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente+' - '+C.Dsc_Compania,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',C.Cod_SAP,'','',T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_Compania
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Cod_Modalidad_Venta='M0005' AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin  AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,C.Dsc_Compania,T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_IATA,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
	END
END ELSE IF(@Tip_Venta='BRD')BEGIN
	set @nomCliente='BOARDING'
	IF(@Flg_Aerolinea='0')BEGIN
	  select sociedad,periodo,nroDocUser,secuencia,orga,canal,sector,cliente,nomCliente,RUC,claseDoc,fecha,tipoDoc,nroSerie,nroDoc,servicio,nomServicio,sum(Can_Ticket),impUnitario,sum(impTotal),fecFactura,sum(impSaldo),moneda,estado,obs,clientePago,filler1,filler2,cobro
	  from(
		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+T.Fch_Creacion nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente nomCliente,@ruc as RUC,@clasedoc claseDoc,T.Fch_Creacion fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,T.Fch_Creacion fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,@clientePago clientePago,'' filler1,'' filler2,'1' cobro
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Tip_Estado<>'X' OR (T.Tip_Estado='X' AND T.Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin  AND T.Num_Secuencial_Bcbp_Rel=0
		GROUP BY anho,mes,T.Fch_Creacion,T.Tip_Vuelo,PG.Cod_Relativo,PG.Dsc_Campo,T.Imp_Precio,T.Cod_Moneda

		UNION ALL
		
		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+SUBSTRING(T.Fch_Rehabilitacion,1,8) nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente nomCliente,@ruc as RUC,'ZCR' claseDoc,SUBSTRING(T.Fch_Rehabilitacion,1,8) fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,SUBSTRING(T.Fch_Rehabilitacion,1,8) fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,@clientePago clientePago,'' filler1,'' filler2,'2' cobro
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Fch_Rehabilitacion IS NOT NULL
		 and SUBSTRING(T.Fch_Rehabilitacion,1,8)>=@Fch_Inicio and SUBSTRING(T.Fch_Rehabilitacion,1,8)<=@Fch_Fin  AND T.Num_Secuencial_Bcbp_Rel=0
		GROUP BY anho,mes,T.Fch_Rehabilitacion,T.Tip_Vuelo,PG.Cod_Relativo,PG.Dsc_Campo,T.Imp_Precio,T.Cod_Moneda

		
		UNION ALL
		
		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+T.Log_Fecha_Mod nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente nomCliente,@ruc as RUC,'ZCR' claseDoc,T.Log_Fecha_Mod fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,T.Log_Fecha_Mod fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,@clientePago clientePago,'' filler1,'' filler2,'2' cobro
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Tip_Estado='X' AND T.Tip_Anulacion='1' AND T.Log_Fecha_Mod>=@Fch_Inicio and T.Log_Fecha_Mod<=@Fch_Fin  AND T.Num_Secuencial_Bcbp_Rel=0 and  C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial'  
		GROUP BY anho,mes,T.Log_Fecha_Mod,T.Tip_Vuelo,PG.Cod_Relativo,PG.Dsc_Campo,T.Imp_Precio,T.Cod_Moneda
	    )tabla 
		group by sociedad,periodo,nroDocUser,secuencia,orga,canal,sector,cliente,nomCliente,RUC,claseDoc,fecha,tipoDoc,nroSerie,nroDoc,servicio,nomServicio,impUnitario,fecFactura,moneda,estado,obs,clientePago,filler1,filler2,cobro
		order by fecha, nomServicio desc --ordenamiento DC 2019-05-21
	
	END ELSE IF(@Flg_Aerolinea='1')BEGIN
	 select sociedad,periodo,nroDocUser,secuencia,orga,canal,sector,cliente,nomCliente,RUC,claseDoc,fecha,tipoDoc,nroSerie,nroDoc,servicio,nomServicio,sum(Can_Ticket),impUnitario,sum(impTotal),fecFactura,sum(impSaldo),moneda,estado,obs,clientePago,filler1,filler2,modalidad,cobro,Cod_Compania
	 from(
		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente+' - '+C.Dsc_Compania nomCliente,@ruc as RUC,@clasedoc claseDoc,T.Fch_Creacion fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,T.Fch_Creacion fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,C.Cod_SAP clientePago,'' filler1,'' filler2,'M0002' modalidad,'1' cobro,C.Cod_Compania 
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin 
			and C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Tip_Estado<>'X' OR (T.Tip_Estado='X' AND T.Tip_Anulacion='3'))
		GROUP BY anho,mes,T.Fch_Creacion,T.Tip_Vuelo,C.Dsc_Compania,C.Cod_IATA,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania,T.Imp_Precio,T.Cod_Moneda
	
		UNION ALL

		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+SUBSTRING(T.Fch_Rehabilitacion,1,8)+C.Cod_IATA nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente+' - '+C.Dsc_Compania nomCliente,@ruc as RUC,'ZCR' claseDoc,SUBSTRING(T.Fch_Rehabilitacion,1,8) fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,SUBSTRING(T.Fch_Rehabilitacion,1,8) fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,C.Cod_SAP clientePago,'' filler1,'' filler2,'M0002' modalidad,'2' cobro,C.Cod_Compania 
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON SUBSTRING(T.Fch_Rehabilitacion,1,8) LIKE ( anho + mes + '%') 
		WHERE SUBSTRING(T.Fch_Rehabilitacion,1,8)>=@Fch_Inicio and SUBSTRING(T.Fch_Rehabilitacion,1,8)<=@Fch_Fin AND T.Fch_Rehabilitacion IS NOT NULL
			  AND C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial'
		GROUP BY anho,mes,T.Fch_Rehabilitacion,T.Tip_Vuelo,C.Dsc_Compania,C.Cod_IATA,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania,T.Imp_Precio,T.Cod_Moneda

		UNION ALL

		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+T.Log_Fecha_Mod+C.Cod_IATA nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente+' - '+C.Dsc_Compania nomCliente,@ruc as RUC,'ZCR' claseDoc,T.Log_Fecha_Mod fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,T.Log_Fecha_Mod fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,C.Cod_SAP clientePago,'' filler1,'' filler2,'M0002' modalidad,'2' cobro,C.Cod_Compania 
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON SUBSTRING(T.Fch_Rehabilitacion,1,8) LIKE ( anho + mes + '%') 
		WHERE T.Log_Fecha_Mod>=@Fch_Inicio and T.Log_Fecha_Mod<=@Fch_Fin AND T.Fch_Rehabilitacion IS NOT NULL and T.Tip_Estado='X' AND T.Tip_Anulacion='1'
			  AND C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial'
		GROUP BY anho,mes,T.Log_Fecha_Mod,T.Tip_Vuelo,C.Dsc_Compania,C.Cod_IATA,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania,T.Imp_Precio,T.Cod_Moneda
		)tabla group by sociedad,periodo,nroDocUser,secuencia,orga,canal,sector,cliente,nomCliente,RUC,claseDoc,fecha,tipoDoc,nroSerie,nroDoc,servicio,nomServicio,impUnitario,fecFactura,moneda,estado,obs,clientePago,filler1,filler2,modalidad,cobro,Cod_Compania

		order by fecha, nomServicio desc -- ordenamiento DC 2019-05-23
	END
END


GO


 
  
ALTER FUNCTION [dbo].[fdb_TUA_GET_PARAMETRO_BOARDING]  -- select [dbo].[fdb_TUA_GETPARAMETRO]('000000400234','Z5')
(
	@Num_Secuencial_Bcbp int,
	@Cod_Atributo CHAR(2)
)
RETURNS VARCHAR(20)
AS 
BEGIN

DECLARE @Dsc_Valor as VARCHAR(20)

SELECT @Dsc_Valor = Dsc_Valor
FROM TUA_ModVentaCompAtr tmca, TUA_BoardingBcbp tb, TUA_ModalidadVenta tmv
WHERE tb.Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp and
	tb.Cod_Compania = tmca.Cod_Compania and
	tmca.Cod_Modalidad_Venta = tmv.Cod_Modalidad_Venta and
	tmv.Tip_Modalidad = '2' and
	tmca.Cod_Atributo = @Cod_Atributo

IF (@Dsc_Valor IS NULL)
BEGIN
	SELECT @Dsc_Valor = Dsc_Valor
	FROM TUA_ModalidadAtrib tma, TUA_BoardingBcbp tb, TUA_ModalidadVenta tmv
	WHERE tb.Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp and
		tma.Cod_Modalidad_Venta = tmv.Cod_Modalidad_Venta and
		tmv.Tip_Modalidad = '2' and
		tma.Cod_Atributo = @Cod_Atributo
	
	IF (@Dsc_Valor IS NULL)
	BEGIN
		SELECT @Dsc_Valor = Valor
		FROM TUA_ParameGeneral tpg
		WHERE tpg.Identificador = @Cod_Atributo
	END	
END

RETURN @Dsc_Valor;
END
GO


ALTER FUNCTION [dbo].[fdb_TUA_GET_PARAMETRO_TICKET]  -- select [dbo].[fdb_TUA_GETPARAMETRO]('000000400234','Z5')
(
	@Cod_Numero_Ticket VARCHAR(16),
	@Cod_Atributo CHAR(2)
)
RETURNS VARCHAR(20)
AS 
BEGIN

DECLARE @Dsc_Valor as VARCHAR(20)

--LEVEL TIPO TICKET - COMPANIA
SELECT @Dsc_Valor = Dsc_Valor
FROM TUA_ModVentaCompAtr tmca, TUA_Ticket tt
WHERE tt.Cod_Numero_Ticket = @Cod_Numero_Ticket and
	tt.Cod_Modalidad_Venta = tmca.Cod_Modalidad_Venta and
	tt.Cod_Compania = tmca.Cod_Compania and
	tt.Cod_Tipo_Ticket = tmca.Cod_Tipo_Ticket and
	tmca.Cod_Atributo = @Cod_Atributo

IF (@Dsc_Valor IS NULL)
BEGIN
	--LEVEL COMPANIA
	SELECT @Dsc_Valor = Dsc_Valor
	FROM TUA_ModVentaCompAtr tmca, TUA_Ticket tt
	WHERE tt.Cod_Numero_Ticket = @Cod_Numero_Ticket and
		tt.Cod_Modalidad_Venta = tmca.Cod_Modalidad_Venta and
		tt.Cod_Compania = tmca.Cod_Compania and
		tmca.Cod_Atributo = @Cod_Atributo
	
	IF (@Dsc_Valor IS NULL)
	BEGIN
		--LEVEL TIPO TICKET - MODALIDAD VENTA
		SELECT @Dsc_Valor = Dsc_Valor
		FROM TUA_ModalidadAtrib tma, TUA_Ticket tt
		WHERE tt.Cod_Numero_Ticket = @Cod_Numero_Ticket and
			tt.Cod_Modalidad_Venta = tma.Cod_Modalidad_Venta and
			tt.Cod_Tipo_Ticket = tma.Cod_Tipo_Ticket and
			tma.Cod_Atributo = @Cod_Atributo
		
		IF (@Dsc_Valor IS NULL)
		BEGIN
			--LEVEL MODALIDAD VENTA
			SELECT @Dsc_Valor = Dsc_Valor
			FROM TUA_ModalidadAtrib tma, TUA_Ticket tt
			WHERE tt.Cod_Numero_Ticket = @Cod_Numero_Ticket and
				tt.Cod_Modalidad_Venta = tma.Cod_Modalidad_Venta and
				tma.Cod_Atributo = @Cod_Atributo AND tma.Cod_Tipo_Ticket IS NULL
			
			IF (@Dsc_Valor IS NULL)
			BEGIN
				SELECT @Dsc_Valor = Valor
				FROM TUA_ParameGeneral tpg
				WHERE tpg.Identificador = @Cod_Atributo
			END
		END
	END	
END

RETURN @Dsc_Valor;
END

--select [dbo].[fdb_TUA_GET_PARAMETRO_TICKET]('000000400234','Z5')
GO


ALTER  FUNCTION [dbo].[fdb_TUA_ROLES]
( @Cod_Usuario varchar(7) )
RETURNS varchar(4000)
AS
BEGIN

DECLARE @NomRol varchar(50)
DECLARE  @RESULT varchar(4000)

DECLARE Rol_Cursor CURSOR FOR
select r.Nom_Rol
from TUA_UsuarioRol u,TUA_Rol r
where u.Cod_Rol=r.Cod_Rol and @Cod_Usuario=u.Cod_Usuario

OPEN Rol_Cursor
FETCH NEXT FROM Rol_Cursor  
INTO @NomRol

set @RESULT=''

WHILE @@FETCH_STATUS = 0
BEGIN
    IF @RESULT=''
       SET @RESULT= @RESULT+ @NomRol
    ELSE
    set @RESULT= @RESULT+'|'+ @NomRol
   
    FETCH NEXT FROM Rol_Cursor  
    INTO @NomRol
END

CLOSE Rol_Cursor
DEALLOCATE Rol_Cursor 

RETURN @RESULT
END
GO


ALTER PROCEDURE [dbo].[usp_acs_cns_modventacomp_sel]
(@Cod_Compania char(10),
@Nom_Modalidad varchar(50)
)

AS
SET NOCOUNT ON

SELECT c.Cod_Compania,c.Cod_Modalidad_Venta,c.Dsc_Valor_Acumulado
FROM  dbo.TUA_ModVentaComp c (NOLOCK), dbo.TUA_ModalidadVenta m (NOLOCK)
WHERE c.Cod_Compania=@Cod_Compania and
      c.Cod_Modalidad_Venta=m.Cod_Modalidad_Venta and
      rtrim(m.Nom_Modalidad)=rtrim(@Nom_Modalidad)






GO

ALTER PROCEDURE [dbo].[usp_adm_cns_precioticket_sel](--[usp_vta_cns_precioticket_sel] 'I','A','N'
@Tip_Vuelo char(1),
@Tip_Pasajero char(1),
@Tip_Trasbordo char(1)
)AS

SELECT TT.Cod_Tipo_Ticket,TT.Tip_Vuelo,TT.Tip_Pasajero,TT.Tip_Trasbordo,TT.Dsc_Tipo_Ticket,
TT.Cod_Moneda,TT.Imp_Precio,TT.Tip_Estado,TT.Log_Usuario_Mod,TT.Log_Fecha_Mod,TT.Log_Hora_Mod,'' as Dsc_Simbolo,M.Dsc_Moneda
FROM TUA_TipoTicket TT,TUA_MONEDA M
WHERE TT.Tip_Vuelo=@Tip_Vuelo AND TT.Tip_Pasajero=@Tip_Pasajero AND TT.Tip_Trasbordo=@Tip_Trasbordo AND TT.Cod_Moneda=M.Cod_Moneda



GO


ALTER PROCEDURE [dbo].[usp_adm_pcs_detallemoneda_sel] 
@CodMoneda VARCHAR(3)
AS
SELECT Cod_Moneda,
Dsc_Moneda,
Dsc_Simbolo,
(SELECT Dsc_Campo FROM TUA_ListaDeCampos WHERE Cod_Campo=Tip_Estado AND Nom_Campo='EstadoRegistro') Dsc_Tip_Estado,
Log_Usuario_Mod,
Log_Fecha_Mod,
Log_Hora_Mod,
Tip_Estado,
Dsc_Nemonico
FROM TUA_MONEDA 
WHERE Cod_Moneda= @CodMoneda
ORDER BY 2






GO


ALTER PROCEDURE [dbo].[usp_adm_pcs_listarmonedas_sel]
AS
SELECT M.Cod_Moneda,
M.Dsc_Moneda,
Dsc_Simbolo,
(SELECT Dsc_Campo FROM TUA_ListaDeCampos WHERE Cod_Campo=M.Tip_Estado AND Nom_Campo='EstadoRegistro') Tip_Estado,
U.Nom_Usuario+', '+U.Ape_Usuario Log_Usuario_Mod,
M.Log_Fecha_Mod,
M.Log_Hora_Mod,
M.Dsc_Nemonico
FROM TUA_MONEDA M LEFT JOIN TUA_Usuario U
ON M.Log_Usuario_Mod=U.Cod_Usuario

ORDER BY 2






GO




ALTER PROCEDURE [dbo].[usp_adm_pcs_listarpuntoventa_sel]
AS
SELECT PV.Cod_Equipo,
(
SELECT U.Nom_Usuario+' '+U.Ape_Usuario 
FROM tua_turno TR (NOLOCK)
INNER JOIN TUA_Usuario U (NOLOCK) ON TR.Cod_Usuario=U.Cod_Usuario
WHERE TR.Fch_Fin IS NULL
AND TR.Hor_Fin IS NULL
AND TR.Cod_Usuario_Cierre IS NULL
AND TR.Cod_Equipo= PV.Cod_Equipo
GROUP BY U.Nom_Usuario,U.Ape_Usuario 
) Usuario_Logeado, (SELECT Dsc_Campo 
					FROM TUA_ListaDeCampos
					WHERE Cod_Campo=PV.Flg_Estado AND Nom_Campo='EstadoRegistro' ) Flg_Estado ,
PV.Num_Ip_Equipo, PV.Log_Fecha_Mod, PV.Log_Hora_Mod, PV.Log_Usuario_Mod, PV.Dsc_Estacion,
	CASE WHEN (U1.Ape_Usuario IS NULL AND U1.Nom_Usuario IS NULL) THEN PV.Log_Usuario_Mod
	ELSE ISNULL(U1.Nom_Usuario,' - ') +', '+ ISNULL(U1.Ape_Usuario, ' - ') END Nom_Usuario
FROM TUA_EstacionPtoVta PV 
	 LEFT JOIN TUA_Usuario U1 ON PV.Log_Usuario_Mod = U1.Cod_Usuario
WHERE PV.Num_Ip_Equipo<>'127.0.0.1'
ORDER BY 1,2




GO


--[usp_cns_pcs_cuadre_sel]'000240','SOL'
--[usp_cns_pcs_cuadre_sel]'008728','DOL'
--[usp_cns_pcs_cuadre_sel]'000240','EUR'

ALTER PROCEDURE [dbo].[usp_cns_pcs_cuadre_sel]
@Cod_Turno  CHAR(6),
@Cod_Moneda CHAR(3)
as 
declare
@Imp_Efec_Ini numeric(8,2) ,
@Can_Ticket_Int int ,
@Can_Ticket_Nac int ,
@Imp_Ticket_Int numeric(8,2) ,
@Imp_Ticket_Nac numeric(8,2) ,
@Can_Ingreso_Caja int ,
@Imp_Ingreso_Caja numeric(8,2) ,
@Can_Venta_Moneda int ,
@Imp_Venta_Moneda numeric(8,2) ,
@Can_Egreso_Caja int ,
@Imp_Egreso_Caja numeric(8,2) ,
@Can_Compra_Moneda int ,
@Imp_Compra_Moneda numeric(8,2) ,
@Imp_Efectivo_Final numeric(8,2) ,
@Can_Anul_Int int ,
@Can_Anul_Nac int ,
@Can_Infante_Int int ,
@Can_Infante_Nac int ,
@Can_Credito_Int int ,
@Can_Credito_Nac int ,
@Imp_Credito_Int numeric(8,2) ,
@Imp_Credito_Nac numeric(8,2) ,
@Imp_Monto_Actual numeric(8,2),
@Imp_Monto_Aux numeric(8,2),
@Can_Ticket_Efe_Int int ,
@Imp_Ticket_Efe_Int numeric(8,2) ,
@Can_Ticket_Tra_Int int ,
@Imp_Ticket_Tra_Int numeric(8,2) ,
@Can_Ticket_Deb_Int int ,
@Imp_Ticket_Deb_Int numeric(8,2) ,
@Can_Ticket_Cre_Int int ,
@Imp_Ticket_Cre_Int numeric(8,2) ,
@Can_Ticket_Cre_Int2 int ,
@Imp_Ticket_Cre_Int2 numeric(8,2) ,
@Can_Ticket_Che_Int int ,
@Imp_Ticket_Che_Int numeric(8,2) ,
@Can_Ticket_Efe_Nac int ,
@Imp_Ticket_Efe_Nac numeric(8,2) ,
@Can_Ticket_Tra_Nac int ,
@Imp_Ticket_Tra_Nac numeric(8,2) ,
@Can_Ticket_Deb_Nac int ,
@Imp_Ticket_Deb_Nac numeric(8,2) ,
@Can_Ticket_Cre_Nac int ,
@Imp_Ticket_Cre_Nac numeric(8,2) ,
@Can_Ticket_Cre_Nac2 int ,
@Imp_Ticket_Cre_Nac2 numeric(8,2) ,
@Can_Ticket_Che_Nac int ,
@Imp_Ticket_Che_Nac numeric(8,2),
@Imp_Recaudado_Final numeric(8,2),
@Imp_Monto_Cierre numeric(8,2)

--if(@Cod_Moneda='SOL')
--begin
	set @Imp_Monto_Actual=(select Imp_Monto_Actual from TUA_TurnoMontos (NOLOCK) where Cod_Turno=@Cod_Turno and Cod_Moneda=@Cod_Moneda and Tip_Pago='E')
--	set @Imp_Monto_Cierre=(select Imp_Monto_Final from TUA_TurnoMontos where Cod_Turno=@Cod_Turno and Cod_Moneda=@Cod_Moneda)
--end

set @Imp_Efec_Ini=(select Imp_Monto_Inicial from TUA_TurnoMontos (NOLOCK) where Cod_Turno=@Cod_Turno and Cod_Moneda=@Cod_Moneda and Tip_Pago='E')
select @Can_Ticket_Int=count(T.Cod_Numero_Ticket),@Imp_Ticket_Int=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT  where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' AND T.Tip_Estado_Actual<>'X' --AND T.Tip_Pago='E'
select @Can_Ticket_Efe_Int=count(T.Cod_Numero_Ticket),@Imp_Ticket_Efe_Int=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='E' 
select @Can_Ticket_Tra_Int=count(T.Cod_Numero_Ticket),@Imp_Ticket_Tra_Int=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='T' 
select @Can_Ticket_Deb_Int=count(T.Cod_Numero_Ticket),@Imp_Ticket_Deb_Int=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='D'
select @Can_Ticket_Cre_Int=count(T.Cod_Numero_Ticket),@Imp_Ticket_Cre_Int=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='C' 
select @Can_Ticket_Cre_Int2=count(T.Cod_Numero_Ticket),@Imp_Ticket_Cre_Int2=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' AND T.Tip_Estado_Actual<>'X' AND (T.Tip_Pago='C' OR T.Tip_Pago='X') 
select @Can_Ticket_Che_Int=count(T.Cod_Numero_Ticket),@Imp_Ticket_Che_Int=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='Q'

select @Can_Ticket_Nac=count(T.Cod_Numero_Ticket),@Imp_Ticket_Nac=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' AND T.Tip_Estado_Actual<>'X' --AND T.Tip_Pago='E'
select @Can_Ticket_Efe_Nac=count(T.Cod_Numero_Ticket),@Imp_Ticket_Efe_Nac=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='E'
select @Can_Ticket_Tra_Nac=count(T.Cod_Numero_Ticket),@Imp_Ticket_Tra_Nac=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='T'
select @Can_Ticket_Deb_Nac=count(T.Cod_Numero_Ticket),@Imp_Ticket_Deb_Nac=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='D'
select @Can_Ticket_Cre_Nac=count(T.Cod_Numero_Ticket),@Imp_Ticket_Cre_Nac=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='C' 
select @Can_Ticket_Cre_Nac2=count(T.Cod_Numero_Ticket),@Imp_Ticket_Cre_Nac2=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' AND T.Tip_Estado_Actual<>'X' AND (T.Tip_Pago='C' OR T.Tip_Pago='X') 
select @Can_Ticket_Che_Nac=count(T.Cod_Numero_Ticket),@Imp_Ticket_Che_Nac=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Moneda=@Cod_Moneda AND T.Cod_Turno=@Cod_Turno AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' AND T.Tip_Estado_Actual<>'X' AND T.Tip_Pago='Q'

select @Can_Ingreso_Caja=count(LOC.Num_Operacion),@Imp_Ingreso_Caja=sum(LOC.Imp_Operacion) from TUA_LogOperacCaja LOC (NOLOCK),TUA_LogOperacion LO (NOLOCK) where LOC.Cod_Moneda=@Cod_Moneda AND LOC.Num_Operacion=LO.Num_Operacion AND LO.Cod_Tipo_Operacion='IC' AND LO.Cod_Turno=@Cod_Turno
if(@Cod_Moneda='SOL')
select @Can_Venta_Moneda=count(LCV.Num_Operacion),@Imp_Venta_Moneda=sum(LCV.Imp_ACambiar) from TUA_LogCompraVenta LCV (NOLOCK),TUA_LogOperacion LO (NOLOCK) where LCV.Num_Operacion=LO.Num_Operacion AND LO.Cod_Turno=@Cod_Turno AND LO.Tip_Estado_Actual='E' AND LO.Cod_Tipo_Operacion='VM'
ELSE select @Can_Venta_Moneda=count(LCV.Num_Operacion),@Imp_Venta_Moneda=sum(LCV.Imp_Cambiado) from TUA_LogCompraVenta LCV (NOLOCK),TUA_LogOperacion LO (NOLOCK) where LCV.Cod_Moneda=@Cod_Moneda AND LCV.Num_Operacion=LO.Num_Operacion AND LO.Cod_Turno=@Cod_Turno AND LO.Tip_Estado_Actual='E' AND LO.Cod_Tipo_Operacion='VM'

if @Imp_Efec_Ini is null
	set @Imp_Efec_Ini=0
if @Imp_Ticket_Int is null
	set @Imp_Ticket_Int=0
if @Imp_Ticket_Nac is null
	set @Imp_Ticket_Nac=0
if @Imp_Ingreso_Caja is null
	set @Imp_Ingreso_Caja=0
if @Imp_Venta_Moneda is null
	set @Imp_Venta_Moneda=0

if @Imp_Ticket_Efe_Int is null
	set @Imp_Ticket_Efe_Int=0
if @Imp_Ticket_Tra_Int is null
	set @Imp_Ticket_Tra_Int=0
if @Imp_Ticket_Deb_Int is null
	set @Imp_Ticket_Deb_Int=0
if @Imp_Ticket_Cre_Int is null
	set @Imp_Ticket_Cre_Int=0
if @Imp_Ticket_Che_Int is null
	set @Imp_Ticket_Che_Int=0

if @Imp_Ticket_Efe_Nac is null
	set @Imp_Ticket_Efe_Nac=0
if @Imp_Ticket_Tra_Nac is null
	set @Imp_Ticket_Tra_Nac=0
if @Imp_Ticket_Deb_Nac is null
	set @Imp_Ticket_Deb_Nac=0
if @Imp_Ticket_Cre_Nac is null
	set @Imp_Ticket_Cre_Nac=0
if @Imp_Ticket_Che_Nac is null
	set @Imp_Ticket_Che_Nac=0

select @Can_Egreso_Caja=COUNT(LOC.Num_Operacion),@Imp_Egreso_Caja=sum(LOC.Imp_Operacion) from TUA_LogOperacCaja LOC (NOLOCK),TUA_LogOperacion LO (NOLOCK) where LOC.Cod_Moneda=@Cod_Moneda AND LOC.Num_Operacion=LO.Num_Operacion AND LO.Cod_Tipo_Operacion='EC' AND LO.Cod_Turno=@Cod_Turno
if(@Cod_Moneda='SOL')
select @Can_Compra_Moneda=count(LCV.Num_Operacion),@Imp_Compra_Moneda=sum(LCV.Imp_Cambiado) from TUA_LogCompraVenta LCV (NOLOCK),TUA_LogOperacion LO (NOLOCK) where LCV.Num_Operacion=LO.Num_Operacion AND LO.Cod_Turno=@Cod_Turno AND LO.Tip_Estado_Actual='E' AND LO.Cod_Tipo_Operacion='CM'
ELSE select @Can_Compra_Moneda=count(LCV.Num_Operacion),@Imp_Compra_Moneda=sum(LCV.Imp_ACambiar) from TUA_LogCompraVenta LCV (NOLOCK),TUA_LogOperacion LO (NOLOCK) where LCV.Cod_Moneda=@Cod_Moneda AND LCV.Num_Operacion=LO.Num_Operacion AND LO.Cod_Turno=@Cod_Turno AND LO.Tip_Estado_Actual='E' AND LO.Cod_Tipo_Operacion='CM'

if @Imp_Egreso_Caja is null
	set @Imp_Egreso_Caja=0
if @Imp_Compra_Moneda is null
	set @Imp_Compra_Moneda=0

set @Imp_Efectivo_Final=@Imp_Monto_Actual
--set @Imp_Recaudado_Final=@Imp_Efec_Ini+@Imp_Ticket_Int+@Imp_Ticket_Nac+@Imp_Ingreso_Caja+@Imp_Compra_Moneda-@Imp_Egreso_Caja-@Imp_Venta_Moneda
set @Imp_Recaudado_Final=(SELECT SUM(Imp_Monto_Actual) FROM TUA_TurnoMontos (NOLOCK) where Cod_Turno=@Cod_Turno and Cod_Moneda=@Cod_Moneda)--@Imp_Monto_Cierre+@Imp_Ticket_Tra_Int+@Imp_Ticket_Deb_Int+@Imp_Ticket_Cre_Int+@Imp_Ticket_Che_Int+@Imp_Ticket_Tra_Nac+@Imp_Ticket_Deb_Nac+@Imp_Ticket_Cre_Nac+@Imp_Ticket_Che_Nac

if(@Cod_Moneda='SOL')
begin
	set @Imp_Monto_Aux=@Imp_Efec_Ini+@Imp_Ticket_Int+@Imp_Ticket_Nac+@Imp_Ingreso_Caja+@Imp_Venta_Moneda-@Imp_Compra_Moneda-@Imp_Egreso_Caja

--	set @Imp_Compra_Moneda=@Imp_Monto_Aux-@Imp_Monto_Actual
--	set @Imp_Recaudado_Final=@Imp_Monto_Aux
end



select @Can_Anul_Int=count(T.Cod_Numero_Ticket) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Turno=@Cod_Turno and T.Tip_Estado_Actual='X' AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' and T.Cod_Moneda=@Cod_Moneda
select @Can_Anul_Nac=count(T.Cod_Numero_Ticket) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Turno=@Cod_Turno and T.Tip_Estado_Actual='X' AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' and T.Cod_Moneda=@Cod_Moneda
--
select @Can_Infante_Int=count(T.Cod_Numero_Ticket) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Turno=@Cod_Turno and T.Tip_Estado_Actual<>'X' AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Pasajero='I' AND TT.Tip_Vuelo='I' and T.Cod_Moneda=@Cod_Moneda --and T.Tip_Pago='E'
select @Can_Infante_Nac=count(T.Cod_Numero_Ticket) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Turno=@Cod_Turno and T.Tip_Estado_Actual<>'X' AND TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Pasajero='I' AND TT.Tip_Vuelo='N' and T.Cod_Moneda=@Cod_Moneda --and T.Tip_Pago='E'
--
select @Can_Credito_Int=count(T.Cod_Numero_Ticket),@Imp_Credito_Int=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Turno=@Cod_Turno and TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='I' AND T.Cod_Modalidad_Venta='M0004'
select @Can_Credito_Nac=count(T.Cod_Numero_Ticket),@Imp_Credito_Nac=sum(T.Imp_Precio) from TUA_Ticket T (NOLOCK),TUA_TipoTicket TT where T.Cod_Turno=@Cod_Turno and TT.Cod_Tipo_Ticket=T.Cod_Tipo_Ticket AND TT.Tip_Vuelo='N' AND T.Cod_Modalidad_Venta='M0004'
if @Imp_Credito_Int is null
	set @Imp_Credito_Int=0
if @Imp_Credito_Nac is null
	set @Imp_Credito_Nac=0

select @Imp_Efec_Ini,@Can_Ticket_Int,@Can_Ticket_Nac,@Imp_Ticket_Int,@Imp_Ticket_Nac,@Can_Ingreso_Caja,@Imp_Ingreso_Caja,@Can_Venta_Moneda,@Imp_Venta_Moneda as 'venta moneda',@Can_Egreso_Caja,@Imp_Egreso_Caja,@Can_Compra_Moneda,@Imp_Compra_Moneda as 'compra moneda',@Imp_Efectivo_Final as 'efectivo final',@Can_Anul_Int,@Can_Anul_Nac,@Can_Infante_Int,@Can_Infante_Nac,@Can_Credito_Int,@Can_Credito_Nac,@Imp_Credito_Int,@Imp_Credito_Nac,
	@Can_Ticket_Efe_Int,@Imp_Ticket_Efe_Int,@Can_Ticket_Tra_Int,@Imp_Ticket_Tra_Int,@Can_Ticket_Deb_Int,@Imp_Ticket_Deb_Int,@Can_Ticket_Cre_Int,@Imp_Ticket_Cre_Int,@Can_Ticket_Che_Int,@Imp_Ticket_Che_Int,
	@Can_Ticket_Efe_Nac,@Imp_Ticket_Efe_Nac,@Can_Ticket_Tra_Nac,@Imp_Ticket_Tra_Nac,@Can_Ticket_Deb_Nac,@Imp_Ticket_Deb_Nac,@Can_Ticket_Cre_Nac,@Imp_Ticket_Cre_Nac,@Can_Ticket_Che_Nac,@Imp_Ticket_Che_Nac,
	@Imp_Recaudado_Final, @Can_Ticket_Cre_Int2, @Imp_Ticket_Cre_Int2, @Can_Ticket_Cre_Nac2, @Imp_Ticket_Cre_Nac2














GO



--[usp_cns_pcs_resumencompraventa_sel] '3','20101006','','20101006','',null,null
-- ============================================================
-- Author:		KINZI
-- ALTER date: 07/12/2009
-- Description:	RESUMEN DE OPERACIONES DE COMPRA VENTA MONEDA
--              POR RANGO FECHA
-- ============================================================
ALTER PROCEDURE [dbo].[usp_cns_pcs_resumencompraventa_sel](
	@Flag_Resumen NVARCHAR(1),	
	@Fecha_Desde NVARCHAR(8)=NULL,
	@Hora_Desde NVARCHAR(6)=NULL,
	@Fecha_Hasta NVARCHAR(8)=NULL,
	@Hora_Hasta NVARCHAR(6)=NULL,
	@Turno_Desde NVARCHAR(6)=NULL,
	@Turno_Hasta NVARCHAR(6)=NULL
)
AS
BEGIN
	
	IF (@Hora_Desde = '' AND Len(@Hora_Hasta) > 0 )
		SET @Hora_Desde = '000000'
	IF (Len(@Hora_Desde) > 0 AND @Hora_Hasta = '')
		SET @Hora_Hasta = '235959'
	IF (@Hora_Desde = '' AND @Hora_Hasta = '')
	BEGIN
		SET @Hora_Desde = '000000'
		SET @Hora_Hasta = '235959'
	END

	--PARA RESUMEN DIARIO QUE DICEN QUE NO CUADRA
	IF(@Flag_Resumen='3')
	BEGIN
		IF(@Fecha_Desde IS NOT NULL)
		begin
			SELECT TM.Cod_Moneda,SUM(TM.Imp_Monto_Actual) FROM TUA_TurnoMontos TM (NOLOCK),TUA_Turno T (NOLOCK) WHERE T.Cod_Turno=TM.Cod_Turno 
			AND T.Fch_Inicio>=@Fecha_Desde AND T.Fch_Inicio<=@Fecha_Hasta AND T.Fch_Fin IS NOT NULL AND T.Fch_Fin<=@Fecha_Hasta 
			AND T.Fch_Inicio+T.Hor_Inicio>=@Fecha_Desde+@Hora_Desde AND T.Fch_Inicio+T.Hor_Inicio<=@Fecha_Hasta+@Hora_Hasta AND T.Hor_Fin IS NOT NULL AND T.Fch_Fin+T.Hor_Fin<=@Fecha_Hasta+@Hora_Hasta
			AND TM.Tip_Pago='E'
			GROUP BY TM.Cod_Moneda
--		IF(@Fecha_Desde IS NULL)
--		SELECT TM.Cod_Moneda,SUM(TM.Imp_Monto_Actual) FROM TUA_TurnoMontos TM,TUA_Turno T WHERE T.Cod_Turno=TM.Cod_Turno 
--		AND T.Cod_Turno>=@Turno_Desde AND T.Cod_Turno<=@Turno_Hasta --AND T.Fch_Fin IS NOT NULL AND T.Fch_Fin<=@Fecha_Hasta
--		GROUP BY TM.Cod_Moneda
			RETURN
		end
		else begin
			SELECT TM.Cod_Moneda,SUM(TM.Imp_Monto_Actual) FROM TUA_TurnoMontos TM (NOLOCK),TUA_Turno T (NOLOCK) WHERE T.Cod_Turno=TM.Cod_Turno 
			AND T.Cod_Turno>=@Turno_Desde and T.Cod_Turno<=@Turno_Hasta and TM.Tip_Pago='E'
			GROUP BY TM.Cod_Moneda
			RETURN
		end
	END
	--FIN REMESAS

	-- CONSULTA POR RANGO DE FECHA
	IF (@Flag_Resumen = '1' or @Flag_Resumen = '3')
	BEGIN
		SELECT AA.Cod_Tipo_Operacion, AA.Cod_Moneda, ISNULL(BB.Num_Operacion,0) Num_Operacion, 
				ISNULL(BB.Monto_Moneda_Extranjera,0) Monto_Moneda_Extranjera, 
				ISNULL(BB.Monto_Moneda_Soles,0) Monto_Moneda_Soles, AA.Dsc_Moneda Dsc_Moneda_Extranjera
		FROM
		(SELECT M.Cod_Moneda, L.Cod_Tipo_Operacion, M.Dsc_Moneda 
		FROM TUA_Moneda M (NOLOCK), TUA_TipoOperacion L (NOLOCK)
		where L.Cod_Tipo_Operacion in ('CM','VM') and M.Cod_Moneda <> 'SOL'
		) AA LEFT JOIN
		
		(SELECT B.Cod_Tipo_Operacion, A.Cod_Moneda, COUNT(A.Cod_Moneda) Num_Operacion, 
			   SUM(Imp_ACambiar) Monto_Moneda_Extranjera, 
			   SUM(Imp_Cambiado) Monto_Moneda_Soles, C.Dsc_Moneda Dsc_Moneda_Extranjera
	    FROM TUA_LogCompraVenta A (NOLOCK) LEFT JOIN TUA_Moneda C (NOLOCK) ON A.Cod_Moneda = C.Cod_Moneda,
			(SELECT tlo.Num_Operacion, tlo.Cod_Tipo_Operacion FROM TUA_LogOperacion tlo (NOLOCK)
	         WHERE 
--				tlo.Fch_Proceso >= CONVERT( datetime,SUBSTRING(@Fecha_Desde,1,4 )+'-'+
--												SUBSTRING(@Fecha_Desde,5,2 )+'-'+
--												SUBSTRING(@Fecha_Desde,7,2 ) +' '+ 
--												SUBSTRING(@Hora_Desde,1,2 )+':'+
--												SUBSTRING(@Hora_Desde,3,2 )+':'+
--												SUBSTRING(@Hora_Desde,5,2 ),20) 
--			   AND tlo.Fch_Proceso <= CONVERT( datetime,SUBSTRING(@Fecha_Hasta,1,4 )+'-'+
--												SUBSTRING(@Fecha_Hasta,5,2 )+'-'+
--												SUBSTRING(@Fecha_Hasta,7,2 ) +' '+ 
--												SUBSTRING(@Hora_Hasta,1,2 )+':'+
--												SUBSTRING(@Hora_Hasta,3,2 )+':'+
--												SUBSTRING(@Hora_Hasta,5,2 ),20)
--			   AND 
				tlo.Tip_Estado_Actual = 'E' AND tlo.Cod_Turno in (select cod_turno from tua_turno (NOLOCK) where Fch_Inicio>=@Fecha_Desde AND Fch_Inicio<=@Fecha_Hasta AND Fch_Fin IS NOT NULL AND Fch_Fin<=@Fecha_Hasta 
			AND Hor_Inicio>=@Hora_Desde AND Hor_Inicio<=@Hora_Hasta AND Hor_Fin IS NOT NULL AND Hor_Fin<=@Hora_Hasta)) B
		WHERE A.Num_Operacion = B.Num_Operacion
		GROUP BY B.Cod_Tipo_Operacion, A.Cod_Moneda, C.Dsc_Moneda) BB 
		
		ON AA.Cod_Moneda = BB.Cod_Moneda and AA.Cod_Tipo_Operacion = BB.Cod_Tipo_Operacion 
		ORDER BY AA.Cod_Tipo_Operacion, AA.Cod_Moneda
	END
	ELSE
	BEGIN
		IF (@Flag_Resumen = '2')
		BEGIN
			SELECT AA.Cod_Tipo_Operacion, AA.Cod_Moneda, ISNULL(BB.Num_Operacion,0) Num_Operacion, 
				ISNULL(BB.Monto_Moneda_Extranjera,0) Monto_Moneda_Extranjera, 
				ISNULL(BB.Monto_Moneda_Soles,0) Monto_Moneda_Soles, AA.Dsc_Moneda Dsc_Moneda_Extranjera
			FROM
			(SELECT M.Cod_Moneda, L.Cod_Tipo_Operacion, M.Dsc_Moneda 
			FROM TUA_Moneda M (NOLOCK), TUA_TipoOperacion L (NOLOCK)
			where L.Cod_Tipo_Operacion in ('CM','VM') and M.Cod_Moneda <> 'SOL'
			) AA LEFT JOIN			
			(SELECT B.Cod_Tipo_Operacion, A.Cod_Moneda, COUNT(A.Cod_Moneda) Num_Operacion, 
				SUM(Imp_ACambiar) Monto_Moneda_Extranjera, 
				SUM(Imp_Cambiado) Monto_Moneda_Soles, C.Dsc_Moneda Dsc_Moneda_Extranjera
			FROM TUA_LogCompraVenta A (NOLOCK) LEFT JOIN TUA_Moneda C (NOLOCK) ON A.Cod_Moneda = C.Cod_Moneda,
				(SELECT tlo.Num_Operacion, tlo.Cod_Tipo_Operacion FROM TUA_LogOperacion tlo (NOLOCK)
				 WHERE tlo.Cod_Turno >= @Turno_Desde 
				   AND tlo.Cod_Turno <= @Turno_Hasta
				   AND tlo.Tip_Estado_Actual = 'E') B
			WHERE A.Num_Operacion = B.Num_Operacion
			GROUP BY B.Cod_Tipo_Operacion, A.Cod_Moneda, C.Dsc_Moneda ) BB
			
			ON AA.Cod_Moneda = BB.Cod_Moneda and AA.Cod_Tipo_Operacion = BB.Cod_Tipo_Operacion 
			ORDER BY AA.Cod_Tipo_Operacion, AA.Cod_Moneda
		END
	END
END
--usp_cns_pcs_resumencompraventa_sel '1','20091205','','20091205','230000','',''
--usp_cns_pcs_resumencompraventa_sel '2','','','','','000047','000048'
--usp_cns_pcs_resumencompraventa_sel '3','20091205','','20091205','','',''






GO


--usp_ext_cns_validarcompania_bcbp_sel 'LP'
ALTER PROCEDURE [dbo].[usp_ext_cns_validarcompania_bcbp_sel]
(
	@Cod_IATA varchar(5)
)
AS
BEGIN
	SELECT CV.*,Nom_Modalidad
	FROM (	SELECT C.Cod_Compania,Cod_Modalidad_Venta,Dsc_Compania,Cod_IATA 
			FROM TUA_Compania C, TUA_ModVentaComp MC
			WHERE C.Cod_Compania = MC.Cod_Compania AND Cod_IATA=@Cod_IATA and C.Tip_Estado='1'
		  ) CV, TUA_ModalidadVenta MV 
	WHERE CV.Cod_Modalidad_Venta=MV.Cod_Modalidad_Venta AND Nom_Modalidad='BCBP' 
END
GO


ALTER PROCEDURE [dbo].[usp_ope_cns_turnoabierto_sel]
AS
SELECT T.Cod_Turno,T.Cod_Usuario,U.Nom_Usuario,U.Ape_Usuario,T.Fch_Inicio,T.Hor_Inicio,E.Num_Ip_Equipo FROM TUA_Turno T,TUA_Usuario U,TUA_EstacionPtoVta E WHERE T.Fch_Fin IS NULL AND T.Cod_Usuario_Cierre IS NULL 
AND E.Cod_Equipo=T.Cod_Equipo AND T.Cod_Usuario=U.Cod_Usuario
GO




--[usp_ope_pcs_archventa_sel] '20110104','20110104','BRD','0'
--[usp_ope_pcs_archventa_sel] '20110105','20110105','BRD','0'
--[usp_ope_pcs_archventa_sel] '20110104','20110105','BRD','0'



--[usp_ope_pcs_archventa_sel] '20101201','20101231','CON','0'
ALTER PROCEDURE [dbo].[usp_ope_pcs_archventa_sel](
@Fch_Inicio char(8),
@Fch_Fin char(8),
@Tip_Venta char(3),
@Flg_Aerolinea char(1)
)
as
declare @campo1 varchar(20),
@secuencia int,
@orga varchar(20),
@canal varchar(20),
@sector varchar(20),
@cliente varchar(20),
@ruc varchar(20),
@clasedoc varchar(20),
@fecha char(8),
@tipdoc varchar(20),
@nroserie varchar(20),
@nrodoc varchar(20),
@maxSec int,
@nomCliente varchar(30),
@IGV numeric(8,4),
@estado varchar(20),
@clientePago varchar(40)


set @IGV=(SELECT CONVERT(DECIMAL(18, 2),Valor) FROM TUA_ParameGeneral (NOLOCK) WHERE Identificador='IGV')
set @maxSec=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='MSAV')
set @secuencia=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='SAAV')

if(@secuencia>=@maxSec)
set @secuencia=1
else set @secuencia=@secuencia+1

--update TUA_ParameGeneral set Valor=@secuencia where Identificador='SAAV'

set @campo1=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A100')
set @orga=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A120')
--set @secuencia=(select Valor from TUA_ParameGeneral where Identificador='SCAV')
set @canal=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A125')
set @sector=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A130')
set @cliente=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A135')
set @ruc=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A145')
set @clasedoc=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A150')
set @fecha=Substring([dbo].NTPFunction(),1,8)
set @tipdoc=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A160')
set @nroserie=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A165')
set @nrodoc=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A170')
set @estado=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A215')
set @clientePago=(select Valor from TUA_ParameGeneral (NOLOCK) where Identificador='A315')

if(@Tip_Venta='' and @Flg_Aerolinea='' and @Fch_Inicio is not null)
begin
	-- @Fch_Inicio contiene el valor secuencial para guardar en parametros.
	update TUA_ParameGeneral set Valor=@Fch_Inicio where Identificador='SAAV'
	select '',''
	return 0
end

if(@Tip_Venta='CON')
BEGIN
	set @nomCliente='CONTADO'
	IF(@Flg_Aerolinea='0')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',@clientePago,'','',''
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE  T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin and Fch_Vencimiento<>'' and (T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' OR (T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
			   AND T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial'  AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo
		ORDER BY PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
--		UNION		
--
--		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,@fecha,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),-CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,'',T.Cod_Moneda,@estado,'','','',''
--		from TUA_ListaDeCampos PG,TUA_TipoTicket TT,TUA_Compania C,TUA_TICKET T  INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
--		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' OR (T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND T.Tip_Estado_Actual='X' AND T.Tip_Anulacion='2'
--		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin
--		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,T.Cod_Moneda,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo	

	END ELSE IF(@Flg_Aerolinea='1')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente+' - '+C.Dsc_Compania,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),(T.Imp_Precio/@IGV)),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',C.Cod_SAP,'','',T.Flg_Cobro,C.Cod_Compania
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Cod_Modalidad_Venta='M0001' OR T.Cod_Modalidad_Venta='M0003' OR (T.Cod_Modalidad_Venta IS NULL AND T.Flg_Contingencia='1')) AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin and Fch_Vencimiento<>'' AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,C.Dsc_Compania,T.Flg_Cobro,C.Cod_IATA,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania

	END
END 
ELSE IF(@Tip_Venta='CRE')BEGIN
	set @nomCliente='CREDITO EMISION'
	IF(@Flg_Aerolinea='0')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',@clientePago,'','',''
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Cod_Modalidad_Venta='M0004') AND  T.Flg_Cobro='0' AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin  AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
	END ELSE IF(@Flg_Aerolinea='1')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente+' - '+C.Dsc_Compania,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',C.Cod_SAP,'','',T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_Compania
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Cod_Modalidad_Venta='M0004') AND  T.Flg_Cobro='0' AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,C.Dsc_Compania,T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_IATA,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
	END
END
ELSE IF(@Tip_Venta='CRU') BEGIN
	set @nomCliente='CREDITO USO'
	IF(@Flg_Aerolinea='0')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',@clientePago,'','',''
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Cod_Modalidad_Venta='M0004' AND  T.Flg_Cobro='1'
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin and T.Fch_Uso IS NOT NULL AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION

	END ELSE IF(@Flg_Aerolinea='1')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente+' - '+C.Dsc_Compania,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',C.Cod_SAP,'','',T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_Compania
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Cod_Modalidad_Venta='M0004' AND  T.Flg_Cobro='1' 
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin and T.Fch_Uso IS NOT NULL AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,C.Dsc_Compania,T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_IATA,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania
		ORDER BY M.Dsc_Nemonico, PG.Dsc_Campo DESC, T.Fch_Creacion -- PARCHE ORDENACION
	END
END ELSE IF(@Tip_Venta='ATM')BEGIN
	set @nomCliente='CREDITO ATM'
	IF(@Flg_Aerolinea='0')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',@clientePago,'','',''
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Cod_Modalidad_Venta='M0005' AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin  AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo

	END ELSE IF(@Flg_Aerolinea='1')BEGIN
		select  @campo1,anho+mes,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA,@secuencia,@orga,@canal,@sector,@cliente,@nomCliente+' - '+C.Dsc_Compania,@ruc as RUC,@clasedoc,T.Fch_Creacion,@tipdoc,@nroserie,@nrodoc,PG.Cod_Relativo,PG.Dsc_Campo,CONVERT(DECIMAL(18, 2),COUNT(T.Cod_Numero_Ticket)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV),CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),T.Fch_Creacion,CONVERT(DECIMAL(18, 6),COUNT(T.Cod_Numero_Ticket)*ROUND(T.Imp_Precio/@IGV,6)*@IGV),M.Dsc_Nemonico,@estado,'',C.Cod_SAP,'','',T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_Compania
		from TUA_ListaDeCampos PG (NOLOCK),TUA_TipoTicket TT (NOLOCK),TUA_Compania C (NOLOCK),TUA_Moneda M (NOLOCK),TUA_TICKET T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket AND C.Cod_Compania=T.Cod_Compania AND TT.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Cod_Modalidad_Venta='M0005' AND (Tip_Estado_Actual<>'X' OR (Tip_Estado_Actual='X' AND Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin  AND M.Cod_Moneda=T.Cod_Moneda
		GROUP BY anho,mes,T.Fch_Creacion,TT.Tip_Vuelo,M.Dsc_Nemonico,C.Dsc_Compania,T.Cod_Modalidad_Venta,T.Flg_Cobro,C.Cod_IATA,T.Imp_Precio,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania
	END
END ELSE IF(@Tip_Venta='BRD')BEGIN
	set @nomCliente='BOARDING'
	IF(@Flg_Aerolinea='0')BEGIN
	  select sociedad,periodo,nroDocUser,secuencia,orga,canal,sector,cliente,nomCliente,RUC,claseDoc,fecha,tipoDoc,nroSerie,nroDoc,servicio,nomServicio,sum(Can_Ticket),impUnitario,sum(impTotal),fecFactura,sum(impSaldo),moneda,estado,obs,clientePago,filler1,filler2,cobro
	  from(
		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+T.Fch_Creacion nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente nomCliente,@ruc as RUC,@clasedoc claseDoc,T.Fch_Creacion fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,T.Fch_Creacion fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,@clientePago clientePago,'' filler1,'' filler2,'1' cobro
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Tip_Estado<>'X' OR (T.Tip_Estado='X' AND T.Tip_Anulacion='3'))
		 and T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin  AND T.Num_Secuencial_Bcbp_Rel=0
		GROUP BY anho,mes,T.Fch_Creacion,T.Tip_Vuelo,PG.Cod_Relativo,PG.Dsc_Campo,T.Imp_Precio,T.Cod_Moneda

		UNION ALL
		
		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+SUBSTRING(T.Fch_Rehabilitacion,1,8) nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente nomCliente,@ruc as RUC,'ZCR' claseDoc,SUBSTRING(T.Fch_Rehabilitacion,1,8) fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,SUBSTRING(T.Fch_Rehabilitacion,1,8) fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,@clientePago clientePago,'' filler1,'' filler2,'2' cobro
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND T.Fch_Rehabilitacion IS NOT NULL
		 and SUBSTRING(T.Fch_Rehabilitacion,1,8)>=@Fch_Inicio and SUBSTRING(T.Fch_Rehabilitacion,1,8)<=@Fch_Fin  AND T.Num_Secuencial_Bcbp_Rel=0
		GROUP BY anho,mes,T.Fch_Rehabilitacion,T.Tip_Vuelo,PG.Cod_Relativo,PG.Dsc_Campo,T.Imp_Precio,T.Cod_Moneda

		
		UNION ALL
		
		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+T.Log_Fecha_Mod nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente nomCliente,@ruc as RUC,'ZCR' claseDoc,T.Log_Fecha_Mod fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,T.Log_Fecha_Mod fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,@clientePago clientePago,'' filler1,'' filler2,'2' cobro
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Tip_Estado='X' AND T.Tip_Anulacion='1' AND T.Log_Fecha_Mod>=@Fch_Inicio and T.Log_Fecha_Mod<=@Fch_Fin  AND T.Num_Secuencial_Bcbp_Rel=0 and  C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial'  
		GROUP BY anho,mes,T.Log_Fecha_Mod,T.Tip_Vuelo,PG.Cod_Relativo,PG.Dsc_Campo,T.Imp_Precio,T.Cod_Moneda
	    )tabla group by sociedad,periodo,nroDocUser,secuencia,orga,canal,sector,cliente,nomCliente,RUC,claseDoc,fecha,tipoDoc,nroSerie,nroDoc,servicio,nomServicio,impUnitario,fecFactura,moneda,estado,obs,clientePago,filler1,filler2,cobro
	
	END ELSE IF(@Flg_Aerolinea='1')BEGIN
	 select sociedad,periodo,nroDocUser,secuencia,orga,canal,sector,cliente,nomCliente,RUC,claseDoc,fecha,tipoDoc,nroSerie,nroDoc,servicio,nomServicio,sum(Can_Ticket),impUnitario,sum(impTotal),fecFactura,sum(impSaldo),moneda,estado,obs,clientePago,filler1,filler2,modalidad,cobro,Cod_Compania
	 from(
		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+T.Fch_Creacion+C.Cod_IATA nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente+' - '+C.Dsc_Compania nomCliente,@ruc as RUC,@clasedoc claseDoc,T.Fch_Creacion fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,T.Fch_Creacion fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,C.Cod_SAP clientePago,'' filler1,'' filler2,'M0002' modalidad,'1' cobro,C.Cod_Compania 
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON T.Fch_Creacion LIKE ( anho + mes + '%') 
		WHERE T.Fch_Creacion>=@Fch_Inicio and T.Fch_Creacion<=@Fch_Fin 
			and C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial' AND (T.Tip_Estado<>'X' OR (T.Tip_Estado='X' AND T.Tip_Anulacion='3'))
		GROUP BY anho,mes,T.Fch_Creacion,T.Tip_Vuelo,C.Dsc_Compania,C.Cod_IATA,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania,T.Imp_Precio,T.Cod_Moneda
	
		UNION ALL

		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+SUBSTRING(T.Fch_Rehabilitacion,1,8)+C.Cod_IATA nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente+' - '+C.Dsc_Compania nomCliente,@ruc as RUC,'ZCR' claseDoc,SUBSTRING(T.Fch_Rehabilitacion,1,8) fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,SUBSTRING(T.Fch_Rehabilitacion,1,8) fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,C.Cod_SAP clientePago,'' filler1,'' filler2,'M0002' modalidad,'2' cobro,C.Cod_Compania 
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON SUBSTRING(T.Fch_Rehabilitacion,1,8) LIKE ( anho + mes + '%') 
		WHERE SUBSTRING(T.Fch_Rehabilitacion,1,8)>=@Fch_Inicio and SUBSTRING(T.Fch_Rehabilitacion,1,8)<=@Fch_Fin AND T.Fch_Rehabilitacion IS NOT NULL
			  AND C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial'
		GROUP BY anho,mes,T.Fch_Rehabilitacion,T.Tip_Vuelo,C.Dsc_Compania,C.Cod_IATA,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania,T.Imp_Precio,T.Cod_Moneda

		UNION ALL

		select  @campo1 sociedad,anho+mes periodo,@Tip_Venta+T.Log_Fecha_Mod+C.Cod_IATA nroDocUser,@secuencia secuencia,@orga orga,@canal canal,@sector sector,@cliente cliente,@nomCliente+' - '+C.Dsc_Compania nomCliente,@ruc as RUC,'ZCR' claseDoc,T.Log_Fecha_Mod fecha,@tipdoc tipoDoc,@nroserie nroSerie,@nrodoc nroDoc,PG.Cod_Relativo servicio,PG.Dsc_Campo nomServicio,CONVERT(DECIMAL(18, 2),COUNT(T.Num_Secuencial_Bcbp)) as Can_Ticket,CONVERT(DECIMAL(18, 6),T.Imp_Precio/@IGV) impUnitario,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impTotal,T.Log_Fecha_Mod fecFactura,CONVERT(DECIMAL(18, 6),COUNT(T.Num_Secuencial_Bcbp)*ROUND(T.Imp_Precio/@IGV,6)*@IGV) impSaldo,T.Cod_Moneda moneda,@estado estado,'' obs,C.Cod_SAP clientePago,'' filler1,'' filler2,'M0002' modalidad,'2' cobro,C.Cod_Compania 
		from TUA_ListaDeCampos PG (NOLOCK),TUA_Compania C (NOLOCK),TUA_BoardingBcbp T (NOLOCK) INNER JOIN FECHAS(@Fch_Inicio, @Fch_Fin) ON SUBSTRING(T.Fch_Rehabilitacion,1,8) LIKE ( anho + mes + '%') 
		WHERE T.Log_Fecha_Mod>=@Fch_Inicio and T.Log_Fecha_Mod<=@Fch_Fin AND T.Fch_Rehabilitacion IS NOT NULL and T.Tip_Estado='X' AND T.Tip_Anulacion='1'
			  AND C.Cod_Compania=T.Cod_Compania AND T.Tip_Vuelo=PG.Cod_Campo AND PG.Nom_Campo='NroMaterial'
		GROUP BY anho,mes,T.Log_Fecha_Mod,T.Tip_Vuelo,C.Dsc_Compania,C.Cod_IATA,PG.Cod_Relativo,PG.Dsc_Campo,C.Cod_SAP,C.Cod_Compania,T.Imp_Precio,T.Cod_Moneda
		)tabla group by sociedad,periodo,nroDocUser,secuencia,orga,canal,sector,cliente,nomCliente,RUC,claseDoc,fecha,tipoDoc,nroSerie,nroDoc,servicio,nomServicio,impUnitario,fecFactura,moneda,estado,obs,clientePago,filler1,filler2,modalidad,cobro,Cod_Compania
	END
END











GO



ALTER PROCEDURE [dbo].[usp_reh_pcs_boardingbcbp_sel] 
(
	@Cod_Compania varchar(10) =NULL,
    @Fch_Vuelo_Ini char(8)=NULL,
    @Fch_Vuelo_Fin char(8)=NULL,
    @Nom_Pasajero varchar(50)=NULL,
    @Num_Vuelo varchar(10)=NULL,
    @Num_Asiento varchar(10) =NULL
)
AS
BEGIN
SELECT  B.Num_Secuencial_Bcbp,
        C.Dsc_Compania,
        B.Fch_Vuelo,
        B.Num_Vuelo,
        B.Num_Asiento,
        B.Nom_Pasajero,
        B.Tip_Estado
FROM TUA_BoardingBcbp B,TUA_Compania C where B.Cod_Compania LIKE '%'+ rtrim(@Cod_Compania)+'%' and
                            B.Fch_Vuelo >= @Fch_Vuelo_Ini and 
                            B.Fch_Vuelo <= @Fch_Vuelo_Fin and
                            B.Nom_Pasajero LIKE '%'+ rtrim (@Nom_Pasajero) +'%' and
                            B.Num_Vuelo LIKE '%'+ rtrim(@Num_Vuelo) +'%' and
                            B.Num_Asiento LIKE '%' + rtrim(@Num_Asiento) +'%'  and
                            B.Cod_Compania=C.Cod_Compania

END


GO

ALTER PROCEDURE [dbo].[usp_reh_pcs_obtenerlistadecampo_xnombre_orderbyDesc_sel] --'EstadoUsuario'
(
	@Nom_Campo varchar(50)
)

AS

SET NOCOUNT ON

SELECT [Nom_Campo],
	[Cod_Campo],
	[Cod_Relativo],
	[Dsc_Campo]
FROM [TUA_ListaDeCampos]
WHERE [Nom_Campo] = @Nom_Campo
ORDER BY 4





GO


ALTER PROCEDURE [dbo].[usp_rpt_cns_contiusados_sel] (  
@Fch_Inicio char(8),  
@Fch_Fin char(8)  
)  
AS  
SELECT Fch_Resumen,LV.Cod_Tipo_Ticket+'('+TT.Dsc_Tipo_Ticket+')' as Dsc_Tipo_Ticket,coalesce(LV.Num_Total_Usado,0) Num_Total_Usado 
FROM TUA_RES_Liquidacion LV,TUA_TipoTicket TT 
WHERE Cod_Modalidad='CONTI' and LV.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket and 
LV.Fch_Resumen>=@Fch_Inicio and LV.Fch_Resumen<=@Fch_Fin

GO




--[usp_rpt_cns_contiuso_sel] '20101021','20101021'
ALTER PROCEDURE [dbo].[usp_rpt_cns_contiuso_sel] (
@Fch_Inicio char(8),
@Fch_Fin char(8)
)
AS
SELECT Fch_Resumen,LV.Cod_Tipo_Ticket+'('+TT.Dsc_Tipo_Ticket+')' as Dsc_Tipo_Ticket,LV.Num_Total_Vendido Num_Total
FROM TUA_RES_Liquidacion LV,TUA_TipoTicket TT WHERE Cod_Modalidad='CONTI' and LV.Cod_Tipo_Ticket=TT.Cod_Tipo_Ticket and LV.Fch_Resumen>=@Fch_Inicio and LV.Fch_Resumen<=@Fch_Fin
	AND LV.Num_Total_Vendido > 0
--group by LV.Fch_Resumen,LV.Cod_Tipo_Ticket,TT.Dsc_Tipo_Ticket


GO




/*----------------------------------------------------------------------------------------------------------------------------------------
PROCEDURE : Muestra el detalle de los TICKET y/o Boarding Pass usados pertenecientes a vuelos con fecha programada en un rango específico
			Para la seleccion de los datos utiliza:
				Tickets : usa la Fecha de Uso
				Boarding: usa la Fecha de Vuelo
			IMPORTANTE: considerar que la Fecha de Uso del TUUA es menor a la Fecha de Prog/Real del Vuelo por 3-5 horas aprox.

AUTHOR    : Rosario Salirrosas E.
DATE      : 14-Marzo-2016
USE       : EXEC usp_rpt_cns_ticketsboarding_usados_resumen '%', '20160314', '20160314', 'trafico', 'compañia', 'cod_vuelo'
			EXEC usp_rpt_cns_ticketsboarding_usados_resumen '%', '20160314', '20160314', '%', '%', '%'

REVIEWS   :
@001 05.04.2016 [RSE] Muestra los tickets con fecha de uso UN dia ANTERIOR a la fecha desde indicada
@002 18.10.2017 [RSE] Muestra los boardings con fecha de uso UN dia ANTERIOR a la fecha desde indicada
@003 24.05.2018 [RSE] Se agrega como agrupador el Tráfico (TipVuelo) para Tickets y Boardings - HD 165515
@004 24.07.2018 [RSE] Se agrupan los boardings por FechaProgramada (Vuelo), Fecha y Hora de Uso
@005 01.10.2018 [RSE] Se valida que cuando la fecha de uso del BOARDING sea mayor a la  fecha de uso, tome la fecha de uso como fecha programada HD: 158920. 
                      Además se agrupa los datos por fecha de Uso, Fecha Programada, Compañía, Vuelo, etc
------------------------------------------------------------------------------------------------------------------------------------------*/
ALTER PROCEDURE [dbo].[usp_rpt_cns_ticketsboarding_usados_resumen]
( @as_tip_documento_tuua	char(1)		= '%',				-- (B)oarding Pass / (T)icket / (%) Ambos  
  @as_fch_desde				char(8)		= NULL,				-- Fecha Desde. Formato yyyymmdd
  @as_fch_hasta				char(8)		= NULL,      		-- Fecha Hasta. Formato yyyymmdd  
  @as_tip_trafico			char(1)		= '%',				-- (I)nternacional / (N)acional / (%) Ambos
  @as_cod_compania			varchar(10)	= '%',				-- Código Aerolínea
  @as_cod_vuelo				varchar(10)	= '%'				-- Código de Vuelo
)
AS

--If len(@as_fch_desde) = 6 
--	select @as_fch_desde += '01'	

--If len(@as_fch_hasta) = 6 
--	select @as_fch_hasta = convert(varchar(8),eomonth(@as_fch_hasta+'01'),112)

If @as_fch_desde = '00000000' 
	select @as_fch_desde = convert(nvarchar(6), dateadd(dd,-1,getdate()),112) + '01'	

If @as_fch_hasta = '00000000' 
	select @as_fch_hasta = convert(nvarchar(8), dateadd(dd,-1,getdate()),112) 		

SET @as_fch_desde = ISNULL(@as_fch_desde, convert(char(8),GETDATE(),112))
SET @as_fch_hasta = ISNULL(@as_fch_hasta, convert(char(8),GETDATE(),112))

---------------------------------------------------------------------------------------------------------------------
-- Ticket: Le resta un día a la fecha desde para sacar los tickest usados un día antes a la fecha de inicio indicada
---------------------------------------------------------------------------------------------------------------------
BEGIN
	
	SELECT  
		left(tic.fch_uso,8)			as fch_uso,			/* Fecha de USO del Ticket */
		SUBSTRING(tic.fch_uso,9,2)	as hra_uso,			/* Hora de Uso del Ticket */								
		''							as fch_vuelo_prog,	/* Fecha Programada Vuelo */
		''							as hra_vuelo_prog,	/* Hora Programada Vuelo */
		Max(tipt.tip_vuelo)			as tip_trafico, 
		Max('T')					as cod_clase_documento,
		Max(cia.cod_iata)			as cod_iata,
		Max(cia.cod_oaci)			as cod_oaci,
		tic.cod_compania			as cod_compania,
		Max(cia.dsc_compania)		as dsc_cia,		
		Upper(rtrim(ltrim(tic.dsc_num_vuelo)))			as cod_vuelo, 		
		convert(varchar(15), Max(replace(Upper(rtrim(ltrim(tic.dsc_num_vuelo))), cia.cod_iata, ''))) as num_vuelo,
		IsNull(tic.Cod_Modalidad_Venta,'')		as cod_modalidad_venta,			-- M0001: Venta Normal / M0003: Venta Masiva 		
		count(*)					as num_total,		
		Sum(case when tic.Cod_Moneda = 'DOL' then tic.Imp_Precio * IsNull(tic.Imp_Tasa_Cambio,0) else tic.Imp_Precio end) as imp_vta_valor_sol,
		Sum(case when tic.Cod_Moneda = 'SOL' then tic.Imp_Precio / IsNull(tic.Imp_Tasa_Venta,0) else tic.Imp_Precio end) as imp_vta_valor_dol			
	FROM TUA_Ticket tic 
		 INNER JOIN TUA_TipoTicket tipt ON tic.cod_tipo_ticket = tipt.cod_tipo_ticket            
 		 INNER JOIN TUA_Compania cia    ON tic.cod_compania    = cia.cod_compania 
	--WHERE	tic.fch_uso				between @as_fch_desde and @as_fch_hasta + '235959' 
	--@001 Inicio
    WHERE tic.Fch_Uso between  convert(nvarchar(8),dateadd(dd,-1,convert(datetime,@as_fch_desde)),112) and @as_fch_hasta + '235959' 
	--@001 Fin
		and tipt.tip_vuelo			like	@as_tip_trafico
		and tic.cod_compania		like	@as_cod_compania
		and tic.dsc_num_vuelo		like    @as_cod_vuelo
		and tic.tip_estado_actual	=		'U' 
		and @as_tip_documento_tuua	in		('%','T') 	  
	--@003 Inicio
	--GROUP BY left(tic.fch_uso,8), SUBSTRING(tic.fch_uso,9,2), tic.cod_compania, tic.dsc_num_vuelo, tic.Cod_Modalidad_Venta, tic.cod_tipo_ticket
	GROUP BY left(tic.fch_uso,8), SUBSTRING(tic.fch_uso,9,2), tic.cod_compania, tic.dsc_num_vuelo, tipt.tip_vuelo, tic.Cod_Modalidad_Venta, tic.cod_tipo_ticket
	--@003 Fin
	UNION
------------------------------------------------------------
-- Boarding
------------------------------------------------------------
	SELECT  
		Max(boa.fch_creacion)		as fch_uso,						/* Fecha Uso Boarding*/		
		-- @004 Inicio
		substring(Max(boa.fch_creacion + hor_creacion), 9, 2) 
		--substring(boa.fch_creacion + Max(hor_creacion), 9, 2) 
		--@004 Fin
									as hra_uso,						/* Hora Uso Boarding*/				
		--@005 Inicio
		--boa.fch_vuelo				as fch_vuelo_prog,				/* Fecha Programada Vuelo del Boarding*/		
		case when boa.fch_creacion > fch_vuelo  
					then boa.Fch_Creacion  
					else  boa.fch_vuelo	end	
									as fch_vuelo_prog,/* Fecha Programada Vuelo del Boarding*/		
		--@005 Fin
		'00'						as hra_vuelo_prog,				/* Siempre  00 */		
		Max(boa.tip_vuelo)			as tip_trafico, 
		'B'							as cod_clase_documento,
		Max(cia.cod_iata)			as cod_iata,
		Max(cia.cod_oaci)			as cod_oaci,
		boa.cod_compania			as cod_compania,
		Max(cia.dsc_compania)		as dsc_cia,
		Upper(rtrim(ltrim(boa.num_vuelo)))		as cod_vuelo, 		
		convert(varchar(15), Max(Replace(Upper(boa.num_vuelo), cia.cod_iata, ''))) as num_vuelo,		
		'M0001'						as cod_modalidad_venta,
		count(*)					as num_total,				
		-- En Boardings NO se está registrando el TC de Compra
		Sum(case when boa.Cod_Moneda = 'DOL' then boa.Imp_Precio * IsNull(boa.Imp_Tasa_Compra, boa.Imp_Tasa_Venta) else boa.Imp_Precio end) as imp_vta_valor_sol,
		Sum(case when boa.Cod_Moneda = 'SOL' then boa.Imp_Precio / IsNull(boa.Imp_Tasa_Venta, 0) else  boa.Imp_Precio end) as imp_vta_valor_dol
	FROM TUA_BoardingBCbp boa		 
 		 INNER JOIN TUA_Compania cia    ON boa.cod_compania    = cia.cod_compania 
	WHERE	
		-- @002 Inicio
		--boa.fch_vuelo				between @as_fch_desde and @as_fch_hasta 
		boa.fch_vuelo				between convert(nvarchar(8),dateadd(dd,-1,convert(datetime,@as_fch_desde)),112) and @as_fch_hasta 
		--@002 Fin --
		and boa.tip_vuelo				like	@as_tip_trafico 
		and boa.cod_compania			like	@as_cod_compania 
		and boa.num_vuelo				like    @as_cod_vuelo 
		and boa.tip_estado			 =	'U'	
		and @as_tip_documento_tuua	in ('%','B')
	--@003 Inicio
	--GROUP BY boa.fch_vuelo, boa.cod_compania, boa.num_vuelo, boa.tip_pasajero	
	--@004 Inicio
	--@005 Inicio
	--GROUP BY boa.fch_vuelo, boa.cod_compania, boa.num_vuelo, boa.tip_vuelo, boa.tip_pasajero	-- Se puso con el @003 pero se comenta con el @004
	GROUP BY boa.fch_creacion, boa.fch_vuelo, boa.cod_compania, boa.num_vuelo, boa.tip_vuelo, boa.tip_pasajero
	--@005 Fin
	--4 GROUP BY boa.fch_creacion, substring(boa.fch_creacion + hor_creacion, 9, 2), boa.fch_vuelo, boa.cod_compania, boa.num_vuelo, boa.tip_vuelo, boa.tip_pasajero				
	--@003 Fin	
	--ORDER BY fch_uso, hra_uso, dsc_cia, cod_vuelo, cod_clase_documento -- Se comenta con el @004		
	ORDER BY 3, 1, 2, dsc_cia, cod_vuelo, cod_clase_documento 
	--@004 Fin	
	
END


  /*SELECT *
  FROM TUA_BoardingBcbp
  WHERE fch_vuelo = '20160311'
  and Tip_Estado = 'U'
  and Num_Vuelo = 'UA855'

  SELECT *
  FROM TUA_Ticket
  WHERE fch_uso like '20160603%'
  and tip_estado_actual = 'U'
  and dsc_Num_Vuelo = 'UA855'
  */

  
GO


--[usp_rpt_pcs_liquidacionventa_sel] '20110106','20110106'
--[usp_rpt_pcs_liquidacionventa_sel] '20100517','20100517'

ALTER PROCEDURE [dbo].[usp_rpt_pcs_liquidacionventa_sel]
	@Fecha_Desde NVARCHAR(8)	= NULL,
	@Fecha_Hasta NVARCHAR(8)	= NULL
AS
DECLARE @CANT INT
DECLARE @COUNTI INT
SET @COUNTI = 1
SELECT @CANT = COUNT(*) FROM TUA_RES_Liquidacion WHERE Fch_Resumen >= @Fecha_Desde AND 
													   Fch_Resumen <= @Fecha_Hasta 


--CREACION TABLE
DECLARE @TablaTemporal TABLE (
	Fecha_Venta varchar(8),
	ModalxComp varchar(100),
	Rango_Inicial varchar(22),
	Rango_Final varchar(22),
	Cod_Tipo_Ticket varchar(3),
	Cabecera varchar(20),
	SubCabecera varchar(20),
	Valor numeric(12,2),
	Moneda varchar(50)
) 

DECLARE
@TOTANAC NUMERIC(12,2),
@TOTAINT NUMERIC(12,2),
@BANDINSERT INT 

SET @TOTANAC = 0.00
SET @TOTAINT = 0.00

WHILE(@COUNTI <= @CANT)
BEGIN
DECLARE 
@ACT_FECHAVENTA VARCHAR(8),
@ACT_MODALIDAD VARCHAR(5),
@ACT_CODCOMPANIA VARCHAR(10),
@ACT_RANGOINICIAL VARCHAR(22),
@ACT_RANGOFINAL VARCHAR(22),
@ACT_TIPOPRECIO VARCHAR(1),
@ACT_TIPOTICKET VARCHAR(3),
@ACT_NUMTOTAL INT,
@ACT_NUMANULADO INT,
@ACT_NUMVEND INT,
@ACT_IMPUNIT NUMERIC(12,2),
@ACT_IMPTOT NUMERIC(12,2),
@MONTNAC NUMERIC(12,2),
@MONTINT NUMERIC(12,2),
@ACT_MONEDA VARCHAR(50)

SELECT @ACT_FECHAVENTA = ISNULL(Fch_Resumen, '*'), 
	   @ACT_MODALIDAD = ISNULL(Cod_Modalidad, '*'),
	   @ACT_CODCOMPANIA = ISNULL(Cod_Compania, '*'), 
       @ACT_RANGOINICIAL = ISNULL(Num_Rango_Inicial, '*'), 
       @ACT_RANGOFINAL = ISNULL(Num_Rango_Final, '*'),
	   @ACT_TIPOPRECIO = ISNULL(Num_Variacion, 0),
	   @ACT_TIPOTICKET = ISNULL(Cod_Tipo_Ticket,'*'),
	   @ACT_NUMTOTAL = ISNULL(Num_Total, 0),
	   @ACT_NUMVEND = ISNULL(Num_Total_Vendido, 0),
	   @ACT_NUMANULADO = ISNULL(Num_Total_Anulado, 0),
	   @ACT_IMPUNIT = ISNULL(Imp_Monto_Unit,0.0),
	   @ACT_IMPTOT = ISNULL(Imp_Monto_Total,0.0),
	   @ACT_MONEDA = Moneda	 	   	
FROM (SELECT ROW_NUMBER() OVER (ORDER BY Fch_Resumen ASC) AS ROWID,* FROM TUA_RES_Liquidacion WHERE (Fch_Resumen >= @Fecha_Desde AND Fch_Resumen <= @Fecha_Hasta)) AS TBRESLIQUIDACION
WHERE ROWID = @COUNTI 
ORDER BY Fch_Resumen, Cod_Modalidad, Cod_Compania, Num_Variacion, Num_Rango_Inicial, Num_Rango_Final, Cod_Tipo_Ticket

--OBTENEMOS MODALIDAD
DECLARE @MODALIDAD VARCHAR(30)
IF(@ACT_MODALIDAD <> '*') 
BEGIN
	IF @ACT_MODALIDAD='BCBPR'
		SET @MODALIDAD='BCBP_REHABILITADO (-)'
	ELSE IF @ACT_MODALIDAD='BCBPA'
		SET @MODALIDAD='BCBP_ANULADO (-)'
	ELSE
	SELECT @MODALIDAD = Nom_Modalidad FROM TUA_ModalidadVenta
	WHERE Cod_Modalidad_Venta = @ACT_MODALIDAD
END 

IF(@ACT_MODALIDAD = 'CONTI')
BEGIN
	--print 'BIEN'
	SET @MODALIDAD = 'Contingencia'
END

IF(@ACT_CODCOMPANIA = '')
BEGIN
	SET @ACT_CODCOMPANIA = '*'
END

--OBTENEMOS COMPANIA SI HUBIERA
DECLARE @COMPANIA VARCHAR(30)
IF(@ACT_CODCOMPANIA <> '*')
BEGIN
	SELECT @COMPANIA = ISNULL(Dsc_Compania, '*') FROM TUA_Compania
	WHERE Cod_Compania = @ACT_CODCOMPANIA
END

--OBTENEMOS CANTIDADES DE GENERADAS VENDIDAS ANULADAS
DECLARE 
@GENERADAS INT,
@VENDIDAS INT,
@ANULADAS INT
SET @GENERADAS = @ACT_NUMTOTAL
SET @VENDIDAS =  @ACT_NUMVEND
SET @ANULADAS = @ACT_NUMANULADO

--OBTENEMOS TIPO PASAJERO	  
DECLARE @TIPOPASAJERO VARCHAR(10)
IF(@ACT_TIPOTICKET <> '*')
BEGIN
	--PARA EL MANEJO DE BOARDING
	IF(@ACT_TIPOTICKET <> '0')
	BEGIN
		SELECT @TIPOPASAJERO =  LC.Dsc_Campo
		FROM TUA_TipoTicket TT, TUA_ListaDeCampos LC
		WHERE TT.Cod_Tipo_Ticket = @ACT_TIPOTICKET AND 
		LC.Nom_Campo = 'TipoPasajero' AND 
		TT.Tip_Pasajero = LC.Cod_Campo
	END
	ELSE
	BEGIN
		SELECT @TIPOPASAJERO =  LC.Dsc_Campo
		FROM TUA_ListaDeCampos LC
		WHERE  
		LC.Nom_Campo = 'TipoPasajero' AND LC.Cod_Campo = 'A'
	END
END

--OBTENEMOS DATOS DEL TIPO TICKET
DECLARE @TPVUELO VARCHAR
DECLARE @TPPASAJERO VARCHAR
DECLARE @TPTRANS VARCHAR

IF(@ACT_TIPOTICKET <> '0')
BEGIN
	SELECT @TPVUELO = Tip_Vuelo, @TPPASAJERO = Tip_Pasajero, @TPTRANS = Tip_Trasbordo FROM TUA_TipoTicket WHERE Cod_Tipo_Ticket = @ACT_TIPOTICKET
END
ELSE
BEGIN
	--SET @ACT_TIPOTICKET = 'T01'
	SET @TPVUELO = 'I'
	SET @TPPASAJERO = 'A'
	SET @TPTRANS = 'N'
END

--VALIDACIONES
--SI LA MODALIDAD TIENE O NO COMPAÑIA
--PARA OBTENER LAS VARIACIONS DE PRECIOS
IF(@ACT_CODCOMPANIA = '*')
--COMPARAMOS SI LOS PRECIOS DE TICKET HAN VARIADO SI ASI LO FUESE INSERTAMOS UN REGISTRO CON DATOS PRINCIPALES DEL REGISTRO ANTERIRO
--Y CON LOS PRECIOS DEL TIPO TICKET
--INSERTAMOS SOLO A LA MODALIDAD CON SUS CANTIDADES
BEGIN
	IF(@ACT_TIPOTICKET <> '0')
	BEGIN
	--INSERTAMOS EL REGISTRO DEL PRECIO DE TICKET PARA ESTE REGISTRO
		INSERT INTO @TablaTemporal VALUES (@ACT_FECHAVENTA, @MODALIDAD + ';AAA', @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, 'AAA PRECIO', @ACT_TIPOTICKET+'('+@TPVUELO+'-'+@TPPASAJERO+'-'+@TPTRANS+')', @ACT_IMPUNIT, @ACT_MONEDA)
	END

	--INSERCCION PARA LAS CANTIDADES DE TICKET DE ACUERDO AL ESTADO
	INSERT INTO @TablaTemporal VALUES(@ACT_FECHAVENTA, @MODALIDAD + ';AAA', @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, @TIPOPASAJERO, 'AAA;Generadas', @GENERADAS, @ACT_MONEDA)
	INSERT INTO @TablaTemporal VALUES(@ACT_FECHAVENTA, @MODALIDAD + ';AAA', @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, @TIPOPASAJERO, 'BBB;Vendidas', @VENDIDAS, @ACT_MONEDA)
	INSERT INTO @TablaTemporal VALUES(@ACT_FECHAVENTA, @MODALIDAD + ';AAA', @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, @TIPOPASAJERO, 'CCC;Anuladas', @ANULADAS, @ACT_MONEDA)
END
ELSE
BEGIN
	IF(@ACT_TIPOTICKET <> '0')
	BEGIN
		INSERT INTO @TablaTemporal VALUES (@ACT_FECHAVENTA, @MODALIDAD + ';' + @COMPANIA, @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, 'AAA PRECIO', @ACT_TIPOTICKET+'('+@TPVUELO+'-'+@TPPASAJERO+'-'+@TPTRANS+')', @ACT_IMPUNIT, @ACT_MONEDA)
	END
	--INSERTAMOS LOS TIPOS  DE CANTIDADES DE TICKET DE ACUERDO A LA MODALIDAD Y COMPANIA
	--CM--INSERT INTO @TablaTemporal VALUES(@ACT_FECHAVENTA, @MODALIDAD + ';AAA', '*', '*', '*', @TIPOPASAJERO, 'AAA;Generadas', '0')
	--COMPANIA
	INSERT INTO @TablaTemporal VALUES(@ACT_FECHAVENTA, @MODALIDAD + ';' + @COMPANIA, @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, @TIPOPASAJERO, 'AAA;Generadas', @GENERADAS, @ACT_MONEDA)
	INSERT INTO @TablaTemporal VALUES(@ACT_FECHAVENTA, @MODALIDAD + ';' + @COMPANIA, @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, @TIPOPASAJERO, 'BBB;Vendidas', @VENDIDAS, @ACT_MONEDA)
	INSERT INTO @TablaTemporal VALUES(@ACT_FECHAVENTA, @MODALIDAD + ';' + @COMPANIA, @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, @TIPOPASAJERO, 'CCC;Anuladas', @ANULADAS, @ACT_MONEDA)
END

--OBTENENMOS LOS TOTALES DE NACIONAL E INTERNACIONAL
SET @TOTANAC = 0.00
SET @TOTAINT = 0.00
IF(@TPVUELO = 'I')
	BEGIN
	SET @TOTAINT = @ACT_IMPTOT
	END
ELSE
	BEGIN
	SET @TOTANAC = @ACT_IMPTOT
	END

	IF(@ACT_CODCOMPANIA = '*')
		BEGIN
			INSERT INTO @TablaTemporal VALUES (@ACT_FECHAVENTA, @MODALIDAD + ';AAA', @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, 'ZZZ IMPORTE', 'Nacional', @TOTANAC, @ACT_MONEDA)
			INSERT INTO @TablaTemporal VALUES (@ACT_FECHAVENTA, @MODALIDAD + ';AAA', @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, 'ZZZ IMPORTE', 'Internac.', @TOTAINT, @ACT_MONEDA)
		END
	ELSE
		BEGIN
			INSERT INTO @TablaTemporal VALUES (@ACT_FECHAVENTA, @MODALIDAD + ';' + @COMPANIA, @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, 'ZZZ IMPORTE', 'Nacional', @TOTANAC, @ACT_MONEDA)
			INSERT INTO @TablaTemporal VALUES (@ACT_FECHAVENTA, @MODALIDAD + ';' + @COMPANIA, @ACT_TIPOPRECIO + '-' + @ACT_RANGOINICIAL, @ACT_RANGOFINAL, @ACT_TIPOTICKET, 'ZZZ IMPORTE', 'Internac.', @TOTAINT, @ACT_MONEDA)
		END

--SITUAMOS EL ULTIMO REGISTRO DEL BUCLE
IF(@COUNTI = @CANT) 
BEGIN
	--OBTENER UN LISTADO DE LAS FECHAS
	DECLARE @MESCANT INT
	DECLARE @INIMESCANT INT
	DECLARE @DESMES VARCHAR(10)
	SET @INIMESCANT = 1
	SELECT @MESCANT = COUNT(DISTINCT(Fecha_Venta)) FROM @TablaTemporal
	
	WHILE(@INIMESCANT <= @MESCANT)
	BEGIN
		SELECT @DESMES = Fecha_Venta 
		FROM (SELECT ROW_NUMBER() OVER (ORDER BY Fecha_Venta ASC) AS ROWID, Fecha_Venta 
			  FROM @TablaTemporal GROUP BY Fecha_Venta) AS TABLAMES
		WHERE ROWID = @INIMESCANT
		--print @DESMES
		--OBTENER UN LISTADO DE TODAS LAS MODALIDADES
		DECLARE @MODCANT INT
		DECLARE @INIMODCANT INT
		DECLARE @DESMOD VARCHAR(100)
		SET @INIMODCANT = 1
		SELECT @MODCANT = COUNT(*) FROM TUA_ModalidadVenta
		WHILE(@INIMODCANT <= @MODCANT)
		BEGIN
			SELECT @DESMOD = Nom_Modalidad FROM TUA_ModalidadVenta WHERE Cod_Modalidad_Venta = 'M'+SUBSTRING('000'+CONVERT(VARCHAR, @INIMODCANT),LEN(CONVERT(VARCHAR, @INIMODCANT)),4)
			DECLARE @BANDICONT INT
			SELECT @BANDICONT = COUNT(*) FROM @TablaTemporal WHERE ModalxComp = @DESMOD + ';AAA' AND Fecha_Venta = @DESMES
			IF(@BANDICONT = 0)
			BEGIN
				INSERT INTO @TablaTemporal VALUES(@DESMES, @DESMOD + ';AAA', '*', '**', '*', 'ZZZ IMPORTE', 'Nacional', 0.00, '0')
			END
			SET @INIMODCANT = @INIMODCANT + 1
		END
		SET @INIMESCANT = @INIMESCANT + 1
		--SOLO PARA CONTINGENCIA
		SELECT @BANDICONT = COUNT(*) FROM @TablaTemporal WHERE ModalxComp = 'Contingencia' + ';AAA' AND Fecha_Venta = @DESMES
		IF(@BANDICONT = 0)
		BEGIN
			INSERT INTO @TablaTemporal VALUES(@DESMES, 'Contingencia' + ';AAA', '*', '**', '*', 'ZZZ IMPORTE', 'Nacional', 0.00, '0')
		END
--		SELECT @BANDICONT = COUNT(*) FROM @TablaTemporal WHERE ModalxComp like '%BCBP_REHABILITADO%' AND Fecha_Venta = @DESMES
--		IF(@BANDICONT > 0)
		INSERT INTO @TablaTemporal VALUES(@DESMES, 'BCBP_REHABILITADO (-)' + ';AAA', '*', '**', '*', 'ZZZ IMPORTE', 'Nacional', 0.00, '0')
--		SELECT @BANDICONT = COUNT(*) FROM @TablaTemporal WHERE ModalxComp like '%BCBP_ANULADO%' AND Fecha_Venta = @DESMES
--		IF(@BANDICONT > 0)
		INSERT INTO @TablaTemporal VALUES(@DESMES, 'BCBP_ANULADO (-)' + ';AAA', '*', '**', '*', 'ZZZ IMPORTE', 'Nacional', 0.00, '0')
	END
	
END
SET @COUNTI = @COUNTI + 1
END

SELECT Fecha_Venta, ModalxComp, Rango_Inicial, Rango_Final, Cod_Tipo_Ticket, Cabecera, SubCabecera, Valor, Moneda FROM @TablaTemporal 
order by Fecha_Venta desc
--usp_rpt_pcs_liquidacionventa_sel '20100101', '20100303'
















GO



--[usp_rpt_pcs_listarresumrecaudacionmensual_sel] '2011'
ALTER PROCEDURE [dbo].[usp_rpt_pcs_listarresumrecaudacionmensual_sel]
@ANIO VARCHAR(4)
AS
DECLARE @CONT1 INT
DECLARE @CONT2 INT 

DECLARE @DESTIPOTICK VARCHAR(120)

SELECT TOP 1 @DESTIPOTICK = TT.Cod_Tipo_Ticket+'('+TT.Tip_Vuelo+'-'+TT.Tip_Pasajero+'-'+TT.Tip_Trasbordo+')' FROM TUA_RES_RecaudacionxMes RM, TUA_TipoTicket TT
WHERE RM.Cod_Tipo_Ticket = TT.Cod_Tipo_Ticket

--Obtenemos Existencia de Datos
SELECT @CONT1 = COUNT(*) FROM TUA_RES_RecaudacionxMes RM, TUA_TipoTicket TT
WHERE SUBSTRING(RM.Fch_Resumen,0,5) = @ANIO AND RM.Cod_Tipo_Ticket = TT.Cod_Tipo_Ticket

print @CONT1
IF @CONT1 <> 0
BEGIN

SELECT 
SUBSTRING(RM.Fch_Resumen,0,5) AS Anio, 
CONVERT(VARCHAR, RM.Num_Total_Ticket) AS Mes,
CONVERT(INT, substring(RM.Fch_Resumen,5,2)) AS Num_Mes,
CASE Tip_Documento  WHEN 'T' THEN 'TICKET'
					WHEN 'B' THEN 'BCBP USO(+)'
					WHEN 'R' THEN 'BCBP REH(-)' 
					WHEN 'X' THEN 'BCBP ANU(-)' END Dsc_Documento,
TT.Cod_Tipo_Ticket+'('+TT.Tip_Vuelo+'-'+TT.Tip_Pasajero+'-'+TT.Tip_Trasbordo+')' as Dsc_Tipo_Ticket, 'Cantidad' AS Tipo_Valor, CONVERT(NUMERIC(12,2),RM.Num_Total_Ticket) AS Valor
FROM TUA_RES_RecaudacionxMes RM, TUA_TipoTicket TT
WHERE SUBSTRING(RM.Fch_Resumen,0,5) = @ANIO AND RM.Cod_Tipo_Ticket = TT.Cod_Tipo_Ticket

UNION 

SELECT 
SUBSTRING(RM.Fch_Resumen,0,5) AS Anio, 
CONVERT(VARCHAR, RM.Imp_Monto_Dol) AS Mes,
CONVERT(INT, substring(RM.Fch_Resumen,5,2)) AS Num_Mes,
CASE Tip_Documento  WHEN 'T' THEN 'TICKET'
					WHEN 'B' THEN 'BCBP USO(+)'
					WHEN 'R' THEN 'BCBP REH(-)' 
					WHEN 'X' THEN 'BCBP ANU(-)' END Dsc_Documento,
TT.Cod_Tipo_Ticket+'('+TT.Tip_Vuelo+'-'+TT.Tip_Pasajero+'-'+TT.Tip_Trasbordo+')' as Dsc_Tipo_Ticket, 'Importe' AS Tipo_Valor, CONVERT(NUMERIC(12,2),RM.Imp_Monto_Dol) AS Valor
FROM TUA_RES_RecaudacionxMes RM, TUA_TipoTicket TT
WHERE SUBSTRING(RM.Fch_Resumen,0,5) = @ANIO AND RM.Cod_Tipo_Ticket = TT.Cod_Tipo_Ticket

UNION
--PARA OBTENER TODOS LOS MESES DEL ANIO


SELECT @ANIO, '0', 1,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 1,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 1,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 1,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 2,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 2,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 2,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 2,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 3,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 3,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 3,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 3,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 4,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 4,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 4,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 4,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 5,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 5,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 5,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 5,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 6,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 6,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 6,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 6,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 7,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 7,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 7,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 7,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 8,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 8,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 8,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 8,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 9,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 9,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 9,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 9,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 10,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 10,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 10,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 10,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 11,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 11,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 11,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 11,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 12,'TICKET', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 12,'BCBP USO(+)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 12,'BCBP REH(-)', @DESTIPOTICK, 'Cantidad', 0
UNION
SELECT @ANIO, '0', 12,'BCBP ANU(-)', @DESTIPOTICK, 'Cantidad', 0


--PARA OBTENER TODOS LOS TIPOS DE TICKET
UNION
SELECT @ANIO, '0',1,'TICKET', Cod_Tipo_Ticket+'('+Tip_Vuelo+'-'+Tip_Pasajero+'-'+Tip_Trasbordo+')' as Dsc_Tipo_Ticket, 'Cantidad', 0 FROM TUA_TipoTicket
UNION
SELECT @ANIO, '0',1,'TICKET', Cod_Tipo_Ticket+'('+Tip_Vuelo+'-'+Tip_Pasajero+'-'+Tip_Trasbordo+')' as Dsc_Tipo_Ticket, 'Importe', 0 FROM TUA_TipoTicket



------PARA OBTENER TOTALES POR MES 
--UNION
--SELECT @ANIO, '0', 1,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'01' 
--UNION
--SELECT @ANIO, '0', 1,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'01' 
--UNION
--SELECT @ANIO, '0', 2,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'02' 
--UNION
--SELECT @ANIO, '0', 2,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'02' 
--UNION
--SELECT @ANIO, '0', 3,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'03' 
--UNION
--SELECT @ANIO, '0', 3,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'03' 
--UNION
--SELECT @ANIO, '0', 4,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'04' 
--UNION
--SELECT @ANIO, '0', 4,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'04' 
--UNION
--SELECT @ANIO, '0', 5,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'05' 
--UNION
--SELECT @ANIO, '0', 5,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'05' 
--UNION
--SELECT @ANIO, '0', 6,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'06' 
--UNION
--SELECT @ANIO, '0', 6,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'06' 
--UNION
--SELECT @ANIO, '0', 7,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'07' 
--UNION
--SELECT @ANIO, '0', 7,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'07' 
--UNION
--SELECT @ANIO, '0', 8,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'08' 
--UNION
--SELECT @ANIO, '0', 8,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'08' 
--UNION
--SELECT @ANIO, '0', 9,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'09' 
--UNION
--SELECT @ANIO, '0', 9,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'09' 
--UNION
--SELECT @ANIO, '0', 10,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'10' 
--UNION
--SELECT @ANIO, '0', 10,'', 'Zz', 'Importe',  CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'10' 
--UNION
--SELECT @ANIO, '0', 11,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END  FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'11' 
--UNION
--SELECT @ANIO, '0', 11,'', 'Zz', 'Importe', CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END  FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'11' 
--UNION
--SELECT @ANIO, '0', 12,'', 'Zz', 'Cantidad', CASE WHEN SUM(Num_Total_Ticket) IS NULL  THEN 0.00 ELSE SUM(Num_Total_Ticket) END FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'12' 
--UNION
--SELECT @ANIO, '0', 12,'', 'Zz', 'Importe',  CASE WHEN SUM(Imp_Monto_Dol) IS NULL  THEN 0.00 ELSE SUM(Imp_Monto_Dol) END  FROM TUA_RES_RecaudacionxMes RM
--WHERE RM.Fch_Resumen = @ANIO+'12' 

--OBTENER 

END
ELSE
BEGIN
	SELECT 
	SUBSTRING(RM.Fch_Resumen,0,5) AS Anio, 
	CONVERT(VARCHAR, RM.Num_Total_Ticket) AS Mes,
	CONVERT(INT, substring(RM.Fch_Resumen,5,2)) AS Num_Mes,
	TT.Cod_Tipo_Ticket+'('+TT.Tip_Vuelo+'-'+TT.Tip_Pasajero+'-'+TT.Tip_Trasbordo+')' as Dsc_Tipo_Ticket, 'Cantidad' AS Tipo_Valor, CONVERT(NUMERIC(12,2),RM.Num_Total_Ticket) AS Valor
	FROM TUA_RES_RecaudacionxMes RM, TUA_TipoTicket TT
	WHERE SUBSTRING(RM.Fch_Resumen,0,5) = @ANIO AND RM.Cod_Tipo_Ticket = TT.Cod_Tipo_Ticket
	UNION 
	SELECT 
	SUBSTRING(RM.Fch_Resumen,0,5) AS Anio, 
	CONVERT(VARCHAR, RM.Imp_Monto_Dol) AS Mes,
	CONVERT(INT, substring(RM.Fch_Resumen,5,2)) AS Num_Mes,
	TT.Cod_Tipo_Ticket+'('+TT.Tip_Vuelo+'-'+TT.Tip_Pasajero+'-'+TT.Tip_Trasbordo+')' as Dsc_Tipo_Ticket, 'Importe' AS Tipo_Valor, CONVERT(NUMERIC(12,2),RM.Imp_Monto_Dol) AS Valor
	FROM TUA_RES_RecaudacionxMes RM, TUA_TipoTicket TT
	WHERE SUBSTRING(RM.Fch_Resumen,0,5) = @ANIO AND RM.Cod_Tipo_Ticket = TT.Cod_Tipo_Ticket
END






GO





ALTER PROCEDURE [dbo].[usp_scz_pcs_centralTolocal_ins]( 
@Dsc_Message varchar(255) OUTPUT,
@Dsc_Information varchar(2000) OUTPUT
)
AS
BEGIN

SET @Dsc_Message=''
SET @Dsc_Information=''

DECLARE @Cod_Numero_Ticket CHAR(16),
@Cod_Compania CHAR(10),
@Cod_Venta_Masiva CHAR(5),
@Dsc_Num_Vuelo CHAR(10),
@Fch_Vuelo CHAR(8),
@Tip_Estado_Actual CHAR(1),
@Fch_Creacion CHAR(8),
@Cod_Turno CHAR(6),
@Cod_Usuario_Venta CHAR(8),
@Imp_Precio NUMERIC(8,2),
@Cod_Moneda CHAR(3),
@Fch_Vencimiento CHAR(8),
@Cod_Modalidad_Venta CHAR(5),
@Num_Rehabilitaciones INT,
@Cod_Tipo_Ticket CHAR(3),
@Num_Referencia CHAR(10),
@Flg_Contingencia CHAR(1),
@Hor_Creacion CHAR(6),
@Num_Extensiones INT,
@UltimaVez char(14),
@Identificador CHAR(2),
@Valor VARCHAR(200),
@Tip_Compania CHAR(1),
@Dsc_Compania VARCHAR(50),
@Fch_Creacion2 datetime,
@Cod_Especial_Compania CHAR(10),
@Cod_Aerolinea CHAR(3),
@Cod_SAP CHAR(16),
@Cod_OACI VARCHAR(5),
@Cod_IATA VARCHAR(5),
@Dsc_Ruc CHAR(11),
@Tip_Estado CHAR(1),
@Log_Usuario_Mod CHAR(7),
@Log_Fecha_Mod CHAR(8),
@Log_Hora_Mod CHAR(6),
@Dsc_Tipo_Ticket VARCHAR(50),
@Tip_Vuelo CHAR(1),
@Tip_Pasajero CHAR(1),
@Tip_Trasbordo CHAR(1),
@Cod_Usuario CHAR(7),
@Nom_Usuario VARCHAR(50),
@Ape_Usuario VARCHAR(50),
@Cta_Usuario VARCHAR(30),
@Pwd_Actual_Usuario VARCHAR(255),
@Flg_Cambio_Clave CHAR(1),
@Fch_Vigencia DATETIME,
@Tip_Grupo CHAR(1),
@Cod_Usuario_Creacion CHAR(7),
@Flg_Usuario_PinPad CHAR(1),
@Pwd_Usuario_Pinpad CHAR(8),
@Hora AS CHAR(6),
@Fecha AS CHAR(8),
@host VARCHAR(15),
@Retorno int,
@DBName varchar(50),
@DBUser varchar(20),
@DBPassword VARCHAR(20),
@Cod_Molinete varchar(3),
@Num_Vuelo varchar(10),
@Num_Asiento varchar(10),
@Nom_Pasajero varchar(50),
@Dsc_Trama_Bcbp varchar(200),
@Tip_Ingreso char(1),
@Tip_Anula char(1),
@Inicio CHAR(14),
@Num_SecVuelo bigint,
@Hor_Vuelo char(6),
@Dsc_Vuelo varchar(50),
@Dsc_Destino varchar(60),
@Can_Ticket int,
@cFch_Fin_Registro DATETIME,
@cFch_Inicio_Registro DATETIME,
@nProceso INT,
@Num_secuencial INT,
@dsc_valor_acumulado varchar(20),
@Cod_Atributo varchar(2),
@Tip_Atributo varchar(1),
@Dsc_Valor varchar(200),
@cEstado_proc varchar(1),
@Cod_Relativo varchar(7),
@Dsc_Campo varchar(80),
@Nom_Campo varchar(50),
@Cod_Campo varchar(5),
@Procesar INT,
@Validacion_Hora INT,
@Nom_Modalidad char(50),
@Tip_Modalidad char(1),
@nProcesoMV INT,
@nProcesoMVC INT,
@nProcesoMVCA INT

		SET @Hora = Substring([dbo].NTPFunction(),10,15)
		SET @Fecha = Substring([dbo].NTPFunction(),1,8)
		SET @Inicio=@Fecha+@Hora

		DECLARE @c_Molinetes CURSOR;
		SET @c_Molinetes = CURSOR STATIC FOR SELECT Dsc_IP, Dsc_DBName,Dsc_DBUser,Dsc_DBPassword,Cod_Molinete FROM TUA_Molinete WITH (NOLOCK) WHERE Tip_Estado='A' AND Tip_Molinete='F'
		OPEN @c_Molinetes

		FETCH NEXT FROM @c_Molinetes INTO @host, @DBName,@DBUser,@DBPassword,@Cod_Molinete

		WHILE @@FETCH_STATUS = 0
		BEGIN
			BEGIN TRY
			--SET @Procesar = 0
			--SET @Validacion_Hora = 0
			--- Verificar en la tabla de horas de sincronización la marca para iniciar la sincronización
			SET @Validacion_Hora = dbo.fnValidar_Horas_Sincronizacion('CL',@Cod_Molinete)

			IF ( @Validacion_Hora = 0 )
			BEGIN
				SET @Dsc_Information=@Dsc_Information+'No hay Hora Configurada para el Molinete '+@Cod_Molinete+' en estos momentos para procesar.#'
			END
			ELSE
			BEGIN
				IF ( @Validacion_Hora = 1 )
				BEGIN
					SET @Dsc_Information=@Dsc_Information+'Hora Configurada para el Molinete '+@Cod_Molinete+' ya se proceso.#'
				END
				ELSE
				BEGIN
					DECLARE @Sel nvarchar(2000)
					DECLARE @mysl nvarchar(2000)
					DECLARE @Param nvarchar(1000)
					DECLARE @Link varchar(8)
					DECLARE @BD varchar(20)
					DECLARE @Check BIT
					DECLARE @retcode   int
					DECLARE @linkedsvr sysname

					SET @Link = SUBSTRING(@DBName,1,CHARINDEX('.',@DBName)-1)
					SET @BD = SUBSTRING(@DBName,CHARINDEX('.',@DBName)+1,LEN(RTRIM(@DBName)))
					SET @linkedsvr = SUBSTRING(@Link,2,5)

					BEGIN TRY
						EXEC @retcode = sp_testlinkedserver @server = @linkedsvr
					END TRY
					BEGIN CATCH
						SET @retcode = sign(@@error);
					END CATCH

					IF @retcode = 0
					BEGIN
						DECLARE @nTotal_Reg INT
						DECLARE @nTotal_RegMV INT
						DECLARE @No_Reg INT
						DECLARE @No_RegMV INT
						DECLARE @Tabla_Sincronizacion varchar(20)
						SET @No_Reg = 0
						
						SET @Tabla_Sincronizacion = 'TUA_Compania'
						SET @nTotal_Reg = 0

						--***COMPANIAS***
						DECLARE @c_comp CURSOR
						SET @c_comp = CURSOR FOR SELECT Cod_Compania, Dsc_Compania, Tip_Estado, Tip_Compania, Cod_Especial_Compania, Cod_Aerolinea,
													Cod_SAP, Cod_OACI, Cod_IATA, Dsc_Ruc, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod
												FROM TUA_Compania WITH (NOLOCK) WHERE Tip_Compania='1'

						OPEN @c_comp
						
						FETCH NEXT FROM @c_comp INTO @Cod_Compania, @Dsc_Compania, @Tip_Estado, @Tip_Compania, @Cod_Especial_Compania, @Cod_Aerolinea,
								@Cod_SAP, @Cod_OACI, @Cod_IATA, @Dsc_Ruc, @Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod

						IF @@FETCH_STATUS = 0
						BEGIN
							--- Insertar Registro en la Tabla Procesos 'TUA_Sincronizacion' - Sincronizando Compañía
							SET @cFch_Inicio_Registro = GETDATE()
							INSERT INTO TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro )
								VALUES ( 'COMPANIA', 'CO', @Cod_Molinete, 'P', @cFch_Inicio_Registro, 'CL', 0 )
							SET @nProceso =  SCOPE_IDENTITY()
							SET @No_Reg = 1
						END
						ELSE
							SET @Dsc_Information=@Dsc_Information+' No hay informacion de Companias para procesar. ['+@host+']#'

						WHILE @@FETCH_STATUS = 0
						BEGIN
							SET @Sel = ''
							SET @mysl = ''
							SET @Param = ''

							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProceso
							IF @cEstado_proc = 'C' 
							BEGIN
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
								WHERE Cod_Sincronizacion = @nProceso
								SET @Dsc_Information=@Dsc_Information+' TUA_Compania Proceso Cancelado.['+@host+']#'
								RETURN
							END

							SET @Check = 0
							SET @mysl = N'IF EXISTS (SELECT Cod_Compania FROM ' + @Link + '.'+@BD +'.dbo.TUA_Compania WITH (NOLOCK) WHERE Cod_Compania = '''+@Cod_Compania+''' ) BEGIN SET @Check1 = 1 END'
							EXECUTE sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT',@Check1= @Check OUTPUT

							IF @Check = 0
							BEGIN
								SET @mysl = N'INSERT INTO ' + RTRIM(@Link) + '.'+ RTRIM(@BD) +'.dbo.TUA_Compania '+
											'SELECT * FROM TUA_Compania WHERE Cod_Compania = @pCod_Compania '
								EXECUTE sp_executesql @mysl, N'@pCod_Compania char(10)',@pCod_Compania = @Cod_Compania
							END
							ELSE
							BEGIN
								SET @Param = N'@pTip_Compania char(1), 
										@pDsc_Compania varchar(50), 
										@pFch_Creacion datetime, 
										@pCod_Especial_Compania char(3), 
										@pCod_Aerolinea char(10), 
										@pCod_SAP char(16), 
										@pCod_OACI varchar(5), 
										@pCod_IATA varchar(5), 
										@pDsc_Ruc char(11), 
										@pTip_Estado char(1), 
										@pLog_Usuario_Mod char(7), 
										@pLog_Fecha_Mod char(8), 
										@pLog_Hora_Mod char(6) '

								SET @Sel = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_Compania SET Dsc_Compania=@pDsc_Compania,Tip_Estado=@pTip_Estado, '+
										'Tip_Compania=@pTip_Compania, Cod_Especial_Compania=@pCod_Especial_Compania, Cod_Aerolinea=@pCod_Aerolinea, '+
										'Cod_SAP=@pCod_SAP, Cod_OACI=@pCod_OACI, Cod_IATA=@pCod_IATA, Dsc_Ruc=@pDsc_Ruc, '+
										'Log_Usuario_Mod=@pLog_Usuario_Mod,Log_Fecha_Mod=@pLog_Fecha_Mod,Log_Hora_Mod=@pLog_Hora_Mod  '+
										'WHERE Cod_Compania='''+@Cod_Compania+''''
								
								EXECUTE sp_executesql @Sel, @Param, 
									@pTip_Compania = @Tip_Compania, 
									@pDsc_Compania = @Dsc_Compania, 
									@pFch_Creacion = @Fch_Creacion, 
									@pCod_Especial_Compania = @Cod_Especial_Compania, 
									@pCod_Aerolinea = @Cod_Aerolinea, 
									@pCod_SAP = @Cod_SAP,
									@pCod_OACI = @Cod_OACI,
									@pCod_IATA = @Cod_IATA,
									@pDsc_Ruc = @Dsc_Ruc,
									@pTip_Estado = @Tip_Estado,
									@pLog_Usuario_Mod = @Log_Usuario_Mod,
									@pLog_Fecha_Mod = @Log_Fecha_Mod,
									@pLog_Hora_Mod = @Log_Hora_Mod

							END

							FETCH NEXT FROM @c_comp INTO @Cod_Compania, @Dsc_Compania, @Tip_Estado, @Tip_Compania, @Cod_Especial_Compania, @Cod_Aerolinea,
									@Cod_SAP, @Cod_OACI, @Cod_IATA, @Dsc_Ruc, @Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod
							SET @nTotal_Reg = @nTotal_Reg + 1
						END
						CLOSE @c_comp
						DEALLOCATE @c_comp

						IF @No_Reg = 1
						BEGIN
							SET @cFch_Fin_Registro = GETDATE()
							UPDATE TUA_Sincronizacion SET Tip_Estado = 'T', Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
							WHERE Cod_Sincronizacion = @nProceso

							SET @Dsc_Information=@Dsc_Information+' TUA_Compania Sincronizada Correctamente.['+@host+']#'
						END

						SET @Tabla_Sincronizacion = 'TUA_ListaDeCampos'
						SET @nTotal_Reg = 0
						SET @No_Reg = 0

						DECLARE @c_HoraSincro CURSOR
						SET @c_HoraSincro = CURSOR FOR SELECT * FROM TUA_ListaDeCampos WITH (NOLOCK) WHERE Nom_Campo IN ('LC','CL')

						OPEN @c_HoraSincro

						FETCH NEXT FROM @c_HoraSincro INTO @Nom_Campo, @Cod_Campo, @Cod_Relativo, @Dsc_Campo, @Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod

						IF @@FETCH_STATUS = 0
						BEGIN
							SET @cFch_Inicio_Registro = GETDATE()
							INSERT INTO TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro )
								VALUES ( 'HORASINCRO', 'HS', @Cod_Molinete, 'P', @cFch_Inicio_Registro, 'CL', 0 )
							SET @nProceso =  SCOPE_IDENTITY()
							SET @No_Reg = 1
						END
						ELSE
							SET @Dsc_Information=@Dsc_Information + ' No hay informacion de Horas de Sincrnismo para procesar. ['+@host+']#'
						
						WHILE @@FETCH_STATUS = 0
						BEGIN
							SET @Sel = ''
							SET @mysl = ''
							SET @Param = ''

							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProceso
							IF @cEstado_proc = 'C' 
							BEGIN
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
								WHERE Cod_Sincronizacion = @nProceso

								SET @Dsc_Information=@Dsc_Information +' TUA_ListaDeCampos - Horas de Sincronismo - Proceso Cancelado.['+@host+']#'
								RETURN
							END
							
							SET @Check = 0
							SET @mysl = N'IF EXISTS (SELECT Nom_Campo,Cod_Campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WHERE Nom_Campo = '''+@Nom_Campo+''' AND Cod_Campo = '''+@Cod_Campo+''' ) BEGIN SET @Check1 = 1 END'
							EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1=@Check OUTPUT

							IF @Check = 0
							BEGIN
								SET @mysl = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos  SELECT * FROM TUA_ListaDeCampos WHERE Nom_Campo='''+@Nom_Campo+''' AND Cod_Campo= '''+@Cod_Campo+''' '
								EXEC sp_executesql @mysl
							END
							ELSE
							BEGIN
								SET @Param = N'@pCod_Relativo char(7), 
										@pDsc_Campo varchar(80),
										@pLog_Usuario_Mod char(7),
										@pLog_Fecha_Mod char(8),
										@pLog_Hora_Mod char(6) '

								SET @mysl = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_ListaDeCampos SET Cod_Relativo= @pCod_Relativo, Dsc_Campo=@pDsc_Campo,Log_Usuario_Mod=@pLog_Usuario_Mod, '+
											'Log_Fecha_Mod=@pLog_Fecha_Mod, Log_Hora_Mod=@pLog_Hora_Mod '+
											'WHERE Nom_Campo='''+@Nom_Campo+''' AND Cod_Campo='''+@Cod_Campo+''' '

								EXEC sp_executesql @mysl, @Param,
												@pCod_Relativo = @Cod_Relativo,
												@pDsc_Campo = @Dsc_Campo,
												@pLog_Usuario_Mod = @Log_Usuario_Mod,
												@pLog_Fecha_Mod = @Log_Fecha_Mod,
												@pLog_Hora_Mod = @Log_Hora_Mod

							END
							FETCH NEXT FROM @c_HoraSincro INTO @Nom_Campo, @Cod_Campo, @Cod_Relativo, @Dsc_Campo, @Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod
							SET @nTotal_Reg = @nTotal_Reg + 1
						END 
						CLOSE @c_HoraSincro
						DEALLOCATE @c_HoraSincro

						IF @No_Reg = 1
						BEGIN
							SET @cFch_Fin_Registro = GETDATE()
							UPDATE TUA_Sincronizacion SET Tip_Estado = 'T', Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
							WHERE Cod_Sincronizacion = @nProceso

							SET @Dsc_Information=@Dsc_Information+' TUA_ListaDeCampos Sincronizada Correctamente.['+@host+']#'
						END

						SET @Tabla_Sincronizacion = 'TUA_TipoTicket'
						SET @nTotal_Reg = 0
						SET @No_Reg = 0

						--***TIPO TICKET***
						SET @Cod_Tipo_Ticket =(SELECT MIN(Cod_Tipo_Ticket) FROM TUA_TipoTicket WITH (NOLOCK) )

						IF @Cod_Tipo_Ticket IS NOT NULL
						BEGIN
							SET @cFch_Inicio_Registro = GETDATE()
							INSERT INTO TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro )
								VALUES ( 'TIPOTICKET', 'TC', @Cod_Molinete, 'P', @cFch_Inicio_Registro, 'CL', 0 )
							SET @nProceso =  SCOPE_IDENTITY()
							SET @No_Reg = 1
						END
						ELSE
							SET @Dsc_Information=@Dsc_Information+' No hay informacion de Tipo Ticket para procesar. ['+@host+']#'
						
						WHILE @Cod_Tipo_Ticket IS NOT NULL
						BEGIN
							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProceso
							IF @cEstado_proc = 'C' 
							BEGIN
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
								WHERE Cod_Sincronizacion = @nProceso

								SET @Dsc_Information=@Dsc_Information+' TUA_TipoTicket Proceso Cancelado.['+@host+']#'
								RETURN
							END

							SET @Check = 0
							SET @mysl = N'IF EXISTS (SELECT Cod_Tipo_Ticket FROM ' + @Link + '.'+@BD +'.dbo.TUA_TipoTicket WHERE Cod_Tipo_Ticket = '''+@Cod_Tipo_Ticket+''' ) BEGIN SET @Check1 = 1 END'
							 
							EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1=@Check OUTPUT

							IF @Check = 0
							BEGIN
								SET @mysl = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_TipoTicket  SELECT * FROM TUA_TipoTicket WITH (NOLOCK) WHERE Cod_Tipo_Ticket='''+@Cod_Tipo_Ticket+''''
								EXEC sp_executesql @mysl
							END			
							ELSE BEGIN

								SELECT @Dsc_Tipo_Ticket=Dsc_Tipo_Ticket,@Cod_Moneda=Cod_Moneda,@Imp_Precio=Imp_Precio,@Tip_Estado=Tip_Estado,
									@Log_Usuario_Mod=Log_Usuario_Mod,@Log_Fecha_Mod=Log_Fecha_Mod,@Log_Hora_Mod=Log_Hora_Mod 
								FROM TUA_TipoTicket WITH (NOLOCK) WHERE Cod_Tipo_Ticket=@Cod_Tipo_Ticket
							
								SET @Param = N'@pDsc_Tipo_Ticket varchar(50), 
												@pCod_Moneda char(3), 
												@pImp_Precio NUMERIC(8,2), 
												@pTip_Estado char(1), 
												@pLog_Usuario_Mod char(7), 
												@pLog_Fecha_Mod char(8), 
												@pLog_Hora_Mod char(6) '


								SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_TipoTicket SET Dsc_Tipo_Ticket= @pDsc_Tipo_Ticket, Cod_Moneda=@pCod_Moneda,Imp_Precio=@pImp_Precio, '+
																			'Tip_Estado=@pTip_Estado, Log_Usuario_Mod=@pLog_Usuario_Mod ,Log_Fecha_Mod=@pLog_Fecha_Mod,Log_Hora_Mod=@pLog_Hora_Mod '+
																			'WHERE Cod_Tipo_Ticket='''+@Cod_Tipo_Ticket+''''

								EXEC sp_executesql @mysl, @Param, 
									@pDsc_Tipo_Ticket = @Dsc_Tipo_Ticket, 
									@pCod_Moneda = @Cod_Moneda, 
									@pImp_Precio = @Imp_Precio, 
									@pTip_Estado =@Tip_Estado ,
									@pLog_Usuario_Mod = @Log_Usuario_Mod,
									@pLog_Fecha_Mod =@Log_Fecha_Mod ,
									@pLog_Hora_Mod = @Log_Hora_Mod
						
							END
							SET @Cod_Tipo_Ticket =(SELECT MIN(Cod_Tipo_Ticket) FROM TUA_TipoTicket WITH (NOLOCK) WHERE Cod_Tipo_Ticket>@Cod_Tipo_Ticket)
							SET @nTotal_Reg = @nTotal_Reg + 1
						END
						
						IF @No_Reg = 1
						BEGIN
							SET @cFch_Fin_Registro = GETDATE()
							UPDATE TUA_Sincronizacion SET Tip_Estado = 'T', Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
							WHERE Cod_Sincronizacion = @nProceso

							SET @Dsc_Information=@Dsc_Information+' TUA_TipoTicket Sincronizada Correctamente.['+@host+']#'
						END

						SET @Tabla_Sincronizacion = 'TUA_Usuario'
						SET @nTotal_Reg = 0
						SET @No_Reg = 0

						--***USUARIO***--
						SET @Cod_Usuario =(SELECT MIN(Cod_Usuario) FROM TUA_Usuario WITH (NOLOCK) )

						IF @Cod_Usuario IS NOT NULL
						BEGIN
							SET @cFch_Inicio_Registro = GETDATE()
							INSERT INTO TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro )
								VALUES ( 'USUARIO', 'US', @Cod_Molinete, 'P', @cFch_Inicio_Registro, 'CL', 0 )
							SET @nProceso =  SCOPE_IDENTITY()
							SET @No_Reg = 1
						END
						ELSE
							SET @Dsc_Information=@Dsc_Information+' No hay informacion de Usuarios para procesar. ['+@host+']#'

						WHILE @Cod_Usuario IS NOT NULL
						BEGIN
							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProceso
							IF @cEstado_proc = 'C' 
							BEGIN
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
								WHERE Cod_Sincronizacion = @nProceso

								SET @Dsc_Information=@Dsc_Information+' TUA_Usuario Proceso Cancelado.['+@host+']#'
								RETURN
							END

							SET @Check = 0

							SET @mysl = N'IF EXISTS (SELECT Cod_Usuario FROM ' + @Link + '.'+@BD +'.dbo.TUA_Usuario WHERE Cod_Usuario = '''+@Cod_Usuario+''' ) BEGIN SET @Check1 = 1 END'
							EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1=@Check OUTPUT

							IF @Check = 0
							BEGIN
								SET @mysl = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_Usuario  SELECT * FROM TUA_Usuario WITH (NOLOCK) WHERE Cod_Usuario='''+@Cod_Usuario+''''
								EXEC sp_executesql @mysl
							END
							ELSE
							BEGIN
								SELECT @Nom_Usuario=Nom_Usuario,@Ape_Usuario=Ape_Usuario,@Cta_Usuario=Cta_Usuario,@Pwd_Actual_Usuario=Pwd_Actual_Usuario,
									@Flg_Cambio_Clave=Flg_Cambio_Clave,@Tip_Estado_Actual=Tip_Estado_Actual,@Fch_Vigencia=Fch_Vigencia,@Tip_Grupo=Tip_Grupo,
									@Log_Usuario_Mod=Log_Usuario_Mod,@Log_Fecha_Mod=Log_Fecha_Mod,@Log_Hora_Mod=Log_Hora_Mod,@Flg_Usuario_PinPad=Flg_Usuario_PinPad,
									@Pwd_Usuario_Pinpad=Pwd_Usuario_Pinpad
								FROM TUA_Usuario WITH (NOLOCK) WHERE Cod_Usuario=@Cod_Usuario

								SET @Param = N'@pNom_Usuario varchar(50), 
										@pApe_Usuario varchar(50), 
										@pCta_Usuario varchar(30), 
										@pFlg_Cambio_Clave char(1),
										@pTip_Estado_Actual char(1), 
										@pFch_Vigencia DATETIME,
										@pTip_Grupo char(1), 
										@pLog_Usuario_Mod char(7), 
										@pLog_Fecha_Mod char(8), 
										@pLog_Hora_Mod char(6),
										@pFlg_Usuario_PinPad char(1),
										@pPwd_Actual_Usuario varchar(255),							
										@pPwd_Usuario_Pinpad char(8) '

								SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_Usuario SET Nom_Usuario= @pNom_Usuario,Ape_Usuario=@pApe_Usuario,Cta_Usuario=@pCta_Usuario, '+
																			' Pwd_Actual_Usuario=@pPwd_Actual_Usuario, Flg_Cambio_Clave=@pFlg_Cambio_Clave ,Tip_Estado_Actual=@pTip_Estado_Actual, '+
																			' Fch_Vigencia=@pFch_Vigencia,Tip_Grupo=@pTip_Grupo,Log_Usuario_Mod=@pLog_Usuario_Mod,Log_Fecha_Mod= @pLog_Fecha_Mod, '+
																			' Log_Hora_Mod=@pLog_Hora_Mod ,Flg_Usuario_PinPad= @pFlg_Usuario_PinPad,Pwd_Usuario_Pinpad= @pPwd_Usuario_Pinpad '+
																			' WHERE Cod_Usuario='''+@Cod_Usuario+''''

								EXEC sp_executesql @mysl, @Param, 
													@pNom_Usuario = @Nom_Usuario, 
													@pApe_Usuario = @Ape_Usuario, 
													@pCta_Usuario = @Cta_Usuario,
													@pFlg_Cambio_Clave = @Flg_Cambio_Clave,
													@pTip_Estado_Actual = @Tip_Estado_Actual,
													@pFch_Vigencia = @Fch_Vigencia,
													@pTip_Grupo = @Tip_Grupo,
													@pLog_Usuario_Mod = @Log_Usuario_Mod,
													@pLog_Fecha_Mod = @Log_Fecha_Mod,
													@pLog_Hora_Mod = @Log_Hora_Mod,
													@pFlg_Usuario_PinPad = @Flg_Usuario_PinPad,
													@pPwd_Actual_Usuario = @Pwd_Actual_Usuario,
													@pPwd_Usuario_Pinpad = @Pwd_Usuario_Pinpad

							END
							SET @Cod_Usuario =(SELECT MIN(Cod_Usuario) FROM TUA_Usuario WITH (NOLOCK) WHERE Cod_Usuario>@Cod_Usuario)
							SET @nTotal_Reg = @nTotal_Reg + 1
						END
						
						IF @No_Reg = 1
						BEGIN
							SET @cFch_Fin_Registro = GETDATE()
							UPDATE TUA_Sincronizacion SET Tip_Estado = 'T', Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
							WHERE Cod_Sincronizacion = @nProceso

							SET @Dsc_Information=@Dsc_Information+' TUA_Usuario Sincronizada Correctamente.['+@host+']#'
						END

						SET @Tabla_Sincronizacion = 'TUA_ModalidadVenta'
						SET @nTotal_RegMV = 0
						SET @No_RegMV = 0
						--***Modalidad Venta
						DECLARE @ModVenta CURSOR;
						SET @ModVenta = CURSOR FOR SELECT Cod_Modalidad_Venta, Nom_Modalidad, Tip_Modalidad, Tip_Estado, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod FROM TUA_ModalidadVenta WITH (NOLOCK)
						OPEN @ModVenta

						FETCH NEXT FROM @ModVenta INTO @Cod_Modalidad_Venta, @Nom_Modalidad, @Tip_Modalidad, @Tip_Estado, @Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod

						IF @@FETCH_STATUS = 0
						BEGIN
							SET @cFch_Inicio_Registro = GETDATE()
							INSERT INTO TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro )
								VALUES ( 'MODALIDADVENTA', 'MT', @Cod_Molinete, 'P', @cFch_Inicio_Registro, 'CL', 0 )
							SET @nProceso =  SCOPE_IDENTITY()
							SET @nProcesoMV = @nProceso
							SET @No_RegMV = 1
						END
						ELSE
							SET @Dsc_Information=@Dsc_Information+' No hay informacion de Modalidad Venta para procesar. ['+@host+']#'

						WHILE @@FETCH_STATUS = 0
						BEGIN
							SET @Sel = ''
							SET @mysl = ''
							SET @Param = ''

							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProcesoMV
							IF @cEstado_proc = 'C' 
							BEGIN
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_RegMV
								WHERE Cod_Sincronizacion = @nProcesoMV

								SET @Dsc_Information=@Dsc_Information +' TUA_ModalidadVenta Proceso Cancelado.['+@host+']#'
								RETURN
							END

							SET @Check = 0
							SET @mysl = N' IF EXISTS (SELECT Cod_Modalidad_Venta FROM ' + @Link +'.'+ @BD +'.dbo.TUA_ModalidadVenta WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' ) BEGIN SET @Check1 = 1 END '
							EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1 = @Check OUTPUT

							IF @Check = 0
							BEGIN
								SET @mysl = N' INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_ModalidadVenta (Cod_Modalidad_Venta, Nom_Modalidad, Tip_Modalidad, Tip_Estado, '+
									' Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod) SELECT Cod_Modalidad_Venta, Nom_Modalidad, Tip_Modalidad, Tip_Estado, Log_Usuario_Mod, '+
									' Log_Fecha_Mod, Log_Hora_Mod FROM TUA_ModalidadVenta WITH (NOLOCK) WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' '
								
								EXEC sp_executesql @mysl
							END
							ELSE 
							BEGIN
								SET @Param = N'@pNom_Modalidad CHAR(50) ,
										@pTip_Modalidad CHAR(1) ,
										@pTip_Estado CHAR(1) ,
										@pLog_Usuario_Mod CHAR(7) ,
										@pLog_Fecha_Mod CHAR(8) ,
										@pLog_Hora_Mod  CHAR(6) '

								SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_ModalidadVenta SET Nom_Modalidad=@pNom_Modalidad, Tip_Modalidad=@pTip_Modalidad, Tip_Estado=@pTip_Estado, Log_Usuario_Mod=@pLog_Usuario_Mod, '+
															' Log_Fecha_Mod=@pLog_Fecha_Mod, Log_Hora_Mod=@pLog_Hora_Mod '+
															' WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' '

								EXEC sp_executesql @mysl, @Param,
											@pNom_Modalidad   = @Nom_Modalidad,
											@pTip_Modalidad   = @Tip_Modalidad,
											@pTip_Estado      = @Tip_Estado,
											@pLog_Usuario_Mod = @Log_Usuario_Mod,
											@pLog_Fecha_Mod   = @Log_Fecha_Mod,
											@pLog_Hora_Mod    = @Log_Hora_Mod

							END
							FETCH NEXT FROM @ModVenta INTO @Cod_Modalidad_Venta, @Nom_Modalidad, @Tip_Modalidad, @Tip_Estado, @Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod
							SET @nTotal_RegMV = @nTotal_RegMV + 1
						END
						CLOSE @ModVenta
						DEALLOCATE @ModVenta

						IF @No_RegMV = 1
						BEGIN
							SET @cFch_Fin_Registro = GETDATE()
							UPDATE TUA_Sincronizacion SET Tip_Estado = 'T', Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_RegMV
							WHERE Cod_Sincronizacion = @nProcesoMV

							SET @Dsc_Information=@Dsc_Information+' TUA_ModalidadVenta Sincronizada Correctamente.['+@host+']#'
						END

						SET @Tabla_Sincronizacion = 'TUA_ModVentaComp'
						SET @nTotal_Reg = 0
						SET @No_Reg = 0

						--***Mod Venta Compañia
						DECLARE @t_comp CURSOR
						SET @t_comp = CURSOR FOR SELECT mvc.cod_compania, mvc.cod_modalidad_venta, mvc.dsc_valor_acumulado FROM TUA_ModVentaComp mvc, TUA_Compania co WITH (NOLOCK) WHERE mvc.Cod_Compania = co.Cod_Compania AND co.Tip_Compania = '1'
						OPEN @t_comp
						FETCH NEXT FROM @t_comp INTO @Cod_Compania, @Cod_Modalidad_Venta, @dsc_valor_acumulado

						IF @@FETCH_STATUS = 0
						BEGIN
							SET @cFch_Inicio_Registro = GETDATE()
							INSERT INTO TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro )
								VALUES ( 'MODVENCOMP', 'MV', @Cod_Molinete, 'P', @cFch_Inicio_Registro, 'CL', 0 )
							SET @nProcesoMVC =  SCOPE_IDENTITY()
							SET @No_Reg = 1
						END
						ELSE
							SET @Dsc_Information=@Dsc_Information+' No hay informacion de Modalidad Venta de Compania para procesar. ['+@host+']#'

						WHILE @@FETCH_STATUS = 0
						BEGIN
							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProceso
							IF @cEstado_proc = 'C' 
							BEGIN
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
								WHERE Cod_Sincronizacion = @nProcesoMVC
								SET @Dsc_Information=@Dsc_Information + ' TUA_ModVentaComp Proceso Cancelado.['+@host+']#'
								RETURN
							END

							SET @Check = 0
							SET @mysl = N'IF EXISTS (SELECT cod_compania, cod_modalidad_venta FROM ' + @Link + '.'+@BD +'.dbo.TUA_ModVentaComp WHERE cod_compania = '''+@Cod_Compania+''' AND cod_modalidad_venta= '''+@Cod_Modalidad_Venta+''' ) BEGIN SET @Check1 = 1 END'
											 
							EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1=@Check OUTPUT
							
							IF @Check = 0
							BEGIN
								SET @mysl = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_ModVentaComp  SELECT * FROM TUA_ModVentaComp WITH (NOLOCK) WHERE cod_compania = '''+@Cod_Compania+''' AND cod_modalidad_venta = '''+@Cod_Modalidad_Venta+''''
								EXEC sp_executesql @mysl
							END
							ELSE
							BEGIN
								SET @Param = N'@pdsc_valor_acumulado  varchar(20) '
								SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_ModVentaComp SET dsc_valor_acumulado = @pdsc_valor_acumulado '+								
																			' WHERE cod_compania = '''+@Cod_Compania+''' AND cod_modalidad_venta = '''+@Cod_Modalidad_Venta+''' '

								EXEC sp_executesql @mysl, @Param, @pdsc_valor_acumulado = @dsc_valor_acumulado

							END
							FETCH NEXT FROM @t_comp INTO @Cod_Compania, @Cod_Modalidad_Venta, @dsc_valor_acumulado
							SET @nTotal_Reg = @nTotal_Reg + 1
						END 
						CLOSE @t_comp
						DEALLOCATE @t_comp

						IF @No_Reg = 1
						BEGIN
							SET @cFch_Fin_Registro = GETDATE()
							UPDATE TUA_Sincronizacion SET Tip_Estado = 'T', Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
							WHERE Cod_Sincronizacion = @nProcesoMVC

							SET @Dsc_Information=@Dsc_Information+' TUA_ModVentaComp Sincronizada Correctamente.['+@host+']#'
						END

						SET @Tabla_Sincronizacion = 'TUA_ModVentaCompAtr'
						SET @nTotal_Reg = 0
						SET @No_Reg = 0
						--***Mod Venta Compania Atributo
						DECLARE @ModVCAtr CURSOR;
						SET @ModVCAtr = CURSOR FOR SELECT mvca.cod_compania, mvca.cod_modalidad_venta, mvca.cod_atributo FROM TUA_ModVentaCompAtr mvca, TUA_Compania co WITH (NOLOCK) WHERE mvca.Cod_Compania = co.Cod_Compania AND co.Tip_Compania = '1'
						OPEN @ModVCAtr

						FETCH NEXT FROM @ModVCAtr INTO @Cod_Compania, @Cod_Modalidad_Venta, @Cod_Atributo

						IF @@FETCH_STATUS = 0
						BEGIN
							SET @cFch_Inicio_Registro = GETDATE()
							INSERT INTO TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro )
								VALUES ( 'MODVENCOMPATR', 'MA', @Cod_Molinete, 'P', @cFch_Inicio_Registro, 'CL', 0 )
							SET @nProcesoMVCA =  SCOPE_IDENTITY()
							SET @No_Reg = 1
						END
						ELSE
							SET @Dsc_Information=@Dsc_Information+' No hay informacion de Modalidad Venta de Compania Atributo para procesar. ['+@host+']#'

						WHILE @@FETCH_STATUS = 0
						BEGIN
							SET @Sel = ''
							SET @mysl = ''
							SET @Param = ''

							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProceso
							IF @cEstado_proc = 'C' 
							BEGIN
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
								WHERE Cod_Sincronizacion = @nProcesoMVCA

								SET @Dsc_Information=@Dsc_Information +' TUA_ModVentaCompAtr Proceso Cancelado.['+@host+']#'
								RETURN
							END

							SET @Check = 0
							SET @mysl = N' IF EXISTS (SELECT Num_secuencial FROM ' + @Link +'.'+ @BD +'.dbo.TUA_ModVentaCompAtr WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' AND Cod_Compania = '''+@Cod_Compania+''' AND Cod_Atributo = '''+@Cod_Atributo+''' ) BEGIN SET @Check1 = 1 END '
							EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1 = @Check OUTPUT

							IF @Check = 0
							BEGIN
								SET @mysl = N' INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_ModVentaCompAtr(Cod_Modalidad_Venta, Cod_Compania, Cod_Atributo, Tip_Atributo, Cod_Tipo_Ticket, '+
									' Dsc_Valor, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod) SELECT Cod_Modalidad_Venta, Cod_Compania, Cod_Atributo, Tip_Atributo, Cod_Tipo_Ticket, Dsc_Valor, Log_Usuario_Mod, '+
									' Log_Fecha_Mod, Log_Hora_Mod FROM TUA_ModVentaCompAtr WITH (NOLOCK) WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' AND Cod_Compania = '''+@Cod_Compania+''' AND Cod_Atributo = '''+@Cod_Atributo+''''
								
								EXEC sp_executesql @mysl
							END
							ELSE 
							BEGIN
								SELECT @Tip_Atributo=Tip_Atributo, @Cod_Tipo_Ticket=Cod_Tipo_Ticket, @Dsc_Valor=Dsc_Valor, 
									@Log_Usuario_Mod=Log_Usuario_Mod, @Log_Fecha_Mod=Log_Fecha_Mod, @Log_Hora_Mod=Log_Hora_Mod
								FROM dbo.TUA_ModVentaCompAtr WITH (NOLOCK) 
								WHERE Cod_Modalidad_Venta = @Cod_Modalidad_Venta 
									AND Cod_Compania = @Cod_Compania 
									AND Cod_Atributo = @Cod_Atributo
								
								SET @Param = N'@pTip_Atributo varchar(1),
										@pCod_Tipo_Ticket CHAR(3) ,
										@pDsc_Valor varchar(200) ,
										@pLog_Usuario_Mod CHAR(7) ,
										@pLog_Fecha_Mod CHAR(8) ,
										@pLog_Hora_Mod  CHAR(6) '

								SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_ModVentaCompAtr SET Tip_Atributo=@pTip_Atributo, Cod_Tipo_Ticket=@pCod_Tipo_Ticket, Dsc_Valor=@pDsc_Valor, Log_Usuario_Mod=@pLog_Usuario_Mod, '+
															' Log_Fecha_Mod=@pLog_Fecha_Mod, Log_Hora_Mod=@pLog_Hora_Mod '+
															' WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' AND Cod_Compania = '''+@Cod_Compania+''' AND Cod_Atributo = '''+@Cod_Atributo+''''

								EXEC sp_executesql @mysl, @Param,
											@pTip_Atributo = @Tip_Atributo,
											@pCod_Tipo_Ticket = @Cod_Tipo_Ticket,
											@pDsc_Valor = @Dsc_Valor,
											@pLog_Usuario_Mod = @Log_Usuario_Mod,
											@pLog_Fecha_Mod = @Log_Fecha_Mod,
											@pLog_Hora_Mod = @Log_Hora_Mod

							END
							FETCH NEXT FROM @ModVCAtr INTO @Cod_Compania, @Cod_Modalidad_Venta, @Cod_Atributo
							SET @nTotal_Reg = @nTotal_Reg + 1
						END
						CLOSE @ModVCAtr
						DEALLOCATE @ModVCAtr

						IF @No_Reg = 1
						BEGIN
							SET @cFch_Fin_Registro = GETDATE()
							UPDATE TUA_Sincronizacion SET Tip_Estado = 'T', Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
							WHERE Cod_Sincronizacion = @nProcesoMVCA

							SET @Dsc_Information=@Dsc_Information+' TUA_ModVentaCompAtr Sincronizada Correctamente.['+@host+']#'
						END

						--- /* Verifica si Existe en Central sino Elimina de Local Modalidad de Venta Atributo
						SET @mysl = N' SET @my_cur = CURSOR STATIC FOR SELECT cod_compania,cod_modalidad_venta,cod_atributo FROM '+ @Link + '.'+@BD +'.dbo.TUA_ModVentaCompAtr WITH (NOLOCK); '+' OPEN @my_cur '

						DECLARE @my_cur CURSOR
						EXEC sp_executesql
							 @mysl,
							 N'@my_cur cursor OUTPUT', @my_cur OUTPUT

						FETCH NEXT FROM @my_cur INTO @Cod_Compania, @Cod_Modalidad_Venta, @Cod_Atributo

						WHILE @@FETCH_STATUS = 0
						BEGIN
							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProceso
							IF @cEstado_proc = 'C' 
							BEGIN
								SELECT @nTotal_Reg = COUNT(Cod_Modalidad_Venta) FROM TUA_ModVentaCompAtr WITH (NOLOCK)
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
								WHERE Cod_Sincronizacion = @nProcesoMVCA

								SET @Dsc_Information=@Dsc_Information+' TUA_ModVentaCompAtr Proceso Cancelado.['+@host+']#'
								RETURN
							END

							SET @Check = 0
							SET @mysl = N' IF EXISTS (SELECT Num_secuencial FROM TUA_ModVentaCompAtr WITH (NOLOCK) WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' AND Cod_Compania= '''+@Cod_Compania+''' AND Cod_Atributo = '''+@Cod_Atributo+''' )  BEGIN SET @Check1 = 1 END '
							EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1 = @Check OUTPUT

							IF @Check = 0
							BEGIN
								SET @mysl = N' DELETE FROM ' + @Link + '.'+@BD +'.dbo.TUA_ModVentaCompAtr  WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' AND Cod_Compania= '''+@Cod_Compania+''' AND Cod_Atributo = '''+@Cod_Atributo+''' '
								EXEC sp_executesql @mysl
							END
							FETCH NEXT FROM @my_cur into @Cod_Compania, @Cod_Modalidad_Venta, @Cod_Atributo
						END
						CLOSE @my_cur
						DEALLOCATE @my_cur

						--- /* Verifica si Existe en Central sino Elimina de Local Modalidad de Venta Compañia
						SET @mysl = N' SET @MDV_cur = CURSOR STATIC FOR SELECT cod_compania,cod_modalidad_venta FROM '+ @Link + '.'+@BD +'.dbo.TUA_ModVentaComp WITH (NOLOCK); '+' OPEN @MDV_cur '

						DECLARE @MDV_cur CURSOR
						EXEC sp_executesql
							 @mysl,
							 N'@MDV_cur cursor OUTPUT', @MDV_cur OUTPUT

						FETCH NEXT FROM @MDV_cur INTO @Cod_Compania, @Cod_Modalidad_Venta

						WHILE @@FETCH_STATUS = 0
						BEGIN
							SELECT @cEstado_proc = Tip_Estado FROM TUA_Sincronizacion WITH (ROWLOCK) WHERE Cod_Sincronizacion = @nProceso
							IF @cEstado_proc = 'C' 
							BEGIN
								SELECT @nTotal_Reg = COUNT(Cod_Modalidad_Venta) FROM TUA_ModVentaComp WITH (NOLOCK)
								SET @cFch_Fin_Registro = GETDATE()
								UPDATE TUA_Sincronizacion SET Fch_Fin_Registro = @cFch_Fin_Registro, Num_Registro = @nTotal_Reg
								WHERE Cod_Sincronizacion = @nProcesoMVC

								SET @Dsc_Information=@Dsc_Information+' TUA_ModVentaComp Proceso Cancelado.['+@host+']#'
								RETURN
							END

							SET @Check = 0
							SET @mysl = N' IF EXISTS (SELECT Cod_Compania, Cod_Modalidad_Venta FROM TUA_ModVentaComp WITH (NOLOCK) WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' AND Cod_Compania= '''+@Cod_Compania+''' )  BEGIN SET @Check1 = 1 END '
							EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1 = @Check OUTPUT

							IF @Check = 0
							BEGIN
								SET @mysl = N' DELETE FROM ' + @Link + '.'+@BD +'.dbo.TUA_ModVentaComp WHERE Cod_Modalidad_Venta = '''+@Cod_Modalidad_Venta+''' AND Cod_Compania= '''+@Cod_Compania+''' '
								EXEC sp_executesql @mysl
							END
							FETCH NEXT FROM @MDV_cur into @Cod_Compania, @Cod_Modalidad_Venta
						END
						CLOSE @MDV_cur
						DEALLOCATE @MDV_cur

						SET @Dsc_Information=@Dsc_Information+' Sincronizacion Satisfactoriamente ...['+@host+']#'
					END
					ELSE
					BEGIN
						SET @Dsc_Information=@Dsc_Information+'Error en Conexion - Linked Server para la IP ['+@host+']'+ ' No esta Activo para procesar.'
					END
				END
			END
			
			FETCH NEXT FROM @c_Molinetes INTO @host, @DBName,@DBUser,@DBPassword,@Cod_Molinete

			END TRY
			BEGIN CATCH
				SET @Dsc_Message=@Tabla_Sincronizacion+' Error en Transaccion - Servidor Local : '+@host+' - Linea Error : '+CONVERT(varchar(5), ERROR_LINE())+' - Nro Error : '+CONVERT(varchar(20), ERROR_NUMBER())+' - Mensaje Error : '+ERROR_MESSAGE()
				IF XACT_STATE() <> 0
				BEGIN
					ROLLBACK --TRANSACTION
					BREAK
				END
			END CATCH
		END
		CLOSE @c_Molinetes
		DEALLOCATE @c_Molinetes

END
GO



ALTER PROCEDURE [dbo].[usp_seg_cns_perfilxusarioArchivamiento_sel](
@Cod_Usuario CHAR(7)
)
AS
SELECT PR.Cod_Proceso, PR.Cod_Modulo,AM.Id_Proceso,AM.Dsc_Archivo,AM.Dsc_Icono,AM.Cod_Proceso_Padre AS Dsc_Modulo, PR.Cod_Rol,PR.Flg_Permitido,AM.Dsc_Proceso 
FROM TUA_PerfilRol PR,TUA_UsuarioRol UR,TUA_ArbolModulo AM 
WHERE PR.Cod_Rol=UR.Cod_Rol AND UR.Cod_Usuario=@Cod_Usuario AND 
(PR.Cod_Modulo='009') AND AM.Cod_Modulo=PR.Cod_Modulo
 AND AM.Cod_Proceso=PR.Cod_Proceso order by PR.Cod_Modulo,PR.Cod_Proceso ASC

GO



ALTER PROCEDURE [dbo].[usp_seg_cns_perfilxusarioventa_sel](
@Cod_Usuario CHAR(7)
)
AS
SELECT PR.Cod_Proceso, PR.Cod_Modulo,AM.Id_Proceso,AM.Dsc_Archivo,AM.Dsc_Icono,AM.Cod_Proceso_Padre AS Dsc_Modulo, PR.Cod_Rol,PR.Flg_Permitido,AM.Dsc_Proceso 
FROM TUA_PerfilRol PR,TUA_UsuarioRol UR,TUA_ArbolModulo AM 
WHERE PR.Cod_Rol=UR.Cod_Rol AND UR.Cod_Usuario=@Cod_Usuario AND 
(PR.Cod_Modulo='I20' OR PR.Cod_Modulo='I21') AND AM.Cod_Modulo=PR.Cod_Modulo
 AND AM.Cod_Proceso=PR.Cod_Proceso order by PR.Cod_Modulo,PR.Cod_Proceso ASC


GO


ALTER PROCEDURE [dbo].[usp_seg_pcs_arbolmodulo_all_sel]

AS

SET NOCOUNT ON

SELECT AM.Cod_Proceso as Cod_Proceso,
	AM.Cod_Proceso_Padre as Cod_Proceso_Padre,
	M.Cod_Modulo as Cod_Modulo,
	M.Dsc_Modulo as Dsc_Modulo,
	M.Tip_Modulo,
	AM.Id_Proceso as Id_Proceso,
	AM.Dsc_Proceso as Dsc_Proceso,
	AM.Tip_Nivel as Tip_Nivel,
	AM.Tip_Estado,
	PR.Flg_Permitido,
	AM.Dsc_Archivo,
	AM.Dsc_Icono,
	AM.Dsc_Texto_Ayuda,
	AM.Dsc_Ind_Critic,
	AM.Dsc_Color_Critic,
	AM.Dsc_Tab_Filtro,
	AM.Dsc_Licencia,
	AM.Num_Posicion as Num_Pos_Proceso,
	M.Num_Posicion as Num_Pos_Modulo

FROM  TUA_ArbolModulo AM, TUA_Modulo M,TUA_PerfilRol PR
WHERE AM.Cod_Modulo = M.Cod_Modulo    AND
	  AM.Cod_Proceso = PR.Cod_Proceso AND
	  PR.Cod_Modulo = AM.Cod_Modulo   AND	
	  AM. Flg_Permitido ='1' 




GO



ALTER PROCEDURE [dbo].[usp_seg_pcs_listararbolmodulo_xrol]-- 'R0001'
(
	@Cod_Rol char(5)
)

AS

SET NOCOUNT ON


SELECT DISTINCT AM.Cod_Modulo,
	   M.Dsc_Modulo,
	   M.Tip_Modulo,
       AM.Cod_Proceso, 
       AM.Id_Proceso,
       AM.Dsc_Proceso, 
       AM.Dsc_Icono,
       AM.Dsc_Archivo,
	   PR.Flg_Permitido,
	   R.Nom_Rol,
	   M.Num_Posicion as Num_Pos_Modulo, 
       AM.Num_Posicion as Num_Pos_Proceso,
	   AM.Tip_Nivel,
	   AM.Cod_Proceso_Padre		
	   	
		
FROM  TUA_ArbolModulo AM, TUA_PerfilRol PR, TUA_Modulo M, TUA_Rol R
WHERE AM.Cod_Proceso = PR.Cod_Proceso AND 
      PR.Cod_Rol =@Cod_Rol AND 
      PR.Cod_Modulo = AM.Cod_Modulo   AND
	  AM.Cod_Modulo = M.Cod_Modulo    AND
	  AM. Flg_Permitido ='1' AND	
	  AM.Tip_Nivel IN(0,1,2)AND	
	  PR.Cod_Rol = R.Cod_Rol 
	
ORDER BY M.Num_Posicion,AM.Tip_Nivel,AM.Cod_Proceso_Padre	

















GO



ALTER PROCEDURE [dbo].[usp_seg_pcs_listararbolmodulo_xusuario] --'U000064'
(
	@Cod_Usuario char(7)
)

AS

SET NOCOUNT ON


SELECT AM.Cod_Modulo,
	   M.Dsc_Modulo,
	   M.Tip_Modulo,
       AM.Cod_Proceso, 
       AM.Id_Proceso,
       AM.Dsc_Proceso, 
       AM.Dsc_Icono,
       AM.Dsc_Archivo,
	   R.Nom_Rol,
	   M.Num_Posicion as Pos_Modulo,
       AM.Num_Posicion as Pos_Proceso	
	   	
		
FROM  TUA_ArbolModulo AM, TUA_PerfilRol PR, TUA_Modulo M, TUA_Rol R
WHERE AM.Cod_Proceso = PR.Cod_Proceso AND 
      PR.Cod_Rol IN 
	  (SELECT Cod_Rol 
	   FROM TUA_UsuarioRol 
	   WHERE Cod_Usuario=@Cod_Usuario) AND 
       PR.Cod_Modulo = AM.Cod_Modulo   AND
	   AM.Cod_Modulo = M.Cod_Modulo    AND
	   M.Tip_Modulo='01'AND	
	   (AM. Flg_Permitido ='1' AND	
	   PR.Flg_Permitido='1') AND	
	   PR.Cod_Rol = R.Cod_Rol and AM.Tip_Nivel IN(0,1)
	
ORDER BY M.Num_Posicion,AM.Num_Posicion,AM.Tip_Nivel

















GO




ALTER PROCEDURE [dbo].[usp_seg_pcs_obtenerarbolmodulo_xproceso_xmodulo_xrol_sel] --'001', 'E0020' ,'R0001'
(
	@Cod_Modulo char(3),	
	@Cod_Proceso char(5),
	@Cod_Rol char(5)
)

AS

SET NOCOUNT ON

SELECT DISTINCT AM.Cod_Proceso as Cod_Proceso,
	AM.Cod_Proceso_Padre as Cod_Proceso_Padre,
	M.Cod_Modulo as Cod_Modulo,
	M.Dsc_Modulo as Dsc_Modulo,
	M.Tip_Modulo,
	AM.Id_Proceso as Id_Proceso,
	AM.Dsc_Proceso as Dsc_Proceso,
	AM.Tip_Nivel as Tip_Nivel,
	AM.Tip_Estado,
	PR.Flg_Permitido,
	AM.Dsc_Archivo,
	AM.Dsc_Icono,
	AM.Dsc_Texto_Ayuda,
	AM.Dsc_Ind_Critic,
	AM.Dsc_Color_Critic,
	AM.Dsc_Tab_Filtro,
	AM.Dsc_Licencia,
	AM.Num_Posicion as Num_Pos_Proceso,
	M.Num_Posicion as Num_Pos_Modulo
	
		
FROM  TUA_ArbolModulo AM, TUA_PerfilRol PR, TUA_Modulo M
WHERE AM.Cod_Proceso = PR.Cod_Proceso AND 
      PR.Cod_Modulo = AM.Cod_Modulo   AND
	  AM.Cod_Modulo = M.Cod_Modulo    AND
	  PR.Cod_Proceso =@Cod_Proceso AND 
	  PR.Cod_Modulo = @Cod_Modulo  AND	
	  PR.Cod_Rol =@Cod_Rol AND
	  AM. Flg_Permitido ='1' 
	
ORDER BY M.Num_Posicion,AM.Num_Posicion





GO




ALTER PROCEDURE [dbo].[usp_seg_pcs_perfilrol_flag_opcion_sel] --'U000001' ,'R0001','Seg_VerUsuario.aspx', 'Nuevo'
(
	@Cod_Usuario char(7),
	@Cod_Rol char(5),
	@Dsc_Archivo varchar(255),
	@Dsc_Proceso varchar(100)
	
)

AS

SET NOCOUNT ON


SELECT PR.Cod_Modulo,
       PR.Cod_Proceso, 
	   PR.Flg_Permitido
	   	
		
FROM  TUA_ArbolModulo AM, TUA_PerfilRol PR, TUA_Modulo M, TUA_Rol R
WHERE AM.Cod_Proceso = PR.Cod_Proceso AND 
      PR.Cod_Rol IN 
	  (SELECT Cod_Rol 
	   FROM TUA_UsuarioRol 
	   WHERE Cod_Usuario=@Cod_Usuario) AND 
       PR.Cod_Modulo = AM.Cod_Modulo   AND
	   AM.Cod_Modulo = M.Cod_Modulo    AND
	   AM. Flg_Permitido ='1'  AND	
	   (AM.Cod_Proceso_Padre= (SELECT Cod_Proceso 
	   FROM TUA_ArbolModulo 
	   WHERE Dsc_Archivo=@Dsc_Archivo)) and 	
	   LTRIM(RTRIM(AM.Dsc_Proceso))= @Dsc_Proceso AND 	
	   PR.Cod_Rol= @Cod_Rol AND	
	   PR.Cod_Rol = R.Cod_Rol and AM.Tip_Nivel IN(0,1)
	   	
ORDER BY M.Num_Posicion,AM.Cod_Proceso,AM.Num_Posicion,AM.Tip_Nivel
















GO


ALTER PROCEDURE [dbo].[usp_seg_pcs_rol_all_sel]

AS

SET NOCOUNT ON

SELECT S.Cod_Rol,
	S.Cod_Padre_Rol,
	(SELECT R.Nom_Rol FROM TUA_Rol R WHERE R.Cod_Rol = S.Cod_Padre_Rol) Nom_Padre_Rol, 
	S.Nom_Rol,
	S.Cod_Usuario_Creacion,
	(SELECT U.Cta_Usuario FROM TUA_Usuario U WHERE U.Cod_Usuario = S.Cod_Usuario_Creacion) Cta_Usuario_Creacion, 
	S.Fch_Creacion,
	S.Hor_Creacion,
	S.Log_Usuario_Mod,
	S.Log_Fecha_Mod,
	S.Log_Hora_Mod,
	CASE WHEN (U.Ape_Usuario IS NULL AND U.Nom_Usuario IS NULL) THEN S.Cod_Usuario_Creacion
		 ELSE ISNULL(U.Nom_Usuario,' - ') +', '+ ISNULL(U.Ape_Usuario, ' - ') END Nom_Usuario_Creacion
	,'0' Flg_Sec
FROM TUA_Rol S
	LEFT JOIN TUA_Usuario U ON S.Cod_Usuario_Creacion = U.Cod_Usuario
ORDER BY 1





GO





ALTER PROCEDURE [dbo].[usp_vta_cns_allmoneda_sel]

AS

SET NOCOUNT ON

SELECT DISTINCT M.Cod_Moneda,
	M.Dsc_Moneda,
	M.Dsc_Simbolo,
	M.Log_Usuario_Mod,
	M.Log_Fecha_Mod,
	M.Log_Hora_Mod
FROM TUA_Moneda M,TUA_TasaCambio TC WHERE M.Tip_Estado='1' 
 and ((TC.Cod_Moneda=M.Cod_Moneda and TC.Tip_Estado='1' and M.Cod_Moneda in (select Cod_Moneda from TUA_TasaCambio where Tip_Estado='1' and Tip_Cambio='C') and M.Cod_Moneda in (select Cod_Moneda from TUA_TasaCambio where Tip_Estado='1' and Tip_Cambio='V')) OR M.Cod_Moneda='SOL')



GO




--[usp_vta_cns_companiaxmodventa_sel] NULL,''
ALTER PROCEDURE [dbo].[usp_vta_cns_companiaxmodventa_sel](--usp_vta_cns_companiaxmodventa_sel 'M0003',null
@Cod_Modalidad_Venta char(5)=NULL,
@Tip_Compania char(1)=NULL
)
AS

IF(@Cod_Modalidad_Venta IS NOT NULL)
BEGIN
	IF(@Tip_Compania IS NOT NULL)
	BEGIN
		SELECT MVC.Cod_Compania,C.Tip_Compania,C.Dsc_Compania,C.Dsc_Ruc,MVC.Cod_Modalidad_Venta,MVC.Dsc_Valor_Acumulado FROM TUA_ModVentaComp MVC,TUA_Compania C
		WHERE MVC.Cod_Modalidad_Venta=@Cod_Modalidad_Venta AND MVC.Cod_Compania=C.Cod_Compania 
		AND C.Tip_Compania=@Tip_Compania AND C.Tip_Estado='1' ORDER BY C.Dsc_Compania ASC
	END ELSE BEGIN
		SELECT MVC.Cod_Compania,C.Tip_Compania,C.Dsc_Compania,C.Dsc_Ruc,MVC.Cod_Modalidad_Venta,MVC.Dsc_Valor_Acumulado FROM TUA_ModVentaComp MVC,TUA_Compania C
		WHERE MVC.Cod_Modalidad_Venta=@Cod_Modalidad_Venta AND MVC.Cod_Compania=C.Cod_Compania 
		AND C.Tip_Estado='1' ORDER BY C.Dsc_Compania ASC
	END
END ELSE
BEGIN
	if(@Tip_Compania IS NOT NULL AND @Tip_Compania='')--listar solo aerolineas que hacen venta contado
	begin
		SELECT distinct(MVC.Cod_Compania),C.Tip_Compania,C.Dsc_Compania,C.Dsc_Ruc,'' Cod_Modalidad_Venta,MVC.Dsc_Valor_Acumulado FROM TUA_ModVentaComp MVC,TUA_Compania C
		WHERE (MVC.Cod_Modalidad_Venta='M0001' OR MVC.Cod_Modalidad_Venta='M0003') AND MVC.Cod_Compania=C.Cod_Compania 
		 AND MVC.Cod_Compania='C000000000' 
		UNION ALL
		SELECT distinct(MVC.Cod_Compania),C.Tip_Compania,C.Dsc_Compania,C.Dsc_Ruc,'' Cod_Modalidad_Venta,MVC.Dsc_Valor_Acumulado FROM TUA_ModVentaComp MVC,TUA_Compania C
		WHERE (MVC.Cod_Modalidad_Venta='M0001' OR MVC.Cod_Modalidad_Venta='M0003') AND MVC.Cod_Compania=C.Cod_Compania 
		AND C.Tip_Estado='1' AND C.Tip_Compania='1' AND MVC.Cod_Compania<>'C000000000' ORDER BY C.Dsc_Compania ASC
		
	end
	else begin --listando todas las compañias para generacion de archivo de ventas
		SELECT MVC.Cod_Compania,C.Tip_Compania,C.Dsc_Compania,C.Dsc_Ruc,MVC.Cod_Modalidad_Venta,MVC.Dsc_Valor_Acumulado FROM TUA_ModVentaComp MVC,TUA_Compania C, TUA_ModalidadVenta MV
		WHERE MVC.Cod_Compania=C.Cod_Compania AND MV.Cod_Modalidad_Venta=MVC.Cod_Modalidad_Venta
		ORDER BY MVC.Cod_Modalidad_Venta
	end
END






GO



ALTER PROCEDURE [dbo].[usp_vta_cns_monedasinter_sel]

AS
SELECT DISTINCT M.Cod_Moneda,
	M.Dsc_Moneda,
	M.Dsc_Simbolo,
	M.Log_Usuario_Mod,
	M.Log_Fecha_Mod,
	M.Log_Hora_Mod
FROM TUA_Moneda M,TUA_TasaCambio TC WHERE M.Tip_Estado='1' 
 and (TC.Cod_Moneda=M.Cod_Moneda AND M.Cod_Moneda<>'SOL' AND TC.Tip_Estado='1' and M.Cod_Moneda in (select Cod_Moneda from TUA_TasaCambio where Tip_Estado='1' and Tip_Cambio='C') and M.Cod_Moneda in (select Cod_Moneda from TUA_TasaCambio where Tip_Estado='1' and Tip_Cambio='V'))




GO

ALTER PROCEDURE [dbo].[usp_vta_cns_perfilrolbyuser_sel](
	@Cod_Usuario char(7),
	@Cod_Modulo char(3)
)
AS
 
SELECT PR.Cod_Modulo AS Cod_Modulo, PR.Cod_Proceso AS Cod_Proceso,
PR.Cod_Rol AS Cod_Rol,PR.Flg_Permitido AS Flg_Permitido
FROM TUA_PerfilRol PR, TUA_UsuarioRol UR WHERE
UR.Cod_Usuario=@Cod_Usuario AND UR.Cod_Rol=PR.Cod_Rol AND PR.Cod_Modulo=@Cod_Modulo

GO



ALTER PROCEDURE [dbo].[usp_vta_cns_precioticket_sel](--[usp_vta_cns_precioticket_sel] 'N','A','N'
@Tip_Vuelo char(1),
@Tip_Pasajero char(1),
@Tip_Trasbordo char(1)
)AS

SELECT TT.Cod_Tipo_Ticket,TT.Tip_Vuelo,TT.Tip_Pasajero,TT.Tip_Trasbordo,TT.Dsc_Tipo_Ticket,
TT.Cod_Moneda,TT.Imp_Precio,TT.Tip_Estado,TT.Log_Usuario_Mod,TT.Log_Fecha_Mod,TT.Log_Hora_Mod, 
M.Dsc_Simbolo,M.Dsc_Moneda FROM TUA_TipoTicket TT,TUA_MONEDA M
WHERE TT.Tip_Vuelo=@Tip_Vuelo AND TT.Tip_Pasajero=@Tip_Pasajero AND TT.Tip_Trasbordo=@Tip_Trasbordo
AND TT.Cod_Moneda=M.Cod_Moneda


GO


--[usp_vta_cns_vuelosprogramados_sel] '1','20100105','I'
ALTER PROCEDURE [dbo].[usp_vta_cns_vuelosprogramados_sel](
@Cod_Compania char(10)=NULL,
@fecha char(8),
@Tip_Vuelo char(1)
)AS
declare @modo char(1),
@Cod_Modalidad char(5)

if(Len(@Cod_Compania)=1)
begin
	set @modo=@Cod_Compania
	set @Cod_Compania=null
end 
--else begin
--	set @modo=substring(@Cod_Compania,11,1)
--end

if(@modo='0')
	set @Cod_Modalidad='M0001'
else set @Cod_Modalidad='M0003'

IF(@Cod_Compania IS NULL)
BEGIN
	IF(EXISTS(SELECT Dsc_Vuelo FROM TUA_VueloProgramado WHERE @fecha=Fch_Vuelo AND Tip_Estado='1' AND Tip_Vuelo=@Tip_Vuelo))
	SELECT C.Dsc_Compania,VP.Cod_Compania,VP.Num_Vuelo,Substring(VP.Fch_Vuelo,7,2)+'/'+Substring(VP.Fch_Vuelo,5,2)+'/'+Substring(VP.Fch_Vuelo,1,4) as Fch_Vuelo,Substring(VP.Hor_Vuelo,1,2)+':'+Substring(VP.Hor_Vuelo,3,2)+':'+Substring(VP.Hor_Vuelo,5,2) as Hor_Vuelo,VP.Dsc_Vuelo,VP.Tip_Vuelo,VP.Tip_Estado,VP.Dsc_Destino FROM TUA_VueloProgramado VP,TUA_Compania C,TUA_ModVentaComp MVC
	WHERE @fecha=VP.Fch_Vuelo AND VP.Tip_Estado='1' AND VP.Tip_Vuelo=@Tip_Vuelo AND C.Cod_Compania=VP.Cod_Compania AND MVC.Cod_Compania=C.Cod_Compania AND MVC.Cod_Modalidad_Venta=@Cod_Modalidad and C.Tip_Estado='1'
	ELSE
	SELECT C.Dsc_Compania,VT.Cod_Compania,VT.Num_Vuelo,Substring(VT.Fch_Vuelo,7,2)+'/'+Substring(VT.Fch_Vuelo,5,2)+'/'+Substring(VT.Fch_Vuelo,1,4) as Fch_Vuelo,Substring(VT.Hor_Vuelo,1,2)+':'+Substring(VT.Hor_Vuelo,3,2)+':'+Substring(VT.Hor_Vuelo,5,2) as Hor_Vuelo,VT.Dsc_Vuelo,VT.Tip_Vuelo,VT.Tip_Estado,VT.Dsc_Destino FROM  TUA_VuelosTemporada VT,TUA_Compania C,TUA_ModVentaComp MVC
	WHERE @fecha=VT.Fch_Vuelo AND VT.Tip_Estado='1' AND VT.Tip_Vuelo=@Tip_Vuelo AND C.Cod_Compania=VT.Cod_Compania AND MVC.Cod_Compania=C.Cod_Compania AND MVC.Cod_Modalidad_Venta=@Cod_Modalidad and C.Tip_Estado='1'
END ELSE BEGIN
	IF(EXISTS(SELECT Dsc_Vuelo FROM TUA_VueloProgramado WHERE @Cod_Compania=Cod_Compania AND @fecha=Fch_Vuelo AND Tip_Estado='1' AND Tip_Vuelo=@Tip_Vuelo))
	SELECT C.Dsc_Compania,VP.Cod_Compania,VP.Num_Vuelo,Substring(VP.Fch_Vuelo,7,2)+'/'+Substring(VP.Fch_Vuelo,5,2)+'/'+Substring(VP.Fch_Vuelo,1,4) as Fch_Vuelo,Substring(VP.Hor_Vuelo,1,2)+':'+Substring(VP.Hor_Vuelo,3,2)+':'+Substring(VP.Hor_Vuelo,5,2) as Hor_Vuelo,VP.Dsc_Vuelo,VP.Tip_Vuelo,VP.Tip_Estado,VP.Dsc_Destino FROM TUA_VueloProgramado VP,TUA_Compania C
	WHERE @Cod_Compania=VP.Cod_Compania AND @fecha=VP.Fch_Vuelo AND VP.Tip_Estado='1' AND VP.Tip_Vuelo=@Tip_Vuelo AND C.Cod_Compania=VP.Cod_Compania and C.Tip_Estado='1'
	ELSE
	SELECT C.Dsc_Compania,VT.Cod_Compania,VT.Num_Vuelo,Substring(VT.Fch_Vuelo,7,2)+'/'+Substring(VT.Fch_Vuelo,5,2)+'/'+Substring(VT.Fch_Vuelo,1,4) as Fch_Vuelo,Substring(VT.Hor_Vuelo,1,2)+':'+Substring(VT.Hor_Vuelo,3,2)+':'+Substring(VT.Hor_Vuelo,5,2) as Hor_Vuelo,VT.Dsc_Vuelo,VT.Tip_Vuelo,VT.Tip_Estado,VT.Dsc_Destino FROM  TUA_VuelosTemporada VT,TUA_Compania C
	WHERE @Cod_Compania=VT.Cod_Compania AND @fecha=VT.Fch_Vuelo AND VT.Tip_Estado='1' AND VT.Tip_Vuelo=@Tip_Vuelo AND C.Cod_Compania=VT.Cod_Compania and C.Tip_Estado='1'
END







GO








