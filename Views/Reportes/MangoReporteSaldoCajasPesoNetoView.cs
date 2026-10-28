using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using FresamLogistica.Views.Reportes.Titulos;
using Microsoft.Reporting.WinForms;
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

namespace FresamLogistica.Views.Reportes
{
    public partial class MangoReporteSaldoCajasPesoNetoView : Form
    {
        private readonly CiclosController controller = new CiclosController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private string nombreElabora = string.Empty;
        private string nombreVerifica = string.Empty;

        private DataTable datosImpresion = new DataTable();

        public MangoReporteSaldoCajasPesoNetoView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            dtpFechaFin.Checked = true;

            //this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            ValidarPermisos();

            ConfigurarComboBoxFirmas();
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

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarInformacionReporte();
        }
        #endregion

        #region MetodosPrivados
        private void ConfigurarComboBoxFirmas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbElabora, _repoEmpleados.ListarComboBox());
            EstadosHelper.GenerarEstadosDesdeLista(ref cbVerifica, _repoEmpleados.ListarComboBox());
            cbElabora.SelectedIndex = -1;
            cbVerifica.SelectedIndex = -1;
        }

        private void ValidarPermisos()
        {
            // Aquí se pueden agregar validaciones de permisos según el usuario actual.
            // Por ejemplo:
            tsbImprimir.Enabled = btnGenerar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar Reporte");
        }

        private void CargarDatos()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void GuardarDatos()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void BorrarDatos()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void EliminarDatos()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirDatos()
        {
            Debug.WriteLine("Función no implementada.");
            //ImprimirReporte();
        }


        private void GenerarInformacionReporte()
        {

            try
            {
                if(!chkCajas.Checked && !chkPesoNeto.Checked)
                {
                    MessageBox.Show("Seleccione una opción de Saldo a mostrar.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarFechas())
                {
                    return;
                }
                btnGenerar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                GenerarReporteDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar: " + ex.Message);
            }
            finally
            {
                btnGenerar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void GenerarReporteDetalle()
        {
            var resultado = await controller.ObtenerReporteMangoSaldoRecepcionesAsync(
                dtpFechaInicio.Value,
                dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1)
            );

            dgvRegistros.DataSource = null;
            dgvRegistros.AutoGenerateColumns = true;
            dgvRegistros.DataSource = resultado;

            FormatearGrid(dgvRegistros);
        }

        private void FormatearGrid(DataGridView dgv)
        {
            bool mostrarCajas = chkCajas.Checked;
            bool mostrarPesoNeto = chkPesoNeto.Checked;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Name.StartsWith("Cajas", StringComparison.OrdinalIgnoreCase))
                {
                    col.Visible = mostrarCajas;
                    col.DefaultCellStyle.Format = "N0";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (col.Name.StartsWith("Peso Neto", StringComparison.OrdinalIgnoreCase))
                {
                    col.Visible = mostrarPesoNeto;
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
        }

        private bool ValidarFechas()
        {
            if (dtpFechaFin.Checked)
            {
                if (dtpFechaInicio.Value.Date > dtpFechaFin.Value.Date)
                {
                    MessageBox.Show("La fecha de inicio no puede ser mayor a la fecha fin.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        #endregion

        private void InspeccionVehiculosView_Load(object sender, EventArgs e)
        {

        }

        private void cbElabora_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nombreElabora = cbElabora.Text;
            }
            catch (Exception ex)
            { }
        }

        private void cbVerifica_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nombreVerifica = cbVerifica.Text;
            }
            catch (Exception ex)
            { }
        }

        private void tsbTitulos_Click(object sender, EventArgs e)
        {
        }
    }
}
