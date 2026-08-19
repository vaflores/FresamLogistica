using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Productos;
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

namespace FresamLogistica.Views.General
{
    public partial class CicloView : Form
    {
        private readonly CiclosController _repo;
        private int _IdRegistroSeleccionado;
        public CicloView(int _idRegistro)
        {
            InitializeComponent();

            _repo = new CiclosController();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = _idRegistro;

            ConfigurarComboBoxEstado();
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
            EstadosHelper.GenerarEstadosCiclos(ref cbEstado);
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado != 0)
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtCiclo.Text = registro.ciclo_id.ToString();
                    dtpFechaInicio.Value = registro.fecha_inicio;
                    dtpFechaFin.Value = registro.fecha_fin ?? DateTime.Now;
                    cbEstado.SelectedValue = registro.estado;
                    txtProxRevision.Text = registro.proxima_revision;
                    txtFechaEmision.Text = registro.fecha_emision;
                    txtEdicion.Text = registro.edicion;
                }
                else
                {
                    MessageBox.Show("No se encontró el ciclo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                var registro = new Model.Ciclos
                {
                    ciclo_id = _IdRegistroSeleccionado,
                    fecha_inicio = dtpFechaInicio.Value,
                    estado = cbEstado.SelectedValue.ToString(),
                    proxima_revision = txtProxRevision.Text,
                    fecha_emision = txtFechaEmision.Text,
                    edicion = txtEdicion.Text
                };

                bool fechaInicialPrimeraRecepcion = _repo.ValidarFechaInicialPrimeraRecepcionDelCiclo(registro.ciclo_id,dtpFechaInicio.Value.Date);
                
                if (!fechaInicialPrimeraRecepcion)
                {
                   throw new Exception($"Existe una recepciones con fecha menor a la seleccionada.");
                }

                if(registro.ciclo_id == 0)
                {
                    bool fechaInicialUltimaRecepcion = _repo.ValidarFechaInicialUltimaRecepcion(registro.ciclo_id, dtpFechaInicio.Value.Date);
                    if (!fechaInicialUltimaRecepcion)
                    {
                        throw new Exception($"Existe una recepcion con fecha mayor a la seleccionada.");
                    }
                }

                Ciclos cicloAnterior = _repo.ObtenerCicloAnterior(registro);

                if(cicloAnterior != null)
                {
                    bool fechaInicialUltimaRecepcion = _repo.ValidarFechaInicialUltimaRecepcion(cicloAnterior.ciclo_id, dtpFechaInicio.Value.Date);
                    if (!fechaInicialUltimaRecepcion)
                    {
                        throw new Exception($"Existe una recepcion con fecha mayor a la seleccionada.");
                    }
                }

                if (_IdRegistroSeleccionado == 0)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Ciclo creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Ciclo guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el registro. \n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            _IdRegistroSeleccionado = 0;
            dtpFechaInicio.Value = DateTime.Now;
            cbEstado.SelectedIndex = 0;
            txtProxRevision.Clear();
            txtFechaEmision.Clear();
            txtEdicion.Clear();
        }

        private void EliminarRegistro()
        {
            try
            {

                if (_IdRegistroSeleccionado != 0)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este ciclo?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Ciclo eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun ciclo seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
