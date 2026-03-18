using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.General;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace FresamLogistica.Views.General
{
    public partial class SectorView : Form
    {
        private readonly SectoresController _repo;
        private readonly FrutasController _repoFrutas = new FrutasController();
        private readonly VariedadesController _repoVariedades = new VariedadesController();

        private int _IdRegistroSeleccionado;
        private int _IdRanchoSeleccionado;
        private int _IdFrutaSeleccionada;
        private int _IdVarieradSeleccionada;
        private int _IdCicloSeleccionado;

        public SectorView(int _idRegistro, int _idCiclo, int _idRancho, int _idFruta, int _idVariedad)
        {
            InitializeComponent();

            _repo = new SectoresController();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = _idRegistro;
            this._IdCicloSeleccionado = _idCiclo;
            this._IdRanchoSeleccionado = _idRancho;
            this._IdFrutaSeleccionada = _idFruta;
            this._IdVarieradSeleccionada = _idVariedad;

            ConfigurarComboBoxEstado();
            ConfigurarComboBoxFrutas();
            ConfigurarComboBoxVariedades();

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

        private void cbFruta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdFrutaSeleccionada = Convert.ToInt32(cbFruta.SelectedValue);
                ConfigurarComboBoxVariedades();
            }
            catch
            {

            }
        }

        private void cbVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdVarieradSeleccionada = Convert.ToInt32(cbVariedad.SelectedValue);
            }
            catch
            {

            }
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

        private void ConfigurarComboBoxEstado()
        {
            EstadosHelper.GenerarEstadosActivoInactivo(ref cbEstado);
        }

        private void ConfigurarComboBoxFrutas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFruta, _repoFrutas.ListarComboBox());
        }

        private void ConfigurarComboBoxVariedades()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbVariedad, _repoVariedades.ListarPorFrutaComboBox(_IdFrutaSeleccionada));
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado != 0)
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtNombre.Text = registro.nombre;
                    txtSuperficie.Text = registro.superficie.ToString();
                    cbFruta.SelectedValue = _IdFrutaSeleccionada.ToString();
                    cbVariedad.SelectedValue = registro.variedad_id.ToString();
                    cbEstado.SelectedValue = registro.estado;
                }
                else
                {
                    MessageBox.Show("No se encontró el sector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.Sectores
                {
                    sector_id = _IdRegistroSeleccionado,
                    nombre = txtNombre.Text,
                    superficie = Convert.ToInt32(txtSuperficie.Text),
                    id_rancho = _IdRanchoSeleccionado,
                    id_ciclo = _IdCicloSeleccionado,
                    variedad_id = Convert.ToInt32(cbVariedad.SelectedValue),
                    estado = cbEstado.SelectedValue.ToString()
                };


                if (_IdRegistroSeleccionado == 0)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Sector creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Sector guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            cbFruta.SelectedIndex = 0;
        }

        private void EliminarRegistro()
        {
            try
            {

                if (_IdRegistroSeleccionado != 0)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este sector?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Sector eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun sector seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
