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
    internal class DestinosResiduosController : ICatalogoCRUD<DestinosResiduo, int>
    {
        public DestinosResiduosController()
        {

        }

        public void Crear(DestinosResiduo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.DestinosResiduo.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(DestinosResiduo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.DestinosResiduo.Find(entidad.destino_residuo_id);
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
                var registro = _context.DestinosResiduo.Find(id);
                if (registro != null)
                {
                    _context.DestinosResiduo.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public DestinosResiduo ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.DestinosResiduo.Find(id);
            }
        }

        public List<DestinosResiduo> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.DestinosResiduo.ToList();
            }
        }

        public List<DestinosResiduo> ListarDestinosResiduoActivos()
        {
            using (var _context = new Model.Model())
            {
                return _context.DestinosResiduo
                    .Where(t => t.estado == "A")
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxDestinosResiduos()
        {
            using (var _context = new Model.Model())
            {
                return _context.DestinosResiduo
                    .AsEnumerable()
                    .Select(t => new KeyValuePair<string, string>(t.destino_residuo_id.ToString(), t.nombre))
                    .ToList();
            }
        }
        public List<KeyValuePair<string, string>> ListarComboBoxDestinosResiduosRequierenFolio()
        {
            using (var _context = new Model.Model())
            {
                return _context.DestinosResiduo
                    .Where(t => t.requiere_folio)
                    .AsEnumerable()
                    .Select(t => new KeyValuePair<string, string>(t.destino_residuo_id.ToString(), t.nombre))
                    .ToList();
            }
        }

        public bool ValidarSiDestinoRequiereFolio(int destinoResiduoId)
        {
            using (var _context = new Model.Model())
            {
                var destino = _context.DestinosResiduo.Find(destinoResiduoId);
                return destino != null && destino.requiere_folio;
            }

        }

        public List<KeyValuePair<string, string>> ListarComboBoxDestinosResiduosRequierenFolioActivosYSeleccionado(int _destinoId)
        {
            using (var _context = new Model.Model())
            {
                return _context.DestinosResiduo
                    .Where(t => t.requiere_folio && t.estado == "A" || t.destino_residuo_id == _destinoId)
                    .AsEnumerable()
                    .Select(t => new KeyValuePair<string, string>(t.destino_residuo_id.ToString(), t.nombre))
                    .ToList();
            }
        }
    }
}
