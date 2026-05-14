using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Reportes
{
    internal class ReporteCalidadesRecepcionesController
    {
        public async Task<DataTable> ObtenereporteReporteCalidadesRecepcionesAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteCalidadesRecepciones";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                    var table = new DataTable();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                    }

                    return table;
                }
            }
        }

        public async Task<DataTable> ObtenereporteReporteMuestrasCalidadesRecepcionesAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteCalidadesMuestrasRecepciones";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

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
