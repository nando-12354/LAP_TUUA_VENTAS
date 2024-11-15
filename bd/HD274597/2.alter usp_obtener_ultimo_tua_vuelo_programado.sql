alter proc usp_obtener_ultimo_tua_vuelo_programado
as
begin
    SELECT top 1 *
    FROM [dbo].[TUA_VueloProgramado]
    ORDER BY Log_Fecha_Mod desc, Log_Hora_Mod desc 
end
go

