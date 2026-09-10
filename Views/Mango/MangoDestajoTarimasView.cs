
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Helper;
using FresamLogistica.Views.Helpers;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Views.Mango
{
    public partial class MangoDestajoTarimasView : Form
    {
        private readonly MangoDestajosController _repo;
        private readonly MangoRecepcionesSalidasController _repoSalidas = new MangoRecepcionesSalidasController();

        private int _IdRegistroSeleccionado;
        private int _destajoIdSeleccionado;
        private DateTime _fechaDestajoSeleccionado = DateTime.Now;
        private int _cicloId;

        public MangoDestajoTarimasView( int destajoIdSeleccionado, DateTime fechaDestajoSeleccionado, int cicloId)
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repo = new MangoDestajosController();

            _destajoIdSeleccionado = destajoIdSeleccionado;
            _fechaDestajoSeleccionado = fechaDestajoSeleccionado;
            _cicloId = cicloId;
            _IdRegistroSeleccionado = 0;

            ConfigurarDataGridViewAprobadas(dgvTarimasAprobadas);
            CargarRegistros();

            this.Tag = "40"; // Asignar un ID de pantalla único para permisos (ajustar según corresponda)

            ValidarPermisos();
        }


        #region EventosFormulario
        private void RecepcionesView_Load(object sender, EventArgs e)
        {
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
           Debug.WriteLine("Función no implementada.");
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistro();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            ImprimirRegistro();
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                ActualizarRegistro();
            }
            else if (e.Control && e.KeyCode == Keys.G && tsbGuardar.Enabled) // Ctrl + G → Guardar
            {
                GuardarRegistro();
            }
            else if (e.Control && e.KeyCode == Keys.E && tsbEliminar.Enabled) // Ctrl + E → Eliminar
            {
                EliminarRegistro();
            }
            else if (e.Control && e.KeyCode == Keys.B && tsbBuscar.Enabled) // Ctrl + B → Buscar
            {
                CargarRegistros();
            }
            else if (e.Control && e.KeyCode == Keys.P && tsbImprimir.Enabled) // Ctrl + P → Imprimir
            {
                ImprimirRegistro();
            }
        }


        private void dgvTarimasAprobadas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirTarimaAprobadaSeleccionado(e, dgvTarimasAprobadas.Columns[e.ColumnIndex].Name);
        }

        private void dgvTarimasAprobadas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirTarimaAprobadaSeleccionado(e, "Procesar");
        }

        #endregion

        #region MetodosPrivados

        private void ValidarPermisos()
        {
            tsbCrear.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Guardar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
        }


        private async void CargarRegistros()
        {
            dgvTarimasAprobadas.DataSource = null;

            if (chkMostrarTarimasPendientes.Checked)
            {
                dgvTarimasAprobadas.DataSource = await _repoSalidas.ConsultaTarimasAprobadasPendientesProcesarAsync(_cicloId);
            }
            else
            { 
                //dgvTarimasAprobadas.DataSource = await _repoSalidas.ConsultaTarimasAprobadasPorDiaAsync(dtpFecha.Value);
            }

            //OrdernarPorFechaInicio();

        }

        private void ActualizarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void GuardarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ConfigurarDataGridViewAprobadas(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Viaje", "mango_recepcion_salida_tarima_id", 50, false);
            DataGridViewHelper.AddColumn(dgv, "Folio Ciclo", "folio_ciclo", 50);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha Recepción", "fecha_recepcion", 150);
            DataGridViewHelper.AddColumn(dgv, "Código", "codigo", 50);
            DataGridViewHelper.AddColumn(dgv, "Distribuidor", "proveedor", 150);
            DataGridViewHelper.AddColumn(dgv, "Predio", "predio", 100);
            DataGridViewHelper.AddColumn(dgv, "Variedad", "variedad", 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha Traspaleo", "fecha_traspaleo", 50);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Número Tarima", "numero_tarima", 50);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Cantidad de Cajas", "cantidad_cajas", 120);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Neto", "peso_neto", 120);
            DataGridViewHelper.AddColumnCheckBox(dgv, "Aprobada", "aprobada", 120, false);

            DataGridViewHelper.AddButtonColumn(dgv, "Procesar", "Procesar", 50);

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void AbrirTarimaAprobadaSeleccionado(DataGridViewCellEventArgs e, string _columnName)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                _columnName == "Procesar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar"))
            {
                // Preguntar si desea continuar con la operacion
                var result = MessageBox.Show("¿Está seguro de que desea procesar esta tarima?", "Confirmar proceso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    // Obtén el objeto actual (puede ser por ID)
                    _IdRegistroSeleccionado = Convert.ToInt32(dgvTarimasAprobadas.Rows[e.RowIndex].Cells[0].Value);

                    //validar tarima seleccionada
                    if (_IdRegistroSeleccionado <= 0)
                    {
                        MessageBox.Show("No se ha seleccionado una tarima válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Lógica de edición (por ejemplo, abrir otro formulario)
                    //Abrir Ventana de Dialogo para capturar hora de proceso
                    DateTime fechaHoraProceso = _fechaDestajoSeleccionado.Date;
                    using (var dialogo = new SeleccionarHoraView())
                    {
                        dialogo.StartPosition = FormStartPosition.CenterScreen;
                        if (dialogo.ShowDialog(this) == DialogResult.OK)
                        {
                            DateTime hora = dialogo.HoraSeleccionada;
                            fechaHoraProceso = fechaHoraProceso.Date + hora.TimeOfDay;
                        }
                        else
                        {
                            return;
                        }
                    }


                    try
                    {
                        _repo.ProcesarTarimaSeleccionada(_IdRegistroSeleccionado, _destajoIdSeleccionado, fechaHoraProceso);
                        CargarRegistros();
                        MessageBox.Show("Tarima procesada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al procesar la tarima: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (e.RowIndex >= 0 &&
                _columnName == "Desaprobar")
            {
                MessageBox.Show("No tiene permiso para desaprobar tarimas.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void crearNuevaRecepcionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void chkMostrarTarimasPendientes_CheckedChanged(object sender, EventArgs e)
        {
            dtpFecha.Enabled = !chkMostrarTarimasPendientes.Checked;
            CargarRegistros();
        }

    }
}
