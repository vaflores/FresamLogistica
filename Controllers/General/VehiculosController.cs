using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class VehiculosController : ICatalogoCRUD<Vehiculos, int>
    {
        public VehiculosController()
        {
        }

        public void Crear(Vehiculos entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Vehiculos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void CrearVehiculoRecepcionViewModel(VehiculosRecepcionesViewModel entidad)
        {
            VehiculosRecepcionesViewModel vehiculo = ObtenerPorPlacas(entidad.Placas);
            if (vehiculo == null)
            {
                using (var _context = new Model.Model())
                {
                    Vehiculos nuevaVehiculo = new Vehiculos
                    {
                        vehiculo_id = 0,
                        placas = entidad.Placas,
                        marca_modelo = entidad.MarcaModelo,
                        color = entidad.Color,
                        estado = "A"
                    };
                    AsignarIdSiCero(ref nuevaVehiculo);
                    SesionActualHelper.RegistrarSesionCrea(ref nuevaVehiculo);
                    _context.Vehiculos.Add(nuevaVehiculo);
                    _context.SaveChanges();
                }
            }
        }

        public void CrearVehiculoRecepcion(VehiculosRecepcionesViewModel entidad)
        {
            VehiculosRecepcionesViewModel vehiculo = ObtenerPorPlacas(entidad.Placas);
            if (vehiculo != null)
            {
                using (var _context = new Model.Model())
                {
                VehiculosRecepciones nuevaVehiculoRecepcion = new VehiculosRecepciones
                {
                    vehiculo_recepcion_id = 0,
                    recepcion_id = entidad.RecepcionId,
                    vehiculo_id = vehiculo.VehiculoId,
                    nombre_chofer = entidad.Chofer
                };
                AsignarIdSiCero(ref nuevaVehiculoRecepcion);
                SesionActualHelper.RegistrarSesionCrea(ref nuevaVehiculoRecepcion);
                _context.VehiculosRecepciones.Add(nuevaVehiculoRecepcion);
                _context.SaveChanges();
                }
            }
        }

        public void Actualizar(Vehiculos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Vehiculos.Find(entidad.vehiculo_id);
                entidad.usuario_crea = registro.usuario_crea;
                entidad.fecha_crea = registro.fecha_crea;
                if (registro != null)
                {
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void ActualizarVehiculoRecepcionViewModel(VehiculosRecepcionesViewModel entidad)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.VehiculosRecepciones.Find(entidad.VehiculoRecepcionId);
                if (registro != null)
                {
                    registro.vehiculo_id = entidad.VehiculoId;
                    registro.nombre_chofer = entidad.Chofer;

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
                var registro = _context.Vehiculos.Find(id);
                if (registro != null)
                {
                    _context.Vehiculos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Vehiculos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Vehiculos.Find(id);
            }
        }

        public VehiculosRecepciones ObtenerPorRecepcionIdVehiculoId(int _idRecepcion, int _idVehiculo)
        {
            using (var _context = new Model.Model())
            {
                return _context.VehiculosRecepciones
                    .Where(r => r.recepcion_id == _idRecepcion )
                    .FirstOrDefault();
            }
        }

        public VehiculosRecepcionesViewModel ObtenerVehiculoRecepcionPorRecepcionId(int _RececionId)
        {
            using (var _context = new Model.Model())
            {
                return _context.VehiculosRecepciones
                    .Where(r => r.recepcion_id == _RececionId)
                    .Select(r => new VehiculosRecepcionesViewModel
                    {
                        VehiculoRecepcionId = r.vehiculo_recepcion_id,
                        RecepcionId = r.recepcion_id,
                        VehiculoId = r.vehiculo_id,
                        Chofer = r.nombre_chofer,
                        Placas = r.Vehiculos.placas,
                        Color = r.Vehiculos.color,
                        MarcaModelo = r.Vehiculos.marca_modelo,
                        Estado = r.Vehiculos.estado
                    })
                    .FirstOrDefault();
            }
        }

        public List<Vehiculos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Vehiculos.ToList();
            }
        }

        private void AsignarIdSiCero(ref Vehiculos entidad)
        {
            if (entidad.vehiculo_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Vehiculos
                                      .OrderByDescending(p => p.vehiculo_id)
                                      .Select(p => p.vehiculo_id)
                                      .FirstOrDefault();

                    entidad.vehiculo_id = ultimoId + 1;
                }
                ;
            }
        }

        private void AsignarIdSiCero(ref VehiculosRecepciones entidad)
        {
            if (entidad.vehiculo_recepcion_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.VehiculosRecepciones
                                      .OrderByDescending(p => p.vehiculo_recepcion_id)
                                      .Select(p => p.vehiculo_recepcion_id)
                                      .FirstOrDefault();
                    entidad.vehiculo_recepcion_id = ultimoId + 1;
                }
                ;
            }
        }

        public VehiculosRecepcionesViewModel ObtenerPorPlacas(string _placas, int _idAgriculor)
        {
            VehiculosRecepcionesViewModel vehiculo;
            using (var _context = new Model.Model())
            {
                vehiculo = _context.VehiculosRecepciones
                    .Where(r => r.Vehiculos.placas == _placas)
                    .Select(r => new VehiculosRecepcionesViewModel
                    {
                        VehiculoRecepcionId = r.vehiculo_recepcion_id,
                        RecepcionId = r.recepcion_id,
                        VehiculoId = r.vehiculo_id,
                        Chofer = r.nombre_chofer,
                        Placas = r.Vehiculos.placas,
                        Color = r.Vehiculos.color,
                        MarcaModelo = r.Vehiculos.marca_modelo,
                        Estado = r.Vehiculos.estado
                    })
                    .FirstOrDefault();
            }

            if(vehiculo == null)
            {
                vehiculo = ObtenerPorPlacas(_placas);
            }
            return vehiculo;
        }

        public VehiculosRecepcionesViewModel ObtenerPorPlacas(string _placas)
        {
            using (var _context = new Model.Model())
            {
                return _context.Vehiculos
                    .Where(r => r.placas == _placas)
                    .Select(r => new VehiculosRecepcionesViewModel
                    {
                        VehiculoRecepcionId = 0,
                        RecepcionId = 0,
                        VehiculoId = r.vehiculo_id,
                        Chofer = "",
                        Placas = r.placas,
                        Color = r.color,
                        MarcaModelo = r.marca_modelo,
                        Estado = r.estado
                    })
                    .FirstOrDefault();
            }
        }

    }
}
