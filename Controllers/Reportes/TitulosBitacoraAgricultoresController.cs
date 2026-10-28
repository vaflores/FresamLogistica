using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Reportes
{
    internal class TitulosBitacoraAgricultoresController : ICatalogoCRUD<CicloBitacoraAgricultores, int>
    {
        public TitulosBitacoraAgricultoresController()
        {
        }

        public void Crear(CicloBitacoraAgricultores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                _context.CicloBitacoraAgricultores.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CicloBitacoraAgricultores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CicloBitacoraAgricultores.Find(entidad.bitacora_id);
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
                var registro = _context.CicloBitacoraAgricultores.Find(id);
                if (registro != null)
                {
                    _context.CicloBitacoraAgricultores.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CicloBitacoraAgricultores ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloBitacoraAgricultores.Find(id);
            }
        }

        public CicloBitacoraAgricultores ObtenerPorCicloId(int cicloId)
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloBitacoraAgricultores
                               .FirstOrDefault(ci => ci.ciclo_id == cicloId);
            }
        }
        public List<CicloBitacoraAgricultores> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CicloBitacoraAgricultores.ToList();
            }
        }

    }
}
