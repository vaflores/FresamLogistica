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
    public partial class GradoView : Form
    {
        private readonly GradosController _repo;
        private string _IdRegistroSeleccionado;
        private int _IdFrutaSeleccionado;
        public GradoView(string IdRegistro, int IdFruta)
        {
            InitializeComponent();
        
            _repo = new GradosController();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = IdRegistro;
            this._IdFrutaSeleccionado = IdFruta;

            ConfigurarComboBoxEstado();
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

        #endregion


        #region MetodosPrivados
        private void ConfigurarComboBoxEstado()
        {
            EstadosHelper.GenerarEstadosActivoInactivo(ref cbEstado);
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (!string.IsNullOrWhiteSpace(_IdRegistroSeleccionado))
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado, _IdFrutaSeleccionado);
                if (registro != null)
                {
                    txtGrado.Text = registro.grado_id.ToString();
                    txtLimiteInferior.Text = registro.limite_inferior.ToString();
                    txtLimiteSuperior.Text = registro.limite_superior.ToString();
                    cbEstado.SelectedValue = registro.estado;
                }
                else
                {
                    MessageBox.Show("No se encontró el grado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.GradosFruta
                {
                    fruta_id = _IdFrutaSeleccionado,
                    grado_id = txtGrado.Text,
                    limite_inferior = Convert.ToInt16(txtLimiteInferior.Text),
                    limite_superior = Convert.ToInt16(txtLimiteSuperior.Text),
                    estado = cbEstado.SelectedValue.ToString()
                };

                if (string.IsNullOrWhiteSpace(_IdRegistroSeleccionado))
                {
                    _repo.CrearOActualizar(registro);
                    MessageBox.Show("Grado creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.CrearOActualizar(registro);
                    MessageBox.Show("Grado guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtGrado.Clear();
            txtLimiteInferior.Clear();
            txtLimiteSuperior.Clear();
            cbEstado.SelectedIndex = 0;
        }

        private void EliminarRegistro()
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(_IdRegistroSeleccionado))
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este grado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado, _IdFrutaSeleccionado);
                        MessageBox.Show("Grado eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun grado seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
