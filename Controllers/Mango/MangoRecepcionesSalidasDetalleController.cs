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

        public void Crear(MangoRecepcionesSalidas entidad, List<MangoRecepcionSalidaDetalleViewModel> salidas)
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
            }
        }

        public void Actualizar(MangoRecepcionesSalidas entidad, List<MangoRecepcionSalidaDetalleViewModel> salidas)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoRecepcionesSalidas.Find(Convert.ToInt16(entidad.mango_recepcion_salida_id));
                entidad.usuario_crea = registro.usuario_crea;
                entidad.fecha_crea = registro.fecha_crea;
                if (registro != null)
                {
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


    }
}
