using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
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
    public partial class MangoInspeccionVehiculosView : Form
    {
        private readonly TitulosInspeccionesController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly MangoRecepcionesController _repoRecepciones = new MangoRecepcionesController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private string nombreElabora = string.Empty;
        private string nombreVerifica = string.Empty;
        public MangoInspeccionVehiculosView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosInspeccionesController();
            dtpFechaFin.Checked = false;

            this.MaximizeBox = false;
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
            ImprimirReporte();
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

        private async void ImprimirReporte()
        {
            try
            {

                if (!ValidarFechas())
                    return;

                DataTable datos;

                if (dtpFechaFin.Checked)
                {
                    datos = await _repoRecepciones.ObteneReporteMangoRecepcionesInspeccionVehiculosPorRangoFechasAsync(dtpFechaInicio.Value, dtpFechaFin.Value);
                }
                else {
                    datos = await _repoRecepciones.ObteneReporteMangoRecepcionesInspeccionVehiculosPorRangoFechasAsync(dtpFechaInicio.Value, dtpFechaInicio.Value);
                }
            
                if(datos.Rows.Count == 0)
                {
                    MessageBox.Show("No existen recepciones a mostrar en el rango de fechas seleccionado.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(nombreElabora) || string.IsNullOrWhiteSpace(nombreVerifica))
                {
                    MessageBox.Show("Falta seleccionar nombre de responsable y verifica.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var _ciclos = _repoCiclos.ObtenerCicloPorFecha(dtpFechaInicio.Value);
                Ciclos _ciclo = _repoCiclos.ObtenerPorId(_ciclos);
                var _titulos = _repo.ObtenerPorCicloId(_ciclos);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;

                List<ReportParameter> parametros = new List<ReportParameter>()
                {
                    new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("nombreEmpresaParam", _empresa.nombre),
                    new ReportParameter("titulo1Param", _titulos == null ? "" : _titulos.titulo1),
                    new ReportParameter("titulo2Param", _titulos == null ? "" : _titulos.titulo2),
                    new ReportParameter("titulo3Param", _titulos == null ? "" : _titulos.titulo3),
                    new ReportParameter("fechaReporteParam", dtpFechaInicio.Value.ToString("dd/MM/yyyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion),

                    new ReportParameter("responsableParam", nombreElabora),
                    new ReportParameter("verificaParam", nombreVerifica)
                };

                #region definir_tabla
                DataTable _dtMaster = new DataTable("Inspeccionesdts");

                _dtMaster.Columns.Add("codigo", typeof(string));
                _dtMaster.Columns.Add("localidad", typeof(string));
                _dtMaster.Columns.Add("predio", typeof(string));
                _dtMaster.Columns.Add("variedad", typeof(string));
                _dtMaster.Columns.Add("folio_ciclo", typeof(string));
                _dtMaster.Columns.Add("peso_neto", typeof(string));
                _dtMaster.Columns.Add("hora", typeof(string));
                _dtMaster.Columns.Add("marca", typeof(string));
                _dtMaster.Columns.Add("color", typeof(string));
                _dtMaster.Columns.Add("placas", typeof(string));
                _dtMaster.Columns.Add("chofer", typeof(string));
                _dtMaster.Columns.Add("limpieza", typeof(string));
                _dtMaster.Columns.Add("materia", typeof(string));
                _dtMaster.Columns.Add("plagas", typeof(string));
                _dtMaster.Columns.Add("malla", typeof(string));
                _dtMaster.Columns.Add("candado", typeof(string));
                _dtMaster.Columns.Add("alergenos", typeof(string));

                #endregion

                foreach (DataRow _rec in datos.Rows)
                {
                    DataRow row = _dtMaster.NewRow();

                    row["codigo"] = _rec.ItemArray[0];
                    row["localidad"] = _rec.ItemArray[1];
                    row["predio"] = _rec.ItemArray[2];
                    row["variedad"] = _rec.ItemArray[3];
                    row["folio_ciclo"] = _rec.ItemArray[4];
                    row["peso_neto"] = Convert.ToDecimal(_rec.ItemArray[5]).ToString("N2");
                    row["hora"] = _rec.ItemArray[6];
                    row["marca"] = _rec.ItemArray[7];
                    row["color"] = _rec.ItemArray[8];
                    row["placas"] = _rec.ItemArray[9];
                    row["chofer"] = _rec.ItemArray[10];
                    row["limpieza"] = _rec.ItemArray[11].ToString() == "1" ? "☑" : "☐";
                    row["materia"] = _rec.ItemArray[12].ToString() == "1" ? "☑" : "☐";
                    row["plagas"] = _rec.ItemArray[13].ToString() == "1" ? "☑" : "☐";
                    row["malla"] = _rec.ItemArray[14].ToString() == "1" ? "☑" : "☐";
                    row["candado"] = _rec.ItemArray[15].ToString() == "1" ? "☑" : "☐";
                    row["alergenos"] = _rec.ItemArray[16].ToString() == "1" ? "☑" : "☐";

                    _dtMaster.Rows.Add(row);
                }

                ReportHelper reporte = new ReportHelper(
                    "Reports/Reportes/MangoInspeccionVehiculo/rptMangoInspeccionVehiculos.rdlc",
                    parametros,
                    _dtMaster);

                reporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message, "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
    }
}
