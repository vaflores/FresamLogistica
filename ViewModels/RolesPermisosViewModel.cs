using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    public class RolesPermisosViewModel
    {
        public string RolId { get; set; }
        public int PermisoId { get; set; }
        public string NombrePermiso { get; set; }
        public int PantallaId { get; set; }
        public string NombrePantalla { get; set; }
        public bool Asignado { get; set; }
    }
}
