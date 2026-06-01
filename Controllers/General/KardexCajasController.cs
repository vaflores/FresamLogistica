using FresamLogistica.Helper;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class KardexCajasController
    {
        public KardexCajasController()
        {
        }

        public void ConfigurarPrecioCaja(ConfiguracionGeneralCajas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.ConfiguracionGeneralCajas.Add(entidad);
                _context.SaveChanges();
            }
        }

        public decimal ObtenerPrecioCaja()
        {
            using (var _context = new Model.Model())
            {
                var configuracion = _context.ConfiguracionGeneralCajas.FirstOrDefault();
                return configuracion != null ? configuracion.costo_caja : 0;
            }
        }

        public void ActualizarPrecioCaja(ConfiguracionGeneralCajas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.ConfiguracionGeneralCajas.Find(entidad.configuracion_id);
                if (registro != null)
                {
                    entidad.usuario_crea = registro.usuario_crea;
                    entidad.fecha_crea = registro.fecha_crea;
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public ConfiguracionGeneralCajas ObtenerConfiguracionCaja(int configuracionId)
        {
            using (var _context = new Model.Model())
            {
                return _context.ConfiguracionGeneralCajas.Find(configuracionId);
            }
        }

        public bool ExisteConfiguracionCaja()
        {
            using (var _context = new Model.Model())
            {
                return _context.ConfiguracionGeneralCajas.Any();
            }
        }

        public void ActualizarSaldoCajasAgricultor(int agricultorId, DateTime fecha, decimal cantidad, bool esIngreso)
        {
            using (var _context = new Model.Model())
            {
                var saldo = _context.AgricultorCajasSaldo.FirstOrDefault(s => s.agricultor_id == agricultorId);
                if (saldo != null)
                {
                    saldo.total_cajas += esIngreso ? -cantidad : cantidad;
                    saldo.fecha_actualizacion = fecha;
                    SesionActualHelper.RegistrarUsuarioModifica(ref saldo);
                    _context.SaveChanges();
                }
                else if (!esIngreso)
                {
                    var nuevoSaldo = new AgricultorCajasSaldo
                    {
                        agricultor_id = agricultorId,
                        total_cajas = cantidad,
                        fecha_actualizacion = fecha
                    };
                    SesionActualHelper.RegistrarSesionCrea(ref nuevoSaldo);
                    _context.AgricultorCajasSaldo.Add(nuevoSaldo);
                    _context.SaveChanges();
                }
            }
        }

        public int AgregarMovimientoKardex(int agricultorId, DateTime fecha, decimal cantidad, bool esIngreso, string observaciones = "")
        {
            int movimientoId;
            using (var _context = new Model.Model())
            {
                var movimiento = new KardexCajas
                {
                    agricultor_id = agricultorId,
                    fecha = fecha,
                    tipo_movimiento = esIngreso ? "Recepción" : "Entrega",
                    entrada = esIngreso ? cantidad : 0,
                    salida = esIngreso ? 0 : cantidad,
                    saldo_anterior = _context.AgricultorCajasSaldo.Where(s => s.agricultor_id == agricultorId).Select(s => s.total_cajas).FirstOrDefault(),
                    saldo_actual = _context.AgricultorCajasSaldo.Where(s => s.agricultor_id == agricultorId).Select(s => s.total_cajas).FirstOrDefault() + (esIngreso ? -cantidad : cantidad),
                    observaciones = observaciones
                };
                SesionActualHelper.RegistrarSesionCrea(ref movimiento);
                _context.KardexCajas.Add(movimiento);
                _context.SaveChanges();
                movimientoId = movimiento.kardex_id;
            }
            
            ActualizarSaldoCajasAgricultor(agricultorId, fecha, cantidad, esIngreso);
            return movimientoId;
        }

        public List<KardexCajas> ObtenerMovimientosKardex(int agricultorId)
        {
            using (var _context = new Model.Model())
            {
                return _context.KardexCajas.Where(k => k.agricultor_id == agricultorId).OrderByDescending(k => k.fecha).ToList();
            }
        }

        public decimal ObtenerSaldoActualCajas(int agricultorId)
        {
            using (var _context = new Model.Model())
            {
                return _context.AgricultorCajasSaldo.Where(s => s.agricultor_id == agricultorId).Select(s => s.total_cajas).FirstOrDefault();
            }
        }

        public KardexCajas ObtenerMovimientoKardexPorId(int movimientoId)
        {
            using (var _context = new Model.Model())
            {
                return _context.KardexCajas.Find(movimientoId);
            }
        }
    }

}
