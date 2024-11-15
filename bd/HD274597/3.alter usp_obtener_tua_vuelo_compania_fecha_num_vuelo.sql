alter proc usp_obtener_tua_vuelo_compania_fecha_num_vuelo
@Cod_Compania varchar(10),
@Fch_Vuelo varchar(8),
@Num_Vuelo varchar(10)
as
begin

    SELECT top 1 *
    FROM [dbo].[TUA_VueloProgramado]
    where Cod_Compania = @Cod_Compania and Fch_Vuelo = @Fch_Vuelo and Num_Vuelo = @Num_Vuelo

  end
go

