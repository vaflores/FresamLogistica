using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Controllers.Residuos;
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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace FresamLogistica.Views.Reportes
{
    public partial class ReporteMuestrasCalidadesRecepcionesView : Form
    {
        private readonly TitulosCalidadesController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly ReporteCalidadesRecepcionesController controller = new ReporteCalidadesRecepcionesController();

        private DataTable datosImpresion = new DataTable();

        public ReporteMuestrasCalidadesRecepcionesView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosCalidadesController();
            dtpFechaFin.Checked = true;

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

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

                DataTable _residuos = await controller.ObtenereporteReporteMuestrasCalidadesRecepcionesAsync(
                    dtpFechaInicio.Value,
                    dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1)
                );

                if (_residuos == null || _residuos.Rows.Count == 0)
                {
                    MessageBox.Show("No existen datos para imprimir en el rango de fechas seleccionado.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                datosImpresion = _residuos;


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
                    new ReportParameter("titulo2Param", "Grado de Calidad de la Fruta"),
                    new ReportParameter("titulo3Param", _titulos == null ? "" : _titulos.titulo3),
                    new ReportParameter("fechaReporteParam", dtpFechaInicio.Value.ToString("dd/MM/yyyy").ToUpper() + " AL " + dtpFechaFin.Value.ToString("dd/MM/yyyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion)
                };


                DataTable _dtMaster = new DataTable("Recepciones");

                _dtMaster.Columns.Add("recepcion_id", typeof(string));
                _dtMaster.Columns.Add("fecha", typeof(string));
                _dtMaster.Columns.Add("id_folio_diario", typeof(string));
                _dtMaster.Columns.Add("rancho", typeof(string));
                _dtMaster.Columns.Add("sector", typeof(string));
                _dtMaster.Columns.Add("peso_caja", typeof(string));
                _dtMaster.Columns.Add("desecho", typeof(string));
                _dtMaster.Columns.Add("porcentaje", typeof(string));
                _dtMaster.Columns.Add("Podrida", typeof(string));
                _dtMaster.Columns.Add("Defecto", typeof(string));
                _dtMaster.Columns.Add("Caracter", typeof(string));
                _dtMaster.Columns.Add("Color", typeof(string));
                _dtMaster.Columns.Add("Larva", typeof(string));
                _dtMaster.Columns.Add("materia", typeof(string));
                _dtMaster.Columns.Add("calidad", typeof(string));
                _dtMaster.Columns.Add("grado_id", typeof(string));
                _dtMaster.Columns.Add("cant_cajas", typeof(string));

                foreach (DataRow fila in _residuos.Rows)
                {
                    DataRow row = _dtMaster.NewRow();
                    row["recepcion_id"] = fila["recepcion_id"].ToString();
                    row["fecha"] = fila["fecha"].ToString();
                    row["id_folio_diario"] = fila["id_folio_diario"].ToString();
                    row["rancho"] = fila["rancho"].ToString();
                    row["sector"] = fila["sector"].ToString();
                    row["peso_caja"] = fila["peso_caja"].ToString();
                    row["desecho"] = fila["desecho"].ToString();
                    row["porcentaje"] = fila["porcentaje"].ToString();
                    row["Podrida"] = fila["Podrida"].ToString();
                    row["Defecto"] = fila["Defecto"].ToString();
                    row["Caracter"] = fila["Caracter"].ToString();
                    row["Color"] = fila["Color"].ToString();
                    row["Larva"] = fila["Larva"].ToString() == "1" ? "Pos." : "Neg.";
                    row["materia"] = fila["materia"].ToString() == "True" ? "Pos." : "Neg.";
                    row["calidad"] = fila["calidad"].ToString();
                    row["grado_id"] = fila["grado_id"].ToString();
                    row["cant_cajas"] = fila["cant_cajas"].ToString();

                    _dtMaster.Rows.Add(row);
                }


                ReportHelper reporte = new ReportHelper(
                    "Reports/Reportes/ReporteCalidadesRecepciones/rptReporteCalidadesRecepciones.rdlc",
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

    }
}
