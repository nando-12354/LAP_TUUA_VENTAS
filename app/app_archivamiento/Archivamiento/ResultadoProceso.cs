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
using LAP.TUUA.UTIL;

namespace LAP.TUUA.ARCHIVAMIENTO
{
    public partial class ResultadoProceso : Form
    {
        private BO_Configuracion objBOConfiguracion;


        public ResultadoProceso(bool res, String valor, BO_Consultas objBOConsultas, BO_Configuracion objBOConfiguracion)
        {
            InitializeComponent();
            if (res)
            {
                lblTitulo.Text = "PROCESO DE ARCHIVAMIENTO EXITOSO";
            }
            else
            {
                lblTitulo.Text = "PROCESO DE ARCHIVAMIENTO FALLIDO";
            }

            this.objBOConfiguracion = objBOConfiguracion;

            String Tip_Estado = "";
            String Cod_Etapa = "";
            ArrayList arrDetalle = null;
            String xmlStringDocument = null;

            DataTable dtReturn = objBOConsultas.ObtenerDetalleArchivamiento(valor);
            if (dtReturn.Rows.Count > 0)
            {
                String cultureName = (String)Property.htProperty[Define.CULTURENAME_FOR_MONTHSABREVIATED];
                String rangoFechaInicial = dtReturn.Rows[0]["Fch_Ini"].ToString();
                String rangoFechaFinal = dtReturn.Rows[0]["Fch_Fin"].ToString();

                lblFechaInicial.Text = rangoFechaInicial.Substring(6, 2) + " " + Function.GetAbbreviatedMonthName(cultureName, Int32.Parse(rangoFechaInicial.Substring(4, 2))) + " " + rangoFechaInicial.Substring(0, 4);
                lblFechaFinal.Text = rangoFechaFinal.Substring(6, 2) + " " + Function.GetAbbreviatedMonthName(cultureName, Int32.Parse(rangoFechaFinal.Substring(4, 2))) + " " + rangoFechaFinal.Substring(0, 4);

                lblPeriodo.Text = dtReturn.Rows[0]["Dsc_Periodo"].ToString();

                String fecha = dtReturn.Rows[0]["Log_Fecha_Mod"].ToString();
                String hora = dtReturn.Rows[0]["Log_Hora_Mod"].ToString();

                lblFechaProceso.Text = fecha.Substring(6, 2) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(0, 4) + " " + hora.Substring(0, 2) + ":" + hora.Substring(2, 2);

                xmlStringDocument = dtReturn.Rows[0]["Xml_Rango"].ToString();
                Tip_Estado = dtReturn.Rows[0]["Tip_Estado"].ToString();
                Cod_Etapa = dtReturn.Rows[0]["Cod_Etapa"].ToString();
                arrDetalle = objBOConsultas.ObtenerXmlRangoArchivamiento(xmlStringDocument);
                
            }

            constructPasosRealizados(Tip_Estado, Cod_Etapa);

            if (xmlStringDocument != null && !xmlStringDocument.Equals(String.Empty) && Cod_Etapa.CompareTo("02")>0)
            {
                TreeNode mainNode = new TreeNode();
                mainNode.Name = "mainNode";
                mainNode.Text = ".....";
                this.treeViewRangos.Nodes.Add(mainNode);

                TreeNode treeNode = treeViewRangos.Nodes["mainNode"];

                constructTreeView(arrDetalle, treeNode);
                treeViewRangos.ExpandAll();                
            }
            else
            {
                btnCerrar.Location = new Point(205, 445);
                this.Size = new Size(508, 507);
                grbRangos.Visible = false;
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
            if (Tip_Estado.Equals("1"))
            {
                for (int i = 0; i < dtCodigoEtapa.Rows.Count; i++)
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
                    lblNroPaso.Location = new Point(50, 30 + i * 40);
                    lblNroPaso.AutoSize = true;
                    lblDescPaso.Location = new Point(150, 30 + i * 40);
                    lblDescPaso.AutoSize = true;
                    lblStatusPaso.Location = new Point(350, 30 + i * 40);
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

                    lblNroPaso.Location = new Point(50, 30 + i * 40);
                    lblNroPaso.AutoSize = true;
                    lblDescPaso.Location = new Point(150, 30 + i * 40);
                    lblDescPaso.AutoSize = true;
                    lblStatusPaso.Location = new Point(350, 30 + i * 40);
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
                    campoValor = (CampoValor)arrayList[i];
                    if (campoValor.Valor is String)
                    {
                        TreeNode treeNodeAux = treeNode.Nodes.Add(campoValor.Campo.ToUpper());
                        treeNodeAux = treeNodeAux.Nodes.Add(campoValor.Valor.ToString());
                        treeNodeAux.ForeColor = Color.Blue;
                    }
                    else if (campoValor.Valor is ArrayList)
                    {
                        ArrayList arrayListAux = (ArrayList)campoValor.Valor;
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
