using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class AgricultoresController : ICatalogoCRUD<Agricultores, int>
    {
        public AgricultoresController()
        {
        }

        public void Crear(Agricultores entidad)
        {
            using (var _context = new Model.Model())
            {
                AsignarIdSiCero(ref entidad);
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.Agricultores.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(Agricultores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.Agricultores.Find(entidad.agricultor_id);
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
                var registro = _context.Agricultores.Find(id);
                if (registro != null)
                {
                    _context.Agricultores.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public Agricultores ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.Agricultores.Find(id);
            }
        }

        public List<Agricultores> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.Agricultores.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                var lista = _context.Agricultores
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.agricultor_id.ToString(),
                        g.nombre))
                    .OrderBy(x => x.Value)
                    .ToList();

                // Agregar "Todos" al inicio
                lista.Insert(0, new KeyValuePair<string, string>("", "Todos"));

                return lista;
            }
        }

        private void AsignarIdSiCero(ref Agricultores entidad)
        {
            if (entidad.agricultor_id == 0)
            {
                using (var _context = new Model.Model())
                {
                    // Obtiene el último Id (si no hay registros, devuelve 0)
                    int ultimoId = _context.Agricultores
                                      .OrderByDescending(p => p.agricultor_id)
                                      .Select(p => p.agricultor_id)
                                      .FirstOrDefault();

                    entidad.agricultor_id = ultimoId + 1;
                }
                ;
            }
        }

        public async Task<DataTable> ObtenerReporteCalificacionAgricultorAnalisisCampoAsync(int agricultorId, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_CalificacionAgricultorAnalisisCampo";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@AgricultorId", agricultorId));
                    command.Parameters.Add(new SqlParameter("@FechaInicial", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@FechaFinal", fechaFin));

                    var table = new DataTable();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                    }

                    return table;
                }
            }
        }

        public async Task<(DataTable Summarizado, DataTable DetalleCampo, DataTable DetalleRegistros)> ObtenerReporteCalificacionAgricultorAnalisisCampoDetalleAsync(int agricultorId, DateTime fechaInicio, DateTime fechaFin)
        {
                using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_CalificacionAgricultorAnalisisCampoDetalles";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@AgricultorId", agricultorId));
                    command.Parameters.Add(new SqlParameter("@FechaInicial", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@FechaFinal", fechaFin));

                    var tableSummarizado = new DataTable("Summarizado");
                    var tableDetalleCampo = new DataTable("DetalleCampo");
                    var tableDetalleRegistros = new DataTable("DetalleRegistros");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // 1. Primer Result Set: Summarizado
                        tableSummarizado.Load(reader);

                        // 2. Segundo Result Set: Detalle Campo
                        //if (await reader.NextResultAsync())
                        //{
                        tableDetalleCampo.Load(reader);
                        //}

                        // 3. Tercer Result Set: Detalle Registros
                        //if (await reader.NextResultAsync())
                        //{
                        tableDetalleRegistros.Load(reader);
                        //}
                    }

                    return (tableSummarizado, tableDetalleCampo, tableDetalleRegistros);
                }
            }
        }

    }
}
