using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Reportes
{
    internal class ResumenRecepcionesProductoAgricultorController
    {

        public async Task<DataTable> ObtenereporteResumenRecepcionesPorAgricultorAsync(DateTime fechaInicio, DateTime fechaFin, string agricultor = null)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.sp_ReporteResumenRecepcionesPorAgricultor";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                    command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                    command.Parameters.Add(new SqlParameter("@agricultorId",
                        string.IsNullOrEmpty(agricultor) ? (object)DBNull.Value : agricultor));

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
