using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class SectoresController : ICatalogoCRUD<Sectores, int>
    {
        public SectoresController()
        {
        }

        public void Crear(Sectores entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Sectores.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Sectores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Sectores.Find(entidad.sector_id);
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
                var registro = _context.Sectores.Find(id);
                if (registro != null)
                {
                    _context.Sectores.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Sectores ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Sectores.Find(id);
            }
        }

        public List<Sectores> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Sectores.ToList();
            }
        }
        public List<SectoresVariedadesViewModel> ListarPorRanchoCiclo(int _ranchoId, int _cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Sectores
                    .Join(_context.Variedades,
                          sector => sector.variedad_id,
                          variedad => variedad.variedad_id,
                          (sector, variedad) => new SectoresVariedadesViewModel{ 
                                sector_id = sector.sector_id,
                                superficie = sector.superficie,
                                variedad_id = variedad.variedad_id,
                                nombre_variedad = variedad.nombre,
                                estado = sector.estado,
                                rancho_id = sector.id_rancho,
                                ciclo_id = sector.id_ciclo,
                                nombre = sector.nombre,
                                fruta_id = _context.Frutas
                                                    .Where(f => f.fruta_id == variedad.id_fruta)
                                                    .Select(f => f.fruta_id)
                                                    .FirstOrDefault(),
                                nombre_fruta = _context.Frutas
                                                    .Where(f => f.fruta_id == variedad.id_fruta)
                                                    .Select(f => f.nombre)
                                                    .FirstOrDefault()
                          })
                    .Where(a => a.rancho_id == _ranchoId && a.ciclo_id == _cicloId)
                    .OrderBy(r => r.ciclo_id)
                    .ToList();
            }
        }

        public List<KeyValuePair<string,string>> ListarPorRanchoCicloComboBox(int _ranchoId, int _cicloId)
        {
            List<SectoresVariedadesViewModel> _list = ListarPorRanchoCiclo(_ranchoId,_cicloId);

            return _list
                    .Select(v => new KeyValuePair<string, string>(v.sector_id.ToString(), v.nombre))
                    .ToList();
        }

        private void AsignarIdSiCero(ref Sectores entidad)
        {
            if (entidad.sector_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Sectores
                                      .OrderByDescending(p => p.sector_id)
                                      .Select(p => p.sector_id)
                                      .FirstOrDefault();

                    entidad.sector_id = ultimoId + 1;
                }
                ;
            }
        }
    }
}
