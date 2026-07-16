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
    internal class MangoProveedoresController : ICatalogoCRUD<MangoProveedores, int>
    {
        public MangoProveedoresController()
        {
        }

        public void Crear(MangoProveedores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoProveedores.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoProveedores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoProveedores.Find(entidad.proveedor_id);
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
                var registro = _context.MangoProveedores.Find(id);
                if (registro != null)
                {
                    _context.MangoProveedores.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoProveedores ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoProveedores.Find(id);
            }
        }

        public List<MangoProveedores> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoProveedores.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                var lista = _context.MangoProveedores
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.proveedor_id.ToString(),
                        g.codigo.ToUpper() + "-" + g.nombre))
                    .OrderBy(x => x.Value)
                    .ToList();

                // Agregar "Todos" al inicio
                //lista.Insert(0, new KeyValuePair<string, string>("", "Todos"));

                return lista;
            }
        }

    }
}
