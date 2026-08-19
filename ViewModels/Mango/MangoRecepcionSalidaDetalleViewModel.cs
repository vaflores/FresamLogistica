using System;
using System.Collections.Generic;
using FresamLogistica.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels.Mango
{
    internal class MangoRecepcionSalidaDetalleViewModel
    {
        public int MangoRecepcionSalidaDetalleId { get; set; }
        public int MangoRecepcionSalidaId { get; set; }
        public int MangoTipoSalidaId { get; set; }
        public string TipoSalida { get; set; }
        public decimal Peso { get; set; }
        public string Observaciones { get; set; }
    }
}
