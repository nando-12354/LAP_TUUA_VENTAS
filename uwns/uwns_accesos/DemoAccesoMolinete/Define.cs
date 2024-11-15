using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoAccesoMolinete
{
  public class Define
  {
    public const string XML_CONFIG = "resources\\PLC.config";
    public const string XML_CONFIG_DISCAPACITADOS = "resources\\PLCDiscapacitados.config";
    public const string KEY_PARAMS = "Params";
    public const string KEY_WRITER = "Writer";
    public const string KEY_READER = "Reader";

    public const string OPEN_MOLINETE = "OPENMOL";
    public const string SEMAFORO_ROJO = "SEMROJO";
    public const string SEMAFORO_VERDE = "SEMVERDE";
    public const string SEMAFORO_AMBAR = "SEMAMBAR";
    public const string CLOSE_MOLINETE = "CLOSEMOL";
    public const string GIRO_MOLINETE = "GIROMOL";
    public const string OUTPUTSIGN_STATUS_MOLINETE = "OUTPUTSIGNSTATUSMOL";

    public const string TEXTOCLOSED = "CLOSED";
    public const string TEXTOOPENED = "OPENED";
  }
}
