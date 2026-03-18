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
    public partial class UsuarioView : Form
    {
        private readonly UsuariosController _repo;
        private readonly RolesController _repoRoles = new RolesController();
        private string _IdRegistroSeleccionado;
        public UsuarioView(string _IdRegistro)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = _IdRegistro;

            _repo = new UsuariosController();

            ConfigurarComboBoxEstatus();
            ConfigurarComboBoxRoles();

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

        private void ConfigurarComboBoxRoles()
        {
            var roles = _repoRoles.ListarComboBox();
            cbRol.DataSource = roles;
            cbRol.DisplayMember = "Value";
            cbRol.ValueMember = "Key";
            cbRol.SelectedIndex = -1;
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (!string.IsNullOrWhiteSpace(this._IdRegistroSeleccionado))
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtUsuario.Text = registro.usuario_id;
                    txtPassword.Text = Helper.PasswordHelper.Decrypt(registro.password, Constantes.claveSecreta);
                    cbRol.SelectedValue = registro.rol_id;
                    cbEstado.SelectedValue = registro.estado;
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.Usuarios
                {
                    usuario_id = txtUsuario.Text.Trim(),
                    password = Helper.PasswordHelper.Encrypt(txtPassword.Text.Trim(),Constantes.claveSecreta),
                    rol_id = cbRol.SelectedValue.ToString(),
                    estado = cbEstado.SelectedValue.ToString()
                };

                if(txtPassword.Text.Trim().Length < 8)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var _usuarioExistente = _repo.ObtenerPorId(registro.usuario_id);
                if (_usuarioExistente == null)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Usuario creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            _IdRegistroSeleccionado = "";
            txtUsuario.Clear();
            txtPassword.Clear();
            cbRol.SelectedValue = "A";
            cbEstado.SelectedIndex = -1;
        }

        private void EliminarRegistro()
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(this._IdRegistroSeleccionado))
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Usuario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun usuario seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
