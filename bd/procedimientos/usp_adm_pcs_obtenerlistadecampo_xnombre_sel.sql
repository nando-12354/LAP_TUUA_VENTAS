
ALTER PROCEDURE usp_adm_pcs_obtenerlistadecampo_xnombre_sel --'EstadoUsuario'

/*----------------------------------------------------------------------------------------------------------------
Permite seleccionar la lista de campos según el nombre de campo consultado

------------------------------------------------------------------------------------------------------------------
AUTHOR    : HIPER
DATE      : 25-Feb-2015
USE       : GPV
REVIEWS   : Daniel Castillo (04-Mar-2015) Se debe establecer el código de causal de rehabilitación de
		  : mal tiempo como primera opcion
----------------------------------------------------------------------------------------------------*/



(

	@Nom_Campo varchar(50)

)



AS



SET NOCOUNT ON



IF @Nom_Campo ='CausalRehabilitacion'

begin

	declare @cod_defecto as varchar(5)

	set @cod_defecto = '7'

	

	SELECT '0' as num, 

	    [Nom_Campo],

		[Cod_Campo],

		[Cod_Relativo],

		[Dsc_Campo]

	FROM [TUA_ListaDeCampos]

	WHERE [Nom_Campo] = @Nom_Campo

	and Cod_Campo = @cod_defecto

	UNION

	SELECT [Cod_Campo] as num,[Nom_Campo],

		[Cod_Campo],

		[Cod_Relativo],

		[Dsc_Campo]

	FROM [TUA_ListaDeCampos]

	WHERE [Nom_Campo] = @Nom_Campo

	and Cod_Campo <> @cod_defecto

	order by 1

end

ELSE

BEGIN

		SELECT [Nom_Campo],

		[Cod_Campo],

		[Cod_Relativo],

		[Dsc_Campo]

	FROM [TUA_ListaDeCampos]

	WHERE [Nom_Campo] = @Nom_Campo

	ORDER BY 2



END


























