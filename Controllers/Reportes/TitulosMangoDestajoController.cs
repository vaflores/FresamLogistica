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
    internal class TitulosMangoDestajoController : ICatalogoCRUD<CicloMangosDestajo, int>
    {
        public TitulosMangoDestajoController()
        {
        }

        public void Crear(CicloMangosDestajo entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CicloMangosDestajo.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloMangosDestajo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloMangosDestajo.Find(entidad.mango_destajo_id);
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
                var registro = _context.CicloMangosDestajo.Find(id);
                if (registro != null)
                {
                    _context.CicloMangosDestajo.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloMangosDestajo ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangosDestajo.Find(id);
            }
        }

        public CicloMangosDestajo ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangosDestajo
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }

        public List<CicloMangosDestajo> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangosDestajo.ToList();
            }
        }

        public CicloMangosDestajo ObtenerTitulosPorCiclo(int _cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloMangosDestajo
                               .FirstOrDefault(e => e.ciclo_id == _cicloId);
            }
        }

        private void AsignarIdSiCero(ref CicloMangosDestajo entidad)
        {
            if (entidad.mango_destajo_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CicloMangosDestajo
                                      .OrderByDescending(p => p.mango_destajo_id)
                                      .Select(p => p.mango_destajo_id)
                                      .FirstOrDefault();

                    entidad.mango_destajo_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
