using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using FresamLogistica.ViewModels.Mango;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Controllers.Mango
{
    internal class MangoRecepcionesController : ICatalogoCRUD<MangoRecepciones, int>
    {
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly VehiculosController _repoVehiculos = new VehiculosController();
        private readonly CondicionesController _repoCondiciones = new CondicionesController();
        private readonly MangoCalidadesController _repoCalidades = new MangoCalidadesController();
        private readonly MangoDescuentosController _repoDescuentos = new MangoDescuentosController();

        public MangoRecepcionesController()
        {
        }

        public void Crear(MangoRecepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoRecepciones.Add(entidad);
                _context.SaveChanges();
            }
        }

        public int CrearRecepcionViewModel(MangoRecepcionesViewModel entidad)
        {
            using (var _context = new Model.Model())
            {
                MangoRecepciones nuevaRecepcion = new MangoRecepciones
                {
                    mango_recepcion_id = entidad.RecepcionId,
                    ciclo_id = entidad.CicloId,
                    folio_ciclo = entidad.FolioCiclo > 0 ? entidad.FolioCiclo.Value : ObtenerFolioCicloIdSiguiente(entidad.CicloId),
                    folio_proveedor = entidad.FolioCiclo > 0 ? entidad.FolioCiclo.Value : ObtenerFolioCicloIdSiguiente(entidad.CicloId),
                    fecha = entidad.Fecha,
                    proveedor_id = entidad.PropietarioId,
                    predio_id = entidad.PredioId,
                    variedad_id = entidad.VariedadId,
                    guia_sanitaria = entidad.GuiaSanitaria,
                    ticket_bascula = entidad.TicketBascula,
                    peso_bruto = entidad.PesoBruto,
                    tara = entidad.Tara,
                    peso_neto = entidad.PesoNeto,
                    calidad_promedio = entidad.CalidadPromedio,
                    grado_id = entidad.GradoId,
                    importe = entidad.Importe,
                    precio_unitario = entidad.PrecioUnitario,
                    estado = entidad.Estado,
                    porcentaje_costo_afecta_peso = entidad.AplicarPorcentajeCosto,
                    porcentaje_costo = entidad.PorcentajeCosto
                };
                SesionActualHelper.RegistrarSesionCrea(ref nuevaRecepcion);
                _context.MangoRecepciones.Add(nuevaRecepcion);
                _context.SaveChanges();
                entidad.RecepcionId = nuevaRecepcion.mango_recepcion_id;
            }

            if (!string.IsNullOrWhiteSpace(entidad.VehiculoInfo.Placas))
            {
                // Crear vehículo
                VehiculosRecepcionesViewModel vehiculo = _repoVehiculos.ObtenerPorPlacasMango(entidad.VehiculoInfo.Placas, entidad.ProveedorId);
                if (vehiculo == null)
                {
                    _repoVehiculos.CrearVehiculoMangoRecepcionViewModel(entidad.VehiculoInfo);
                }
                entidad.VehiculoInfo.RecepcionId = entidad.RecepcionId;
                _repoVehiculos.CrearVehiculoMangoRecepcion(entidad.VehiculoInfo);

                vehiculo = _repoVehiculos.ObtenerPorPlacasMango(entidad.VehiculoInfo.Placas, entidad.ProveedorId);
                MangoVehiculoRecepcion vehiculoRecepcion = _repoVehiculos.ObtenerPorMangoRecepcionIdVehiculoId(entidad.RecepcionId, vehiculo.VehiculoId);

                // Crear condiciones
                foreach (CondicionesVehiculoViewModel condicion in entidad.VehiculoInfo.CondicionesVehiculoInfo)
                {
                    if (!condicion.Valor) continue;
                    condicion.VehiculoRecepcionId = vehiculoRecepcion.mango_vehiculo_recepcion_id;
                    _repoCondiciones.CrearMangoCondicionVehiculoViewModel(condicion);
                }
            }

            // Crear descuentos
            entidad.Descuentos.mango_recepcion_id = entidad.RecepcionId;
            _repoDescuentos.CrearDescuentoRecepcionViewModel(entidad.Descuentos);

            // Crear calidades
            foreach(MangoCalidadesRecepcionesViewModel calidad in entidad.CalidadesInfo)
            {
                if (calidad.Valor == 0) continue;
                calidad.CalidadRecepcionId = entidad.RecepcionId;
                _repoCalidades.CrearCalidadesRecepcionesViewModel(calidad);
            }
            
            return entidad.RecepcionId;
        }

        public void Actualizar(MangoRecepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoRecepciones.Find(entidad.mango_recepcion_id);
                if (registro != null)
                {
                    entidad.usuario_crea = registro.usuario_crea;
                    entidad.fecha_crea = registro.fecha_crea;
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void ActualizarRecepcionViewModel(MangoRecepcionesViewModel entidad)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MangoRecepciones.Find(entidad.RecepcionId);
                if (registro != null)
                {
                    registro.mango_recepcion_id = entidad.RecepcionId;
                    registro.ciclo_id = entidad.CicloId;
                    registro.folio_ciclo = entidad.FolioCiclo > 0 ? entidad.FolioCiclo.Value : ObtenerFolioCicloIdSiguiente(entidad.CicloId);
                    registro.folio_proveedor = entidad.FolioCiclo > 0 ? entidad.FolioCiclo.Value : ObtenerFolioCicloIdSiguiente(entidad.CicloId);
                    registro.fecha = entidad.Fecha;
                    registro.proveedor_id = entidad.PropietarioId;
                    registro.predio_id = entidad.PredioId;
                    registro.variedad_id = entidad.VariedadId;
                    registro.guia_sanitaria = entidad.GuiaSanitaria;
                    registro.ticket_bascula = entidad.TicketBascula;
                    registro.peso_bruto = entidad.PesoBruto;
                    registro.tara = entidad.Tara;
                    registro.peso_neto = entidad.PesoNeto;
                    registro.calidad_promedio = entidad.CalidadPromedio;
                    registro.grado_id = entidad.GradoId;
                    registro.importe = entidad.Importe;
                    registro.precio_unitario = entidad.PrecioUnitario;
                    registro.estado = entidad.Estado;
                    registro.porcentaje_costo_afecta_peso = entidad.AplicarPorcentajeCosto;
                    registro.porcentaje_costo = entidad.PorcentajeCosto;
                    SesionActualHelper.RegistrarUsuarioModifica(ref registro);
                    _context.Entry(registro).CurrentValues.SetValues(registro);
                    _context.SaveChanges();
                }
                else return;
            }

            if (!string.IsNullOrWhiteSpace(entidad.VehiculoInfo.Placas))
            {
                // Crear vehículo
                VehiculosRecepcionesViewModel vehiculo = _repoVehiculos.ObtenerPorPlacasMango(entidad.VehiculoInfo.Placas, entidad.ProveedorId);
                if (vehiculo == null)
                {
                    _repoVehiculos.CrearVehiculoMangoRecepcionViewModel(entidad.VehiculoInfo);
                }
                vehiculo = _repoVehiculos.ObtenerPorPlacasMango(entidad.VehiculoInfo.Placas, entidad.ProveedorId);

                MangoVehiculoRecepcion vehiculoRecepcion = _repoVehiculos.ObtenerPorMangoRecepcionIdVehiculoId(entidad.RecepcionId, vehiculo.VehiculoId);

                entidad.VehiculoInfo.RecepcionId = entidad.RecepcionId;
                entidad.VehiculoInfo.VehiculoId = vehiculo.VehiculoId;
                if(vehiculoRecepcion == null)
                {
                    _repoVehiculos.CrearVehiculoMangoRecepcion(entidad.VehiculoInfo);
                    vehiculoRecepcion = _repoVehiculos.ObtenerPorMangoRecepcionIdVehiculoId(entidad.RecepcionId, vehiculo.VehiculoId);

                    entidad.VehiculoInfo.VehiculoRecepcionId = vehiculoRecepcion.mango_vehiculo_recepcion_id;

                }
                else
                {
                    entidad.VehiculoInfo.VehiculoRecepcionId = vehiculoRecepcion.mango_vehiculo_recepcion_id;
                    _repoVehiculos.ActualizarVehiculoMangoRecepcionViewModel(entidad.VehiculoInfo);
                }
                entidad.VehiculoInfo.VehiculoRecepcionId = vehiculoRecepcion == null ? 0 : vehiculoRecepcion.mango_vehiculo_recepcion_id;
                _repoVehiculos.ActualizarVehiculoMangoRecepcionViewModel(entidad.VehiculoInfo);

                // Actualizar condiciones
                List<MangoCondicionesVehiculoRecepcion> _elimarCondiciones = _repoCondiciones.ListarMangoCondicionesVehiculosRecepcionesPorRecepcionId(entidad.RecepcionId);

                if (_elimarCondiciones.Any())
                    foreach (MangoCondicionesVehiculoRecepcion c in _elimarCondiciones)
                    {
                        _repoCondiciones.EliminarMangoCondicionVehiculoRecepcion(c.condicion_vehiculo_recepcion_id);
                    }

                foreach (CondicionesVehiculoViewModel condicion in entidad.VehiculoInfo.CondicionesVehiculoInfo)
                {
                    if (!condicion.Valor) continue;
                    condicion.VehiculoRecepcionId = vehiculoRecepcion.mango_vehiculo_recepcion_id;
                    _repoCondiciones.CrearMangoCondicionVehiculoViewModel(condicion);
                }
            }
            
            // Actualizar descuentos
            MangoDescuentos descuento = _repoDescuentos.ObtenerPorRecepcionId(entidad.RecepcionId);
            entidad.Descuentos.mango_recepcion_id = entidad.RecepcionId;
            entidad.Descuentos.descuento_id = descuento == null ? 0 : descuento.descuento_id;
            if (descuento != null)
                _repoDescuentos.Actualizar(entidad.Descuentos);
            else
                _repoDescuentos.CrearDescuentoRecepcionViewModel(entidad.Descuentos);

            // Actualizar calidades
            
            List<MangoCalidadesRecepcion> _eliminarCalidades = _repoCalidades.ListarCalidadesRecepcionesPorRecepcionId(entidad.RecepcionId);

            if (_eliminarCalidades.Any())
                foreach (MangoCalidadesRecepcion c in _eliminarCalidades)
                {
                    _repoCalidades.EliminarCalidadRecepcion(c.calidad_recepcion_id);
                }
            foreach (MangoCalidadesRecepcionesViewModel calidad in entidad.CalidadesInfo)
            {
                if (calidad.Valor == 0) continue;
                calidad.CalidadRecepcionId = entidad.RecepcionId;
                _repoCalidades.CrearCalidadesRecepcionesViewModel(calidad);
            }
            
        }
        
        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MangoRecepciones.Find(id);
                if (registro != null)
                {
                    _context.MangoRecepciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoRecepciones ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoRecepciones.Find(id);
            }
        }

        public MangoRecepcionesViewModel ObtenerRecepcionPorId( int _idRecepcion)
        {
            MangoRecepcionesViewModel registro = new MangoRecepcionesViewModel();
            using (var _context = new Model.Model())
            {
                registro = _context.MangoRecepciones
                    .Where(r => r.mango_recepcion_id == _idRecepcion)
                    .Select(r => new MangoRecepcionesViewModel
                    {
                        RecepcionId = r.mango_recepcion_id,
                        CicloId = r.ciclo_id,
                        FolioCiclo = r.folio_ciclo,
                        FolioProveedor = r.folio_proveedor,
                        Fecha = r.fecha,
                        ProveedorId = r.proveedor_id,
                        ProveedorNombre = r.MangoProveedores.nombre,
                        PredioId = r.predio_id,
                        PredioNombre = r.MangoPredios.nombre,
                        PropietarioId = r.MangoPredios.propietario_id,
                        PropietarioNombre = r.MangoPredios.MangoPropietarios.nombre,
                        LocalidadId = r.MangoPredios.localidad_id,
                        LocalidadNombre = r.MangoPredios.MangoLocalidades.nombre,
                        FrutaId = r.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Variedades.Frutas.nombre,
                        VariedadId = r.variedad_id,
                        VariedadNombre = r.Variedades.nombre,
                        GuiaSanitaria = r.guia_sanitaria,
                        TicketBascula = r.ticket_bascula,
                        PesoBruto = r.peso_bruto,
                        Tara = r.tara,
                        PesoNeto = r.peso_neto,
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,
                        Placas = r.MangoVehiculoRecepcion.Select(vr => vr.Vehiculos.placas).FirstOrDefault(),
                        GradoId = r.grado_id,
                        CalidadPromedio = r.calidad_promedio,
                        Estado = r.estado,
                        AplicarPorcentajeCosto = r.porcentaje_costo_afecta_peso,
                        PorcentajeCosto = r.porcentaje_costo
                    })
                    .FirstOrDefault();
            }

            if (registro != null)
            {
                registro.VehiculoInfo = _repoVehiculos.ObtenerVehiculoMangoRecepcionPorRecepcionId(registro.RecepcionId);
                registro.Descuentos = _repoDescuentos.ObtenerPorRecepcionId(_idRecepcion);

                registro.CalidadesInfo = _repoCalidades.ListarCalidadesPorRecepcionId(_idRecepcion);
                registro.CalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(_idRecepcion);

                if (registro.VehiculoInfo != null)
                {
                    registro.VehiculoInfo.CondicionesVehiculoInfo = _repoCondiciones.ListarMangoCondicionesPorRecepcionId(_idRecepcion);
                    registro.VehiculoInfo.CondicionesVehiculoActivas = _repoCondiciones.ListarMangoCondicionesActivasSinValorPorRecepcionId(_idRecepcion);
                }

                using (var _context = new Model.Model())
                {
                    registro.ResponsableEmpleadoId = _context.MangoMuestreos.Where(m => m.mango_recepcion_id == registro.RecepcionId).Select(m => m.responsable_empleado_id).FirstOrDefault();
                    registro.VerificaEmpleadoId = _context.MangoMuestreos.Where(m => m.mango_recepcion_id == registro.RecepcionId).Select(m => m.verifica_empleado_id).FirstOrDefault();
                }
            }
            return registro;
        }

        public List<MangoRecepcionesViewModel> ObtenerRecepcionesPorCicloIdProveedorId(int _cicloId, int _proveedorId)
        {
            List<MangoRecepcionesViewModel> _registros = new List<MangoRecepcionesViewModel>();

            using (var _context = new Model.Model())
            {
                _registros = _context.MangoRecepciones
                    .Where(r => r.estado != "I" && r.ciclo_id == _cicloId && r.proveedor_id == _proveedorId)
                    .Select(r => new MangoRecepcionesViewModel
                    {
                        RecepcionId = r.mango_recepcion_id,
                        CicloId = r.ciclo_id,
                        FolioCiclo = r.folio_ciclo,
                        FolioProveedor = r.folio_proveedor,
                        Fecha = r.fecha,
                        ProveedorId = r.proveedor_id,
                        ProveedorNombre = r.MangoProveedores.nombre,
                        PredioId = r.predio_id,
                        PredioNombre = r.MangoPredios.nombre,
                        PropietarioId = r.MangoPredios.propietario_id,
                        PropietarioNombre = r.MangoPredios.MangoPropietarios.nombre,
                        LocalidadId = r.MangoPredios.localidad_id,
                        LocalidadNombre = r.MangoPredios.MangoLocalidades.nombre,
                        FrutaId = r.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Variedades.Frutas.nombre,
                        VariedadId = r.variedad_id,
                        VariedadNombre = r.Variedades.nombre,
                        GuiaSanitaria = r.guia_sanitaria,
                        TicketBascula = r.ticket_bascula,
                        PesoBruto = r.peso_bruto,
                        Tara = r.tara,
                        PesoNeto = r.peso_neto,
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,
                        Placas = r.MangoVehiculoRecepcion.Select(vr => vr.Vehiculos.placas).FirstOrDefault(),
                        GradoId = r.grado_id,
                        CalidadPromedio = r.calidad_promedio,
                        Estado = r.estado,
                        AplicarPorcentajeCosto = r.porcentaje_costo_afecta_peso,
                        PorcentajeCosto = r.porcentaje_costo
                    })
                    .OrderBy(x => x.Fecha)
                    .ToList();
            }

            foreach (MangoRecepcionesViewModel _registro in _registros)
            {
                _registro.VehiculoInfo = _repoVehiculos.ObtenerVehiculoMangoRecepcionPorRecepcionId(_registro.RecepcionId);
                _registro.Descuentos = _repoDescuentos.ObtenerPorRecepcionId(_registro.RecepcionId);

                _registro.CalidadesInfo = _repoCalidades.ListarCalidadesPorRecepcionId(_registro.RecepcionId);
                _registro.CalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoInfo = _repoCondiciones.ListarMangoCondicionesPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoActivas = _repoCondiciones.ListarMangoCondicionesActivasSinValorPorRecepcionId(_registro.RecepcionId);
            }

            return _registros;
        }

        public List<MangoRecepcionesViewModel> ObtenerRecepcionesPorFecha(DateTime _fecha)
        {
            List<MangoRecepcionesViewModel> _registros = new List<MangoRecepcionesViewModel>();

            using (var _context = new Model.Model())
            {
                _registros =  _context.MangoRecepciones
                    .Where(r => r.estado != "I")
                    .ToList()
                    .Select(r => new MangoRecepcionesViewModel
                    {
                        RecepcionId = r.mango_recepcion_id,
                        CicloId = r.ciclo_id,
                        FolioCiclo = r.folio_ciclo,
                        FolioProveedor = r.folio_proveedor,
                        Fecha = r.fecha,
                        ProveedorId = r.proveedor_id,
                        ProveedorNombre = r.MangoProveedores.nombre,
                        PredioId = r.predio_id,
                        PredioNombre = r.MangoPredios.nombre,
                        PropietarioId = r.MangoPredios.propietario_id,
                        PropietarioNombre = r.MangoPredios.MangoPropietarios.nombre,
                        LocalidadId = r.MangoPredios.localidad_id,
                        LocalidadNombre = r.MangoPredios.MangoLocalidades.nombre,
                        FrutaId = r.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Variedades.Frutas.nombre,
                        VariedadId = r.variedad_id,
                        VariedadNombre = r.Variedades.nombre,
                        GuiaSanitaria = r.guia_sanitaria,
                        TicketBascula = r.ticket_bascula,
                        PesoBruto = r.peso_bruto,
                        Tara = r.tara,
                        PesoNeto = r.peso_neto,
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,
                        Placas = r.MangoVehiculoRecepcion.Select(vr => vr.Vehiculos.placas).FirstOrDefault(),
                        GradoId = r.grado_id,
                        CalidadPromedio = r.calidad_promedio,
                        Estado = r.estado,
                        AplicarPorcentajeCosto = r.porcentaje_costo_afecta_peso,
                        PorcentajeCosto = r.porcentaje_costo
                    })
                    .Where(r => r.Fecha.Date == _fecha.Date)
                    .OrderBy(x => x.Fecha)
                    .ToList();
            }

            foreach(MangoRecepcionesViewModel _registro in _registros)
            {
                _registro.VehiculoInfo = _repoVehiculos.ObtenerVehiculoMangoRecepcionPorRecepcionId(_registro.RecepcionId);
                if(_registro.VehiculoInfo == null)
                {
                    throw new Exception("La recepción con ID " + _registro.RecepcionId + " no tiene vehículo asociado.");
                }
                _registro.Descuentos = _repoDescuentos.ObtenerPorRecepcionId(_registro.RecepcionId);

                _registro.CalidadesInfo = _repoCalidades.ListarCalidadesPorRecepcionId(_registro.RecepcionId);
                _registro.CalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoInfo = _repoCondiciones.ListarMangoCondicionesPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoActivas = _repoCondiciones.ListarMangoCondicionesActivasSinValorPorRecepcionId(_registro.RecepcionId);
            }

            return _registros;
        }

        public List<MangoRecepcionesViewModel> ObtenerRecepcionesPorRangoFechas(DateTime _fechaInicio, DateTime _fechaFin)
        {
            List<MangoRecepcionesViewModel> _registros = new List<MangoRecepcionesViewModel>();

            using (var _context = new Model.Model())
            {
                _registros = _context.MangoRecepciones
                    .Where(r=>r.estado != "I")
                    .ToList()
                    .Select(r => new MangoRecepcionesViewModel
                    {

                        RecepcionId = r.mango_recepcion_id,
                        CicloId = r.ciclo_id,
                        FolioCiclo = r.folio_ciclo,
                        FolioProveedor = r.folio_proveedor,
                        Fecha = r.fecha,
                        ProveedorId = r.proveedor_id,
                        ProveedorNombre = r.MangoProveedores.nombre,
                        PredioId = r.predio_id,
                        PredioNombre = r.MangoPredios.nombre,
                        PropietarioId = r.MangoPredios.propietario_id,
                        PropietarioNombre = r.MangoPredios.MangoPropietarios.nombre,
                        LocalidadId = r.MangoPredios.localidad_id,
                        LocalidadNombre = r.MangoPredios.MangoLocalidades.nombre,
                        FrutaId = r.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Variedades.Frutas.nombre,
                        VariedadId = r.variedad_id,
                        VariedadNombre = r.Variedades.nombre,
                        GuiaSanitaria = r.guia_sanitaria,
                        TicketBascula = r.ticket_bascula,
                        PesoBruto = r.peso_bruto,
                        Tara = r.tara,
                        PesoNeto = r.peso_neto,
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,
                        Placas = r.MangoVehiculoRecepcion.Select(vr => vr.Vehiculos.placas).FirstOrDefault(),
                        GradoId = r.grado_id,
                        CalidadPromedio = r.calidad_promedio,
                        Estado = r.estado,
                        AplicarPorcentajeCosto = r.porcentaje_costo_afecta_peso,
                        PorcentajeCosto = r.porcentaje_costo
                    })
                    .Where(r => r.Fecha.Date >= _fechaInicio.Date && r.Fecha.Date <= _fechaFin.Date)
                    .OrderBy(x => x.Fecha)
                    .ToList();
            }

            foreach (MangoRecepcionesViewModel _registro in _registros)
            {
                _registro.VehiculoInfo = _repoVehiculos.ObtenerVehiculoMangoRecepcionPorRecepcionId(_registro.RecepcionId);
                _registro.Descuentos = _repoDescuentos.ObtenerPorRecepcionId(_registro.RecepcionId);

                _registro.CalidadesInfo = _repoCalidades.ListarCalidadesPorRecepcionId(_registro.RecepcionId);
                _registro.CalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoInfo = _repoCondiciones.ListarMangoCondicionesPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoActivas = _repoCondiciones.ListarMangoCondicionesActivasSinValorPorRecepcionId(_registro.RecepcionId);
            }

            return _registros;
        }

        public int ObtenerFolioCicloIdSiguiente(int _cicloId)
        {
            using (var _context = new Model.Model())
            {
                int? ultimoFolio = _context.MangoRecepciones
                    .OrderByDescending(r => r.folio_ciclo)
                    .ToList()
                    .Where(r => r.ciclo_id == _cicloId)
                    .Select(r => r.folio_ciclo)
                    .FirstOrDefault();
                return (ultimoFolio ?? 0) + 1;
            }
        }
        public int ObtenerFolioProveedorIdSiguiente(int _cicloId, int _proveedorId)
        {
            using (var _context = new Model.Model())
            {
                int? ultimoFolio = _context.MangoRecepciones
                    .OrderByDescending(r => r.folio_proveedor)
                    .ToList()
                    .Where(r => r.ciclo_id == _cicloId && r.proveedor_id == _proveedorId)
                    .Select(r => r.folio_proveedor)
                    .FirstOrDefault();
                return (ultimoFolio ?? 0) + 1;
            }
        }

        public List<MangoRecepciones> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoRecepciones.ToList();
            }
        }

        public async Task<List<MangoRecepcionesViewModel>> ListarPorCiclo(int _cicloId, DateTime _fecha)
        {
            if (_repoCiclos.ObtenerPorId(_cicloId)==null)
            {
                _cicloId = _repoCiclos.ObtenerUltimoCicloActivo();
            }
            
            List<MangoRecepcionesViewModel> _registros = new List<MangoRecepcionesViewModel>();

            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_ReporteMangoRecepcionesPorCicloFecha";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@cicloId", _cicloId));
                        command.Parameters.Add(new SqlParameter("@fecha", _fecha));

                        var table = new DataTable();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            table.Load(reader);
                        }

                        if(table.Rows.Count == 0)
                        {
                            return _registros;
                        }

                        foreach(var row in table.Rows)
                        {
                            var dataRow = row as DataRow;
                            if (dataRow != null)
                            {
                                MangoRecepcionesViewModel registro = new MangoRecepcionesViewModel
                                {
                                    RecepcionId = Convert.ToInt32(dataRow["mango_recepcion_id"]),
                                    CicloId = Convert.ToInt32(dataRow["ciclo_id"]),
                                    FolioCiclo = Convert.ToInt32(dataRow["folio_ciclo"]),
                                    Fecha = Convert.ToDateTime(dataRow["fecha"]),
                                    ProveedorId = Convert.ToInt32(dataRow["proveedor_id"]),
                                    ProveedorNombre = dataRow["proveedor_nombre"].ToString(),
                                    PredioId = Convert.ToInt32(dataRow["predio_id"]),
                                    PredioNombre = dataRow["predio_nombre"].ToString(),
                                    PropietarioId = Convert.ToInt32(dataRow["propietario_id"]),
                                    PropietarioNombre = dataRow["propietario_nombre"].ToString(),
                                    LocalidadId = Convert.ToInt32(dataRow["localidad_id"]),
                                    LocalidadNombre = dataRow["localidad_nombre"].ToString(),
                                    FrutaId = Convert.ToInt32(dataRow["id_fruta"]),
                                    FrutaNombre = dataRow["fruta_nombre"].ToString(),
                                    VariedadId = Convert.ToInt32(dataRow["variedad_id"]),
                                    VariedadNombre = dataRow["variedad_nombre"].ToString(),
                                    GuiaSanitaria = dataRow["guia_sanitaria"].ToString(),
                                    TicketBascula = dataRow["ticket_bascula"].ToString(),
                                    PesoNeto = Convert.ToDecimal(dataRow["peso_neto"]),
                                    CalidadPromedio = Convert.ToInt16(dataRow["calidad_promedio"]),
                                    GradoId = dataRow["grado_id"].ToString(),
                                    Placas = dataRow["placas"].ToString(),
                                    Estado = dataRow["estado"].ToString()
                                };
                                _registros.Add(registro);
                            }
                        }

                        return _registros;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<DataTable> ObteneMuestreosPorFechaAsync(DateTime fecha)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_DetalleMuestreosPorRecepcionesPorDia";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@fecha", fecha));

                        var table = new DataTable();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            table.Load(reader);
                        }

                        return table;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<DataTable> ObteneReporteRecepcionesAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteRecepciones";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                    var table = new DataTable();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                    }

                    return table;
                }
            }
        }

        public async Task<DataTable> ObteneReporteMangoRecepcionesInspeccionVehiculosPorRangoFechasAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteMangoRecepcionesInspeccionVehiculosPorRangoFechas";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicial", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFinal", fechaFin));

                    var table = new DataTable();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                    }

                    return table;
                }
            }
        }
    }
}
