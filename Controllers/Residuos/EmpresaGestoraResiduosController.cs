using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Residuos
{
    internal class EmpresaGestoraResiduosController : ICatalogoCRUD<EmpresaGestoraDeResiduos, int>
    {
        public EmpresaGestoraResiduosController()
        {

        }

        public void Crear(EmpresaGestoraDeResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.EmpresaGestoraDeResiduos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public int CrearEmpresa(EmpresaGestoraDeResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.EmpresaGestoraDeResiduos.Add(entidad);
                _context.SaveChanges();
            }
            return entidad.empresa_gestora_id;
        }

        public void Actualizar(EmpresaGestoraDeResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.EmpresaGestoraDeResiduos.Find(entidad.empresa_gestora_id);
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
                var registro = _context.EmpresaGestoraDeResiduos.Find(id);
                if (registro != null)
                {
                    _context.EmpresaGestoraDeResiduos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public EmpresaGestoraDeResiduos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.EmpresaGestoraDeResiduos.Find(id);
            }
        }

        public List<EmpresaGestoraDeResiduos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.EmpresaGestoraDeResiduos.ToList();
            }
        }

        public List<EmpresaGestoraDeResiduos> ListarEmpresaGestoraResiduoActivos()
        {
            using (var _context = new Model.Model())
            {
                return _context.EmpresaGestoraDeResiduos
                    .Where(t => t.estado == "A")
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxEmpresasGestorasResiduos()
        {
            using (var _context = new Model.Model())
            {
                return _context.EmpresaGestoraDeResiduos
                    .Select(g => new
                    {
                        Id = g.empresa_gestora_id,
                        Nombre = g.nombre
                    })
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.Id.ToString(),
                        g.Nombre.ToString()))
                    .OrderBy(x => x.Value)
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxEmpresasGestorasResiduosActivasYSeleccionada(int _empresaId)
        {
            using (var _context = new Model.Model())
            {
                return _context.EmpresaGestoraDeResiduos
                    .Where(t => t.estado == "A" || t.empresa_gestora_id == _empresaId)
                    .Select(g => new
                    {
                        Id = g.empresa_gestora_id,
                        Nombre = g.nombre
                    })
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.Id.ToString(),
                        g.Nombre.ToString()))
                    .OrderBy(x => x.Value)
                    .ToList();
            }
        }
    }
}
