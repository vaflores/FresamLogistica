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
    public partial class ReporteRecepcionesView : Form
    {
        private readonly TitulosCalidadesController _repo;
        private readonly CiclosResiduosController _repoCiclos = new CiclosResiduosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly RecepcionesController controller = new RecepcionesController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private string nombreElabora = string.Empty;
        private string nombreVerifica = string.Empty;


        public ReporteRecepcionesView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosCalidadesController();
            dtpFechaFin.Checked = false;

            //this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            AgrgarRegistros();
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

        private void AgrgarRegistros()
        {
            //Agregar datos al DataGridView o a la fuente de datos correspondiente.

            dgvRegistros.Rows.Add("Recepción Id", false, "recepcion_id");
            dgvRegistros.Rows.Add("Fecha", false, "fecha");
            dgvRegistros.Rows.Add("Folio Diario", false, "id_folio_diario");
            dgvRegistros.Rows.Add("Agricultor", true, "agricultor");
            dgvRegistros.Rows.Add("Rancho", false, "rancho");
            dgvRegistros.Rows.Add("Sector", false, "sector");
            dgvRegistros.Rows.Add("Variedad", false, "variedad");
            dgvRegistros.Rows.Add("Calidad", false, "calidad");
            dgvRegistros.Rows.Add("Grado", true, "grado_id");
            dgvRegistros.Rows.Add("Descuento", true, "descuento");
            dgvRegistros.Rows.Add("Cajas Recibidas", true, "cajas_recibidas");
            dgvRegistros.Rows.Add("Cajas Procesadas", true, "cajas_procesadas");
            dgvRegistros.Rows.Add("Cajas Pendientes", true, "cajas_pendientes");
            dgvRegistros.Rows.Add("Peso Neto Recibido", false, "peso_neto_recibido");
            dgvRegistros.Rows.Add("Peso Neto Procesado", false, "peso_neto_procesado");
            dgvRegistros.Rows.Add("Peso Neto Pendiente", false, "peso_neto_pendiente");
            dgvRegistros.Rows.Add("Precio Unitario", false, "precio_unitario");
            dgvRegistros.Rows.Add("Importe", false, "importe");
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

                if (string.IsNullOrWhiteSpace(nombreElabora) || string.IsNullOrWhiteSpace(nombreVerifica))
                {
                    MessageBox.Show("Falta seleccionar nombre de responsable y verifica.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var _ciclos = _repoCiclos.ObtenerCicloResiduosPorFecha(dtpFechaInicio.Value);
                CiclosResiduos _ciclo = _repoCiclos.ObtenerPorCicloResiduosId(_ciclos);
                var _titulos = _repo.ObtenerPorCicloId(_ciclos);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;

                Totales totales = TotalCajas(_recepciones);


                List<ReportParameter> parametros = new List<ReportParameter>()
                {
                    new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("nombreEmpresaParam", _empresa.nombre),
                    new ReportParameter("titulo1Param", _titulos == null ? "" : _titulos.titulo1),
                    new ReportParameter("titulo2Param", "Resumen de Recepciones de Producto por Agricultor "),
                    new ReportParameter("titulo3Param", _titulos == null ? "" : _titulos.titulo3),
                    new ReportParameter("fechaReporteParam", !dtpFechaFin.Checked ?
                        dtpFechaInicio.Value.ToString("ddMMyy").ToUpper() :
                        dtpFechaInicio.Value.ToString("ddMMyy").ToUpper() + " AL " + dtpFechaFin.Value.ToString("ddMMyy").ToUpper()),

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
                    new ReportParameter("calidad", ElCampoEsVisible("Calidad")),
                    new ReportParameter("grado", ElCampoEsVisible("Grado")),
                    new ReportParameter("cajasRecibidasParam", ElCampoEsVisible("Cajas Recibidas")),
                    new ReportParameter("cajasProcesadasParam", ElCampoEsVisible("Cajas Procesadas")),
                    new ReportParameter("cajasPendientesParam", ElCampoEsVisible("Cajas Pendientes")),
                    new ReportParameter("pesoNetoRecibidoParam", ElCampoEsVisible("Peso Neto Recibido")),
                    new ReportParameter("pesoNetoProcesadoParam", ElCampoEsVisible("Peso Neto Procesado")),
                    new ReportParameter("pesoNetoPendienteParam", ElCampoEsVisible("Peso Neto Pendiente")),
                    new ReportParameter("precio_unitario", ElCampoEsVisible("Precio Unitario")),
                    new ReportParameter("importe", ElCampoEsVisible("Importe")),
                    new ReportParameter("estado", ElCampoEsVisible("Estado")),
                    new ReportParameter("descuento", ElCampoEsVisible("Descuento")),
                    new ReportParameter("totalCajasRecibidas", totales.CajasRecibidas.ToString("N0")),
                    new ReportParameter("totalCajasProcesadas", totales.CajasProcesadas.ToString("N0")),
                    new ReportParameter("totalCajasPendientes", totales.CajasPendientes.ToString("N0")),
                    new ReportParameter("totalPesoNetoRecibido", totales.PesoNetoRecibido.ToString("N2")),
                    new ReportParameter("totalPesoNetoProcesado", totales.PesoNetoProcesado.ToString("N2")),
                    new ReportParameter("totalPesoNetoPendiente", totales.PesoNetoPendiente.ToString("N2")),

                    new ReportParameter("responsableParam", nombreElabora),
                    new ReportParameter("verificaParam", nombreVerifica)

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
                _dtMaster.Columns.Add("descuento", typeof(string));
                _dtMaster.Columns.Add("cajas_recibidas", typeof(string));
                _dtMaster.Columns.Add("cajas_procesadas", typeof(string));
                _dtMaster.Columns.Add("cajas_pendientes", typeof(string));
                _dtMaster.Columns.Add("peso_neto_recibido", typeof(string));
                _dtMaster.Columns.Add("peso_neto_procesado", typeof(string));
                _dtMaster.Columns.Add("peso_neto_pendiente", typeof(string));
                _dtMaster.Columns.Add("precio_unitario", typeof(string));
                _dtMaster.Columns.Add("importe", typeof(string));
                _dtMaster.Columns.Add("estado", typeof(string));


                foreach (DataRow fila in _recepciones.Rows)
                {
                    DataRow _row = _dtMaster.NewRow();
                    _row["recepcion_id"] = fila["recepcion_id"].ToString();
                    _row["fecha"] = Convert.ToDateTime(fila["fecha"]).ToString("ddMMyy").ToUpper();
                    _row["id_folio_diario"] = fila["id_folio_diario"].ToString();
                    _row["agricultor"] = fila["agricultor"].ToString();
                    _row["rancho"] = fila["rancho"].ToString();
                    _row["sector"] = fila["sector"].ToString();
                    _row["variedad"] = fila["variedad"].ToString();
                    _row["calidad"] = Convert.ToInt32(fila["calidad"]).ToString("N0");
                    _row["grado_id"] = fila["grado_id"].ToString();
                    _row["descuento"] = Convert.ToDecimal(fila["descuento"]).ToString("N2");
                    _row["cajas_recibidas"] = Convert.ToInt32(fila["cajas_recibidas"]).ToString("N0");
                    _row["cajas_pendientes"] = Convert.ToInt32(fila["cajas_pendientes"]).ToString("N0");
                    _row["cajas_procesadas"] = Convert.ToInt32(fila["cajas_procesadas"]).ToString("N0");
                    _row["peso_neto_recibido"] = Convert.ToDecimal(fila["peso_neto_recibido"]).ToString("N2");
                    _row["peso_neto_procesado"] = Convert.ToDecimal(fila["peso_neto_procesado"]).ToString("N2");
                    _row["peso_neto_pendiente"] = Convert.ToDecimal(fila["peso_neto_pendiente"]).ToString("N2");
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

        public Totales TotalCajas(DataTable recepcionesDt)
        {
            Totales totales = new Totales();
            foreach (DataRow fila in recepcionesDt.Rows)
            {
                //Si la fecha es igual al inicio del reporte

                if(Convert.ToDateTime(fila["fecha"]).Date == dtpFechaInicio.Value.Date)
                {
                    totales.CajasRecibidas += Convert.ToInt32(fila["cajas_recibidas"]);
                    totales.CajasProcesadas += Convert.ToInt32(fila["cajas_procesadas"]);
                    totales.PesoNetoRecibido += Convert.ToDecimal(fila["peso_neto_recibido"]);
                    totales.PesoNetoProcesado += Convert.ToDecimal(fila["peso_neto_procesado"]);
                }

                totales.CajasPendientes += Convert.ToInt32(fila["cajas_pendientes"]);
                totales.PesoNetoPendiente += Convert.ToDecimal(fila["peso_neto_pendiente"]);
            }

            return totales;
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

    public class Totales
    {
        public int CajasRecibidas { get; set; }
        public int CajasProcesadas { get; set; }
        public int CajasPendientes { get; set; }
        public decimal PesoNetoRecibido { get; set; }
        public decimal PesoNetoProcesado { get; set; }
        public decimal PesoNetoPendiente { get; set; }
    }
}
