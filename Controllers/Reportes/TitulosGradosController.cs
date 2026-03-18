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
    internal class TitulosGradosController : ICatalogoCRUD<CicloGrados, int>
    {
        public TitulosGradosController()
        {
        }

        public void Crear(CicloGrados entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CicloGrados.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloGrados entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloGrados.Find(entidad.grado_id);
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
                var registro = _context.CicloGrados.Find(id);
                if (registro != null)
                {
                    _context.CicloGrados.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloGrados ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloGrados.Find(id);
            }
        }

        public CicloGrados ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloGrados
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }

        public List<CicloGrados> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloGrados.ToList();
            }
        }

        private void AsignarIdSiCero(ref CicloGrados entidad)
        {
            if (entidad.grado_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CicloGrados
                                      .OrderByDescending(p => p.grado_id)
                                      .Select(p => p.grado_id)
                                      .FirstOrDefault();

                    entidad.grado_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
