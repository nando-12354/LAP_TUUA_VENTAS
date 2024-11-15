using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.AccesoMolinete;

namespace DemoAccesoMolinete
{
  class StartPoint
  {
    static void Main(string[] args)
    {
      //Molinete molinete = new Molinete();
      //Escenario escenario = new Escenario(molinete);
      //PLC plc = new PLC(escenario);
      //plc.Iniciar();
      //escenario.setPLC(plc);
      //Application.Run(escenario);
        OptionForm optionForm = new OptionForm();
        Application.Run(optionForm);
    }
  }
}
