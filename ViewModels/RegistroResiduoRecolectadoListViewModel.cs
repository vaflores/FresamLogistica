using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    internal class RegistroResiduoRecolectadoListViewModel
    {
        public int RegistroResiduoId { get; set; }

        // Datos principales
        public DateTime Fecha { get; set; }

        public string EmpresaResiduo { get; set; }
        public string Operador { get; set; }
        public string Destino { get; set; }
        public int? FolioId { get; set; }
        public string PlacasVehiculo { get; set; }

        public decimal CantidadTotal { get; set; }
        public decimal ProduccionTotal { get; set; }
        public decimal PorcentajeMerma { get; set; }

        public string Observaciones { get; set; }
        public string Estado { get; set; }
    }
}
