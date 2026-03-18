using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Helper
{
    public sealed class SesionActualHelper
    {
        private RolesPermisosController _repoPermisos = new RolesPermisosController();
        // Instancia única
        private static readonly Lazy<SesionActualHelper> _instancia =
            new Lazy<SesionActualHelper>(() => new SesionActualHelper());

        public static SesionActualHelper Instancia => _instancia.Value;

        private SesionActualHelper()
        {
        }

        public static string UsuarioId { get; private set; } = string.Empty;   
        public static string RolId { get; private set; } = string.Empty;

        public static List<RolesPermisosViewModel> Permisos { get; private set; } = new List<RolesPermisosViewModel>();

        public void RegistraSesionCrea(string usuarioId, string rolId)
        {
            UsuarioId = usuarioId;
            RolId = rolId;
            Permisos = _repoPermisos.ListarPermisosAsignadosPorRol(rolId);
        }

        public static void RegistrarSesionCrea<T>(ref T entidad)
        {
            dynamic obj = entidad;
            obj.usuario_crea = UsuarioId;
            obj.fecha_crea = DateTime.Now;
            obj.usuario_modifica = UsuarioId;
            obj.fecha_modifica = DateTime.Now;
            entidad = obj;
        }

        public static void RegistrarUsuarioModifica<T>( ref T entidad)
        {
            dynamic obj = entidad;
            obj.usuario_modifica = UsuarioId;
            obj.fecha_modifica = DateTime.Now;
            entidad = obj;
        }
        
        // Cerrar sesión
        public void CerrarSesion()
        {
            UsuarioId = string.Empty;
            RolId = string.Empty;
        }

        public bool HaySesionActiva =>
            UsuarioId != "" && RolId != "";
    }

}
