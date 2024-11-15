
/****** Object:  StoredProcedure [dbo].[usp_tuua_insertar_bp_transito]    Script Date: 28/02/2018 03:35:28 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------------------------------
--	AUTOR			: DCASTILLO
--	FECHA			: 14/02/2018
--	DESCRIPCION		: Insertar registro de pasajeros en tránsito 
-------------------------------------------------------------------------------------------------

CREATE PROC [dbo].[usp_tuua_insertar_bp_transito]
@num_vuelo varchar(10),
@fecVueloNum varchar(10),
@num_asiento varchar(10),
@num_checkin varchar(5),
@nom_pasajero varchar(50),
@dsc_trama_bcbp varchar(500),
@dsc_destino varchar(3),
@cod_iata char(2),
@fch_registro datetime,
@tip_vuelo char(1),
@tip_status_pax char(1),
@cod_molinete varchar(5),
@dsc_archivo varchar(50),
@usuario varchar(20)

as
begin

	declare @pastYear as int
	declare @strPastDate as varchar(20)
	declare @fch_vuelo as datetime
	declare @pastDate as datetime
	declare @cant as int
	declare @cpax as int
	declare @fechaActual as datetime
	
	set @fechaActual = getdate()
	set @pastYear = year(@fechaActual)-1

	set @strPastDate = cast(@pastYear as varchar)+'-12-31'
	set @pastDate = @strPastDate
	set @cant = cast(@fecVueloNum as int)
	set @fch_vuelo =  dateadd(DAY,@cant,@pastDate) 

	set @num_vuelo = @cod_iata+@num_vuelo

	set @cpax = 0

	select @cpax = count(*) 
	from TUA_BP_TRANSITO
	where [num_vuelo] = @num_vuelo and [fch_vuelo] = @fch_vuelo and num_asiento = @num_asiento and num_checkin = @num_checkin and nom_pasajero = @nom_pasajero

	if @cpax is null
	begin
		set @cpax = 0
	end

	if @cpax = 0
	begin
		INSERT INTO [dbo].[TUA_BP_TRANSITO]
			   ([num_vuelo]
			   ,[fch_vuelo]
			   ,[num_asiento]
			   ,[num_checkin]
			   ,[nom_pasajero]
			   ,[dsc_trama_bcbp]
			   ,[dsc_destino]
			   ,[cod_iata]
			   ,[fch_registro]
			   ,[tip_vuelo]
			   ,[tip_status_pax]
			   ,[cod_molinete]
			   ,[dsc_archivo]
			   ,[log_usr_cre]
			   ,[log_usr_mod]
			   ,[log_fch_cre]
			   ,[log_fch_mod]
			   )
		 VALUES
			   (@num_vuelo
			   ,@fch_vuelo
			   ,@num_asiento
			   ,@num_checkin
			   ,@nom_pasajero
			   ,@dsc_trama_bcbp
			   ,@dsc_destino
			   ,@cod_iata
			   ,@fch_registro
			   ,@tip_vuelo
			   ,@tip_status_pax
			   ,@cod_molinete
			   ,@dsc_archivo
			   ,@usuario
			   ,@usuario
			   ,@fechaActual
			   ,@fechaActual
			   )

	end

end
GO