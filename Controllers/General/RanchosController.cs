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
    internal class RanchosController : ICatalogoCRUD<Ranchos, int>
    {
        public RanchosController()
        {
        }

        public void Crear(Ranchos entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Ranchos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Ranchos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Ranchos.Find(entidad.rancho_id);
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
                var registro = _context.Ranchos.Find(id);
                if (registro != null)
                {
                    _context.Ranchos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Ranchos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ranchos.Find(id);
            }
        }

        public List<Ranchos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Ranchos.ToList();
            }
        }
        public List<Ranchos> ListarPorAgricultor(int _agricultor_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ranchos
                    .Where(a=> a.agricultor_id == _agricultor_id)
                    .OrderBy(r=>r.rancho_id)
                    .ToList();
            }
        }

        public List<KeyValuePair<string,string>> ListarPorAgricultorComboBox(int _agricultorId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ranchos
                    .AsEnumerable()
                    .Where(a=> a.agricultor_id == _agricultorId)
                    .Select(g => new KeyValuePair<string, string>(g.rancho_id.ToString(), g.nombre))
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Ranchos entidad)
        {
            if (entidad.rancho_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Ranchos
                                      .OrderByDescending(p => p.rancho_id)
                                      .Select(p => p.rancho_id)
                                      .FirstOrDefault();

                    entidad.rancho_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
