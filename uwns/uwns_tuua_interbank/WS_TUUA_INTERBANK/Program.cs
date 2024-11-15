using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaWSInterbank.Dao;
using PruebaWSInterbank.Entidades;
using PruebaWSInterbank.SvcTipoCambio;
using Serilog;

namespace WS_TUUA_INTERBANK
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\ws_tipocambio_interbank.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Iniciando servicio de actualización de Tipo de Cambio");
                wsTipoCambioLAPSoapClient service = new wsTipoCambioLAPSoapClient();

                double tc = service.fCargaTipoDeCambio("C");
                double tcv = service.fCargaTipoDeCambio("V");
                Log.Information($"Tipo de Cambio Compra: {tc}, Tipo de Cambio Venta: {tcv}");

                //registrar tasas de cambio
                //solo puede existir una tasa de cambio de compra y de venta activa
                // se debe modificar el tipo de cambio siempre que el importe sea distinto

                TUATasaCambioDao dao = new TUATasaCambioDao();
                List<TUATasaCambio> lsTasaCambio = await dao.GetAll();

                var tasaCambioCompra = lsTasaCambio.Where(t => t.Cod_Moneda == "DOL" && t.Tip_Cambio == "C").FirstOrDefault();
                var tasaCambioVenta = lsTasaCambio.Where(t => t.Cod_Moneda == "DOL" && t.Tip_Cambio == "V").FirstOrDefault();
                await RegistrarCompra(tc, dao, tasaCambioCompra);
                await RegistrarVenta(tcv, dao, tasaCambioVenta);

                Log.Information("Se ha actualizado el tipo de cambio satisfactoriamente");

               

            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                Log.Error(ex.StackTrace);
            }

        }

        private static async Task RegistrarCompra(double tc, TUATasaCambioDao dao, TUATasaCambio tasaCambioCompra)
        {
            bool flgInsertarCompra = true;
            //verificar si el importe ha variado
            if (tasaCambioCompra != null && tasaCambioCompra.Imp_Cambio_Actual == (decimal)tc)
            {
                //insertar nuevo cambio de compra y eliminar el anterior y guardar en historial
                flgInsertarCompra = false;
                Log.Information("El tipo de cambio de compra no ha variado");
            }

            if (flgInsertarCompra)
            {

                var tcCompra = new TUATasaCambio();
                tcCompra.Cod_Tasa_Cambio = "";
                tcCompra.Cod_Moneda = "DOL";
                tcCompra.Fch_Proceso = DateTime.Now;
                tcCompra.Fch_Programacion = DateTime.Today;
                tcCompra.Imp_Cambio_Actual = (decimal)tc;
                tcCompra.Log_Fecha_Mod = DateTime.Now.ToString("yyyyMMdd");
                tcCompra.Log_Hora_Mod = DateTime.Now.ToString("HHmmss");
                tcCompra.Log_Usuario_Mod = "USR_SVC";
                tcCompra.Tip_Ingreso = "1";
                tcCompra.Tip_Estado = "1";
                tcCompra.Tip_Cambio = "C";
                await dao.Add(tcCompra, tasaCambioCompra);
               
            }

        }

        private static async Task RegistrarVenta(double tv, TUATasaCambioDao dao, TUATasaCambio tasaCambioVenta)
        {
            bool flgInsertarVenta = true;
            //verificar si el importe ha variado
            if (tasaCambioVenta != null && tasaCambioVenta.Imp_Cambio_Actual == (decimal)tv)
            {
                //insertar nuevo cambio de venta y eliminar el anterior y guardar en historial
                flgInsertarVenta = false;
                Log.Information("El tipo de cambio de venta no ha variado");
            }

            if (flgInsertarVenta)
            {

                var tcVenta = new TUATasaCambio();
                tcVenta.Cod_Tasa_Cambio = "";
                tcVenta.Cod_Moneda = "DOL";
                tcVenta.Fch_Proceso = DateTime.Now;
                tcVenta.Fch_Programacion = DateTime.Today;
                tcVenta.Imp_Cambio_Actual = (decimal)tv;
                tcVenta.Log_Fecha_Mod = DateTime.Now.ToString("yyyyMMdd");
                tcVenta.Log_Hora_Mod = DateTime.Now.ToString("HHmmss");
                tcVenta.Log_Usuario_Mod = "USR_SVC";
                tcVenta.Tip_Ingreso = "1";
                tcVenta.Tip_Estado = "1";
                tcVenta.Tip_Cambio = "V";
                await dao.Add(tcVenta, tasaCambioVenta);
               
            }

        }
    }
}
