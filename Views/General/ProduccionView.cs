using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Residuos;
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

namespace FresamLogistica.Views.General
{
    public partial class ProduccionView : Form
    {
        private readonly ProduccionesController _repo;
        private readonly FrutasController _repoFrutas = new FrutasController();
        private int _IdRegistroSeleccionado;
        private int _IdFrutaSeleccionado;
        public ProduccionView(int _idRegistro, int _idFruta)
        {
            InitializeComponent();

            _repo = new ProduccionesController();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = _idRegistro;
            this._IdFrutaSeleccionado = _idFruta;

            ConfigurarComboBoxFrutas();
            Helper.TextBoxValidarNumerosDecimales.Attach(txtCantidad);
            cbFrutas.SelectedValue = _idFruta.ToString();
            CargarDatosRegistroSeleccionado();
            this.Tag = 30;
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

        private void ConfigurarComboBoxFrutas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFrutas, _repoFrutas.ListarComboBox());
        }


        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado != 0)
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    cbFrutas.SelectedValue = registro.fruta_id.ToString();
                    dtpFecha.Value = registro.fecha;
                    txtCantidad.Text = registro.cantidad.ToString("N2");

                }
                else
                {
                    MessageBox.Show("No se encontró la producción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.Producciones
                {
                    produccion_id = _IdRegistroSeleccionado,
                    fruta_id = _IdFrutaSeleccionado,
                    fecha= dtpFecha.Value.Date,
                    cantidad = Convert.ToDecimal(txtCantidad.Text)
                };

                var existente = _repo.ObtenerProduccionPorFrutaYFecha(registro.fruta_id, registro.fecha);

                if (existente == null)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Producción registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    existente.cantidad = Convert.ToDecimal(txtCantidad.Text);
                    _repo.Actualizar(existente);
                    MessageBox.Show("Producción actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la producción. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            _IdRegistroSeleccionado = 0;
            cbFrutas.SelectedIndex = 0;
            txtCantidad.Clear();
        }

        private void EliminarRegistro()
        {
            try
            {

                if (_IdRegistroSeleccionado != 0)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar la producción?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Producción eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ninguna producción seleccionada para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
