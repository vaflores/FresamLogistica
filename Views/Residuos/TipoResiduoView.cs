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
    public partial class TipoResiduoView : Form
    {
        private readonly PreciosController _repo;
        private readonly GradosController _repoGrados = new GradosController();
        private int _IdRegistroSeleccionado;
        private int _IdFrutaSeleccionado;
        public TipoResiduoView(int _idRegistro, string _IdGrado, int _IdFruta)
        {
            InitializeComponent();

            _repo = new PreciosController();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = _idRegistro;
            this._IdFrutaSeleccionado = _IdFruta;

            ConfigurarComboBoxGrados();
            ConfigurarComboBoxEstado();
            cbGrados.SelectedValue = _IdGrado;
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

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtImporte.Text, out decimal valor))
            {
                txtImporte.Text = valor.ToString("C2"); // Formato moneda con 2 decimales
            }
            else
            {
                txtImporte.Text = "0.00"; // Valor por defecto si no es válido
            }
        }
        #endregion


        #region MetodosPrivados

        private void ConfigurarComboBoxGrados()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbGrados, _repoGrados.ListarComboBoxFruta(_IdFrutaSeleccionado) );
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
                    cbGrados.SelectedValue = registro.grado_id;
                    dtpFechaInicio.Value = registro.fecha_inicio;
                    dtpFechaFin.Value = registro.fecha_fin ?? DateTime.Now;
                    txtImporte.Text = registro.importe.ToString("C2");
                    cbEstado.SelectedValue = registro.estado;
                }
                else
                {
                    MessageBox.Show("No se encontró el precio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.Precios
                {
                    precio_id = _IdRegistroSeleccionado,
                    fruta_id = _IdFrutaSeleccionado,
                    grado_id = cbGrados.SelectedValue.ToString(),
                    fecha_inicio = dtpFechaInicio.Value,
                    fecha_fin = dtpFechaFin.Value,
                    importe = decimal.Parse(txtImporte.Text, System.Globalization.NumberStyles.Currency),
                    estado = cbEstado.SelectedValue.ToString()
                };

                if (_IdRegistroSeleccionado == 0)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Precio creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Precio guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            cbGrados.SelectedIndex = 0;
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            txtImporte.Clear();
            cbEstado.SelectedIndex = 0;
        }

        private void EliminarRegistro()
        {
            try
            {

                if (_IdRegistroSeleccionado != 0)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este precio?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Precio eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun precio seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir control keys
            if (char.IsControl(e.KeyChar))
                return;

            // Permitir dígitos
            if (char.IsDigit(e.KeyChar))
                return;

            // Permitir un solo punto decimal
            if (e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
                return;

            // Si nada aplica, bloquear
            e.Handled = true;
        }
    }
}
