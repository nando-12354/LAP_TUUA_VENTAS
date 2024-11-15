using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace LAP.TUUA.UTIL
{
    public class Sglobales
    {
        //Modalidad de Venta
        private ArrayList ListaControles;
        private ArrayList ListaRegistradosMV;
        private Hashtable ListaRegistradosTT;
        private ArrayList ListaEliminadosMV;
        private Hashtable ListaEliminadosTT;
        private ArrayList ListaSeleccionados;
        private Hashtable ListaActualesMV;
        private Hashtable ListaActualesTT;
        private string CodModalidadVenta;
        private string DscModalidadVenta;
        private string DscTipoModalidadVenta;
        private Int32 IndxTipoModalidad;
        private Int32 IndxEstadoModalidad;
        private string MjeError;
        private string TTSeleccionado;
        private bool FlagModal = false;
        private bool FlagPosPage = false;

        //Representante
        private ArrayList RListaControles;
        private Hashtable RSeleccionado;
        private Hashtable RRegistrados;

        //Compañía
        private ArrayList CListaControles;
        private Hashtable MVRegistrados;
        private Hashtable MVEliminados;
        private Hashtable ValoresActuales;
        private string txtCodCompania;
        private string txtNombre;
        private string txtIATA;
        private string txtOACI;
        private string txtRuc;
        private string txtCodigoEspecial;
        private string txtAerolinea;
        private string txtSAP;
        private Int32 IndxTipoCompania;
        private Int32 IndxEstadoCompania;
        private string MjeErrorRepres;
        private string sIndxTipoCompania;

        //Modalidad de Venta
        public ArrayList mListaControles
        {
            get { return ListaControles; }
            set { ListaControles = value; }
        }

        public ArrayList mListaRegistradosMV
        {
            get { return ListaRegistradosMV; }
            set { ListaRegistradosMV = value; }
        }

        public Hashtable mListaRegistradosTT
        {
            get { return ListaRegistradosTT; }
            set { ListaRegistradosTT = value; }
        }

        public ArrayList mListaEliminadosMV
        {
            get { return ListaEliminadosMV; }
            set { ListaEliminadosMV = value; }
        }

        public Hashtable mListaEliminadosTT
        {
            get { return ListaEliminadosTT; }
            set { ListaEliminadosTT = value; }
        }

        public ArrayList mListaSeleccionados
        {
            get { return ListaSeleccionados; }
            set { ListaSeleccionados = value; }
        }

        public Hashtable mListaActualesMV
        {
            get { return ListaActualesMV; }
            set { ListaActualesMV = value; }
        }

        public Hashtable mListaActualesTT
        {
            get { return ListaActualesTT; }
            set { ListaActualesTT = value; }
        }

        public string mCodModalidadVenta
        {
            get { return CodModalidadVenta; }
            set { CodModalidadVenta = value; }
        }

        public string mDscModalidadVenta
        {
            get { return DscModalidadVenta; }
            set { DscModalidadVenta = value; }
        }

        public string mDscTipoModalidadVenta
        {
            get { return DscTipoModalidadVenta; }
            set { DscTipoModalidadVenta = value; }
        }

        public Int32 mIndxTipoModalidad
        {
            get { return IndxTipoModalidad; }
            set { IndxTipoModalidad = value; }
        }

        public Int32 mIndxEstadoModalidad
        {
            get { return IndxEstadoModalidad; }
            set { IndxEstadoModalidad = value; }
        }

        public string mMjeError
        {
            get { return MjeError; }
            set { MjeError = value; }
        }
        
        public string mTTSeleccionado
        {
            get { return TTSeleccionado; }
            set { TTSeleccionado = value; }
        }
        
        public bool mFlagModal
        {
            get { return FlagModal; }
            set { FlagModal = value; }
        }
        
        public bool mFlagPosPage
        {
            get { return FlagPosPage; }
            set { FlagPosPage = value; }
        }

        //Representante
        public ArrayList mRListaControles
        {
            get { return RListaControles; }
            set { RListaControles = value; }
        }

        public Hashtable mRSeleccionado
        {
            get { return RSeleccionado; }
            set { RSeleccionado = value; }
        }

        public Hashtable mRRegistrados
        {
            get { return RRegistrados; }
            set { RRegistrados = value; }
        }


        //Compania
        public ArrayList mCListaControles
        {
            get { return CListaControles; }
            set { CListaControles = value; }
        }


        public Hashtable mMVRegistrados
        {
            get { return MVRegistrados; }
            set { MVRegistrados = value; }
        }


        public Hashtable mMVEliminados
        {
            get { return MVEliminados; }
            set { MVEliminados = value; }
        }


        public Hashtable mValoresActuales
        {
            get { return ValoresActuales; }
            set { ValoresActuales = value; }
        }


        public string mTxtCodCompania
        {
            get { return txtCodCompania; }
            set { txtCodCompania = value; }
        }

        public string mTxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; }
        }

        public string mTxtIATA
        {
            get { return txtIATA; }
            set { txtIATA = value; }
        }
        
        public string mTxtOACI
        {
            get { return txtOACI; }
            set { txtOACI = value; }
        }
        
        public string mTxtRuc
        {
            get { return txtRuc; }
            set { txtRuc = value; }
        }
        
        public string mTxtCodigoEspecial
        {
            get { return txtCodigoEspecial; }
            set { txtCodigoEspecial = value; }
        }
        
        public string mTxtAerolinea
        {
            get { return txtAerolinea; }
            set { txtAerolinea = value; }
        }
        
        public string mTxtSAP
        {
            get { return txtSAP; }
            set { txtSAP = value; }
        }
        
        public Int32 mIndxTipoCompania
        {
            get { return IndxTipoCompania; }
            set { IndxTipoCompania = value; }
        }
        
        public Int32 mIndxEstadoCompania
        {
            get { return IndxEstadoCompania; }
            set { IndxEstadoCompania = value; }
        }
        
        public string mMjeErrorRepres
        {
            get { return MjeErrorRepres; }
            set { MjeErrorRepres = value; }
        }

        public string SIndxTipoCompania
        {
            get { return sIndxTipoCompania; }
            set { sIndxTipoCompania = value; }
        }

    }
}
