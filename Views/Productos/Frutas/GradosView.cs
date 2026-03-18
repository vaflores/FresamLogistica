using FresamLogistica.Controllers.Productos;
using FresamLogistica.Helper;
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

namespace FresamLogistica.Views.Productos.Frutas
{
    public partial class GradosView : Form
    {
        private readonly GradosController _repo;
        private int _IdFrutaSeleccionado;

        const string _columnaSeleccionada = "grado_id";
        public GradosView(int idFrutaSeleccionado)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this._IdFrutaSeleccionado = idFrutaSeleccionado;
            _repo = new GradosController();

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, 0, _repo.ListarPorFruta(_IdFrutaSeleccionado));

        }

        #region EventosFormulario
        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
            ActualizarRegistro("");
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

        private void FrutasView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                ActualizarRegistro("");
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

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.ListarPorFruta(_IdFrutaSeleccionado));
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }
        #endregion

        #region MetodosPrivados
        private void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repo.ListarPorFruta(_IdFrutaSeleccionado);
        }

        private void ActualizarRegistro(string _IdRegistro)
        {
            Helper.FormulariosHelper.CargarFormulario(new GradoView(_IdRegistro, _IdFrutaSeleccionado), this);
            CargarRegistros();
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
            DataGridViewHelper.AddColumn(dgv, "Grado Id", _columnaSeleccionada, 100);
            DataGridViewHelper.AddColumn(dgv, "Límite Superior", "limite_superior", 300);
            DataGridViewHelper.AddColumn(dgv, "Límite Inferior", "limite_inferior", 300);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

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
                nombreColumna == "Editar"
                )
            {
                // Obtén el objeto actual (puede ser por ID)
                var id = dgvRegistros.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(id);
            }
        }
    }
}
