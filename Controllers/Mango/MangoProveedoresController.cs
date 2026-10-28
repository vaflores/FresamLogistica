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

namespace FresamLogistica.Controllers.Mango
{
    internal class MangoProveedoresController : ICatalogoCRUD<MangoProveedores, int>
    {
        public MangoProveedoresController()
        {
        }

        public void Crear(MangoProveedores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoProveedores.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoProveedores entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoProveedores.Find(entidad.proveedor_id);
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
                var registro = _context.MangoProveedores.Find(id);
                if (registro != null)
                {
                    _context.MangoProveedores.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoProveedores ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoProveedores.Find(id);
            }
        }

        public List<MangoProveedores> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoProveedores.ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                var lista = _context.MangoProveedores
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.proveedor_id.ToString(),
                        g.codigo.ToUpper() + "-" + g.nombre))
                    .OrderBy(x => x.Value)
                    .ToList();

                // Agregar "Todos" al inicio
                //lista.Insert(0, new KeyValuePair<string, string>("", "Todos"));

                return lista;
            }
        }

        public async Task<DataTable> ObtenerReporteCalificacionProveedorAnalisisCampoAsync(int proveedorId, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_CalificacionProveedorAnalisisCampo";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ProveedorId", proveedorId));
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


        public async Task<(DataTable Summarizado, DataTable DetalleCampo, DataTable DetalleRegistros)> ObtenerReporteCalificacionProveedorAnalisisCampoDetalleAsync(int proveedorId, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_CalificacionProveedorAnalisisCampoDetalle";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ProveedorId", proveedorId));
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
