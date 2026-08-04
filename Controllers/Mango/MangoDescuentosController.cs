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
    internal class MangoDescuentosController : ICatalogoCRUD<MangoDescuentos, int>
    {
        public MangoDescuentosController()
        {
        }

        public void Crear(MangoDescuentos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoDescuentos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void CrearDescuentoRecepcionViewModel(MangoDescuentos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoDescuentos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoDescuentos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoDescuentos.Find(entidad.descuento_id);
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
                var registro = _context.MangoDescuentos.Find(id);
                if (registro != null)
                {
                    _context.MangoDescuentos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoDescuentos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoDescuentos.Find(id);
            }
        }

        public MangoDescuentos ObtenerPorRecepcionId(int _idRecepcion)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoDescuentos
                    .Where(t => t.mango_recepcion_id == _idRecepcion)
                    .FirstOrDefault();
            }
        }

        public List<MangoDescuentos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoDescuentos.ToList();
            }
        }

        public List<MangoDescuentos> ListarPorRecepcion(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoDescuentos
                    .Where(t => t.mango_recepcion_id == _recepcionId)
                    .ToList();
            }
        }

    }
}
