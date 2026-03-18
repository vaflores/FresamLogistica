using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Configuracion
{
    internal class UsuariosController : ICatalogoCRUD<Usuarios, string>
    {
        public UsuariosController()
        {
        }

        public void Crear(Usuarios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Usuarios.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Usuarios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Usuarios.Find(entidad.usuario_id);
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
                var registro = _context.Usuarios.Find(id);
                if (registro != null)
                {
                    _context.Usuarios.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Usuarios ObtenerPorId(string id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Usuarios.Find(id);
            }
        }

        public List<Usuarios> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Usuarios.ToList();
            }
        }

        public Usuarios ValidarCredenciales(string usuarioId, string password)
        {
            using (var _context = new Model.Model())
            {
                var usuario = _context.Usuarios
                    .Where(u => u.usuario_id == usuarioId && u.password == password)
                    .FirstOrDefault();
                return usuario;
            }
        }
    }
}
