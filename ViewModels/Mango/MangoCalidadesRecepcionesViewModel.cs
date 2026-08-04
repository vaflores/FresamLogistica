using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels.Mango
{
    internal class MangoCalidadesRecepcionesViewModel
    {
        public int CalidadRecepcionId { get; set; }
        public int CalidadId { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; } = 0;
        public bool AfectaCalificacion { get; set; } = false;
        public bool IncluyeEnReporte { get; set; } = false;
        public bool? UsoPorcentajeMangoChico { get; set; } = false;
    }
}
