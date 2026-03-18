using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Productos
{
    internal class VariedadesController : ICatalogoCRUD<Variedades, int>
    {
        public VariedadesController()
        {
        }

        public void Crear(Variedades entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Variedades.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Variedades entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Variedades.Find(entidad.variedad_id);
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
                var registro = _context.Variedades.Find(id);
                if (registro != null)
                {
                    _context.Variedades.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Variedades ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Variedades.Find(id);
            }
        }

        public List<Variedades> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Variedades.ToList();
            }
        }

        public List<Variedades> ListarPorFruta(int fruta_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Variedades
                    .Where(v => v.id_fruta == fruta_id)
                    .ToList();
            }
        }

        public List<KeyValuePair<string,string>> ListarPorFrutaComboBox(int fruta_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Variedades
                    .Where(v => v.id_fruta == fruta_id)
                    .ToList()
                    .Select(v => new KeyValuePair<string, string>(v.variedad_id.ToString(), v.nombre))
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Variedades entidad)
        {
            if (entidad.variedad_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Variedades
                                      .OrderByDescending(p => p.variedad_id)
                                      .Select(p => p.variedad_id)
                                      .FirstOrDefault();

                    entidad.variedad_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
