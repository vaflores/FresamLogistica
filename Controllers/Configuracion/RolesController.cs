using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Configuracion
{
    internal class RolesController : ICatalogoCRUD<Roles, string>
    {
        public RolesController()
        {
        }

        public void Crear(Roles entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Roles.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Roles entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Roles.Find(entidad.rol_id);
                entidad.usuario_crea = registro.usuario_crea;
                entidad.fecha_crea = registro.fecha_crea;
                if (registro != null)
                {
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }


        public void Eliminar(string id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.Roles.Find(id);
                if (registro != null)
                {
                    _context.Roles.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Roles ObtenerPorId(string id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Roles.Find(id);
            }
        }

        public List<Roles> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Roles.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.Roles
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.rol_id.ToString(), g.rol_id.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }


    }
}
