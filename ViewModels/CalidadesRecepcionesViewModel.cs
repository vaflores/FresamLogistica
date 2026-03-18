using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    internal class CalidadesRecepcionesViewModel
    {
        public int CalidadRecepcionId { get; set; }
        public int CalidadId { get; set; }
        public string Nombre { get; set; }
        public int Valor { get; set; } = 0;
    }
}
