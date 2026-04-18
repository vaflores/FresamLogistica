using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using FresamLogistica.Views.General.Agricultores;
using FresamLogistica.Views.Productos.Frutas;
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

namespace FresamLogistica.Views.Residuos
{
    public partial class RecoleccionResiduosView : Form
    {
        private readonly RegistroResiduosRecolectadosController _repo;
        private readonly EmpresaGestoraResiduosController _repoEmpresaGestora = new EmpresaGestoraResiduosController();
        private readonly OperadoresResiduosController _repoOperadores = new OperadoresResiduosController();
        private readonly DestinosResiduosController _repoDestinos = new DestinosResiduosController();
        private readonly VehiculosController _repoVehiculos = new VehiculosController();
        private readonly FoliosController _repoFolios = new FoliosController();

        private int _IdRegistroSeleccionado;
        private int _empresaResiduoIdSeleccionada;
        private bool _requiereFolio;
        private int _vehiculoIdSeleccionado;
        private int _cantidadFoliosRequeridos;
        private int _cantidadFoliosSeleccionados;

        private System.Windows.Forms.Timer delayTimerVehiculo;

        public RecoleccionResiduosView(int _IdRegistro)
        {
            InitializeComponent();

            this.KeyPreview = true;

            this._IdRegistroSeleccionado = _IdRegistro;
            this._empresaResiduoIdSeleccionada = 0;
            this._requiereFolio = false;
            this._vehiculoIdSeleccionado = 0;

            _repo = new RegistroResiduosRecolectadosController();

            ConfigurarCombosBox();

            ConfigurarDataGridViewResiduos(dgvResiduos); 
            ConfigurarDataGridViewFolios(dgvFolios);

            ConfigurarTextBoxNumericos();

            InicializarTimer();


            cbEmpresa.SelectedIndex = -1;
            cbDestino.SelectedIndex = -1;

            this.Tag = 27;
            ValidarPermisos();
        }

        #region EventosFormulario
        private void RecepcionView_Load(object sender, EventArgs e)
        {
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

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
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
                //CargarDatosRegistroSeleccionado();
            }
            else if (e.Control && e.KeyCode == Keys.R && tsbImprimir.Enabled) // Ctrl + P → Imprimir 
            {
                Imprimir();
            }

        }

        private void txtPlacas_TextChanged(object sender, EventArgs e)
        {
            delayTimerVehiculo.Stop();
            delayTimerVehiculo.Start();
        }

        private void DelayTimerVehiculo_Tick(object sender, EventArgs e)
        {
            delayTimerVehiculo.Stop();
            EjecutarConsulta();
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

        private void ConfigurarCombosBox()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbEmpresa, _repoEmpresaGestora.ListarComboBoxEmpresasGestorasResiduosActivasYSeleccionada(cbEmpresa.SelectedValue != null ? Convert.ToInt32(cbEmpresa.SelectedValue) : 0));
            EstadosHelper.GenerarEstadosDesdeLista(ref cbOperador, _repoOperadores.ListarComboBoxOperadoresResiduoPorEmpresaYSeleccionado(this._empresaResiduoIdSeleccionada, cbOperador.SelectedValue != null ? Convert.ToInt32(cbOperador.SelectedValue) : 0));
            ValidarComboBoxFolios();
            EstadosHelper.GenerarEstadosDesdeLista(ref cbDestino, _repoDestinos.ListarComboBoxDestinosResiduos());
            EstadosHelper.GenerarEstadosRecepciones(ref cbEstado);
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado > 0)
            {
                //Cargar info recepción
                var registro = _repo.ConsultarRegistroResiduosRecolectado(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    txtRecoleccion.Text = registro.RegistroResiduoId.ToString();
                    dtpFecha.Value = registro.Fecha;
                    cbEmpresa.SelectedValue = registro.EmpresaResiduoId.ToString();
                    cbOperador.SelectedValue = registro.OperadorResiduoId.ToString();
                    cbDestino.SelectedValue = registro.DestinoResiduoId.ToString();
                    txtTotal.Text = registro.CantidadTotal.ToString("N2");
                    txtObservaciones.Text = registro.Observaciones;
                    cbEstado.SelectedValue = registro.Estado;
                    txtPlacas.Text = registro.Vehiculo != null ? registro.Vehiculo.placas : string.Empty;

                    //Cargar Info Tarimas
                    CargarDatosDetalleResiduos(registro.DetalleResiduosRecolectados);
                    CargarDatosFoliosResiduos(registro.FoliosRegistroResiduosRecolectados);
                    HabilitarControlesSegunRequiereFolio();
                    
                }
                else
                {
                    MessageBox.Show("No se encontró la recepción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                BorrarCampos();
            }
        }

        private void GuardarRegistro()
        {
            try
            {
                if(_requiereFolio && _cantidadFoliosSeleccionados < _cantidadFoliosRequeridos)
                {
                    MessageBox.Show($"La cantidad de folios seleccionados es menor a la requerida. Folios requeridos: {_cantidadFoliosRequeridos}, Folios seleccionados: {_cantidadFoliosSeleccionados}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Cargar info recepción
                var registro = new RegistroResiduosRecolectadoViewModel()
                {
                    RegistroResiduoId = this._IdRegistroSeleccionado,
                    Fecha = dtpFecha.Value,
                    Estado = cbEstado.SelectedValue != null ? cbEstado.SelectedValue.ToString() : string.Empty,
                    EmpresaResiduoId = cbEmpresa.SelectedValue != null ? Convert.ToInt32(cbEmpresa.SelectedValue) : 0,
                    OperadorResiduoId = cbOperador.SelectedValue != null ? Convert.ToInt32(cbOperador.SelectedValue) : 0,
                    DestinoResiduoId = cbDestino.SelectedValue != null ? Convert.ToInt32(cbDestino.SelectedValue) : 0,
                    RequiereFolio = this._requiereFolio,
                    VehiculoId = this._vehiculoIdSeleccionado,
                    CantidadTotal = decimal.Parse(txtTotal.Text),
                    Observaciones = txtObservaciones.Text
                };

                //Cargar Info Residuos Recolectados
                List<DetalleRegistroResiduoRecolectadoViewModel> residuosRecolectados = new List<DetalleRegistroResiduoRecolectadoViewModel>();
                foreach (DataGridViewRow row in dgvResiduos.Rows)
                {
                    DetalleRegistroResiduoRecolectadoViewModel condicion = new DetalleRegistroResiduoRecolectadoViewModel()
                    {
                        DetalleRegistroId = row.Cells["DetalleRegistroId"].Value != null ? Convert.ToInt32(row.Cells["DetalleRegistroId"].Value) : 0,
                        FrutaId = row.Cells["FrutaId"].Value != null ? Convert.ToInt32(row.Cells["FrutaId"].Value.ToString()) : 0,
                        NombreFruta = row.Cells["NombreFruta"].Value != null ? row.Cells["NombreFruta"].Value.ToString() : "",
                        TipoResiduoId = row.Cells["TipoResiduoId"].Value != null ? Convert.ToInt32(row.Cells["TipoResiduoId"].Value.ToString()) : 0,
                        NombreTipoResiduo = row.Cells["NombreTipoResiduo"].Value != null ? row.Cells["NombreTipoResiduo"].Value.ToString() : "",
                        Cantidad = row.Cells["Cantidad"].Value != null ? Convert.ToDecimal(row.Cells["Cantidad"].Value) : 0
                    };
                    residuosRecolectados.Add(condicion);
                }
                registro.DetalleResiduosRecolectados = residuosRecolectados;

                List<FoliosRegistroResiduoRecolectadoViewModel> foliosSeleccionados = new List<FoliosRegistroResiduoRecolectadoViewModel>();
                foreach(DataGridViewRow row in dgvFolios.Rows)
                {
                    if (row.Cells["FolioId"].Value != null)
                    {
                        FoliosRegistroResiduoRecolectadoViewModel folio = new FoliosRegistroResiduoRecolectadoViewModel();
                        folio.FolioId = Convert.ToInt32(row.Cells["FolioId"].Value);
                        folio.numero = Convert.ToInt32(row.Cells["numero"].Value);
                        foliosSeleccionados.Add(folio);
                    }
                }
                registro.FoliosRegistroResiduosRecolectados = foliosSeleccionados;

                //Cargar Info Vehiculo
                registro.Vehiculo = new Vehiculos()
                {
                    vehiculo_id = this._vehiculoIdSeleccionado,
                    placas = txtPlacas.Text.Trim(),
                    color = txtColor.Text.Trim(),
                    marca_modelo = txtMarcaModelo.Text.Trim()
                };

                if (_IdRegistroSeleccionado == 0)
                {
                    _IdRegistroSeleccionado = _repo.GuardarRegistroResiduoRecolectado(registro);
                    MessageBox.Show("Recepción creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.ActualizarRegistroResiduoRecolectado(registro);
                    MessageBox.Show("Recepción actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //this.Close();
                CargarDatosRegistroSeleccionado();
                //ImprimirRegistro();
                txtRecoleccion.Text = _IdRegistroSeleccionado.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el registro. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            this._IdRegistroSeleccionado = 0;
            txtRecoleccion.Text = "";
            cbEstado.SelectedIndex = 0;
            cbEmpresa.SelectedIndex = -1;
            cbOperador.SelectedIndex = -1;
            cbDestino.SelectedIndex = -1;
            txtPlacas.Text = "";
            txtMarcaModelo.Text = "";
            txtColor.Text = "";
            txtTotal.Text = "0.00";
            txtObservaciones.Text = "";
            this._requiereFolio = false;
            HabilitarControlesSegunRequiereFolio();
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void Imprimir()
        {

        }

        private void ConfigurarDataGridViewResiduos(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "DetalleRegistroId", "DetalleRegistroId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "FrutaId", "FrutaId", 80, false);
            DataGridViewHelper.AddColumn(dgv, "Fruta", "NombreFruta", 150);
            DataGridViewHelper.AddColumn(dgv, "TipoResiduoId", "TipoResiduoId", 80, false);
            DataGridViewHelper.AddColumn(dgv, "Residuo", "NombreTipoResiduo", 150);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Cantidad", "cantidad", 150);

            //dgv.CellValueChanged += dgvTarimas_CellValueChanged;

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void CargarDatosDetalleResiduos(List<DetalleRegistroResiduoRecolectadoViewModel> _registros)
        {
            dgvResiduos.Rows.Clear();
            DataGridViewRow _fila = new DataGridViewRow();
            foreach (DetalleRegistroResiduoRecolectadoViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvResiduos);
                _fila.Cells[0].Value = t.DetalleRegistroId;
                _fila.Cells[1].Value = t.FrutaId;
                _fila.Cells[2].Value = t.NombreFruta;
                _fila.Cells[3].Value = t.TipoResiduoId;
                _fila.Cells[4].Value = t.NombreTipoResiduo;
                _fila.Cells[5].Value = t.Cantidad.ToString("N2");
                dgvResiduos.Rows.Add(_fila);
            }
        }

        private void ConfigurarTextBoxNumericos()
        {
            Helper.TextBoxValidarEnteros.Attach(txtRecoleccion);
            Helper.TextBoxValidarEnteros.Attach(txtTotal);
        }

        private void CargarVehiculoInfo(Vehiculos _registro)
        {
            if (_registro != null)
            {
                this._vehiculoIdSeleccionado = _registro.vehiculo_id;
                txtPlacas.Text = _registro != null ? _registro.placas : string.Empty;
                txtColor.Text = _registro != null ? _registro.color : string.Empty;
                txtMarcaModelo.Text = _registro != null ? _registro.marca_modelo : string.Empty;
            }
        }

        private void InicializarTimer()
        {
            delayTimerVehiculo = new System.Windows.Forms.Timer();
            delayTimerVehiculo.Interval = 400;  // ms de espera
            delayTimerVehiculo.Tick += DelayTimerVehiculo_Tick;

        }

        private void EjecutarConsulta()
        {
            CargarVehiculoInfo(_repoVehiculos.ConsultarDatosVehiculoPorPlaca(txtPlacas.Text.Trim()));
        }

        #endregion

        private void cbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbEmpresa.SelectedValue != null)
                {
                    this._empresaResiduoIdSeleccionada = Convert.ToInt32(cbEmpresa.SelectedValue);
                    EstadosHelper.GenerarEstadosDesdeLista(ref cbOperador, _repoOperadores.ListarComboBoxOperadoresResiduoPorEmpresaActivos(this._empresaResiduoIdSeleccionada));
                    cbOperador.SelectedIndex = -1;

                    ValidarComboBoxFolios();
                }
            }
            catch
            {
            }
        }

        private void cbDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidarComboBoxFolios();
            }
            catch { }
        }

        private void ValidarComboBoxFolios()
        {
            int _destinoId = cbDestino.SelectedValue != null ? Convert.ToInt32(cbDestino.SelectedValue) : 0;
            this._requiereFolio = _repoDestinos.ValidarSiDestinoRequiereFolio(_destinoId);

            HabilitarControlesSegunRequiereFolio();
        }

        private void HabilitarControlesSegunRequiereFolio()
        {
            tsbAddGridFolios.Enabled = this._requiereFolio;
            tsbDelGridFolios.Enabled = this._requiereFolio;
            dgvFolios.Enabled = this._requiereFolio;
            txtCantFolios.Enabled = false;
            calcularCantidadFolios();
            if (!this._requiereFolio)
            {
                txtCantFolios.Text = "No requerido";
                dgvFolios.Rows.Clear();
            }
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            RecoleccionResiduosDetalleView formDetalle = new RecoleccionResiduosDetalleView();
            formDetalle.Detalle = new DetalleRegistroResiduoRecolectadoViewModel();
            formDetalle.Detalle.DetalleRegistroId = 0;
            formDetalle.Detalle.FrutaId = 0;
            formDetalle.Detalle.TipoResiduoId = 0;
            formDetalle.Detalle.Cantidad = 0;
            formDetalle.FormClosed += (s, args) =>
            {
                if (formDetalle.DialogResult == DialogResult.OK)
                {
                    DetalleRegistroResiduoRecolectadoViewModel nuevoDetalle = formDetalle.Detalle;
                    DataGridViewRow _fila = new DataGridViewRow();
                    _fila.CreateCells(dgvResiduos);
                    _fila.Cells[0].Value = nuevoDetalle.DetalleRegistroId;
                    _fila.Cells[1].Value = nuevoDetalle.FrutaId;
                    _fila.Cells[2].Value = nuevoDetalle.NombreFruta;
                    _fila.Cells[3].Value = nuevoDetalle.TipoResiduoId;
                    _fila.Cells[4].Value = nuevoDetalle.NombreTipoResiduo;
                    _fila.Cells[5].Value = nuevoDetalle.Cantidad.ToString("N2");
                    dgvResiduos.Rows.Add(_fila);
                }
                CalcularTotal();
            };

            formDetalle.ShowDialog();

        }

        private void tsbDelGrid_Click(object sender, EventArgs e)
        {
            try
            {
                dgvResiduos.Rows.RemoveAt(dgvResiduos.CurrentRow.Index);
                CalcularTotal();
            }
            catch { }
        }

        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvResiduos.Rows)
            {
                if (row.Cells["Cantidad"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Cantidad"].Value);
                }
            }
            txtTotal.Text = total.ToString("N2");
            calcularCantidadFolios();
        }


        private void ConfigurarDataGridViewFolios(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "FoliosRegistroId", "FoliosRegistroId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "FolioId", "FolioId", 80, false);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Folio", "numero", 150);

            //dgv.CellValueChanged += dgvTarimas_CellValueChanged;

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void tsbAddGridFolios_Click(object sender, EventArgs e)
        {
            RecoleccionResiduosFolioView formFolios = new RecoleccionResiduosFolioView();
            formFolios.Folios = ListaFoliosSeleccionadosDelGrid();
            formFolios.DestinoResiduoIdSeleccionado = cbDestino.SelectedValue != null ? Convert.ToInt32(cbDestino.SelectedValue) : 0;
            formFolios.FormClosed += (s, args) =>
            {
                if (formFolios.DialogResult == DialogResult.OK)
                {
                    dgvFolios.Rows.Clear();
                    List<FoliosRegistroResiduoRecolectadoViewModel> nuevoDetalle = formFolios.Folios;
                    foreach (FoliosRegistroResiduoRecolectadoViewModel f in nuevoDetalle)
                    {
                        DataGridViewRow _fila = new DataGridViewRow();
                        _fila.CreateCells(dgvFolios);
                        _fila.Cells[1].Value = f.FolioId;
                        _fila.Cells[2].Value = f.numero;

                        dgvFolios.Rows.Add(_fila);
                    }
                }
                calcularCantidadFolios();
            };

            formFolios.ShowDialog();

        }
        private void CargarDatosFoliosResiduos(List<FoliosRegistroResiduoRecolectadoViewModel> _registros)
        {
            dgvFolios.Rows.Clear();
            List<FoliosRegistroResiduoRecolectadoViewModel> nuevoDetalle = _registros;
            foreach (FoliosRegistroResiduoRecolectadoViewModel f in nuevoDetalle)
            {
                DataGridViewRow _fila = new DataGridViewRow();
                _fila.CreateCells(dgvFolios);
                _fila.Cells[1].Value = f.FolioId;
                _fila.Cells[2].Value = f.numero;

                dgvFolios.Rows.Add(_fila);
            
            }
            calcularCantidadFolios();
        }

        private void tsbDelGridFolios_Click(object sender, EventArgs e)
        {

            try
            {
                dgvFolios.Rows.RemoveAt(dgvFolios.CurrentRow.Index);
            }
            catch { }
        }

        private List<FoliosRegistroResiduoRecolectadoViewModel> ListaFoliosSeleccionadosDelGrid()
        {
            List<FoliosRegistroResiduoRecolectadoViewModel> lista = new List<FoliosRegistroResiduoRecolectadoViewModel>();
            foreach (DataGridViewRow row in dgvFolios.Rows)
            {
                if (row.Cells["FolioId"].Value != null)
                {
                    FoliosRegistroResiduoRecolectadoViewModel folio = new FoliosRegistroResiduoRecolectadoViewModel();
                    folio.FolioId = Convert.ToInt32(row.Cells["FolioId"].Value);
                    folio.numero = Convert.ToInt32(row.Cells["numero"].Value);
                    lista.Add(folio);
                }
            }
            return lista;
        }

        private void calcularCantidadFolios()
        {
            if (this._requiereFolio && txtTotal.Text != "")
            {
        
                int cantidadFolios = 0;
                decimal total = Convert.ToDecimal(txtTotal.Text);

                if (total > 0)
                {
                    cantidadFolios = (int)Math.Ceiling(total / 1000); // Suponiendo que cada folio cubre hasta 1000 unidades de residuo
                }

                _cantidadFoliosRequeridos = cantidadFolios;
                _cantidadFoliosSeleccionados = dgvFolios.Rows.Count;
                txtCantFolios.Text = _cantidadFoliosSeleccionados +"/" + _cantidadFoliosRequeridos.ToString("N0");
            }
        }
    }
}
