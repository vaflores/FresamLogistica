using FresamLogistica.Helper;
using FresamLogistica.Interfaces;
using FresamLogistica.Model;
using FresamLogistica.Reports.Reportes.MuestreoCalidad;
using FresamLogistica.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.General
{
    internal class MuestreosRecepcionesController : ICatalogoCRUD<MuestreosRecepciones, int>
    {
        public MuestreosRecepcionesController()
        {
        }
        public void Crear(MuestreosRecepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MuestreosRecepciones.Add(entidad);
                _context.SaveChanges();
            }
        }
        public void Actualizar(MuestreosRecepciones entidad)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MuestreosRecepciones.Find(entidad.muestreo_id);
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
                var registro = _context.MuestreosRecepciones.Find(id);
                if (registro != null)
                {
                    _context.MuestreosRecepciones.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }
        public MuestreosRecepciones ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MuestreosRecepciones.Find(id);
            }
        }

        public List<MuestreosRecepciones> Listar()
        {
            throw new NotImplementedException();
        }

        public int CrearMuestreoConCalidades(MuestreosRecepciones _muestreo, List<MuestreosCalidadesRecepciones> _listaCalidades)
        {
            using (var _context = new Model.Model())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        SesionActualHelper.RegistrarSesionCrea(ref _muestreo);
                        _context.MuestreosRecepciones.Add(_muestreo);
                        _context.SaveChanges();
                        EliminarCalidadesPorMuestreo(_muestreo.muestreo_id);
                        for (int i = 0; i < _listaCalidades.Count; i++)
                        {
                            var calidad = _listaCalidades[i];
                            calidad.muestreo_id = _muestreo.muestreo_id;
                            SesionActualHelper.RegistrarSesionCrea(ref calidad);
                            _context.MuestreosCalidadesRecepciones.Add(calidad);
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

        public void ActualizarMuestreoConCalidades(MuestreosRecepciones _muestreo, List<MuestreosCalidadesRecepciones> _listaCalidades)
        {
            using (var _context = new Model.Model())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var registro = _context.MuestreosRecepciones.Find(_muestreo.muestreo_id);
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
                                _context.MuestreosCalidadesRecepciones.Add(calidad);
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
                        var registro = _context.MuestreosRecepciones.Find(muestreoId);
                        if (registro != null)
                        {
                            _context.MuestreosRecepciones.Remove(registro);
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
                var calidades = _context.MuestreosCalidadesRecepciones.Where(c => c.muestreo_id == muestreoId).ToList();
                if (calidades.Any())
                {
                    _context.MuestreosCalidadesRecepciones.RemoveRange(calidades);
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
                        command.CommandText = "sp_ActualizarPromedioDescuentosRecepcion";
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
                        command.CommandText = "sp_ConsultarPromedioDescuentosRecepcion";
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
                        command.CommandText = "sp_ActualizarPromedioCalidadesRecepcion";
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

        public List<CalidadesRecepcionesViewModel> ConsultarPromedioCalidadesRecepcion(int _recepcionId)
        {
            var resultado = new List<CalidadesRecepcionesViewModel>();

            using (var _context = new Model.Model())
            {
                var connection = _context.Database.Connection;
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_ConsultarPromedioCalidadesRecepcion";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@recepcionId", _recepcionId));
                        command.Parameters.Add(new SqlParameter("@resultadoOpcion", 1));

                        using (var reader = command.ExecuteReader())
                        {
                            var table = new DataTable();
                            table.Load(reader);
                            resultado = new List<CalidadesRecepcionesViewModel>();
                            foreach (DataRow row in table.Rows)
                            {
                                int GetInt(DataRow r, string col) => table.Columns.Contains(col) && r[col] != DBNull.Value ? Convert.ToInt32(r[col]) : 0;
                                string GetStr(DataRow r, string col) => table.Columns.Contains(col) ? r[col]?.ToString() : null;

                                resultado.Add(new CalidadesRecepcionesViewModel
                                {
                                    CalidadRecepcionId = 0,
                                    CalidadId = GetInt(row, "calidad_id"),
                                    Nombre = GetStr(row, "calidad"),
                                    Valor = GetInt(row, "valor"),
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
                        command.CommandText = "sp_DetalleMuestreosPorRecepcion";
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
                        command.CommandText = "sp_DetalleMuestreosPorRecepcionesPorDia";
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
    }
}
