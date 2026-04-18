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
    public partial class ResumenRecepcionesProductoAgricultorView : Form
    {
        private readonly TitulosInspeccionesController _repo;
        private readonly CiclosResiduosController _repoCiclos = new CiclosResiduosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly AgricultoresController _repoAgricultores = new AgricultoresController();
        private readonly ResumenRecepcionesProductoAgricultorController controller = new ResumenRecepcionesProductoAgricultorController();

        private DataTable datosImpresion = new DataTable();

        public ResumenRecepcionesProductoAgricultorView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new TitulosInspeccionesController();
            ConfigurarComboBoxAgricultores();
            dtpFechaFin.Checked = true;

            //this.MaximizeBox = false;
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

                DataTable _residuos = await controller.ObtenereporteResumenRecepcionesPorAgricultorAsync(
                    dtpFechaInicio.Value,
                    dtpFechaFin.Checked ? dtpFechaFin.Value : dtpFechaInicio.Value.Date.AddDays(1).AddSeconds(-1),
                    cbAgricultores.SelectedValue.ToString()
                );

                if (_residuos == null || _residuos.Rows.Count == 0)
                {
                    MessageBox.Show("No existen datos para imprimir en el rango de fechas seleccionado.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    new ReportParameter("titulo1Param", _titulos == null ? "" : _titulos.titulo1),
                    new ReportParameter("titulo2Param", "Resumen de Recepciones de Producto por Agricultor "),
                    new ReportParameter("titulo3Param", _titulos == null ? "" : _titulos.titulo3),
                    new ReportParameter("fechaReporteParam", dtpFechaInicio.Value.ToString("dd/MM/yyyy").ToUpper() + " AL " + dtpFechaFin.Value.ToString("dd/MM/yyyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion)
                };


                DataTable _dtMaster = new DataTable("Detalle");

                _dtMaster.Columns.Add("Agricultor", typeof(string));
                _dtMaster.Columns.Add("Fecha", typeof(string));
                _dtMaster.Columns.Add("TotalCajas", typeof(string));
                _dtMaster.Columns.Add("TotalKgs", typeof(string));

                foreach (DataRow fila in _residuos.Rows)
                {
                    DataRow row = _dtMaster.NewRow();
                    row["Agricultor"] = fila["Agricultor"];
                    row["Fecha"] = Convert.ToDateTime(fila["Fecha"]).ToShortDateString();
                    row["TotalCajas"] = fila["TotalCajas"];
                    row["TotalKgs"] = fila["TotalKgs"];

                    _dtMaster.Rows.Add(row);
                }


                ReportHelper reporte = new ReportHelper(
                    "Reports/Reportes/ResumenRecepcionesProductoAgricultor/rptResumenRecepcionesProductoAgricultor.rdlc",
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
        private void ConfigurarComboBoxAgricultores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbAgricultores, _repoAgricultores.ListarComboBox());
        }
    }
}
