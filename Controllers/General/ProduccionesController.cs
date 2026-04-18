using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class ProduccionesController : ICatalogoCRUD<Producciones, int>
    {
        public ProduccionesController()
        {

        }

        public void Crear(Producciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Producciones.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Producciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Producciones.Find(entidad.produccion_id);
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
                var registro = _context.Producciones.Find(id);
                if (registro != null)
                {
                    _context.Producciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Producciones ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Producciones.Find(id);
            }
        }

        public List<Producciones> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Producciones.ToList();
            }
        }

        public List<Producciones> ListarPorFruta(int fruta_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Producciones
                    .Where(v => v.fruta_id == fruta_id)
                    .ToList();
            }
        }

        public Producciones ObtenerProduccionPorFrutaYFecha(int fruta_id, DateTime fecha)
        {
            using (var _context = new Model.Model())
            {
                return _context.Producciones
                    .FirstOrDefault(v => v.fruta_id == fruta_id && v.fecha == fecha.Date);
            }

        }
    }
}
