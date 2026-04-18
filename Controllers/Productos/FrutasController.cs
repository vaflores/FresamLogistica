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
    internal class FrutasController : ICatalogoCRUD<Frutas, int>
    {
        public FrutasController()
        {
        }

        public void Crear(Frutas entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Frutas.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Frutas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Frutas.Find(entidad.fruta_id);
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
                var registro = _context.Frutas.Find(id);
                if (registro != null)
                {
                    _context.Frutas.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Frutas ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Frutas.Find(id);
            }
        }

        public List<Frutas> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Frutas.ToList();
            }
        }

        public List<KeyValuePair<string,string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.Frutas
                    .Where(f => f.estado == "A")
                    .ToList()
                    .Select(f => new KeyValuePair<string, string>(f.fruta_id.ToString(),f.nombre))
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox(int _frutaId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Frutas
                    .Where(f => f.estado == "A" || f.fruta_id == _frutaId)
                    .ToList()
                    .Select(f => new KeyValuePair<string, string>(f.fruta_id.ToString(), f.nombre))
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Frutas entidad)
        {
            if (entidad.fruta_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Frutas
                                      .OrderByDescending(p => p.fruta_id)
                                      .Select(p => p.fruta_id)
                                      .FirstOrDefault();

                    entidad.fruta_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
