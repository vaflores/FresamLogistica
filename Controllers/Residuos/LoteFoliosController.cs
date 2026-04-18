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
    internal class LoteFoliosController : ICatalogoCRUD<LoteFolios, int>
    {
        private readonly FoliosController _repoFolios = new FoliosController();
        public LoteFoliosController()
        {

        }

        public void Crear(LoteFolios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.LoteFolios.Add(entidad);
                _context.SaveChanges();
            }

            _repoFolios.CrearFoliosPorLote(entidad.lote_folio_id, entidad.folio_inicio, entidad.folio_fin, entidad.importe / (entidad.folio_fin - entidad.folio_inicio + 1));

        }

        public void Actualizar(LoteFolios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.LoteFolios.Find(entidad.lote_folio_id);
                if (registro != null)
                {
                    entidad.usuario_crea = registro.usuario_crea;
                    entidad.fecha_crea = registro.fecha_crea;
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }

            if (!_repoFolios.VerificarLoteFoliosTieneFoliosUtilizados(entidad.lote_folio_id))
            {
                _repoFolios.EliminarFoliosPorLote(entidad.lote_folio_id);
                _repoFolios.CrearFoliosPorLote(entidad.lote_folio_id, entidad.folio_inicio, entidad.folio_fin, entidad.importe / (entidad.folio_fin - entidad.folio_inicio + 1));
            }

        }

        public void Eliminar(int id)
        {
            _repoFolios.EliminarFoliosPorLote(id);

            using (var _context = new Model.Model())
            {
                var registro = _context.LoteFolios.Find(id);
                if (registro != null)
                {
                    _context.LoteFolios.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public LoteFolios ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.LoteFolios.Find(id);
            }
        }

        public List<LoteFolios> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.LoteFolios.ToList();
            }
        }

        public List<LoteFolios> ListarPorDestinoResiduos(int _destinoResiduoId)
        {
            using (var _context = new Model.Model())
            {
                return _context.LoteFolios
                    .Where(lf => lf.destino_residuo_id == _destinoResiduoId)
                    .ToList();
            }
        }

        public int ObtenerLoteFoliosIdMasAntiguoConFoliosDisponibles(int _destinoResiduoId)
        {
            using (var _context = new Model.Model())
            {
                var loteFolios = _context.LoteFolios
                    .Where(lf => lf.destino_residuo_id == _destinoResiduoId)
                    .OrderBy(lf => lf.fecha_crea)
                    .ToList();
                foreach (var lote in loteFolios)
                {
                    if (_repoFolios.VerificarLoteFoliosTieneFoliosDisponibles(lote.lote_folio_id))
                    {
                        return lote.lote_folio_id;
                    }
                }
                return 0; // Retorna 0 si no se encuentra ningún lote con folios disponibles
            }
        }

    }
}
