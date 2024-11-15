using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class BCBP
    {
        public string Indicator { get; set; }
        public string SegmentsIndicator { get; set; }
        public string PaxName { get; set; }
        public string ElectronicIndicator { get; set; }
        public string PNR { get; set; }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public string AirLineCode { get; set; }
        public string FlightNumber { get; set; }
        public string FlightDate { get; set; } 
        public string CompartmentCode { get; set; }
        public string SeatNumber { get; set; }
        public string CheckinSequence { get; set; }
        public string PassengerStatus { get; set; }
        public string BytesIndicator { get; set; }

    }
}