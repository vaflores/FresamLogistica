using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Helper;
using FresamLogistica.Views.Productos.Frutas;
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

namespace FresamLogistica.Views.Productos
{
    public partial class FrutaView : Form
    {
        private readonly FrutasController _repo;
        private readonly VariedadesController _repoVariedades = new VariedadesController();
        private readonly UnidadesMedidaController _repoUnidadesMedida = new UnidadesMedidaController();
        private int _IdRegistroSeleccionado;

        const string _columnaSeleccionada = "variedad_id";

        public FrutaView(int IdRegistro)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this._IdRegistroSeleccionado = IdRegistro;

            _repo = new FrutasController();

            ConfigurarComboBoxEstado();
            ConfigurarComboBoxUnidadesMedida();

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

        private void tsbGrados_Click(object sender, EventArgs e)
        {
            VisualizarGrados();
        }

        private void tsbPrecios_Click(object sender, EventArgs e)
        {
            VisualizarPrecios();
        }
        private void UnidadMedidaView_KeyDown(object sender, KeyEventArgs e)
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
            else if (e.Control && e.KeyCode == Keys.R && tsbGrados.Enabled) // Ctrl + R → Grados
            {
                VisualizarGrados();
            }
            else if (e.Control && e.KeyCode == Keys.P && tsbPrecios.Enabled) // Ctrl + S → Precios
            {
                VisualizarPrecios();
            }

        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repoVariedades.ListarPorFruta(_IdRegistroSeleccionado));
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarRegistrosGrid();
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            ActualizarRegistro(0);
        }

        #endregion


        #region MetodosPrivados

        private void ValidarPermisos()
        {
            // Aquí se deben implementar las validaciones de permisos según el usuario actual.
            // Por ahora, habilitamos todos los botones.
            tsbCrear.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear") ||
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
            tsbPrecios.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Actualizar Precios");
            tsbGrados.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Actualizar Grados");

            tsbAddGrid.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear") ||
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar");
        }
        private void ConfigurarComboBoxEstado()
        {
            EstadosHelper.GenerarEstadosActivoInactivo(ref cbEstado);
        }

        private void ConfigurarComboBoxUnidadesMedida()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbUnidadesMedida, _repoUnidadesMedida.ListarCombo());
        }
        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado > 0)
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtNombre.Text = registro.nombre;
                    cbUnidadesMedida.SelectedValue = registro.unidad_medida_id.ToString();
                    cbEstado.SelectedValue = registro.estado;

                    CargarRegistrosGrid();
                }
                else
                {
                    MessageBox.Show("No se encontró la unidad de medida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.Frutas
                {
                    fruta_id = _IdRegistroSeleccionado,
                    nombre = txtNombre.Text,
                    unidad_medida_id = Convert.ToInt32(cbUnidadesMedida.SelectedValue),
                    estado = cbEstado.SelectedValue.ToString()
                };
                if (_IdRegistroSeleccionado == 0)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Fruta creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Fruta actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el registro. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            _IdRegistroSeleccionado = 0;
            txtNombre.Clear();
            cbUnidadesMedida.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;

            dgvRegistros.DataSource = null;
        }

        private void EliminarRegistro()
        {
            try
            {

                if (_IdRegistroSeleccionado > 0)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar esta fruta?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Fruta eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ninguna fruta seleccionada para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ActualizarRegistro(int id)
        {
            Helper.FormulariosHelper.CargarFormulario(new VariedadView(id, this._IdRegistroSeleccionado), this);
            CargarRegistrosGrid();
        }

        private void CargarRegistrosGrid()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repoVariedades.ListarPorFruta(_IdRegistroSeleccionado);
        }


        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Variedad Id", _columnaSeleccionada, 100);
            DataGridViewHelper.AddColumn(dgv, "Descripción", "nombre", 300);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

        private void VisualizarGrados()
        {
            Helper.FormulariosHelper.CargarFormulario(new GradosView(_IdRegistroSeleccionado), this);
        }

        private void VisualizarPrecios()
        {
            Helper.FormulariosHelper.CargarFormulario(new PreciosView(_IdRegistroSeleccionado), this);

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
                var id = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(id);
            }
        }
    }
}
