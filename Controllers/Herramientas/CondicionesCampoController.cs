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
    internal class CondicionesCampoController : ICatalogoCRUD<CatTiposAnalisisCampo, int>
    {
        public CondicionesCampoController()
        {
        }

        public void Crear(CatTiposAnalisisCampo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.CatTiposAnalisisCampo.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(CatTiposAnalisisCampo entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.CatTiposAnalisisCampo.Find(entidad.tipo_analisis_campo_id);
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
                var registro = _context.CatTiposAnalisisCampo.Find(id);
                if (registro != null)
                {
                    _context.CatTiposAnalisisCampo.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public CatTiposAnalisisCampo ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.CatTiposAnalisisCampo.Find(id);
            }
        }

        public List<CatTiposAnalisisCampo> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.CatTiposAnalisisCampo.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarCombo()
        {
            using (var _context = new Model.Model())
            {
                var temp = _context.CatTiposAnalisisCampo
                    .Select(r => new { r.tipo_analisis_campo_id, r.nombre }) // traducible a SQL
                    .ToList(); // materializa la consulta

                return temp
                    .Select(x => new KeyValuePair<string, string>(x.tipo_analisis_campo_id.ToString(), x.nombre))
                    .ToList();
            }
        }


    }
}
