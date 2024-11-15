-- =============================================
-- Author:		Hiper
-- Create date: 2011-01-06
-- Description:	Realiza la inserción de los boardingpass leidos localmente hacia el servidor
-- Reviews:
-- 20.10.2015 (HIPER) boarding pass no migrados por tipo de vuelo incorrecto
-- =============================================

ALTER PROCEDURE [dbo].[usp_scz_pcs_LocalToCentral_ins](
@IP_SERVIDOR CHAR(15),
@DBName VARCHAR(50),
@cDsc_IP VARCHAR(15),
@nInter_Reg INT,
@Modo_SIN INT,
@Dsc_Message VARCHAR(255) OUTPUT,
@Dsc_Information VARCHAR(2000) OUTPUT
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
@Fch_Vencimiento DATETIME,
@Fch_Vencimiento_ticket CHAR(8),
@Cod_Modalidad_Venta CHAR(5),
@Num_Rehabilitaciones INT,
@Cod_Tipo_Ticket CHAR(3),
@Cod_Unico_Bcbp VARCHAR(20),
@Cod_Unico_Bcbp_Rel VARCHAR(20),
@Flg_Sincroniza CHAR(1),
@Tip_Trasbordo VARCHAR(1),
@Tip_Anulacion VARCHAR(10),
@Num_Serie CHAR(3),
@Flg_Tipo_Bcbp CHAR(1),
@Num_Secuencial_Bcbp_Rel BIGINT,
@Num_Secuencial_Bcbp_Rel_Sec BIGINT,
@Nro_Boarding CHAR(5),
@Dsc_Destino CHAR(3),
@Cod_Eticket VARCHAR(20),
@Num_Proceso_Rehab VARCHAR(20),
@Flg_Bloqueado CHAR(1),
@Flg_WSError CHAR(1),
@Flg_Incluye_Tuua CHAR(1),
@Fch_Rehabilitacion CHAR(14),
@Num_Airline_Code CHAR(3),
@Num_Document_Form CHAR(10),
@Num_Referencia CHAR(10),
@Flg_Contingencia CHAR(1),
@Hor_Creacion CHAR(6),
@Num_Extensiones INT,
@UltimaVez char(14),
@Identificador CHAR(2),
@Valor VARCHAR(200),
@Tip_Estado CHAR(1),
@Log_Usuario_Mod CHAR(7),
@Log_Fecha_Mod CHAR(8),
@Log_Hora_Mod CHAR(6),
@Cod_Usuario CHAR(7),
@Cod_Usuario_Creacion CHAR(7),
@Hora AS CHAR(6),
@Fecha AS CHAR(8),
@Retorno int,
@Cod_Molinete varchar(3),
@Num_Vuelo varchar(10),
@Num_Asiento varchar(10),
@Nom_Pasajero varchar(50),
@Dsc_Trama_Bcbp varchar(500),
@Tip_Ingreso char(1),
@Dsc_Ticket_Estado VARCHAR(20),
@Cod_Equipo_Mod VARCHAR(20),
@Dsc_Boarding_Estado VARCHAR(20),
@Num_SecHist int,
@Num_Secuencial_Bcbp BIGINT,
@Nuevo_Secuencial_Bcbp BIGINT,
@Inicio CHAR(14),
@Flg_Cambio char(1),
@Cod_Tasa_Venta char(10),
@Imp_Tasa_Venta decimal(8,4),
@nInicia_carga tinyint,
@nCancelado tinyint,
@nCnta_Reg INT,
@nProceso INT,
@cFch_Fin_Registro DATETIME,
@cFch_Inicio_Registro DATETIME,
@cCod_Molinete varchar(4),
@cEstado_proc varchar(1),
@Tip_Estado_Actual_CE varchar(1),
@Tip_Estado_CE varchar(1),
@Tip_Anulacion_CE varchar(1),
@Num_Serie_Tuua char(3),
@Num_Secuencia_Tuua char(9),
@Num_Old_Ticket char(16),
@PosDig varchar(20),
@Cod_Numero_Bcbp char(16),
@Cod_campo varchar(5),
@Dsc_campo varchar(80),
@Imp_Tasa_Compra numeric(8,4),
@Tip_Vuelo char(1),
@xTip_Vuelo char(1),
@xCod_Compania char(10),
@Tip_Pasajero char(1),
@Procesar INT,
@Filtro char(4),
@Fch_Uso char(14),
@Cod_Error char(1)

	---- Validacion de Horas de Sincronismo
	DECLARE @cComp_Hora varchar(5)
	DECLARE @nBuscar INT
    DECLARE @Hora_Sin varchar(5)
	DECLARE @mysl nvarchar(2500)
	DECLARE @Param nvarchar(2500)
	DECLARE @Link varchar(8)
	DECLARE @BD varchar(20)
	DECLARE @Sel nvarchar(2000)
	DECLARE @Check INT
	DECLARE @retcode   int
	DECLARE @linkedsvr sysname
	
	SET @Cod_Error = '0'
	SET @Link = SUBSTRING(@DBName,1,CHARINDEX('.',@DBName)-1)
	SET @BD   = SUBSTRING(@DBName,CHARINDEX('.',@DBName)+1,LEN(RTRIM(@DBName)))
	SET @linkedsvr = SUBSTRING(@Link,2,5)

	BEGIN TRY
		EXEC @retcode = sp_testlinkedserver @server = @linkedsvr
	END TRY
	BEGIN CATCH
		SET @retcode = sign(@@error);
	END CATCH

	IF @retcode = 0
	BEGIN
		SET @nBuscar = 0
		--- Seleccionar Datos del Molinete
		SET @mysl = N'SELECT @pCod_Molinete=Cod_Molinete FROM ' + @Link + '.'+@BD +'.dbo.TUA_Molinete WHERE Dsc_IP = '''+@cDsc_IP+''' AND Tip_Estado=''A'''
		EXEC sp_ExecuteSQL @mysl, N'@pCod_Molinete char(4) OUTPUT',@pCod_Molinete = @cCod_Molinete OUTPUT

		--- Verificar Modo de Sincronismo si es Automatico = 1 o Configuracion de Horario = 2
		SET @Procesar = 0
		IF ( @Modo_SIN = 2 OR @Modo_SIN = 1)
		BEGIN
			IF ( @Modo_SIN = 2 )
			BEGIN
				----- Valida Horas para Modalidad por Horas
				DECLARE HSin_Cursor CURSOR FOR
				SELECT Dsc_campo
				FROM TUA_ListaDeCampos
				WHERE RTRIM(nom_campo) = 'LC'
					AND RTRIM(Cod_Relativo) = '1'

				OPEN HSin_Cursor
				FETCH NEXT FROM HSin_Cursor INTO @cComp_Hora

				WHILE @@FETCH_STATUS = 0
				BEGIN
				   IF  CONVERT(VARCHAR(3), GETDATE(), 114)+'00' = RTRIM(@cComp_Hora)			
				   BEGIN
						SET @mysl  = ' SELECT @pHora_Sin = MAX(CONVERT(VARCHAR(3), Fch_Inicio_Registro, 114)+''00'') 
							FROM ' + @Link +'.'+ @BD + '.dbo.TUA_sincronizacion WITH (NOLOCK) 
							WHERE CONVERT(VARCHAR(10),Fch_Inicio_Registro, 103) = CONVERT(VARCHAR(10), GETDATE(), 103)
							AND RTRIM(Cod_Molinete) = '''+@cCod_Molinete+'''
							AND RTRIM(Tip_Sincronizacion) = ''LC''	'
						EXEC sp_executesql @mysl, N'@pHora_Sin varchar(5) OUTPUT', @pHora_Sin = @Hora_Sin OUTPUT

						IF  @cComp_Hora = @Hora_Sin
						BEGIN
							--- Ya se proceso la Hora Programada
							SET @nBuscar = 1
							BREAK
						END
						ELSE
						BEGIN
							--- No se ha procesado la Hora Programada
							SET @nBuscar = 2
							BREAK
						END
					END
					FETCH NEXT FROM HSin_Cursor INTO @cComp_Hora
				END
				CLOSE HSin_Cursor
				DEALLOCATE HSin_Cursor
				----- Fin Validacion Modalidad por Horas

				IF ( @nBuscar = 0 )
				BEGIN
					SET @Dsc_Information='No hay Hora Configurada en estos momentos para procesar.'
					RETURN
				END
				ELSE
				BEGIN
					IF ( @nBuscar = 1 )
					BEGIN
						SET @Dsc_Information='Hora Configurada ya se proceso.'
						RETURN
					END
					ELSE
					BEGIN
						SET @Procesar = 1
					END
				END
			END
			ELSE
			BEGIN
				SET @Procesar = 1
			END
		END
		ELSE
			SET @Dsc_Message='MODO DE Sincronizacion INCORRECTA.'
	END
	ELSE
	BEGIN
		--No hay Conexion
		SET @Dsc_Message='Error en Conexion - Linked Server para la IP ['+@IP_SERVIDOR+']'+ ' No esta Activo para procesar.'
		RETURN
	END
	---- Fin de Validacion

	IF @Procesar = 1
	BEGIN
		BEGIN TRY
		--***TICKET***
		DECLARE @Cod_Ticket char(16)
		DECLARE @nTotal_Reg INT
		DECLARE @nTotal_RegErr INT
		DECLARE @nContar_RestoUsos INT
		DECLARE @NO_Ticket INT
		DECLARE @NO_BP INT
		DECLARE @tmp_Num_Secuencial INT
		SET @nTotal_Reg = 0
		SET @nCancelado = 0
		SET @nCnta_Reg = 0
		SET @nTotal_RegErr = 0
		SET @NO_Ticket = 0
		SET @NO_BP = 0
		--- Leer Ticket's de Servidor Local
		SET @Cod_Ticket=(SELECT MIN(Cod_Numero_Ticket) FROM dbo.TUA_Ticket WITH (NOLOCK) WHERE Tip_Estado_Actual='U' and Flg_Sincroniza='0')

		IF @Cod_Ticket IS NOT NULL
		BEGIN
			--- Insertar Registro en la Tabla Procesos 'TUA_Sincronizacion' - Sincronizando Ticket's
			SET @cFch_Inicio_Registro = GETDATE()

			SET @mysl = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro, Num_RegErr ) ' +
				 N' VALUES ( ''TICKET'', ''TI'','''+@cCod_Molinete+''', ''P'', @pFch_Inicio_Registro, ''LC'', 0, 0 ) '

			EXECUTE sp_executesql @mysl, N'@pFch_Inicio_Registro DATETIME', @pFch_Inicio_Registro = @cFch_Inicio_Registro

			SET @mysl = N'SELECT @pProceso=MAX(Cod_Sincronizacion) FROM ' + @Link + '.'+@BD +'.dbo.TUA_Sincronizacion WITH (NOLOCK) WHERE Cod_Molinete = '''+@cCod_Molinete+''' '
			EXEC sp_ExecuteSQL @mysl, N'@pProceso INT OUTPUT', @pProceso = @nProceso OUTPUT
			SET @NO_Ticket = 1
		END
		ELSE
			SET @Dsc_Information=@Dsc_Information+'Sincronizacion de Ticket no se realiza, no hay informacion .['+@cDsc_IP+']#'
		
		WHILE @Cod_Ticket IS NOT NULL
		BEGIN
				SET @Hora = Substring([dbo].NTPFunction(),10,15)
				SET @Fecha = Substring([dbo].NTPFunction(),1,8)
				SET @Sel = ''
				SET @mysl = ''
				SET @Param = ''

				--- Leer Ticket de Servidor Local
				SELECT @Cod_Numero_Ticket=Cod_Numero_Ticket, @Tip_Estado_Actual=Tip_Estado_Actual,
					@Fch_Vencimiento_ticket=Fch_Vencimiento, @Num_Rehabilitaciones=Num_Rehabilitaciones, @Num_Extensiones=Num_Extensiones,
					@Log_Usuario_Mod=Log_Usuario_Mod, @Log_Fecha_Mod=Log_Fecha_Mod, @Log_Hora_Mod=Log_Hora_Mod, @Fch_Uso = Fch_Uso
				FROM dbo.TUA_Ticket WHERE Cod_Numero_Ticket=@Cod_Ticket

				SET @Check = 1
				SET @mysl = N'IF EXISTS (SELECT Cod_Numero_Ticket FROM ' + @Link + '.'+@BD +'.dbo.TUA_Ticket WITH (NOLOCK) WHERE Cod_Numero_Ticket = '''+@Cod_Ticket+''' ) BEGIN SET @Check1 = 0 END'
				EXEC sp_ExecuteSQL @mysl, N'@Check1 BIT OUTPUT', @Check1 = @Check OUTPUT

				--- Validar si Existe en el Servidor Central
				IF @Check = 0
				BEGIN
					SET @Sel = ''
					SET @mysl = ''
					SET @Param = ''

					--- Capturar Datos del Ticket Servidor Central
					SET @mysl = N'SELECT @pTip_Estado_Actual_CE=Tip_Estado_Actual, @pTip_Anulacion_CE=Tip_Anulacion, @pCod_Tipo_Ticket=Cod_Tipo_Ticket FROM ' + @Link + '.'+@BD +'.dbo.TUA_Ticket WITH (NOLOCK) WHERE Cod_Numero_Ticket = '''+@Cod_Ticket+''''
					EXEC sp_ExecuteSQL @mysl, N'@pTip_Estado_Actual_CE char(1) OUTPUT, @pTip_Anulacion_CE char(1) OUTPUT, @pCod_Tipo_Ticket char(3) OUTPUT', @pTip_Estado_Actual_CE = @Tip_Estado_Actual_CE OUTPUT, @pTip_Anulacion_CE = @Tip_Anulacion_CE OUTPUT, @pCod_Tipo_Ticket = @Cod_Tipo_Ticket OUTPUT

					--- Validar Estado Ticket
					IF @Tip_Estado_Actual_CE = 'R' OR @Tip_Estado_Actual_CE = 'P' OR @Tip_Estado_Actual_CE = 'E'
					BEGIN
						SET @tmp_Num_Secuencial = 0
						SET @mysl = N'UPDATE ' + @Link + '.'+@BD +'.dbo.TUA_Ticket SET Tip_Estado_Actual = ''U'', Flg_Sincroniza=''1''
									WHERE Cod_Numero_Ticket = '''+@Cod_Ticket+''''
						EXEC sp_ExecuteSQL @mysl

						SET @mysl = N'UPDATE ' + @Link + '.'+@BD +'.dbo.TUA_Ticket SET Log_Usuario_Mod = '''+@Log_Usuario_Mod+''','+
									' Log_Fecha_Mod = '''+@Log_Fecha_Mod+''', Log_Hora_Mod = '''+@Log_Hora_mod+''', Fch_Uso = '''+@Fch_Uso+''' WHERE Cod_Numero_Ticket = '''+@Cod_Ticket+''''

						EXECUTE sp_executesql @mysl

						SET @mysl = N'SELECT @ptmp_Num_Secuencial = MAX(Num_Secuencial) FROM ' + @Link + '.'+@BD +'.dbo.TUA_TicketEStHist WHERE Cod_Numero_Ticket = '''+@Cod_Ticket+''''
						EXEC sp_ExecuteSQL @mysl, N'@ptmp_Num_Secuencial INT OUTPUT', @ptmp_Num_Secuencial = @tmp_Num_Secuencial OUTPUT
						SET @tmp_Num_Secuencial = @tmp_Num_Secuencial + 1

						SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_TicketEstHist ( Cod_Numero_Ticket, Num_Secuencial, '+
									' Tip_Estado, Dsc_Ticket_Estado, Cod_Equipo_Mod, Dsc_Num_Vuelo, Log_Usuario_Mod, '+
									' Log_Fecha_Mod, Log_Hora_Mod, Causal_Rehabilitacion, Dsc_Motivo, Nro_Operacion, '+
									' Cod_Tasa_Cambio, Imp_Tasa_Cambio, Cod_Tasa_Venta, Imp_Tasa_Venta ) '+
									'SELECT Cod_Numero_Ticket, (@ptmp_Num_Secuencial), Tip_Estado, Dsc_Ticket_Estado, Cod_Equipo_Mod, '+
									' Dsc_Num_Vuelo, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, Causal_Rehabilitacion, '+
									' Dsc_Motivo, Nro_Operacion, Cod_Tasa_Cambio, Imp_Tasa_Cambio, Cod_Tasa_Venta, Imp_Tasa_Venta '+
									'FROM dbo.TUA_TicketEstHist WITH (NOLOCK) WHERE Num_Secuencial = 1 AND Cod_Numero_Ticket = '''+@Cod_Ticket+''''

						EXEC sp_executesql @mysl, N'@ptmp_Num_Secuencial INT ', @ptmp_Num_Secuencial = @tmp_Num_Secuencial
					END

					--- Definicion de Parametros para el insert de Error de Ticket
					SET @Param = N'@pCod_Numero_Ticket char(16),
								@pCod_Molinete char(10),
								@pCod_Tipo_Ticket char(3),
								@pDsc_campo varchar(80),
								@pCod_campo varchar(5),
								@pLog_Usuario_Mod char(7),
								@pLog_Fecha_Mod char(8),
								@pLog_Hora_Mod char(6) '

					IF @Tip_Estado_Actual_CE = 'U'
					BEGIN
						--- Extraer Descripcion Error de la Tabla ListadeCampos
						SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorTicket'' AND cod_campo = ''8'''
						EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT
						
						SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_TicketErr (Cod_Numero_Ticket,Cod_Molinete,Cod_Tipo_Ticket,Dsc_Error,Tip_Ingreso, '+
									'Tip_Error,Log_Usuario_Mod,Log_Fecha_Mod,Log_Hora_Mod) '+
									'VALUES (@pCod_Numero_Ticket,@pCod_Molinete,@pCod_Tipo_Ticket,@pDsc_campo,''A'',@pCod_campo, '+
									'@pLog_Usuario_Mod,@pLog_Fecha_Mod,@pLog_Hora_Mod ) '

						EXEC sp_executesql @mysl, @Param,
										@pCod_Numero_Ticket = @Cod_Numero_Ticket,
										@pCod_Molinete = @cCod_Molinete,
										@pCod_Tipo_Ticket = @Cod_Tipo_Ticket,
										@pDsc_campo = @Dsc_campo,
										@pCod_campo = @Cod_campo,
										@pLog_Usuario_Mod = @Log_Usuario_Mod,
										@pLog_Fecha_Mod = @Log_Fecha_Mod,
										@pLog_Hora_Mod = @Log_Hora_Mod

						SET @nTotal_RegErr = @nTotal_RegErr + 1
					END

					IF @Tip_Estado_Actual_CE = 'X' AND @Tip_Anulacion_CE = '1'
					BEGIN
						--- Extraer Descripcion Error de la Tabla ListadeCampos
						SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorTicket'' AND cod_campo = ''9'''
						EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT

						SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_TicketErr (Cod_Numero_Ticket,Cod_Molinete,Cod_Tipo_Ticket,Dsc_Error,Tip_Ingreso, '+
									'Tip_Error,Log_Usuario_Mod,Log_Fecha_Mod,Log_Hora_Mod) '+
									'VALUES (@pCod_Numero_Ticket,@pCod_Molinete,@pCod_Tipo_Ticket,@pDsc_campo,''A'',@pCod_campo, '+
									'@pLog_Usuario_Mod,@pLog_Fecha_Mod,@pLog_Hora_Mod ) '

						EXEC sp_executesql @mysl, @Param, 
										@pCod_Numero_Ticket = @Cod_Numero_Ticket,
										@pCod_Molinete = @cCod_Molinete,
										@pCod_Tipo_Ticket = @Cod_Tipo_Ticket,
										@pDsc_campo = @Dsc_campo,
										@pCod_campo = @Cod_campo,
										@pLog_Usuario_Mod = @Log_Usuario_Mod,
										@pLog_Fecha_Mod = @Log_Fecha_Mod,
										@pLog_Hora_Mod = @Log_Hora_Mod

						SET @nTotal_RegErr = @nTotal_RegErr + 1
					END

					IF @Tip_Estado_Actual_CE = 'X' AND @Tip_Anulacion_CE = '3'
					BEGIN
						--- Extraer Descripcion Error de la Tabla ListadeCampos
						SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorTicket'' AND cod_campo = ''10'''
						EXEC sp_executesql @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT

						SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_TicketErr (Cod_Numero_Ticket,Cod_Molinete,Cod_Tipo_Ticket,Dsc_Error,Tip_Ingreso, '+
									'Tip_Error,Log_Usuario_Mod,Log_Fecha_Mod,Log_Hora_Mod) '+
									'VALUES (@pCod_Numero_Ticket,@pCod_Molinete,@pCod_Tipo_Ticket,@pDsc_campo,''A'',@pCod_campo, '+
									'@pLog_Usuario_Mod,@pLog_Fecha_Mod,@pLog_Hora_Mod ) '

						EXEC sp_executesql @mysl, @Param,
										@pCod_Numero_Ticket = @Cod_Numero_Ticket,
										@pCod_Molinete = @cCod_Molinete,
										@pCod_Tipo_Ticket = @Cod_Tipo_Ticket,
										@pDsc_campo = @Dsc_campo,
										@pCod_campo = @Cod_campo,
										@pLog_Usuario_Mod = @Log_Usuario_Mod,
										@pLog_Fecha_Mod = @Log_Fecha_Mod,
										@pLog_Hora_Mod = @Log_Hora_Mod

						SET @nTotal_RegErr = @nTotal_RegErr + 1
					END

					--- Leer los Usos del Ticket del Servidor Local (TUA_TicketEstHist) e insertarlos en TicketErr
					IF @Tip_Estado_Actual_CE = 'U' OR @Tip_Estado_Actual_CE = 'X'
						BEGIN
							SET @nContar_RestoUsos = 0
							SET @nInicia_carga = 2
							SET @Param = N'@pCod_Molinete char(10),
										@pDsc_campo char(80),
										@pCod_campo char(5),
										@pCod_Ticket char(16),
										@pInicia_carga INT '

							SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_TicketErr (Cod_Numero_Ticket,Cod_Molinete,Cod_Tipo_Ticket,Dsc_Error,Tip_Ingreso, '+
										'Tip_Error,Log_Usuario_Mod,Log_Fecha_Mod,Log_Hora_Mod) '+
										'SELECT TCK.Cod_Numero_Ticket, (@pCod_Molinete), TCK.Cod_Tipo_Ticket, @pDsc_campo, ''A'', @pCod_campo, TCKH.Log_Usuario_Mod, '+
										' TCKH.Log_Fecha_Mod, TCKH.Log_Hora_Mod '+
										'FROM dbo.TUA_Ticket TCK INNER JOIN dbo.TUA_TicketEstHist TCKH '+
										' ON ( TCK.Cod_Numero_Ticket = TCKH.Cod_Numero_Ticket ) '+
										'WHERE TCK.Cod_Numero_Ticket = @pCod_Ticket '+
										' AND TCKH.Num_Secuencial >= @pInicia_carga '

							EXEC sp_executesql @mysl, @Param,
											@pCod_Molinete = @cCod_Molinete,
											@pDsc_campo = @Dsc_campo,
											@pCod_campo = @Cod_campo,
											@pCod_Ticket = @Cod_Ticket,
											@pInicia_carga = @nInicia_carga

							SELECT @nContar_RestoUsos = COUNT(TCKH.Cod_Numero_Ticket) FROM dbo.TUA_Ticket TCK INNER JOIN dbo.TUA_TicketEstHist TCKH ON ( TCK.Cod_Numero_Ticket = TCKH.Cod_Numero_Ticket )
							WHERE TCK.Cod_Numero_Ticket = @Cod_Ticket AND TCKH.Num_Secuencial >= @nInicia_carga
							
							SET @nTotal_RegErr = @nTotal_RegErr + @nContar_RestoUsos
						END
					ELSE
						BEGIN
							SET @nContar_RestoUsos = 0
							--- Extraer Descripcion Error de la Tabla ListadeCampos
							SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorTicket'' AND cod_campo = ''8'''
							EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT
							SET @nInicia_carga = 1

							SET @Param = N'@pCod_Molinete char(10),
										@pDsc_campo char(80),
										@pCod_campo char(5),
										@pCod_Ticket char(16),
										@pInicia_carga INT '

							SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_TicketErr (Cod_Numero_Ticket,Cod_Molinete,Cod_Tipo_Ticket,Dsc_Error,Tip_Ingreso, '+
										'Tip_Error,Log_Usuario_Mod,Log_Fecha_Mod,Log_Hora_Mod) '+
										'SELECT TCK.Cod_Numero_Ticket, (@pCod_Molinete), TCK.Cod_Tipo_Ticket, @pDsc_campo, ''A'', @pCod_campo, TCKH.Log_Usuario_Mod, '+
										' TCKH.Log_Fecha_Mod, TCKH.Log_Hora_Mod '+
										'FROM dbo.TUA_Ticket TCK INNER JOIN dbo.TUA_TicketEstHist TCKH '+
										' ON ( TCK.Cod_Numero_Ticket = TCKH.Cod_Numero_Ticket ) '+
										'WHERE TCK.Cod_Numero_Ticket = @pCod_Ticket '+
										' AND TCKH.Num_Secuencial > @pInicia_carga '

							EXEC sp_executesql @mysl, @Param,
											@pCod_Molinete = @cCod_Molinete,
											@pDsc_campo = @Dsc_campo,
											@pCod_campo = @Cod_campo,
											@pCod_Ticket = @Cod_Ticket,
											@pInicia_carga = @nInicia_carga

							SELECT @nContar_RestoUsos = COUNT(TCKH.Cod_Numero_Ticket) FROM dbo.TUA_Ticket TCK INNER JOIN dbo.TUA_TicketEstHist TCKH ON ( TCK.Cod_Numero_Ticket = TCKH.Cod_Numero_Ticket )
							WHERE TCK.Cod_Numero_Ticket = @Cod_Ticket AND TCKH.Num_Secuencial > @nInicia_carga
							
							--IF @nContar_RestoUsos > 1
							--	SET @nTotal_RegErr = @nTotal_RegErr + @nContar_RestoUsos
						END
				END
				ELSE
				BEGIN
					--- Extraer Descripcion Error de la Tabla ListadeCampos
					SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorTicket'' AND cod_campo = ''7'''
					EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT

					SET @Param = N'@pCod_Numero_Ticket char(16),
								@pCod_Molinete char(10),
								@pCod_Tipo_Ticket char(3),
								@pDsc_campo varchar(80),
								@pCod_campo varchar(5),
								@pLog_Usuario_Mod char(7),
								@pLog_Fecha_Mod char(8),
								@pLog_Hora_Mod char(6) '

					SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_TicketErr (Cod_Numero_Ticket,Cod_Molinete,Cod_Tipo_Ticket,Dsc_Error,Tip_Ingreso, '+
								'Tip_Error,Log_Usuario_Mod,Log_Fecha_Mod,Log_Hora_Mod) '+
								'VALUES (@pCod_Numero_Ticket,@pCod_Molinete,@pCod_Tipo_Ticket,@pDsc_campo,''A'',@pCod_campo, '+
								'@pLog_Usuario_Mod,@pLog_Fecha_Mod,@pLog_Hora_Mod ) '

					EXEC sp_executesql @mysl, @Param,
									@pCod_Numero_Ticket = @Cod_Numero_Ticket,
									@pCod_Molinete = @cCod_Molinete,
									@pCod_Tipo_Ticket = @Cod_Tipo_Ticket,
									@pDsc_campo = @Dsc_campo,
									@pCod_campo = @Cod_campo,
									@pLog_Usuario_Mod = @Log_Usuario_Mod,
									@pLog_Fecha_Mod = @Log_Fecha_Mod,
									@pLog_Hora_Mod = @Log_Hora_Mod

					SET @nTotal_RegErr = @nTotal_RegErr + 1
				END
				DELETE FROM dbo.TUA_TicketEstHist WHERE Cod_Numero_Ticket=@Cod_Ticket
				UPDATE dbo.TUA_Ticket SET Flg_Sincroniza='1' WHERE Cod_Numero_Ticket=@Cod_Ticket
				SET @Cod_Ticket=(SELECT MIN(Cod_Numero_Ticket) FROM dbo.TUA_Ticket WITH (NOLOCK) WHERE Cod_Numero_Ticket>@Cod_Ticket AND Tip_Estado_Actual='U' and Flg_Sincroniza='0')
				SET @nCnta_Reg = @nCnta_Reg + 1
				SET @nTotal_Reg = @nTotal_Reg + 1

				IF @nCnta_Reg = @nInter_Reg
				BEGIN
					SET @nCnta_Reg = 0
					--- Verificacion del Estado del Proceso si esta cancelado
					SET @mysl = N'SELECT @pEstado_proc = Tip_Estado FROM ' + @Link + '.'+@BD +'.dbo.TUA_Sincronizacion WHERE cod_Sincronizacion = @pProceso '
					EXEC sp_ExecuteSQL @mysl, N'@pEstado_proc char(1) OUTPUT, @pProceso INT ', @pEstado_proc = @cEstado_proc OUTPUT, @pProceso = @nProceso

					IF @cEstado_proc = 'C' 
					BEGIN
						SET @nCancelado = 1
						SET @cFch_Fin_Registro = GETDATE()
						SET @Param = N'@pFch_Fin_Registro DATETIME, '+
								N'@pTotal_Reg INT, '+
								N'@pProceso INT, '+
								N'@pTotal_RegErr INT '

						SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_Sincronizacion SET Fch_Fin_Registro = @pFch_Fin_Registro, Num_Registro = @pTotal_Reg, Num_RegErr = @pTotal_RegErr '+
									' WHERE Cod_Sincronizacion = @pProceso '

						EXEC sp_executesql @mysl, @Param,
										@pFch_Fin_Registro = @cFch_Fin_Registro,
										@pTotal_Reg = @nTotal_Reg,
										@pProceso = @nProceso,
										@pTotal_RegErr = nTotal_RegErr

						SET @Dsc_Information=@Dsc_Information+'Cierra Transaccion Ticket por Cancelacion.['+@cDsc_IP+']#'
						BREAK
					END

				END
		END

		IF @Cod_Ticket IS NULL AND @nCancelado = 0
		BEGIN
			IF @NO_Ticket = 1
			BEGIN
				SET @cFch_Fin_Registro = GETDATE()
				SET @Param = N'@pFch_Fin_Registro DATETIME, '+
						N'@pTotal_Reg INT, '+
						N'@pProceso INT, '+
						N'@pTotal_RegErr INT '

				SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_Sincronizacion SET Tip_Estado = ''T'', Fch_Fin_Registro = @pFch_Fin_Registro, Num_Registro = @pTotal_Reg, Num_RegErr = @pTotal_RegErr '+
							'WHERE Cod_Sincronizacion = @pProceso '

				EXEC sp_executesql @mysl, @Param,
								@pFch_Fin_Registro = @cFch_Fin_Registro,
								@pTotal_Reg = @nTotal_Reg,
								@pProceso = @nProceso,
								@pTotal_RegErr = @nTotal_RegErr

				SET @Dsc_Information=@Dsc_Information+'TUA_Ticket Sincronizada Correctamente.['+@cDsc_IP+']#'
				SET @Dsc_Information=@Dsc_Information+'Cierra Transaccion Ticket .['+@cDsc_IP+']#'

			END
		END

		END TRY
		BEGIN CATCH
			SET @Cod_Ticket = NULL
			SET @nCancelado = 2
			SET @Dsc_Message='TUA_Ticket Error en Transaccion. # Servidor Local : '+@cDsc_IP+' # Nro Error : '+CONVERT(varchar(20), ERROR_NUMBER())+' - Mensaje Error : '+ERROR_MESSAGE()
			IF XACT_STATE() <> 0
			BEGIN
				ROLLBACK --TRANSACTION
			END
		END CATCH


		IF @nCancelado = 0
		BEGIN
			BEGIN TRY

			SET @nCnta_Reg = 0
			SET @nTotal_Reg = 0
			SET @nTotal_RegErr = 0
			--***BCBP***
			DECLARE @Num_Secuencial BIGINT

			DECLARE @c_BCBP CURSOR;
			SET @c_BCBP = CURSOR FOR SELECT Num_Secuencial_Bcbp, Cod_Compania, Num_Vuelo, Fch_Vuelo, 
						Num_Asiento, Nom_Pasajero, Dsc_Trama_Bcbp, Log_Usuario_Mod,
						Log_Fecha_Mod, Log_Hora_Mod, Tip_Ingreso, Tip_Estado, 
						Num_Rehabilitaciones, Cod_Unico_Bcbp, Fch_Vencimiento, Fch_Creacion, Hor_Creacion,
						Cod_Unico_Bcbp_Rel, Flg_Sincroniza, Tip_Pasajero, Tip_Vuelo, Tip_Trasbordo,
						Tip_Anulacion, Cod_Numero_Bcbp, Num_Serie, Flg_Tipo_Bcbp, Num_Secuencial_Bcbp_Rel, Num_Secuencial_Bcbp_Rel_Sec,
						Nro_Boarding, Dsc_Destino, Cod_Eticket, Cod_Moneda, Imp_Precio, Imp_Tasa_Compra,
						Imp_Tasa_Venta, Num_Proceso_Rehab, Flg_Bloqueado, Flg_WSError, Flg_Incluye_Tuua,
						Fch_Rehabilitacion, Num_Airline_Code, Num_Document_Form
				FROM dbo.TUA_BoardingBcbp WITH (NOLOCK)
				WHERE Tip_Estado='U' AND Flg_Sincroniza='0'

			OPEN @c_BCBP

			FETCH NEXT FROM @c_BCBP INTO @Num_Secuencial_Bcbp, @Cod_Compania, @Num_Vuelo, @Fch_Vuelo, @Num_Asiento, @Nom_Pasajero, @Dsc_Trama_Bcbp, @Log_Usuario_Mod,
				@Log_Fecha_Mod, @Log_Hora_Mod, @Tip_Ingreso, @Tip_Estado, @Num_Rehabilitaciones, @Cod_Unico_Bcbp, @Fch_Vencimiento, @Fch_Creacion, @Hor_Creacion,
				@Cod_Unico_Bcbp_Rel, @Flg_Sincroniza, @Tip_Pasajero, @Tip_Vuelo, @Tip_Trasbordo, @Tip_Anulacion, @Cod_Numero_Bcbp, @Num_Serie, @Flg_Tipo_Bcbp,
				@Num_Secuencial_Bcbp_Rel, @Num_Secuencial_Bcbp_Rel_Sec, @Nro_Boarding, @Dsc_Destino, @Cod_Eticket, @Cod_Moneda, @Imp_Precio, @Imp_Tasa_Compra,
				@Imp_Tasa_Venta, @Num_Proceso_Rehab, @Flg_Bloqueado, @Flg_WSError, @Flg_Incluye_Tuua, @Fch_Rehabilitacion, @Num_Airline_Code, @Num_Document_Form

			IF @@FETCH_STATUS = 0
			BEGIN
				SET @cFch_Inicio_Registro = GETDATE()

				SET @mysl = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_Sincronizacion ( Tabla_Sincronizacion, Tipo_Tabla, Cod_Molinete, Tip_Estado, Fch_Inicio_Registro, Tip_Sincronizacion, Num_Registro, Num_RegErr ) ' +
					 ' VALUES ( ''BOARDING'', ''BP'','''+@cCod_Molinete+''', ''P'', @pFch_Inicio_Registro, ''LC'', 0, 0 ) '
				EXEC sp_executesql @mysl, N'@pFch_Inicio_Registro DATETIME', @pFch_Inicio_Registro = @cFch_Inicio_Registro
				
				SET @mysl = ''
				SET @mysl = N'SELECT @pProceso=MAX(Cod_Sincronizacion) FROM ' + @Link + '.'+@BD +'.dbo.TUA_Sincronizacion WITH (NOLOCK) WHERE Cod_Molinete = '''+@cCod_Molinete+''' '
				EXEC sp_ExecuteSQL @mysl, N'@pProceso INT OUTPUT', @pProceso = @nProceso OUTPUT

				SET @NO_BP = 1
				WHILE @@FETCH_STATUS = 0
				BEGIN
						SET @Sel = ''
						SET @mysl = ''
						SET @Param = ''
						--print 'Antes de Select'
						SELECT @Dsc_Boarding_Estado=Dsc_Boarding_Estado,@Cod_Equipo_Mod=Cod_Equipo_Mod FROM dbo.TUA_BoardingBcbpEstHist WITH (NOLOCK) WHERE Cod_Compania=@Cod_Compania AND Num_Vuelo=@Num_Vuelo AND Fch_Vuelo=@Fch_Vuelo AND Num_Asiento=@Num_Asiento AND Nom_Pasajero=@Nom_Pasajero AND Tip_Estado='U'
						SET @Check = 1
						--- Extraer el Tipo de Vuelo y el codigo de compañia del vuelo
					   SET @mysl = N'SELECT @pTip_Vuelo=Tip_Vuelo,@pCod_Compania=Cod_Compania FROM ' + @Link + '.'+@BD +'.dbo.TUA_VueloProgramado WITH (NOLOCK) WHERE Fch_Vuelo = @pFch_Vuelo AND Num_Vuelo = @pNum_Vuelo ; IF (@pTip_Vuelo IS NULL) BEGIN SET @pTip_Vuelo = '''' END '
					   EXEC sp_ExecuteSQL @mysl, N'@pTip_Vuelo varchar(1) OUTPUT, @pFch_Vuelo char(8), @pNum_Vuelo char(10), @pCod_Compania char(10) OUTPUT', @pTip_Vuelo = @xTip_Vuelo OUTPUT, @pCod_Compania=@xCod_Compania OUTPUT, @pFch_Vuelo = @Fch_Vuelo, @pNum_Vuelo = @Num_Vuelo
					   SET @Tip_Vuelo = @xTip_Vuelo
					   SET @Cod_Compania = @xCod_Compania
						
						SET @mysl = N'IF EXISTS (SELECT Num_Secuencial_Bcbp FROM ' + @Link + '.'+@BD +'.dbo.TUA_BoardingBcbp WITH (NOLOCK) WHERE Cod_Compania = '''+@Cod_Compania+''' AND Num_Vuelo = '''+@Num_Vuelo+''' AND Fch_Vuelo = '''+@Fch_Vuelo+''' AND Num_Asiento = '''+@Num_Asiento+''' AND Nom_Pasajero = '''+@Nom_Pasajero+''' ) BEGIN SET @Check1 = 0 END'
						EXEC sp_ExecuteSQL @mysl, N'@Check1 INT OUTPUT', @Check1 = @Check OUTPUT
						--print 'Despues de Select' + @mysl + ' '
						--- // Definir Parametrso para la Tabla Error de Boarding
						SET @Param = N'@pDsc_Trama_Bcbp varchar(200),
									@pCod_campo varchar(5),
									@pLog_Usuario_Mod char(7),
									@pLog_Fecha_Mod char(8),
									@pLog_Hora_Mod char(6),
									@pCod_Equipo_Mod varchar(20),
									@pCod_Compania varchar(10),
									@pNum_Vuelo varchar(10),
									@pFch_Vuelo varchar(8),
									@pNum_Asiento varchar(10),
									@pDsc_campo varchar(80),
									@pNom_Pasajero varchar(50)'

						--- Validar si Existe en el Servidor Central
						IF @Check = 0
						BEGIN
							--- Extraer el Tipo de Vuelo y el codigo de compañia del vuelo
							   SET @mysl = N'SELECT @pTip_Vuelo=Tip_Vuelo,@pCod_Compania=Cod_Compania FROM ' + @Link + '.'+@BD +'.dbo.TUA_VueloProgramado WITH (NOLOCK) WHERE Fch_Vuelo = @pFch_Vuelo AND Num_Vuelo = @pNum_Vuelo ; IF (@pTip_Vuelo IS NULL) BEGIN SET @pTip_Vuelo = '''' END '
							   EXEC sp_ExecuteSQL @mysl, N'@pTip_Vuelo varchar(1) OUTPUT, @pFch_Vuelo char(8), @pNum_Vuelo char(10), @pCod_Compania char(10) OUTPUT', @pTip_Vuelo = @xTip_Vuelo OUTPUT, @pCod_Compania=@xCod_Compania OUTPUT, @pFch_Vuelo = @Fch_Vuelo, @pNum_Vuelo = @Num_Vuelo
							   SET @Tip_Vuelo = @xTip_Vuelo
							   SET @Cod_Compania = @xCod_Compania
							   

								--- Capturar Datos del Boarding Servidor Central
								SET @mysl = N'SELECT @pTip_Estado_CE=Tip_Estado, @pTip_Anulacion_CE=Tip_Anulacion FROM ' + @Link + '.'+@BD +'.dbo.TUA_BoardingBcbp WITH (NOLOCK) WHERE Cod_Compania = '''+@Cod_Compania+''' AND Num_Vuelo = '''+@Num_Vuelo+''' AND Fch_Vuelo = '''+@Fch_Vuelo+''' AND Num_Asiento = '''+@Num_Asiento+''' AND Nom_Pasajero = '''+@Nom_Pasajero+''''
								EXEC sp_ExecuteSQL @mysl, N'@pTip_Estado_CE char(1) OUTPUT, @pTip_Anulacion_CE char(1) OUTPUT', @pTip_Estado_CE = @Tip_Estado_CE OUTPUT, @pTip_Anulacion_CE = @Tip_Anulacion_CE OUTPUT

								--- Validacion de Boarding
								IF @Tip_Estado_CE = 'U'
								BEGIN
									--- Extraer Descripcion Error de la Tabla ListadeCampos
									SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorBCBP'' AND cod_campo = ''10'''
									EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT
									
									SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_BoardingBcbpErr (Dsc_Trama_Bcbp, Tip_Error, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, Tip_Ingreso, '+
												' Cod_Equipo_Mod,Tip_Boarding,Cod_Compania,Num_Vuelo,Fch_Vuelo,Num_Asiento,Nom_Pasajero,Log_Error) '+
												'VALUES (@pDsc_Trama_Bcbp, @pCod_campo, @pLog_Usuario_Mod, @pLog_Fecha_Mod, @pLog_Hora_Mod,''A'', '+
												' @pCod_Equipo_Mod,'''', @pCod_Compania, @pNum_Vuelo, @pFch_Vuelo, @pNum_Asiento, @pNom_Pasajero, @pDsc_campo ) '

									--print @Num_Secuencial_Bcbp
									EXEC sp_executeSQL @mysl, @Param, @pDsc_Trama_Bcbp = @Dsc_Trama_Bcbp,
													@pCod_campo = @Cod_campo,														
													@pLog_Usuario_Mod = @Log_Usuario_Mod,
													@pLog_Fecha_Mod = @Log_Fecha_Mod,
													@pLog_Hora_Mod = @Log_Hora_Mod,
													@pCod_Equipo_Mod = @Cod_Equipo_Mod,
													@pCod_Compania = @Cod_Compania,
													@pNum_Vuelo = @Num_Vuelo,
													@pFch_Vuelo = @Fch_Vuelo,
													@pNum_Asiento = @Num_Asiento,
													@pNom_Pasajero = @Nom_Pasajero,
													@pDsc_campo = @Dsc_campo

									--ACTUALIZANDO EL FLAG DE BD LOCAL
									UPDATE dbo.TUA_BoardingBcbp SET Flg_Sincroniza='1' WHERE Cod_Compania=@Cod_Compania AND Num_Vuelo=@Num_Vuelo AND Fch_Vuelo=@Fch_Vuelo AND Num_Asiento=@Num_Asiento AND Nom_Pasajero=@Nom_Pasajero AND Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp
									SET @nTotal_RegErr = @nTotal_RegErr + 1
								END

								IF @Tip_Estado_CE = 'R'
								BEGIN
									--- Extraer el Tipo de Vuelo
									SET @mysl = N'SELECT @pTip_Vuelo=Tip_Vuelo,@pCod_Compania=Cod_Compania FROM ' + @Link + '.'+@BD +'.dbo.TUA_VueloProgramado WITH (NOLOCK) WHERE Fch_Vuelo = @pFch_Vuelo AND Num_Vuelo = @pNum_Vuelo ; IF (@pTip_Vuelo IS NULL) BEGIN SET @pTip_Vuelo = '''' END '
									EXEC sp_ExecuteSQL @mysl, N'@pTip_Vuelo varchar(1) OUTPUT, @pFch_Vuelo char(8), @pNum_Vuelo char(10), @pCod_Compania char(10) OUTPUT', @pTip_Vuelo = @xTip_Vuelo OUTPUT, @pCod_Compania=@xCod_Compania OUTPUT, @pFch_Vuelo = @Fch_Vuelo, @pNum_Vuelo = @Num_Vuelo
									SET @Tip_Vuelo = @xTip_Vuelo
									SET @Cod_Compania = @xCod_Compania

									--- Validacion de Vuelo Programado no Existe se Envia Tabla de Error TUA_BoardingBcbpErr
									IF @xTip_Vuelo IS NULL OR RTRIM(@xTip_Vuelo) = ''
									BEGIN

										SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorBCBP'' AND cod_campo = ''13'''
										EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT

										SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_BoardingBcbpErr (Dsc_Trama_Bcbp, Tip_Error, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, Tip_Ingreso, '+
													' Cod_Equipo_Mod,Tip_Boarding,Cod_Compania,Num_Vuelo,Fch_Vuelo,Num_Asiento,Nom_Pasajero,Log_Error) '+
													'VALUES (@pDsc_Trama_Bcbp, @pCod_campo, @pLog_Usuario_Mod, @pLog_Fecha_Mod, @pLog_Hora_Mod,''A'', '+
													' @pCod_Equipo_Mod,'''', @pCod_Compania, @pNum_Vuelo, @pFch_Vuelo, @pNum_Asiento, @pNom_Pasajero, @pDsc_campo ) '

										EXEC sp_executeSQL @mysl, @Param,
														@pDsc_Trama_Bcbp = @Dsc_Trama_Bcbp,
														@pCod_campo = @Cod_campo,														
														@pLog_Usuario_Mod = @Log_Usuario_Mod,
														@pLog_Fecha_Mod = @Log_Fecha_Mod,
														@pLog_Hora_Mod = @Log_Hora_Mod,
														@pCod_Equipo_Mod = @Cod_Equipo_Mod,
														@pCod_Compania = @Cod_Compania,
														@pNum_Vuelo = @Num_Vuelo,
														@pFch_Vuelo = @Fch_Vuelo,
														@pNum_Asiento = @Num_Asiento,
														@pNom_Pasajero = @Nom_Pasajero,
														@pDsc_campo = @Dsc_campo

										SET @nTotal_RegErr = @nTotal_RegErr + 1
									END
									ELSE
									BEGIN

                                        -- Insertar BP Rehabilitado si existe vuelo programado
										
										SET @Param = N' @pCod_Compania char(10), @pNum_Vuelo varchar(10), @pFch_Vuelo char(8), @pNum_Asiento varchar(10), @pNom_Pasajero varchar(50), @pDsc_Trama_Bcbp varchar(500),
														@pLog_Usuario_Mod char(7), @pLog_Fecha_Mod char(8), @pLog_Hora_Mod char(6), @pTip_Ingreso char(1), @pTip_Estado char(1), @pCod_Equipo_Mod varchar(20),
														@pDsc_Boarding_Estado varchar(20), @pNum_Rehabilitaciones INT, @pCod_Unico_Bcbp VARCHAR(20), @pFch_Vencimiento datetime, @pFch_Creacion char(8), @pHor_Creacion char(6),
														@pCod_Unico_Bcbp_Rel VARCHAR(20), @pFlg_Sincroniza char(1), @pTip_Pasajero char(1), @pTip_Vuelo char(1), @pTip_Trasbordo char(1), @pFlg_Tipo_Bcbp char(1),
														@pNum_Secuencial_Bcbp_Rel bigint, @pNum_Secuencial_Bcbp_Rel_Sec bigint, @pNro_Boarding char(5), @pDsc_Destino char(3), @pCod_Eticket varchar(20), 
														@pNum_Proceso_Rehab VARCHAR(20), @pFlg_Bloqueado char(1), @pFlg_WSError char(1), @pFlg_Incluye_Tuua char(1), @pDsc_Observacion varchar(255), @pFch_Rehabilitacion char(14),
														@pNum_Airline_Code CHAR(3), @pNum_Document_Form CHAR(10), @pCod_Error char(1) OUTPUT '

										SET @mysl = N' '+@Link + '.'+@BD +'.dbo.usp_scz_pcs_boardingbcbp_ins @pCod_Compania, @pNum_Vuelo, @pFch_Vuelo, @pNum_Asiento, @pNom_Pasajero, @pDsc_Trama_Bcbp,
														@pLog_Usuario_Mod, @pLog_Fecha_Mod, @pLog_Hora_Mod, @pTip_Ingreso, @pTip_Estado, @pCod_Equipo_Mod,
														@pDsc_Boarding_Estado, @pNum_Rehabilitaciones, @pCod_Unico_Bcbp, @pFch_Vencimiento, @pFch_Creacion, @pHor_Creacion,
														@pCod_Unico_Bcbp_Rel, @pFlg_Sincroniza, @pTip_Pasajero, @pTip_Vuelo, @pTip_Trasbordo, @pFlg_Tipo_Bcbp,
														@pNum_Secuencial_Bcbp_Rel, @pNum_Secuencial_Bcbp_Rel_Sec, @pNro_Boarding, @pDsc_Destino, @pCod_Eticket, 
														@pNum_Proceso_Rehab, @pFlg_Bloqueado, @pFlg_WSError, @pFlg_Incluye_Tuua, @pDsc_Observacion, @pFch_Rehabilitacion,
														@pNum_Airline_Code, @pNum_Document_Form, @pCod_Error '

										EXEC sp_ExecuteSQL @mysl, @Param, @pCod_Compania = @Cod_Compania,
														@pNum_Vuelo = @Num_Vuelo, 
														@pFch_Vuelo = @Fch_Vuelo, 
														@pNum_Asiento = @Num_Asiento, 
														@pNom_Pasajero = @Nom_Pasajero, 
														@pDsc_Trama_Bcbp = @Dsc_Trama_Bcbp,
														@pLog_Usuario_Mod = @Log_Usuario_Mod, 
														@pLog_Fecha_Mod = @Log_Fecha_Mod, 
														@pLog_Hora_Mod = @Log_Hora_Mod, 
														@pTip_Ingreso = @Tip_Ingreso, 
														@pTip_Estado = @Tip_Estado, 
														@pCod_Equipo_Mod = @Cod_Equipo_Mod,
														@pDsc_Boarding_Estado = @Dsc_Boarding_Estado, 
														@pNum_Rehabilitaciones = @Num_Rehabilitaciones, 
														@pCod_Unico_Bcbp = @Cod_Unico_Bcbp, 
														@pFch_Vencimiento = @Fch_Vencimiento, 
														@pFch_Creacion = @Fch_Creacion, 
														@pHor_Creacion = @Hor_Creacion,
														@pCod_Unico_Bcbp_Rel = @Cod_Unico_Bcbp_Rel, 
														@pFlg_Sincroniza = '1', 
														@pTip_Pasajero = @Tip_Pasajero, 
														@pTip_Vuelo = @Tip_Vuelo, 
														@pTip_Trasbordo = @Tip_Trasbordo, 
														@pFlg_Tipo_Bcbp = @Flg_Tipo_Bcbp,
														@pNum_Secuencial_Bcbp_Rel = @Num_Secuencial_Bcbp_Rel, 
														@pNum_Secuencial_Bcbp_Rel_Sec = @Num_Secuencial_Bcbp_Rel_Sec, 
														@pNro_Boarding = @Nro_Boarding, 
														@pDsc_Destino = @Dsc_Destino, 
														@pCod_Eticket = @Cod_Eticket, 
														@pNum_Proceso_Rehab = @Num_Proceso_Rehab, 
														@pFlg_Bloqueado = @Flg_Bloqueado, 
														@pFlg_WSError = @Flg_WSError, 
														@pFlg_Incluye_Tuua = @Flg_Incluye_Tuua, 
														@pDsc_Observacion = NULL, 
														@pFch_Rehabilitacion = @Fch_Rehabilitacion,
														@pNum_Airline_Code = @Num_Airline_Code, 
														@pNum_Document_Form = @Num_Document_Form,
														@pCod_Error = @Cod_Error OUTPUT

										IF @Cod_Error = '0'
										BEGIN
											--ACTUALIZANDO EL FLAG DE BD LOCAL
											UPDATE dbo.TUA_BoardingBcbp SET Flg_Sincroniza='1' WHERE Cod_Compania=@Cod_Compania AND Num_Vuelo=@Num_Vuelo AND Fch_Vuelo=@Fch_Vuelo AND Num_Asiento=@Num_Asiento AND Nom_Pasajero=@Nom_Pasajero AND Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp
										END
									END
								END
								
								--- Anulacion
								IF @Tip_Estado_CE = 'X' AND @Tip_Anulacion_CE = '1'
								BEGIN
									--- Extraer Descripcion Error de la Tabla ListadeCampos
									SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorBCBP'' AND cod_campo = ''11'''
									EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT

									SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_BoardingBcbpErr (Dsc_Trama_Bcbp, Tip_Error, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, Tip_Ingreso, '+
												' Cod_Equipo_Mod,Tip_Boarding,Cod_Compania,Num_Vuelo,Fch_Vuelo,Num_Asiento,Nom_Pasajero,Log_Error) '+
												'VALUES (@pDsc_Trama_Bcbp, @pCod_campo, @pLog_Usuario_Mod, @pLog_Fecha_Mod, @pLog_Hora_Mod,''A'', '+
												' @pCod_Equipo_Mod,'''', @pCod_Compania, @pNum_Vuelo, @pFch_Vuelo, @pNum_Asiento, @pNom_Pasajero, @pDsc_campo ) '

									EXEC sp_executeSQL @mysl, @Param,
													@pDsc_Trama_Bcbp = @Dsc_Trama_Bcbp,
													@pCod_campo = @Cod_campo,
													@pDsc_campo = @Dsc_campo,
													@pLog_Usuario_Mod = @Log_Usuario_Mod,
													@pLog_Fecha_Mod = @Log_Fecha_Mod,
													@pLog_Hora_Mod = @Log_Hora_Mod,
													@pCod_Equipo_Mod = @Cod_Equipo_Mod,
													@pCod_Compania = @Cod_Compania,
													@pNum_Vuelo = @Num_Vuelo,
													@pFch_Vuelo = @Fch_Vuelo,
													@pNum_Asiento = @Num_Asiento,
													@pNom_Pasajero = @Nom_Pasajero

									--ACTUALIZANDO EL FLAG DE BD LOCAL
									UPDATE dbo.TUA_BoardingBcbp SET Flg_Sincroniza='1' WHERE Cod_Compania=@Cod_Compania AND Num_Vuelo=@Num_Vuelo AND Fch_Vuelo=@Fch_Vuelo AND Num_Asiento=@Num_Asiento AND Nom_Pasajero=@Nom_Pasajero AND Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp
									SET @nTotal_RegErr = @nTotal_RegErr + 1
								END
					
								--- Vencimiento
								IF @Tip_Estado_CE = 'X' AND @Tip_Anulacion_CE = '3'
								BEGIN
									--- Extraer Descripcion Error de la Tabla ListadeCampos
									SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorBCBP'' AND cod_campo = ''12'''
									EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT

									SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_BoardingBcbpErr (Dsc_Trama_Bcbp, Tip_Error, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, Tip_Ingreso, '+
												' Cod_Equipo_Mod,Tip_Boarding,Cod_Compania,Num_Vuelo,Fch_Vuelo,Num_Asiento,Nom_Pasajero,Log_Error) '+
												'VALUES (@pDsc_Trama_Bcbp, @pCod_campo, @pLog_Usuario_Mod, @pLog_Fecha_Mod, @pLog_Hora_Mod,''A'', '+
												' @pCod_Equipo_Mod,'''', @pCod_Compania, @pNum_Vuelo, @pFch_Vuelo, @pNum_Asiento, @pNom_Pasajero,@pDsc_campo ) '

									EXEC sp_executeSQL @mysl, @Param,
													@pDsc_Trama_Bcbp = @Dsc_Trama_Bcbp,
													@pCod_campo = @Cod_campo,
													@pDsc_campo = @Dsc_campo,
													@pLog_Usuario_Mod = @Log_Usuario_Mod,
													@pLog_Fecha_Mod = @Log_Fecha_Mod,
													@pLog_Hora_Mod = @Log_Hora_Mod,
													@pCod_Equipo_Mod = @Cod_Equipo_Mod,
													@pCod_Compania = @Cod_Compania,
													@pNum_Vuelo = @Num_Vuelo,
													@pFch_Vuelo = @Fch_Vuelo,
													@pNum_Asiento = @Num_Asiento,
													@pNom_Pasajero = @Nom_Pasajero

									--ACTUALIZANDO EL FLAG DE BD LOCAL
									UPDATE dbo.TUA_BoardingBcbp SET Flg_Sincroniza='1' WHERE Cod_Compania=@Cod_Compania AND Num_Vuelo=@Num_Vuelo AND Fch_Vuelo=@Fch_Vuelo AND Num_Asiento=@Num_Asiento AND Nom_Pasajero=@Nom_Pasajero AND Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp
									SET @nTotal_RegErr = @nTotal_RegErr + 1
								END
							--END		
						END
						ELSE
						BEGIN
                            --- insertar BP  que no existen en la central 
							--- Extraer el Tipo de Vuelo
							--print 'Antes de Consulta vuelo'
							SET @mysl = N'SELECT @pTip_Vuelo = Tip_Vuelo,@pCod_Compania=Cod_Compania FROM ' + @Link + '.'+@BD +'.dbo.TUA_VueloProgramado WITH (NOLOCK) WHERE Fch_Vuelo = @pFch_Vuelo AND Num_Vuelo = @pNum_Vuelo '
							EXEC sp_ExecuteSQL @mysl, N'@pTip_Vuelo varchar(4) OUTPUT, @pFch_Vuelo char(8), @pNum_Vuelo char(10), @pCod_Compania char(10) OUTPUT', @pTip_Vuelo = @xTip_Vuelo OUTPUT, @pCod_Compania=@xCod_Compania OUTPUT, @pFch_Vuelo = @Fch_Vuelo, @pNum_Vuelo = @Num_Vuelo
							SET @Tip_Vuelo = @xTip_Vuelo
							SET @Cod_Compania = @xCod_Compania								
							
							--print 'Despues de Consulta vuelo'
							
							IF @xTip_Vuelo IS NULL OR RTRIM(@xTip_Vuelo) = ''
							BEGIN

								SET @mysl = N'SELECT @pCod_campo=cod_campo, @pDsc_campo=dsc_campo FROM ' + @Link + '.'+@BD +'.dbo.TUA_ListaDeCampos WITH (NOLOCK) WHERE nom_campo = ''ErrorBCBP'' AND cod_campo = ''13'''
								EXEC sp_ExecuteSQL @mysl, N'@pCod_campo varchar(5) OUTPUT, @pDsc_campo varchar(80) OUTPUT', @pCod_campo = @Cod_campo OUTPUT, @pDsc_campo = @Dsc_campo OUTPUT

								SET @mysl  = N'INSERT INTO ' + @Link + '.'+@BD +'.dbo.TUA_BoardingBcbpErr (Dsc_Trama_Bcbp, Tip_Error, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, Tip_Ingreso, '+
											' Cod_Equipo_Mod,Tip_Boarding,Cod_Compania,Num_Vuelo,Fch_Vuelo,Num_Asiento,Nom_Pasajero,Log_Error) '+
											'VALUES (@pDsc_Trama_Bcbp, @pCod_campo, @pLog_Usuario_Mod, @pLog_Fecha_Mod, @pLog_Hora_Mod,''A'', '+
											' @pCod_Equipo_Mod,'''', @pCod_Compania, @pNum_Vuelo, @pFch_Vuelo, @pNum_Asiento, @pNom_Pasajero, @pDsc_campo ) '

								print @Num_Secuencial_Bcbp
								EXEC sp_executeSQL @mysl, @Param,
												@pDsc_Trama_Bcbp = @Dsc_Trama_Bcbp,
												@pCod_campo = @Cod_campo,														
												@pLog_Usuario_Mod = @Log_Usuario_Mod,
												@pLog_Fecha_Mod = @Log_Fecha_Mod,
												@pLog_Hora_Mod = @Log_Hora_Mod,
												@pCod_Equipo_Mod = @Cod_Equipo_Mod,
												@pCod_Compania = @Cod_Compania,
												@pNum_Vuelo = @Num_Vuelo,
												@pFch_Vuelo = @Fch_Vuelo,
												@pNum_Asiento = @Num_Asiento,
												@pNom_Pasajero = @Nom_Pasajero,
												@pDsc_campo = @Dsc_campo

								SET @nTotal_RegErr = @nTotal_RegErr + 1
								print @nTotal_RegErr
							END
							ELSE
							BEGIN
							        -- insertar BP nuevo si existe vuelo programado 
										SET @Param = N' @pCod_Compania char(10), @pNum_Vuelo char(10), @pFch_Vuelo char(8), @pNum_Asiento char(10), @pNom_Pasajero char(50), @pDsc_Trama_Bcbp char(200), '+
														' @pLog_Usuario_Mod char(7), @pLog_Fecha_Mod char(8), @pLog_Hora_Mod char(6), @pTip_Ingreso char(1), @pTip_Estado varchar(1), @pCod_Equipo_Mod varchar(20), '+
														' @pDsc_Boarding_Estado varchar(20), @pNum_Rehabilitaciones INT, @pCod_Unico_Bcbp varchar(20), @pFch_Vencimiento char(8), @pFch_Creacion char(8), @pHor_Creacion char(6), '+
														' @pCod_Unico_Bcbp_Rel varchar(20), @pFlg_Sincroniza char(1), @pTip_Pasajero char(1), @pTip_Vuelo char(1), @pTip_Trasbordo char(1), @pFlg_Tipo_Bcbp char(1), '+
														' @pNum_Secuencial_Bcbp_Rel char(1), @pNum_Secuencial_Bcbp_Rel_Sec char(1), @pNro_Boarding char(5), @pDsc_Destino varchar(3), @pCod_Eticket char(13), '+
														' @pNum_Proceso_Rehab INT, @pFlg_Bloqueado char(1), @pFlg_WSError char(1), @pFlg_Incluye_Tuua char(1), @pDsc_Observacion varchar(255), @pFch_Rehabilitacion char(8), '+
														' @pNum_Airline_Code char(3), @pNum_Document_Form char(10), @pCod_Error char(1) OUTPUT '

										SET @mysl = N' '+@Link + '.'+@BD +'.dbo.usp_scz_pcs_boardingbcbp_ins @pCod_Compania, @pNum_Vuelo, @pFch_Vuelo, @pNum_Asiento, '+
														' @pNom_Pasajero, @pDsc_Trama_Bcbp, @pLog_Usuario_Mod, @pLog_Fecha_Mod, @pLog_Hora_Mod, @pTip_Ingreso, @pTip_Estado, @pCod_Equipo_Mod, '+
														' @pDsc_Boarding_Estado, @pNum_Rehabilitaciones, @pCod_Unico_Bcbp, @pFch_Vencimiento, @pFch_Creacion, @pHor_Creacion, '+
														' @pCod_Unico_Bcbp_Rel, @pFlg_Sincroniza, @pTip_Pasajero, @pTip_Vuelo, @pTip_Trasbordo, @pFlg_Tipo_Bcbp, '+
														' @pNum_Secuencial_Bcbp_Rel, @pNum_Secuencial_Bcbp_Rel_Sec, @pNro_Boarding, @pDsc_Destino, @pCod_Eticket, '+
														' @pNum_Proceso_Rehab, @pFlg_Bloqueado, @pFlg_WSError, @pFlg_Incluye_Tuua, @pDsc_Observacion, @pFch_Rehabilitacion, '+
														' @pNum_Airline_Code, @pNum_Document_Form, @pCod_Error '
                                        --print 'Insertar  BCBP'
										EXEC sp_ExecuteSQL @mysl, @Param, @pCod_Compania = @Cod_Compania,
														@pNum_Vuelo = @Num_Vuelo, 
														@pFch_Vuelo = @Fch_Vuelo, 
														@pNum_Asiento = @Num_Asiento, 
														@pNom_Pasajero = @Nom_Pasajero, 
														@pDsc_Trama_Bcbp = @Dsc_Trama_Bcbp,
														@pLog_Usuario_Mod = @Log_Usuario_Mod, 
														@pLog_Fecha_Mod = @Log_Fecha_Mod, 
														@pLog_Hora_Mod = @Log_Hora_Mod, 
														@pTip_Ingreso = @Tip_Ingreso, 
														@pTip_Estado = @Tip_Estado, 
														@pCod_Equipo_Mod = @Cod_Equipo_Mod,
														@pDsc_Boarding_Estado = @Dsc_Boarding_Estado, 
														@pNum_Rehabilitaciones = @Num_Rehabilitaciones, 
														@pCod_Unico_Bcbp = @Cod_Unico_Bcbp, 
														@pFch_Vencimiento = @Fch_Vencimiento, 
														@pFch_Creacion = @Fch_Creacion, 
														@pHor_Creacion = @Hor_Creacion,
														@pCod_Unico_Bcbp_Rel = @Cod_Unico_Bcbp_Rel, 
														@pFlg_Sincroniza = '1', 
														@pTip_Pasajero = @Tip_Pasajero, 
														@pTip_Vuelo = @Tip_Vuelo, 
														@pTip_Trasbordo = @Tip_Trasbordo, 
														@pFlg_Tipo_Bcbp = @Flg_Tipo_Bcbp,
														@pNum_Secuencial_Bcbp_Rel = @Num_Secuencial_Bcbp_Rel, 
														@pNum_Secuencial_Bcbp_Rel_Sec = @Num_Secuencial_Bcbp_Rel_Sec, 
														@pNro_Boarding = @Nro_Boarding, 
														@pDsc_Destino = @Dsc_Destino, 
														@pCod_Eticket = @Cod_Eticket, 
														@pNum_Proceso_Rehab = @Num_Proceso_Rehab, 
														@pFlg_Bloqueado = @Flg_Bloqueado, 
														@pFlg_WSError = @Flg_WSError, 
														@pFlg_Incluye_Tuua = @Flg_Incluye_Tuua, 
														@pDsc_Observacion = NULL, 
														@pFch_Rehabilitacion = @Fch_Rehabilitacion,
														@pNum_Airline_Code = @Num_Airline_Code, 
														@pNum_Document_Form = @Num_Document_Form,
														@pCod_Error = @Cod_Error OUTPUT

								IF @Cod_Error = '0'
								BEGIN
									--ACTUALIZANDO EL FLAG DE BD LOCAL
									UPDATE dbo.TUA_BoardingBcbp SET Flg_Sincroniza='1' WHERE Cod_Compania=@Cod_Compania AND Num_Vuelo=@Num_Vuelo AND Fch_Vuelo=@Fch_Vuelo AND Num_Asiento=@Num_Asiento AND Nom_Pasajero=@Nom_Pasajero AND Num_Secuencial_Bcbp = @Num_Secuencial_Bcbp
								END
							END
						END

						SET @nCnta_Reg = @nCnta_Reg + 1
						SET @nTotal_Reg = @nTotal_Reg + 1

						IF @nCnta_Reg = @nInter_Reg
						BEGIN
							SET @nCnta_Reg = 0

							--- Verificacion del Estado del Proceso si esta cancelado
							SET @mysl = N'SELECT @pEstado_proc = Tip_Estado FROM ' + @Link + '.'+@BD +'.dbo.TUA_Sincronizacion WHERE cod_Sincronizacion = @pProceso '
							EXEC sp_ExecuteSQL @mysl, N'@pEstado_proc char(1) OUTPUT, @pProceso INT ', @pEstado_proc = @cEstado_proc OUTPUT, @pProceso = @nProceso

							IF @cEstado_proc = 'C' 
							BEGIN
								SET @nCancelado = 1
								SET @cFch_Fin_Registro = GETDATE()
								SET @Param = N'@pFch_Fin_Registro DATETIME, '+
										N'@pTotal_Reg INT, '+
										N'@pProceso INT, '+
										N'@pTotal_RegErr INT '

								SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_Sincronizacion SET Fch_Fin_Registro = @pFch_Fin_Registro, Num_Registro = @pTotal_Reg, Num_RegErr = @pTotal_RegErr '+
											' WHERE Cod_Sincronizacion = @pProceso '

								EXEC sp_executesql @mysl, @Param,
											@pFch_Fin_Registro = @cFch_Fin_Registro,
											@pTotal_Reg = @nTotal_Reg,
											@pProceso = @nProceso,
											@pTotal_RegErr = @nTotal_RegErr

								SET @Dsc_Information=@Dsc_Information+'Cierra Transaccion Boarding por Cancelacion.['+@cDsc_IP+']#'
								BREAK
							END

						END
						--print 'Antes de Fetch'
						FETCH NEXT FROM @c_BCBP INTO @Num_Secuencial_Bcbp, @Cod_Compania, @Num_Vuelo, @Fch_Vuelo, @Num_Asiento, @Nom_Pasajero, @Dsc_Trama_Bcbp, @Log_Usuario_Mod,
							@Log_Fecha_Mod, @Log_Hora_Mod, @Tip_Ingreso, @Tip_Estado, @Num_Rehabilitaciones, @Cod_Unico_Bcbp, @Fch_Vencimiento, @Fch_Creacion, @Hor_Creacion,
							@Cod_Unico_Bcbp_Rel, @Flg_Sincroniza, @Tip_Pasajero, @Tip_Vuelo, @Tip_Trasbordo, @Tip_Anulacion, @Cod_Numero_Bcbp, @Num_Serie, @Flg_Tipo_Bcbp,
							@Num_Secuencial_Bcbp_Rel, @Num_Secuencial_Bcbp_Rel_Sec, @Nro_Boarding, @Dsc_Destino, @Cod_Eticket, @Cod_Moneda, @Imp_Precio, @Imp_Tasa_Compra,
							@Imp_Tasa_Venta, @Num_Proceso_Rehab, @Flg_Bloqueado, @Flg_WSError, @Flg_Incluye_Tuua, @Fch_Rehabilitacion, @Num_Airline_Code, @Num_Document_Form
						--print 'Despues de Fetch'	
				END
				CLOSE @c_BCBP
				DEALLOCATE @c_BCBP
			END
			ELSE
				SET @Dsc_Information=@Dsc_Information+'Sincronizacion de Boarding no se realiza, no hay informacion.['+@cDsc_IP+']#'

			IF @nCancelado = 0
			BEGIN
				IF @NO_BP = 1
				BEGIN
					SET @cFch_Fin_Registro = GETDATE()
					SET @Param = N'@pFch_Fin_Registro DATETIME, '+
							N'@pTotal_Reg INT, '+
							N'@pProceso INT, '+
							N'@pTotal_RegErr INT '

					SET @mysl  = N' UPDATE ' + @Link +'.'+ @BD +'.dbo.TUA_Sincronizacion SET Tip_Estado = ''T'', Fch_Fin_Registro = @pFch_Fin_Registro, Num_Registro = @pTotal_Reg, Num_RegErr = @pTotal_RegErr '+
								' WHERE Cod_Sincronizacion = @pProceso '

					EXEC sp_executesql @mysl, @Param, 
								@pFch_Fin_Registro = @cFch_Fin_Registro,
								@pTotal_Reg = @nTotal_Reg,
								@pProceso = @nProceso,
								@pTotal_RegErr = @nTotal_RegErr

					SET @Dsc_Information=@Dsc_Information+'TUA_BoardingBcbp Sincronizada Correctamente.['+@cDsc_IP+']#'
					SET @Dsc_Information=@Dsc_Information+'Cierra Transaccion Boarding .['+@cDsc_IP+']#'
				END
			END

			END TRY
			BEGIN CATCH
				SET @Num_Secuencial = NULL
				SET @nCancelado = 2
				SET @Dsc_Message='TUA_BoardingBcbp Error en Transaccion. # Servidor Local : '+@cDsc_IP+' # Nro Error : '+CONVERT(varchar(20), ERROR_NUMBER())+' - Mensaje Error : '+ERROR_MESSAGE()
				IF XACT_STATE() <> 0
					ROLLBACK --TRANSACTION
			END CATCH
			
			IF @nCancelado <> 0
			BEGIN
				SET @Dsc_Information=@Dsc_Information+'TUA_BoardingBcbp Proceso Cancelado.['+@cDsc_IP+']#'
				SET @Dsc_Information=@Dsc_Information+'Sincronizacion de Boarding Cancelado.['+@cDsc_IP+']#'
			END
		END
		ELSE
		BEGIN
			SET @Dsc_Information=@Dsc_Information+'TUA_Ticket Proceso Cancelado.['+@cDsc_IP+']#'
			SET @Dsc_Information=@Dsc_Information+'Sincronizacion de Ticket Cancelado.['+@cDsc_IP+']#'
		END
	END
END
GO
