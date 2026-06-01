using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Herramientas
{
    internal class CondicionesCajasController : ICatalogoCRUD<CondicionesCajas, int>
    {
        public CondicionesCajasController()
        {
        }

        public void Crear(CondicionesCajas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CondicionesCajas.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CondicionesCajas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CondicionesCajas.Find(entidad.condicion_caja_id);
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
                var registro = _context.CondicionesCajas.Find(id);
                if (registro != null)
                {
                    _context.CondicionesCajas.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CondicionesCajas ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CondicionesCajas.Find(id);
            }
        }

        public List<CondicionesCajas> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CondicionesCajas.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarCombo()
        {
            using (var _context = new Model.Model())
            {
                var temp = _context.CondicionesCajas
                    .Select(r => new { r.condicion_caja_id, r.nombre }) // traducible a SQL
                    .ToList(); // materializa la consulta

                return temp
                    .Select(x => new KeyValuePair<string, string>(x.condicion_caja_id.ToString(), x.nombre))
                    .ToList();
            }
        }
    }
}
