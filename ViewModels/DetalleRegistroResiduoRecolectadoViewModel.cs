using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    public class DetalleRegistroResiduoRecolectadoViewModel
    {
        public int DetalleRegistroId { get; set; }
        public int FrutaId { get; set; }
        public string NombreFruta { get; set; }
        public int TipoResiduoId { get; set; } 
        public string NombreTipoResiduo { get; set; } 
        [Range(0.01, double.MaxValue)] 
        public decimal Cantidad { get; set; } 
    }
}
