using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Residuos
{
    internal class CiclosResiduosController: ICatalogoCRUD<CiclosResiduos, int>
    {
        public CiclosResiduosController()
        {

        }

        public void Crear(CiclosResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                var registroAnterior = ObtenerCicloResiduosAnterior(entidad);
                if (registroAnterior != null)
                {
                    registroAnterior.fecha_fin = entidad.fecha_inicio.AddDays(-1);
                    _context.Entry(registroAnterior).State = System.Data.Entity.EntityState.Modified;
                }
                entidad.fecha_fin = null;
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CiclosResiduos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CiclosResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CiclosResiduos.Find(entidad.ciclo_residuos_id);

                var registroAnterior = ObtenerCicloResiduosAnterior(entidad);
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
                var registro = _context.CiclosResiduos.Find(id);
                if (registro != null)
                {
                    _context.CiclosResiduos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CiclosResiduos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CiclosResiduos.Find(id);
            }
        }

        public CiclosResiduos ObtenerPorCicloResiduosId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CiclosResiduos
                               .FirstOrDefault(ci => ci.ciclo_residuos_id == cicloId); ;
            }
        }

        public CiclosResiduos ObtenerCicloResiduosAnterior(CiclosResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                return _context.CiclosResiduos
                    .Where(p => p.ciclo_residuos_id != entidad.ciclo_residuos_id && p.fecha_inicio < entidad.fecha_inicio)
                    .OrderByDescending(p => p.fecha_inicio)
                    .FirstOrDefault();
            }
        }

        public int ObtenerUltimoCicloResiduosActivo()
        {
            using (var _context = new Model.Model())
            {
                return _context.CiclosResiduos
                    .Where(p => p.estado == "A")
                    .OrderByDescending(p => p.ciclo_residuos_id)
                    .Select(p=> p.ciclo_residuos_id)
                    .FirstOrDefault();
            }
        }

        public int ObtenerCicloResiduosPorFecha(DateTime fecha)
        {
            using (var _context = new Model.Model())
            {
                var ciclo = _context.CiclosResiduos
                    .Where(p => p.fecha_inicio <= fecha && 
                                (p.fecha_fin == null || p.fecha_fin >= fecha))
                    .FirstOrDefault();
                return ciclo != null ? ciclo.ciclo_residuos_id : 0;
            }
        }

        public List<CiclosResiduos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CiclosResiduos.ToList();
            }
        }

        public List<CiclosResiduos> ListarPorEstado(string estado_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CiclosResiduos
                    .Where(v => v.estado == estado_id)
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarCombo()
        {
            using (var _context = new Model.Model())
            {
                var temp = _context.CiclosResiduos
                    .Where(c => c.estado == "A")
                    .Select(r => new { Value = r.ciclo_residuos_id, Text = r.fecha_inicio }) // traducible a SQL
                    .OrderByDescending(r => r.Value)
                    .ToList(); // materializa la consulta

                return temp
                    .Select(x => new KeyValuePair<string, string>(x.Value.ToString(), $"{x.Value} - {x.Text:dd/MM/yyyy}"))
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboCiclosResiduosActivosOrdenDescendete()
        {
            using (var _context = new Model.Model())
            {
                return _context.CiclosResiduos
                    .Where(c => c.estado == "A")
                    .OrderByDescending(c => c.ciclo_residuos_id)
                    .ToList()
                    .Select(c => new KeyValuePair<string, string>(
                        c.ciclo_residuos_id.ToString(),
                        $"{c.ciclo_residuos_id} - {c.fecha_inicio:dd/MM/yyyy}"
                    ))
                    .ToList();
            }
        }

    }
}
