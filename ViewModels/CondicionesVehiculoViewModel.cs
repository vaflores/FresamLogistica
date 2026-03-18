using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    internal class CondicionesVehiculoViewModel
    {
        public int CondicionVehiculoRecepcionId { get; set; }
        public int VehiculoRecepcionId { get; set; }
        public int CondicionId { get; set; }
        public string Nombre { get; set; }
        public bool Valor {  get; set; }

    }
}
