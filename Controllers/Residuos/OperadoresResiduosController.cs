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
    internal class OperadoresResiduosController : ICatalogoCRUD<OperadoresResiduos, int>
    {
        public OperadoresResiduosController()
        {

        }

        public void Crear(OperadoresResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.OperadoresResiduos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(OperadoresResiduos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.OperadoresResiduos.Find(entidad.empresa_gestora_id);
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
                var registro = _context.OperadoresResiduos.Find(id);
                if (registro != null)
                {
                    _context.OperadoresResiduos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public OperadoresResiduos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.OperadoresResiduos.Find(id);
            }
        }

        public List<OperadoresResiduos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.OperadoresResiduos.ToList();
            }
        }

        public List<OperadoresResiduos> ListarOperadoresResiduoPorEmpresa(int _idEmpresaGestora)
        {
            using (var _context = new Model.Model())
            {
                return _context.OperadoresResiduos
                    .Where(t => t.empresa_gestora_id == _idEmpresaGestora)
                    .ToList();
            }
        }

        public List<OperadoresResiduos> ListarOperadoresResiduoActivosPorEmpresa(int _idEmpresaGestora)
        {
            using (var _context = new Model.Model())
            {
                return _context.OperadoresResiduos
                    .Where(t => t.empresa_gestora_id == _idEmpresaGestora && t.estado == "A")
                    .ToList();
            }
        }

        public List<OperadoresResiduos> ListarOperadoresResiduoActivos()
        {
            using (var _context = new Model.Model())
            {
                return _context.OperadoresResiduos
                    .Where(t => t.estado == "A")
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxOperadoresResiduoActivosYSeleccionado(int _empresaId)
        {
            using (var _context = new Model.Model())
            {
                return _context.EmpresaGestoraDeResiduos
                    .Where(t => t.estado == "A" || t.empresa_gestora_id == _empresaId)
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.empresa_gestora_id.ToString(), g.nombre.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Value)
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxOperadoresResiduoPorEmpresaYSeleccionado(int _empresaId, int _operadorId)
        {
            using (var _context = new Model.Model())
            {
                return _context.OperadoresResiduos
                    .Where(t => t.empresa_gestora_id == _empresaId && t.estado == "A" || t.operador_residuo_id == _operadorId )
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.operador_residuo_id.ToString(), g.nombre.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Value)
                    .ToList();
            }
        }
        public List<KeyValuePair<string, string>> ListarComboBoxOperadoresResiduoPorEmpresaActivos(int _empresaId)
        {
            using (var _context = new Model.Model())
            {
                return _context.OperadoresResiduos
                    .Where(t => t.empresa_gestora_id == _empresaId && t.estado == "A")
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.operador_residuo_id.ToString(), g.nombre.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Value)
                    .ToList();
            }
        }
    }
}
