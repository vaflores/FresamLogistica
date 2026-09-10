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
    internal class MangoDestajosController : ICatalogoCRUD<MangoDestajos, int>
    {
        public MangoDestajosController()
        {
        }

        public void Crear(MangoDestajos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoDestajos.Add(entidad);
                _context.SaveChanges();
            }
        }

        public int CrearDestajo(MangoDestajos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarSesionCrea(ref entidad);
                _context.MangoDestajos.Add(entidad);
                _context.SaveChanges();
                return entidad.mango_destajo_id;
            }
        }

        public void Actualizar(MangoDestajos entidad)
        {
            using (var _context = new Model.Model())
            {
                SesionActualHelper.RegistrarUsuarioModifica(ref entidad);
                var registro = _context.MangoDestajos.Find(entidad.mango_destajo_id);
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
                var registro = _context.MangoDestajos.Find(id);
                if (registro != null)
                {
                    _context.MangoDestajos.Remove(registro);
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarDestajo(int id)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MangoDestajos.Find(id);
                if (registro != null)
                {
                    // Eliminar las tarimas asociadas al destajo
                    var tarimasAsociadas = _context.MangoRecepcionesSalidasTarimas
                        .Where(t => t.mango_destajo_id == id)
                        .ToList();
                    foreach (var tarima in tarimasAsociadas)
                    {
                        tarima.mango_destajo_id = null;
                        tarima.fecha_hora_destajo = null;
                    }
                    registro.estado = "I"; // Marcar el destajo como inactivo
                    registro.peso_neto = 0; // Reiniciar el peso neto
                    registro.total_cajas = 0; // Reiniciar el total de cajas
                    _context.SaveChanges();
                }
            }
        }

        public MangoDestajos ObtenerPorId(int id)
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoDestajos.Find(id);
            }
        }

        public List<MangoDestajos> Listar()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoDestajos.ToList();
            }
        }

        public List<MangoDestajos> ListarProducciones()
        {
            using (var _context = new Model.Model())
            {
                return _context.MangoDestajos
                    .OrderByDescending(i=> i.fecha_hora_inicio)
                    .ToList();
            }
        }

        public List<KeyValuePair<string, string>> ListarComboBox()
        {
            using (var _context = new Model.Model())
            {
                var lista = _context.MangoDestajos
                    .AsEnumerable()
                    .Select(g => new KeyValuePair<string, string>(
                        g.mango_destajo_id.ToString(),
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

                return _context.MangoDestajos.Where(d =>
                    d.fecha_hora_inicio >= inicio &&
                    d.fecha_hora_inicio < fin && d.estado == "A").Select(d => d.mango_destajo_id).FirstOrDefault();
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
                        command.CommandText = "sp_ConsultaMangoTarimasProcesadasPorDestajo";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@mangoDestajoId", destajoId));

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
                var registro = _context.MangoRecepcionesSalidasTarimas.Find(tarimaId);
                if (registro != null)
                {
                    registro.mango_destajo_id = destajoId;
                    registro.fecha_hora_destajo = fechaHoraDestajo;
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarTarimaSeleccionada(int tarimaId)
        {
            using (var _context = new Model.Model())
            {
                var registro = _context.MangoRecepcionesSalidasTarimas.Find(tarimaId);
                if (registro != null)
                {
                    registro.mango_destajo_id = null;
                    registro.fecha_hora_destajo = null;
                    _context.SaveChanges();
                }
            }
        }
    }
}
