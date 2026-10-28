using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace FresamLogistica.Views.Reportes
{
    public partial class MangoPromediosRecepcionesView : Form
    {
        private readonly TitulosCalidadesController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly MangoRecepcionesController _repoRecepciones = new MangoRecepcionesController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();
        private readonly MangoMuestreosController _repoMuestreos = new MangoMuestreosController();

        private string nombreElabora = string.Empty;
        private string nombreVerifica = string.Empty;
        public MangoPromediosRecepcionesView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosCalidadesController();
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
                    datos = await _repoRecepciones.ObtenerReporteMangoPromediosRecepcionesPorRangoFechasAsync(dtpFechaInicio.Value, dtpFechaFin.Value);
                }
                else {
                    datos = await _repoRecepciones.ObtenerReporteMangoPromediosRecepcionesPorRangoFechasAsync(dtpFechaInicio.Value, dtpFechaInicio.Value);
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
                    new ReportParameter("fechaReporteParam", dtpFechaInicio.Value.ToString("ddMMyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion),

                    new ReportParameter("responsableParam", nombreElabora),
                    new ReportParameter("verificaParam", nombreVerifica)
                };

                #region definir_tabla
                DataTable _dtMaster = new DataTable("Promediosdt");

                _dtMaster.Columns.Add("recepcion_id", typeof(string));
                _dtMaster.Columns.Add("fecha_recepcion", typeof(string));
                _dtMaster.Columns.Add("distribuidor", typeof(string));
                _dtMaster.Columns.Add("folio_ciclo", typeof(string));
                _dtMaster.Columns.Add("variedad", typeof(string));
                _dtMaster.Columns.Add("desecho", typeof(string));
                _dtMaster.Columns.Add("mango_bueno", typeof(string));
                _dtMaster.Columns.Add("grado_id", typeof(string));
                _dtMaster.Columns.Add("peso_caja", typeof(string));
                _dtMaster.Columns.Add("defecto", typeof(string));
                _dtMaster.Columns.Add("porcentaje_maduro", typeof(string));
                _dtMaster.Columns.Add("aceptado", typeof(string));
                _dtMaster.Columns.Add("materia", typeof(string));
                _dtMaster.Columns.Add("mallugado", typeof(string));
                _dtMaster.Columns.Add("insecto", typeof(string));
                _dtMaster.Columns.Add("mancha", typeof(string));
                _dtMaster.Columns.Add("podrido", typeof(string));
                _dtMaster.Columns.Add("quemadura", typeof(string));
                _dtMaster.Columns.Add("madurado", typeof(string));
                _dtMaster.Columns.Add("chico", typeof(string));
                _dtMaster.Columns.Add("porcentaje_chico", typeof(string));

                #endregion

                foreach (DataRow _rec in datos.Rows)
                {
                    DataRow row = _dtMaster.NewRow();

                    row["recepcion_id"] = _rec.ItemArray[0];
                    row["fecha_recepcion"] = Convert.ToDateTime(_rec.ItemArray[1]).ToString("ddMMyy");
                    row["distribuidor"] = _rec.ItemArray[2];
                    row["folio_ciclo"] = _rec.ItemArray[3];
                    row["variedad"] = _rec.ItemArray[4];
                    row["desecho"] = _rec.ItemArray[8];
                    row["mango_bueno"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[5]), 2).ToString("N2");
                    row["grado_id"] = _rec.ItemArray[6];
                    row["peso_caja"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[7]), 2).ToString("N2");
                    row["defecto"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[9]), 2).ToString("N2");
                    row["porcentaje_maduro"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[10]), 2).ToString("N2");
                    row["aceptado"] = _rec.ItemArray[11].ToString() == "True" ? "☑" : "☐"; 
                    row["materia"] = _rec.ItemArray[12].ToString() == "True" ? "☑" : "☐"; 
                    row["mallugado"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[13]), 2).ToString("N2");
                    row["insecto"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[14]), 2).ToString("N2");
                    row["mancha"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[15]), 2).ToString("N2");
                    row["podrido"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[16]), 2).ToString("N2");
                    row["quemadura"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[17]), 2).ToString("N2");
                    row["madurado"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[18]), 2).ToString("N2");
                    row["chico"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[19]), 2).ToString("N2");
                    row["porcentaje_chico"] = _repoMuestreos.ObtenerPromedioMangoChicoPorMuestreosRecepcion(Convert.ToInt16(_rec.ItemArray[0])).ToString("N2");
                    _dtMaster.Rows.Add(row);

                }

                ReportHelper reporte = new ReportHelper(
                    "Reports/Reportes/MangoPromedios/rptMangoPromedios.rdlc",
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

        private void tsbTitulos_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarDialogoFormulario(new TitulosCalidadesView());
        }
    }
}
