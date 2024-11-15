using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SEAE_DAO;
using SEAE_Entidades;
using Serilog;
using ZXing;

namespace TUUA_SEAE.Controllers
{
    public class HomeController : Controller
    {
        private readonly TuuaSeaeDao _tuuaSeaeDao;

        public HomeController()
        {
            string motor = ConfigurationManager.AppSettings["motorbd"];
            IDataAccess dataAccess = DataAccessFactory.GetDataAccess(motor);
            
            _tuuaSeaeDao = new TuuaSeaeDao(dataAccess);
        }
        
        public async Task<ActionResult> Index()
        {
            
            ViewBag.urlBase =  ConfigurationManager.AppSettings["urlBase"];
            ViewBag.fechaAyer = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            ViewBag.nroVuelo = "";
            ViewBag.nroAsiento = "";
            ViewBag.nombrePax = "";
            
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Index(FormCollection frm)
        {
            ViewBag.urlBase =  ConfigurationManager.AppSettings["urlBase"];
            ViewBag.fechaAyer = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            
            try
            {
                string fechaCreacion =  DateTime.ParseExact(frm["fechaVuelo"],"yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyyMMdd") ;
                string numVuelo = frm["nroVuelo"];
                string numAsiento = frm["nroAsiento"];
                string nomPax = frm["nombrePax"];
            
                //validar
                if (string.IsNullOrEmpty(fechaCreacion))
                {
                    ViewBag.MensajeError = "Debe indicar la fecha del vuelo";
                    return View();
                }
                ViewBag.fechaAyer = DateTime.ParseExact(fechaCreacion,"yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                if (string.IsNullOrEmpty(numVuelo))
                {
                    ViewBag.MensajeError = "Debe indicar un número de vuelo";
                    return View();
                }
                ViewBag.nroVuelo = numVuelo;
                if (string.IsNullOrEmpty(numAsiento) && string.IsNullOrEmpty(nomPax))
                {
                    ViewBag.MensajeError = "Debe indicar un número de asiento o el nombre del pasajero";
                    return View();
                }
             
                ViewBag.nroAsiento = numAsiento;
                ViewBag.nombrePax = nomPax;
            
                TuuaSeae seae = await _tuuaSeaeDao.ConsultarBcbp(fechaCreacion,numVuelo,numAsiento, nomPax);

                if (seae != null && !string.IsNullOrEmpty(seae.Fch_Creacion))
                {
                    string hora = "000000";
                    if (!string.IsNullOrEmpty(seae.Hor_Creacion))
                    {
                        hora = seae.Hor_Creacion;
                    }
                    string fec = seae.Fch_Creacion +" "+ hora;
                
                    seae.FechaHoraCreacion = DateTime.ParseExact(fec,"yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
                    seae.StrFecha = seae.FechaHoraCreacion.ToString("dd/MM/yyyy");
                    seae.StrHora = seae.FechaHoraCreacion.ToString("HH:mm tt");
                
                    //generar código QR
                    var writer = new BarcodeWriter { Format = BarcodeFormat.QR_CODE };
                    var result = writer.Write(seae.Cod_Numero_Bcbp);
                    var barcodeBitmap = new Bitmap(result);
                    using (var streamImg = new MemoryStream())
                    {
                        barcodeBitmap.Save(streamImg,ImageFormat.Bmp);
                
                        seae.QRCode = Convert.ToBase64String(streamImg.ToArray());
                    }
                
                
                }
                else
                {
                    ViewBag.MensajeError = "No se encontró el documento";
                }
            
            
                return View(seae);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                Log.Error(e.StackTrace);
                ViewBag.MensajeError = "Se encontró un error inesperado";
            }
            
            return View();
        }

      
    }
}