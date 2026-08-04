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
    internal class TitulosMangoEtiquetasController : ICatalogoCRUD<CicloMangos, int>
    {
        public TitulosMangoEtiquetasController()
        {
        }

        public void Crear(CicloMangos entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CicloMangos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloMangos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloMangos.Find(entidad.mango_id);
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
                var registro = _context.CicloMangos.Find(id);
                if (registro != null)
                {
                    _context.CicloMangos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloMangos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangos.Find(id);
            }
        }

        public CicloMangos ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangos
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }

        public List<CicloMangos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangos.ToList();
            }
        }

        public CicloMangos ObtenerTitulosPorCiclo(int _cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangos
                               .FirstOrDefault(e => e.ciclo_id == _cicloId);
            }
        }

        private void AsignarIdSiCero(ref CicloMangos entidad)
        {
            if (entidad.mango_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CicloMangos
                                      .OrderByDescending(p => p.mango_id)
                                      .Select(p => p.mango_id)
                                      .FirstOrDefault();

                    entidad.mango_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
