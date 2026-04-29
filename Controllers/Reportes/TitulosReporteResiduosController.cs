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
    internal class TitulosReporteResiduosController : ICatalogoCRUD<TitulosReporteResiduos, int>
    {
        public TitulosReporteResiduosController()
        {
        }

        public void Crear(TitulosReporteResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.TitulosReporteResiduos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(TitulosReporteResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.TitulosReporteResiduos.Find(entidad.titulo_id);
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
                var registro = _context.TitulosReporteResiduos.Find(id);
                if (registro != null)
                {
                    _context.TitulosReporteResiduos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public TitulosReporteResiduos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.TitulosReporteResiduos.Find(id);
            }
        }

        public TitulosReporteResiduos ObtenerPorCicloId(int tituloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.TitulosReporteResiduos
                               .FirstOrDefault(ci => ci.titulo_id == tituloId);
            }
        }

        public List<TitulosReporteResiduos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.TitulosReporteResiduos.ToList();
            }
        }

    }
}
