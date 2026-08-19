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
    internal class MangoRecepcionesSalidasController : ICatalogoCRUD<MangoRecepcionesSalidas, int>
    {
        public MangoRecepcionesSalidasController()
        {
        }

        public void Crear(MangoRecepcionesSalidas entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoRecepcionesSalidas.Add(entidad);
                _context.SaveChanges();
            }
        }

        public void Actualizar(MangoRecepcionesSalidas entidad)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MangoRecepcionesSalidas.Find(entidad.mango_recepcion_salida_id);
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
                var registro = _context.MangoRecepcionesSalidas.Find(id);
                if (registro != null)
                {
                    _context.MangoRecepcionesSalidas.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public MangoRecepcionesSalidas ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoRecepcionesSalidas.Find(id);
            }
        }

        public List<MangoRecepcionesSalidas> Listar()
        {
            throw new NotImplementedException();
        }

        public async Task<DataTable> ConsultarMangoRecepcionesSalidasPorFecha(DateTime _fecha)
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
                        command.CommandText = "sp_ConsultarMangoRecepcionesSalidasPorFecha";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@fecha", _fecha));

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

        public async Task<DataTable> ConsultarMangoRecepcionesSalidasPorCiclo(int _cicloId)
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
                        command.CommandText = "sp_ConsultarMangoRecepcionesSalidasPorCiclo";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@cicloId", _cicloId));

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

        public async Task<DataTable> ConsultarMangoRecepcionesSalidasDetallePorCiclo(int _cicloId)
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
                        command.CommandText = "sp_ConsultarMangoRecepcionesSalidasDetallePorCiclo";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@cicloId", _cicloId));

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

        public async Task<DataTable> ConsultarMangoRecepcionesSalidasPorRecepcion(int recepcionId)
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
                        command.CommandText = "sp_ConsultarMangoRecepcionesSalidasPorRecepcion";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@recepcionId", recepcionId));

                        DataTable table = new DataTable();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            table.Load(reader);
                        }

                        return table;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void RecalcularMangoRecepcionesSalidas(int recepcionId)
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
                        command.CommandText = "sp_RecalcularMangoRecepcionesSalidas";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@recepcion_id", recepcionId));

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
