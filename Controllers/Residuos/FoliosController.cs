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
    internal class FoliosController : ICatalogoCRUD<Folios, int>
    {
        public FoliosController()
        {

        }

        public void Crear(Folios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Folios.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Folios entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Folios.Find(entidad.folio_id);
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
                var registro = _context.Folios.Find(id);
                if (registro != null)
                {
                    _context.Folios.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Folios ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Folios.Find(id);
            }
        }

        public List<Folios> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Folios.ToList();
            }
        }

        public List<Folios> ListarFoliosPorLote(int _loteFoliosId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Folios
                    .Where(t => t.lote_folio_id == _loteFoliosId)
                    .OrderBy(t => t.numero)
                    .ToList();
            }
        }

        public List<Folios> ListarFoliosVigentesPorLote(int _loteFoliosId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Folios
                    .Where(t => t.utilizado == false)
                    .OrderBy(t => t.numero)
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxFoliosVigentesPorDestino(int _destinoId)
        {
            using (var _context = new Model.Model())
            {
                return (from folio in _context.Folios
                        join lote in _context.LoteFolios
                            on folio.lote_folio_id equals lote.lote_folio_id
                        where lote.destino_residuo_id == _destinoId && !folio.utilizado
                        orderby folio.numero
                        select new
                        {
                            folio.folio_id,
                            folio.numero
                        })
                        .AsEnumerable()
                        .Select(x => new KeyValuePair<string, string>(
                            x.folio_id.ToString(),
                            x.numero.ToString()))
                        .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxFoliosVigentesPorDestinoYSeleccionados(int _destinoId, List<int> _foliosSeleccionados)
        {
            using (var _context = new Model.Model())
            {
                return (from folio in _context.Folios
                        join lote in _context.LoteFolios
                            on folio.lote_folio_id equals lote.lote_folio_id
                        where lote.destino_residuo_id == _destinoId && !folio.utilizado && !_foliosSeleccionados.Contains(folio.folio_id)
                        orderby folio.numero
                        select new
                        {
                            folio.folio_id,
                            folio.numero
                        })
                        .AsEnumerable()
                        .Select(x => new KeyValuePair<string, string>(
                            x.folio_id.ToString(),
                            x.numero.ToString()))
                        .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBoxFoliosVigentesPorDestinoYFolioUsado(int _destinoId, int folioId)
        {
            using (var _context = new Model.Model())
            {
                return (from folio in _context.Folios
                        join lote in _context.LoteFolios
                            on folio.lote_folio_id equals lote.lote_folio_id
                        where lote.destino_residuo_id == _destinoId && !folio.utilizado
                        || folio.folio_id == folioId
                        orderby folio.numero
                        select new
                        {
                            folio.folio_id,
                            folio.numero
                        })
                        .AsEnumerable()
                        .Select(x => new KeyValuePair<string, string>(
                            x.folio_id.ToString(),
                            x.numero.ToString()))
                        .ToList();
            }
        }

        public bool VerificarLoteFoliosTieneFoliosUtilizados(int _loteFoliosId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Folios.Any(t => t.lote_folio_id == _loteFoliosId && t.utilizado);
            }
        }

        public bool VerificarLoteFoliosTieneFoliosDisponibles(int _loteFoliosId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Folios.Any(t => t.lote_folio_id == _loteFoliosId && !t.utilizado);
            }
        }

        public void CrearFoliosPorLote(int _loteFoliosId, int _folioInicial, int _folioFinal, decimal _costo)
        {
            for (int i = 0; i <= (_folioFinal - _folioInicial); i++)
            {
                var folio = new Folios
                {
                    lote_folio_id = _loteFoliosId,
                    numero = _folioInicial + i,
                    costo = _costo,
                    fecha_uso = null,
                    utilizado = false
                };

                Crear(folio);
            }
        }

        public void EliminarFoliosPorLote(int _loteFoliosId)
        {
            using (var _context = new Model.Model())
            {
                var folios = _context.Folios.Where(t => t.lote_folio_id == _loteFoliosId).ToList();
                if (folios.Any())
                {
                    _context.Folios.RemoveRange(folios);
                    _context.SaveChanges();
                }
            }
        }

        public bool MarcarFolioUtilizado(int folioId, DateTime fechaUso)
        {
            using (var _context = new Model.Model())
            {
                var folio = _context.Folios.Find(folioId);
                if (folio != null && !folio.utilizado)
                {
                    folio.utilizado = true;
                    folio.fecha_uso = fechaUso;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool MarcarFolioNoUtilizado(int folioId)
        {
            using (var _context = new Model.Model())
            {
                var folio = _context.Folios.Find(folioId);
                if (folio != null && folio.utilizado)
                {
                    folio.utilizado = false;
                    folio.fecha_uso = null;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public int ObtenerSiguienteFolioDisponible(int loteFoliosId)
        {
            using (var _context = new Model.Model())
            {
                var folio = _context.Folios
                    .Where(t => t.lote_folio_id == loteFoliosId && !t.utilizado)
                    .OrderBy(t => t.numero)
                    .FirstOrDefault();
                return folio != null ? folio.numero : -1; // Retorna -1 si no hay folios disponibles
            }
        }

        public int ContarFoliosDisponibles(int loteFoliosId)
        {
            using (var _context = new Model.Model())
            {
                return _context.Folios.Count(t => t.lote_folio_id == loteFoliosId && !t.utilizado);
            }
        }

        public bool VerificarFolioUtilizado(int folioId)
        {
            using (var _context = new Model.Model())
            {
                var folio = _context.Folios.Find(folioId);
                return folio != null && folio.utilizado;
            }
        }

        public FoliosRegistroResiduoRecolectado ObtenerFolioRecepcionPorId(int registroId, int folioId)
        {
            using (var _context = new Model.Model())
            {
                return _context.FoliosRegistroResiduoRecolectado.Where(t => t.folio_id == folioId && t.registro_residuo_id == registroId).FirstOrDefault();
            }
        }
    }
}
