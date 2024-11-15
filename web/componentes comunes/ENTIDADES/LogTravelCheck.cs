using System;

namespace LAP.TUUA.ENTIDADES
{
    public class LogTravelCheck
    {
        #region Fields

        private string sNumOperacion;
        private string sNumCheque;
        private double dImpChequeDolares;
        private double dImpChequeSoles;
        private string sCodBanco;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LogTravelCheck class.
        /// </summary>
        public LogTravelCheck()
        {
        }

        /// <summary>
        /// Initializes a new instance of the LogTravelCheck class.
        /// </summary>
        public LogTravelCheck(string sNumOperacion, string sNumCheque, double dImpChequeDolares, double dImpChequeSoles, string sCodBanco)
        {
            this.sNumOperacion = sNumOperacion;
            this.sNumCheque = sNumCheque;
            this.dImpChequeDolares = dImpChequeDolares;
            this.dImpChequeSoles = dImpChequeSoles;
            this.sCodBanco = sCodBanco;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sNumOperacion value.
        /// </summary>
        public string SNumOperacion
        {
            get { return sNumOperacion; }
            set { sNumOperacion = value; }
        }

        /// <summary>
        /// Gets or sets the sNumCheque value.
        /// </summary>
        public string SNumCheque
        {
            get { return sNumCheque; }
            set { sNumCheque = value; }
        }

        /// <summary>
        /// Gets or sets the dImpChequeDolares value.
        /// </summary>
        public double DImpChequeDolares
        {
            get { return dImpChequeDolares; }
            set { dImpChequeDolares = value; }
        }

        /// <summary>
        /// Gets or sets the dImpChequeSoles value.
        /// </summary>
        public double DImpChequeSoles
        {
            get { return dImpChequeSoles; }
            set { dImpChequeSoles = value; }
        }

        /// <summary>
        /// Gets or sets the sCodBanco value.
        /// </summary>
        public string SCodBanco
        {
            get { return sCodBanco; }
            set { sCodBanco = value; }
        }

        #endregion
    }
}
