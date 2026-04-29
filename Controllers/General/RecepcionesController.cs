using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Controllers.General
{
    internal class RecepcionesController : ICatalogoCRUD<Recepciones, int>
    {
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly VehiculosController _repoVehiculos = new VehiculosController();
        private readonly CondicionesController _repoCondiciones = new CondicionesController();
        private readonly TarimasController _repoTarimas = new TarimasController();
        private readonly CalidadesController _repoCalidades = new CalidadesController();
        private readonly DescuentosController _repoDescuentos = new DescuentosController();

        public RecepcionesController()
        {
        }

        public void Crear(Recepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Recepciones.Add(entidad);
                _context.SaveChanges();
            }
        }

        public int CrearRecepcionViewModel(RecepcionesViewModel entidad)
        {
            using (var _context = new Model.Model())
            {
                Recepciones nuevaRecepcion = new Recepciones
                {
                    recepcion_id = entidad.RecepcionId,
                    id_folio_diario = entidad.FolioRecepcionDiaria.HasValue ? entidad.FolioRecepcionDiaria.Value : ObtenerFolioDiarioSiguiente(entidad.Fecha),
                    ciclo_id = entidad.CicloId,
                    fecha = entidad.Fecha,
                    agricultor_id = entidad.AgriculorId,
                    sector_id = entidad.SectorId,
                    no_tarimas = entidad.NoTarimas,
                    importe = entidad.Importe,
                    precio_unitario = entidad.PrecioUnitario,
                    grado_id = entidad.GradoId,
                    folio_reception = entidad.RecepcionId.ToString() + "-" + entidad.CicloId.ToString(),
                    estado = entidad.Estado
                };
                AsignarIdSiCero(ref nuevaRecepcion);
                entidad.RecepcionId = nuevaRecepcion.recepcion_id;
                SesionActualHelper.RegistrarSesionCrea(ref nuevaRecepcion);
                _context.Recepciones.Add(nuevaRecepcion);
                _context.SaveChanges();
            }

            if (!string.IsNullOrWhiteSpace(entidad.VehiculoInfo.Placas))
            {
                // Crear vehículo
                VehiculosRecepcionesViewModel vehiculo = _repoVehiculos.ObtenerPorPlacas(entidad.VehiculoInfo.Placas, entidad.AgriculorId);
                if (vehiculo == null)
                {
                    _repoVehiculos.CrearVehiculoRecepcionViewModel(entidad.VehiculoInfo);
                }
                entidad.VehiculoInfo.RecepcionId = entidad.RecepcionId;
                _repoVehiculos.CrearVehiculoRecepcion(entidad.VehiculoInfo);

                vehiculo = _repoVehiculos.ObtenerPorPlacas(entidad.VehiculoInfo.Placas, entidad.AgriculorId);
                VehiculosRecepciones vehiculoRecepcion = _repoVehiculos.ObtenerPorRecepcionIdVehiculoId(entidad.RecepcionId, vehiculo.VehiculoId);

                // Crear condiciones
                foreach (CondicionesVehiculoViewModel condicion in entidad.VehiculoInfo.CondicionesVehiculoInfo)
                {
                    if (!condicion.Valor) continue;
                    condicion.VehiculoRecepcionId = vehiculoRecepcion.vehiculo_recepcion_id;
                    _repoCondiciones.CrearCondicionVehiculoViewModel(condicion);
                }
            }

            // Crear tarimas
            foreach (Tarimas tarimas in entidad.TarimasInfo)
            {
                tarimas.recepcion_id = entidad.RecepcionId;
                _repoTarimas.Crear(tarimas);
            }

            // Crear descuentos
            entidad.Descuentos.recepcion_id = entidad.RecepcionId;
            _repoDescuentos.CrearDescuentoRecepcionViewModel(entidad.Descuentos);

            // Crear calidades
            foreach(CalidadesRecepcionesViewModel calidad in entidad.CalidadesInfo)
            {
                if (calidad.Valor == 0) continue;
                calidad.CalidadRecepcionId = entidad.RecepcionId;
                _repoCalidades.CrearCalidadesRecepcionesViewModel(calidad);
            }

            return entidad.RecepcionId;
        }

        public void Actualizar(Recepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Recepciones.Find(entidad.sector_id);
                if (registro != null)
                {
                    entidad.usuario_crea = registro.usuario_crea;
                    entidad.fecha_crea = registro.fecha_crea;
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void ActualizarRecepcionViewModel(RecepcionesViewModel entidad)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.Recepciones.Find(entidad.RecepcionId);
                if (registro != null)
                {
                    registro.id_folio_diario = entidad.FolioRecepcionDiaria.HasValue ? entidad.FolioRecepcionDiaria.Value : ObtenerFolioDiarioSiguiente(entidad.Fecha);
                    registro.ciclo_id = entidad.CicloId;
                    registro.fecha = entidad.Fecha;
                    registro.agricultor_id = entidad.AgriculorId;
                    registro.sector_id = entidad.SectorId;
                    registro.no_tarimas = entidad.NoTarimas;
                    registro.importe = entidad.Importe;
                    registro.precio_unitario = entidad.PrecioUnitario;
                    registro.grado_id = entidad.GradoId;
                    registro.folio_reception = entidad.RecepcionId.ToString() + "-" + entidad.CicloId.ToString();
                    registro.estado = entidad.Estado;
                    SesionActualHelper.RegistrarUsuarioModifica(ref registro);
                    _context.Entry(registro).CurrentValues.SetValues(registro);
                    _context.SaveChanges();
                }
                else return;
            }

            if (!string.IsNullOrWhiteSpace(entidad.VehiculoInfo.Placas))
            {
                // Crear vehículo
                VehiculosRecepcionesViewModel vehiculo = _repoVehiculos.ObtenerPorPlacas(entidad.VehiculoInfo.Placas, entidad.AgriculorId);
                if (vehiculo == null)
                {
                    _repoVehiculos.CrearVehiculoRecepcionViewModel(entidad.VehiculoInfo);
                }
                vehiculo = _repoVehiculos.ObtenerPorPlacas(entidad.VehiculoInfo.Placas, entidad.AgriculorId);

                VehiculosRecepciones vehiculoRecepcion = _repoVehiculos.ObtenerPorRecepcionIdVehiculoId(entidad.RecepcionId, vehiculo.VehiculoId);

                entidad.VehiculoInfo.RecepcionId = entidad.RecepcionId;
                entidad.VehiculoInfo.VehiculoId = vehiculo.VehiculoId;
                if(vehiculoRecepcion == null)
                {
                    _repoVehiculos.CrearVehiculoRecepcion(entidad.VehiculoInfo);
                    vehiculoRecepcion = _repoVehiculos.ObtenerPorRecepcionIdVehiculoId(entidad.RecepcionId, vehiculo.VehiculoId);

                    entidad.VehiculoInfo.VehiculoRecepcionId = vehiculoRecepcion.vehiculo_recepcion_id;

                }
                else
                {
                    entidad.VehiculoInfo.VehiculoRecepcionId = vehiculoRecepcion.vehiculo_recepcion_id;
                    _repoVehiculos.ActualizarVehiculoRecepcionViewModel(entidad.VehiculoInfo);
                }
                entidad.VehiculoInfo.VehiculoRecepcionId = vehiculoRecepcion == null ? 0 : vehiculoRecepcion.vehiculo_recepcion_id;
                _repoVehiculos.ActualizarVehiculoRecepcionViewModel(entidad.VehiculoInfo);

                // Actualizar condiciones
                List<CondicionesVehiculosRecepciones> _elimarCondiciones = _repoCondiciones.ListarCondicionesVehiculosRecepcionesPorRecepcionId(entidad.RecepcionId);

                if (_elimarCondiciones.Any())
                    foreach (CondicionesVehiculosRecepciones c in _elimarCondiciones)
                    {
                        _repoCondiciones.EliminarCondicionVehiculoRecepcion(c.condicion_vehiculo_recepcion_id);
                    }

                foreach (CondicionesVehiculoViewModel condicion in entidad.VehiculoInfo.CondicionesVehiculoInfo)
                {
                    if (!condicion.Valor) continue;
                    condicion.VehiculoRecepcionId = vehiculoRecepcion.vehiculo_recepcion_id;
                    _repoCondiciones.CrearCondicionVehiculoViewModel(condicion);
                }
            }
            
            //Actualizar Tarimas tanto en bd como en registro
            List<Tarimas> _bdTarimas = _repoTarimas.ListarPorRecepcion(entidad.RecepcionId);

            var _tarimasLocales = entidad.TarimasInfo.Where(t => t.tarima_id > 0).Select(t => t.tarima_id).ToList();
            List<Tarimas> _eliminarTarimas = _bdTarimas.Where(t => !_tarimasLocales.Contains(t.tarima_id)).ToList();

            if (_eliminarTarimas.Any())
                foreach (Tarimas t in _eliminarTarimas)
                {
                    _repoTarimas.Eliminar(t.tarima_id);
                }

            foreach (Tarimas t in entidad.TarimasInfo)
            {
                t.recepcion_id = entidad.RecepcionId;
                if (t.tarima_id != 0)
                    _repoTarimas.Actualizar(t);
                else
                    _repoTarimas.Crear(t);
            }

            // Actualizar descuentos
            DescuentosRecepciones descuento = _repoDescuentos.ObtenerPorRecepcionId(entidad.RecepcionId);
            entidad.Descuentos.recepcion_id = entidad.RecepcionId;
            entidad.Descuentos.descuento_recepcion_id = descuento == null ? 0 : descuento.descuento_recepcion_id;
            if (descuento != null)
                _repoDescuentos.Actualizar(entidad.Descuentos);
            else
                _repoDescuentos.CrearDescuentoRecepcionViewModel(entidad.Descuentos);

            // Actualizar calidades
            List<CalidadesRecepciones> _eliminarCalidades = _repoCalidades.ListarCalidadesRecepcionesPorRecepcionId(entidad.RecepcionId);

            if (_eliminarCalidades.Any())
                foreach (CalidadesRecepciones c in _eliminarCalidades)
                {
                    _repoCalidades.EliminarCalidadRecepcion(c.calidad_recepcion_id);
                }
            foreach (CalidadesRecepcionesViewModel calidad in entidad.CalidadesInfo)
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
                var registro = _context.Recepciones.Find(id);
                if (registro != null)
                {
                    _context.Recepciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Recepciones ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Recepciones.Find(id);
            }
        }

        public RecepcionesViewModel ObtenerRecepcionPorId( int _idRecepcion)
        {
            RecepcionesViewModel registro = new RecepcionesViewModel();
            using (var _context = new Model.Model())
            {
                registro = _context.Recepciones
                    .Where(r => r.recepcion_id == _idRecepcion)
                    .Select(r => new RecepcionesViewModel
                    {
                        RecepcionId = r.recepcion_id,
                        FolioRecepcionDiaria = r.id_folio_diario,
                        CicloId = r.ciclo_id,
                        Fecha = r.fecha,
                        AgriculorId = r.agricultor_id,
                        AgricultorNombre = r.Agricultores.nombre,
                        RanchoId = r.Sectores.Ranchos.rancho_id,
                        RanchoNombre = r.Sectores.Ranchos.nombre,
                        SectorId = r.sector_id,
                        SectorNombre = r.Sectores.nombre,
                        VariedadId = r.Sectores.Variedades.variedad_id,
                        VariedadNombre = r.Sectores.Variedades.nombre,
                        FrutaId = r.Sectores.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Sectores.Variedades.Frutas.nombre,
                        NoTarimas = r.no_tarimas,
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,
                        Placas = r.VehiculosRecepciones.Select(vr => vr.Vehiculos.placas).FirstOrDefault(),
                        GradoId = r.grado_id,
                        Folio = r.folio_reception,
                        Estado = r.estado
                    })
                    .FirstOrDefault();
            }

            if(registro != null)
            {
                registro.VehiculoInfo = _repoVehiculos.ObtenerVehiculoRecepcionPorRecepcionId(registro.RecepcionId);
                registro.Descuentos = _repoDescuentos.ObtenerPorRecepcionId(_idRecepcion);

                registro.TarimasInfo = _repoTarimas.ListarPorRecepcion(_idRecepcion);
                registro.CalidadesInfo = _repoCalidades.ListarCalidadesPorRecepcionId(_idRecepcion);
                registro.CalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(_idRecepcion);
                
                if(registro.VehiculoInfo != null)
                { 
                    registro.VehiculoInfo.CondicionesVehiculoInfo = _repoCondiciones.ListarCondicionesPorRecepcionId(_idRecepcion);
                    registro.VehiculoInfo.CondicionesVehiculoActivas = _repoCondiciones.ListarCondicionesActivasSinValorPorRecepcionId(_idRecepcion);
                }
            }

            return registro;
        }

        public List<RecepcionesViewModel> ObtenerRecepcionesPorCicloIdAgricultorId(int _cicloId, int _agricultorId)
        {
            List<RecepcionesViewModel> _registros = new List<RecepcionesViewModel>();

            using (var _context = new Model.Model())
            {
                _registros = _context.Recepciones
                    .Where(r => r.estado != "I" && r.ciclo_id == _cicloId && r.agricultor_id == _agricultorId)
                    .Select(r => new RecepcionesViewModel
                    {
                        RecepcionId = r.recepcion_id,
                        FolioRecepcionDiaria = r.id_folio_diario,
                        CicloId = r.ciclo_id,
                        Fecha = r.fecha,
                        AgriculorId = r.agricultor_id,
                        AgricultorNombre = r.Agricultores.nombre,
                        RanchoId = r.Sectores.Ranchos.rancho_id,
                        RanchoNombre = r.Sectores.Ranchos.nombre,
                        SectorId = r.sector_id,
                        SectorNombre = r.Sectores.nombre,
                        VariedadId = r.Sectores.Variedades.variedad_id,
                        VariedadNombre = r.Sectores.Variedades.nombre,
                        FrutaId = r.Sectores.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Sectores.Variedades.Frutas.nombre,
                        NoTarimas = r.no_tarimas,
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,
                        Placas = r.VehiculosRecepciones.Any() ? r.VehiculosRecepciones.FirstOrDefault().Vehiculos.placas : string.Empty,
                        GradoId = r.grado_id,
                        Folio = r.folio_reception,
                        Estado = r.estado,
                        Semana = SqlFunctions.DatePart("week", r.fecha),
                        Anio = SqlFunctions.DatePart("year", r.fecha)
                    })
                    .OrderBy(x => x.Fecha)
                    .ToList();
            }

            foreach (RecepcionesViewModel _registro in _registros)
            {
                _registro.VehiculoInfo = _repoVehiculos.ObtenerVehiculoRecepcionPorRecepcionId(_registro.RecepcionId);
                _registro.Descuentos = _repoDescuentos.ObtenerPorRecepcionId(_registro.RecepcionId);

                _registro.TarimasInfo = _repoTarimas.ListarPorRecepcion(_registro.RecepcionId);
                _registro.CalidadesInfo = _repoCalidades.ListarCalidadesPorRecepcionId(_registro.RecepcionId);
                _registro.CalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoInfo = _repoCondiciones.ListarCondicionesPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoActivas = _repoCondiciones.ListarCondicionesActivasSinValorPorRecepcionId(_registro.RecepcionId);
            }

            return _registros;
        }

        public List<RecepcionesViewModel> ObtenerRecepcionesPorFecha(DateTime _fecha)
        {
            List<RecepcionesViewModel> _registros = new List<RecepcionesViewModel>();

            using (var _context = new Model.Model())
            {
                _registros =  _context.Recepciones
                    .Where(r => r.estado != "I")
                    .ToList()
                    .Select(r => new RecepcionesViewModel
                    {
                        RecepcionId = r.recepcion_id,
                        FolioRecepcionDiaria = r.id_folio_diario,
                        CicloId = r.ciclo_id,
                        Fecha = r.fecha,
                        AgriculorId = r.agricultor_id,
                        AgricultorNombre = r.Agricultores.nombre,
                        RanchoId = r.Sectores.Ranchos.rancho_id,
                        RanchoNombre = r.Sectores.Ranchos.nombre,
                        SectorId = r.sector_id,
                        SectorNombre = r.Sectores.nombre,
                        VariedadId = r.Sectores.Variedades.variedad_id,
                        VariedadNombre = r.Sectores.Variedades.nombre,
                        FrutaId = r.Sectores.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Sectores.Variedades.Frutas.nombre,
                        NoTarimas = r.no_tarimas,
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,
                        Placas = r.VehiculosRecepciones.Any() ? r.VehiculosRecepciones.FirstOrDefault().Vehiculos.placas : string.Empty,
                        GradoId = r.grado_id,
                        Folio = r.folio_reception,
                        Estado = r.estado
                    })
                    .Where(r => r.Fecha.Date == _fecha.Date)
                    .OrderBy(x => x.Fecha)
                    .ToList();
            }

            foreach(RecepcionesViewModel _registro in _registros)
            {
                _registro.VehiculoInfo = _repoVehiculos.ObtenerVehiculoRecepcionPorRecepcionId(_registro.RecepcionId);
                if(_registro.VehiculoInfo == null)
                {
                    throw new Exception("La recepción con ID " + _registro.RecepcionId + " no tiene vehículo asociado.");
                }
                _registro.Descuentos = _repoDescuentos.ObtenerPorRecepcionId(_registro.RecepcionId);

                _registro.TarimasInfo = _repoTarimas.ListarPorRecepcion(_registro.RecepcionId);
                _registro.CalidadesInfo = _repoCalidades.ListarCalidadesPorRecepcionId(_registro.RecepcionId);
                _registro.CalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoInfo = _repoCondiciones.ListarCondicionesPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoActivas = _repoCondiciones.ListarCondicionesActivasSinValorPorRecepcionId(_registro.RecepcionId);
            }

            return _registros;
        }

        public List<RecepcionesViewModel> ObtenerRecepcionesPorRangoFechas(DateTime _fechaInicio, DateTime _fechaFin)
        {
            List<RecepcionesViewModel> _registros = new List<RecepcionesViewModel>();

            using (var _context = new Model.Model())
            {
                _registros = _context.Recepciones
                    .Where(r=>r.estado != "I")
                    .ToList()
                    .Select(r => new RecepcionesViewModel
                    {
                        RecepcionId = r.recepcion_id,
                        FolioRecepcionDiaria = r.id_folio_diario,
                        CicloId = r.ciclo_id,
                        Fecha = r.fecha,
                        AgriculorId = r.agricultor_id,
                        AgricultorNombre = r.Agricultores.nombre,
                        RanchoId = r.Sectores.Ranchos.rancho_id,
                        RanchoNombre = r.Sectores.Ranchos.nombre,
                        SectorId = r.sector_id,
                        SectorNombre = r.Sectores.nombre,
                        VariedadId = r.Sectores.Variedades.variedad_id,
                        VariedadNombre = r.Sectores.Variedades.nombre,
                        FrutaId = r.Sectores.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Sectores.Variedades.Frutas.nombre,
                        NoTarimas = r.no_tarimas,
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,
                        Placas = r.VehiculosRecepciones.Any() ? r.VehiculosRecepciones.FirstOrDefault().Vehiculos.placas : string.Empty,
                        GradoId = r.grado_id,
                        Folio = r.folio_reception,
                        Estado = r.estado
                    })
                    .Where(r => r.Fecha.Date >= _fechaInicio.Date && r.Fecha.Date <= _fechaFin.Date)
                    .OrderBy(x => x.Fecha)
                    .ToList();
            }

            foreach (RecepcionesViewModel _registro in _registros)
            {
                _registro.VehiculoInfo = _repoVehiculos.ObtenerVehiculoRecepcionPorRecepcionId(_registro.RecepcionId);
                _registro.Descuentos = _repoDescuentos.ObtenerPorRecepcionId(_registro.RecepcionId);

                _registro.TarimasInfo = _repoTarimas.ListarPorRecepcion(_registro.RecepcionId);
                _registro.CalidadesInfo = _repoCalidades.ListarCalidadesPorRecepcionId(_registro.RecepcionId);
                _registro.CalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoInfo = _repoCondiciones.ListarCondicionesPorRecepcionId(_registro.RecepcionId);
                _registro.VehiculoInfo.CondicionesVehiculoActivas = _repoCondiciones.ListarCondicionesActivasSinValorPorRecepcionId(_registro.RecepcionId);
            }

            return _registros;
        }

        public int ObtenerFolioDiarioSiguiente(DateTime _fecha)
        {
            using (var _context = new Model.Model())
            {
                int? ultimoFolio = _context.Recepciones
                    .OrderByDescending(r => r.id_folio_diario)
                    .ToList()
                    .Where(r => r.fecha.Date == _fecha.Date)
                    .Select(r => r.id_folio_diario)
                    .FirstOrDefault();
                return (ultimoFolio ?? 0) + 1;
            }
        }
        
        public List<Recepciones> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Recepciones.ToList();
            }
        }

        public List<RecepcionesViewModel> ListarPorCiclo(int _cicloId, DateTime _fecha)
        {
            if (_repoCiclos.ObtenerPorId(_cicloId)==null)
            {
                _cicloId = _repoCiclos.ObtenerUltimoCicloActivo();
            }
            using (var _context = new Model.Model())
            {
                return _context.Recepciones
                    .Where(r => r.ciclo_id == _cicloId)
                    .Select(r => new RecepcionesViewModel
                    {
                        RecepcionId = r.recepcion_id,
                        FolioRecepcionDiaria = r.id_folio_diario,
                        CicloId = r.ciclo_id,
                        Fecha = r.fecha,
                        AgriculorId = r.agricultor_id,
                        AgricultorNombre = r.Agricultores.nombre,

                        RanchoId = r.Sectores.Ranchos.rancho_id,
                        RanchoNombre = r.Sectores.Ranchos.nombre,

                        SectorId = r.sector_id,
                        SectorNombre = r.Sectores.nombre,

                        VariedadId = r.Sectores.Variedades.variedad_id,
                        VariedadNombre = r.Sectores.Variedades.nombre,

                        FrutaId = r.Sectores.Variedades.Frutas.fruta_id,
                        FrutaNombre = r.Sectores.Variedades.Frutas.nombre,

                        NoTarimas = r.Tarimas.Count(),
                        PesoNeto = r.Tarimas.Count() == 0 ? 0 : r.Tarimas.Sum(t => t.peso_neto),
                        Importe = r.importe,
                        PrecioUnitario = r.precio_unitario,

                        Placas = r.VehiculosRecepciones.Any() ? r.VehiculosRecepciones.FirstOrDefault().Vehiculos.placas : string.Empty,

                        GradoId = r.grado_id,
                        Folio = r.folio_reception,
                        Estado = r.estado,

                    })
                    .ToList()
                    .Where(r => r.Fecha.Date == _fecha.Date)
                    .OrderByDescending(x => x.Fecha)
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Recepciones entidad)
        {
            if (entidad.recepcion_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Recepciones
                                      .OrderByDescending(p => p.recepcion_id)
                                      .Select(p => p.recepcion_id)
                                      .FirstOrDefault();

                    entidad.recepcion_id = ultimoId + 1;
                }
                ;
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
    }
}
