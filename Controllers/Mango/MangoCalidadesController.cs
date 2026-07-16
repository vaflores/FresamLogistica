using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Mango
{
    internal class MangoCalidadesController : ICatalogoCRUD<MangoCalidades, int>
    {

        public MangoCalidadesController()
        {
        }

        public void Crear(MangoCalidades entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoCalidades.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void CrearCalidadesRecepcionesViewModel(CalidadesRecepcionesViewModel calidad)
        {
            using (var _context = new Model.Model())
            {
                var entidad = new MangoCalidadesRecepcion
                {
                    calidad_recepcion_id = 0, // Asumiendo que es autogenerado
                    mango_recepcion_id = calidad.CalidadRecepcionId,
                    calidad_id = calidad.CalidadId,
                    valor = calidad.Valor
                };
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoCalidadesRecepcion.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoCalidades entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoCalidades.Find(entidad.calidad_id);
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
                var registro = _context.MangoCalidades.Find(id);
                if (registro != null)
                {
                    _context.MangoCalidades.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarCalidadRecepcion(int _id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MangoCalidadesRecepcion.Find(_id);
                if (registro != null)
                {
                    _context.MangoCalidadesRecepcion.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoCalidades ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoCalidades.Find(id);
            }
        }

        public List<MangoCalidades> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoCalidades.ToList();
            }
        }

        public List<CalidadesRecepcionesViewModel> ListarCalidadesPorRecepcionId(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                var query = from cr in _context.MangoCalidadesRecepcion
                            join c in _context.MangoCalidades on cr.calidad_id equals c.calidad_id
                            where cr.mango_recepcion_id == _recepcionId
                            select new CalidadesRecepcionesViewModel
                            {
                                CalidadRecepcionId = cr.calidad_recepcion_id,
                                CalidadId = c.calidad_id,
                                Nombre = c.nombre,
                                Valor = cr.valor,
                                AfectaCalificacion = c.afecta_calificacion,
                                IncluyeEnReporte = c.visible_reporte
                            };
                return query.ToList();
            }
        }

        public List<MangoCalidadesRecepcion> ListarCalidadesRecepcionesPorRecepcionId(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoCalidadesRecepcion
                               .Where(cr => cr.mango_recepcion_id == _recepcionId)
                               .ToList();
            }
        }

        public List<CalidadesRecepcionesViewModel> ListarCalidadesActivasSinValorPorRecepcionId(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                var query = from c in _context.MangoCalidades
                            where c.estado == "A"
                                  && !_context.MangoCalidadesRecepcion
                                              .Where(cr => cr.mango_recepcion_id == _recepcionId)
                                              .Select(cr => cr.calidad_id)
                                              .Contains(c.calidad_id)
                            select new CalidadesRecepcionesViewModel
                            {
                                CalidadRecepcionId = 0,
                                CalidadId = c.calidad_id,
                                Nombre = c.nombre,
                                Valor = 0,
                                AfectaCalificacion = c.afecta_calificacion,
                                IncluyeEnReporte = c.visible_reporte
                            };
                return query.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarCombo()
        {
            using (var _context = new Model.Model())
            {
                var temp = _context.MangoCalidades
                    .Select(r => new { r.calidad_id, r.nombre }) // traducible a SQL
                    .ToList(); // materializa la consulta

                return temp
                    .Select(x => new KeyValuePair<string, string>(x.calidad_id.ToString(), x.nombre))
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref MangoCalidades entidad)
        {
            if (entidad.calidad_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.MangoCalidades
                                      .OrderByDescending(p => p.calidad_id)
                                      .Select(p => p.calidad_id)
                                      .FirstOrDefault();

                    entidad.calidad_id = ultimoId + 1;
                }
                ;
            }
        }

        private void AsignarIdSiCero(ref MangoCalidadesRecepcion entidad)
        {
            if (entidad.calidad_recepcion_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.MangoCalidadesRecepcion
                                      .OrderByDescending(p => p.calidad_recepcion_id)
                                      .Select(p => p.calidad_recepcion_id)
                                      .FirstOrDefault();
                    entidad.calidad_recepcion_id = ultimoId + 1;
                }
                ;
            }
        }

        public List<CalidadesRecepcionesViewModel> ListarCalidadesPorMuestreoId(int _muestreoId)
        {
            using (var _context = new Model.Model())
            {
                var query = from mcr in _context.MangoMuestreosCalidades
                            join c in _context.MangoCalidades on mcr.calidad_id equals c.calidad_id
                            where mcr.muestreo_id == _muestreoId
                            select new CalidadesRecepcionesViewModel
                            {
                                CalidadRecepcionId = mcr.muestreo_id,
                                CalidadId = c.calidad_id,
                                Nombre = c.nombre,
                                Valor = mcr.valor,
                                AfectaCalificacion = c.afecta_calificacion,
                                IncluyeEnReporte = c.visible_reporte
                            };
                return query.ToList();
            }
        }

        public List<CalidadesRecepcionesViewModel> ListarCalidadesActivasSinValorPorMuestreoId(int _muestreoId)
        {
            using (var _context = new Model.Model())
            {
                var query = from c in _context.MangoCalidades
                            where c.estado == "A"
                                  && !_context.MangoMuestreosCalidades
                                              .Where(mcr => mcr.muestreo_id == _muestreoId)
                                              .Select(mcr => mcr.calidad_id)
                                              .Contains(c.calidad_id)
                            select new CalidadesRecepcionesViewModel
                            {
                                CalidadRecepcionId = 0,
                                CalidadId = c.calidad_id,
                                Nombre = c.nombre,
                                Valor = 0,
                                AfectaCalificacion = c.afecta_calificacion,
                                IncluyeEnReporte = c.visible_reporte
                            };
                return query.ToList();
            }
        }

    }
}
