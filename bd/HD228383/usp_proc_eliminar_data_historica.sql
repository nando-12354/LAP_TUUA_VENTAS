
/*----------------------------------------------------------------------------------------------------------------
Elimina la información Antigua de la base de datos operacional del sistema TUUA.
------------------------------------------------------------------------------------------------------------------
PROCEDURE : usp_proc_eliminar_data_historica : 
AUTHOR    : Daniel Castillo
DATE      : 22-Sept-2020
USE       : TUUA
----------------------------------------------------------------------------------------------------*/

create proc usp_proc_eliminar_data_historica
as
begin
    
	-- obtener cantidad de meses a mantener en base de datos operacional
	declare @nroMeses int

	select @nroMeses= cast(Valor as int) from TUA_ParameGeneral
	where Identificador = 'NRMA';
	if @nroMeses is null
	begin
		set @nroMeses = 12
	end

	-- fecha tope para eliminar la data
	declare @fechaHasta date
	set @fechaHasta = DATEADD(month, -1*@nroMeses, GETDATE())
	select @fechaHasta

	--1. eliminar estado hist�rico BCBP
	delete from TUA_BoardingBcbpEstHist
	where CONVERT(date,Log_Fecha_Mod,112) < @fechaHasta ;

	--2. eliminar errores BCBP

	delete from TUA_BoardingBcbpErr
	where CONVERT(date,Log_Fecha_Mod,112) <  @fechaHasta ;

	--3. eliminar BCBP

	delete from TUA_BoardingBcbp
	where CONVERT(date,Fch_Creacion,112) < @fechaHasta;


	--4. eliminar estado historico tickets
	delete from TUA_TicketEstHist
	where CONVERT(date,Log_Fecha_Mod,112) < @fechaHasta;


	--5. eliminar errores tickets
	delete from TUA_TicketErr
	where CONVERT(date,Log_Fecha_Mod,112) < @fechaHasta;

	-- 6. eliminar tickets (solo usados, vencidos y rehabilitados)
	delete from TUA_Ticket
	where Tip_Estado_Actual in ('U','X', 'R') and CONVERT(date,Log_Fecha_Mod,112) < @fechaHasta;

end
