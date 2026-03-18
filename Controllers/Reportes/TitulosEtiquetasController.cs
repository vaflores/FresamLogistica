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
    internal class TitulosEtiquetasController : ICatalogoCRUD<CicloEtiquetas, int>
    {
        public TitulosEtiquetasController()
        {
        }

        public void Crear(CicloEtiquetas entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CicloEtiquetas.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloEtiquetas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloEtiquetas.Find(entidad.etiqueta_id);
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
                var registro = _context.CicloEtiquetas.Find(id);
                if (registro != null)
                {
                    _context.CicloEtiquetas.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloEtiquetas ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloEtiquetas.Find(id);
            }
        }

        public CicloEtiquetas ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloEtiquetas
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }

        public List<CicloEtiquetas> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloEtiquetas.ToList();
            }
        }

        public CicloEtiquetas ObtenerTitulosPorCiclo(int _cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloEtiquetas
                               .FirstOrDefault(e => e.ciclo_id == _cicloId);
            }
        }

        private void AsignarIdSiCero(ref CicloEtiquetas entidad)
        {
            if (entidad.etiqueta_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CicloEtiquetas
                                      .OrderByDescending(p => p.etiqueta_id)
                                      .Select(p => p.etiqueta_id)
                                      .FirstOrDefault();

                    entidad.etiqueta_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
