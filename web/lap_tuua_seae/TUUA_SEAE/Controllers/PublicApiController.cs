using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using SEAE_DAO;
using SEAE_Entidades;
using Serilog;


namespace TUUA_SEAE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PublicApiController : ApiController
    {
        private readonly TuuaSeaeDao _tuuaSeaeDao;
        public PublicApiController()
        {
            string motor = ConfigurationManager.AppSettings["motorbd"];
            IDataAccess dataAccess = DataAccessFactory.GetDataAccess(motor);
            _tuuaSeaeDao = new TuuaSeaeDao(dataAccess);
            
        }
        
        // GET
        [HttpGet]
        [Route("api/publicapi/GetCompanias")]
        public async Task<IHttpActionResult> GetCompanias()
        {
            try
            {
                List<TuuaCompania> ls = await _tuuaSeaeDao.GetCompanias();
                return Ok(ls);
            }
            catch (Exception e)
            {
                Log.Error(e,e.Message);
                Log.Error(e.StackTrace);
                
            }
            return StatusCode(HttpStatusCode.InternalServerError);
           
            
        }
        //get vuelos por compania y fecha
        [HttpGet]
        [Route("api/publicapi/GetVuelosCompaniaFecha")]
        public async Task<IHttpActionResult> GetVuelosCompaniaFecha([FromUri]string codCompania, [FromUri]DateTime fechaVuelo)
        {
            
            try
            {
                List<TuuaVuelo> ls = await _tuuaSeaeDao.GetVuelosCompaniaFecha(codCompania,fechaVuelo.ToString("yyyyMMdd"));
                return Ok(ls);
            }
            catch (Exception e)
            {
                Log.Error(e,e.Message);
                Log.Error(e.StackTrace);
                
            }
            return StatusCode(HttpStatusCode.InternalServerError);
            
        }
        
    }
}