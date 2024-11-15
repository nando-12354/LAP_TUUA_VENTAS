using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using LAP.Seguridad.Entidades;
using LAP.Tuua.Web.Model;

namespace LAP.Tuua.Web.Dao
{
    public class Saa_TokenDAO
    {
        public void InsertarToken(Saa_Token token)
        {
            string cnstr = ConfigurationManager.ConnectionStrings["HelpDesk"].ConnectionString;
            using (var conn = new SqlConnection(cnstr))
            {

                var affectedRows = conn.Execute("usp_saa_insertar_token", new { token.dsc_token, token.cod_usuario, token.fch_acceso }, commandType: CommandType.StoredProcedure);
                Console.WriteLine($"Affected Rows; {affectedRows}");

            }
        }
        public Saa_Token ObtenerToken(string strToken)
        {
            Saa_Token token = null;
            string cnstr = ConfigurationManager.ConnectionStrings["HelpDesk"].ConnectionString;
            using (var conn = new SqlConnection(cnstr))
            {
                var lsToken = conn.Query<Saa_Token>("usp_saa_consultar_token", new { dsc_token = strToken }, commandType: CommandType.StoredProcedure);
                if (lsToken != null)
                {
                    token = lsToken.FirstOrDefault();
                }
            }
            return token;
        }

        public Usuario LoginExtranet(Usuario objUsuario)
        {
            

            Saa_Usuario_DB usuarioDb = null;


            string cnstr = ConfigurationManager.ConnectionStrings["HelpDesk"].ConnectionString;
            using (var conn = new SqlConnection(cnstr))
            {
                var lsToken = conn.Query<Saa_Usuario_DB>("usp_saa_cns_usuario_sel", new { CODIGO = objUsuario.Codigo }, commandType: CommandType.StoredProcedure);
                if (lsToken != null)
                {
                    usuarioDb = lsToken.FirstOrDefault();
                    objUsuario.Codigo = usuarioDb.AAUS_CODIGO;
                    objUsuario.Login = usuarioDb.AAUS_LOGIN;
                    objUsuario.Password = usuarioDb.AAUS_PASSWORD;
                    objUsuario.Apellidos = usuarioDb.AAUS_APELLIDOS;
                    objUsuario.Email = usuarioDb.AAUS_EMAIL;
                    objUsuario.Activo = usuarioDb.AAUS_ACTIVO;
                    objUsuario.EsExterno = usuarioDb.AAUS_EXTERNO;
                }
            }

            return objUsuario;


        }
        

    }


}