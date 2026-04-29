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
    internal class CalidadesController : ICatalogoCRUD<Calidades, int>
    {

        public CalidadesController()
        {
        }

        public void Crear(Calidades entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Calidades.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void CrearCalidadesRecepcionesViewModel(CalidadesRecepcionesViewModel calidad)
        {
            using (var _context = new Model.Model())
            {
                var entidad = new CalidadesRecepciones
                {
                    calidad_recepcion_id = 0, // Asumiendo que es autogenerado
                    recepcion_id = calidad.CalidadRecepcionId,
                    calidad_id = calidad.CalidadId,
                    valor = calidad.Valor
                };
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CalidadesRecepciones.Add(entidad);
                _context.SaveChanges();
            }
        }
        public void Actualizar(Calidades entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Calidades.Find(entidad.id_calidad);
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
                var registro = _context.Calidades.Find(id);
                if (registro != null)
                {
                    _context.Calidades.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarCalidadRecepcion(int _id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.CalidadesRecepciones.Find(_id);
                if (registro != null)
                {
                    _context.CalidadesRecepciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Calidades ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Calidades.Find(id);
            }
        }

        public List<Calidades> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Calidades.ToList();
            }
        }

        public List<CalidadesRecepcionesViewModel> ListarCalidadesPorRecepcionId(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                var query = from cr in _context.CalidadesRecepciones
                            join c in _context.Calidades on cr.calidad_id equals c.id_calidad
                            where cr.recepcion_id == _recepcionId
                            select new CalidadesRecepcionesViewModel
                            {
                                CalidadRecepcionId = cr.calidad_recepcion_id,
                                CalidadId = c.id_calidad,
                                Nombre = c.nombre,
                                Valor = cr.valor,
                                AfectaCalificacion = c.afecta_calificacion,
                                IncluyeEnReporte = c.incluir_en_reporte
                            };
                return query.ToList();
            }
        }

        public List<CalidadesRecepciones> ListarCalidadesRecepcionesPorRecepcionId(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CalidadesRecepciones
                               .Where(cr => cr.recepcion_id == _recepcionId)
                               .ToList();
            }
        }

        public List<CalidadesRecepcionesViewModel> ListarCalidadesActivasSinValorPorRecepcionId(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                var query = from c in _context.Calidades
                            where c.estado == "A"
                                  && !_context.CalidadesRecepciones
                                              .Where(cr => cr.recepcion_id == _recepcionId)
                                              .Select(cr => cr.calidad_id)
                                              .Contains(c.id_calidad)
                            select new CalidadesRecepcionesViewModel
                            {
                                CalidadRecepcionId = 0,
                                CalidadId = c.id_calidad,
                                Nombre = c.nombre,
                                Valor = 0,
                                AfectaCalificacion = c.afecta_calificacion,
                                IncluyeEnReporte = c.incluir_en_reporte
                            };
                return query.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarCombo()
        {
            using (var _context = new Model.Model())
            {
                var temp = _context.Calidades
                    .Select(r => new { r.id_calidad, r.nombre }) // traducible a SQL
                    .ToList(); // materializa la consulta

                return temp
                    .Select(x => new KeyValuePair<string, string>(x.id_calidad.ToString(), x.nombre))
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Calidades entidad)
        {
            if (entidad.id_calidad == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Calidades
                                      .OrderByDescending(p => p.id_calidad)
                                      .Select(p => p.id_calidad)
                                      .FirstOrDefault();

                    entidad.id_calidad = ultimoId + 1;
                }
                ;
            }
        }

        private void AsignarIdSiCero(ref CalidadesRecepciones entidad)
        {
            if (entidad.calidad_recepcion_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.CalidadesRecepciones
                                      .OrderByDescending(p => p.calidad_recepcion_id)
                                      .Select(p => p.calidad_recepcion_id)
                                      .FirstOrDefault();
                    entidad.calidad_recepcion_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
