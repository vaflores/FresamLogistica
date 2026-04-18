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
    internal class TiposDestinosResiduosController : ICatalogoCRUD<TiposDestinoResiduo, int>
    {
        public TiposDestinosResiduosController()
        {

        }

        public void Crear(TiposDestinoResiduo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.TiposDestinoResiduo.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(TiposDestinoResiduo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.TiposDestinoResiduo.Find(entidad.tipo_destino_residuo_id);
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
                var registro = _context.TiposDestinoResiduo.Find(id);
                if (registro != null)
                {
                    _context.TiposDestinoResiduo.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public TiposDestinoResiduo ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.TiposDestinoResiduo.Find(id);
            }
        }

        public List<TiposDestinoResiduo> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.TiposDestinoResiduo.ToList();
            }
        }

        public List<TiposDestinoResiduo> ListarDestinosResiduoActivos()
        {
            using (var _context = new Model.Model())
            {
                return _context.TiposDestinoResiduo
                    .Where(t => t.estado == "A")
                    .ToList();
            }
        }

        public List<KeyValuePair<string,string>> ListarComboBoxDestinosResiduoActivos()
        {
            using (var _context = new Model.Model())
            {
                return _context.TiposDestinoResiduo
                    .Where(t => t.estado == "A")
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.tipo_destino_residuo_id.ToString(), g.nombre.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }


    }
}
