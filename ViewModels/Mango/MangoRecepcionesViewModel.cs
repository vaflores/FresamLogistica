using System;
using System.Collections.Generic;
using FresamLogistica.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels.Mango
{
    internal class MangoRecepcionesViewModel
    {
        public int RecepcionId { get; set; }
        public int CicloId { get; set; }
        public int? FolioCiclo { get; set; }
        public int? FolioProveedor { get; set; }
        public DateTime Fecha  { get; set; } = DateTime.Now;
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; }
        public int PredioId { get; set; }   
        public string PredioNombre { get; set; }
        public int PropietarioId { get; set; }
        public string PropietarioNombre { get; set; }
        public int LocalidadId { get; set; }
        public string LocalidadNombre { get; set; }
        public int FrutaId { get; set; }
        public string FrutaNombre { get; set; }
        public int VariedadId { get; set; }
        public string VariedadNombre { get; set; }
        public string GuiaSanitaria { get; set; }
        public string TicketBascula { get; set; }
        public Decimal PesoBruto { get; set; }
        public Decimal Tara { get; set; }
        public Decimal PesoNeto { get; set; }
        public Decimal? CalidadPromedio { get; set; }
        public string GradoId { get; set; }
        public decimal? Importe { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public string Placas { get; set; }
        public string Estado { get; set; }
        public decimal? PorcentajeCosto { get; set; }
        public bool AplicarPorcentajeCosto { get; set; }
        public int? ResponsableEmpleadoId { get; set; }
        public int? VerificaEmpleadoId { get; set; }

        public VehiculosRecepcionesViewModel VehiculoInfo { get; set; }

        public MangoDescuentos Descuentos { get; set; }

        public List<MangoCalidadesRecepcionesViewModel> CalidadesInfo { get; set; }

        public List<MangoCalidadesRecepcionesViewModel> CalidadesActivas { get; set; }
    }
}
