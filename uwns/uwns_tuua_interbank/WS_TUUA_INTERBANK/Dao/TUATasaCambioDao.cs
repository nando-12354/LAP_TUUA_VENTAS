using Dapper;
using PruebaWSInterbank.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PruebaWSInterbank.Dao
{
    public class TUATasaCambioDao
    {
        string connectionString;
        public TUATasaCambioDao()
        {
            connectionString = ConfigurationManager.ConnectionStrings["cnnstr"].ConnectionString;
        }

        public async Task<List<TUATasaCambio>> GetAll()
        {
            List<TUATasaCambio> ls = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var output = await connection.QueryAsync<TUATasaCambio>("select * from TUA_TasaCambio");
                ls = output.ToList();
            }
            return ls;
        }

        public async Task<TUATasaCambio> GetById(string id)
        {
            TUATasaCambio ls = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var output = await connection.QueryAsync<TUATasaCambio>("select * from TUA_TasaCambio where Cod_Tasa_Cambio = @cod", new { cod = id });
                ls = output.FirstOrDefault();
            }
            return ls;
        }


        public async Task Add(TUATasaCambio tasacambio, TUATasaCambio old)
        {
            //iniciar transacción
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) { 
            
                using (var connection = new SqlConnection(connectionString))
                {
                    // 1. Insertar Nuevo Tipo de Cambio
                    var param = new
                    {
                        Cod_Tasa_Cambio = tasacambio.Cod_Tasa_Cambio,
                        Tip_Cambio = tasacambio.Tip_Cambio,
                        Cod_Moneda = tasacambio.Cod_Moneda,
                        Imp_Cambio_Actual = tasacambio.Imp_Cambio_Actual,
                        Log_Usuario_Mod = tasacambio.Log_Usuario_Mod,
                        Tip_Ingreso = tasacambio.Tip_Ingreso,
                        Tip_Estado = tasacambio.Tip_Estado,
                        Fch_Prog = tasacambio.Fch_Programacion,
                        Cod_Modulo_Mod = "",
                        Cod_SubModulo_Mod = ""
                    };

                    string query = @"EXECUTE [dbo].[usp_ope_pcs_tasacambio_ins] 
                                                   @Cod_Tasa_Cambio
                                                  ,@Tip_Cambio
                                                  ,@Cod_Moneda
                                                  ,@Imp_Cambio_Actual
                                                  ,@Log_Usuario_Mod
                                                  ,@Tip_Ingreso
                                                  ,@Tip_Estado
                                                  ,@Fch_Prog
                                                  ,@Cod_Modulo_Mod
                                                  ,@Cod_SubModulo_Mod";

                    await connection.ExecuteAsync(query, param);

                    //2. insertar en historial en funcion al codigo de tasa de cambio antiguo
                    string queryHist = @"EXECUTE [dbo].[usp_ope_pcs_tasacambiohist_ins] 
                       @Cod_Tasa_Cambio
                      ,@Tip_Cambio
                      ,@Cod_Moneda
                      ,@Imp_Valor
                      ,@Cod_Moneda2
                      ,@Imp_Valor2
                      ,@Fch_Ini
                      ,@Fch_Fin
                      ,@Log_Usuario_Mod
                      ,@Cod_Modulo_Mod
                      ,@Cod_SubModulo_Mod";

                    var paramHist = new
                    {
                        Cod_Tasa_Cambio = old.Cod_Tasa_Cambio,
                        Tip_Cambio = old.Tip_Cambio,
                        Cod_Moneda = old.Cod_Moneda,
                        Imp_Valor = old.Imp_Cambio_Actual,
                        Cod_Moneda2 = tasacambio.Cod_Moneda,
                        Imp_Valor2 = tasacambio.Imp_Cambio_Actual,
                        Fch_Ini = old.Fch_Proceso,
                        Fch_Fin = tasacambio.Fch_Proceso,
                        Log_Usuario_Mod = tasacambio.Log_Usuario_Mod,
                        Cod_Modulo_Mod = "",
                        Cod_SubModulo_Mod = ""
                    };

                    await connection.ExecuteAsync(queryHist, paramHist);

                    //3. eliminar tipo de cambio antiguo
                    string queryDel = @"delete from TUA_TasaCambio
                                        where Cod_Tasa_Cambio = @cod";

                    await connection.ExecuteAsync(queryDel, new { cod = old.Cod_Tasa_Cambio });
                                        
                    //finalizar transacción
                    transaction.Complete();
                    
                }
            }
        }

        public async Task Update(TUATasaCambio tasacambio)
        {

            using (var connection = new SqlConnection(connectionString))
            {

                string query = @"UPDATE [dbo].[mpv_tasacambio_correo]
                               SET [titulo] = @titulo
                                  ,[mensaje] = @mensaje
                             WHERE [cod_tasacambio] = @cod_tasacambio";

                await connection.ExecuteAsync(query, tasacambio);

            }
        }

        public async Task Delete(string id)
        {

            using (var connection = new SqlConnection(connectionString))
            {

                string query = @"delete from TUA_TasaCambio
                                 where Cod_Tasa_Cambio = @cod";

                await connection.ExecuteAsync(query, new { cod = id});

            }
        }

    }
}
