using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    internal class VehiculosRecepcionesViewModel
    {
        public int VehiculoRecepcionId { get; set; }
        public int RecepcionId { get; set; }
        public int VehiculoId { get; set; }
        public string Chofer { get; set; }
        public string Placas { get; set; }
        public string Color { get; set; }
        public string MarcaModelo { get; set; }
        public string Estado { get; set; }

        public List<CondicionesVehiculoViewModel> CondicionesVehiculoInfo {  get; set; }

        public List<CondicionesVehiculoViewModel> CondicionesVehiculoActivas { get; set; }
    }
}
