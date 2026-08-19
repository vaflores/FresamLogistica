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
    internal class CiclosController : ICatalogoCRUD<Ciclos, int>
    {
        public CiclosController()
        {

        }

        public void Crear(Ciclos entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);

                var registroAnterior = ObtenerCicloAnterior(entidad);
                if (registroAnterior != null)
                {
                    registroAnterior.fecha_fin = entidad.fecha_inicio.AddDays(-1);
                    _context.Entry(registroAnterior).State = System.Data.Entity.EntityState.Modified;
                }
                entidad.fecha_fin = null;
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Ciclos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Ciclos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Ciclos.Find(entidad.ciclo_id);

                var registroAnterior = ObtenerCicloAnterior(entidad);
                if (registroAnterior != null)
                {
                    registroAnterior.fecha_fin = entidad.fecha_inicio.AddDays(-1);
                    _context.Entry(registroAnterior).State = System.Data.Entity.EntityState.Modified;
                }
                //_context.SaveChanges();

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
                var registro = _context.Ciclos.Find(id);
                if (registro != null)
                {
                    _context.Ciclos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Ciclos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciclos.Find(id);
            }
        }

        public Ciclos ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciclos
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId); ;
            }
        }

        public Ciclos ObtenerCicloAnterior(Ciclos entidad)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciclos
                    .Where(p => p.ciclo_id != entidad.ciclo_id && p.fecha_inicio < entidad.fecha_inicio)
                    .OrderByDescending(p => p.fecha_inicio)
                    .FirstOrDefault();
            }
        }

        public int ObtenerUltimoCicloActivo()
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciclos
                    .Where(p => p.estado == "A")
                    .OrderByDescending(p => p.ciclo_id)
                    .Select(p => p.ciclo_id)
                    .FirstOrDefault();
            }
        }

        public int ObtenerCicloPorFecha(DateTime fecha)
        {
            using (var _context = new Model.Model())
            {
                var ciclo = _context.Ciclos
                    .Where(p => p.fecha_inicio <= fecha &&
                                (p.fecha_fin == null || p.fecha_fin >= fecha))
                    .FirstOrDefault();
                return ciclo != null ? ciclo.ciclo_id : 0;
            }
        }

        public List<Ciclos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciclos.ToList();
            }
        }

        public List<Ciclos> ListarPorEstado(string estado_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciclos
                    .Where(v => v.estado == estado_id)
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarCombo()
        {
            using (var _context = new Model.Model())
            {
                var temp = _context.Ciclos
                    .Where(c => c.estado == "A")
                    .Select(r => new { Value = r.ciclo_id, Text = r.fecha_inicio }) // traducible a SQL
                    .OrderByDescending(r => r.Value)
                    .ToList(); // materializa la consulta

                return temp
                    .Select(x => new KeyValuePair<string, string>(x.Value.ToString(), $"{x.Value} - {x.Text:dd/MM/yyyy}"))
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboCiclosActivosOrdenDescendete()
        {
            using (var _context = new Model.Model())
            {
                return _context.Ciclos
                    .Where(c => c.estado == "A")
                    .OrderByDescending(c => c.ciclo_id)
                    .ToList()
                    .Select(c => new KeyValuePair<string, string>(
                        c.ciclo_id.ToString(),
                        $"{c.ciclo_id} - {c.fecha_inicio:dd/MM/yyyy}"
                    ))
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref Ciclos entidad)
        {
            if (entidad.ciclo_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Ciclos
                                      .OrderByDescending(p => p.ciclo_id)
                                      .Select(p => p.ciclo_id)
                                      .FirstOrDefault();

                    entidad.ciclo_id = ultimoId + 1;
                }
                ;
            }
        }

        public bool ValidarFechaInicialPrimeraRecepcionDelCiclo(int cicloId, DateTime fecha)
        {
            DateTime fechaPrimeraRecepcion = fecha;
            DateTime fechaPrimeraRecepcionMango = fecha;

            using (var _context = new Model.Model())
            {
                bool existeRecepcion = _context.Recepciones
                    .Any(r => r.ciclo_id == cicloId);

                bool existeMangoRecepcion = _context.MangoRecepciones
                    .Any(r => r.ciclo_id == cicloId);

                if (existeRecepcion)
                {
                    fechaPrimeraRecepcion = _context.Recepciones
                        .Where(r => r.ciclo_id == cicloId)
                        .OrderBy(r => r.fecha)
                        .Select(r => r.fecha)
                        .FirstOrDefault();
                }

                if(existeMangoRecepcion)
                {
                    fechaPrimeraRecepcionMango = _context.MangoRecepciones
                        .Where(r => r.ciclo_id == cicloId)
                        .OrderBy(r => r.fecha)
                        .Select(r => r.fecha)
                        .FirstOrDefault();
                }

                if (existeRecepcion && existeMangoRecepcion)
                {
                    return fechaPrimeraRecepcion > fecha && fechaPrimeraRecepcionMango > fecha;
                }
                else if (existeRecepcion)
                {
                    return fechaPrimeraRecepcion > fecha;
                }
                else if (existeMangoRecepcion)
                {
                    return fechaPrimeraRecepcionMango > fecha;
                }
                else
                {
                    // No hay recepciones ni de mango ni de fruta, por lo que no hay restricciones
                    return true;
                }

            }
        }

        public bool ValidarFechaInicialUltimaRecepcion(int cicloId, DateTime fecha)
        {
            DateTime fechaPrimeraRecepcion = fecha;
            DateTime fechaPrimeraRecepcionMango = fecha;

            using (var _context = new Model.Model())
            {
                bool existeRecepcion = _context.Recepciones
                    .Any();

                bool existeMangoRecepcion = _context.MangoRecepciones
                    .Any();

                if (existeRecepcion)
                {
                    fechaPrimeraRecepcion = _context.Recepciones
                        .OrderByDescending(r => r.fecha)
                        .Select(r => r.fecha)
                        .FirstOrDefault();
                }

                if (existeMangoRecepcion)
                {
                    fechaPrimeraRecepcionMango = _context.MangoRecepciones
                        .OrderByDescending(r => r.fecha)
                        .Select(r => r.fecha)
                        .FirstOrDefault();
                }

                if (existeRecepcion && existeMangoRecepcion)
                {
                    return fechaPrimeraRecepcion < fecha && fechaPrimeraRecepcionMango < fecha;
                }
                else if (existeRecepcion)
                {
                    return fechaPrimeraRecepcion < fecha;
                }
                else if (existeMangoRecepcion)
                {
                    return fechaPrimeraRecepcionMango < fecha;
                }
                else
                {
                    // No hay recepciones ni de mango ni de fruta, por lo que no hay restricciones
                    return true;
                }

            }
        }
    }
}
