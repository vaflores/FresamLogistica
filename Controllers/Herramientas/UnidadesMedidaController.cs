using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Herramientas
{
    internal class UnidadesMedidaController : ICatalogoCRUD<UnidadesMedida, int>
    {

        public UnidadesMedidaController()
        {
        }

        public void Crear(UnidadesMedida entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.UnidadesMedida.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(UnidadesMedida entidad)
        {
            using(var _context = new Model.Model()) { 
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var unidadMedida = _context.UnidadesMedida.Find(entidad.unidad_medida_id);
                if (unidadMedida != null)
                {
                    entidad.usuario_crea = unidadMedida.usuario_crea;
                    entidad.fecha_crea = unidadMedida.fecha_crea;
                    _context.Entry(unidadMedida).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var unidadMedida = _context.UnidadesMedida.Find(id);
                if (unidadMedida != null)
                {
                    _context.UnidadesMedida.Remove(unidadMedida);
                    _context.SaveChanges();
                }
            }
        }

        public UnidadesMedida ObtenerPorId(int id)
        {
            using(var _context = new Model.Model()) { 
                return _context.UnidadesMedida.Find(id);
            }
        }

        public List<UnidadesMedida> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.UnidadesMedida.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarCombo()
        {
            using (var _context = new Model.Model())
            {
                var temp = _context.UnidadesMedida
                    .Select(um => new { um.unidad_medida_id, um.nombre }) // traducible a SQL
                    .ToList(); // materializa la consulta

                return temp
                    .Select(x => new KeyValuePair<string, string>(x.unidad_medida_id.ToString(), x.nombre))
                    .ToList();
            }
        }

        private void AsignarIdSiCero(ref UnidadesMedida entidad)
        {
            if (entidad.unidad_medida_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.UnidadesMedida
                                      .OrderByDescending(p => p.unidad_medida_id)
                                      .Select(p => p.unidad_medida_id)
                                      .FirstOrDefault();

                    entidad.unidad_medida_id = ultimoId + 1;
                 };
            }
        }

    }
}
