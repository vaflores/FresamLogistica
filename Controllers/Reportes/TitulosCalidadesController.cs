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
    internal class TitulosCalidadesController : ICatalogoCRUD<CicloCalidades, int>
    {
        public TitulosCalidadesController()
        {
        }

        public void Crear(CicloCalidades entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CicloCalidades.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloCalidades entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloCalidades.Find(entidad.calidad_id);
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
                var registro = _context.CicloCalidades.Find(id);
                if (registro != null)
                {
                    _context.CicloCalidades.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloCalidades ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloCalidades.Find(id);
            }
        }

        public CicloCalidades ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloCalidades
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }

        public List<CicloCalidades> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloCalidades.ToList();
            }
        }

        private void AsignarIdSiCero(ref CicloCalidades entidad)
        {
            if (entidad.calidad_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CicloCalidades
                                      .OrderByDescending(p => p.calidad_id)
                                      .Select(p => p.calidad_id)
                                      .FirstOrDefault();

                    entidad.calidad_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
