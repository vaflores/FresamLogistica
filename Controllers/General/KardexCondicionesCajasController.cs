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
    internal class KardexCondicionesCajasController : ICatalogoCRUD<KardexCondicionesCajas, int>
    {
        public KardexCondicionesCajasController()
        {
        }

        public void Crear(KardexCondicionesCajas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.KardexCondicionesCajas.Add(entidad);
                _context.SaveChanges();
            }
        }
        public void Actualizar(KardexCondicionesCajas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.KardexCondicionesCajas.Find(entidad.kardex_condicion_caja_id);
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
                var registro = _context.KardexCondicionesCajas.Find(id);
                if (registro != null)
                {
                    _context.KardexCondicionesCajas.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }
        public KardexCondicionesCajas ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.KardexCondicionesCajas.Find(id);
            }
        }

        public List<KardexCondicionesCajas> Listar()
        {
            throw new NotImplementedException();
        }

        public List<CondicionesCajasViewModel> ListarPorKardex(int kardexId)
        {
            using (var _context = new Model.Model())
            {
                var query = from kcc in _context.KardexCondicionesCajas
                            join cc in _context.CondicionesCajas on kcc.condicion_caja_id equals cc.condicion_caja_id
                            where kcc.kardex_id == kardexId
                            orderby cc.nombre
                            select new CondicionesCajasViewModel
                            {
                                KardexCondicionCajaId = kcc.kardex_condicion_caja_id,
                                KardexId = kcc.kardex_id,
                                CondicionCajaId = kcc.condicion_caja_id,
                                Nombre = cc.nombre,
                                Valor = kcc.valor
                            };
                return query.ToList();
            }
        }

        public List<CondicionesCajasViewModel> ListarCondicionesCajasActivas()
        {
            using (var _context = new Model.Model())
            {
                var query = from cc in _context.CondicionesCajas
                            where cc.estado == "A"
                            orderby cc.nombre
                            select new CondicionesCajasViewModel
                            {
                                KardexCondicionCajaId = 0,
                                KardexId = 0,
                                CondicionCajaId = cc.condicion_caja_id,
                                Nombre = cc.nombre,
                                Valor = 0
                            };
                return query.ToList();
            }
        }

        public void AlmacenarCondicionesCajas(int kardexId, List<CondicionesCajasViewModel> condiciones)
        {
            using (var _context = new Model.Model())
            {
                var existentes = _context.KardexCondicionesCajas.Where(kcc => kcc.kardex_id == kardexId).ToList();
                _context.KardexCondicionesCajas.RemoveRange(existentes);
                _context.SaveChanges();
                foreach (var condicion in condiciones)
                {
                    var nuevaCondicion = new KardexCondicionesCajas
                    {
                        kardex_id = kardexId,
                        condicion_caja_id = condicion.CondicionCajaId,
                        valor = condicion.Valor
                    };
                    SesionActualHelper.RegistrarSesionCrea(ref nuevaCondicion);
                    _context.KardexCondicionesCajas.Add(nuevaCondicion);
                }
                _context.SaveChanges();
            }
        }
    }
}
