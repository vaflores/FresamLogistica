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
    internal class TiposResiduosController : ICatalogoCRUD<TipoResiduo, int>
    {
        public TiposResiduosController()
        {

        }

        public void Crear(TipoResiduo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.TipoResiduo.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(TipoResiduo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.TipoResiduo.Find(entidad.tipo_residuo_id);
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
                var registro = _context.TipoResiduo.Find(id);
                if (registro != null)
                {
                    _context.TipoResiduo.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public TipoResiduo ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.TipoResiduo.Find(id);
            }
        }

        public List<TipoResiduo> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.TipoResiduo.ToList();
            }
        }
        public List<TipoResiduo> ListarPorFruta(int fruta_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.TipoResiduo
                    .Where(v => v.fruta_id == fruta_id)
                    .ToList();
            }
        }


        public List<KeyValuePair<string, string>> ListarComboBoxPorFruta(int fruta_id, int tipo_residuo_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.TipoResiduo
                    .Where(v => v.fruta_id == fruta_id && v.estado == "A" || v.tipo_residuo_id == tipo_residuo_id)
                    .AsEnumerable()
                    .ToList()
                    .Select(f => new KeyValuePair<string, string>(f.tipo_residuo_id.ToString(), f.nombre))
                    .ToList();
            }
        }

    }
}
