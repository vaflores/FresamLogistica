using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.Reports.Reportes.MuestreoCalidad;
using FresamLogistica.ViewModels.Mango;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Controllers.General
{
    internal class MangoMuestreosController : ICatalogoCRUD<MangoMuestreos, int>
    {
        public MangoMuestreosController()
        {
        }
        public void Crear(MangoMuestreos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoMuestreos.Add(entidad);
                _context.SaveChanges();
            }
        }
        public void Actualizar(MangoMuestreos entidad)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MangoMuestreos.Find(entidad.muestreo_id);
                if (registro != null)
                {
                    SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                    _context.Entry(registro).CurrentValues.SetValues(entidad);
                    _context.SaveChanges();
                }
            }
        }
        public void Eliminar(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MangoMuestreos.Find(id);
                if (registro != null)
                {
                    _context.MangoMuestreos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }
        public MangoMuestreos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoMuestreos.Find(id);
            }
        }

        public List<MangoMuestreos> Listar()
        {
            throw new NotImplementedException();
        }

        public int CrearMuestreoConCalidades(MangoMuestreos _muestreo, List<MangoMuestreosCalidades> _listaCalidades)
        {
            using (var _context = new Model.Model())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        SesionActualHelper.RegistrarSesionCrea(ref _muestreo);
                        _context.MangoMuestreos.Add(_muestreo);
                        _context.SaveChanges();
                        EliminarCalidadesPorMuestreo(_muestreo.muestreo_id);
                        for (int i = 0; i < _listaCalidades.Count; i++)
                        {
                            var calidad = _listaCalidades[i];
                            calidad.muestreo_id = _muestreo.muestreo_id;
                            SesionActualHelper.RegistrarSesionCrea(ref calidad);
                            _context.MangoMuestreosCalidades.Add(calidad);
                            _listaCalidades[i] = calidad;
                        }
                        _context.SaveChanges();
                        transaction.Commit();
                        return _muestreo.muestreo_id;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void ActualizarMuestreoConCalidades(MangoMuestreos _muestreo, List<MangoMuestreosCalidades> _listaCalidades)
        {
            using (var _context = new Model.Model())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var registro = _context.MangoMuestreos.Find(_muestreo.muestreo_id);
                        if (registro != null)
                        {
                            SesionActualHelper.RegistrarUsuarioModifica(ref _muestreo);
                            _context.Entry(registro).CurrentValues.SetValues(_muestreo);
                            _context.SaveChanges();
                            EliminarCalidadesPorMuestreo(_muestreo.muestreo_id);
                            for (int i = 0; i < _listaCalidades.Count; i++)
                            {
                                var calidad = _listaCalidades[i];
                                calidad.muestreo_id = _muestreo.muestreo_id;
                                SesionActualHelper.RegistrarSesionCrea(ref calidad);
                                _context.MangoMuestreosCalidades.Add(calidad);
                                _listaCalidades[i] = calidad;
                            }
                            _context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void EliminarMuestreoConCalidades(int muestreoId)
        {
            using (var _context = new Model.Model())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        EliminarCalidadesPorMuestreo(muestreoId);
                        var registro = _context.MangoMuestreos.Find(muestreoId);
                        if (registro != null)
                        {
                            _context.MangoMuestreos.Remove(registro);
                            _context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void EliminarCalidadesPorMuestreo(int muestreoId)
        {
            using (var _context = new Model.Model())
            {
                var calidades = _context.MangoMuestreosCalidades.Where(c => c.muestreo_id == muestreoId).ToList();
                if (calidades.Any())
                {
                    _context.MangoMuestreosCalidades.RemoveRange(calidades);
                    _context.SaveChanges();
                }
            }
        }

        public void ActualizarPromedioDescuentosRecepcion(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                var connection = _context.Database.Connection;
                try
                {
                    if (connection.State != ConnectionState.Open)
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_ActualizarMangoPromedioDescuentosRecepcion";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@recepcionId", _recepcionId));
                        command.Parameters.Add(new SqlParameter("@usuario", SesionActualHelper.UsuarioId));
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public DataTable ConsultarPromedioDescuentosRecepcion(int _recepcionId)
        {
            using (var _context = new Model.Model())
            {
                var connection = _context.Database.Connection;
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_ConsultarMangoPromedioDescuentosRecepcion";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@recepcionId", _recepcionId));

                        var table = new DataTable();

                        using (var reader = command.ExecuteReader())
                        {
                            table.Load(reader);
                        }

                        return table;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void ActualizarPromedioCalidadesRecepcion(int _recepcionId)
        {
             using (var _context = new Model.Model())
            {
                var connection = _context.Database.Connection;
                try
                {
                    if (connection.State != ConnectionState.Open)
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_ActualizarMangoPromedioCalidadesRecepcion";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@recepcionId", _recepcionId));
                        command.Parameters.Add(new SqlParameter("@usuario", SesionActualHelper.UsuarioId));
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<MangoCalidadesRecepcionesViewModel> ConsultarPromedioCalidadesRecepcion(int _recepcionId)
        {
            var resultado = new List<MangoCalidadesRecepcionesViewModel>();

            using (var _context = new Model.Model())
            {
                var connection = _context.Database.Connection;
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_ConsultarMangoPromedioCalidadesRecepcion";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@recepcionId", _recepcionId));
                        command.Parameters.Add(new SqlParameter("@resultadoOpcion", 1));

                        using (var reader = command.ExecuteReader())
                        {
                            var table = new DataTable();
                            table.Load(reader);
                            resultado = new List<MangoCalidadesRecepcionesViewModel>();
                            foreach (DataRow row in table.Rows)
                            {
                                int GetInt(DataRow r, string col) => table.Columns.Contains(col) && r[col] != DBNull.Value ? Convert.ToInt32(r[col]) : 0;
                                decimal GetDecimal(DataRow r, string col) => table.Columns.Contains(col) && r[col] != DBNull.Value ? Convert.ToDecimal(r[col]) : 0;
                                string GetStr(DataRow r, string col) => table.Columns.Contains(col) ? r[col]?.ToString() : null;

                                resultado.Add(new MangoCalidadesRecepcionesViewModel
                                {
                                    CalidadRecepcionId = 0,
                                    CalidadId = GetInt(row, "calidad_id"),
                                    Nombre = GetStr(row, "calidad"),
                                    Valor = GetDecimal(row, "valor"),
                                    AfectaCalificacion = GetInt(row, "afecta_calificacion") == 1,
                                    IncluyeEnReporte = false
                                });
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }

                return resultado;
            }
        }

        public DataTable ObtenerMuestreosPorRecepcion(int _recepcionId)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                try
                {
                    if (connection.State != ConnectionState.Open)
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_DetalleMangoMuestreosPorRecepcion";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@recepcionId", _recepcionId));
                    
                        var table = new DataTable();

                        using (var reader = command.ExecuteReader())
                        {
                            table.Load(reader);
                        }

                        return table;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public decimal ObtenerPromedioMangoChicoPorMuestreosRecepcion(int _recepcionId)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_DetalleMangoMuestreosPorRecepcion";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@recepcionId", _recepcionId));

                        var table = new DataTable();

                        using (var reader = command.ExecuteReader())
                        {
                            table.Load(reader);
                        }

                        int _count = 0;
                        decimal _porcentaje = 0;

                        foreach (DataRow row in table.Rows)
                        {
                            _count++;
                            _porcentaje += Math.Round(Convert.ToDecimal(row["Tamaño chico (≤350 g)"]) * 100 / Convert.ToDecimal(row["peso_caja"]), 2);
                            
                        }

                        return _porcentaje/_count;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<DataTable> ObteneMuestreosPorFechaAsync(DateTime fecha)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                try
                {
                    if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_DetalleMangoMuestreosPorRecepcionesPorDia";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@fecha", fecha));

                        var table = new DataTable();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            table.Load(reader);
                        }

                        return table;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<DataTable> ObteneReporteMuestreosPorRecepcionAsync(int _recepcionId)
        {
            using (var context = new Model.Model())
            {
                var connection = context.Database.Connection;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_ReporteMangoDetalleRecepcionMuestrosCalidades";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@recepcionId", _recepcionId));

                        var table = new DataTable();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            table.Load(reader);
                        }

                        return table;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
