using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.CONTROL;

namespace LAP.TUUA.ARCHIVAMIENTO
{
    public partial class DetalleArchivamiento : Form
    {

        private BO_Configuracion objBOConfiguracion;


        public DetalleArchivamiento(String valor, BO_Consultas objBOConsultas, BO_Configuracion objBOConfiguracion)
        {
            InitializeComponent();
            this.objBOConfiguracion = objBOConfiguracion;

            String Tip_Estado = "";
            String Cod_Etapa = "";
            ArrayList arrDetalle = null;
            String xmlStringDocument = null;

            DataTable dtReturn = objBOConsultas.ObtenerDetalleArchivamiento(valor);
            if (dtReturn.Rows.Count > 0)
            {
                xmlStringDocument = dtReturn.Rows[0]["Xml_Rango"].ToString();
                Tip_Estado = dtReturn.Rows[0]["Tip_Estado"].ToString();
                Cod_Etapa = dtReturn.Rows[0]["Cod_Etapa"].ToString();
                arrDetalle = objBOConsultas.ObtenerXmlRangoArchivamiento(xmlStringDocument);
            }

            constructPasosRealizados(Tip_Estado, Cod_Etapa);

            if (xmlStringDocument != null && !xmlStringDocument.Equals(String.Empty) && Cod_Etapa.CompareTo("02") > 0)
            {
                TreeNode mainNode = new TreeNode();
                mainNode.Name = "mainNode";
                mainNode.Text = ".....";
                this.treeViewRangos.Nodes.Add(mainNode);

                TreeNode treeNode = treeViewRangos.Nodes["mainNode"];

                constructTreeView(arrDetalle, treeNode);
                treeViewRangos.ExpandAll();                
            }
            
        }

        private void constructPasosRealizados(String Tip_Estado, String Cod_Etapa)
        {
            DataTable dtCodigoEtapa = objBOConfiguracion.ObtenerCodEtapaArchivamiento();
            Label lblNroPaso;
            Label lblDescPaso;
            Label lblStatusPaso;
            String OK = "OK";
            String ERROR = "ERROR";
            if(Tip_Estado.Equals("1"))
            {
                for(int i=0; i < dtCodigoEtapa.Rows.Count; i++)
                {
                    String cod_Campo = dtCodigoEtapa.Rows[i]["Cod_Campo"].ToString();
                    String dsc_Campo = dtCodigoEtapa.Rows[i]["Dsc_Campo"].ToString();
                    
                    lblNroPaso = new Label();
                    lblNroPaso.Text = "Paso" + Int32.Parse(cod_Campo);
                    lblDescPaso = new Label();
                    lblDescPaso.Text = dsc_Campo;
                    lblStatusPaso = new Label();
                    lblStatusPaso.Text = OK;
                    lblStatusPaso.ForeColor = Color.Blue;
                    lblNroPaso.Location = new Point(50, 50 + i * 40);
                    lblNroPaso.AutoSize = true;
                    lblDescPaso.Location = new Point(150, 50 + i * 40);
                    lblDescPaso.AutoSize = true;
                    lblStatusPaso.Location = new Point(350, 50 + i * 40);
                    lblStatusPaso.AutoSize = true;
                    
                    grbPasosRealizados.Controls.Add(lblNroPaso);
                    grbPasosRealizados.Controls.Add(lblDescPaso);
                    grbPasosRealizados.Controls.Add(lblStatusPaso);
                        
                }
            }
            else
            {
                bool noFinalizado = false;
                for (int i = 0; i < dtCodigoEtapa.Rows.Count; i++)
                {
                    String cod_Campo = dtCodigoEtapa.Rows[i]["Cod_Campo"].ToString();
                    String dsc_Campo = dtCodigoEtapa.Rows[i]["Dsc_Campo"].ToString();

                    lblNroPaso = new Label();
                    lblNroPaso.Text = "Paso" + Int32.Parse(cod_Campo);
                    lblDescPaso = new Label();
                    lblDescPaso.Text = dsc_Campo;
                    lblStatusPaso = new Label();

                    lblNroPaso.Location = new Point(50, 50 + i * 40);
                    lblNroPaso.AutoSize = true;
                    lblDescPaso.Location = new Point(150, 50 + i * 40);
                    lblDescPaso.AutoSize = true;
                    lblStatusPaso.Location = new Point(350, 50 + i * 40);
                    lblStatusPaso.AutoSize = true;

                    if (Cod_Etapa.Equals(cod_Campo) || noFinalizado)
                    {
                        noFinalizado = true;
                        lblStatusPaso.Text = ERROR;
                        lblStatusPaso.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblStatusPaso.Text = OK;
                        lblStatusPaso.ForeColor = Color.Blue;                        
                    }

                    grbPasosRealizados.Controls.Add(lblNroPaso);
                    grbPasosRealizados.Controls.Add(lblDescPaso);
                    grbPasosRealizados.Controls.Add(lblStatusPaso);

                }                
            }

        }

        private void constructTreeView(ArrayList arrayList, TreeNode treeNode)
        {
            CampoValor campoValor;
            if (arrayList != null)
            {
                for (int i = 0; i < arrayList.Count; i++)
                {
                    campoValor = (CampoValor) arrayList[i];
                    if (campoValor.Valor is String)
                    {
                        TreeNode treeNodeAux = treeNode.Nodes.Add(campoValor.Campo.ToUpper());
                        treeNodeAux = treeNodeAux.Nodes.Add(campoValor.Valor.ToString());
                        treeNodeAux.ForeColor = Color.Blue;
                    }
                    else if (campoValor.Valor is ArrayList)
                    {
                        ArrayList arrayListAux = (ArrayList) campoValor.Valor;
                        TreeNode treeNodeAux = treeNode.Nodes.Add(campoValor.Campo.ToUpper());
                        constructTreeView(arrayListAux, treeNodeAux);
                    }
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
