using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
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
    public partial class ReporteRecepcionesView : Form
    {
        private readonly TitulosCalidadesController _repo;
        private readonly CiclosResiduosController _repoCiclos = new CiclosResiduosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly AgricultoresController _repoAgricultores = new AgricultoresController();
        private readonly RecepcionesController controller = new RecepcionesController();

        public ReporteRecepcionesView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosCalidadesController();
            dtpFechaFin.Checked = true;

            //this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            AgrgarRegistros();
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

        private void AgrgarRegistros()
        {
            //Agregar datos al DataGridView o a la fuente de datos correspondiente.
             
            dgvRegistros.Rows.Add("Recepción Id", true, "recepcion_id");
            dgvRegistros.Rows.Add("Fecha", true, "fecha");
            dgvRegistros.Rows.Add("Folio Diario", true, "id_folio_diario");
            dgvRegistros.Rows.Add("Agricultor", true, "agricultor");
            dgvRegistros.Rows.Add("Rancho", false, "rancho");
            dgvRegistros.Rows.Add("Sector", false, "sector");
            dgvRegistros.Rows.Add("Variedad", true, "variedad");
            dgvRegistros.Rows.Add("Calidad", true, "calidad");
            dgvRegistros.Rows.Add("Grado", true, "grado_id");
            dgvRegistros.Rows.Add("Cantidad de Cajas", true, "no_tarimas");
            dgvRegistros.Rows.Add("Peso Neto", true, "peso_neto");
            dgvRegistros.Rows.Add("Precio Unitario", true, "precio_unitario");
            dgvRegistros.Rows.Add("Importe", true, "importe");
            dgvRegistros.Rows.Add("Estado", false, "estado");
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

                DataTable _recepciones = await controller.ObteneReporteRecepcionesAsync(
                    dtpFechaInicio.Value,
                    dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1)
                );

                if (_recepciones == null || _recepciones.Rows.Count == 0)
                {
                    MessageBox.Show("No existen datos para imprimir en el rango de fechas seleccionado.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                                
                var _ciclos = _repoCiclos.ObtenerCicloResiduosPorFecha(dtpFechaInicio.Value);
                CiclosResiduos _ciclo = _repoCiclos.ObtenerPorCicloResiduosId(_ciclos);
                var _titulos = _repo.ObtenerPorCicloId(_ciclos);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;
                

                List<ReportParameter> parametros = new List<ReportParameter>()
                {
                    new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("nombreEmpresaParam", _empresa.nombre),
                    new ReportParameter("titulo1Param", _titulos == null ? "" : _titulos.titulo1),
                    new ReportParameter("titulo2Param", "Resumen de Recepciones de Producto por Agricultor "),
                    new ReportParameter("titulo3Param", _titulos == null ? "" : _titulos.titulo3),
                    new ReportParameter("fechaReporteParam", !dtpFechaFin.Checked ? 
                        dtpFechaInicio.Value.ToString("dd/MM/yyyy").ToUpper() :
                        dtpFechaInicio.Value.ToString("dd/MM/yyyy").ToUpper() + " AL " + dtpFechaFin.Value.ToString("dd/MM/yyyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion),

                    new ReportParameter("recepcion_id", ElCampoEsVisible("Recepción Id")),
                    new ReportParameter("fecha",ElCampoEsVisible("Fecha")),
                    new ReportParameter("id_folio_diario", ElCampoEsVisible("Folio Diario")),
                    new ReportParameter("agricultor", ElCampoEsVisible("Agricultor")),
                    new ReportParameter("rancho", ElCampoEsVisible("Rancho")),
                    new ReportParameter("sector", ElCampoEsVisible("Sector")),
                    new ReportParameter("variedad", ElCampoEsVisible("Variedad")),
                    new ReportParameter("no_tarimas", ElCampoEsVisible("Cantidad de Tarimas")),
                    new ReportParameter("peso_neto", ElCampoEsVisible("Peso Neto")),
                    new ReportParameter("precio_unitario", ElCampoEsVisible("Precio Unitario")),
                    new ReportParameter("importe", ElCampoEsVisible("Importe")),
                    new ReportParameter("estado", ElCampoEsVisible("Estado"))

                };


                DataTable _dtMaster = new DataTable("Recepciones");

                _dtMaster.Columns.Add("recepcion_id", typeof(string));
                _dtMaster.Columns.Add("fecha", typeof(string));
                _dtMaster.Columns.Add("id_folio_diario", typeof(string));
                _dtMaster.Columns.Add("agricultor", typeof(string));
                _dtMaster.Columns.Add("rancho", typeof(string));
                _dtMaster.Columns.Add("sector", typeof(string));
                _dtMaster.Columns.Add("variedad", typeof(string));
                _dtMaster.Columns.Add("calidad", typeof(string));
                _dtMaster.Columns.Add("grado_id", typeof(string));
                _dtMaster.Columns.Add("no_tarimas", typeof(string));
                _dtMaster.Columns.Add("peso_neto", typeof(string));
                _dtMaster.Columns.Add("precio_unitario", typeof(string));
                _dtMaster.Columns.Add("importe", typeof(string));
                _dtMaster.Columns.Add("estado", typeof(string));


                foreach (DataRow fila in _recepciones.Rows)
                {
                    DataRow _row = _dtMaster.NewRow();
                    _row["recepcion_id"] = fila["recepcion_id"].ToString();
                    _row["fecha"] = Convert.ToDateTime(fila["fecha"]).ToString("dd/MM/yyyy");
                    _row["id_folio_diario"] = fila["id_folio_diario"].ToString();
                    _row["agricultor"] = fila["agricultor"].ToString();
                    _row["rancho"] = fila["rancho"].ToString();
                    _row["sector"] = fila["sector"].ToString();
                    _row["variedad"] = fila["variedad"].ToString();
                    _row["calidad"] = Convert.ToInt32(fila["calidad"]).ToString("N0");
                    _row["grado_id"] = fila["grado_id"].ToString();
                    _row["no_tarimas"] = Convert.ToInt32(fila["no_tarimas"]).ToString("N0");
                    _row["peso_neto"] = Convert.ToDecimal(fila["peso_neto"]).ToString("N2");
                    _row["precio_unitario"] = Convert.ToDecimal(fila["precio_unitario"]).ToString("C2");
                    _row["importe"] = Convert.ToDecimal(fila["importe"]).ToString("C2");
                    _row["estado"] = fila["estado"].ToString();

                    _dtMaster.Rows.Add(_row);
                }


                ReportHelper reporte = new ReportHelper(
                    "Reports/Reportes/ReporteRecepciones/rptReporteRecepciones.rdlc",
                    parametros,
                    _dtMaster);

                reporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message, "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ElCampoEsVisible(string nombreCampo)
        {
            foreach (DataGridViewRow row in dgvRegistros.Rows)
            {
                if (row.Cells[0].Value.ToString() == nombreCampo)
                {
                    return ((bool)row.Cells[1].Value) ? "False" : "True";

                }
            }
            return "False";
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

    }
}
