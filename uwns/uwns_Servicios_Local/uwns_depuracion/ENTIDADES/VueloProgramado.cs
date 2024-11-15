using System;

namespace LAP.TUUA.ENTIDADES
{
    public class VueloProgramado
    {
        #region Fields
        private string cod_Compania;
        private string num_Vuelo;
        private string fch_Vuelo;
        private string hor_Vuelo;
        private string dsc_Vuelo;
        private string tip_Vuelo;
        private string tip_Estado;
        private string dsc_Destino;
        private string log_Usuario_Mod;
        private string log_Fecha_Mod;
        private string log_Hora_Mod;

       



        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VueloProgramado class.
        /// </summary>
        public VueloProgramado()
        {
        }

        

        #endregion

        #region Properties
 

        /// <summary>
        /// Gets or sets the sNumVuelo value.
        /// </summary>
        public string Num_Vuelo
        {
            get { return num_Vuelo; }
            set { num_Vuelo = value; }
        }


        /// <summary>
        /// Gets or sets the sDscVuelo value.
        /// </summary>
        public string Dsc_Vuelo
        {
            get { return dsc_Vuelo; }
            set { dsc_Vuelo = value; }
        }

        /// <summary>
        /// Gets or sets the sHorVuelo value.
        /// </summary>
        public string Hor_Vuelo
        {
            get { return hor_Vuelo; }
            set { hor_Vuelo = value; }
        }




        /// <summary>
        /// Gets or sets the sTipVuelo value.
        /// </summary>
        public string Tip_Vuelo
        {
            get { return tip_Vuelo; }
            set { tip_Vuelo = value; }
        }

        /// <summary>
        /// Gets or sets the sTipEstado value.
        /// </summary>
        public string Tip_Estado
        {
            get { return tip_Estado; }
            set { tip_Estado = value; }
        }

        /// <summary>
        /// Gets or sets the sDscDestino value.
        /// </summary>
        public string Dsc_Destino
        {
            get { return dsc_Destino; }
            set { dsc_Destino = value; }
        }

        /// <summary>
        /// Gets or sets the sCodCompania value.
        /// </summary>
        public string Cod_Compania
        {
            get { return cod_Compania; }
            set { cod_Compania = value; }
        }


        public string Log_Usuario_Mod
        {
              get { return log_Usuario_Mod; }
              set { log_Usuario_Mod = value; }
        }


        public string Log_Fecha_Mod
        {
              get { return log_Fecha_Mod; }
              set { log_Fecha_Mod = value; }
        }


        public string Log_Hora_Mod
        {
              get { return log_Hora_Mod; }
              set { log_Hora_Mod = value; }
        }

        public string Fch_Vuelo
        {
              get { return fch_Vuelo; }
              set { fch_Vuelo = value; }
        }
        #endregion
    }
}
