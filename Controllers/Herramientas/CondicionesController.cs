using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Herramientas
{
    internal class CondicionesController : ICatalogoCRUD<Condiciones, int>
    {
        public CondicionesController()
        {
        }

        public void Crear(Condiciones entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Condiciones.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void CrearCondicionVehiculoViewModel(CondicionesVehiculoViewModel entidad)
        {
            using (var _context = new Model.Model())
            {
                CondicionesVehiculosRecepciones nuevaCondicionVehiculo = new CondicionesVehiculosRecepciones
                {
                    condicion_vehiculo_recepcion_id = entidad.CondicionVehiculoRecepcionId,
                    vehiculo_recepcion_id = entidad.VehiculoRecepcionId,
                    condicion_id = entidad.CondicionId,
                    condicion_estado = entidad.Valor
                };
                AsignarIdSiCero(ref nuevaCondicionVehiculo);
                SesionActualHelper.RegistrarSesionCrea(ref nuevaCondicionVehiculo);
                _context.CondicionesVehiculosRecepciones.Add(nuevaCondicionVehiculo);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Condiciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Condiciones.Find(entidad.condicion_id);
                if (registro != null)
                {
                    entidad.usuario_crea = registro.usuario_crea;
                    entidad.fecha_crea = registro.fecha_crea;
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void ActualizarCondicionVehiculoViewModel(CondicionesVehiculoViewModel entidad)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.CondicionesVehiculosRecepciones.Find(entidad.CondicionVehiculoRecepcionId);
                if (registro != null)
                {
                    registro.condicion_estado = entidad.Valor;
                    SesionActualHelper.RegistrarUsuarioModifica(ref registro);
                    _context.Entry(registro).CurrentValues.SetValues(registro);
                    _context.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.Condiciones.Find(id);
                if (registro != null)
                {
                    _context.Condiciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarCondicionVehiculoRecepcion(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.CondicionesVehiculosRecepciones.Find(id);
                if (registro != null)
                {
                    _context.CondicionesVehiculosRecepciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Condiciones ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Condiciones.Find(id);
            }
        }

        public List<Condiciones> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Condiciones.ToList();
            }
        }

        public List<CondicionesVehiculoViewModel> ListarCondicionesPorRecepcionId(int _recepcionId)
        {
            List<CondicionesVehiculoViewModel> _condiciones;
            using (var _context = new Model.Model())
            {
                var query = from cv in _context.VehiculosRecepciones
                            join cvr in _context.CondicionesVehiculosRecepciones on cv.vehiculo_recepcion_id equals cvr.vehiculo_recepcion_id
                            join c in _context.Condiciones on cvr.condicion_id equals c.condicion_id
                            where cv.recepcion_id == _recepcionId
                            select new CondicionesVehiculoViewModel
                            {
                                CondicionVehiculoRecepcionId = cvr.condicion_vehiculo_recepcion_id,
                                VehiculoRecepcionId = cv.vehiculo_recepcion_id,
                                CondicionId = c.condicion_id,
                                Nombre = c.nombre,
                                Valor = cvr.condicion_estado
                            }
            ;
                _condiciones = query.ToList();

            }
            if (_condiciones.Count == 0)
                return new List<CondicionesVehiculoViewModel>();
            else
                return _condiciones;
        }

        public List<CondicionesVehiculosRecepciones> ListarCondicionesVehiculosRecepcionesPorRecepcionId(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                var query = from cv in _context.VehiculosRecepciones
                            join cvr in _context.CondicionesVehiculosRecepciones on cv.vehiculo_recepcion_id equals cvr.vehiculo_recepcion_id
                            where cv.recepcion_id == _recepcionId
                            select cvr;
                return query.ToList();
            }
        }

        public List<CondicionesVehiculoViewModel> ListarCondicionesActivasSinValorPorRecepcionId(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                var query =
                    (from c in _context.Condiciones
                     where !(from cv in _context.VehiculosRecepciones
                             join cvr in _context.CondicionesVehiculosRecepciones
                                on cv.vehiculo_recepcion_id equals cvr.vehiculo_recepcion_id
                             where cv.recepcion_id == _recepcionId
                             select cvr.condicion_id
                            ).Contains(c.condicion_id)
                     select new CondicionesVehiculoViewModel
                     {
                         CondicionVehiculoRecepcionId = 0,
                         VehiculoRecepcionId = 0,
                         CondicionId = c.condicion_id,
                         Nombre = c.nombre,
                         Valor = false
                     }).ToList();
                return query.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarCombo()
        {
            using (var _context = new Model.Model())
            {
                var temp = _context.Condiciones
                    .Select(r => new { r.condicion_id, r.nombre }) // traducible a SQL
                    .ToList(); // materializa la consulta

                return temp
                    .Select(x => new KeyValuePair<string, string>(x.condicion_id.ToString(), x.nombre))
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Condiciones entidad)
        {
            if (entidad.condicion_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Condiciones
                                      .OrderByDescending(p => p.condicion_id)
                                      .Select(p => p.condicion_id)
                                      .FirstOrDefault();

                    entidad.condicion_id = ultimoId + 1;
                }
                ;
            }
        }

        private void AsignarIdSiCero(ref CondicionesVehiculosRecepciones entidad)
        {
            if (entidad.condicion_vehiculo_recepcion_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CondicionesVehiculosRecepciones
                                      .OrderByDescending(p => p.condicion_vehiculo_recepcion_id)
                                      .Select(p => p.condicion_vehiculo_recepcion_id)
                                      .FirstOrDefault();
                    entidad.condicion_vehiculo_recepcion_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
