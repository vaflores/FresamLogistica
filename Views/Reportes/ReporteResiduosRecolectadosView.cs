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
    public partial class ReporteResiduosRecolectadosView : Form
    {
        private readonly TitulosReporteResiduosController _repo;
        private readonly CiclosResiduosController _repoCiclos = new CiclosResiduosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly FrutasController _repoFrutas = new FrutasController();
        private readonly ReporteResiduosController controller = new ReporteResiduosController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private string nombreElabora = string.Empty;
        private string nombreVerifica = string.Empty;

        private DataTable datosImpresion = new DataTable();

        public ReporteResiduosRecolectadosView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosReporteResiduosController();
            ConfigurarComboBoxFrutas();
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
            ImprimirReporte();
        }


        private void GenerarInformacionReporte()
        {

            try
            {
                ValidarFechas();
                btnGenerar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                GenerarReporteDetalle();
                GenerarReportePorMes();
                GenerarReporteTotales();
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

                DataTable _residuos = await controller.ObtenerReporteResiduosImpresionAsync(
                    dtpFechaInicio.Value,
                dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1),
                    null,
                    cbFrutas.SelectedValue.ToString()
                );

                if (_residuos == null || _residuos.Rows.Count == 0)
                {
                    MessageBox.Show("No existen datos para imprimir en el rango de fechas seleccionado.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(nombreElabora) || string.IsNullOrWhiteSpace(nombreVerifica))
                {
                    MessageBox.Show("Falta seleccionar nombre de responsable y verifica.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                datosImpresion = _residuos;

                
                var _ciclos = _repoCiclos.ObtenerCicloResiduosPorFecha(dtpFechaInicio.Value);
                CiclosResiduos _ciclo = _repoCiclos.ObtenerPorCicloResiduosId(_ciclos);
                var _titulos = _repo.ObtenerPorCicloId(_ciclos);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;


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

                    new ReportParameter("responsableParam", nombreElabora),
                    new ReportParameter("verificaParam", nombreVerifica)
                };


                DataTable _dtMaster = new DataTable("Detalle");

                _dtMaster.Columns.Add("fecha", typeof(string));
                _dtMaster.Columns.Add("operador", typeof(string));
                _dtMaster.Columns.Add("fruta", typeof(string));
                _dtMaster.Columns.Add("tipo_residuo", typeof(string));
                _dtMaster.Columns.Add("cantidad_residuo", typeof(string)); 
                _dtMaster.Columns.Add("total_residuos_fecha", typeof(string));
                _dtMaster.Columns.Add("produccion_dia", typeof(string));
                _dtMaster.Columns.Add("porcentaje_merma", typeof(string));

                foreach (DataRow fila in _residuos.Rows)
                {
                    DataRow row = _dtMaster.NewRow();
                    row["fecha"] = Convert.ToDateTime(fila["fecha"]).ToString("ddMMyy").ToUpper();
                    row["operador"] = fila["operador"];
                    row["fruta"] = fila["fruta"];
                    row["tipo_residuo"] = fila["tipo_residuo"];
                    row["cantidad_residuo"] = fila["cantidad_residuo"];
                    row["total_residuos_fecha"] = fila["total_residuos_fecha"];
                    row["produccion_dia"] = fila["produccion_dia"];
                    row["porcentaje_merma"] = fila["porcentaje_merma"];

                    _dtMaster.Rows.Add(row);
                }


                ReportHelper reporte = new ReportHelper(
                    "Reports/Residuos/rptReporteResiduos.rdlc",
                    parametros,
                    _dtMaster);

                reporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message, "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GenerarReporteDetalle()
        {
            var resultado = await controller.ObtenerReporteResiduosADetalleAsync(
                dtpFechaInicio.Value,
                dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1),
                null,
                cbFrutas.SelectedValue.ToString()
            );

            dgvRegistros.DataSource = null;
            dgvRegistros.AutoGenerateColumns = true;
            dgvRegistros.DataSource = resultado;

            FormatearGrid(dgvRegistros);
        }

        private async void GenerarReportePorMes()
        {
            var resultado = await controller.ObtenerReporteResiduosPorMesAsync(
                dtpFechaInicio.Value,
                dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1),
                null,
                cbFrutas.SelectedValue.ToString()
            );

            dgvMensual.DataSource = null;
            dgvMensual.AutoGenerateColumns = true;
            dgvMensual.DataSource = resultado;

            FormatearGrid(dgvMensual);
        }

        private async void GenerarReporteTotales()
        {
            var resultado = await controller.ObtenerReporteResiduosTotalesAsync(
                dtpFechaInicio.Value,
                dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1),
                null,
                cbFrutas.SelectedValue.ToString()
            );

            dgvTotales.DataSource = null;
            dgvTotales.AutoGenerateColumns = true;
            dgvTotales.DataSource = resultado;

            FormatearGrid(dgvTotales);
        }


        private void FormatearGrid(DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Name != "Fecha" &&
                    col.Name != "Operador" &&
                    col.Name != "Fruta" &&
                    col.Name != "Tipo de Residuo" &&
                    col.Name != "Mes")
                {
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
        private void ConfigurarComboBoxFrutas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFrutas, _repoFrutas.ListarComboBox());
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
            Helper.FormulariosHelper.CargarDialogoFormulario(new TitulosReporteResiduosView());
        }
    }
}
