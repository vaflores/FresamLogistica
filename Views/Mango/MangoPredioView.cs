using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
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

namespace FresamLogistica.Views.Mango
{
    public partial class MangoPredioView : Form
    {
        private readonly MangoPrediosController  _repo;
        private readonly MangoLocalidadesController _repoLocalidades = new MangoLocalidadesController();
        private readonly MangoPropietariosController _repoPropietarios = new MangoPropietariosController();
        private int _IdRegistroSeleccionado;
        public MangoPredioView(string _IdRegistro)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.Tag = 39; // Id de pantalla para permisos

            this._IdRegistroSeleccionado = _IdRegistro == "" ? 0 : Convert.ToInt16(_IdRegistro);

            _repo = new MangoPrediosController();

            ConfigurarComboBoxEstatus();
            ConfigurarComboBoxLocalidades();
            ConfigurarComboBoxPropietarios();

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

        private void ConfigurarComboBoxLocalidades()
        {
            var roles = _repoLocalidades.ListarComboBox();
            cbLocalidades.DataSource = roles;
            cbLocalidades.DisplayMember = "Value";
            cbLocalidades.ValueMember = "Key";
            cbLocalidades.SelectedIndex = -1;
        }
        private void ConfigurarComboBoxPropietarios()
        {
            var roles = _repoPropietarios.ListarComboBox();
            cbPropietario.DataSource = roles;
            cbPropietario.DisplayMember = "Value";
            cbPropietario.ValueMember = "Key";
            cbPropietario.SelectedIndex = -1;
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (this._IdRegistroSeleccionado > 0)
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtNombre.Text = registro.nombre;
                    cbLocalidades.SelectedValue = registro.localidad_id.ToString();
                    cbPropietario.SelectedValue = registro.propietario_id.ToString();
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

                var registro = new Model.MangoPredios
                {
                    predio_id = Convert.ToInt16(this._IdRegistroSeleccionado),
                    nombre = txtNombre.Text.Trim(),
                    localidad_id = Convert.ToInt16(cbLocalidades.SelectedValue),
                    propietario_id = Convert.ToInt16(cbPropietario.SelectedValue),
                    estado = cbEstado.SelectedValue.ToString()
                };


                var _usuarioExistente = _repo.ObtenerPorId(registro.predio_id);
                if (_usuarioExistente == null)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Predio creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Predio actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            cbLocalidades.SelectedValue = "A";
            cbEstado.SelectedIndex = -1;
        }

        private void EliminarRegistro()
        {
            try
            {

                if (this._IdRegistroSeleccionado > 0)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este predio?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Predio eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun predio seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
