using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace LAP.TUUA.AUDITORIA_CLR
{
      public class AUDIT_ColumnaXML
      {
            public  string Nom_Columna;              //nombre de la columna
            public  string Nom_Alias;               //alias
            public  string Dsc_Tipo;                //tipo de columna (TEXT, BOOLEAN, OPTION,NUM)
            public  ArrayList Lst_Valores;     //conj de valores de la columna (OPTION)
            public  ArrayList Lst_Descrip;     //conj de descripciones de la col (OPTION)
            public  bool      Flag_Relacion;           //indica si la tabla presenta relacion
            public  string Nom_Tabla;             //nombre de la tabla con la que se relaciona
            public  string Rel_CampoId;           //campo de la tabla relacionada que coinicde con el actual
            public  string Rel_CampoDes;          //campo de la tabla relacionada que describe al actual
            public  string Rel_FlagMaestra;          //si presenta tabla maestra: 'TRUE' || 'FALSE' 
            public string  Ord_ID;                  //Representa el identificador de columna
            public string  Key_Flag;
            public string Audit_Flag;

            public AUDIT_ColumnaXML() 
            { 

            }
      }
}
