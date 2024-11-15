using System;

namespace LAP.TUUA.ENTIDADES
{
    public class ModVentaComp
    {
        #region Fields

        private string sCodCompania;
        private string sCodModalidadVenta;
        private string sDscValorAcumulado;
        private string tip_Compania;
        private string dsc_Compania;
        private string dsc_Ruc;
        //obteniendo codigo IATA para Modulo ventas
        private string cod_IATA;

        private Int64 idTxCritica;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ModVentaComp class.
        /// </summary>
        public ModVentaComp()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ModVentaComp class.
        /// </summary>
        public ModVentaComp(string sCodCompania, string SCodModalidadVenta, string sDscValorAcumulado)
        {
            this.sCodCompania = sCodCompania;
            this.SCodModalidadVenta = SCodModalidadVenta;
            this.sDscValorAcumulado = sDscValorAcumulado;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodCompania value.
        /// </summary>
        public string SCodCompania
        {
            get { return sCodCompania; }
            set { sCodCompania = value; }
        }

        /// <summary>
        /// Gets or sets the SCodModalidadVenta value.
        /// </summary>
        public string SCodModalidadVenta
        {
            get { return sCodModalidadVenta; }
            set { sCodModalidadVenta = value; }
        }

        /// <summary>
        /// Gets or sets the sDscValorAcumulado value.
        /// </summary>
        public string SDscValorAcumulado
        {
            get { return sDscValorAcumulado; }
            set { sDscValorAcumulado = value; }
        }

        /// <summary>
        /// Gets or sets the tip_Compania value.
        /// </summary>
        public string Tip_Compania
        {
            get{ return tip_Compania;}
            set{ tip_Compania = value;}
        }

        /// <summary>
        /// Gets or sets the dsc_Compania value.
        /// </summary>
        public string Dsc_Compania
        {
            get{return dsc_Compania;}
            set{dsc_Compania = value;}
        }


        /// <summary>
        /// Gets or sets the dsc_Ruc value.
        /// </summary>
        public string Dsc_Ruc
        {
            get{return dsc_Ruc;}
            set{dsc_Ruc = value;}
        }

        public Int64 IdTxCritica
        {
            get { return idTxCritica; }
            set { idTxCritica = value; }
        }

        #endregion

       

    }
}