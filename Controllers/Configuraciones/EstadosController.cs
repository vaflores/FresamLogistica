using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Configuraciones
{
    internal class EstadosController : ICatalogoCRUD<Estados, int>
    {
        public EstadosController()
        {

        }

        public void Crear(Estados entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Estados.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Estados entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Estados.Find(entidad.estado_id);
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
                var registro = _context.Estados.Find(id);
                if (registro != null)
                {
                    _context.Estados.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Estados ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Estados.Find(id);
            }
        }

        public List<Estados> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Estados.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.Estados
                            .Where(e => _context.Ciudades.Select(c => c.estado_id).Contains(e.estado_id))
                            .ToList() // ejecuta SQL
                            .Select(e => new KeyValuePair<string, string>(e.estado_id.ToString(), e.nombre))
                            .ToList();
            }
        }

        private void AsignarIdSiCero(ref Estados entidad)
        {
            if (entidad.estado_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Estados
                                      .OrderByDescending(p => p.estado_id)
                                      .Select(p => p.estado_id)
                                      .FirstOrDefault();

                    entidad.estado_id = ultimoId + 1;
                }
                ;
            }
        }

    }
}
