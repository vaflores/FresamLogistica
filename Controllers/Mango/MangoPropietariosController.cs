using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Mango
{
    internal class MangoPropietariosController : ICatalogoCRUD<MangoPropietarios, int>
    {
        public MangoPropietariosController()
        {
        }

        public void Crear(MangoPropietarios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoPropietarios.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoPropietarios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoPropietarios.Find(entidad.propietario_id);
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
                var registro = _context.MangoPropietarios.Find(id);
                if (registro != null)
                {
                    _context.MangoPropietarios.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoPropietarios ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoPropietarios.Find(id);
            }
        }

        public List<MangoPropietarios> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoPropietarios.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                var lista = _context.MangoPropietarios
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.propietario_id.ToString(),
                        g.nombre))
                    .OrderBy(x => x.Value)
                    .ToList();

                // Agregar "Todos" al inicio
                //lista.Insert(0, new KeyValuePair<string, string>("", "Todos"));

                return lista;
            }
        }
        public List<KeyValuePair<string, string>> ListarComboBoxPorPredio(int predioId)
        {
            using (var _context = new Model.Model())
            {
                return (from p in _context.MangoPredios
                        join pr in _context.MangoPropietarios
                            on p.propietario_id equals pr.propietario_id
                        where p.predio_id == predioId
                        select new
                        {
                            pr.propietario_id,
                            pr.nombre
                        })
                        .AsEnumerable()
                        .Select(x => new KeyValuePair<string, string>(
                            x.propietario_id.ToString(),
                            x.nombre))
                        .OrderBy(x => x.Value)
                        .ToList();
            }
        }

    }
}
