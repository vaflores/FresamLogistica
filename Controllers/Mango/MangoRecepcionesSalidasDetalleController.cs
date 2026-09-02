using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels.Mango;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Mango
{
    internal class MangoRecepcionesSalidasDetalleController : ICatalogoCRUD<MangoRecepcionesSalidasDetalle, int>
    {
        public void Crear(MangoRecepcionesSalidasDetalle entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoRecepcionesSalidasDetalle.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoRecepcionesSalidasDetalle entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoRecepcionesSalidasDetalle.Find(Convert.ToInt16(entidad.mango_recepcion_salida_detalle_id));
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
                var registro = _context.MangoRecepcionesSalidasDetalle.Find(id);
                if (registro != null)
                {
                    _context.MangoRecepcionesSalidasDetalle.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoRecepcionesSalidasDetalle ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoRecepcionesSalidasDetalle.Find(id);
            }
        }

        public List<MangoRecepcionesSalidasDetalle> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoRecepcionesSalidasDetalle.ToList();
            }
        }

        public int Crear(MangoRecepcionesSalidas entidad, List<MangoRecepcionSalidaDetalleViewModel> salidas, List<MangoRecepcionesSalidasTarimas> tarimas)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoRecepcionesSalidas.Add(entidad);
                _context.SaveChanges();
                
                var existentes = _context.MangoRecepcionesSalidasDetalle.Where(kcc => kcc.mango_recepcion_salida_id == entidad.mango_recepcion_salida_id).ToList();
                _context.MangoRecepcionesSalidasDetalle.RemoveRange(existentes);
                _context.SaveChanges();
                foreach (var salida in salidas)
                {
                    var nuevaSalida = new MangoRecepcionesSalidasDetalle
                    {
                        mango_recepcion_salida_id = entidad.mango_recepcion_salida_id,
                        mango_tipo_salida_id = salida.MangoTipoSalidaId,
                        peso = salida.Peso,
                        observaciones = ""
                    };
                    SesionActualHelper.RegistrarSesionCrea(ref nuevaSalida);
                    _context.MangoRecepcionesSalidasDetalle.Add(nuevaSalida);
                }
                _context.SaveChanges();

                //Crear nuevas tarimas
                var tarimasExistentes = _context.MangoRecepcionesSalidasTarimas.Where(kcc => kcc.mango_recepcion_salida_id == entidad.mango_recepcion_salida_id).ToList();
                _context.MangoRecepcionesSalidasTarimas.RemoveRange(tarimasExistentes);
                _context.SaveChanges();

                foreach (var tarima in tarimas)
                {
                    var nuevaTarima = new MangoRecepcionesSalidasTarimas
                    {
                        mango_recepcion_salida_id = entidad.mango_recepcion_salida_id,
                        numero_tarima = ObtenerSiguienteNumeroTarimaPorDia(entidad.fecha),
                        cantidad_cajas = tarima.cantidad_cajas,
                        peso_neto = tarima.peso_neto,
                        aprobada = tarima.aprobada,
                    };
                    SesionActualHelper.RegistrarSesionCrea(ref nuevaTarima);
                    _context.MangoRecepcionesSalidasTarimas.Add(nuevaTarima);

                    _context.SaveChanges();
                }

                return entidad.mango_recepcion_salida_id;
            }
        }

        public void Actualizar(MangoRecepcionesSalidas entidad, List<MangoRecepcionSalidaDetalleViewModel> salidas, List<MangoRecepcionesSalidasTarimas> tarimas)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoRecepcionesSalidas.Find(Convert.ToInt16(entidad.mango_recepcion_salida_id));
                entidad.usuario_crea = registro.usuario_crea;
                entidad.fecha_crea = registro.fecha_crea;
                if (registro != null)
                {
                    entidad.consecutivo = registro.consecutivo;
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();

                    var existentes = _context.MangoRecepcionesSalidasDetalle.Where(kcc => kcc.mango_recepcion_salida_id == entidad.mango_recepcion_salida_id).ToList();
                    _context.MangoRecepcionesSalidasDetalle.RemoveRange(existentes);
                    _context.SaveChanges();
                    foreach (var salida in salidas)
                    {
                        var nuevaSalida = new MangoRecepcionesSalidasDetalle
                        {
                            mango_recepcion_salida_id = entidad.mango_recepcion_salida_id,
                            mango_tipo_salida_id = salida.MangoTipoSalidaId,
                            peso = salida.Peso,
                            observaciones = ""
                        };
                        SesionActualHelper.RegistrarSesionCrea(ref nuevaSalida);
                        _context.MangoRecepcionesSalidasDetalle.Add(nuevaSalida);
                    }
                    _context.SaveChanges();

                    //Actualizar tarimas existentes y eliminar las que no estén en la lista
                    var tarimasExistentes = _context.MangoRecepcionesSalidasTarimas.Where(kcc => kcc.mango_recepcion_salida_id == entidad.mango_recepcion_salida_id).ToList();
                    var tarimasIds = tarimas.Select(t => t.mango_recepcion_salida_tarima_id).ToList();
                    var tarimasAEliminar = tarimasExistentes.Where(t => !tarimasIds.Contains(t.mango_recepcion_salida_tarima_id)).ToList();
                    _context.MangoRecepcionesSalidasTarimas.RemoveRange(tarimasAEliminar);
                    _context.SaveChanges();

                    foreach (var tarima in tarimas)
                    {
                        var tarimaExistente = _context.MangoRecepcionesSalidasTarimas.Find(tarima.mango_recepcion_salida_tarima_id);
                        if (tarimaExistente != null)
                        {
                            tarimaExistente.cantidad_cajas = tarima.cantidad_cajas;
                            tarimaExistente.peso_neto = tarima.peso_neto;
                            if (tarima.numero_tarima == 0)
                                tarimaExistente.numero_tarima = ObtenerSiguienteNumeroTarimaPorDia(entidad.fecha);
                            SesionActualHelper.RegistrarUsuarioModifica(ref tarimaExistente);
                            if(tarima.cancelada != tarimaExistente.cancelada)
                            {
                                tarimaExistente.cancelada = tarima.cancelada;
                                tarimaExistente.fecha_cancela = tarimaExistente.fecha_modifica;
                                tarimaExistente.usuario_cancela = tarimaExistente.usuario_modifica;
                            }
                            //_context.Entry(tarimaExistente).CurrentValues.SetValues(tarima);
                        }
                        else
                        {
                            if (tarima.numero_tarima == 0)
                                tarima.numero_tarima = ObtenerSiguienteNumeroTarimaPorDia(entidad.fecha);
                            var nuevaTarima = new MangoRecepcionesSalidasTarimas
                            {
                                mango_recepcion_salida_id = entidad.mango_recepcion_salida_id,
                                numero_tarima = tarima.numero_tarima,
                                cantidad_cajas = tarima.cantidad_cajas,
                                peso_neto = tarima.peso_neto,
                                aprobada = tarima.aprobada,
                            };
                            SesionActualHelper.RegistrarSesionCrea(ref nuevaTarima);
                            _context.MangoRecepcionesSalidasTarimas.Add(nuevaTarima);
                        }
                        _context.SaveChanges();
                    }
                }
            }
        }

        public List<MangoRecepcionSalidaDetalleViewModel> ListarTiposSalida(int mangoRecepcionSalidaId)
        {
            using (var _context = new Model.Model())
            {
                var query =
                    from mts in _context.MangoTiposSalidas

                    join mrsd in _context.MangoRecepcionesSalidasDetalle
                        .Where(x => x.mango_recepcion_salida_id == mangoRecepcionSalidaId)
                    on mts.mango_tipo_salida_id equals mrsd.mango_tipo_salida_id
                    into detalle

                    from mrsd in detalle.DefaultIfEmpty()

                    where mts.estado == "A"

                    orderby mts.orden

                    select new MangoRecepcionSalidaDetalleViewModel
                    {
                        MangoRecepcionSalidaDetalleId = mrsd == null
                            ? 0
                            : mrsd.mango_recepcion_salida_detalle_id,

                        MangoRecepcionSalidaId = mrsd == null
                            ? mangoRecepcionSalidaId
                            : mrsd.mango_recepcion_salida_id,

                        MangoTipoSalidaId = mts.mango_tipo_salida_id,

                        TipoSalida = mts.nombre,

                        Peso = mrsd == null
                            ? 0
                            : mrsd.peso,

                        Observaciones = mrsd == null
                            ? ""
                            : mrsd.observaciones
                    };

                return query.ToList();
            }
        }

        public List<MangoRecepcionesSalidasTarimas> ListarTarimas(int mangoRecepcionSalidaId)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoRecepcionesSalidasTarimas
                    .Where(t => t.mango_recepcion_salida_id == mangoRecepcionSalidaId)
                    .ToList();
            }
        }

        public int ObtenerSiguienteNumeroTarimaPorDia(DateTime fecha)
        {
            using (var _context = new Model.Model())
            {
                var fechaInicio = fecha.Date;
                var fechaFin = fecha.Date.AddDays(1).AddTicks(-1);
                var maxNumeroTarima = _context.MangoRecepcionesSalidasTarimas
                    .Where(t => t.MangoRecepcionesSalidas.fecha >= fechaInicio && t.MangoRecepcionesSalidas.fecha <= fechaFin)
                    .Max(t => (int?)t.numero_tarima) ?? 0;
                return maxNumeroTarima + 1;
            }
        }
    }
}
