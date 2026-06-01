using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
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

namespace FresamLogistica.Views.Configuracion
{
    public partial class EmpleadoView : Form
    {
        private readonly EmpleadosController _repo;
        private readonly PuestosController _repoPuestos = new PuestosController();
        private string _IdRegistroSeleccionado;
        public EmpleadoView(string _IdRegistro)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.Tag = 39; // Id de pantalla para permisos

            this._IdRegistroSeleccionado = _IdRegistro == "" ? "0" : _IdRegistro;

            _repo = new EmpleadosController();

            ConfigurarComboBoxEstatus();
            ConfigurarComboBoxPuestos();

            CargarDatosRegistroSeleccionado();
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

        }
        #endregion


        #region MetodosPrivados

        private void ConfigurarComboBoxEstatus()
        {
            EstadosHelper.GenerarEstadosRecepciones(ref cbEstado);
        }

        private void ConfigurarComboBoxPuestos()
        {
            var roles = _repoPuestos.ListarComboBox();
            cbPuestos.DataSource = roles;
            cbPuestos.DisplayMember = "Value";
            cbPuestos.ValueMember = "Key";
            cbPuestos.SelectedIndex = -1;
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (this._IdRegistroSeleccionado != "0")
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtNombre.Text = registro.nombre;
                    txtApellidoPaterno.Text = registro.apellido_paterno;
                    txtApellidoMaterno.Text = registro.apellido_materno;
                    cbPuestos.SelectedValue = registro.puesto_id.ToString();
                    cbEstado.SelectedValue = registro.estado;
                }
                else
                {
                    MessageBox.Show("No se encontró el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.Empleados
                {
                    empleado_id = Convert.ToInt16(this._IdRegistroSeleccionado),
                    nombre = txtNombre.Text.Trim(),
                    apellido_paterno = txtApellidoPaterno.Text.Trim(),
                    apellido_materno = txtApellidoMaterno.Text.Trim(),
                    puesto_id = Convert.ToInt16(cbPuestos.SelectedValue),
                    estado = cbEstado.SelectedValue.ToString()
                };


                var _usuarioExistente = _repo.ObtenerPorId(registro.empleado_id.ToString());
                if (_usuarioExistente == null)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Empleado creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Empleado actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            _IdRegistroSeleccionado = "0";
            txtNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            cbPuestos.SelectedValue = "A";
            cbEstado.SelectedIndex = -1;
        }

        private void EliminarRegistro()
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(this._IdRegistroSeleccionado))
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este empleado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Empleado eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun empleado seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        #endregion

    }
}
