using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Configuracion
{
    internal class EmpleadosController : ICatalogoCRUD<Empleados, string>
    {
        public EmpleadosController()
        {
        }

        public void Crear(Empleados entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Empleados.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Empleados entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Empleados.Find(Convert.ToInt16(entidad.empleado_id));
                entidad.usuario_crea = registro.usuario_crea;
                entidad.fecha_crea = registro.fecha_crea;
                if (registro != null)
                {
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }


        public void Eliminar(string id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.Empleados.Find(id);
                if (registro != null)
                {
                    _context.Empleados.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Empleados ObtenerPorId(string id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Empleados.Find(Convert.ToInt16(id));
            }
        }

        public List<Empleados> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Empleados.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.Empleados
                    .AsEnumerable()
                    .Where(e => e.estado == "A")
                    .Select(g => new KeyValuePair<string, string>(
                        g.empleado_id.ToString(),
                        g.apellido_paterno.ToString() + " " + g.apellido_materno.ToString() + " " + g.nombre.ToString()
                        )
                    )
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }


    }
}
