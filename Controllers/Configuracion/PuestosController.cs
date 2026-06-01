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
    internal class PuestosController : ICatalogoCRUD<Puestos, string>
    {
        public PuestosController()
        {
        }

        public void Crear(Puestos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Puestos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Puestos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Puestos.Find(Convert.ToInt16(entidad.puesto_id));
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
                var registro = _context.Puestos.Find(id);
                if (registro != null)
                {
                    _context.Puestos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Puestos ObtenerPorId(string id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Puestos.Find(Convert.ToInt16(id));
            }
        }

        public List<Puestos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Puestos.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.Puestos
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.puesto_id.ToString(), g.nombre.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }


    }
}
