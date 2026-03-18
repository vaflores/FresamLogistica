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
    internal class DescuentosController : ICatalogoCRUD<DescuentosRecepciones, int>
    {
        public DescuentosController()
        {
        }

        public void Crear(DescuentosRecepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.DescuentosRecepciones.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void CrearDescuentoRecepcionViewModel(DescuentosRecepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.DescuentosRecepciones.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(DescuentosRecepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.DescuentosRecepciones.Find(entidad.descuento_recepcion_id);
                entidad.usuario_crea = registro.usuario_crea;
                entidad.fecha_crea = registro.fecha_crea;
                if (registro != null)
                {
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.DescuentosRecepciones.Find(id);
                if (registro != null)
                {
                    _context.DescuentosRecepciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public DescuentosRecepciones ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.DescuentosRecepciones.Find(id);
            }
        }

        public DescuentosRecepciones ObtenerPorRecepcionId(int _idRecepcion)
        {
            using (var _context = new Model.Model())
            {
                return _context.DescuentosRecepciones
                    .Where(t => t.recepcion_id == _idRecepcion)
                    .FirstOrDefault();
            }
        }

        public List<DescuentosRecepciones> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.DescuentosRecepciones.ToList();
            }
        }

        public List<DescuentosRecepciones> ListarPorRecepcion(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                return _context.DescuentosRecepciones
                    .Where(t => t.recepcion_id == _recepcionId)
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref DescuentosRecepciones entidad)
        {
            if (entidad.descuento_recepcion_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.DescuentosRecepciones
                                      .OrderByDescending(p => p.descuento_recepcion_id)
                                      .Select(p => p.descuento_recepcion_id)
                                      .FirstOrDefault();

                    entidad.descuento_recepcion_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
