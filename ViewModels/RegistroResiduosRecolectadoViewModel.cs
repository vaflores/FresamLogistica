using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    internal class RegistroResiduosRecolectadoViewModel
    {

        public int RegistroResiduoId { get; set; }

        // Datos principales
        public DateTime Fecha { get; set; }
        public int EmpresaResiduoId { get; set; }
        [Required]
        public int OperadorResiduoId { get; set; }
        [Required]
        public int DestinoResiduoId { get; set; }
        public bool RequiereFolio { get; set; }
        [Required]
        public int VehiculoId { get; set; }
        public decimal CantidadTotal { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }

        // =============================
        // DETALLE
        // =============================

        public Vehiculos Vehiculo { get; set; } = new Vehiculos();
        public List<DetalleRegistroResiduoRecolectadoViewModel> DetalleResiduosRecolectados { get; set; } = new List<DetalleRegistroResiduoRecolectadoViewModel>();
        public List<FoliosRegistroResiduoRecolectadoViewModel> FoliosRegistroResiduosRecolectados { get; set; } = new List<FoliosRegistroResiduoRecolectadoViewModel>();

    }
}
