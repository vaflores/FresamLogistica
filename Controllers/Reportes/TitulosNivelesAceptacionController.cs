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
    internal class TitulosNivelesAceptacionController : ICatalogoCRUD<CicloNivelesAceptacion, int>
    {
        public TitulosNivelesAceptacionController()
        {
        }

        public void Crear(CicloNivelesAceptacion entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                _context.CicloNivelesAceptacion.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloNivelesAceptacion entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloNivelesAceptacion.Find(entidad.ciclo_nivel_id);
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
                var registro = _context.CicloNivelesAceptacion.Find(id);
                if (registro != null)
                {
                    _context.CicloNivelesAceptacion.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloNivelesAceptacion ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloNivelesAceptacion.Find(id);
            }
        }

        public CicloNivelesAceptacion ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloNivelesAceptacion
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }
        public List<CicloNivelesAceptacion> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloNivelesAceptacion.ToList();
            }
        }

    }
}
