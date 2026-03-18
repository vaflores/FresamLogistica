using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class AgricultoresController : ICatalogoCRUD<Agricultores, int>
    {
        public AgricultoresController()
        {
        }

        public void Crear(Agricultores entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Agricultores.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Agricultores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Agricultores.Find(entidad.agricultor_id);
                if (registro != null)
                {
                    entidad.usuario_crea = registro.usuario_crea;
                    entidad.fecha_crea = registro.fecha_crea;
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.Agricultores.Find(id);
                if (registro != null)
                {
                    _context.Agricultores.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Agricultores ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Agricultores.Find(id);
            }
        }

        public List<Agricultores> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Agricultores.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.Agricultores
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.agricultor_id.ToString(), g.nombre.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Value)
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Agricultores entidad)
        {
            if (entidad.agricultor_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Agricultores
                                      .OrderByDescending(p => p.agricultor_id)
                                      .Select(p => p.agricultor_id)
                                      .FirstOrDefault();

                    entidad.agricultor_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
