using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Mango
{
    internal class MangoProveedoresAnalisisCampoController : ICatalogoCRUD<ProveedoresAnalisisCampo, int>
    {
        public MangoProveedoresAnalisisCampoController()
        {
        }

        public void Crear(ProveedoresAnalisisCampo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.ProveedoresAnalisisCampo.Add(entidad);
                _context.SaveChanges();
            }

        }

        public int CrearAnalisis(ProveedoresAnalisisCampo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.ProveedoresAnalisisCampo.Add(entidad);
                _context.SaveChanges();
                return entidad.proveedor_analisis_campo_id;
            }

        }

        public void Actualizar(ProveedoresAnalisisCampo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.ProveedoresAnalisisCampo.Find(Convert.ToInt16(entidad.proveedor_analisis_campo_id));
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
                var registro = _context.ProveedoresAnalisisCampo.Find(id);
                if (registro != null)
                {
                    _context.ProveedoresAnalisisCampo.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public ProveedoresAnalisisCampo ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.ProveedoresAnalisisCampo.Find(id);
            }
        }

        public ProveedoresAnalisisCampo ObtenerPorProveedorYFecha(int proveedorId, DateTime fecha)
        {
            DateTime inicioDia = fecha.Date;
            DateTime finDia = inicioDia.AddDays(1);

            using (var _context = new Model.Model())
            {
                return _context.ProveedoresAnalisisCampo
                    .FirstOrDefault(p => p.proveedor_id == proveedorId
                                      && p.fecha_evaluacion >= inicioDia
                                      && p.fecha_evaluacion < finDia);
            }
        }

        public List<ProveedoresAnalisisCampo> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.ProveedoresAnalisisCampo.ToList();
            }
        }

        public List<ProveedoresAnalisisCampo> ListarPorProveedorId(int proveedorId)
        {
            using (var _context = new Model.Model())
            {
                return _context.ProveedoresAnalisisCampo.Where(p => p.proveedor_id == proveedorId).ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.ProveedoresAnalisisCampo
                    .AsEnumerable()
                    .Where(e => e.estado == "A")
                    .Select(g => new KeyValuePair<string, string>(
                        g.proveedor_analisis_campo_id.ToString(),
                        g.fecha_evaluacion.ToShortDateString()
                        )
                    )
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }


    }
}
