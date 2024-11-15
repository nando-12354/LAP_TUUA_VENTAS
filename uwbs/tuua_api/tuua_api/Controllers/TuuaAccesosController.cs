using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using tuua_api.BL;
using tuua_api.Dao;
using tuua_api.Entidades;
using tuua_api.Util;

namespace tuua_api.Controllers
{
    public class TuuaAccesosController : ApiController
    {
        public Loger loger;

        [HttpGet]
        public IHttpActionResult GetStatus() {
            if (loger == null) loger = new Loger();
            loger.Log("GetStatus", "info");

            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("respuesta", "ok");
            payload.Add("mensaje", "conexión satisfactoria");


            return Ok(payload);
        }


        //utilizar método asincrono

        [HttpPost]
        public async Task<IHttpActionResult> PostRegistrarTrama([FromBody] Payload payload)
        {
            if (loger == null) loger = new Loger();
            /*if (payload !=null) {
                string jsonSerialized = JsonConvert.SerializeObject(payload);

                //loger.Log("PostRegistrarTrama: " + jsonSerialized, "info");
            }*/

            Dictionary<string, string> resp = new Dictionary<string, string>();
            try {
                //obtener molinete y procesar trama
                MolineteDao molineteDao = new MolineteDao();

                TUA_Molinete molinete = await molineteDao.getMolineteID(payload.CodPuerta);

                Controlador controlador = new Controlador();
                await controlador.ProcesarTrama(payload.Trama, molinete);

                
                resp.Add("respuesta", "ok");
                resp.Add("color", "verde");
                resp.Add("mensaje", "Registro satisfactorio");
                //determinar tipo de trama

                // llave de destrabe
                // llave para cerrar puerta
                // sticker tuua
                // boardingpass.
                return Ok(resp);

            }
            catch (Exception ex) {
                loger.Log(ex.Message, "Error");
             
                resp.Add("respuesta", "error");
                if (ex.Message == "PASAJERO EN TRANSITO DOM")
                {
                    resp.Add("color", "ambar");
                }
                else {
                    resp.Add("color", "rojo");
                }
                
                resp.Add("mensaje", ex.Message);
                return Ok(resp);
            }

          
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostRegistrarTramaContingencia([FromBody] Payload trama)
        {
            if (loger == null) loger = new Loger();
            if (trama != null)
            {
                string jsonSerialized = JsonConvert.SerializeObject(trama);
                loger.Log("PostRegistrarTrama CONTINGENCIA: " + jsonSerialized, "info");
            }
            Dictionary<string, string> resp = new Dictionary<string, string>();
            try
            {
               
                // en contingenicia no se debe validar la fecha de vuelo

                MolineteDao molineteDao = new MolineteDao();

                TUA_Molinete molinete = await molineteDao.getMolineteID(trama.CodPuerta);

                Controlador controlador = new Controlador();
                await controlador.ProcesarTramaContingencia(trama.Trama, molinete);


                //determinar tipo de trama

                // llave de destrabe
                // llave para cerrar puerta
                // sticker tuua
                // boardingpass


                resp.Add("respuesta", "ok");
                resp.Add("color", "verde");
                resp.Add("mensaje", "Boardingpass - Registro satisfactorio");


                return Ok(resp);

            }
            catch (Exception ex)
            {
                loger.Log(ex.Message, "Error");

                resp.Add("respuesta", "error");
                if (ex.Message == "PASAJERO EN TRANSITO DOM")
                {
                    resp.Add("color", "rojo");
                }
                else
                {
                    resp.Add("color", "ambar");
                }

                resp.Add("mensaje", ex.Message);
                return Ok(resp);
            }
            
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetLlavesDestrabe()
        {
            try
            {
                if (loger == null) loger = new Loger();
                loger.Log("GetLlavesDestrabe", "info");

                UsuariosDao dao = new UsuariosDao();

                List<TUA_Destrabe> molinetes = await dao.getLlavesDestrabe();
                return Ok(molinetes);
            }
            catch (Exception ex)
            {
                loger.Log(ex.Message, "Error");
                loger.Log(ex.StackTrace, "Error");

                return InternalServerError(ex);

            }

        }



        //vuelos transito

        [HttpGet]
        public async Task<IHttpActionResult> GetVuelosProgramados(string tipo_operacion, string tipo_vuelo)
        {
            try
            {
                if (loger == null) loger = new Loger();
                loger.Log("GetVuelosProgramados", "info");

                VueloTransitoDao dao = new VueloTransitoDao();

                List<TUA_Vuelo_Transito> ls = await dao.obtenerVuelosTransitoActuales(tipo_operacion,tipo_vuelo,12);
                return Ok(ls);
            }
            catch (Exception ex)
            {
                loger.Log(ex.Message, "Error");
                loger.Log(ex.StackTrace, "Error");

                return InternalServerError(ex);

            }

        }
               

        [HttpPost]
        public async Task<IHttpActionResult> PostRegistrarPaxTransito([FromBody] TUA_Transito payload)
        {
            if (loger == null) loger = new Loger();
            if (payload != null)
            {
                string jsonSerialized = JsonConvert.SerializeObject(payload);

                loger.Log("PostRegistrarPaxTransito: " + jsonSerialized, "info");
            }

            Dictionary<string, string> resp = new Dictionary<string, string>();
            try
            {
                //obtener molinete y procesar trama
                VueloTransitoDao transitoDao = new VueloTransitoDao();
                MolineteDao molineteDao = new MolineteDao();
                ValidadorTrama validador = new ValidadorTrama();

                TUA_Vuelo_Transito vueloOrigen = null;
                TUA_Vuelo_Transito vueloDestino = null;

                TUA_Molinete molinete = await molineteDao.getMolineteID(payload.Cod_Molinete);
                if (molinete == null) {
                    throw new Exception("Puerta no registrada");
                }

                //validaciones

                if (!string.IsNullOrEmpty(payload.Trama_Origen)) {
                    //validar trama de origen
                    //completar datos del vuelo
                    BCBP boardingOrigen = validador.convertirTrama(payload.Trama_Origen);
                    //obtener Vuelo 
                   


                    

                    int fechaDiasOrigen = Int32.Parse(boardingOrigen.FlightDate.Trim());
                    payload.Fch_Vuelo_Origen = (new DateTime(DateTime.Today.Year - 1, 12, 31)).AddDays(fechaDiasOrigen);

                    vueloOrigen = await transitoDao.obtenerVueloTransitoCodigo(boardingOrigen.AirLineCode.Trim(), boardingOrigen.FlightNumber.Trim(), payload.Fch_Vuelo_Origen, "L");

                    payload.Num_Vuelo_Origen = vueloOrigen.Num_Vuelo;

                }

                if (!string.IsNullOrEmpty(payload.Trama_Destino))
                {
                    //validar trama de origen
                    //completar datos del vuelo
                    BCBP boardingDestino = validador.convertirTrama(payload.Trama_Destino);
                    

                    //obtener vuelo



                    int fechaDiasDestino = Int32.Parse(boardingDestino.FlightDate.Trim());
                    payload.Fch_Vuelo_Destino = (new DateTime(DateTime.Today.Year - 1, 12, 31)).AddDays(fechaDiasDestino);

                    vueloDestino = await transitoDao.obtenerVueloTransitoCodigo(boardingDestino.AirLineCode.Trim(), boardingDestino.FlightNumber.Trim(), payload.Fch_Vuelo_Destino, "S");
                    payload.Num_Vuelo_Destino = vueloDestino.Num_Vuelo;

                }
                if (payload.Fch_Vuelo_Origen == null)
                {
                    throw new Exception("Establecer la fecha de vuelo de origen");
                }

                if (payload.Fch_Vuelo_Destino == null)
                {
                    throw new Exception("Establecer la fecha de vuelo de destino");
                }

                //validar datos de vuelo
                if (string.IsNullOrEmpty(payload.Num_Vuelo_Origen)) {
                    throw new Exception("Establecer el número de vuelo de origen");
                }
                if (string.IsNullOrEmpty(payload.Num_Vuelo_Destino)) {
                    throw new Exception("Establecer el número de vuelo de destino");
                }

                //buscar vuelo de origen
                if (vueloOrigen == null) {
                    vueloOrigen = await transitoDao.obtenerVueloTransitoNumVuelo(payload.Num_Vuelo_Origen, payload.Fch_Vuelo_Origen, "L");
                }
                
                //buscar vuelo de destino
                if (vueloDestino == null)
                {
                    vueloDestino = await transitoDao.obtenerVueloTransitoNumVuelo(payload.Num_Vuelo_Destino, payload.Fch_Vuelo_Destino, "S");
                }

                if (vueloOrigen == null)
                {
                    throw new Exception("Vuelo de origen no programado");
                }
                if (vueloDestino == null) {
                    throw new Exception("Vuelo de destino no programado");
                }


                await transitoDao.registrar_pasajero_transito(payload);

                resp.Add("respuesta", "ok");
                resp.Add("color", "verde");
                resp.Add("mensaje", "Registro satisfactorio");
                //determinar tipo de trama

                // llave de destrabe
                // llave para cerrar puerta
                // sticker tuua
                // boardingpass.
                return Ok(resp);

            }
            catch (Exception ex)
            {
                loger.Log(ex.Message, "Error");
                resp.Add("respuesta", "error");
                resp.Add("color", "rojo");
                resp.Add("mensaje", ex.Message);
                return Ok(resp);
            }


        }


        [HttpPost]
        public async Task<IHttpActionResult> PostObtenerVueloTrama([FromUri] string tip_operacion, [FromBody] string trama) {
            VueloTransitoDao transitoDao = new VueloTransitoDao();
            ValidadorTrama validador = new ValidadorTrama();
            try
            {

                if (string.IsNullOrEmpty(tip_operacion)) {
                    throw new Exception("Debe indicar el tipo de operacion");
                }
                if (string.IsNullOrEmpty(trama))
                {
                    throw new Exception("Debe registrar la trama");
                }

                TUA_Vuelo_Transito vuelo = null;
                BCBP boarding = validador.convertirTrama(trama);

                int fechaDias = Int32.Parse(boarding.FlightDate.Trim());
                DateTime  Fch_Vuelo = (new DateTime(DateTime.Today.Year - 1, 12, 31)).AddDays(fechaDias);

                vuelo = await transitoDao.obtenerVueloTransitoCodigo(boarding.AirLineCode.Trim(), boarding.FlightNumber.Trim(), Fch_Vuelo, tip_operacion);

                if (vuelo==null) {
                    return NotFound();
                    
                }

                return Ok(vuelo);

            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            
           


        }




        [HttpGet]
        public async Task<IHttpActionResult> GetMolinetes() {
            try
            {
                if (loger == null) loger = new Loger();
                loger.Log("GetMolinetes", "info");

                MolineteDao dao = new MolineteDao();

                List<TUA_Molinete> molinetes = await dao.getMolinetes();
                return Ok(molinetes);
            }
            catch (Exception ex)
            {
                loger.Log(ex.Message, "Error");
                loger.Log(ex.StackTrace, "Error");


                return InternalServerError(ex);

            }
            
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMolinetePorCodigo(string cod_molinete)
        {
            try
            {
                if (loger == null) loger = new Loger();
                loger.Log("GetMolinetePorCodigo "+ cod_molinete, "info");

                MolineteDao dao = new MolineteDao();

                TUA_Molinete molinete = await dao.getMolineteID(cod_molinete);
                return Ok(molinete);
            }
            catch (Exception ex)
            {
                loger.Log(ex.Message, "Error");
                loger.Log(ex.StackTrace, "Error");

                return InternalServerError(ex);

            }

        }

    }
}
