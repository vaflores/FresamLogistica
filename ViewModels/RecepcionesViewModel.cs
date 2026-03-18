using System;
using System.Collections.Generic;
using FresamLogistica.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    internal class RecepcionesViewModel
    {
        public int RecepcionId { get; set; }
        public int? FolioRecepcionDiaria { get; set; }
        public int CicloId { get; set; }
        public DateTime Fecha  { get; set; } = DateTime.Now;
        public int AgriculorId { get; set; }
        public string AgricultorNombre { get; set; }
        public int RanchoId { get; set; }   
        public string RanchoNombre { get; set; }
        public int SectorId { get; set; }
        public string SectorNombre { get; set; }
        public int VariedadId { get; set; }
        public string VariedadNombre { get; set; }
        public int FrutaId { get; set; }
        public string FrutaNombre { get; set; }
        public int NoTarimas { get; set; }
        public Decimal PesoNeto { get; set; }
        public string GradoId { get; set; }
        public string Folio { get; set; }
        public string Placas { get; set; }
        public decimal? Importe { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public string Estado { get; set; }
        public int? Semana { get; set; } = 0;
        public int? Anio { get; set; } = 0;

        public VehiculosRecepcionesViewModel VehiculoInfo { get; set; }

        public DescuentosRecepciones Descuentos { get; set; }

        public List<Tarimas> TarimasInfo { get; set; }  

        public List<CalidadesRecepcionesViewModel> CalidadesInfo { get; set; }

        public List<CalidadesRecepcionesViewModel> CalidadesActivas { get; set; }
    }
}
