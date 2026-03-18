using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Reportes
{
    internal class TitulosInspeccionesController : ICatalogoCRUD<CicloInspecciones, int>
    {
        public TitulosInspeccionesController()
        {
        }

        public void Crear(CicloInspecciones entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                _context.CicloInspecciones.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloInspecciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloInspecciones.Find(entidad.inspeccion_id);
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
                var registro = _context.CicloInspecciones.Find(id);
                if (registro != null)
                {
                    _context.CicloInspecciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloInspecciones ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloInspecciones.Find(id);
            }
        }

        public CicloInspecciones ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloInspecciones
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }
        public List<CicloInspecciones> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloInspecciones.ToList();
            }
        }

        private void AsignarIdSiCero(ref CicloInspecciones entidad)
        {
            if (entidad.inspeccion_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CicloInspecciones
                                      .OrderByDescending(p => p.inspeccion_id)
                                      .Select(p => p.inspeccion_id)
                                      .FirstOrDefault();

                    entidad.inspeccion_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
