using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.VENTAS
{
    public partial class Cuadre : Form
    {
        protected Usuario objUsuario;
        protected Turno objTurno;
        protected BO_Turno objBOTurno;
        protected BO_Error objBOError;
        protected bool boError;
        protected Principal formMyParent;
        protected List<TurnoMonto> lobjListaMontos;

        public Cuadre(Usuario objUsuario, Turno objTurno, Principal formMyParent)
        {
            InitializeComponent();

            boError = false;
            objBOTurno = new BO_Turno();
            this.dgvCuadre.DataSource = objBOTurno.ListarMonedas();
            this.objTurno = objTurno;
            this.objUsuario = objUsuario;
            this.formMyParent = formMyParent;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            formMyParent.VerificarTurnoActivo();
            if (!valido())
            {
                return;
            }
            if (objUsuario == null)
            {
                return;
            }
            try
            {
                objBOError = new BO_Error();
                
                if (objBOTurno.VerificarCuadre(lobjListaMontos, objUsuario.SCodUsuario, this.objTurno.SCodTurno) == 0)
                {
                    MessageBox.Show("Cuadre correcto en el turno [" + this.objTurno.SCodTurno + "].", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(objBOTurno.SErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                objBOError.IsError();
                boError = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
            }
            finally
            {
                if (boError)
                {
                    MessageBox.Show(ErrorHandler.Desc_Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool valido()
        {
            lobjListaMontos = new List<TurnoMonto>();
            for (int i = 0; i < dgvCuadre.Rows.Count; i++)
            {
                try
                {
                    TurnoMonto objMonMoneda = new TurnoMonto();
                    objMonMoneda.SCodMoneda = (string)dgvCuadre.Rows[i].Cells[0].Value;
                    objMonMoneda.DImpMontoFinal = Decimal.Parse((string)dgvCuadre.Rows[i].Cells[2].Value);
                    objMonMoneda.Imp_Transferencia = Decimal.Parse((string)dgvCuadre.Rows[i].Cells[3].Value);
                    objMonMoneda.Imp_Cheque = Decimal.Parse((string)dgvCuadre.Rows[i].Cells[4].Value);
                    objMonMoneda.Imp_Tarjeta = Decimal.Parse((string)dgvCuadre.Rows[i].Cells[5].Value);
                    if (objMonMoneda.DImpMontoFinal < 0)
                    {
                        erpMontos.SetError(dgvCuadre, (string)LabelConfig.htLabels["cierre.erpMontos"]);
                        return false;
                    }
                    lobjListaMontos.Add(objMonMoneda);
                }
                catch (Exception ex)
                {
                    erpMontos.SetError(dgvCuadre, "Ingrese montos válidos");
                    return false;
                }
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cuadre_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
        }
    }
}