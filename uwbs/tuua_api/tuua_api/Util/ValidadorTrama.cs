using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tuua_api.Entidades;

namespace tuua_api.Util
{
    public class ValidadorTrama
    {
        public BCBP convertirTrama(string trama) {
            BCBP boarding = new BCBP();

            if (trama.Length < 60) {
                throw new Exception("Trama Ilegible");
            }
            try
            {
                boarding.Indicator = trama.Substring(0, 1);
                boarding.SegmentsIndicator = trama.Substring(1, 1);
                boarding.PaxName = trama.Substring(2, 20).Trim();
                boarding.ElectronicIndicator = trama.Substring(22, 1);
                boarding.PNR = trama.Substring(23, 7).Trim();
                boarding.OriginCode = trama.Substring(30, 3).Trim();
                boarding.DestinationCode = trama.Substring(33, 3).Trim();
                boarding.AirLineCode = trama.Substring(36, 3).Trim();
                boarding.FlightNumber = trama.Substring(39, 5).Trim();
                boarding.FlightDate = trama.Substring(44, 3).Trim();
                boarding.CompartmentCode = trama.Substring(47, 1);
                boarding.SeatNumber = trama.Substring(48, 4).Trim();
                boarding.CheckinSequence = trama.Substring(52, 5).Trim();
                boarding.PassengerStatus = trama.Substring(57, 1);
                boarding.BytesIndicator = trama.Substring(58, 2);
            }
            catch (Exception)
            {

                throw new Exception("Trama Ilegible");
            }
            
            
            return boarding;
        }

    }
}