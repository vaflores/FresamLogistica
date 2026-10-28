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
    internal class AgricultoresAnalisisCampoController : ICatalogoCRUD<AgricultoresAnalisisCampo, int>
    {
        public AgricultoresAnalisisCampoController()
        {
        }

        public void Crear(AgricultoresAnalisisCampo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.AgricultoresAnalisisCampo.Add(entidad);
                _context.SaveChanges();
            }

        }

        public int CrearAnalisis(AgricultoresAnalisisCampo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.AgricultoresAnalisisCampo.Add(entidad);
                _context.SaveChanges();
                return entidad.agricultor_analisis_campo_id;
            }

        }

        public void Actualizar(AgricultoresAnalisisCampo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.AgricultoresAnalisisCampo.Find(Convert.ToInt16(entidad.agricultor_analisis_campo_id));
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
                var registro = _context.AgricultoresAnalisisCampo.Find(id);
                if (registro != null)
                {
                    _context.AgricultoresAnalisisCampo.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public AgricultoresAnalisisCampo ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.AgricultoresAnalisisCampo.Find(id);
            }
        }

        public AgricultoresAnalisisCampo ObtenerPorAgricultorYFecha(int agricultorId, DateTime fecha)
        {
            DateTime inicioDia = fecha.Date;
            DateTime finDia = inicioDia.AddDays(1);

            using (var _context = new Model.Model())
            {
                return _context.AgricultoresAnalisisCampo           
                    .FirstOrDefault(p => p.agricultor_id == agricultorId
                                      && p.fecha_evaluacion >= inicioDia
                                      && p.fecha_evaluacion < finDia);
            }
        }

        public List<AgricultoresAnalisisCampo> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.AgricultoresAnalisisCampo.ToList();
            }
        }

        public List<AgricultoresAnalisisCampo> ListarPorAgricultorId(int agricultorId)
        {
            using (var _context = new Model.Model())
            {
                return _context.AgricultoresAnalisisCampo.Where(p => p.agricultor_id == agricultorId).ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.AgricultoresAnalisisCampo
                    .AsEnumerable()
                    .Where(e => e.estado == "A")
                    .Select(g => new KeyValuePair<string, string>(
                        g.agricultor_analisis_campo_id.ToString(),
                        g.fecha_evaluacion.ToShortDateString()
                        )
                    )
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }


    }
}
