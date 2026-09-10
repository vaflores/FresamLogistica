using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using FresamLogistica.ViewModels.Mango;
using FresamLogistica.Views.General.Agricultores;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FresamLogistica.Views.Mango
{
    public partial class MangoDestajoView : Form
    {
        private readonly MangoDestajosController _repo;
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly TitulosMangoDestajoController _repoTitulos = new TitulosMangoDestajoController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();

        private int _destajoIdSeleccionado;
        private int _idResponsable;
        private int _idVerifica;

        private int _cicloId;

        public MangoDestajoView(int _destajoId, int _responsableId, int _verificaId)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;

            _repo = new MangoDestajosController();

            ConfigurarDataGridView(dgvTarimas);

            ConfigurarTextBoxNumericos();
            ConfigurarComboBoxFirmas();
            ConfigurarComboBoxEstatus();

            this._destajoIdSeleccionado = _destajoId;
            this._idResponsable = _responsableId;
            this._idVerifica = _verificaId;

            ValidarPermisos();

        }

        #region EventosFormulario
        private void RecepcionView_Load(object sender, EventArgs e)
        {
            ObtenerCicloPorFecha();

            if (_cicloId == 0)
            {
                MessageBox.Show("No se ha creado un ciclo de trabajo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            CargarDatosRegistroSeleccionado();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarDatosRegistroSeleccionado();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
            BorrarCampos();
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistro();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                BorrarCampos();
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
                CargarDatosRegistroSeleccionado();
            }

        }

        #endregion


        #region MetodosPrivados

        private void ValidarPermisos()
        {
            // Aquí se deben implementar las validaciones de permisos según el usuario actual.
            // Por ahora, habilitamos todos los botones.
            tsbCrear.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear") ||
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
        }
        private void ConfigurarComboBoxFirmas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbElabora, _repoEmpleados.ListarComboBox());
            EstadosHelper.GenerarEstadosDesdeLista(ref cbVerifica, _repoEmpleados.ListarComboBox());
            cbElabora.SelectedIndex = -1;
            cbVerifica.SelectedIndex = -1;
        }

        private void ValidarPuedeEditarTarimas()
        {
            // Implementar la lógica para validar si se pueden editar las tarimas
            if(_destajoIdSeleccionado > 0)
            {
                // Si el registro ya existe, deshabilitar la edición de tarimas
                tsOpciones.Enabled = true;
            }
            else
            {
                // Si es un nuevo registro, permitir la edición de tarimas
                tsOpciones.Enabled = false;
            }

            //Si el estado es I, deshabilitar la edición de tarimas
            if(cbEstado.SelectedValue != null && cbEstado.SelectedValue.ToString() == "I")
            {
                tsOpciones.Enabled = false;
            }
        }

        private void ConfigurarComboBoxEstatus()
        {
            EstadosHelper.GenerarEstadosRecepciones(ref cbEstado);
        }

        private async void CargarDatosRegistroSeleccionado()
        {
            if (_destajoIdSeleccionado > 0)
            {
                //Cargar info recepción
                var registro = _repo.ObtenerPorId(_destajoIdSeleccionado);
                if (registro != null)
                {
                    dtpFechaInicio.Value = registro.fecha_hora_inicio;
                    dtpFechaTermino.Value = registro.fecha_hora_termino ?? DateTime.Now;
                    txtTotalCajas.Text = registro.total_cajas.ToString("N2");
                    txtPesoNeto.Text = registro.peso_neto.ToString("N2");
                    _idResponsable = registro.empleado_responsable_id;
                    _idVerifica = registro.empleado_verifico_id == null ? 0 : registro.empleado_verifico_id.Value;
                    cbEstado.SelectedValue = registro.estado;

                    cbElabora.SelectedValue = this._idResponsable.ToString();
                    cbVerifica.SelectedValue = this._idVerifica.ToString();

                    await CargarDatosTarimasSeleccionadas();

                }
                else
                {
                    MessageBox.Show("No se encontró la recepción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                dtpFechaInicio.Value = DateTime.Now;
                dtpFechaTermino.Value = DateTime.Now;

                txtTotalCajas.Text = "0";
                txtPesoNeto.Text = "0";

                dgvTarimas.Rows.Clear();

                cbElabora.SelectedValue = 0;
                cbVerifica.SelectedValue = 0;
                ValidarPuedeEditarTarimas();
            }
        }

        private async Task CargarDatosTarimasSeleccionadas()
        {
            await CargarDatos();
            CalcularCantidadCajasYPesoNeto();
            ValidarPuedeEditarTarimas();
        }

        private void GuardarRegistro()
        {
            try
            {

                if (cbElabora.SelectedValue == null || cbVerifica.SelectedValue == null)
                {
                    MessageBox.Show("Falta seleccionar nombre de responsable y verifica.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Vallidar que la fecha de inicio y termino sean del mismo dia
                if (dtpFechaInicio.Value.Date != dtpFechaTermino.Value.Date)
                {
                    MessageBox.Show("La fecha de inicio y la fecha de término deben ser del mismo día.", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Validar la fecha y hora de termino sea mayor a la fecha y hora de inicio
                if (dtpFechaTermino.Value < dtpFechaInicio.Value)
                {
                    MessageBox.Show("La fecha y hora de término no puede ser menor a la fecha y hora de inicio.", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Validar que un destajo estado I no se vuelva a guardar
                if (_destajoIdSeleccionado > 0)
                {
                    var registroExistente = _repo.ObtenerPorId(_destajoIdSeleccionado);
                    if (registroExistente != null && registroExistente.estado == "I")
                    {
                        MessageBox.Show("No se puede guardar una producción con estado Inactivo.", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                //Validar que el destajo no sea en una fecha ya creada
                if (_repo.ValidarDestajosDelMismoDia(dtpFechaInicio.Value.Date) != _destajoIdSeleccionado){
                    MessageBox.Show("Ya existe un producción creada para la fecha seleccionada.", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Cargar info destajo
                var registro = _repo.ObtenerPorId(_destajoIdSeleccionado);

                if (registro == null)
                    registro = new MangoDestajos();

                registro.fecha_hora_inicio = dtpFechaInicio.Value;
                registro.fecha_hora_termino = dtpFechaTermino.Value;
                registro.total_cajas = decimal.Parse(txtTotalCajas.Text);
                registro.peso_neto = decimal.Parse(txtPesoNeto.Text);
                registro.empleado_responsable_id = int.Parse(cbElabora.SelectedValue.ToString());
                registro.empleado_verifico_id = int.Parse(cbVerifica.SelectedValue.ToString());
                registro.estado = cbEstado.SelectedValue.ToString();

                if (_destajoIdSeleccionado == 0)
                {
                    _destajoIdSeleccionado = _repo.CrearDestajo(registro);
                    MessageBox.Show("Producción creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Producción actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarDatosRegistroSeleccionado();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el registro. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void EliminarRegistro()
        {
            try
            {
                //Preguntar si realmente desea eliminar la producción
                var result = MessageBox.Show("¿Está seguro de eliminar la producción seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;


                var registro = _repo.ObtenerPorId(_destajoIdSeleccionado);

                if (registro == null)
                    return;

                _repo.EliminarDestajo(_destajoIdSeleccionado);

                CargarDatosRegistroSeleccionado();

                this.Close();

                MessageBox.Show("Producción eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la producción. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void ObtenerCicloPorFecha()
        {
            _cicloId = _repoCiclos.ObtenerCicloPorFecha(dtpFechaInicio.Value);
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.ReadOnly = true;
            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Tarima Id", "mango_recepcion_salida_tarima_id", 50, false);
            DataGridViewHelper.AddColumn(dgv, "Fecha y hora procesada", "fecha_hora_destajo", 100);
            DataGridViewHelper.AddColumn(dgv, "Folio Ciclo", "folio_ciclo", 50);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha Recepción", "fecha_recepcion", 150);
            DataGridViewHelper.AddColumn(dgv, "Código", "codigo", 50);
            DataGridViewHelper.AddColumn(dgv, "Distribuidor", "proveedor", 150);
            DataGridViewHelper.AddColumn(dgv, "Predio", "predio", 100);
            DataGridViewHelper.AddColumn(dgv, "Variedad", "variedad", 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha Traspaleo", "fecha_traspaleo", 50);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Número Tarima", "numero_tarima", 50);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Cantidad de Cajas", "cantidad_cajas", 120);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Neto", "peso_neto", 120);

        }

        private async Task CargarDatos()
        {
            dgvTarimas.DataSource = await _repo.ListarRecepcionesSalidasTarimasPorDestajo(_destajoIdSeleccionado);
        }

        private void ConfigurarTextBoxNumericos()
        {
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoNeto);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtTotalCajas);

            txtPesoNeto.Enabled = false;
            txtTotalCajas.Enabled = false;
        }

        public void CalcularCantidadCajasYPesoNeto()
        {
            decimal totalCajas = 0;
            decimal totalPesoNeto = 0;
            foreach (DataGridViewRow row in dgvTarimas.Rows)
            {
                if (row.Cells["cantidad_cajas"].Value != null && decimal.TryParse(row.Cells["cantidad_cajas"].Value.ToString(), out decimal cantidadCajas))
                {
                    totalCajas += cantidadCajas;
                }
                if (row.Cells["peso_neto"].Value != null && decimal.TryParse(row.Cells["peso_neto"].Value.ToString(), out decimal pesoNeto))
                {
                    totalPesoNeto += pesoNeto;
                }
            }
            txtTotalCajas.Text = totalCajas.ToString("N2");
            txtPesoNeto.Text = totalPesoNeto.ToString("N2");
        }

        #endregion

        private async void tsbAddGrid_Click(object sender, EventArgs e)
        {
            //Abrir formulario para agregar tarimas al destajo
            if (_destajoIdSeleccionado == 0)
            {
                MessageBox.Show("Debe guardar la producción antes de agregar tarimas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            Helper.FormulariosHelper.CargarFormulario(new MangoDestajoTarimasView(_destajoIdSeleccionado, dtpFechaInicio.Value, _cicloId), this);
            CargarDatosRegistroSeleccionado();
            await CargarDatosTarimasSeleccionadas();
            GuardarRegistro();
        }

        private async void tsbDelGrid_Click(object sender, EventArgs e)
        {
            //Eliminar tarima seleccionada del datagrid y de la base de datos
            //Preguntar confirmacion de eliminacion
            if (dgvTarimas.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("¿Está seguro de eliminar la tarima seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //Eliminar tarima de la base de datos
                    var numeroTarima = dgvTarimas.SelectedRows[0].Cells["mango_recepcion_salida_tarima_id"].Value.ToString();
                    _repo.EliminarTarimaSeleccionada(Convert.ToInt16(numeroTarima));

                    MessageBox.Show("Tarima desasignada correctamente.", "Informacipon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Recargar grid
                    await CargarDatosTarimasSeleccionadas();
                    GuardarRegistro();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una tarima para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            await CargarDatosTarimasSeleccionadas();
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            ImprimirReporte();
        }


        private async void ImprimirReporte()
        {

            try
            {

                if (_destajoIdSeleccionado <= 0)
                    return;

                DataTable datos = await _repo.ListarRecepcionesSalidasTarimasPorDestajo(_destajoIdSeleccionado);

                if (datos.Rows.Count == 0)
                {
                    MessageBox.Show("No existen datos para mostrar.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cbElabora.Text) || string.IsNullOrWhiteSpace(cbVerifica.Text))
                {
                    MessageBox.Show("Falta asignar el nombre del responsable y verifica.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Ciclos _ciclo = _repoCiclos.ObtenerPorCicloId(_cicloId);
                var _titulos = _repoTitulos.ObtenerPorCicloId(_cicloId);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;

                #region definir_tabla
                DataTable _dtMaster = new DataTable("dtsTarimasDestajo");

                _dtMaster.Columns.Add("fecha_recepcion", typeof(string));
                _dtMaster.Columns.Add("fecha_lavado", typeof(string));
                _dtMaster.Columns.Add("proveedor", typeof(string));
                _dtMaster.Columns.Add("predio", typeof(string));
                _dtMaster.Columns.Add("codigo_proveedor", typeof(string));
                _dtMaster.Columns.Add("numero_viaje", typeof(string));
                _dtMaster.Columns.Add("cantidad_cajas", typeof(string));
                _dtMaster.Columns.Add("peso_neto", typeof(string));
                _dtMaster.Columns.Add("hora", typeof(string));
                _dtMaster.Columns.Add("variedad", typeof(string));
                _dtMaster.Columns.Add("numero_tarima", typeof(string));

                #endregion

                var _sumPesoNeto = datos.AsEnumerable().Sum(row => row.Field<decimal>("peso_neto"));
                var _sumCantidadCajas = datos.AsEnumerable().Sum(row => row.Field<decimal>("cantidad_cajas"));

                foreach (DataRow _rec in datos.Rows)
                {
                    DataRow row = _dtMaster.NewRow();

                    row["fecha_recepcion"] = Convert.ToDateTime(_rec.ItemArray[3]).ToString("dd/MM/yyyy");
                    row["fecha_lavado"] = Convert.ToDateTime(_rec.ItemArray[8]).ToString("dd/MM/yyyy");
                    row["proveedor"] = _rec.ItemArray[5];
                    row["predio"] = _rec.ItemArray[6];
                    row["codigo_proveedor"] = _rec.ItemArray[4];
                    row["numero_viaje"] = _rec.ItemArray[2];
                    row["cantidad_cajas"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[10]), 2).ToString("N0");
                    row["peso_neto"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[11]), 2).ToString("N0");
                    row["hora"] = Convert.ToDateTime(_rec.ItemArray[1]).ToString("HH:mm");
                    row["variedad"] = _rec.ItemArray[7];
                    row["numero_tarima"] = _rec.ItemArray[9];
                    _dtMaster.Rows.Add(row);

                }

                List<ReportParameter> parametros = new List<ReportParameter>()
                {
                    new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("nombreEmpresaParam", _empresa.nombre),
                    new ReportParameter("titulo1Param", _titulos == null ? "" : _titulos.titulo1),
                    new ReportParameter("titulo2Param", _titulos == null ? "" : _titulos.titulo2),
                    new ReportParameter("titulo3Param", _titulos == null ? "" : _titulos.titulo3),
                    new ReportParameter("fechaReporteParam", dtpFechaInicio.Value.ToString("dd/MM/yyyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion),

                    new ReportParameter("fruta", "MANGO"),
                    new ReportParameter("pesoNeto", _sumPesoNeto.ToString("N0")),
                    new ReportParameter("cantidadCajas", _sumCantidadCajas.ToString("N0")),

                    new ReportParameter("responsable", cbElabora.Text),
                    new ReportParameter("verifica", cbVerifica.Text)
                };

                ReportHelper reporte = new ReportHelper(
                    "Reports/Mango/MangoDestajos/rptMangoTarimasDestajo.rdlc",
                    parametros,
                    _dtMaster);

                reporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message, "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
