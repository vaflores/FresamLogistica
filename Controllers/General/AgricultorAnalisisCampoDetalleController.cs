using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class AgricultorAnalisisCampoDetalleController : ICatalogoCRUD<AgricultoresAnalisisCampoDetalle, int>
    {
        public AgricultorAnalisisCampoDetalleController()
        {
        }

        public void Crear(AgricultoresAnalisisCampoDetalle entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.AgricultoresAnalisisCampoDetalle.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(AgricultoresAnalisisCampoDetalle entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.AgricultoresAnalisisCampoDetalle.Find(Convert.ToInt16(entidad.agricultor_analisis_campo_detalle_id));
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
                var registro = _context.AgricultoresAnalisisCampoDetalle.Find(id);
                if (registro != null)
                {
                    _context.AgricultoresAnalisisCampoDetalle.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public AgricultoresAnalisisCampoDetalle ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.AgricultoresAnalisisCampoDetalle.Find(id);
            }
        }

        public List<AgricultoresAnalisisCampoDetalle> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.AgricultoresAnalisisCampoDetalle.ToList();
            }
        }

        public List<CondicionesAnalisisCampoViewModel> ListarPorIdCondiciones(int _provedor_analisis_campo_id)
        {
            List<CatTiposAnalisisCampo> listCondiciones = ListarCondcionesActivas();

            using (var _context = new Model.Model())
            {
                var list = _context.AgricultoresAnalisisCampoDetalle.Where(c => c.agricultor_analisis_campo_id == _provedor_analisis_campo_id).ToList();
                foreach (var item in listCondiciones)
                {
                    if (!list.Any(c => c.tipo_analisis_campo_id == item.tipo_analisis_campo_id))
                    {
                        AgricultoresAnalisisCampoDetalle nuevoRegistro = new AgricultoresAnalisisCampoDetalle
                        {
                            agricultor_analisis_campo_id = _provedor_analisis_campo_id,
                            tipo_analisis_campo_id = item.tipo_analisis_campo_id,
                            cumple = false
                        };
                        SesionActualHelper.RegistrarSesionCrea(ref nuevoRegistro);
                        _context.AgricultoresAnalisisCampoDetalle.Add(nuevoRegistro);
                    }
                }
                _context.SaveChanges();
                return _context.AgricultoresAnalisisCampoDetalle.Where(c => c.agricultor_analisis_campo_id == _provedor_analisis_campo_id)
                    .Select(c => new CondicionesAnalisisCampoViewModel
                    {
                        analisis_campo_detalle_id = c.agricultor_analisis_campo_detalle_id,
                        analisis_campo_id = c.agricultor_analisis_campo_id,
                        tipo_analisis_campo_id = c.tipo_analisis_campo_id,
                        codigo_analisis = c.CatTiposAnalisisCampo.codigo,
                        analisis_campo = c.CatTiposAnalisisCampo.nombre,
                        cumple = c.cumple
                    }).ToList();
            }
        }

        public void ActualizarListaCondiciones(List<CondicionesAnalisisCampoViewModel> listaCondiciones)
        {
            using (var _context = new Model.Model())
            {
                foreach (var item in listaCondiciones)
                {
                    var registro = _context.AgricultoresAnalisisCampoDetalle.Find(item.analisis_campo_detalle_id);
                    if (registro != null)
                    {
                        registro.cumple = item.cumple;
                        SesionActualHelper.RegistrarUsuarioModifica(ref registro);
                    }
                }
                _context.SaveChanges();
            }
        }

        public List<CatTiposAnalisisCampo> ListarCondcionesActivas()
        {
            using (var _context = new Model.Model())
            {
                return _context.CatTiposAnalisisCampo.Where(c => c.activo).ToList();
            }
        }
    }
}
