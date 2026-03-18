using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Productos
{
    internal class GradosController
    {
        public GradosController()
        {
        }

        public void Crear(GradosFruta entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.GradosFruta.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(GradosFruta entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.GradosFruta.Find(entidad.grado_id, entidad.fruta_id);
                if (registro != null)
                {
                    entidad.usuario_crea = registro.usuario_crea;
                    entidad.fecha_crea = registro.fecha_crea;
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void Eliminar(string gradoId, int frutaId)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.GradosFruta.Find(gradoId, frutaId);
                if (registro != null)
                {
                    _context.GradosFruta.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public GradosFruta ObtenerPorId(string gradoId, int frutaId)
        {
            using (var _context = new Model.Model())
            {
                return _context.GradosFruta.Find(gradoId, frutaId);
            }
        }

        public List<GradosFruta> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.GradosFruta.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxFruta(int _Fruta_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.GradosFruta
                    .Where(g => g.fruta_id == _Fruta_id)
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(g.grado_id.ToString(), g.grado_id.ToString()))
                    .OrderBy(KeyValuePair => KeyValuePair.Key)
                    .ToList();
            }
        }

        public List<GradosFruta> ListarPorFruta(int fruta_id)
        {
            using (var _context = new Model.Model())
            {
                return _context.GradosFruta
                    .Where(v => v.fruta_id == fruta_id)
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref GradosFruta entidad)
        {
            if (entidad.grado_id == "")
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    string ultimoId = _context.GradosFruta
                                      .OrderByDescending(p => p.grado_id)
                                      .Select(p => p.grado_id)
                                      .FirstOrDefault();

                    entidad.grado_id = ultimoId + 1;
                }
                ;
            }
        }

        public void CrearOActualizar(GradosFruta entidad)
        {
            var existente = ObtenerPorId(entidad.grado_id, entidad.fruta_id);
            if (existente == null)
            {
                Crear(entidad);
            }
            else
            {
                Actualizar(entidad);
            }
        }

        public string ObtenerGradoIdPorFrutaIdCalificacion(int _frutaId, int _calificacion)
        {
            using (var _context = new Model.Model())
            {
                return _context.GradosFruta
                    .Where(g => g.fruta_id == _frutaId
                            && g.limite_inferior <= _calificacion
                            && g.limite_superior >= _calificacion)
                    .Select(g => g.grado_id)
                    .FirstOrDefault();
            }
        }
    }
}
