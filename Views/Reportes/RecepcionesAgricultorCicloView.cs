using FresamLogistica.Controllers.General;
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
    public partial class RecepcionesAgricultorCicloView : Form
    {
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly AgricultoresController _repoAgricultores = new AgricultoresController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly RecepcionesController _repoRecepciones = new RecepcionesController();

        private int _IdAgricultorSeleccionado;
        private int _IdCicloSeleccionado;
        public RecepcionesAgricultorCicloView()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            ConfigurarComboBoxAgricultores();
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

        private void ConfigurarComboBoxAgricultores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbAgricultor, _repoAgricultores.ListarComboBox());
            cbAgricultor.SelectedIndex = -1;
        }

        private void ConfigurarComboBoxCiclos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbCiclos, _repoCiclos.ListarCombo());
            cbCiclos.SelectedIndex = -1;
        }
        private void ImprimirReporte()
        {
            try
            {

                List<RecepcionesViewModel> _recepciones;

                if (this._IdCicloSeleccionado == 0 && this._IdAgricultorSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un ciclo y agricultor.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _recepciones = _repoRecepciones.ObtenerRecepcionesPorCicloIdAgricultorId(this._IdCicloSeleccionado, this._IdAgricultorSeleccionado);
         
                if (_recepciones.Count == 0)
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
                    new ReportParameter("agricultorReporteParam", _recepciones.FirstOrDefault().AgricultorNombre.ToUpper())
                };

                DataTable _dtMaster = new DataTable("recepcionesDataSet");

                _dtMaster.Columns.Add("Semana", typeof(string));
                _dtMaster.Columns.Add("PesoNeto", typeof(string));
                _dtMaster.Columns.Add("KgHa", typeof(string));
                _dtMaster.Columns.Add("Calificacion", typeof(string));
                _dtMaster.Columns.Add("TotalRecepciones", typeof(string));

                _dtMaster.PrimaryKey = new DataColumn[] { _dtMaster.Columns["Semana"] };

                foreach (var _rec in _recepciones)
                {
                    if (_rec.Estado == "I")
                        continue;

                    DataRow _fila = _dtMaster.Rows.Find(_rec.Semana.ToString() + "-" + _rec.Anio.ToString());
                    if (_fila != null)
                    {
                        _fila["PesoNeto"] = Convert.ToDecimal(_fila["PesoNeto"]) + _rec.TarimasInfo.Sum(t=>t.peso_neto);
                        _fila["Calificacion"] = Convert.ToDecimal(_fila["Calificacion"]) + (Constantes.Calificacion - _rec.CalidadesInfo.Sum(r=>r.Valor));
                        _fila["TotalRecepciones"] = Convert.ToDecimal(_fila["TotalRecepciones"]) + 1;
                    }
                    else
                    {
                        DataRow row = _dtMaster.NewRow();
                        row["Semana"] = _rec.Semana.ToString() +"-"+_rec.Anio.ToString();
                        row["PesoNeto"] = _rec.TarimasInfo.Sum(t=>t.peso_neto);
                        row["KgHa"] = "0.00";
                        row["Calificacion"] = Constantes.Calificacion - _rec.CalidadesInfo.Sum(r=>r.Valor);
                        row["TotalRecepciones"] = 1;
                        _dtMaster.Rows.Add(row);
                    }
                    

                }


                ReportHelper reporte = new ReportHelper(
                    "Reports/Reportes/RecepcionesAgricultorCiclo/rptRecepcionesAgricultorCiclo.rdlc", 
                    parametros,
                    _dtMaster);

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

        private void cbAgricultor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdAgricultorSeleccionado = Convert.ToInt32(cbAgricultor.SelectedValue);           
            }
            catch
            {

            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tsbTitulos_Click(object sender, EventArgs e)
        {

        }
    }
}
