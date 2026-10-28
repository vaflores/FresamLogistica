using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
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
    public partial class ReporteCalificacionProveedorView : Form
    {
        private readonly TitulosCalificacionesController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly MangoProveedoresController _repoProveedores = new MangoProveedoresController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private string nombreElabora = string.Empty;
        private string nombreVerifica = string.Empty;

        private decimal sumaTotal = 0;

        public ReporteCalificacionProveedorView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosCalificacionesController();
            ConfigurarComboBoxAgricultores();
            dtpFechaFin.Checked = true;

            //this.MaximizeBox = false;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;

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
            ImprimirReporte();
        }

        private void GenerarInformacionReporte()
        {
            try
            {

                if (Convert.ToInt16(cbAgricultores.SelectedValue.ToString()) < 1)
                {
                    MessageBox.Show("Seleccione un agricultor.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (!ValidarFechas()){
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

        
        private async void ImprimirReporte()
        {
            try
            {

                if (!ValidarFechas())
                    return;

                var ds = await _repoProveedores.ObtenerReporteCalificacionProveedorAnalisisCampoDetalleAsync(Convert.ToInt32(cbAgricultores.SelectedValue), dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);

                DataTable dtResumen = ds.Summarizado;      // o ds.Tables[0]
                DataTable dtCampo = ds.DetalleCampo;       // o ds.Tables[1]
                DataTable dtRegistros = ds.DetalleRegistros; // o ds.Tables[2]

                if (dtCampo == null || dtCampo.Rows.Count == 0 ||
                    dtRegistros == null || dtRegistros.Rows.Count == 0)
                {
                    MessageBox.Show("No existen datos para imprimir en el rango de fechas seleccionado.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(nombreElabora) || string.IsNullOrWhiteSpace(nombreVerifica))
                {
                    MessageBox.Show("Falta seleccionar nombre de responsable y verifica.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int periodoId = ObtenerPeriodoIdDelGrid();

                if (periodoId == 0)
                {
                    MessageBox.Show("Seleccione el periodo a mostrar.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var _ciclos = _repoCiclos.ObtenerCicloPorFecha(dtpFechaInicio.Value);
                Ciclos _ciclo = _repoCiclos.ObtenerPorCicloId(_ciclos);
                var _titulos = _repo.ObtenerPorCicloId(_ciclos);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;

                sumaTotal = 0;

                List<ReportParameter> parametros = new List<ReportParameter>()
                {
                    new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("nombreEmpresaParam", _empresa.nombre),
                    new ReportParameter("titulo1Param", _titulos.titulo1.ToString()),
                    new ReportParameter("titulo2Param", _titulos.titulo2.ToString()),
                    new ReportParameter("titulo3Param", _titulos.titulo3.ToString()),
                    new ReportParameter("fechaReporteParam", dtpFechaInicio.Value.ToString("ddMMyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion),

                    new ReportParameter("proveedorParam", ObtenerNombreProveedor(Convert.ToInt32(cbAgricultores.SelectedValue))),

                    new ReportParameter("campoCuestionario", ObtenerValorReporte("C01", periodoId, dtCampo, dtRegistros)), //C01
                    new ReportParameter("campoAnalisis", ObtenerValorReporte("C02", periodoId, dtCampo, dtRegistros)),  //C02

                    new ReportParameter("plantaAnalisis", ObtenerValorReporte("P01", periodoId, dtCampo, dtRegistros)), //P01
                    new ReportParameter("plantaAlergenos", ObtenerValorReporte("P02", periodoId, dtCampo, dtRegistros)), //P02
                    new ReportParameter("plantaLimpieza", ObtenerValorReporte("P03", periodoId, dtCampo, dtRegistros)), //P03
                    new ReportParameter("plantaMateria", ObtenerValorReporte("P04", periodoId, dtCampo, dtRegistros)), //P04
                    new ReportParameter("plantaPlagas", ObtenerValorReporte("P05", periodoId, dtCampo, dtRegistros)), //P05
                    new ReportParameter("total", sumaTotal.ToString("N0")),

                    new ReportParameter("responsableParam", nombreElabora),
                    new ReportParameter("verificaParam", nombreVerifica)
                };

                ReportHelper reporte = new ReportHelper(
                    "Reports/Reportes/Calificaciones/rptReporteCalificaciones.rdlc",
                    parametros);

                reporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message, "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string ObtenerNombreProveedor(int proveedorId)
        {
            var proveedor = _repoProveedores.ObtenerPorId(proveedorId);
            return proveedor != null ? proveedor.nombre : string.Empty;
        }
        public int ObtenerPeriodoIdDelGrid()
        {
            if (dgvRegistros.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvRegistros.SelectedRows[0];
                if (selectedRow.Cells["Periodo Id"] != null && int.TryParse(selectedRow.Cells["Periodo Id"].Value.ToString(), out int periodoId))
                {
                    return periodoId;
                }
            }
            return 0; // Retorna 0 si no se encuentra el periodo_id
        }

        public string ObtenerValorReporte(string codigo, int periodo, DataTable campo, DataTable registros)
        {
            foreach (DataRow row in campo.Rows)
            {
                if (row["Periodo Id"].ToString() != periodo.ToString())
                {
                    continue;
                }
                if (row["Codigo"].ToString() == codigo)
                {
                    if (row["Cumplido"].ToString() == "1")
                    {
                        sumaTotal += Convert.ToDecimal(row["Calificacion"].ToString());
                        return "☑";
                    }
                }
            }

            foreach (DataRow row in registros.Rows)
            {
                if (row["Periodo Id"].ToString() != periodo.ToString())
                {
                    continue;
                }
                if (row["Codigo"].ToString() == codigo)
                {
                    if (row["Cumplido"].ToString() == "True")
                    {
                        sumaTotal += Convert.ToDecimal(row["Calificacion"].ToString());
                        return "☑";
                    }
                }
            }
            return "☐";
        }

        private async void GenerarReporteDetalle()
        {
            var resultado = await _repoProveedores.ObtenerReporteCalificacionProveedorAnalisisCampoAsync(
                Convert.ToInt16(cbAgricultores.SelectedValue.ToString()),
                dtpFechaInicio.Value,
                dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1)
            );

            dgvRegistros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRegistros.ReadOnly = true;
            dgvRegistros.AllowUserToAddRows = false;
            dgvRegistros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvRegistros.DataSource = null;
            dgvRegistros.AutoGenerateColumns = true;
            dgvRegistros.DataSource = resultado;

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

        private void ConfigurarComboBoxAgricultores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbAgricultores, _repoProveedores.ListarComboBox());
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
            Helper.FormulariosHelper.CargarDialogoFormulario(new TitulosCalificacionesView());
        }
    }
}
