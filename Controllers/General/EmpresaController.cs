using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;

namespace FresamLogistica.Controllers.General
{
    internal class EmpresaController : ICatalogoCRUD<Empresa, int>
    {

        public EmpresaController()
        {
        }

        public void Crear(Empresa entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Empresa.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Empresa entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var empresa = _context.Empresa.Find(entidad.empresa_id);
                entidad.usuario_crea = empresa.usuario_crea;
                entidad.fecha_crea = empresa.fecha_crea;
                if (empresa != null)
                {
                    _context.Entry(empresa).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var empresa = _context.Empresa.Find(id);
                if (empresa != null)
                {
                    _context.Empresa.Remove(empresa);
                    _context.SaveChanges();
                }
            }
        }

        public Empresa ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Empresa.Find(id);
            }
        }

        public List<Empresa> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Empresa.ToList();
            }
        }

    }
}
