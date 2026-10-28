using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Mango
{
    internal class MangoProveedorAnalisisCampoDetalleController : ICatalogoCRUD<ProveedoresAnalisisCampoDetalle, int>
    {
        public MangoProveedorAnalisisCampoDetalleController()
        {
        }

        public void Crear(ProveedoresAnalisisCampoDetalle entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.ProveedoresAnalisisCampoDetalle.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(ProveedoresAnalisisCampoDetalle entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.ProveedoresAnalisisCampoDetalle.Find(Convert.ToInt16(entidad.proveedor_analisis_campo_detalle_id));
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
                var registro = _context.ProveedoresAnalisisCampoDetalle.Find(id);
                if (registro != null)
                {
                    _context.ProveedoresAnalisisCampoDetalle.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public ProveedoresAnalisisCampoDetalle ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.ProveedoresAnalisisCampoDetalle.Find(id);
            }
        }

        public List<ProveedoresAnalisisCampoDetalle> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.ProveedoresAnalisisCampoDetalle.ToList();
            }
        }

        public List<CondicionesAnalisisCampoViewModel> ListarPorIdCondiciones(int _provedor_analisis_campo_id)
        {
            List<CatTiposAnalisisCampo> listCondiciones = ListarCondcionesActivas();

            using (var _context = new Model.Model())
            {
                var list = _context.ProveedoresAnalisisCampoDetalle.Where(c => c.proveedor_analisis_campo_id == _provedor_analisis_campo_id).ToList();
                foreach (var item in listCondiciones)
                {
                    if (!list.Any(c => c.tipo_analisis_campo_id == item.tipo_analisis_campo_id))
                    {
                        ProveedoresAnalisisCampoDetalle nuevoRegistro = new ProveedoresAnalisisCampoDetalle
                        {
                            proveedor_analisis_campo_id = _provedor_analisis_campo_id,
                            tipo_analisis_campo_id = item.tipo_analisis_campo_id,
                            cumple = false
                        };
                        SesionActualHelper.RegistrarSesionCrea(ref nuevoRegistro);
                        _context.ProveedoresAnalisisCampoDetalle.Add(nuevoRegistro);
                    }
                }
                _context.SaveChanges();
                return _context.ProveedoresAnalisisCampoDetalle.Where(c => c.proveedor_analisis_campo_id == _provedor_analisis_campo_id)
                    .Select(c => new CondicionesAnalisisCampoViewModel
                    {
                        analisis_campo_detalle_id = c.proveedor_analisis_campo_detalle_id,
                        analisis_campo_id = c.proveedor_analisis_campo_id,
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
                    var registro = _context.ProveedoresAnalisisCampoDetalle.Find(item.analisis_campo_detalle_id);
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
