using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Reportes
{
    internal class ReporteResiduosController
    {

        public ReporteResiduosController()
        {
        }

        public async Task<DataTable> ObtenerReporteResiduosADetalleAsync(DateTime fechaInicio, DateTime fechaFin, string operador = null, string fruta = null)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteResiduosDinamico";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                    command.Parameters.Add(new SqlParameter("@operador",
                        string.IsNullOrEmpty(operador) ? (object)DBNull.Value : operador));

                    command.Parameters.Add(new SqlParameter("@fruta",
                        string.IsNullOrEmpty(fruta) ? (object)DBNull.Value : fruta));

                    var table = new DataTable();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                    }

                    return table;
                }
            }
        }

        public async Task<DataTable> ObtenerReporteResiduosPorMesAsync(DateTime fechaInicio, DateTime fechaFin, string operador = null, string fruta = null)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteResiduosMensual";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                    command.Parameters.Add(new SqlParameter("@fruta",
                        string.IsNullOrEmpty(fruta) ? (object)DBNull.Value : fruta));

                    var table = new DataTable();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                    }

                    return table;
                }
            }
        }

        public async Task<DataTable> ObtenerReporteResiduosTotalesAsync(DateTime fechaInicio, DateTime fechaFin, string operador = null, string fruta = null)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteResiduosMensualPorFruta";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                    command.Parameters.Add(new SqlParameter("@fruta",
                        string.IsNullOrEmpty(fruta) ? (object)DBNull.Value : fruta));

                    var table = new DataTable();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                    }

                    return table;
                }
            }
        }

        public async Task<DataTable> ObtenerReporteResiduosImpresionAsync(DateTime fechaInicio, DateTime fechaFin, string operador = null, string fruta = null)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteResiduosDinamico2";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                    command.Parameters.Add(new SqlParameter("@fruta",
                        string.IsNullOrEmpty(fruta) ? (object)DBNull.Value : fruta));

                    var table = new DataTable();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                    }

                    return table;
                }
            }
        }
    }
}
