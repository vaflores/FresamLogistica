using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Configuracion
{
    internal class RolesPermisosController : ICatalogoCRUD<RolesPermisos, int>
    {
        public RolesPermisosController()
        {
        }

        public void Crear(RolesPermisos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.RolesPermisos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(RolesPermisos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.RolesPermisos.Find(entidad.id_permiso);
                entidad.usuario_crea = registro.usuario_crea;
                entidad.fecha_crea = registro.fecha_crea;
                if (registro != null)
                {
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.RolesPermisos.Find(id);
                if (registro != null)
                {
                    _context.RolesPermisos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarPorRolYPermiso(string idRol, int idPermiso)
        {
            using (var _context = new Model.Model())
            {
                var sql = @"DELETE FROM RolesPermisos 
                                WHERE id_rol = @p0 AND id_permiso = @p1";

                _context.Database.ExecuteSqlCommand(sql, idRol, idPermiso);
                return;

            }
        }

        public RolesPermisos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.RolesPermisos.Find(id);
            }
        }

        public RolesPermisos ObtenerPorPermisoIdYRolId(int idPermiso, string idRol)
        {
            using (var _context = new Model.Model())
            {
                return _context.RolesPermisos.Where(rp => rp.id_permiso == idPermiso && rp.id_rol == idRol).FirstOrDefault();
            }
        }

        public List<RolesPermisos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.RolesPermisos.ToList();
            }
        }

        public List<RolesPermisosViewModel> ListarPermisos()
        {
            using (var _context = new Model.Model())
            {
                var query = from p in _context.Permisos 
                            join pa in _context.Pantallas on p.id_pantalla equals pa.pantalla_id
                            select new RolesPermisosViewModel
                            {
                                RolId = string.Empty,
                                PermisoId = p.id_permiso,
                                NombrePermiso = p.nombre,
                                PantallaId = pa.pantalla_id,
                                NombrePantalla = pa.nombre,
                                Asignado = false
                            };
                return query.ToList();
            }
        }

        public List<RolesPermisosViewModel> ListarPermisosAsignadosPorRol(string rolId)
        {
            using (var _context = new Model.Model())
            {
                var query = from rp in _context.RolesPermisos
                            join p in _context.Permisos on rp.id_permiso equals p.id_permiso
                            join pa in _context.Pantallas on p.id_pantalla equals pa.pantalla_id
                            where rp.id_rol.ToString() == rolId
                            select new RolesPermisosViewModel
                            {
                                RolId = rp.id_rol,
                                PermisoId = rp.id_permiso,
                                NombrePermiso = p.nombre,
                                PantallaId = pa.pantalla_id,
                                NombrePantalla = pa.nombre,
                                Asignado = true
                            };
                return query.ToList();
            }
        }
    }
}
