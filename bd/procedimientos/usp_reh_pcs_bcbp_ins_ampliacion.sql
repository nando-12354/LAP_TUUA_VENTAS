-- =============================================
-- Author:		C.Huallpa DMS
-- Create date: 2018-07-01
-- Description:	Realiza la inserción histórica de BP Rehabilitados
-- =============================================

CREATE PROCEDURE [dbo].[usp_reh_pcs_bcbp_ins_ampliacion]   
@ListaBCBPs varchar(max),  
@Causal_Rehabilitacion varchar(5),   
@Causal_Rehabilitaciones varchar(max),  
@Flg_Tipo char(1),  
@Log_Usuario_Mod char(7),  
@OutputListaBCBPs varchar(max) OUTPUT,  
@OutputListaNroOpe varchar(max) OUTPUT,
@OutLogFechaRehabi varchar(20) OUTPUT,
@estado_asoc varchar(max),
@nro_vuelo_asoc varchar(max) = null,
@nro_asiento_asoc varchar(max) = null,
@pasajero_asoc varchar(max) = null,
@fecha_vuelo_asoc varchar(max) = null,
@compania_asoc varchar(max) = null,	
@Lst_Bloqueados varchar(max) = null,	
@Cod_Modulo_Mod char(3)=null,  
@Cod_SubModulo_Mod char(5)=null
AS  
BEGIN

--DECLARE @ListaBCBPs varchar(max),  
--@Causal_Rehabilitacion varchar(5),   
--@Causal_Rehabilitaciones varchar(max),  
--@Flg_Tipo char(1),  
--@Log_Usuario_Mod char(7),  
--@OutputListaBCBPs varchar(max),  
--@OutputListaNroOpe varchar(max),
--@OutLogFechaRehabi varchar(20),
--@estado_asoc varchar(max),
--@nro_vuelo_asoc varchar(max) = null,
--@nro_asiento_asoc varchar(max) = null,
--@pasajero_asoc varchar(max) = null,
--@fecha_vuelo_asoc varchar(max) = null,
--@compania_asoc varchar(max) = null,	
--@Lst_Bloqueados varchar(max) = null,	
--@Cod_Modulo_Mod char(3)=null,  
--@Cod_SubModulo_Mod char(5)=null

--SET @ListaBCBPs='38995130|38995131|'
--SET @Causal_Rehabilitacion='7'
--SET @Causal_Rehabilitaciones=NULL
--SET @Flg_Tipo='1'
--SET @Log_Usuario_Mod='U000182'
--SET @estado_asoc=NULL
--SET @nro_vuelo_asoc=NULL
--SET @nro_asiento_asoc=NULL
--SET @pasajero_asoc=NULL
--SET @fecha_vuelo_asoc=NULL
--SET @compania_asoc='AVIANCA-AEROVIAS DEL CONTINENTE AMERICANO'
--SET @Lst_Bloqueados='0|0|'
--SET @Cod_Modulo_Mod='005'
--SET @Cod_SubModulo_Mod='E0471'
  
DECLARE @strPistaNueva varchar(4000),@strPistaOrig varchar(4000),@Cod_Rol varchar(4000),@strPK varchar(1000),@Tip_Operacion char(1)  
SET @Cod_Rol= dbo.fdb_TUA_ROLES(@Log_Usuario_Mod)  
SET NOCOUNT ON;  
BEGIN TRANSACTION  
  
BEGIN TRY  
	DECLARE @Log_Fecha_Mod varchar(8),@Log_Hora_Mod varchar(6)  
	SET @Log_Fecha_Mod = (SELECT SUBSTRING([dbo].[NTPFunction](),1,8))  
	SET @Log_Hora_Mod  = (SELECT SUBSTRING([dbo].[NTPFunction](),10,15))   
	--CMONTES 20-12-2010
	SET @OutLogFechaRehabi = @Log_Fecha_Mod+@Log_Hora_Mod
	--BEGIN ESILVA  
	DECLARE @Param_Dias_Venc int,@Fch_Vencimiento datetime,@Param_Check_Rehab varchar(20),@ID_PARAM_NO_REHAB char (2), @ID_PARAM_NO_USO char (2)  
	SET @ID_PARAM_NO_REHAB = ',R'  
	SET @ID_PARAM_NO_USO = ',U' 
	--END ESILVA  
  
	DECLARE @Inicio INT,@Desc_Bcbp_Estado varchar(20),@Num_Secuencial_Bcbp bigint,@Tip_Estado CHAR(1) ,@Num_Rehabilitaciones int
	DECLARE @Cod_Compania varchar(10) ,@Num_Vuelo varchar(10),@Fch_Vuelo char(8),@Num_Asiento varchar(10),@Nom_Pasajero varchar(50)
	DECLARE @Tip_Transbordo varchar(1) ,@Tip_Ingreso varchar(1),@Cod_Num_Bcbp varchar(16),@Tip_Pasajero varchar(1)
	DECLARE @Tip_Vuelo varchar(1), @Sub_Lista_Block VARCHAR(MAX)
	DECLARE @Sub_ListaBCBPs VARCHAR(MAX),@Num_ListaBCBPs INT,@Num_Longitud BIGINT,@Num_Ult_Valor BIGINT,@NUEVOCODIGO VARCHAR(12),@Num_Secuencial bigint 
	DECLARE @Flg_Bloqueado char(1)
	 
	SET @Inicio = 1 
	SET @Desc_Bcbp_Estado=(SELECT Dsc_Campo FROM TUA_ListaDeCampos WHERE Nom_Campo='EstadoBcbp' AND Cod_Campo='R')  
	SET @OutputListaBCBPs=''  
	SET @Sub_ListaBCBPs=SUBSTRING(@ListaBCBPs,1,LEN(@ListaBCBPs)-1)
	SET @Num_ListaBCBPs=(SELECT COUNT(1) FROM dbo.fdb_TUA_SplitDelimited(@Sub_ListaBCBPs,'|'))  
	SET @Num_Longitud=(select Num_Longitud FROM TUA_Secuencial	WHERE Cod_Secuencial='NRO_OPERA_REHAB_BCBP')
	SET @Num_Ult_Valor=(select Num_Ultimo_Valor FROM TUA_Secuencial	WHERE Cod_Secuencial='NRO_OPERA_REHAB_BCBP')
	SET @Num_Ult_Valor=@Num_Ult_Valor+1
	SET @NUEVOCODIGO=[dbo].fdb_TUA_GENERATECODE(@Num_Longitud,@Num_Ult_Valor,null);-- este es el numero de operacion para el conjunto de BPs que se rehabilitan.

	--BEGIN ESILVA  
	SET @Param_Check_Rehab = ( select [dbo].[fdb_TUA_GET_PARAMETRO_BOARDING](@Num_Secuencial_Bcbp,'Z0') ) --al incicio dado que es un parametro general 

	IF @Flg_Tipo='0'  
	BEGIN
		DECLARE @Sub_Causal_Rehabilitaciones VARCHAR(MAX)  
		SET @Sub_Causal_Rehabilitaciones = SUBSTRING(@Causal_Rehabilitaciones,1,LEN(@Causal_Rehabilitaciones)-1) 
		SET @Sub_Lista_Block= SUBSTRING(@Lst_Bloqueados,1,LEN(@Lst_Bloqueados)-1) 
  
		DECLARE @Num_Causal_Rehabilitaciones INT  
		SELECT @Num_Causal_Rehabilitaciones=COUNT(*) FROM dbo.fdb_TUA_SplitDelimited(@Sub_Causal_Rehabilitaciones,'|')  
   
		IF (@Num_ListaBCBPs<>@Num_Causal_Rehabilitaciones)  
		BEGIN  
			ROLLBACK TRANSACTION  
			--RETURN 0  
		END  
  
		DECLARE @Causal_Rehabilitacion_Aux VARCHAR(5)
		
		--------------  
		WHILE (@Inicio<=@Num_ListaBCBPs)  
		BEGIN 
			SELECT @Flg_Bloqueado=Valor FROM dbo.fdb_TUA_SplitDelimited(@Sub_Lista_Block,'|') WHERE Id=@Inicio  
			SELECT @Num_Secuencial_Bcbp=Valor FROM dbo.fdb_TUA_SplitDelimited(@Sub_ListaBCBPs,'|') WHERE Id=@Inicio  
			SELECT @Causal_Rehabilitacion_Aux=Valor FROM dbo.fdb_TUA_SplitDelimited(@Sub_Causal_Rehabilitaciones,'|') WHERE Id=@Inicio  
			SELECT @Cod_Compania = Cod_Compania, @Num_Vuelo = Num_Vuelo, @Fch_Vuelo = Fch_Vuelo,   
			@Num_Asiento = Num_Asiento, @Nom_Pasajero = Nom_Pasajero,  
			@Tip_Estado=Tip_Estado, @Num_Rehabilitaciones = Num_Rehabilitaciones,  
			@Fch_Vencimiento = Fch_Vencimiento, @Tip_Transbordo = Tip_Trasbordo, @Tip_Ingreso = Tip_Ingreso, @Cod_Num_Bcbp = Cod_Numero_Bcbp, @Tip_Pasajero = Tip_Pasajero, @Tip_Vuelo =  Tip_Vuelo 
			FROM Tua_BoardingBcbp WHERE Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp  
			IF @Tip_Estado<>'U'  --comprobamos en caso alguien lo haya rehabilitado. o haya cambiado de estado.  
			BEGIN  
				SET @OutputListaBCBPs = CAST(@Num_Secuencial_Bcbp as varchar) + @ID_PARAM_NO_USO + '|' + @OutputListaBCBPs  
				SET @Inicio=@Inicio+1  
				CONTINUE  
			END  
			IF RTRIM(@Param_Check_Rehab) = '0' --FLAG PERMISO REHABILITACION DENEGADO  
			BEGIN  
				SET @OutputListaBCBPs = CAST(@Num_Secuencial_Bcbp as varchar) + @ID_PARAM_NO_REHAB + '|' + @OutputListaBCBPs  
				SET @Inicio=@Inicio+1  
				CONTINUE  
			END  
			--END ESILVA  
			SET @Num_Rehabilitaciones = @Num_Rehabilitaciones + 1   
			--BEGIN ESILVA  
			SET @Param_Dias_Venc = ( select [dbo].[fdb_TUA_GET_PARAMETRO_BOARDING](@Num_Secuencial_Bcbp,'DB') )  
			SET @Fch_Vencimiento = convert(datetime, @Log_Fecha_Mod) + @Param_Dias_Venc  
			--END ESILVA  
			--Auditoria  
			SET @strPK = 'Num_Secuencial_Bcbp�'+cast(@Num_Secuencial_Bcbp as varchar(50))  
			SET @Tip_Operacion='3'  
			EXEC usp_audit_tabla_sel  @strPK, 'TUA_BoardingBcbp', @strPistaOrig OUTPUT  
    
			UPDATE Tua_BoardingBcbp   
			SET Log_Usuario_Mod = @Log_Usuario_Mod, Log_Fecha_Mod = @Log_Fecha_Mod, Log_Hora_Mod = @Log_Hora_Mod,  
				Tip_Estado='R', Num_Rehabilitaciones = @Num_Rehabilitaciones,  
				Fch_Vencimiento = @Fch_Vencimiento ,Flg_Sincroniza='0' ,
				Flg_Bloqueado=@Flg_Bloqueado,Fch_Rehabilitacion=@Log_Fecha_Mod+@Log_Hora_Mod,
				Num_Proceso_Rehab=@NUEVOCODIGO
			WHERE Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp 

			EXEC usp_audit_tabla_sel  @strPK, 'TUA_BoardingBcbp', @strPistaNueva OUTPUT  
			EXEC usp_audit_tabla_ins  @Cod_Modulo_Mod,@Cod_SubModulo_Mod,@Log_Usuario_Mod,@Cod_Rol,  
					@Tip_Operacion,'TUA_BoardingBcbp',@strPistaNueva,@strPistaOrig  
			SELECT @Num_Secuencial = Max(Num_Secuencial) FROM TUA_BoardingBcbpEstHist WHERE Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp  
			IF @Num_Secuencial IS Null --En teoria deberia existir  
			BEGIN  
				SET @Num_Secuencial = 0  
			END  
			SET @Num_Secuencial = @Num_Secuencial + 1  
			--katy
			INSERT TUA_BoardingBcbpEstHist   
			(Num_Secuencial_Bcbp, Num_Secuencial, Cod_Compania, Num_Vuelo, Fch_Vuelo, Num_Asiento, Nom_Pasajero,  
			Tip_Estado, Dsc_Boarding_Estado, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, Causal_Rehabilitacion, Nro_Operacion)  
			VALUES  
			(@Num_Secuencial_Bcbp, @Num_Secuencial, @Cod_Compania, @Num_Vuelo, @Fch_Vuelo, @Num_Asiento, @Nom_Pasajero,  
			'R', @Desc_Bcbp_Estado, @Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod, @Causal_Rehabilitacion_Aux, @Num_Ult_Valor) 
			--Auditoria  
			SET @strPK = 'Num_Secuencial_Bcbp�'+cast(@Num_Secuencial_Bcbp as varchar(50))+'|Num_Secuencial�'+cast(@Num_Secuencial as varchar(50))  
			SET @Tip_Operacion='2'  
			EXEC usp_audit_tabla_sel  @strPK, 'TUA_BoardingBcbpEstHist', @strPistaNueva OUTPUT  
			EXEC usp_audit_tabla_ins  @Cod_Modulo_Mod,@Cod_SubModulo_Mod,@Log_Usuario_Mod,@Cod_Rol,@Tip_Operacion,'TUA_BoardingBcbpEstHist',@strPistaNueva,NULL 
		END
	END  
	IF @Flg_Tipo='1'  
	BEGIN 
		SET @Sub_Lista_Block= SUBSTRING(@Lst_Bloqueados,1,LEN(@Lst_Bloqueados)-1) 
		SET @Inicio = 1;
		WHILE (@Inicio<=@Num_ListaBCBPs)  
		BEGIN 
			SELECT @Flg_Bloqueado=Valor FROM dbo.fdb_TUA_SplitDelimited(@Sub_Lista_Block,'|') WHERE Id=@Inicio  
			SELECT @Num_Secuencial_Bcbp=Valor FROM dbo.fdb_TUA_SplitDelimited(@Sub_ListaBCBPs,'|') WHERE Id=@Inicio  
			SELECT @Cod_Compania = Cod_Compania, @Num_Vuelo = Num_Vuelo, @Fch_Vuelo = Fch_Vuelo,   
			@Num_Asiento = Num_Asiento, @Nom_Pasajero = Nom_Pasajero,  
			@Tip_Estado=Tip_Estado, @Num_Rehabilitaciones = Num_Rehabilitaciones,  
			@Fch_Vencimiento = Fch_Vencimiento, @Tip_Transbordo = Tip_Trasbordo, @Tip_Ingreso = Tip_Ingreso, 
			@Cod_Num_Bcbp = Cod_Numero_Bcbp, @Tip_Pasajero = Tip_Pasajero, @Tip_Vuelo =  Tip_Vuelo  
			FROM Tua_BoardingBcbp WHERE Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp  
			
			--INSERT INTO Track(valor) VALUES('@Num_ListaBCBPs: '+CONVERT(VARCHAR,@Num_ListaBCBPs))
			--INSERT INTO Track(valor) VALUES('@Inicio: '+CONVERT(VARCHAR,@Inicio))

			SET @Inicio=@Inicio+1;

			IF @Tip_Estado<>'U'  --comprobamos en caso alguien lo haya rehabilitado. o haya cambiado de estado.  
			BEGIN  
				SET @OutputListaBCBPs = CAST(@Num_Secuencial_Bcbp AS VARCHAR) + @ID_PARAM_NO_USO + '|' + @OutputListaBCBPs  
				--SET @Inicio=@Inicio+1  
				--CONTINUE  
			END
			--BEGIN ESILVA  
			IF RTRIM(@Param_Check_Rehab) = '0' --FLAG PERMISO REHABILITACION DENEGADO  
			BEGIN  
				SET @OutputListaBCBPs = CAST(@Num_Secuencial_Bcbp as varchar) + @ID_PARAM_NO_REHAB + '|' + @OutputListaBCBPs  
				--SET @Inicio=@Inicio+1  
				--CONTINUE  
			END
			--END ESILVA  
			SET @Num_Rehabilitaciones = @Num_Rehabilitaciones + 1   
			--BEGIN ESILVA  
			SET @Param_Dias_Venc = (SELECT [dbo].[fdb_TUA_GET_PARAMETRO_BOARDING](@Num_Secuencial_Bcbp,'DB') )  
			SET @Fch_Vencimiento = convert(datetime, @Log_Fecha_Mod) + @Param_Dias_Venc  
	--		--END ESILVA  
	--		--Auditoria  
			SET @strPK = 'Num_Secuencial_Bcbp�'+cast(@Num_Secuencial_Bcbp as varchar)  
			SET @Tip_Operacion='3'  
			EXEC usp_audit_tabla_sel  @strPK, 'TUA_BoardingBcbp', @strPistaOrig OUTPUT  

			UPDATE Tua_BoardingBcbp SET Log_Usuario_Mod = @Log_Usuario_Mod, Log_Fecha_Mod = @Log_Fecha_Mod, Log_Hora_Mod = @Log_Hora_Mod,  
			Tip_Estado='R', Num_Rehabilitaciones = @Num_Rehabilitaciones,  
			Fch_Vencimiento = @Fch_Vencimiento ,Flg_Sincroniza='0', Flg_Bloqueado=@Flg_Bloqueado, Fch_Rehabilitacion=@Log_Fecha_Mod+@Log_Hora_Mod, 
			Num_Proceso_Rehab=@NUEVOCODIGO
			WHERE Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp  

			EXEC usp_audit_tabla_sel  @strPK, 'TUA_BoardingBcbp', @strPistaNueva OUTPUT  
			EXEC usp_audit_tabla_ins  @Cod_Modulo_Mod,@Cod_SubModulo_Mod,@Log_Usuario_Mod,@Cod_Rol,  
										@Tip_Operacion,'TUA_BoardingBcbp',@strPistaNueva,@strPistaOrig                                                        
			SELECT @Num_Secuencial = Max(Num_Secuencial) FROM TUA_BoardingBcbpEstHist WHERE Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp  
			IF @Num_Secuencial IS Null --En teoria deberia existir  
			BEGIN  
	--			--PRINT 'POR AQUI'  
				SET @Num_Secuencial = 0  
			END  
			SET @Num_Secuencial = @Num_Secuencial + 1  
			INSERT TUA_BoardingBcbpEstHist   
			(Num_Secuencial_Bcbp, Num_Secuencial, Cod_Compania, Num_Vuelo, Fch_Vuelo, Num_Asiento, Nom_Pasajero,  
			Tip_Estado, Dsc_Boarding_Estado, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, Causal_Rehabilitacion, Nro_Operacion)  
			VALUES  
			(@Num_Secuencial_Bcbp, @Num_Secuencial, @Cod_Compania, @Num_Vuelo, @Fch_Vuelo, @Num_Asiento, @Nom_Pasajero,  
			'R', @Desc_Bcbp_Estado, @Log_Usuario_Mod, @Log_Fecha_Mod, @Log_Hora_Mod, @Causal_Rehabilitacion, @Num_Ult_Valor)  
			--Auditoria  
			SET @strPK = 'Num_Secuencial_Bcbp�'+CAST(@Num_Secuencial_Bcbp AS VARCHAR(50))+'|Num_Secuencial�'+cast(@Num_Secuencial AS VARCHAR(50))  
			SET @Tip_Operacion='2'  
			EXEC usp_audit_tabla_sel  @strPK, 'TUA_BoardingBcbpEstHist', @strPistaNueva OUTPUT  
			EXEC usp_audit_tabla_ins  @Cod_Modulo_Mod,@Cod_SubModulo_Mod,@Log_Usuario_Mod,@Cod_Rol,  
										@Tip_Operacion,'TUA_BoardingBcbpEstHist',@strPistaNueva,null   
		END  
	END  
	IF RTRIM(@Param_Check_Rehab) = '1'
	BEGIN
		SET @OutputListaNroOpe = @Num_Ult_Valor
	END
	IF @OutputListaNroOpe <> '' or @OutputListaNroOpe is not null
	BEGIN
		UPDATE TUA_Secuencial SET Num_Ultimo_Valor=@Num_Ult_Valor WHERE Cod_Secuencial='NRO_OPERA_REHAB_BCBP'  
	END
END TRY  
BEGIN CATCH  
	SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState,ERROR_PROCEDURE() AS ErrorProcedure,ERROR_LINE() AS ErrorLine,ERROR_MESSAGE() AS ErrorMessage;  
	SET @OutputListaBCBPs = @ListaBCBPs     
	SET @OutputListaNroOpe = ''
	SET @OutLogFechaRehabi = ''
	IF @@TRANCOUNT > 0  
	ROLLBACK TRANSACTION;  
	RETURN 0  
END CATCH;  
  
IF @@TRANCOUNT > 0  
COMMIT TRANSACTION;  
  
SET NOCOUNT OFF  
RETURN 1  
END












GO


