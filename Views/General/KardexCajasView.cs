using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.Productos;
using FresamLogistica.Views.Residuos;
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

namespace FresamLogistica.Views.General
{
    public partial class KardexCajasView : Form
    {
        private readonly KardexCajasController _repoKardexCajas;
        private readonly AgricultoresController _repoAgricultores = new AgricultoresController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly UsuariosController _repoUsuarios = new UsuariosController();
        private readonly KardexCondicionesCajasController _repoCondicionesCajas = new KardexCondicionesCajasController();

        private int _IdRegistroSeleccionado = 0;

        private string _colImpresion = "Documento";

        public KardexCajasView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repoKardexCajas = new KardexCajasController();

            ConfigurarComboBoxAgricultores();

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, 0, _repoKardexCajas.ObtenerMovimientosKardex(_IdRegistroSeleccionado));

            this.Tag = 36;
            ValidarPermisos();
        }
        #region EventosFormulario
        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
            ActualizarRegistro(0);
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistro();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            ImprimirRegistro();
        }

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repoKardexCajas.ObtenerMovimientosKardex(_IdRegistroSeleccionado));
        }

        private void FrutasView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                ActualizarRegistro(0);
            }
            else if (e.Control && e.KeyCode == Keys.G && tsbGuardar.Enabled) // Ctrl + G → Guardar
            {
                GuardarRegistro();
            }
            else if (e.Control && e.KeyCode == Keys.E && tsbEliminar.Enabled) // Ctrl + E → Eliminar
            {
                EliminarRegistro();
            }
            else if (e.Control && e.KeyCode == Keys.B && tsbBuscar.Enabled) // Ctrl + B → Buscar
            {
                CargarRegistros();
            }
            else if (e.Control && e.KeyCode == Keys.P && tsbImprimir.Enabled) // Ctrl + P → Imprimir
            {
                ImprimirRegistro();
            }
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            if (_IdRegistroSeleccionado == 0)
            {
                MessageBox.Show("No hay un agricultor seleccionado para realizar movimientos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Helper.FormulariosHelper.CargarFormulario(new KardexMovimientoRecepcionView(_IdRegistroSeleccionado, true), this);
            CargarRegistros();
        }

        private void tsbDelGrid_Click(object sender, EventArgs e)
        {
            if(_IdRegistroSeleccionado == 0)
            {
                MessageBox.Show("No hay un agricultor seleccionado para realizar movimientos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Helper.FormulariosHelper.CargarFormulario(new KardexMovimientoEntregaView(_IdRegistroSeleccionado, false), this);
            CargarRegistros();
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                dgvRegistros.Columns[e.ColumnIndex].Name == "Imprimir" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir")
                )
            {
                // Obtén el objeto actual (puede ser por ID)
                var id = dgvRegistros.Rows[e.RowIndex].Cells[1].Value.ToString();
                var tipoMovimiento = dgvRegistros.Rows[e.RowIndex].Cells[4].Value.ToString();
                var fechaMovimiento = Convert.ToDateTime(dgvRegistros.Rows[e.RowIndex].Cells[3].Value);
                var esEntrada = tipoMovimiento.ToString() == "Entrega" ? true : false;
                var cantCajas = esEntrada ? dgvRegistros.Rows[e.RowIndex].Cells[6].Value.ToString() : dgvRegistros.Rows[e.RowIndex].Cells[5].Value.ToString();
                var observaciones = dgvRegistros.Rows[e.RowIndex].Cells[9].Value.ToString();

                ImprimirMovimiento(Convert.ToInt32(id),tipoMovimiento, fechaMovimiento, cantCajas, observaciones, esEntrada);
                // Lógica de edición (por ejemplo, abrir otro formulario)
                //ActualizarRegistro(id);
            }
            else if (e.RowIndex >= 0 &&
                dgvRegistros.Columns[e.ColumnIndex].Name == "Imprimir")
            {
                MessageBox.Show("No tiene permiso para imprimir el documento.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbAgricultor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdRegistroSeleccionado = Convert.ToInt32(cbAgricultor.SelectedValue);
                CargarRegistros();
            }
            catch
            {
                _IdRegistroSeleccionado = 0;
                CargarRegistros();
            }
        }

        #endregion

        #region MetodosPrivados
        private void ValidarPermisos()
        {
            // Aquí se deben implementar las validaciones de permisos según el usuario actual.
            // Por ahora, habilitamos todos los botones.
            tsbCrear.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Guardar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            DataGridViewHelper.AddButtonColumn(dgv, _colImpresion, "Imprimir", 100);

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Kardex Id", "kardex_id", 75, true);
            DataGridViewHelper.AddColumn(dgv, "Agricultor Id", "agricultor_id", 100, false);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha del Movimiento", "fecha", 100);
            DataGridViewHelper.AddColumn(dgv, "Tipo de Movimiento", "tipo_movimiento", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Recepción", "entrada", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Entrega", "salida", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Saldo Anterior", "saldo_anterior", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Saldo Actualizado", "saldo_actual", 100);
            DataGridViewHelper.AddColumn(dgv, "Observaciones", "observaciones", 500);

        }

        private void ConfigurarComboBoxAgricultores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbAgricultor, _repoAgricultores.ListarComboBox());
        }

        private void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repoKardexCajas.ObtenerMovimientosKardex(_IdRegistroSeleccionado);
        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            //Helper.FormulariosHelper.CargarFormulario(new AgricultorView(_IdRegistro), this);
            CargarRegistros();
        }

        private void GuardarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }


        private void ImprimirMovimiento(int folioId, string tipoMovimiento, DateTime fechaMovimiento, string cantCajas, string observaciones, bool esEntrada)
        {
            try
            {

                var _empresa = _repoEmpresa.ObtenerPorId(1);
                var _movimiento = _repoKardexCajas.ObtenerMovimientoKardexPorId(folioId);
                string nombreEmpleado = _repoUsuarios.ObtenerNombreEmpleadoUsuario(_movimiento.usuario_crea);
                string puestoEmpleado = _repoUsuarios.ObtenerPuestoEmpleadoUsuario(_movimiento.usuario_crea);
                byte[] _logoByte = _empresa.logo;

                List<ReportParameter> parametros = new List<ReportParameter>()
                    {
                    new ReportParameter("logo", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("pEmpresa", _empresa.nombre),
                    new ReportParameter("pTipoMovimiento", tipoMovimiento.ToUpper()),
                    new ReportParameter("pFolio", folioId.ToString()),
                    new ReportParameter("pFecha", fechaMovimiento.ToString("ddMMyy").ToUpper()),
                    new ReportParameter("pAgricultor", cbAgricultor.Text),
                    new ReportParameter("pCantidadCajas", cantCajas),
                    new ReportParameter("pObservaciones", observaciones),
                    new ReportParameter("pEntrada", esEntrada ? "False" : "True"),
                    new ReportParameter("pNombreEmpleado", nombreEmpleado),
                    new ReportParameter("pPuestoEmpleado", puestoEmpleado)

                };

                ReportHelper reporte;
                if (tipoMovimiento.ToUpper() == "ENTREGA")
                {
                    reporte = new ReportHelper(
                        "Reports/General/KardexMovimiento/rptKardexMovimientoEntrega.rdlc",
                        parametros);
                }
                else
                {

                    DataTable _dtMaster = new DataTable("CondicionesCajas");

                    _dtMaster.Columns.Add("nombre", typeof(string));
                    _dtMaster.Columns.Add("valor", typeof(string));

                    var condiciones = _repoCondicionesCajas.ListarPorKardex(folioId);
                    
                    foreach (var item in condiciones)
                    {
                        _dtMaster.Rows.Add(item.Nombre, item.Valor);
                    }


                    reporte = new ReportHelper(
                        "Reports/General/KardexMovimiento/rptKardexMovimientoRecepcion.rdlc",
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

    }
}
