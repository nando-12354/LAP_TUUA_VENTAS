using System;
namespace LAP.TUUA.ENTIDADES
{
    public class BoardingBcbp
    {
        #region Fields 

        private int iNumSecuencial;
        private string sCodCompania;
        private string sNumVuelo;
        private string strFchVuelo;
        private string strNumAsiento;
        private string strNomPasajero;
        private string strTrama;
        private string strLogUsuarioMod;
        private string strLogFechaMod;
        private string strLogHoraMod;
        private string strTipIngreso;
        private string strTip_Estado;
        private string strCod_Equipo_Mod;
        private string strDsc_Boarding_Estado;
        private string strMotivo;
        private string strCodUnicoBcbp;

        private int iNum_Rehabilitaciones;
        private DateTime strFch_Vencimiento;
        private string strCodUnicoBcbpRel;

        private string strCodModulo;
        private string strCodSubModulo;

        private string strRol;
        private string strFlg_Sincroniza;
        private string dsc_Bcbp_Estado;
        private string tip_Pasajero;
        private string tip_Vuelo;
        private string tip_Transferencia;

        //Req. bcbp relacionado
        private string flg_Tipo;
        private int num_Secuencial_Bcbp_Rel;
        private int num_Secuenial_Rel_Sec;
        private string num_Vuelo_Rel;

        //nuevos campos proy:7296
        private string dsc_Destino;
        private string cod_Eticket;
        private string nro_boarding;

        //campos respuesta web service
        private string flg_Transito;
        private string flg_Pago;
        private bool flg_Val_WS;
        private bool flg_Val_Transito;
        private bool flg_Val_Tuua;
        private string flg_Bloqueado;
        private string dsc_Observacion;
        private string flg_WSError;

        //campos para archivo SEAE
        private string num_Airline_Code;
        private string num_Document_Form;

        //campos para mantener unicidad para hominimos infate-padre
        private string num_Checkin;

        public string Num_Checkin
        {
            get { return num_Checkin; }
            set { num_Checkin = value; }
        }

        public string StrFlg_Sincroniza
        {
              get { return strFlg_Sincroniza; }
              set { strFlg_Sincroniza = value; }
        }
        public string StrRol
        {
              get { return strRol; }
              set { strRol = value; }
        }

        public string StrCodSubModulo
        {
              get { return strCodSubModulo; }
              set { strCodSubModulo = value; }
        }
        public string StrCodModulo
        {
              get { return strCodModulo; }
              set { strCodModulo = value; }
        }

        public string StrCodUnicoBcbpRel
        {
              get { return strCodUnicoBcbpRel; }
              set { strCodUnicoBcbpRel = value; }
        }

        public DateTime StrFch_Vencimiento
        {
              get { return strFch_Vencimiento; }
              set { strFch_Vencimiento = value; }
        }
  
        private DateTime dtFchVuelo; 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BoardingBcbp class.
        /// </summary>
        public BoardingBcbp()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BoardingBcbp class.
        /// </summary>
        public BoardingBcbp(int iNumSecuencial, string sCodCompania, string sNumVuelo, string strFchVuelo, string strNumAsiento,
         string strNomPasajero,string strTrama, string strLogUsuarioMod, string strLogFechaMod,string strLogHoraMod,string strTipIngreso,string strTip_Estado,
         string strCod_Equipo_Mod, string strDsc_Boarding_Estado, string strMotivo)
        {
            this.iNumSecuencial = iNumSecuencial;
            this.sCodCompania = sCodCompania;
            this.sNumVuelo = sNumVuelo;
            this.strFchVuelo = strFchVuelo;
            this.strNumAsiento = strNumAsiento;
            this.strNomPasajero = strNomPasajero;
            this.strTrama = strTrama;
            this.strLogUsuarioMod = strLogUsuarioMod;
            this.strLogFechaMod = strLogFechaMod;
            this.strLogHoraMod = strLogHoraMod;
            this.strTipIngreso = strTipIngreso;
            this.strTip_Estado = strTip_Estado;
            this.strCod_Equipo_Mod = strCod_Equipo_Mod;
            this.strDsc_Boarding_Estado = strDsc_Boarding_Estado;
            this.strMotivo = strMotivo;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the strCod_Equipo_Mod value.
        /// </summary>
        public int INum_Rehabilitaciones
        {
              get { return iNum_Rehabilitaciones; }
              set { iNum_Rehabilitaciones = value; }
        }

        public string StrCodUnicoBcbp
        {
              get { return strCodUnicoBcbp; }
              set { strCodUnicoBcbp = value; }
        }
        public string StrCod_Equipo_Mod
        {
              get { return strCod_Equipo_Mod; }
              set { strCod_Equipo_Mod = value; }
        }

        /// <summary>
        /// Gets or sets the strDsc_Boarding_Estado value.
        /// </summary>
        public string StrDsc_Boarding_Estado
        {
              get { return strDsc_Boarding_Estado; }
              set { strDsc_Boarding_Estado = value; }
        }

        /// <summary>
        /// Gets or sets the strTip_Estado value.
        /// </summary>
        public string StrTip_Estado
        {
              get { return strTip_Estado; }
              set { strTip_Estado = value; }
        }  
        /// <summary>
        /// Gets or sets the iNumSecuencial value.
        /// </summary>
        public int INumSecuencial
        {
            get { return iNumSecuencial; }
            set { iNumSecuencial = value; }
        }

        /// <summary>
        /// Gets or sets the sCodCompania value.
        /// </summary>
        public string SCodCompania
        {
            get { return sCodCompania; }
            set { sCodCompania = value; }
        }
        
        public DateTime DtFchVuelo
        {
            get { return dtFchVuelo; }
            set { dtFchVuelo = value; }
        }
        /// <summary>
        /// Gets or sets the sNumVuelo value.
        /// </summary>
        public string SNumVuelo
        {
            get { return sNumVuelo; }
            set { sNumVuelo = value; }
        }

        /// <summary>
        /// Gets or sets the strFchVuelo value.
        /// </summary>
        public string StrFchVuelo
        {
            get{return strFchVuelo;}
            set{strFchVuelo = value;}
        }

        /// <summary>
        /// Gets or sets the strNumAsiento value.
        /// </summary>
        public string StrNumAsiento
        {
            get{return strNumAsiento;}
            set{strNumAsiento = value;}
        }

        /// <summary>
        /// Gets or sets the strNomPasajero value.
        /// </summary>
        public string StrNomPasajero
        {
            get{return strNomPasajero;}
            set{strNomPasajero = value;}
        }

        /// <summary>
        /// Gets or sets the strTrama value.
        /// </summary>
        public string StrTrama
        {
            get{return strTrama;}
            set{strTrama = value;}
        }

        /// <summary>
        /// Gets or sets the strLogUsuarioMod value.
        /// </summary>
        public string StrLogUsuarioMod
        {
            get{return strLogUsuarioMod;}
            set{strLogUsuarioMod = value;}
        }

        /// <summary>
        /// Gets or sets the strLogFechaMod value.
        /// </summary>
        public string StrLogFechaMod
        {
            get{return strLogFechaMod;}
            set{strLogFechaMod = value;}
        }

        /// <summary>
        /// Gets or sets the strLogHoraMod value.
        /// </summary>
        public string StrLogHoraMod
        {
            get{return strLogHoraMod;}
            set{strLogHoraMod = value;}
        }

        /// <summary>
        /// Gets or sets the strTipIngreso value.
        /// </summary>
        public string StrTipIngreso
        {
            get{return strTipIngreso;}
            set{strTipIngreso = value;}
        }

        /// <summary>
        /// Gets or sets the strMotivo value.
        /// </summary>
        public string StrMotivo
        {
            get { return strMotivo; }
            set { strMotivo = value; }
        }

        #endregion



        public string Dsc_Bcbp_Estado
        {
            get
            {
                return dsc_Bcbp_Estado;
            }
            set
            {
                dsc_Bcbp_Estado = value;
            }
        }

        public string Tip_Pasajero
        {
            get
            {
                return tip_Pasajero;
            }
            set
            {
                tip_Pasajero = value;
            }
        }

        public string Tip_Vuelo
        {
            get
            {
                return tip_Vuelo;
            }
            set
            {
                tip_Vuelo = value;
            }
        }

        public string Flg_Tipo
        {
            get
            {
                return flg_Tipo;
            }
            set
            {
                flg_Tipo = value;
            }
        }

        public int Num_Secuencial_Bcbp_Rel
        {
            get
            {
                return num_Secuencial_Bcbp_Rel;
            }
            set
            {
                num_Secuencial_Bcbp_Rel = value;
            }
        }

        public int Num_Secuenial_Rel_Sec
        {
            get
            {
                return num_Secuenial_Rel_Sec;
            }
            set
            {
                num_Secuenial_Rel_Sec = value;
            }
        }

        public string Num_Vuelo_Rel
        {
            get
            {
                return num_Vuelo_Rel;
            }
            set
            {
                num_Vuelo_Rel = value;
            }
        }

        public string Dsc_Destino
        {
            get
            {
                return dsc_Destino;
            }
            set
            {
                dsc_Destino = value;
            }
        }

        public string Cod_Eticket
        {
            get
            {
                return cod_Eticket;
            }
            set
            {
                cod_Eticket = value;
            }
        }

        public string Nro_boarding
        {
            get
            {
                return nro_boarding;
            }
            set
            {
                nro_boarding = value;
            }
        }

        public string Flg_Transito
        {
            get
            {
                return flg_Transito;
            }
            set
            {
                flg_Transito = value;
            }
        }

        public string Flg_Pago
        {
            get
            {
                return flg_Pago;
            }
            set
            {
                flg_Pago = value;
            }
        }

        public bool Flg_Val_WS
        {
            get
            {
                return flg_Val_WS;
            }
            set
            {
                flg_Val_WS = value;
            }
        }

        public bool Flg_Val_Transito
        {
            get
            {
                return flg_Val_Transito;
            }
            set
            {
                flg_Val_Transito = value;
            }
        }

        public bool Flg_Val_Tuua
        {
            get
            {
                return flg_Val_Tuua;
            }
            set
            {
                flg_Val_Tuua = value;
            }
        }

        public string Tip_Transferencia
        {
            get
            {
                return tip_Transferencia;
            }
            set
            {
                tip_Transferencia = value;
            }
        }

        public string Flg_Bloqueado
        {
            get
            {
                return flg_Bloqueado;
            }
            set
            {
                flg_Bloqueado = value;
            }
        }

        public string Dsc_Observacion
        {
            get
            {
                return dsc_Observacion;
            }
            set
            {
                dsc_Observacion = value;
            }
        }

        public string Flg_WSError
        {
            get
            {
                return flg_WSError;
            }
            set
            {
                flg_WSError = value;
            }
        }

        public string Num_Airline_Code
        {
            get
            {
                return num_Airline_Code;
            }
            set
            {
                num_Airline_Code = value;
            }
        }

        public string Num_Document_Form
        {
            get
            {
                return num_Document_Form;
            }
            set
            {
                num_Document_Form = value;
            }
        }

    }
}
