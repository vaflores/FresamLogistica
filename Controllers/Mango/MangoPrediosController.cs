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
    internal class MangoPrediosController : ICatalogoCRUD<MangoPredios, int>
    {
        public MangoPrediosController()
        {
        }

        public void Crear(MangoPredios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoPredios.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoPredios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoPredios.Find(Convert.ToInt16(entidad.predio_id));
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
                var registro = _context.MangoPredios.Find(id);
                if (registro != null)
                {
                    _context.MangoPredios.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoPredios ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoPredios.Find(id);
            }
        }

        public List<MangoPredios> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoPredios.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoPredios
                    .AsEnumerable()
                    .Where(e => e.estado == "A")
                    .Select(g => new KeyValuePair<string, string>(
                        g.predio_id.ToString(),
                        g.nombre.ToString()
                        )
                    )
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }
        public List<KeyValuePair<string, string>> ListarComboBoxPorLocalidad(int localidadId)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoPredios
                    .AsEnumerable()
                    .Where(e => e.localidad_id == localidadId)
                    .Select(g => new KeyValuePair<string, string>(
                        g.predio_id.ToString(),
                        g.nombre.ToString()
                        )
                    )
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }


    }
}
