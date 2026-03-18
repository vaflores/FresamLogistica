using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class TarimasController : ICatalogoCRUD<Tarimas, int>
    {
        public TarimasController()
        {
        }

        public void Crear(Tarimas entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Tarimas.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Tarimas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Tarimas.Find(entidad.tarima_id);
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
                var registro = _context.Tarimas.Find(id);
                if (registro != null)
                {
                    _context.Tarimas.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Tarimas ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Tarimas.Find(id);
            }
        }

        public List<Tarimas> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Tarimas.ToList();
            }
        }

        public List<Tarimas> ListarPorRecepcion(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Tarimas
                    .Where(t => t.recepcion_id == _recepcionId)
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Tarimas entidad)
        {
            if (entidad.tarima_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Tarimas
                                      .OrderByDescending(p => p.tarima_id)
                                      .Select(p => p.tarima_id)
                                      .FirstOrDefault();

                    entidad.tarima_id = ultimoId + 1;
                }
                ;
            }
        }

    }
}
