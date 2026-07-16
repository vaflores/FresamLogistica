using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Reportes;
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

namespace FresamLogistica.Views.Reportes.Titulos
{
    public partial class TitulosMangoEtiquetasView : Form
    {
        private readonly TitulosMangoEtiquetasController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();
        private int _IdRegitroSeleccionado;
        private int _IdCicloSeleccionado;
        public TitulosMangoEtiquetasView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosMangoEtiquetasController();
            _IdRegitroSeleccionado = 0;
            _IdCicloSeleccionado = 1;

            ConfigurarComboBoxEstado();
            ConfigurarComboBoxCiclos();
            CargarDatos();

            cbCiclos.SelectedIndex = -1;

            ValidarPermisos();
        }

        #region EventosFormulario
        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            BorrarDatos();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            EliminarDatos();
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            ImprimirDatos();
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        private void cbCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdCicloSeleccionado = Convert.ToInt32(cbCiclos.SelectedValue);
                CargarDatos();
            }
            catch
            {

            }
        }

        private void EmpresaView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                BorrarDatos();
            }
            else if (e.Control && e.KeyCode == Keys.G && tsbGuardar.Enabled) // Ctrl + G → Guardar
            {
                GuardarDatos();
            }
            else if (e.Control && e.KeyCode == Keys.E && tsbEliminar.Enabled) // Ctrl + E → Eliminar
            {
                EliminarDatos();
            }
            else if (e.Control && e.KeyCode == Keys.B && tsbBuscar.Enabled) // Ctrl + B → Buscar
            {
                CargarDatos();
            }
            else if (e.Control && e.KeyCode == Keys.P && tsbImprimir.Enabled) // Ctrl + P → Imprimir
            {
                ImprimirDatos();
            }
        }

        #endregion

        #region MetodosPrivados

        private void ValidarPermisos()
        {
            // Aquí se deben implementar las validaciones de permisos según el usuario actual.
            // Por ahora, habilitamos todos los botones.
            tsbCrear.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
        }

        private void ConfigurarComboBoxEstado()
        {
            EstadosHelper.GenerarEstadosActivoInactivo(ref cbEstado);
        }

        private void ConfigurarComboBoxCiclos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbCiclos, _repoCiclos.ListarCombo());
        }

        private void CargarDatos()
        {
            var datos = _repo.ObtenerPorCicloId(_IdCicloSeleccionado);
            if (datos != null)
            {
                _IdRegitroSeleccionado = datos.mango_id;
                txtTitulo01.Text = datos.titulo1;
                txtTitulo02.Text = datos.titulo2;
                txtTitulo03.Text = datos.titulo3;
                cbEstado.SelectedValue = datos.estado;
            }
            else
            {
                BorrarDatos();
            }

        }

        private void GuardarDatos()
        {
            try
            {
                var empresa = new Model.CicloMangos
                {
                    ciclo_id = Convert.ToInt32(cbCiclos.SelectedValue),
                    mango_id = _IdRegitroSeleccionado,
                    titulo1 = txtTitulo01.Text.Trim(),
                    titulo2 = txtTitulo02.Text.Trim(),
                    titulo3 = txtTitulo03.Text.Trim(),
                    estado = cbEstado.SelectedValue.ToString()
                };
                var existente = _repo.ObtenerPorId(_IdRegitroSeleccionado);
                if (existente != null)
                {
                    _repo.Actualizar(empresa);
                    MessageBox.Show("Datos del titulo actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Crear(empresa);
                    MessageBox.Show("Datos del titulo guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BorrarDatos()
        {
            txtTitulo01.Clear();
            txtTitulo02.Clear();
            txtTitulo03.Clear();
            cbEstado.SelectedIndex = 0;
        }

        private void EliminarDatos()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirDatos()
        {
            Debug.WriteLine("Función no implementada.");
        }
        #endregion

    }
}
