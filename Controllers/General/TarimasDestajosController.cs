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
    internal class TarimasDestajosController : ICatalogoCRUD<TarimasDestajos, int>
    {
        public TarimasDestajosController()
        {
        }

        public void Crear(TarimasDestajos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.TarimasDestajos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public int CrearDestajo(TarimasDestajos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.TarimasDestajos.Add(entidad);
                _context.SaveChanges();
                return entidad.tarima_destajo_id;
            }
        }

        public void Actualizar(TarimasDestajos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.TarimasDestajos.Find(entidad.tarima_destajo_id);
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
                var registro = _context.TarimasDestajos.Find(id);
                if (registro != null)
                {
                    _context.TarimasDestajos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarDestajo(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.TarimasDestajos.Find(id);
                if (registro != null)
                {
                    // Eliminar las tarimas asociadas al destajo
                    var tarimasAsociadas = _context.Tarimas
                        .Where(t => t.tarima_destajo_id == id)
                        .ToList();
                    foreach (var tarima in tarimasAsociadas)
                    {
                        tarima.tarima_destajo_id = null;
                        tarima.fecha_hora_destajo = null;
                    }
                    registro.estado = "I"; // Marcar el destajo como inactivo
                    registro.peso_neto = 0; // Reiniciar el peso neto
                    registro.total_cajas = 0; // Reiniciar el total de cajas
                    _context.SaveChanges();
                }
            }
        }

        public TarimasDestajos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.TarimasDestajos.Find(id);
            }
        }

        public List<TarimasDestajos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.TarimasDestajos.ToList();
            }
        }

        public List<TarimasDestajos> ListarProducciones()
        {
            using (var _context = new Model.Model())
            {
                return _context.TarimasDestajos
                    .OrderByDescending(i=> i.fecha_hora_inicio)
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                var lista = _context.TarimasDestajos
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.tarima_destajo_id.ToString(),
                        g.fecha_hora_inicio.ToShortDateString()))
                    .OrderBy(x => x.Value)
                    .ToList();

                // Agregar "Todos" al inicio
                //lista.Insert(0, new KeyValuePair<string, string>("", "Todos"));

                return lista;
            }
        }

        public int ValidarDestajosDelMismoDia(DateTime _fecha) { 
            using (var _context = new Model.Model())
            {
                DateTime inicio = _fecha.Date;
                DateTime fin = inicio.AddDays(1);

                return _context.TarimasDestajos.Where(d =>
                    d.fecha_hora_inicio >= inicio &&
                    d.fecha_hora_inicio < fin && d.estado == "A").Select(d => d.tarima_destajo_id).FirstOrDefault();
            }
        }

        public async Task<DataTable> ListarRecepcionesSalidasTarimasPorDestajo(int destajoId)
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
                        command.CommandText = "sp_ConsultaTarimasProcesadasPorDestajo";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@destajoId", destajoId));

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

        public void ProcesarTarimaSeleccionada(int tarimaId, int destajoId, DateTime fechaHoraDestajo)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.Tarimas.Find(tarimaId);
                if (registro != null)
                {
                    registro.tarima_destajo_id = destajoId;
                    registro.fecha_hora_destajo = fechaHoraDestajo;
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarTarimaSeleccionada(int tarimaId)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.Tarimas.Find(tarimaId);
                if (registro != null)
                {
                    registro.tarima_destajo_id = null;
                    registro.fecha_hora_destajo = null;
                    _context.SaveChanges();
                }
            }
        }
    }
}
