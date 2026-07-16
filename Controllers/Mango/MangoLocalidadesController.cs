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
    internal class MangoLocalidadesController : ICatalogoCRUD<MangoLocalidades, int>
    {
        public MangoLocalidadesController()
        {
        }

        public void Crear(MangoLocalidades entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoLocalidades.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoLocalidades entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoLocalidades.Find(entidad.localidad_id);
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
                var registro = _context.MangoLocalidades.Find(id);
                if (registro != null)
                {
                    _context.MangoLocalidades.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoLocalidades ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoLocalidades.Find(id);
            }
        }

        public List<MangoLocalidades> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoLocalidades.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                var lista = _context.MangoLocalidades
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.localidad_id.ToString(),
                        g.nombre))
                    .OrderBy(x => x.Value)
                    .ToList();

                // Agregar "Todos" al inicio
                //lista.Insert(0, new KeyValuePair<string, string>("", "Todos"));

                return lista;
            }
        }

    }
}
