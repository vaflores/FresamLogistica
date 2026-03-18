using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.General.Agricultores;
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

namespace FresamLogistica.Views.General
{
    public partial class AgricultorView : Form
    {
        private readonly AgricultoresController _repo;
        private readonly RanchosController _repoRanchos = new RanchosController();
        private readonly EstadosController _estadosRepo = new EstadosController();
        private readonly CiudadesController _ciudadesRepo = new CiudadesController();

        private int _IdRegistroSeleccionado;
        private int _IdEstadoSeleccionado;
        private int _IdCiudadSeleccionado;

        const string _columnaSeleccionada = "rancho_id";
        public AgricultorView(int _registroId)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = _registroId;

            _repo = new AgricultoresController();

            ConfigurarComboBoxEstatus();
            ConfigurarComboBoxEstados();
            ConfigurarComboBoxCiudades();

            Helper.TextBoxValidarEnteros.Attach(txtId);

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
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repoRanchos.ListarPorAgricultor(_IdRegistroSeleccionado));
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarRegistrosGrid();
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            ActualizarRegistro(0);
        }

        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEstado.SelectedIndex == 0)
                return;
            _IdEstadoSeleccionado = Convert.ToInt32(cbEstado.SelectedValue);
            ConfigurarComboBoxCiudades();
        }

        private void cbCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCiudad.SelectedIndex == 0)
                return;
            _IdCiudadSeleccionado = Convert.ToInt32(cbCiudad.SelectedValue);
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

            tsbAddGrid.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear") ||
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar");
        }

        private void ConfigurarComboBoxEstatus()
        {
            EstadosHelper.GenerarEstadosActivoInactivo(ref cbEstatus);
        }

        private void ConfigurarComboBoxEstados()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbEstado, _estadosRepo.ListarComboBox());
        }

        private void ConfigurarComboBoxCiudades()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbCiudad, _ciudadesRepo.ListarComboBoxPorEstado(_IdEstadoSeleccionado));
        }
        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado > 0)
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtId.Text = registro.agricultor_id.ToString();
                    txtNombre.Text = registro.nombre;
                    txtRfc.Text = registro.rfc;
                    cbEstatus.SelectedValue = registro.estado;
                    txtCalleNum.Text = registro.calle;
                    txtColonia.Text = registro.colonia;
                    cbEstado.SelectedValue = registro.estado_id.ToString();
                    cbCiudad.SelectedValue = registro.ciudad_id.ToString();
                    txtTel01.Text = registro.telefono1;
                    txtTel02.Text = registro.telefono2;
                    txtCP.Text = registro.zip_code.ToString();

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

                var registro = new Model.Agricultores
                {
                    agricultor_id = _IdRegistroSeleccionado,
                    nombre = txtNombre.Text,
                    rfc = txtRfc.Text,
                    calle = txtCalleNum.Text,
                    colonia = txtColonia.Text,
                    estado_id = _IdEstadoSeleccionado,
                    ciudad_id = _IdCiudadSeleccionado,
                    zip_code = Convert.ToInt32(txtCP.Text),
                    estado = cbEstatus.SelectedValue.ToString(),
                    telefono1 = txtTel01.Text,
                    telefono2 = txtTel02.Text
                };

                var _existe = _repo.ObtenerPorId(_IdRegistroSeleccionado);

                if (_existe == null)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Agricultor creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas actualizar el agricultor?", "Confirmar guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Actualizar(registro);
                        MessageBox.Show("Agricultor actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
            txtId.Text = "0";
            txtNombre.Clear();
            cbCiudad.SelectedIndex = 0;
            cbEstatus.SelectedIndex = 0;

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
                        MessageBox.Show("Agricultor eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ninguna agricultor seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Helper.FormulariosHelper.CargarFormulario(new RanchoView(id, _IdRegistroSeleccionado), this);
            CargarRegistrosGrid();
        }

        private void CargarRegistrosGrid()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repoRanchos.ListarPorAgricultor(_IdRegistroSeleccionado);
        }


        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Rancho Id", _columnaSeleccionada, 100);
            DataGridViewHelper.AddColumn(dgv, "Descripción", "nombre", 300);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);
        }

        #endregion

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            _IdRegistroSeleccionado = string.IsNullOrEmpty(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string nombreColumna)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                nombreColumna == "Editar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar"))
            {
                // Obtén el objeto actual (puede ser por ID)
                var id = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(id);
            }
            else if (e.RowIndex >= 0 &&
                nombreColumna == "Editar")
            {
                MessageBox.Show("No tiene permiso para editar registros.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
