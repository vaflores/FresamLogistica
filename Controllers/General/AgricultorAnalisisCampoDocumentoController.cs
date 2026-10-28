using FresamLogistica.Helper;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class AgricultorAnalisisCampoDocumentoController
    {
        public AgricultorAnalisisCampoDocumentoController()
        {
        }
        public void Crear(AgricultoresAnalisisCampoDocumentos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.AgricultoresAnalisisCampoDocumentos.Add(entidad);
                _context.SaveChanges();
            }
        }
        public void Actualizar(AgricultoresAnalisisCampoDocumentos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.AgricultoresAnalisisCampoDocumentos.Find(entidad.agricultor_analisis_campo_documento_id);
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
                var registro = _context.AgricultoresAnalisisCampoDocumentos.Find(id);
                if (registro != null)
                {
                    _context.AgricultoresAnalisisCampoDocumentos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public List<AgricultoresAnalisisCampoDocumentos> ObtenerPorAnalisisCampoId(int analisisCampoId)
        {
            using (var _context = new Model.Model())
            {
                //No consultar el campo contenido para no traer datos binarios grandes
                return _context.AgricultoresAnalisisCampoDocumentos
                    .Where(x => x.agricultor_analisis_campo_id == analisisCampoId)
                    .Select(x => new
                    {
                        x.agricultor_analisis_campo_documento_id,
                        x.agricultor_analisis_campo_id,
                        x.nombre_archivo,
                        x.tamano_bytes,
                        x.fecha_crea,
                        x.usuario_crea,
                        x.fecha_modifica,
                        x.usuario_modifica
                    })
                    .AsEnumerable()
                    .Select(x => new AgricultoresAnalisisCampoDocumentos
                    {
                        agricultor_analisis_campo_documento_id = x.agricultor_analisis_campo_documento_id,
                        agricultor_analisis_campo_id = x.agricultor_analisis_campo_id,
                        nombre_archivo = x.nombre_archivo,
                        contenido = null,
                        //tamaño a megabites
                        tamano_bytes = x.tamano_bytes,
                        fecha_crea = x.fecha_crea,
                        usuario_crea = x.usuario_crea,
                        fecha_modifica = x.fecha_modifica,
                        usuario_modifica = x.usuario_modifica
                    })
                    .ToList();
            }
        }

        //Obtener el contenido del registro por id, para descargar el archivo
        public AgricultoresAnalisisCampoDocumentos ObtenerContenidoPorIdAnalisisCampo(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.AgricultoresAnalisisCampoDocumentos
                    .Where(x => x.agricultor_analisis_campo_documento_id == id)
                    .FirstOrDefault();
            }
        }
    }
}
