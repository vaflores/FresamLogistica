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
    public partial class PreciosView : Form
    {
        private readonly PreciosController _repo;
        private readonly GradosController _repoGrados = new GradosController();
        private int _IdFrutaSeleccionado;

        const string _columnaSeleccionada = "precio_id";
        public PreciosView(int _idFrutaSeleccionada)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this._IdFrutaSeleccionado = _idFrutaSeleccionada;
            _repo = new PreciosController();

            ConfigurarComboBoxGrados();

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();

        }


        #region EventosFormulario
        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
            ActualizarRegistro(0, cbGrados.SelectedValue.ToString());
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
                ActualizarRegistro(0, cbGrados.SelectedValue.ToString());
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
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.ListarPorFruta(_IdFrutaSeleccionado).Where(p => p.grado_id == cbGrados.SelectedValue.ToString()).ToList());
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void cbGrados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarRegistros();
        }
        #endregion

        #region MetodosPrivados
        private void ConfigurarComboBoxGrados()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbGrados, _repoGrados.ListarComboBoxFruta(_IdFrutaSeleccionado));
        }

        private void CargarRegistros()
        {

            string filtro = cbGrados.Items.Count == 0 ? "Todos" : cbGrados.SelectedValue.ToString();
            dgvRegistros.DataSource = null;

            if (filtro == "Todos")
                dgvRegistros.DataSource = _repo.ListarPorFruta(_IdFrutaSeleccionado);
            else
                dgvRegistros.DataSource = _repo.ListarPorFruta(_IdFrutaSeleccionado).Where(p => p.grado_id == filtro).ToList();


            OrdernarPorFechaInicio();

        }

        private void ActualizarRegistro(int _IdRegistro, string _IdGrado)
        {
            Helper.FormulariosHelper.CargarFormulario(new PrecioView(_IdRegistro, _IdGrado, _IdFrutaSeleccionado), this);
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
            DataGridViewHelper.AddColumn(dgv, "Precio Id", _columnaSeleccionada, 100);
            DataGridViewHelper.AddColumn(dgv, "Grado Id", "grado_id", 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha Inicial", "fecha_inicio", 300);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha Final", "fecha_fin", 300);
            DataGridViewHelper.AddColumnMoneda(dgv, "Importe", "importe", 300);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

        private void OrdernarPorFechaInicio()
        {
            dgvRegistros.Tag = "ASC";
            try
            {
                DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, 2, _repo.ListarPorFruta(_IdFrutaSeleccionado).Where(p => p.grado_id == cbGrados.SelectedValue.ToString()).ToList());
            }
            catch
            { }
            
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
                nombreColumna == "Editar")
            {
                // Obtén el objeto actual (puede ser por ID)
                var idPrecio = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);
                string idGrado = dgvRegistros.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(idPrecio, idGrado);
            }
        }
    }
}
