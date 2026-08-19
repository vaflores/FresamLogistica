using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Helper;
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

namespace FresamLogistica.Views.Reportes
{
    public partial class MangoPorcentajeViajesProcesadosView : Form
    {
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly MangoRecepcionesSalidasController _repoRecepciones = new MangoRecepcionesSalidasController();

        private int _IdCicloSeleccionado;
        public MangoPorcentajeViajesProcesadosView()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            ConfigurarComboBoxCiclos();

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

        private void ConfigurarComboBoxCiclos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbCiclos, _repoCiclos.ListarCombo());
            cbCiclos.SelectedIndex = -1;
        }
        private async void ImprimirReporte()
        {
            try
            {

                if (this._IdCicloSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un ciclo.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable _recepciones;

                if (chkMostrarDetalleProcesado.Checked)
                    _recepciones = await _repoRecepciones.ConsultarMangoRecepcionesSalidasDetallePorCiclo(this._IdCicloSeleccionado);
                else
                    _recepciones = await _repoRecepciones.ConsultarMangoRecepcionesSalidasPorCiclo(this._IdCicloSeleccionado);

                if (_recepciones.Rows.Count == 0)
                {
                    MessageBox.Show("No existen recepciones a mostrar en el rango de fechas seleccionado.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var _ciclos = _repoCiclos.ObtenerPorCicloId(this._IdCicloSeleccionado);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;

                string _cicloReporte = "";

                if (!_ciclos.fecha_fin.HasValue)
                {
                    _cicloReporte = $"{_ciclos.ciclo_id} - {_ciclos.fecha_inicio:dd/MM/yyyy} - Actual";
                }
                else
                {
                    _cicloReporte = $"{_ciclos.ciclo_id} - {_ciclos.fecha_inicio:dd/MM/yyyy} - {_ciclos.fecha_fin:dd/MM/yyyy}";
                }

                    List<ReportParameter> parametros = new List<ReportParameter>()
                {
                    new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("nombreEmpresaParam", _empresa.nombre),
                    new ReportParameter("fechaEmisionParam", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt").ToUpper()),
                    new ReportParameter("cicloReporteParam", _cicloReporte),
                };

                DataTable _dtMaster;
                ReportHelper reporte;


                if (!chkMostrarDetalleProcesado.Checked)
                {
                    _dtMaster = new DataTable("viajesdt");

                    _dtMaster.Columns.Add("recepcion_id", typeof(string));
                    _dtMaster.Columns.Add("viaje", typeof(string));
                    _dtMaster.Columns.Add("codigo", typeof(string));
                    _dtMaster.Columns.Add("proveedor", typeof(string));
                    _dtMaster.Columns.Add("fecha", typeof(string));
                    _dtMaster.Columns.Add("peso_neto", typeof(string));
                    _dtMaster.Columns.Add("procesado", typeof(string));
                    _dtMaster.Columns.Add("pendiente", typeof(string));
                    _dtMaster.Columns.Add("variedad", typeof(string));
                    _dtMaster.Columns.Add("grado", typeof(string));
                    _dtMaster.Columns.Add("porcentaje", typeof(string));

                    _dtMaster.PrimaryKey = new DataColumn[] { _dtMaster.Columns["recepcion_id"] };


                    foreach (DataRow _rec in _recepciones.Rows)
                    {
                        DataRow row = _dtMaster.NewRow();

                        row["recepcion_id"] = _rec.ItemArray[0];
                        row["viaje"] = _rec.ItemArray[1];
                        row["codigo"] = _rec.ItemArray[2];
                        row["proveedor"] = _rec.ItemArray[3];
                        row["fecha"] = Convert.ToDateTime(_rec.ItemArray[4]).ToShortDateString();
                        row["peso_neto"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[5]), 2).ToString("N2");
                        row["procesado"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[6]), 2).ToString("N2");
                        row["pendiente"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[7]), 2).ToString("N2");
                        row["variedad"] = _rec.ItemArray[8];
                        row["grado"] = _rec.ItemArray[9];
                        row["porcentaje"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[10]), 2).ToString("N2");

                        if (Convert.ToDecimal(_rec.ItemArray[7]) == 0 && !chkMostrarViajesSinSaldo.Checked)
                            continue;

                        _dtMaster.Rows.Add(row);
                    }

                    reporte = new ReportHelper(
                        "Reports/Mango/MangoViajesProcesados/rptMangoViajesProcesados.rdlc",
                        parametros,
                        _dtMaster);
                }
                else
                {
                    _dtMaster = new DataTable("viajesdt");

                    _dtMaster.Columns.Add("recepcion_id", typeof(string));
                    _dtMaster.Columns.Add("viaje", typeof(string));
                    _dtMaster.Columns.Add("viaje_proveedor", typeof(string));
                    _dtMaster.Columns.Add("codigo", typeof(string));
                    _dtMaster.Columns.Add("proveedor", typeof(string));
                    _dtMaster.Columns.Add("fecha", typeof(string));
                    _dtMaster.Columns.Add("peso_neto", typeof(string));
                    _dtMaster.Columns.Add("procesado", typeof(string));
                    _dtMaster.Columns.Add("peso_neto_disponible", typeof(string));
                    _dtMaster.Columns.Add("variedad", typeof(string));
                    _dtMaster.Columns.Add("grado", typeof(string));
                    _dtMaster.Columns.Add("porcentaje_procesado", typeof(string));
                    _dtMaster.Columns.Add("lavado", typeof(string));
                    _dtMaster.Columns.Add("podrido", typeof(string));
                    _dtMaster.Columns.Add("madurado", typeof(string));

                    _dtMaster.PrimaryKey = new DataColumn[] { _dtMaster.Columns["recepcion_id"] };


                    foreach (DataRow _rec in _recepciones.Rows)
                    {
                        DataRow row = _dtMaster.NewRow();

                        row["recepcion_id"] = _rec.ItemArray[0];
                        row["viaje"] = _rec.ItemArray[1];
                        row["viaje_proveedor"] = _rec.ItemArray[2];
                        row["codigo"] = _rec.ItemArray[3];
                        row["proveedor"] = _rec.ItemArray[4];
                        row["fecha"] = Convert.ToDateTime(_rec.ItemArray[5]).ToShortDateString();
                        row["peso_neto"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[6]), 2).ToString("N2");
                        row["procesado"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[7]), 2).ToString("N2");
                        row["peso_neto_disponible"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[8]), 2).ToString("N2");
                        row["variedad"] = _rec.ItemArray[9];
                        row["grado"] = _rec.ItemArray[10];
                        row["porcentaje_procesado"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[11]), 2).ToString("N2");
                        row["lavado"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[12]), 2).ToString("N2");
                        row["podrido"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[13]), 2).ToString("N2");
                        row["madurado"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[14]), 2).ToString("N2");

                        if (Convert.ToDecimal(_rec.ItemArray[7]) == 0 && !chkMostrarViajesSinSaldo.Checked)
                            continue;

                        _dtMaster.Rows.Add(row);
                    }


                    reporte = new ReportHelper(
                        "Reports/Mango/MangoDetalleViajesProcesados/rptMangoViajesProcesados.rdlc",
                        parametros,
                        _dtMaster);
                }

                reporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message, "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        private void cbCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdCicloSeleccionado = Convert.ToInt32(cbCiclos.SelectedValue);
            }
            catch
            {

            }

        }


    }
}
