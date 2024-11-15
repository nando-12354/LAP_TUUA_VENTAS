-- =============================================
-- Author:		Hiper
-- Create date: 2011-01-06
-- Description:	Insertar registro de boardingpass en BD TUUA
-- Reviews:
-- 20.10.2015 (HIPER) boarding pass no migrados por tipo de vuelo incorrecto
-- =============================================

ALTER PROCEDURE [dbo].[usp_scz_pcs_boardingbcbp_ins]
(
		@Cod_Compania char(10), 
		@Num_Vuelo varchar(10),
		@Fch_Vuelo char(8), 
		@Num_Asiento varchar(10), 
		@Nom_Pasajero varchar(50), 
		@Dsc_Trama_Bcbp varchar(500),
		@Log_Usuario_Mod char(7), 
		@Log_Fecha_Mod char(8), 
		@Log_Hora_Mod char(6), 
		@Tip_Ingreso char(1), 
		@Tip_Estado char(1) ,
		@Cod_Equipo_Mod varchar(20)= NULL,
		@Dsc_Boarding_Estado varchar(20)=NULL,
		@Num_Rehabilitaciones INT, 
		@Cod_Unico_Bcbp varchar(20), 
		@Fch_Vencimiento datetime, 
		@Fch_Creacion char(8),
		@Hor_Creacion char(6), 
		@Cod_Unico_Bcbp_Rel varchar(20), 
		@Flg_Sincroniza char(1)=Null,
		@Tip_Pasajero char(1), 
		@Tip_Vuelo char(1), 
		@Tip_Trasbordo char(1),
		@Flg_Tipo_Bcbp char(1),
		@Num_Secuencial_Bcbp_Rel bigint, 
		@Num_Secuencial_Bcbp_Rel_Sec bigint,
		@Nro_Boarding char(5), 
		@Dsc_Destino char(3),
		@Cod_Eticket varchar(20), 
		@Num_Proceso_Rehab varchar(20), 
		@Flg_Bloqueado char(1), 
		@Flg_WSError char(1), 
		@Flg_Incluye_Tuua char(1), 
		@Dsc_Observacion varchar(255)=null,
		@Fch_Rehabilitacion char(14), 
		@Num_Airline_Code char(3), 
		@Num_Document_Form char(10),
		@Cod_Error char(1) OUTPUT
)
AS
BEGIN
    DECLARE @Num_Secuencial int,
	@Num_Secuencial_Bcbp int,
--	@strPistaNueva nvarchar(4000),
--  @strPK  varchar(1000),
    --@Tip_Operacion char(1),  
    --@Fch_Creacion  char(8),
    --@Hor_Creacion  char(6),
	--para Cod_Numero_Bcbp
	@Num_Serie_Tuua char(3),
	@Num_Secuencia_Tuua char(9),
	@Num_Old_Ticket char(16),
	@PosDig varchar(20),
	@Cod_Numero_Bcbp char(16),
	--nuevo TUUA BCBP
	@Cod_Moneda char(3),
	@Imp_Precio numeric(12,2),
	@Imp_Tasa_Venta numeric(8,4),
	@Imp_Tasa_Compra numeric(8,4),
	@Dsc_Message varchar(200)
	SET @Cod_Error = '1'
	--print 'Insert 1'
    
    --INICIO OBTENIENDO DATOS DE TASA TUUA
	SELECT @Cod_Moneda=Cod_Moneda,@Imp_Precio=Imp_Precio FROM TUA_TipoTicket (NOLOCK) WHERE Tip_Vuelo=@Tip_Vuelo AND Tip_Pasajero=@Tip_Pasajero AND Tip_Trasbordo='N' AND Tip_Estado='1'
	IF @Cod_Moneda IS NULL
		SELECT @Cod_Moneda=Cod_Moneda,@Imp_Precio=Imp_Precio FROM TUA_TipoTicket (NOLOCK) WHERE Tip_Vuelo=@Tip_Vuelo AND Tip_Pasajero='A' AND Tip_Trasbordo='N' AND Tip_Estado='1'
	SET @Imp_Tasa_Venta=(SELECT Imp_Cambio_Actual FROM TUA_TasaCambio (NOLOCK) WHERE Cod_Moneda='DOL' AND Tip_Cambio='V' AND Tip_Estado='1')
	IF(@Cod_Moneda<>'SOL' AND @Cod_Moneda<>'DOL')
		SET @Imp_Tasa_Compra=(SELECT Imp_Cambio_Actual FROM TUA_TasaCambio (NOLOCK) WHERE Cod_Moneda=@Cod_Moneda AND Tip_Cambio='C' AND Tip_Estado='1')
	--FIN OBTENIENDO DATOS DE TASA TUUA
    --Insertar Boarding
	IF EXISTS(select Cod_Numero_Bcbp from TUA_BoardingBcbp (NOLOCK) where Cod_Compania=@Cod_Compania AND Num_Vuelo=@Num_Vuelo AND Fch_Vuelo=@Fch_Vuelo AND Num_Asiento=@Num_Asiento AND Nom_Pasajero=@Nom_Pasajero and Tip_estado='U')
	BEGIN
--		ROLLBACK TRANSACTION BCBP;  
		SET @Cod_Error = '1'
		RETURN --ERROR
	END
	--print 'Insert 2'
	SET NOCOUNT ON 
	BEGIN TRANSACTION BCBP
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	BEGIN TRY  

		SELECT @Num_Secuencial_Bcbp = Num_Ultimo_Valor + 1 FROM TUA_Secuencial_BP (ROWLOCK) WHERE Cod_Secuencial ='BCBP'
		
		set @Num_Serie_Tuua=(select Dsc_Valor from TUA_ModalidadAtrib (NOLOCK) where Cod_Modalidad_Venta='M0002' AND Cod_Atributo='ZD')
		SET @Num_Secuencia_Tuua = CONVERT(varchar(9), @Num_Secuencial_Bcbp)
		SET @Num_Secuencia_Tuua = LEFT(REPLICATE('0',9-LEN(@Num_Secuencia_Tuua)) + @Num_Secuencia_Tuua ,9)
		set @Num_Old_Ticket=(select Num_Ultimo_SEAE from TUA_Secuencial_BP (ROWLOCK) WHERE Cod_Secuencial ='BCBP')
		set @PosDig =(SELECT Valor FROM TUA_ParameGeneral (NOLOCK) WHERE Identificador='DSNT')
		SET @Cod_Numero_Bcbp = [dbo].fdb_TUUA_NUMTICKET(@Num_Serie_Tuua,@Num_Secuencia_Tuua,@Num_Old_Ticket,@PosDig)
		
		SET @Num_Old_Ticket=@Cod_Numero_Bcbp
		/*
		print 'Insert 3'
		print '@Cod_Error: ' + @Cod_Error
		print '@Cod_Numero_Bcbp: '+@Cod_Numero_Bcbp
		print '@Num_Secuencial_Bcbp: ' +@Num_Secuencial_Bcbp
		print '@Num_Serie_Tuua: ' +@Num_Serie_Tuua
		print '@Num_Secuencia_Tuua:' +@Num_Secuencia_Tuua
		print '@Num_Old_Ticket:' + @Num_Old_Ticket
		*/
		INSERT INTO TUA_BoardingBcbp(Num_Secuencial_Bcbp, Cod_Compania, Num_Vuelo, Fch_Vuelo, Num_Asiento, Nom_Pasajero,
									 Dsc_Trama_Bcbp, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod,
									 Tip_Ingreso, Tip_Estado,Num_Rehabilitaciones, Cod_Unico_Bcbp,
									 Fch_Creacion, Hor_Creacion, Cod_Unico_Bcbp_Rel, Flg_Sincroniza,
									 Tip_Pasajero, Tip_Vuelo, Tip_Trasbordo, Cod_Numero_Bcbp, Num_Serie, Num_Secuencial,
									 Flg_Tipo_Bcbp, Num_Secuencial_Bcbp_Rel, Num_Secuencial_Bcbp_Rel_Sec,
									 Nro_Boarding, Dsc_Destino, Cod_Eticket, Cod_Moneda, Imp_Precio, Imp_Tasa_Compra, Imp_Tasa_Venta, Flg_Bloqueado,
									 Flg_WSError, Flg_Incluye_Tuua, Num_Airline_Code, Num_Document_Form,
									 Fch_Vencimiento, Num_Proceso_Rehab, Fch_Rehabilitacion )
		VALUES	(@Num_Secuencial_Bcbp,@Cod_Compania, @Num_Vuelo, @Fch_Vuelo, @Num_Asiento, @Nom_Pasajero, @Dsc_Trama_Bcbp,
				@Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod, @Tip_Ingreso, @Tip_Estado, @Num_Rehabilitaciones, @Cod_Unico_Bcbp,
				@Fch_Creacion, @Hor_Creacion, @Cod_Unico_Bcbp_Rel, @Flg_Sincroniza, @Tip_Pasajero, @Tip_Vuelo, @Tip_Trasbordo,
				@Cod_Numero_Bcbp, @Num_Serie_Tuua, @Num_Secuencia_Tuua,
				@Flg_Tipo_Bcbp, @Num_Secuencial_Bcbp_Rel, @Num_Secuencial_Bcbp_Rel_Sec,
				@Nro_Boarding, @Dsc_Destino, @Cod_Eticket, @Cod_Moneda, @Imp_Precio, @Imp_Tasa_Compra, @Imp_Tasa_Venta, 
				@Flg_Bloqueado, @Flg_WSError, @Flg_Incluye_Tuua,
				@Num_Airline_Code,@Num_Document_Form,
 				@Fch_Vencimiento, @Num_Proceso_Rehab, @Fch_Rehabilitacion )
        --print 'Insert 4'
		--ACTUALIZANDO EL SECUENCIAL PARA EL SGTE BCBP
		UPDATE TUA_Secuencial_BP WITH (ROWLOCK) SET Num_Ultimo_Valor=@Num_Secuencial_Bcbp,Num_Ultimo_SEAE=@Num_Old_Ticket WHERE Cod_Secuencial ='BCBP'
        --print 'Insert 5'
	END TRY  
	BEGIN CATCH
	   SET @Dsc_Message='Error Insert BCBP Sincronismo : '+CONVERT(varchar(20), ERROR_NUMBER())+' - Mensaje Error : '+ERROR_MESSAGE()
	   print @Dsc_Message	   
	   IF @@TRANCOUNT > 0  
	   BEGIN
		ROLLBACK TRANSACTION BCBP;  		
		SET @Cod_Error = '1'
		RETURN --ERROR
	   END
	END CATCH;     

	IF @@TRANCOUNT > 0  
	   COMMIT TRANSACTION BCBP;  
	SET NOCOUNT OFF  
--print 'Insert 6'	 
		--Insertar Historico
		SET @Num_Secuencial=1
		INSERT INTO TUA_BoardingBcbpEstHist(Num_Secuencial_Bcbp,Num_Secuencial,Cod_Compania,
											Num_Vuelo,Fch_Vuelo,Num_Asiento,Nom_Pasajero,Tip_Estado,
											Dsc_Boarding_Estado,Cod_Equipo_Mod,Log_Usuario_Mod,
											Log_Fecha_Mod,Log_Hora_Mod,Causal_Rehabilitacion,
											Nro_Operacion,Dsc_Motivo)
		VALUES(@Num_Secuencial_Bcbp,@Num_Secuencial,@Cod_Compania,@Num_Vuelo,@Fch_Vuelo,@Num_Asiento,
			   @Nom_Pasajero,@Tip_Estado,@Dsc_Boarding_Estado,@Cod_Equipo_Mod,
			   @Log_Usuario_Mod,@Log_Fecha_Mod,@Log_Hora_Mod,NULL,NULL,@Dsc_Observacion)
--print 'Insert 7'
	 ---Auditar Boarding
	--   SET @strPK = '@Num_Secuencial_Bcbp©'+cast(@Num_Secuencial_Bcbp as varchar(50))+'|Cod_Compania©'+@Cod_Compania+ '|Num_Vuelo©'+ @Num_Vuelo+'|Fch_Vuelo©'+@Fch_Vuelo+'|Num_Asiento©'+@Num_Asiento+'|Nom_Pasajero©'+ @Nom_Pasajero
	--   EXEC usp_audit_tabla_sel  @strPK, 'TUA_BoardingBcbp', @strPistaNueva OUTPUT
	--   EXEC	usp_audit_tabla_ins	@Cod_Modulo_Mod,@Cod_SubModulo_Mod,@Log_Usuario_Mod,@Cod_Rol,@Tip_Operacion,
	--                            'TUA_BoardingBcbp',@strPistaNueva,NULL	
	   --Auditar Historico
	--   SET @strPK = 'Num_Secuencial_Bcbp©'+cast(@Num_Secuencial_Bcbp as varchar(50))+'|Num_Secuencial©'+cast(@Num_Secuencial as varchar(50))
	--   EXEC usp_audit_tabla_sel  @strPK, 'TUA_BoardingBcbpEstHist', @strPistaNueva OUTPUT
	--   EXEC	usp_audit_tabla_ins	@Cod_Modulo_Mod,@Cod_SubModulo_Mod,@Log_Usuario_Mod,@Cod_Rol,@Tip_Operacion,
	--                            'TUA_BoardingBcbpEstHist',@strPistaNueva,NULL
	SET @Cod_Error = '0'
	RETURN -- SUCCESS

END
GO
