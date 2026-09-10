using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.General;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.General.Agricultores;
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

namespace FresamLogistica.Views.General
{
    public partial class TarimasDestajosView : Form
    {
        private readonly TarimasDestajosController _repo;

        private int _destajoIdSeleccionado;
        private int _responsableIdSeleccionado;
        private int _verificaIdSeleccionado;
        private const string _columnaSeleccionada = "tarima_destajo_id";

        public TarimasDestajosView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TarimasDestajosController();

            ConfigurarDataGridView(dgvRegistros);

            CargarDatosRegistroSeleccionado();

            ValidarPermisos();
        }

        #region EventosFormulario

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarDatosRegistroSeleccionado();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
            BorrarCampos();
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

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                BorrarCampos();
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
                CargarDatosRegistroSeleccionado();
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

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.ListarProducciones());
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarRegistrosGrid();
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            _destajoIdSeleccionado = 0;
            _responsableIdSeleccionado = 0;
            _verificaIdSeleccionado = 0;
            ActualizarRegistro();
        }

        #endregion


        #region MetodosPrivados
        private void ValidarPermisos()
        {
            // Aquí se deben implementar las validaciones de permisos según el usuario actual.
            // Por ahora, habilitamos todos los botones.
            tsbCrear.Enabled = false;
            tsbAddGrid.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Guardar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
        }

        private void CargarDatosRegistroSeleccionado()
        {
            var registro = _repo.ListarProducciones();
            if (registro != null)
            {

                CargarRegistrosGrid();
            }
            else
            {
                MessageBox.Show("No se encontrarón sectores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void GuardarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void BorrarCampos()
        {
            dgvRegistros.DataSource = null;
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ActualizarRegistro()
        {
            Helper.FormulariosHelper.CargarFormulario(new TarimasDestajoView(_destajoIdSeleccionado, _responsableIdSeleccionado, _verificaIdSeleccionado), this);
            CargarRegistrosGrid();
        }

        private void CargarRegistrosGrid()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repo.ListarProducciones();
        }


        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Destajo Id", _columnaSeleccionada, 100, false);
            DataGridViewHelper.AddColumn(dgv, "Fecha y hora de inicio", "fecha_hora_inicio", 150);
            DataGridViewHelper.AddColumn(dgv, "Fecha y hora de cierre", "fecha_hora_termino", 150);
            DataGridViewHelper.AddColumn(dgv, "Peso Neto", "peso_neto", 100);
            DataGridViewHelper.AddColumn(dgv, "Total de cajas", "total_cajas", 100);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);
            DataGridViewHelper.AddColumn(dgv, "Responsable", "empleado_responsable_id", 100,false);
            DataGridViewHelper.AddColumn(dgv, "Verifica", "empleado_verifico_id", 100,false);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);
        }

        #endregion

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string nombreColumna)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                nombreColumna == "Editar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar")
                )
            {
                // Obtén el objeto actual (puede ser por ID)
                _destajoIdSeleccionado = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);
                _responsableIdSeleccionado = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[6].Value);
                _verificaIdSeleccionado = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[7].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro();
            }
            else if (e.RowIndex >= 0 &&
                nombreColumna == "Editar")
            {
                MessageBox.Show("No tiene permiso para editar registros.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
