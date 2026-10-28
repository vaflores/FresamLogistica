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
    public partial class BitacoraAgricultoresView : Form
    {
        private readonly TitulosBitacoraAgricultoresController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private int _IdCicloSeleccionado;

        private string nombreElabora = string.Empty;
        private string nombreVerifica = string.Empty;

        public BitacoraAgricultoresView()
        {
            InitializeComponent();
            this.KeyPreview = true;

            _repo = new TitulosBitacoraAgricultoresController();

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            ValidarPermisos();

            ConfigurarComboBoxCiclos();
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

        private async void ImprimirReporte()
        {
            try
            {

                if (this._IdCicloSeleccionado == 0 )
                {
                    MessageBox.Show("Seleccione un ciclo.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(nombreElabora) || string.IsNullOrWhiteSpace(nombreVerifica))
                {
                    MessageBox.Show("Falta seleccionar nombre de responsable y verifica.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable _datos;

                _datos = await _repoCiclos.ObtenerReporteBitacoranAgricultoresAsync(this._IdCicloSeleccionado);
                
                if ((_datos == null || _datos.Rows.Count == 0))
                {
                    MessageBox.Show("No existen recepciones a mostrar en el ciclo seleccionado.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var _ciclo = _repoCiclos.ObtenerPorCicloId(this._IdCicloSeleccionado);
                var _titulos = _repo.ObtenerPorCicloId(this._IdCicloSeleccionado);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;

                List<ReportParameter> parametros = new List<ReportParameter>()
                {
                    new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("nombreEmpresaParam", _empresa.nombre),
                    new ReportParameter("titulo1Param", _titulos == null ? "" : _titulos.titulo1),
                    new ReportParameter("titulo2Param", _titulos == null ? "" : _titulos.titulo2),
                    new ReportParameter("titulo3Param", _titulos == null ? "" : _titulos.titulo3),
                    new ReportParameter("fechaReporteParam", DateTime.Now.ToString("ddMMyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion),
                    new ReportParameter("responsableParam", nombreElabora),
                    new ReportParameter("verificaParam", nombreVerifica)
                };

                DataTable _dtMaster = new DataTable("dtBitacoraAgricultores");

                _dtMaster.Columns.Add("codigo", typeof(string));
                _dtMaster.Columns.Add("nombre", typeof(string));
                _dtMaster.Columns.Add("rancho", typeof(string));
                _dtMaster.Columns.Add("superficie", typeof(string));
                _dtMaster.Columns.Add("sectores", typeof(string));


                if (_datos != null)
                {
                    // Agregar filas de proveedores al DataTable _dtMaster
                    for (int i = 0; i < _datos.Rows.Count; i++)
                    {
                        DataRow row = _dtMaster.NewRow();
                        row["codigo"] = _datos.Rows[i]["CODIGO"].ToString();
                        row["nombre"] = _datos.Rows[i]["NOMBRE DEL PRODUCTOR"].ToString();
                        row["rancho"] = _datos.Rows[i]["RANCHO"].ToString();
                        row["superficie"] = _datos.Rows[i]["SUPERFICIE"].ToString();
                        row["sectores"] = _datos.Rows[i]["SECTORES"].ToString();
                        _dtMaster.Rows.Add(row); 
                    }
                }

                ReportHelper reporte = new
                    ReportHelper("Reports/Reportes/BitacoraAgricultores/rptBitacoraAgricultores.rdlc",
                    parametros,
                    _dtMaster);

                reporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message, "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ConfigurarComboBoxCiclos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbCiclos, _repoCiclos.ListarCombo());
            cbCiclos.SelectedIndex = -1;
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

        private void tsbTitulos_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarDialogoFormulario(new TitulosBitacoraAgricultoresView());
        }
    }
}
