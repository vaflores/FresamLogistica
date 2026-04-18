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
    internal class CiudadesController : ICatalogoCRUD<Ciudades, int>
    {
        public CiudadesController()
        {

        }

        public void Crear(Ciudades entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Ciudades.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Ciudades entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Ciudades.Find(entidad.ciudad_id);
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
                var registro = _context.Ciudades.Find(id);
                if (registro != null)
                {
                    _context.Ciudades.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Ciudades ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciudades.Find(id);
            }
        }

        public List<Ciudades> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciudades.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxPorEstado(int _IdSelecionado)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciudades
                    .Where(g => g.estado_id == _IdSelecionado)
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.ciudad_id.ToString(), g.nombre.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }
        public List<KeyValuePair<string, string>> ListarComboBoxCiudadesActivas()
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciudades
                    .Where(g => g.estado == "A")
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.ciudad_id.ToString(), g.nombre.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Value)
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Ciudades entidad)
        {
            if (entidad.ciudad_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Ciudades
                                      .OrderByDescending(p => p.ciudad_id)
                                      .Select(p => p.ciudad_id)
                                      .FirstOrDefault();

                    entidad.ciudad_id = ultimoId + 1;
                }
                ;
            }
        }

    }
}
