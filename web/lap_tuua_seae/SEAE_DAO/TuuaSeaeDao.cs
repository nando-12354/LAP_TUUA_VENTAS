using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEAE_Entidades;

namespace SEAE_DAO
{
    public class TuuaSeaeDao
    {
        private readonly IDataAccess _dataAccess;
        public TuuaSeaeDao(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /*Fch_Creacion = '20210308' and b.Num_Vuelo = 'LA2105' and Num_Asiento = '011E' and Nom_Pasajero like '%%'*/
        public async Task<TuuaSeae> ConsultarBcbp(string fechaCreacion, string numVuelo, string numAsiento, string nomPax)
        {
            TuuaSeae ls;

            string query = @"select Cod_Numero_Bcbp, Num_Serie, Num_Secuencial, b.Cod_Compania,
                    Fch_Vuelo, b.Fch_Creacion, b.Hor_Creacion, Num_Vuelo, Dsc_Destino, Num_Asiento,
                    c.Dsc_Compania, Nom_Pasajero,Tip_Vuelo, Imp_Precio,Cod_Eticket
                    from TUA_BoardingBcbp as b
                    inner join TUA_Compania as c
                    on b.Cod_Compania = c.Cod_Compania
                    where b.Fch_Vuelo = @Fch_Creacion and b.Num_Vuelo = @Num_Vuelo and Num_Asiento like @Num_Asiento and Nom_Pasajero like @Nom_Pasajero";
                
            var output = await _dataAccess.Query<TuuaSeae>(query,new {Fch_Creacion = fechaCreacion, Num_Vuelo = numVuelo,Num_Asiento = $"%{numAsiento}%", Nom_Pasajero = $"%{nomPax}%" });
            ls = output.FirstOrDefault();
            
            return ls;
        }
        
        public async Task<List<TuuaCompania>> GetCompanias()
        {
            List<TuuaCompania> ls;

            string query = @"select Cod_Compania, Dsc_Compania
                            from TUA_Compania
                            where Tip_Estado > 0
                            order by Dsc_Compania";
                
            var output = await _dataAccess.Query<TuuaCompania>(query);
            ls = output.ToList();
            
            return ls;
        }
        
        public async Task<List<TuuaVuelo>> GetVuelosCompaniaFecha(string codCompania, string fchVuelo)
        {
            List<TuuaVuelo> ls;

            string query = @"select distinct Num_Vuelo, Fch_Vuelo, Dsc_Destino
                            from TUA_BoardingBcbp (nolock)
                            where Fch_Vuelo = @Fch_Vuelo and Cod_Compania = @Cod_Compania";
                
            var output = await _dataAccess.Query<TuuaVuelo>(query, new {Fch_Vuelo = fchVuelo, Cod_Compania = codCompania});
            ls = output.ToList();
            
            return ls;
        }
    }
}