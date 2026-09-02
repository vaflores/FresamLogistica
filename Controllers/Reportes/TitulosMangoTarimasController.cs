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
    internal class TitulosMangoTarimasController : ICatalogoCRUD<CicloMangosTarimas, int>
    {
        public TitulosMangoTarimasController()
        {
        }

        public void Crear(CicloMangosTarimas entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CicloMangosTarimas.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloMangosTarimas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloMangosTarimas.Find(entidad.mango_tarima_id);
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
                var registro = _context.CicloMangosTarimas.Find(id);
                if (registro != null)
                {
                    _context.CicloMangosTarimas.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloMangosTarimas ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangosTarimas.Find(id);
            }
        }

        public CicloMangosTarimas ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangosTarimas
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }

        public List<CicloMangosTarimas> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangosTarimas.ToList();
            }
        }

        public CicloMangosTarimas ObtenerTitulosPorCiclo(int _cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangosTarimas
                               .FirstOrDefault(e => e.ciclo_id == _cicloId);
            }
        }

        private void AsignarIdSiCero(ref CicloMangosTarimas entidad)
        {
            if (entidad.mango_tarima_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CicloMangosTarimas
                                      .OrderByDescending(p => p.mango_tarima_id)
                                      .Select(p => p.mango_tarima_id)
                                      .FirstOrDefault();

                    entidad.mango_tarima_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
