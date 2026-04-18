using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
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

namespace FresamLogistica.Views.Residuos
{
    public partial class EmpresaGestoraResiduosView : Form
    {
        private readonly EmpresaGestoraResiduosController _repo;
        private readonly OperadoresResiduosController _repoOperadores = new OperadoresResiduosController();
        
        private int _IdRegistroSeleccionado;
        private int _operadorIdSeleccionado;
        public EmpresaGestoraResiduosView(int _idRegistro)
        {
            InitializeComponent();

            _repo = new EmpresaGestoraResiduosController();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = _idRegistro;
            this._operadorIdSeleccionado = 0;

            ConfigurarComboBoxEstado();
            HabilitarControlesGrid(false);

            Helper.TextBoxValidarEnteros.Attach(txtTelefono);
            CargarDatosRegistroSeleccionado();

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();

            this.Tag = 25;
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

        private void FormView_KeyDown(object sender, KeyEventArgs e)
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
        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repoOperadores.ListarOperadoresResiduoPorEmpresa(this._IdRegistroSeleccionado));
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            ActualizarRegistro(0);
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarRegistros();
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

        }

        private void ConfigurarComboBoxEstado()
        {
            EstadosHelper.GenerarEstadosActivoInactivo(ref cbEstado);
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado != 0)
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtNombre.Text = registro.nombre;
                    txtRFC.Text = registro.RFC;
                    txtLicenciaSCT.Text = registro.licenciaSCT;
                    txtLicenciaAmbiental.Text = registro.licenciaEcologica;
                    txtTelefono.Text = registro.telefono;
                    chkEsPersonaMoral.Checked = registro.es_persona_moral;
                    cbEstado.SelectedValue = registro.estado;

                    HabilitarControlesGrid(true);
                }
                else
                {
                    MessageBox.Show("No se encontró la empresa gestora de residuo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string nombreColumna)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                nombreColumna == "Editar")
            {
                // Obtén el objeto actual (puede ser por ID)
                var idTipoResiduo = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(idTipoResiduo);
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.EmpresaGestoraDeResiduos
                {
                    empresa_gestora_id = _IdRegistroSeleccionado,
                    nombre = txtNombre.Text.Trim(),
                    RFC = txtRFC.Text.Trim(),
                    licenciaSCT = txtLicenciaSCT.Text.Trim(),
                    licenciaEcologica = txtLicenciaAmbiental.Text.Trim(),
                    telefono = txtTelefono.Text.Trim(),
                    es_persona_moral = chkEsPersonaMoral.Checked,
                    estado = cbEstado.SelectedValue.ToString()
                };

                if (_IdRegistroSeleccionado == 0)
                {
                    this._IdRegistroSeleccionado = _repo.CrearEmpresa(registro);
                    MessageBox.Show("Empresa Gestora de residuo creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarControlesGrid(true);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Empresa Gestora de residuo guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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
            cbEstado.SelectedIndex = 0;
            CargarRegistros();
            HabilitarControlesGrid(false);
        }

        private void EliminarRegistro()
        {
            try
            {

                if (_IdRegistroSeleccionado != 0)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar esta empresa gestora de residuo?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Empresa Gestora de residuo eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ninguna empresa gestora de residuo seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void CargarRegistros()
        {
            dgvRegistros.DataSource = _repoOperadores.ListarOperadoresResiduoPorEmpresa(this._IdRegistroSeleccionado);
        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            Helper.FormulariosHelper.CargarFormulario(new OperadorResiduosView(_IdRegistro, this._IdRegistroSeleccionado), this);
            CargarRegistros();
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Operador Id", "operador_residuo_id", 100);
            DataGridViewHelper.AddColumn(dgv, "Nombre", "nombre", 600);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

        private void HabilitarControlesGrid(bool habilitar)
        {
            tsbAddGrid.Enabled = habilitar;
            tsbSyncGrid.Enabled = habilitar;
        }

        #endregion

    }
}
