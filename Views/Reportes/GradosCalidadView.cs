using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Reportes;
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

namespace FresamLogistica.Views.Reportes
{
    public partial class GradosCalidadView : Form
    {
        private readonly TitulosGradosController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly RecepcionesController _repoRecepciones = new RecepcionesController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private string nombreElabora = string.Empty;
        private string nombreVerifica = string.Empty;

        public GradosCalidadView()
        {
            InitializeComponent();
            this.KeyPreview = true;

            _repo = new TitulosGradosController();
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

        private void ImprimirReporte()
        {
            try
            {

                if (!ValidarFechas())
                    return;
                List<RecepcionesViewModel> _recepciones;

                if (dtpFechaFin.Checked)
                {
                    _recepciones = _repoRecepciones.ObtenerRecepcionesPorRangoFechas(dtpFechaInicio.Value, dtpFechaFin.Value);
                }
                else
                {
                    _recepciones = _repoRecepciones.ObtenerRecepcionesPorFecha(dtpFechaInicio.Value);
                }

                if (_recepciones.Count == 0)
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
                    new ReportParameter("fechaReporteParam", dtpFechaInicio.Value.ToString("ddMMyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion),
                    new ReportParameter("responsableParam", nombreElabora),
                    new ReportParameter("verificaParam", nombreVerifica)
                };

                DataTable _dtMaster = new DataTable("recepcionesDataSet");

                _dtMaster.Columns.Add("RecepcionId", typeof(string));
                _dtMaster.Columns.Add("AgricultorId", typeof(string));
                _dtMaster.Columns.Add("AgricultorNombre", typeof(string));
                _dtMaster.Columns.Add("RanchoId", typeof(string));
                _dtMaster.Columns.Add("RanchoNombre", typeof(string));
                _dtMaster.Columns.Add("SectorId", typeof(string));
                _dtMaster.Columns.Add("SectorNombre", typeof(string));
                _dtMaster.Columns.Add("Fecha", typeof(string));
                _dtMaster.Columns.Add("Hora", typeof(string));
                _dtMaster.Columns.Add("TotalCajas", typeof(string));
                _dtMaster.Columns.Add("Aceptado", typeof(string));
                _dtMaster.Columns.Add("Materia", typeof(string));
                _dtMaster.Columns.Add("Grado", typeof(string));

                DataTable _dtDetail = new DataTable("condicionesDataSet");

                _dtDetail.Columns.Add("RecepcionId", typeof(string));
                _dtDetail.Columns.Add("CondicionId", typeof(string));
                _dtDetail.Columns.Add("CondicionNombre", typeof(string));
                _dtDetail.Columns.Add("Valor", typeof(string));

                foreach (var _rec in _recepciones)
                {
                    if (_rec.Estado == "I")
                        continue;
                    DataRow row = _dtMaster.NewRow();
                    row["RecepcionId"] = _rec.RecepcionId.ToString();
                    row["AgricultorId"] = _rec.AgriculorId.ToString();
                    row["AgricultorNombre"] = _rec.AgricultorNombre.ToUpper();
                    row["RanchoId"] = _rec.RanchoId.ToString();
                    row["RanchoNombre"] = _rec.RanchoNombre.ToUpper();
                    row["SectorId"] = _rec.SectorId.ToString();
                    row["SectorNombre"] = _rec.SectorNombre.ToUpper();
                    row["Fecha"] = _rec.Fecha.ToString("ddMMyy").ToUpper();
                    row["Hora"] = _rec.Fecha.ToString("hh:mm tt").ToUpper();
                    row["TotalCajas"] = _rec.TarimasInfo != null ? _rec.TarimasInfo.Sum(t => t.cajas).ToString() : "0";
                    row["Aceptado"] = _rec.Descuentos.aceptado ? "☑   Aceptado" : "☐   Aceptado";
                    row["Materia"] = _rec.Descuentos.materia ? "☑   Materia Extraña" : "☐   Materia Extraña";
                    row["Grado"] = _rec.GradoId;
                    _dtMaster.Rows.Add(row);

                    foreach (var _c in _rec.CalidadesInfo)
                    {
                        if (!_c.IncluyeEnReporte)
                            continue;
                        DataRow _row = _dtDetail.NewRow();
                        _row["RecepcionId"] = _rec.RecepcionId;
                        _row["CondicionId"] = _c.CalidadId.ToString();
                        _row["CondicionNombre"] = _c.Nombre.ToString();
                        _row["Valor"] = _c.Valor.ToString();

                        _dtDetail.Rows.Add(_row);
                    }

                    foreach (var _c in _rec.CalidadesActivas)
                    {
                        if (!_c.IncluyeEnReporte)
                            continue;
                        DataRow _row = _dtDetail.NewRow();
                        _row["RecepcionId"] = _rec.RecepcionId;
                        _row["CondicionId"] = _c.CalidadId.ToString();
                        _row["CondicionNombre"] = _c.Nombre.ToString();
                        _row["Valor"] = _c.Valor.ToString();

                        _dtDetail.Rows.Add(_row);
                    }
                }

                ReportMasterDetailHelper reporte = new
                    ReportMasterDetailHelper("Reports/Reportes/GradosCalidad/rptGradosCalidades.rdlc",
                    parametros, _dtMaster, _dtDetail,
                    "rptCriterioAceptacion",
                    "condicionesDataSet",
                    "recepcionId",
                    "RecepcionId");

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

        private void ConfigurarComboBoxFirmas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbElabora, _repoEmpleados.ListarComboBox());
            EstadosHelper.GenerarEstadosDesdeLista(ref cbVerifica, _repoEmpleados.ListarComboBox());
            cbElabora.SelectedIndex = -1;
            cbVerifica.SelectedIndex = -1;
        }
        #endregion

        private void cbElabora_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nombreElabora = cbElabora.Text;
            }
            catch (Exception ex)
            {}
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
            Helper.FormulariosHelper.CargarDialogoFormulario(new TitulosGradosView());
        }
    }
}
