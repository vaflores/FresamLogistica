using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.Productos.Frutas;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Views.Mango
{
    public partial class MangoAprobacionTarimasView : Form
    {
        private readonly MangoRecepcionesSalidasController _repo;

        private int _IdRegistroSeleccionado;

        public MangoAprobacionTarimasView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repo = new MangoRecepcionesSalidasController();

            _IdRegistroSeleccionado = 0;

            ConfigurarDataGridView(dgvRegistros);
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
                ActualizarRegistro(0);
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

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Aprobar");
        }

        private void dgvTarimasAprobadas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirTarimaAprobadaSeleccionado(e, dgvTarimasAprobadas.Columns[e.ColumnIndex].Name);
        }

        private void dgvTarimasAprobadas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirTarimaAprobadaSeleccionado(e, "Aprobar");
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
            dgvRegistros.DataSource = null;
            dgvTarimasAprobadas.DataSource = null;

            if (chkMostrarTarimasPendientes.Checked)
            {
                dgvRegistros.DataSource = await _repo.ConsultaTarimasNoAprobadasAsync();
                dgvTarimasAprobadas.DataSource = await _repo.ConsultaTarimasAprobadasAsync();
            }
            else
            { 
                dgvRegistros.DataSource = await _repo.ConsultaTarimasNoAprobadasPorDiaAsync(dtpFecha.Value);
                dgvTarimasAprobadas.DataSource = await _repo.ConsultaTarimasAprobadasPorDiaAsync(dtpFecha.Value);
            }

            //OrdernarPorFechaInicio();

        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            if(_IdRegistro == 0)
            {
                return;
            }

            try
            {
                _repo.AprobarTarima(_IdRegistro);
                CargarRegistros();
                MessageBox.Show("Tarima aprobada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar la tarima: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarDesaprobarRegistro(int _IdRegistro)
        {
            if (_IdRegistro == 0)
            {
                return;
            }

            //Verificar si la tarima esta procesada
            if(_repo.VetificarSiTarimaEstaProcesada(_IdRegistro))
            {
                MessageBox.Show("No se puede desaprobar la tarima porque ya ha sido procesada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _repo.DesAprobarTarima(_IdRegistro);
                CargarRegistros();
                MessageBox.Show("Tarima aprobada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desaprobar la tarima: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void ConfigurarDataGridView(DataGridView dgv)
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

            DataGridViewHelper.AddButtonColumn(dgv, "Aprobar", "Aprobar", 50);

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

            DataGridViewHelper.AddButtonColumn(dgv, "Desaprobar", "Desaprobar", 50);

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string _columnName)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                _columnName == "Aprobar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar"))
            {
                // Preguntar si desea continuar con la operacion
                var result = MessageBox.Show("¿Está seguro de que desea aprobar esta tarima?", "Confirmar Aprobación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Obtén el objeto actual (puede ser por ID)
                    _IdRegistroSeleccionado = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);

                    // Lógica de edición (por ejemplo, abrir otro formulario)
                    ActualizarRegistro(_IdRegistroSeleccionado);
                }
            }
            else if (e.RowIndex >= 0 &&
                _columnName == "Aprobar")
            {
                MessageBox.Show("No tiene permiso para aprobar tarimas.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void AbrirTarimaAprobadaSeleccionado(DataGridViewCellEventArgs e, string _columnName)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                _columnName == "Desaprobar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar"))
            {
                // Preguntar si desea continuar con la operacion
                var result = MessageBox.Show("¿Está seguro de que desea desaprobar esta tarima?", "Confirmar Desaprobación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    // Obtén el objeto actual (puede ser por ID)
                    _IdRegistroSeleccionado = Convert.ToInt32(dgvTarimasAprobadas.Rows[e.RowIndex].Cells[0].Value);

                    // Lógica de edición (por ejemplo, abrir otro formulario)
                    ActualizarDesaprobarRegistro(_IdRegistroSeleccionado);
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
