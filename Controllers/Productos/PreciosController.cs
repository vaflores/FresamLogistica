using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Productos
{
    internal class PreciosController : ICatalogoCRUD<Precios, int>
    {
        public PreciosController()
        {

        }

        public void Crear(Precios entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);

                var registroAnterior = ObtenerPrecioAnterior(entidad);
                if (registroAnterior != null)
                { 
                    registroAnterior.fecha_fin = entidad.fecha_inicio.AddDays(-1);
                    _context.Entry(registroAnterior).State = System.Data.Entity.EntityState.Modified;
                }
                entidad.fecha_fin = null;
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Precios.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Precios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Precios.Find(entidad.precio_id);

                var registroAnterior = ObtenerPrecioAnterior(entidad);
                if (registroAnterior != null)
                { 
                    registroAnterior.fecha_fin = entidad.fecha_inicio.AddDays(-1);
                    _context.Entry(registroAnterior).State = System.Data.Entity.EntityState.Modified;
                }
                //_context.SaveChanges();

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
                var registro = _context.Precios.Find(id);
                if (registro != null)
                {
                    _context.Precios.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Precios ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Precios.Find(id);
            }
        }

        public Precios ObtenerPrecioAnterior(Precios entidad)
        {
            using (var _context = new Model.Model())
            {
                return _context.Precios
                    .Where(p => p.fruta_id == entidad.fruta_id && p.grado_id == entidad.grado_id && p.fecha_inicio < entidad.fecha_inicio)
                    .OrderByDescending(p => p.fecha_inicio)
                    .FirstOrDefault();
            }
        }

        public List<Precios> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Precios.ToList();
            }
        }

        public List<Precios> ListarPorFruta(int fruta_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Precios
                    .Where(v => v.fruta_id == fruta_id)
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Precios entidad)
        {
            if (entidad.precio_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Precios
                                      .OrderByDescending(p => p.precio_id)
                                      .Select(p => p.precio_id)
                                      .FirstOrDefault();

                    entidad.precio_id = ultimoId + 1;
                }
                ;
            }
        }

        public decimal ObtenerPrecioActual(int fruta_id, string grado_id, DateTime fecha)
        {
            using (var _context = new Model.Model())
            {
                var precioRegistro = _context.Precios
                    .Where(p => p.fruta_id == fruta_id && p.grado_id == grado_id &&
                                p.fecha_inicio <= fecha &&
                                (p.fecha_fin == null || p.fecha_fin >= fecha))
                    .FirstOrDefault();
                return precioRegistro != null ? precioRegistro.importe : 0m;
            }
        }
    }
}
