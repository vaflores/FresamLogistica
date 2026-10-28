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
    internal class TitulosCalificacionesController : ICatalogoCRUD<CicloCalificaciones, int>
    {
        public TitulosCalificacionesController()
        {
        }

        public void Crear(CicloCalificaciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                _context.CicloCalificaciones.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloCalificaciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloCalificaciones.Find(entidad.ciclo_calificacion_id);
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
                var registro = _context.CicloCalificaciones.Find(id);
                if (registro != null)
                {
                    _context.CicloCalificaciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloCalificaciones ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloCalificaciones.Find(id);
            }
        }

        public CicloCalificaciones ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloCalificaciones
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }
        public List<CicloCalificaciones> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloCalificaciones.ToList();
            }
        }

    }
}
