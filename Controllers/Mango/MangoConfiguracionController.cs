using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;

namespace FresamLogistica.Controllers.Mango
{
    internal class MangoConfiguracionController : ICatalogoCRUD<MangoConfiguracion, int>
    {

        public MangoConfiguracionController()
        {
        }

        public void Crear(MangoConfiguracion entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoConfiguracion.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoConfiguracion entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var configuracion = _context.MangoConfiguracion.Find(entidad.mango_configuracion_id);
                entidad.usuario_crea = configuracion.usuario_crea;
                entidad.fecha_crea = configuracion.fecha_crea;
                if (configuracion != null)
                {
                    _context.Entry(configuracion).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var configuracion = _context.MangoConfiguracion.Find(id);
                if (configuracion != null)
                {
                    _context.MangoConfiguracion.Remove(configuracion);
                    _context.SaveChanges();
                }
            }
        }

        public MangoConfiguracion ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoConfiguracion.Find(id);
            }
        }

        public int ObtenerMangoFrutaId(int id)
        {
            using (var _context = new Model.Model())
            {
                var result = _context.MangoConfiguracion.Find(id);

                return result?.fruta_id ?? 0;
            }
        }

        public List<MangoConfiguracion> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoConfiguracion.ToList();
            }
        }

    }
}
