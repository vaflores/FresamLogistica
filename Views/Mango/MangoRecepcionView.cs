using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Herramientas;
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
using FresamLogistica.Controllers.Configuracion;

namespace FresamLogistica.Views.Mango
{
    public partial class MangoRecepcionView : Form
    {
        private readonly MangoRecepcionesController _repo;
        private readonly MangoConfiguracionController _repoConfiguracion = new MangoConfiguracionController();
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly GradosController _repoGrados = new GradosController();
        private readonly MangoProveedoresController _repoProveedores = new MangoProveedoresController();
        private readonly MangoLocalidadesController _repoLocalidades = new MangoLocalidadesController();
        private readonly MangoPrediosController _repoPredios = new MangoPrediosController();
        private readonly MangoPropietariosController _repoPropietarios = new MangoPropietariosController();
        private readonly FrutasController _repoFrutas = new FrutasController();
        private readonly VariedadesController _repoVariedades = new VariedadesController();
        private readonly PreciosController _repoPrecios = new PreciosController();
        private readonly VehiculosController _repoVehiculos = new VehiculosController();
        private readonly CondicionesController _repoCondiciones = new CondicionesController();
        private readonly MangoCalidadesController _repoCalidades = new MangoCalidadesController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly TitulosMangoEtiquetasController _titulosEtiquetasController = new TitulosMangoEtiquetasController();
        private readonly MangoMuestreosController _repoMuestreos = new MangoMuestreosController();

        private int _IdRegistroSeleccionado;
        private int _IdCicloSeleccionado;
        private int _idProveedorSeleccionado;
        private string _codigoProveedor;
        private string _nombreProveedor;
        private int _IdLocalidadSeleccionado;
        private int _IdPredioSeleccionado;
        private int _IdPropietarioSeleccionado;
        private int _IdFrutaSeleccionada;
        private int _IdVariedadSeleccionada;
        private string _IdGradoSeleccionado;
        private decimal _totalPesoNeto = 0;
        private decimal _totalPrecio = 0;
        private decimal _precioFrutaGrado = 0;

        private System.Windows.Forms.Timer delayTimerVehiculo;
        private System.Windows.Forms.Timer delayTimerPrecioUnitario;

        public MangoRecepcionView(int _IdRegistro, int _IdCiclo)
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new MangoRecepcionesController();

            ConfigurarComboBoxEstatus();
            ConfigurarComboBoxCiclos();
            ConfigurarComboBoxGrados();
            ConfigurarComboBoxProveedores();
            ConfigurarComboBoxLocalidades();
            ConfigurarComboBoxPredios();
            ConfigurarComboBoxFrutas();
            ConfigurarComboBoxVariedades();

            ConfigurarDataGridViewCondiciones(dgvCondiciones);
            ConfigurarDataGridViewCalidades(dgvCalidades);

            ConfigurarTextBoxNumericos();

            InhabilitarControles();

            InicializarTimer();

            this._IdRegistroSeleccionado = _IdRegistro;
            this._IdCicloSeleccionado = _IdCiclo;

            EstablecerFechasValidasEnElCiclo();

            ValidarPermisos();

        }

        #region EventosFormulario
        private void RecepcionView_Load(object sender, EventArgs e)
        {
            cbCiclos.SelectedValue = this._IdCicloSeleccionado.ToString();

            _IdFrutaSeleccionada = _repoConfiguracion.ObtenerMangoFrutaId(1);

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
            ImprimirReciboRecepcion();
        }

        private void tbsEtiqueta_Click(object sender, EventArgs e)
        {
            ImprimirEtiquetaRecepcion();
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
            else if (e.Control && e.KeyCode == Keys.R && tsbImprimir.Enabled) // Ctrl + P → Imprimir Recibo
            {
                ImprimirReciboRecepcion();
            }
            else if (e.Control && e.KeyCode == Keys.T && tsbImprimir.Enabled) // Ctrl + P → Imprimir Etiqueta
            {
                ImprimirEtiquetaRecepcion();
            }

        }

        private void cbCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdCicloSeleccionado = Convert.ToInt32(cbCiclos.SelectedValue);

            }
            catch (Exception)
            {
                _IdCicloSeleccionado = 0;
            }
        }

        private void cbAgricultor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _idProveedorSeleccionado = Convert.ToInt32(cbProveedores.SelectedValue);
                string[] proveedor = ((System.Collections.Generic.KeyValuePair<string, string>)cbProveedores.SelectedItem).Value.ToString().Split('-');
                _codigoProveedor = proveedor[0];
                _nombreProveedor = proveedor[1];

            }
            catch (Exception)
            {
                _idProveedorSeleccionado = 0;
                _codigoProveedor = "";
                _nombreProveedor = "";
            }
        }

        private void cbRancho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdLocalidadSeleccionado = Convert.ToInt32(cbLocalidades.SelectedValue);
                ConfigurarComboBoxPredios();
            }
            catch (Exception)
            {
                _IdLocalidadSeleccionado = 0;
            }
        }

        private void cbSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdPredioSeleccionado = Convert.ToInt32(cbPredios.SelectedValue);
                _IdPropietarioSeleccionado = _repoPredios.ObtenerPorId(_IdPredioSeleccionado)?.propietario_id ?? 0;
                
                ConfigurarComboBoxPropietarios();
                CargarDatosSectorSeleccionado();
            }
            catch (Exception)
            {
                _IdPredioSeleccionado = 0;
            }
        }

        private void cbFruta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdFrutaSeleccionada = Convert.ToInt32(cbFrutas.SelectedValue);
                ConfigurarComboBoxVariedades();
            }
            catch (Exception)
            {
                _IdFrutaSeleccionada = 0;
            }
        }

        private void cbVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdVariedadSeleccionada = Convert.ToInt32(cbVariedades.SelectedValue);
                ConfigurarComboBoxGrados();
            }
            catch (Exception)
            {
                _IdVariedadSeleccionada = 0;
            }
        }

        private void cbAgricultor_SelectedValueChanged(object sender, EventArgs e)
        {
            cbAgricultor_SelectedIndexChanged(sender, e);
        }

        private void cbRancho_SelectedValueChanged(object sender, EventArgs e)
        {
            cbRancho_SelectedIndexChanged(sender, e);
        }

        private void cbSector_SelectedValueChanged(object sender, EventArgs e)
        {
            cbSector_SelectedIndexChanged(sender, e);
        }

        private void cbFruta_SelectedValueChanged(object sender, EventArgs e)
        {
            cbFruta_SelectedIndexChanged(sender, e);
        }

        private void cbVariedad_SelectedValueChanged(object sender, EventArgs e)
        {
            cbVariedad_SelectedIndexChanged(sender, e);
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtImporte.Text, out decimal valor))
            {
                txtImporte.Text = valor.ToString("C2"); // Formato moneda con 2 decimales
            }
            else
            {
                txtImporte.Text = "0.00"; // Valor por defecto si no es válido
            }
        }

        private void dgvCalidades_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            { 
                // Ejemplo: columna 3
                if (e.ColumnIndex == 3)
                {
                    if (!decimal.TryParse(e.FormattedValue.ToString(), out _))
                    {
                        MessageBox.Show("Solo se permiten números.");
                        //e.Cancel = true; // evita que pierda el foco
                        dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                        e.Cancel = false; // no marcar error
                    }
                }
            }
            catch { }
        }

        private void dgvCalidades_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try { 

                if (e.ColumnIndex == 3)
                {
                    if (decimal.TryParse(dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out decimal valor))
                    {
                        dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor.ToString("N2");
                    }
                    else
                    {
                        dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                }
            }
            catch { }
        }

        private void dgvCalidades_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            EstablecerCalificacion();
        }

        private void txtCalificacion_TextChanged(object sender, EventArgs e)
        {
            EstablecerCalificacion();
        }

        private void txtPesoCaja_TextChanged(object sender, EventArgs e)
        {
            CalcularPorcentajes();
        }

        private void txtDesecho_TextChanged(object sender, EventArgs e)
        {
            CalcularPorcentajes();
        }

        private void txtPlacas_TextChanged(object sender, EventArgs e)
        {
            delayTimerVehiculo.Stop();
            delayTimerVehiculo.Start();
        }

        private void txtPrecioUnitario_TextChanged(object sender, EventArgs e)
        {
            delayTimerPrecioUnitario.Stop();
            delayTimerPrecioUnitario.Start();
        }

        private void DelayTimerVehiculo_Tick(object sender, EventArgs e)
        {
            delayTimerVehiculo.Stop();
            EjecutarConsulta();
        }

        private void DelayTimerPrecioUnitario_Tick(object sender, EventArgs e)
        {
            delayTimerPrecioUnitario.Stop();
            CalcularPrecioTotal();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            EstablecerCalificacion();
        }

        private void txtPesoBruto_TextChanged(object sender, EventArgs e)
        {
            CalcularPesoNeto();
        }

        private void txtTara_TextChanged(object sender, EventArgs e)
        {
            CalcularPesoNeto();
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
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir Recibo");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir Etiqueta");
            HabilitarControlesTpCalidadDescuento(SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Modificar Calidad/Descuento"));
        }
        private void HabilitarControlesTpCalidadDescuento(bool opcionHabilitar)
        {
            dgvCalidades.Enabled = opcionHabilitar;
            //txtPesoCaja.Enabled = opcionHabilitar;
            //txtDesecho.Enabled = opcionHabilitar;
            //txtPorcentaje.Enabled = opcionHabilitar;
            chkAceptado.Enabled = opcionHabilitar;
            chkMateria.Enabled = opcionHabilitar;
            chkAfectaPeso.Enabled = opcionHabilitar;
            txtPorcentajeMaduro.Enabled = opcionHabilitar;
            txtObservaciones.Enabled = opcionHabilitar;
        }

        private void ConfigurarComboBoxEstatus()
        {
            EstadosHelper.GenerarEstadosRecepciones(ref cbEstado);
        }
        
        private void ConfigurarComboBoxCiclos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbCiclos, _repoCiclos.ListarComboCiclosActivosOrdenDescendete());
        }
        
        private void ConfigurarComboBoxGrados()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbGrados, _repoGrados.ListarComboBoxFruta(this._IdFrutaSeleccionada));
        }
        
        private void ConfigurarComboBoxProveedores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbProveedores, _repoProveedores.ListarComboBox());
        }
        
        private void ConfigurarComboBoxLocalidades()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbLocalidades, _repoLocalidades.ListarComboBox());
        }
        
        private void ConfigurarComboBoxPredios()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbPredios, _repoPredios.ListarComboBoxPorLocalidad(this._IdLocalidadSeleccionado));
        }
        private void ConfigurarComboBoxPropietarios()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbPropietarios, _repoPropietarios.ListarComboBoxPorPredio(this._IdPredioSeleccionado));
        }

        private void ConfigurarComboBoxFrutas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFrutas, _repoFrutas.ListarComboBox());
        }
        
        private void ConfigurarComboBoxVariedades()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbVariedades, _repoVariedades.ListarPorFrutaComboBox(this._IdFrutaSeleccionada));
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado > 0)
            {
                //Cargar info recepción
                var registro = _repo.ObtenerRecepcionPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    dtpFecha.Enabled = true;
                    cbCiclos.SelectedValue = registro.CicloId.ToString();
                    txtRecepcion.Text = registro.RecepcionId.ToString();
                    txtFolioCiclo.Text = registro.FolioCiclo.HasValue ? registro.FolioCiclo.Value.ToString() : string.Empty;
                    txtFolioProveedor.Text = registro.FolioProveedor.HasValue ? registro.FolioProveedor.Value.ToString() : string.Empty;
                    dtpFecha.Value = registro.Fecha;
                    txtGuiaSanitaria.Text = registro.GuiaSanitaria;
                    cbEstado.SelectedValue = registro.Estado;
                    cbGrados.SelectedValue = registro.GradoId == null ? "" : registro.GradoId.ToString();
                    txtCalificacion.Text = registro.CalidadPromedio.ToString();
                    txtPrecioUnitario.Text = registro.PrecioUnitario.HasValue ? registro.PrecioUnitario.Value.ToString("C2"): "$0.00";
                    txtImporte.Text = registro.Importe.HasValue ? registro.Importe.Value.ToString("C2") : "$0.00";
                    txtTicketBascula.Text = registro.TicketBascula;
                    txtPesoBruto.Text = registro.PesoBruto.ToString("N2");
                    txtTara.Text = registro.Tara.ToString("N2");
                    txtPesoNeto.Text = registro.PesoNeto.ToString("N2");
                    txtPesoDisponible.Text = registro.PesoNetoDisponible.ToString("N2");
                    txtPorcentajeCosto.Text = registro.PorcentajeCosto?.ToString("N2");
                    chkAplicaPorcentajeCostos.Checked = registro.AplicarPorcentajeCosto;

                    cbProveedores.SelectedValue = registro.ProveedorId.ToString();
                    cbLocalidades.SelectedValue = registro.LocalidadId.ToString();
                    ConfigurarComboBoxPredios();
                    cbPredios.SelectedValue = registro.PredioId.ToString();
                    ConfigurarComboBoxPropietarios();
                    cbPropietarios.SelectedValue = registro.PropietarioId.ToString();
                    cbFrutas.SelectedValue = this._IdFrutaSeleccionada.ToString();
                    cbVariedades.SelectedValue = registro.VariedadId.ToString();
                    

                    //Cargar Info Vehiculo
                    CargarVehiculoInfo(registro.VehiculoInfo);

                    //Cargar Info Descuentos
                    CargarDatosDescuentoPorRecepcion(registro.Descuentos);

                    //Cargar Info Condiciones
                    if(registro.VehiculoInfo != null)
                    { 
                        CargarDatosCondicionesRecepcion(registro.VehiculoInfo.CondicionesVehiculoInfo);
                        CargarDatosCondicionesActivas(registro.VehiculoInfo.CondicionesVehiculoActivas);
                    }
                    else
                    {
                        dgvCondiciones.Rows.Clear();
                        CargarDatosCondicionesActivas(_repoCondiciones.ListarMangoCondicionesActivasSinValorPorRecepcionId(0));
                    }

                    //Cargar Info Calidades
                    //CargarDatosCalidadesRecepcion(registro.CalidadesInfo);
                    registro.CalidadesActivas.AddRange(registro.CalidadesInfo);
                    CargarDatosCalidadesRecepcion(registro.CalidadesActivas.OrderBy(i => i.CalidadId).ToList());
                    
                    EstablecerCalificacion();

                    if(registro.PrecioUnitario.HasValue)
                    {
                        if(registro.PrecioUnitario != _precioFrutaGrado)
                        { 
                            txtPrecioUnitario.Text = registro.PrecioUnitario.Value.ToString("C2");
                        }
                    }

                    CalcularPorcentajes();
                    CalcularPrecioTotal();

                    txtPorcentajeMangoChico.Text = _repoMuestreos.ObtenerPromedioMangoChicoPorMuestreosRecepcion(_IdRegistroSeleccionado).ToString("N2");
                }
                else
                {
                    MessageBox.Show("No se encontró la recepción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                dtpFecha.Enabled = true;
                cbCiclos.SelectedValue = _IdCicloSeleccionado.ToString();
                cbFrutas.SelectedValue = this._IdFrutaSeleccionada.ToString();

                //Cargar Info Condiciones
                dgvCondiciones.Rows.Clear();
                CargarDatosCondicionesActivas(_repoCondiciones.ListarMangoCondicionesActivasSinValorPorRecepcionId(0));

                //Cargar Info Calidades
                dgvCalidades.Rows.Clear();
                CargarDatosCalidadesActivas(_repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(0));

                EstablecerCalificacion();
                CalcularPorcentajes();
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtGuiaSanitaria.Text))
                    MessageBox.Show("El número de guía es obligatorio. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //Cargar info recepción
                var registro = new MangoRecepcionesViewModel()
                {
                    RecepcionId = _IdRegistroSeleccionado,
                    CicloId = _IdCicloSeleccionado,
                    FolioCiclo = int.Parse(string.IsNullOrWhiteSpace(txtFolioCiclo.Text) ? "0" : txtFolioCiclo.Text),
                    FolioProveedor = int.Parse(string.IsNullOrWhiteSpace(txtFolioProveedor.Text) ? "0" : txtFolioProveedor.Text),
                    Fecha = dtpFecha.Value,
                    ProveedorId = _idProveedorSeleccionado,
                    LocalidadId = _IdLocalidadSeleccionado,
                    PredioId = _IdPredioSeleccionado,
                    PropietarioId = _idProveedorSeleccionado,
                    FrutaId = _IdFrutaSeleccionada,
                    VariedadId = _IdVariedadSeleccionada,
                    GuiaSanitaria = txtGuiaSanitaria.Text,
                    TicketBascula = txtTicketBascula.Text,
                    PesoBruto = decimal.Parse(txtPesoBruto.Text),
                    Tara = string.IsNullOrWhiteSpace(txtTara.Text) ? 0 : decimal.Parse(txtTara.Text),
                    PesoNeto = string.IsNullOrWhiteSpace(txtPesoNeto.Text) ? 0 : decimal.Parse(txtPesoNeto.Text),
                    PesoNetoDisponible = string.IsNullOrWhiteSpace(txtPesoDisponible.Text) ? 0 : decimal.Parse(txtPesoDisponible.Text),
                    CalidadPromedio = string.IsNullOrWhiteSpace(txtCalificacion.Text) ? 0 : decimal.Parse(txtCalificacion.Text),
                    GradoId = _IdGradoSeleccionado,
                    Importe = string.IsNullOrWhiteSpace(txtImporte.Text) ? 0 : decimal.Parse(txtImporte.Text, System.Globalization.NumberStyles.Currency),
                    Placas = txtPlacas.Text,
                    PrecioUnitario = string.IsNullOrWhiteSpace(txtPrecioUnitario.Text) ? 0 : decimal.Parse(txtPrecioUnitario.Text, System.Globalization.NumberStyles.Currency),
                    Estado = cbEstado.SelectedValue.ToString(),
                    AplicarPorcentajeCosto = chkAplicaPorcentajeCostos.Checked,
                    PorcentajeCosto = string.IsNullOrWhiteSpace(txtPorcentajeCosto.Text) ? 0 : decimal.Parse(txtPorcentajeCosto.Text)
                };

                //Cargar Info Vehiculo
                VehiculosRecepcionesViewModel vehiculoInfo = new VehiculosRecepcionesViewModel()
                    {
                        VehiculoRecepcionId = 0,
                        RecepcionId = registro.RecepcionId,
                        VehiculoId = 0,
                        Chofer = txtConductor.Text,
                        Placas = txtPlacas.Text,
                        Color = txtColor.Text,
                        MarcaModelo = txtMarcaModelo.Text,
                        Estado = "Activo"
                    };
                registro.VehiculoInfo = vehiculoInfo;

                //Cargar Info Descuentos
                MangoDescuentos descuentos = new MangoDescuentos()
                {
                    peso_caja = decimal.Parse(txtPesoCaja.Text),
                    desecho = decimal.Parse(txtDesecho.Text),
                    porcentaje = decimal.Parse(txtPorcentaje.Text),
                    observaciones = txtObservaciones.Text,
                    aceptado = chkAceptado.Checked,
                    materia = chkMateria.Checked,
                    porcentaje_descuento_afecta_peso = chkAfectaPeso.Checked,
                    porcentaje_maduro = decimal.Parse(txtPorcentajeMaduro.Text)
                };
                registro.Descuentos = descuentos;

                //Cargar Info Condiciones
                List<CondicionesVehiculoViewModel> condcionesVehiculo = new List<CondicionesVehiculoViewModel>();
                foreach (DataGridViewRow row in dgvCondiciones.Rows)
                {
                    CondicionesVehiculoViewModel condicion = new CondicionesVehiculoViewModel()
                    {
                        CondicionVehiculoRecepcionId = row.Cells["CondicionVehiculoRecepcionId"].Value != null ? Convert.ToInt32(row.Cells["CondicionVehiculoRecepcionId"].Value) : 0,
                        VehiculoRecepcionId = 0,
                        CondicionId = row.Cells["CondicionId"].Value != null ? Convert.ToInt32(row.Cells["CondicionId"].Value) : 0,
                        Nombre = row.Cells["Nombre"].Value != null ? row.Cells["Nombre"].Value.ToString() : string.Empty,
                        Valor = row.Cells["Valor"].Value != null ? Convert.ToBoolean(row.Cells["Valor"].Value) : false
                    };
                    condcionesVehiculo.Add(condicion);
                }
                registro.VehiculoInfo.CondicionesVehiculoInfo = condcionesVehiculo;

                //Cargar Info Calidades
                List<MangoCalidadesRecepcionesViewModel> calidadesRecepcion = new List<MangoCalidadesRecepcionesViewModel>();
                foreach (DataGridViewRow row in dgvCalidades.Rows)
                {
                    MangoCalidadesRecepcionesViewModel calidad = new MangoCalidadesRecepcionesViewModel()
                    {
                        CalidadRecepcionId = row.Cells["CalidadRecepcionId"].Value != null ? Convert.ToInt32(row.Cells["CalidadRecepcionId"].Value) : 0,
                        CalidadId = row.Cells["CalidadId"].Value != null ? Convert.ToInt32(row.Cells["CalidadId"].Value) : 0,
                        Nombre = row.Cells["Nombre"].Value != null ? row.Cells["Nombre"].Value.ToString() : string.Empty,
                        Valor = row.Cells["Valor"].Value != null ? Convert.ToDecimal(row.Cells["Valor"].Value) : 0
                    };
                    calidadesRecepcion.Add(calidad);
                }

                registro.CalidadesInfo = calidadesRecepcion;

                if (_IdRegistroSeleccionado == 0)
                {
                    _IdRegistroSeleccionado =  _repo.CrearRecepcionViewModel(registro);
                    MessageBox.Show("Recepción creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.ActualizarRecepcionViewModel(registro);
                    MessageBox.Show("Recepción actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarDatosRegistroSeleccionado();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el registro. \n{ex.InnerException.InnerException.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            //recargar formulario
            this._IdRegistroSeleccionado = 0;

            CargarDatosRegistroSeleccionado();
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirReciboRecepcion()
        {
            var _empresa = _repoEmpresa.ObtenerPorId(1);
            var _titulos = _titulosEtiquetasController.ObtenerTitulosPorCiclo(_IdCicloSeleccionado);

            byte[] _logoByte = _empresa.logo;

            Ciclos _ciclo = _repoCiclos.ObtenerPorId(_IdCicloSeleccionado);

            List<ReportParameter> parametros = new List<ReportParameter>()
            {
                new ReportParameter("nombreEmpresaParam", _empresa.nombre.ToUpper()),
                new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),

                new ReportParameter("titulo1Param", _titulos != null ? _titulos.titulo1 : string.Empty),
                new ReportParameter("titulo2Param", _titulos != null ? _titulos.titulo2 : string.Empty),
                new ReportParameter("titulo3Param", _titulos != null ? _titulos.titulo3 : string.Empty),

                new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                new ReportParameter("edicionParam", _ciclo?.edicion),

                new ReportParameter("folioCicloParam", txtFolioCiclo.Text),
                new ReportParameter("fechaRecepcionParam", dtpFecha.Value.ToString("ddMMyy")),
                new ReportParameter("proveedorNombreParam", _nombreProveedor),
                new ReportParameter("variedadNombreParam", cbVariedades.Text),
                new ReportParameter("propietarioNombreParam", cbPropietarios.Text),
                new ReportParameter("predioNombreParam", cbPredios.Text),
                new ReportParameter("localidadNombreParam", cbLocalidades.Text),
                new ReportParameter("codigoProveedorParam", _codigoProveedor),
                new ReportParameter("observacionesParam", txtObservaciones.Text)
            };

            ReportHelper reporte = new ReportHelper("Reports/Mango/MangoRecepcion/rptMangoEtiquetaControlRecepcion.rdlc", parametros);

            reporte.ShowDialog();

        }

        private void ImprimirEtiquetaRecepcion()
        {
            var _empresa = _repoEmpresa.ObtenerPorId(1);
            var _titulos = _titulosEtiquetasController.ObtenerTitulosPorCiclo(_IdCicloSeleccionado);
            
            byte[] _logoByte = _empresa.logo;

            Ciclos _ciclo = _repoCiclos.ObtenerPorId(_IdCicloSeleccionado);

            List<ReportParameter> parametros = new List<ReportParameter>()
            {
                new ReportParameter("nombreEmpresaParam", _empresa.nombre.ToUpper()),
                new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),

                new ReportParameter("titulo1Param", _titulos != null ? _titulos.titulo1 : string.Empty),
                new ReportParameter("titulo2Param", _titulos != null ? _titulos.titulo2 : string.Empty),
                new ReportParameter("titulo3Param", _titulos != null ? _titulos.titulo3 : string.Empty),

                new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                new ReportParameter("edicionParam", _ciclo?.edicion),

                new ReportParameter("folioCicloParam", txtFolioCiclo.Text),
                new ReportParameter("fechaRecepcionParam", dtpFecha.Value.ToString("ddMMyy")),
                new ReportParameter("proveedorNombreParam", _nombreProveedor),
                new ReportParameter("variedadNombreParam", cbVariedades.Text),
                new ReportParameter("propietarioNombreParam", cbPropietarios.Text),
                new ReportParameter("predioNombreParam", cbPredios.Text),
                new ReportParameter("localidadNombreParam", cbLocalidades.Text),
                new ReportParameter("codigoProveedorParam", _codigoProveedor),
                new ReportParameter("observacionesParam", txtObservaciones.Text)
            };

            ReportHelper reporte = new ReportHelper("Reports/Mango/MangoRecepcion/rptMangoEtiquetaRecepcion.rdlc", parametros);

            reporte.ShowDialog();

        }

        private void InhabilitarControles()
        {
            cbCiclos.Enabled = false;
            cbFrutas.Enabled = false;
            cbPropietarios.Enabled = false;
            cbGrados.Enabled = false;
            txtCalificacion.Enabled = false;
            chkAplicaPorcentajeCostos.Enabled = true;
            txtPorcentajeCosto.Enabled = true;

            //txtFolioCiclo.Enabled = false;
            txtFolioProveedor.Enabled = false;
            txtPrecioUnitario.Enabled = true;
            txtImporte.Enabled = false;
            txtPorcentaje.Enabled = false;
            txtDesecho.Enabled = false;
            txtPorcentajeMangoChico.Enabled = false;
        }

        private void CargarDatosSectorSeleccionado()
        {
                if (_IdPredioSeleccionado > 0)
                {
                    cbPropietarios.SelectedValue = this._IdPropietarioSeleccionado.ToString();
                }
        }

        private void CargarDatosDescuentoPorRecepcion(MangoDescuentos _registro)
        {
            if (_registro == null)
            {
                txtPesoCaja.Text = "0.00";
                txtDesecho.Text = "0.00";
                txtPorcentaje.Text = "0.00";
                txtObservaciones.Text = string.Empty;
                chkAceptado.Checked = false;
                chkMateria.Checked = false;
                chkAfectaPeso.Checked = false;
            }
            else 
            { 

                txtPesoCaja.Text = _registro.peso_caja.ToString();
                txtDesecho.Text = _registro.desecho.ToString();
                txtPorcentaje.Text = _registro.porcentaje.ToString();
                txtObservaciones.Text = _registro.observaciones;
                chkAceptado.Checked = _registro.aceptado;
                chkMateria.Checked = _registro.materia;
                chkAfectaPeso.Checked = _registro.porcentaje_descuento_afecta_peso;
                txtPorcentajeMaduro.Text = _registro.porcentaje_maduro?.ToString("N2");
            }
        }

        private void ConfigurarDataGridViewCondiciones(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "CondicionVehiculoRecepcionId", "CondicionVehiculoRecepcionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "VehiculoRecepcionId", "VehiculoRecepcionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "CondicionId", "CondicionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Condición", "Nombre", 250, true, true);
            DataGridViewHelper.AddColumnCheckBox(dgv, "Valor", "valor", 150, true, false);

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void ConfigurarDataGridViewCalidades(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "CalidadRecepcionId", "CalidadRecepcionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "CalidadId", "CalidadId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Calidad", "Nombre", 250, true, true);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Valor", "valor", 150, true, false);
            DataGridViewHelper.AddColumnCheckBox(dgv, "AfectaCalificacion", "AfectaCalificacion", 100, false);
            DataGridViewHelper.AddColumnCheckBox(dgv, "UsoPorcentajeMangoChico", "UsoPorcentajeMangoChico", 100, false);

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void CargarDatosCondicionesActivas(List<CondicionesVehiculoViewModel> _registros)
        {
            DataGridViewRow _fila = new DataGridViewRow();
            foreach (CondicionesVehiculoViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvCondiciones);
                _fila.Cells[0].Value = t.CondicionVehiculoRecepcionId;
                _fila.Cells[1].Value = t.VehiculoRecepcionId;
                _fila.Cells[2].Value = t.CondicionId;
                _fila.Cells[3].Value = t.Nombre;
                _fila.Cells[4].Value = t.Valor;
                dgvCondiciones.Rows.Add(_fila);
            }
        }

        private void CargarDatosCondicionesRecepcion(List<CondicionesVehiculoViewModel> _registros)
        {
            dgvCondiciones.Rows.Clear();
            if (_registros.Count == 0) return;

            DataGridViewRow _fila = new DataGridViewRow();
            foreach (CondicionesVehiculoViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvCondiciones);
                _fila.Cells[0].Value = t.CondicionVehiculoRecepcionId;
                _fila.Cells[1].Value = t.VehiculoRecepcionId;
                _fila.Cells[2].Value = t.CondicionId;
                _fila.Cells[3].Value = t.Nombre;
                _fila.Cells[4].Value = t.Valor;
                dgvCondiciones.Rows.Add(_fila);
            }
        }

        private void CargarDatosCalidadesActivas(List<MangoCalidadesRecepcionesViewModel> _registros)
        {
            if (_registros.Count == 0) return;

            DataGridViewRow _fila = new DataGridViewRow();
            foreach (MangoCalidadesRecepcionesViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvCalidades);
                _fila.Cells[0].Value = t.CalidadRecepcionId;
                _fila.Cells[1].Value = t.CalidadId;
                _fila.Cells[2].Value = t.Nombre;
                _fila.Cells[3].Value = t.Valor.ToString("N2");
                _fila.Cells[4].Value = t.AfectaCalificacion;
                _fila.Cells[5].Value = t.UsoPorcentajeMangoChico;
                dgvCalidades.Rows.Add(_fila);
            }
        }

        private void CargarDatosCalidadesRecepcion(List<MangoCalidadesRecepcionesViewModel> _registros)
        {
            dgvCalidades.Rows.Clear();
            DataGridViewRow _fila = new DataGridViewRow();
            foreach (MangoCalidadesRecepcionesViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvCalidades);
                _fila.Cells[0].Value = t.CalidadRecepcionId;
                _fila.Cells[1].Value = t.CalidadId;
                _fila.Cells[2].Value = t.Nombre;
                _fila.Cells[3].Value = t.Valor.ToString("N2");
                _fila.Cells[4].Value = t.AfectaCalificacion;
                _fila.Cells[5].Value = t.UsoPorcentajeMangoChico;
                dgvCalidades.Rows.Add(_fila);
            }
        }

        private void ConfigurarTextBoxNumericos()
        {
            Helper.TextBoxValidarEnteros.Attach(txtRecepcion);
            Helper.TextBoxValidarEnteros.Attach(txtFolioCiclo);
            Helper.TextBoxValidarEnteros.Attach(txtFolioProveedor);

            Helper.TextBoxValidarEnteros.Attach(txtCalificacion);
            Helper.TextBoxValidarMonedas.Attach(txtPrecioUnitario);
            Helper.TextBoxValidarMonedas.Attach(txtImporte);

            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentajeCosto);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoBruto);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtTara);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoNeto);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoDisponible);

            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoCaja);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtDesecho);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentaje);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentajeMaduro);

        }

        private void CalcularPorcentaje()
        {
            try
            {
                decimal pesoCaja = txtPesoCaja.Text.Length > 0 ? decimal.Parse(txtPesoCaja.Text) : 0;
                decimal desecho = txtDesecho.Text.Length > 0 ? decimal.Parse(txtDesecho.Text) : 0;

                if (pesoCaja > 0)
                {
                    decimal porcentaje = (desecho / pesoCaja) * 100;
                    txtPorcentaje.Text = porcentaje.ToString("N2");
                }
                else
                {
                    txtPorcentaje.Text = "0.00";
                }
            }
            catch (Exception)
            {
                txtPorcentaje.Text = "0.00";
            }
        }

        private void CalcularPorcentajes()
        {
            try
            {
                decimal porcentaje = 0;
                decimal pesoCaja = Convert.ToDecimal(txtPesoCaja.Text);

                foreach (DataGridViewRow row in dgvCalidades.Rows)
                {
                    if (!Convert.ToBoolean(row.Cells["AfectaCalificacion"].Value))
                        continue;

                    if (Convert.ToBoolean(row.Cells["UsoPorcentajeMangoChico"].Value))
                        porcentaje += (Convert.ToDecimal(row.Cells["Valor"].Value) / 2);
                    else
                        porcentaje += Convert.ToDecimal(row.Cells["Valor"].Value);

                    if (Convert.ToBoolean(row.Cells["UsoPorcentajeMangoChico"].Value))
                        txtPorcentajeMangoChico.Text = (Convert.ToDecimal(row.Cells["Valor"].Value) * 100 / pesoCaja).ToString("N2");

                }

                txtDesecho.Text = porcentaje.ToString("N2");

                porcentaje = Math.Round(porcentaje * 100 / pesoCaja, 2);
                txtPorcentaje.Text = porcentaje.ToString("N2");
            }
            catch
            {
                txtDesecho.Text = "0.00";
                txtPorcentaje.Text = "0.00";
            }
        }

        private void CalcularPrecioTotal()
        {
            try
            {
                CalcularPesoNeto();
                _IdGradoSeleccionado = cbGrados.SelectedValue.ToString();

                _precioFrutaGrado = _repoPrecios.ObtenerPrecioActual(_IdFrutaSeleccionada, _IdGradoSeleccionado, dtpFecha.Value);
                decimal _precioRecepcion = decimal.Parse(txtPrecioUnitario.Text, System.Globalization.NumberStyles.Currency);
                _totalPesoNeto = decimal.Parse(txtPesoNeto.Text);

                if (_precioFrutaGrado != _precioRecepcion && _precioRecepcion > 0)
                    _precioFrutaGrado = txtPrecioUnitario.Text.Length > 0 ? decimal.Parse(txtPrecioUnitario.Text, System.Globalization.NumberStyles.Currency) : 0;
                else
                {
                    txtPrecioUnitario.Text = _precioFrutaGrado.ToString("C2");
                }

                if (chkAplicaPorcentajeCostos.Checked)
                {
                    decimal porcentajeCosto = decimal.Parse(txtPorcentajeCosto.Text);
                    _totalPrecio = _precioFrutaGrado * (_totalPesoNeto * (1 - (porcentajeCosto/100)));
                }
                else
                {
                    _totalPrecio = _totalPesoNeto * _precioFrutaGrado;
                }

                txtImporte.Text = _totalPrecio.ToString("C2");
                txtPesoNeto.Text = _totalPesoNeto.ToString("N2");

            }
            catch (Exception)
            {
                // Ignorar errores de cálculo
            }
        }

        private void EstablecerCalificacion()
        {
            try
            {
                txtCalificacion.Text = (100 - Convert.ToDecimal(txtPorcentaje.Text)).ToString("N2");

                cbGrados.SelectedValue = _repoGrados.ObtenerGradoIdPorFrutaIdMangoCalificacion(_IdFrutaSeleccionada, Convert.ToInt16(decimal.Parse(txtCalificacion.Text)));
                txtPrecioUnitario.Text = _repoPrecios.ObtenerPrecioActual(_IdFrutaSeleccionada, _IdGradoSeleccionado, dtpFecha.Value).ToString("C2");

            }
            catch (Exception)
            {
                //txtCalificacion.Text = Constantes.Calificacion.ToString("N0");
            }
            CalcularPrecioTotal();
        }

        private void CargarVehiculoInfo(VehiculosRecepcionesViewModel _registro)
        {
            if (_registro != null)
            {
                txtPlacas.Text = _registro != null ? _registro.Placas : string.Empty;
                txtConductor.Text = _registro != null ? _registro.Chofer : string.Empty;
                txtColor.Text = _registro != null ? _registro.Color : string.Empty;
                txtMarcaModelo.Text = _registro != null ? _registro.MarcaModelo : string.Empty;
            }
        }

        private void InicializarTimer()
        {
            delayTimerVehiculo = new System.Windows.Forms.Timer();
            delayTimerVehiculo.Interval = 400;  // ms de espera
            delayTimerVehiculo.Tick += DelayTimerVehiculo_Tick;

            delayTimerPrecioUnitario = new System.Windows.Forms.Timer();
            delayTimerPrecioUnitario.Interval = 400;  // ms de espera
            delayTimerPrecioUnitario.Tick += DelayTimerPrecioUnitario_Tick;
        }

        private void EjecutarConsulta()
        {
            CargarVehiculoInfo(_repoVehiculos.ObtenerPorPlacasMango(txtPlacas.Text.Trim(), _idProveedorSeleccionado));
        }
        
        private void EstablecerFechasValidasEnElCiclo()
        {
            var _ciclo = _repoCiclos.ObtenerPorCicloId(_IdCicloSeleccionado);

            dtpFecha.MinDate = _ciclo.fecha_inicio;
            if (_ciclo.fecha_fin.HasValue)
                dtpFecha.MaxDate = (DateTime)_ciclo.fecha_fin;
        }

        private void CalcularPesoNeto()
        {
            try
            {
                decimal porcentaje = decimal.Parse(txtPorcentaje.Text);
                decimal peso = decimal.Parse(txtPesoBruto.Text) - decimal.Parse(txtTara.Text);
                if (chkAfectaPeso.Checked)
                {
                    txtPesoNeto.Text = (peso - (peso * porcentaje / 100)).ToString("N2");
                }
                else
                {
                    txtPesoNeto.Text = peso.ToString("N2");
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        private void chkAfectaPeso_CheckedChanged(object sender, EventArgs e)
        {
            CalcularPesoNeto();
            CalcularPrecioTotal();
        }

        private void chkAplicaPorcentajeCostos_CheckedChanged(object sender, EventArgs e)
        {
            txtPorcentajeCosto.Enabled = chkAplicaPorcentajeCostos.Checked;
            if (!chkAplicaPorcentajeCostos.Checked) 
            { 
                txtPorcentajeCosto.Text = "0.00";
                chkAfectaPeso.Checked = false;
            }
            else
            {
                chkAfectaPeso.Checked = false;
            }

                chkAfectaPeso.Enabled = !chkAplicaPorcentajeCostos.Checked;

            CalcularPrecioTotal();
        }

        private void txtPorcentajeCosto_TextChanged(object sender, EventArgs e)
        {
            CalcularPrecioTotal();
        }
    }
}
