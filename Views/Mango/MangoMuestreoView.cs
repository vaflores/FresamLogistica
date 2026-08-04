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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FresamLogistica.Views.Mango
{
    public partial class MangoMuestreoView : Form
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
        private readonly CondicionesController _repoCondiciones = new CondicionesController();
        private readonly MangoCalidadesController _repoCalidades = new MangoCalidadesController();
        private readonly MangoMuestreosController _repoMuestreos = new MangoMuestreosController();

        private readonly TitulosCalidadesController _repoTitulos = new TitulosCalidadesController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

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

        private bool _existeMangoChico;

        private int _IdMuestreoSeleccionado;

        public MangoMuestreoView(int _IdRegistro)
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
            ConfigurarComboBoxFirmas();

            ConfigurarDataGridViewCondiciones(dgvCondiciones);
            ConfigurarDataGridViewCalidades(dgvCalidades);
            ConfigurarDataGridViewCalidades(dgvCalidadesPromedio);

            ConfigurarTextBoxNumericos();

            InhabilitarControles();

            this._IdRegistroSeleccionado = _IdRegistro;

            ValidarPermisos();

        }

        #region EventosFormulario
        private void RecepcionView_Load(object sender, EventArgs e)
        {

            _IdFrutaSeleccionada = _repoConfiguracion.ObtenerMangoFrutaId(1);
            ConfigurarComboBoxGrados();
            ConfigurarComboBoxVariedades();

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
            ImprimirMuestreos();
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
            else if (e.Control && e.KeyCode == Keys.P && tsbImprimir.Enabled) // Ctrl + P → Imprimir Recibo
            {
                ImprimirMuestreos();
            }

        }

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repoRanchos.ListarPorAgricultor(_IdRegistroSeleccionado));
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarMuestreosPorRecepcion(_repoMuestreos.ObtenerMuestreosPorRecepcion(_IdRegistroSeleccionado));
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            //Abrir formulario para agregar nuevo muestreo
            Helper.FormulariosHelper.CargarDialogoFormulario(
                new MangoMuestraView(
                    0, 
                    Convert.ToInt16(txtRecepcion.Text), 
                    _IdFrutaSeleccionada,
                    cbElabora.SelectedValue == null ? 0 : int.Parse(cbElabora.SelectedValue?.ToString()),
                    cbVerifica.SelectedValue == null ? 0 : int.Parse(cbVerifica.SelectedValue?.ToString())
                    )
                );
            CargarDatosRegistroSeleccionado();
        }

        private void ActualizarMuestreo(int _muestraId)
        {
            if (_muestraId == 0)
            {
                return;
            }

            Helper.FormulariosHelper.CargarDialogoFormulario(
                new MangoMuestraView(
                    _muestraId, 
                    Convert.ToInt16(txtRecepcion.Text), 
                    _IdFrutaSeleccionada,
                    cbElabora.SelectedValue == null ? 0 : int.Parse(cbElabora.SelectedValue?.ToString()),
                    cbVerifica.SelectedValue == null ? 0 : int.Parse(cbVerifica.SelectedValue?.ToString())
                    )
                );
            CargarDatosRegistroSeleccionado();
        }

        private void cbAgricultor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _idProveedorSeleccionado = Convert.ToInt32(cbProveedores.SelectedValue);
            }
            catch (Exception)
            {
                _idProveedorSeleccionado = 0;
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

        private void dgvCalidades_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            { 
                // Ejemplo: columna 3
                if (e.ColumnIndex == 3)
                {
                    if (!int.TryParse(e.FormattedValue.ToString(), out _))
                    {
                        MessageBox.Show("Solo se permiten números enteros.");
                        //e.Cancel = true; // evita que pierda el foco
                        dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
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
                        dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor.ToString("N0");
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
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(1) && p.NombrePermiso == "Imprimir Recibo");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(1) && p.NombrePermiso == "Imprimir Etiqueta");

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
            var datos = _repoGrados.ListarComboBoxFruta(this._IdFrutaSeleccionada);
            EstadosHelper.GenerarEstadosDesdeLista(ref cbGrados, datos);
            EstadosHelper.GenerarEstadosDesdeLista(ref cbGradoPromedio, datos);
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

        private void ConfigurarComboBoxFirmas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbElabora, _repoEmpleados.ListarComboBox());
            EstadosHelper.GenerarEstadosDesdeLista(ref cbVerifica, _repoEmpleados.ListarComboBox());
            cbElabora.SelectedIndex = -1;
            cbVerifica.SelectedIndex = -1;
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRegistroSeleccionado > 0)
            {
                //Cargar info recepción
                var registro = _repo.ObtenerRecepcionPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    dtpFecha.Enabled = false;
                    cbCiclos.SelectedValue = registro.CicloId.ToString();
                    txtRecepcion.Text = registro.RecepcionId.ToString();
                    txtFolioCiclo.Text = registro.FolioCiclo.HasValue ? registro.FolioCiclo.Value.ToString() : string.Empty;
                    txtFolioProveedor.Text = registro.FolioProveedor.HasValue ? registro.FolioProveedor.Value.ToString() : string.Empty;
                    dtpFecha.Value = registro.Fecha;
                    txtGuiaSanitaria.Text = registro.GuiaSanitaria;
                    cbEstado.SelectedValue = registro.Estado;
                    cbGrados.SelectedValue = registro.GradoId.ToString();
                    txtCalificacion.Text = registro.CalidadPromedio.ToString();
                    txtPrecioUnitario.Text = registro.PrecioUnitario.HasValue ? registro.PrecioUnitario.Value.ToString("C2") : "$0.00";
                    txtImporte.Text = registro.Importe.HasValue ? registro.Importe.Value.ToString("C2") : "$0.00";
                    txtTicketBascula.Text = registro.TicketBascula;
                    txtPesoBruto.Text = registro.PesoBruto.ToString("N2");
                    txtTara.Text = registro.Tara.ToString("N2");
                    txtPesoNeto.Text = registro.PesoNeto.ToString();

                    cbProveedores.SelectedValue = registro.ProveedorId.ToString();
                    cbLocalidades.SelectedValue = registro.LocalidadId.ToString();
                    this._IdLocalidadSeleccionado = registro.LocalidadId;
                    ConfigurarComboBoxPredios();
                    cbPredios.SelectedValue = registro.PredioId.ToString();
                    this._IdPredioSeleccionado = registro.PredioId;
                    ConfigurarComboBoxPropietarios();
                    cbPropietarios.SelectedValue = registro.PropietarioId.ToString();
                    cbFrutas.SelectedValue = this._IdFrutaSeleccionada.ToString();
                    cbVariedades.SelectedValue = registro.VariedadId.ToString();
                    cbElabora.SelectedValue = registro.ResponsableEmpleadoId.ToString();
                    cbVerifica.SelectedValue = registro.VerificaEmpleadoId.ToString();

                    //Cargar Info Vehiculo
                    CargarVehiculoInfo(registro.VehiculoInfo);

                    //Cargar Info Muestreos
                    CargarMuestreosPorRecepcion(_repoMuestreos.ObtenerMuestreosPorRecepcion(registro.RecepcionId));

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
                        CargarDatosCondicionesActivas(_repoCondiciones.ListarCondicionesActivasSinValorPorRecepcionId(0));
                    }

                    //Cargar Info Calidades
                    //CargarDatosCalidadesRecepcion(registro.CalidadesInfo);
                    registro.CalidadesActivas.AddRange(registro.CalidadesInfo);
                    CargarDatosCalidadesActivas(registro.CalidadesActivas.OrderBy(i=>i.CalidadId).ToList(), dgvCalidades);

                    VerificarExisteCalidadMangoChico(registro.CalidadesActivas);
                    EstablecerCalificacion();

                    //Cargar Datos Promedio
                    ActualizarDatosDescuentosPromedio(_repoMuestreos.ConsultarPromedioDescuentosRecepcion(_IdRegistroSeleccionado));
                    CargarDatosCalidadesActivas(_repoMuestreos.ConsultarPromedioCalidadesRecepcion(_IdRegistroSeleccionado), dgvCalidadesPromedio);

                    EstablecerCalificacion();
                    CalcularPorcentajes();

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
                CargarDatosCondicionesActivas(_repoCondiciones.ListarCondicionesActivasSinValorPorRecepcionId(0));

                //Cargar Info Calidades
                CargarDatosCalidadesActivas(_repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(0), dgvCalidades);
                CargarDatosCalidadesActivas(_repoMuestreos.ConsultarPromedioCalidadesRecepcion(0), dgvCalidadesPromedio);

                CalcularPorcentajes();
                EstablecerCalificacion();
            }
        }

        private void GuardarRegistro()
        {
            try
            {

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
                    Tara = decimal.Parse(txtTara.Text),
                    PesoNeto = decimal.Parse(txtPesoNeto.Text),
                    CalidadPromedio = int.Parse(txtCalificacion.Text),
                    GradoId = _IdGradoSeleccionado,
                    Importe = decimal.Parse(txtImporte.Text, System.Globalization.NumberStyles.Currency),
                    Placas = txtPlacas.Text,
                    PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text, System.Globalization.NumberStyles.Currency),
                    Estado = cbEstado.SelectedValue.ToString()
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
                    materia = chkMateria.Checked
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
                //this.Close();
                CargarDatosRegistroSeleccionado();

                //ImprimirRegistro();

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
            Debug.WriteLine("Función no implementada.");
        }

        private async void ImprimirMuestreos()
        {

            try
            {

                if (_IdRegistroSeleccionado <= 0)
                    return;

                DataTable datos = await _repoMuestreos.ObteneReporteMuestreosPorRecepcionAsync(_IdRegistroSeleccionado);

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

                var _ciclos = _repoCiclos.ObtenerCicloPorFecha(dtpFecha.Value);
                Ciclos _ciclo = _repoCiclos.ObtenerPorCicloId(_ciclos);
                var _titulos = _repoTitulos.ObtenerPorCicloId(_ciclos);
                var _empresa = _repoEmpresa.ObtenerPorId(1);
                byte[] _logoByte = _empresa.logo;


                // Dentro del método ImprimirMuestreos(), inicializa todas las variables decimales antes de usarlas.
                decimal _podrido = 0, _insecto = 0, _mancha = 0, _mallugado = 0, 
                    _madurado = 0, _chico = 0, _pChico = 0, _quemadura = 0, 
                    _defecto = 0, _pDefecto = 0, _pBueno = 0, _pMaduro = 0,
                    _count = 0;
                string _aceptado = "☑", _materia = "☑";

                #region definir_tabla
                DataTable _dtMaster = new DataTable("dtsMuestreos");

                _dtMaster.Columns.Add("fecha_recepcion", typeof(string));
                _dtMaster.Columns.Add("hora", typeof(string));
                _dtMaster.Columns.Add("codigo", typeof(string));
                _dtMaster.Columns.Add("folio_ciclo", typeof(string));
                _dtMaster.Columns.Add("nombre_variedad", typeof(string));
                _dtMaster.Columns.Add("muestreo_numero", typeof(string));
                _dtMaster.Columns.Add("mango_defecto", typeof(string));
                _dtMaster.Columns.Add("mango_bueno", typeof(string));
                _dtMaster.Columns.Add("mango_grado", typeof(string));
                _dtMaster.Columns.Add("recepcion_id", typeof(string));
                _dtMaster.Columns.Add("fecha", typeof(string));
                _dtMaster.Columns.Add("peso_caja", typeof(string));
                _dtMaster.Columns.Add("desecho", typeof(string));
                _dtMaster.Columns.Add("porcentaje", typeof(string));
                _dtMaster.Columns.Add("porcentaje_maduro", typeof(string));
                _dtMaster.Columns.Add("porcentaje_chico", typeof(string));
                _dtMaster.Columns.Add("aceptado", typeof(string));
                _dtMaster.Columns.Add("materia", typeof(string));
                _dtMaster.Columns.Add("Mallugado", typeof(string));
                _dtMaster.Columns.Add("Insecto", typeof(string));
                _dtMaster.Columns.Add("Mancha", typeof(string));
                _dtMaster.Columns.Add("Podrido", typeof(string));
                _dtMaster.Columns.Add("Quemadura", typeof(string));
                _dtMaster.Columns.Add("Madurado", typeof(string));
                _dtMaster.Columns.Add("Chico", typeof(string));

                #endregion

                foreach (DataRow _rec in datos.Rows)
                {
                    DataRow row = _dtMaster.NewRow();

                    row["fecha_recepcion"] = _rec.ItemArray[0];
                    row["hora"] = _rec.ItemArray[1];
                    row["codigo"] = _rec.ItemArray[2];
                    row["folio_ciclo"] = _rec.ItemArray[3];
                    row["nombre_variedad"] = _rec.ItemArray[4];
                    row["muestreo_numero"] = _rec.ItemArray[5];
                    row["mango_defecto"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[6]), 2).ToString("N2");
                    row["mango_bueno"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[7]), 2).ToString("N2");
                    _pBueno += Convert.ToDecimal(row["mango_bueno"]);
                    row["mango_grado"] = _rec.ItemArray[8];
                    row["recepcion_id"] = _rec.ItemArray[9];
                    row["fecha"] = _rec.ItemArray[10];
                    row["peso_caja"] = _rec.ItemArray[11];
                    row["desecho"] = _rec.ItemArray[12];
                    _defecto += Convert.ToDecimal(row["desecho"]);
                    row["porcentaje"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[13]), 2).ToString("N2");
                    _pDefecto += Convert.ToDecimal(row["porcentaje"]);
                    row["porcentaje_maduro"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[14]),2).ToString("N2");
                    _pMaduro += Convert.ToDecimal(row["porcentaje_maduro"]);
                    row["porcentaje_chico"] = Math.Round(Convert.ToDecimal(_rec.ItemArray[23])*100/Convert.ToDecimal(_rec.ItemArray[11]),2).ToString("N2");
                    _pChico += Convert.ToDecimal(row["porcentaje_chico"]);
                    row["aceptado"] = _rec.ItemArray[15].ToString() == "True" ? "☑" : "☐";
                    if (_rec.ItemArray[15].ToString() != "True" && _aceptado == "☑")
                        _aceptado = "☐";
                    row["materia"] = _rec.ItemArray[16].ToString() == "True" ? "☑" : "☐";
                    if (_rec.ItemArray[16].ToString() != "True" && _materia == "☑")
                        _materia = "☐";
                    row["Mallugado"] = _rec.ItemArray[17];
                    _mallugado += Convert.ToDecimal(row["Mallugado"]);
                    row["Insecto"] = _rec.ItemArray[18];
                    _insecto += Convert.ToDecimal(row["Insecto"]);
                    row["Mancha"] = _rec.ItemArray[19];
                    _mancha += Convert.ToDecimal(row["Mancha"]);
                    row["Podrido"] = _rec.ItemArray[20];
                    _podrido += Convert.ToDecimal(row["Podrido"]);
                    row["Quemadura"] = _rec.ItemArray[21];
                    _quemadura += Convert.ToDecimal(row["Quemadura"]);
                    row["Madurado"] = _rec.ItemArray[22];
                    _madurado += Convert.ToDecimal(row["Madurado"]);
                    row["Chico"] = _rec.ItemArray[23];
                    _chico += Convert.ToDecimal(row["Chico"]);
                    _dtMaster.Rows.Add(row);
                    _count++;

                }

                _pBueno /= _count;
                _defecto /= _count;
                _pDefecto /= _count;
                _pMaduro /= _count;
                _pChico /= _count;
                _mallugado /= _count;
                _insecto /= _count;
                _mancha /= _count;
                _podrido /= _count;
                _quemadura /= _count;
                _madurado /= _count;
                _chico /= _count;
                string _grado = _repoGrados.ObtenerGradoIdPorFrutaIdMangoCalificacion(3, Convert.ToInt16(_pBueno));


                List<ReportParameter> parametros = new List<ReportParameter>()
                {
                    new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                    new ReportParameter("nombreEmpresaParam", _empresa.nombre),
                    new ReportParameter("titulo1Param", _titulos == null ? "" : _titulos.titulo1),
                    new ReportParameter("titulo2Param", "Muestreo de calidad del mango"),
                    new ReportParameter("titulo3Param", _titulos == null ? "" : _titulos.titulo3),
                    new ReportParameter("fechaReporteParam", dtpFecha.Value.ToString("dd/MM/yyyy").ToUpper()),

                    new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                    new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                    new ReportParameter("edicionParam", _ciclo?.edicion),

                    new ReportParameter("fechaRecepcion", dtpFecha.Value.ToShortDateString()),
                    new ReportParameter("proveedorRecepcion", cbProveedores.Text),
                    new ReportParameter("numeroViaje", txtFolioCiclo.Text),
                    new ReportParameter("variedadRecepcion", cbVariedades.Text),

                    new ReportParameter("observacionesParam", txtObservaciones.Text),
                    new ReportParameter("responsableParam", cbElabora.Text),
                    new ReportParameter("verificaParam", cbVerifica.Text),

                    //Promedios

                    new ReportParameter("p_podrido", _podrido.ToString("N2")),
                    new ReportParameter("p_insecto", _insecto.ToString("N2")),
                    new ReportParameter("p_mancha", _mancha.ToString("N2")),
                    new ReportParameter("p_mallugado", _mallugado.ToString("N2")),
                    new ReportParameter("p_madurado", _madurado.ToString("N2")),
                    new ReportParameter("p_chico", _chico.ToString("N2")),
                    new ReportParameter("p_pchico", _pChico.ToString("N2")),
                    new ReportParameter("p_quemadura", _quemadura.ToString("N2")),
                    new ReportParameter("p_defecto", _defecto.ToString("N2")),
                    new ReportParameter("p_pdefecto", _pDefecto.ToString("N2")),
                    new ReportParameter("p_pbueno", _pBueno.ToString("N2")),
                    new ReportParameter("p_grado", _grado),
                    new ReportParameter("p_maduro", _pMaduro.ToString("N2")),
                    new ReportParameter("p_aceptado", _aceptado),   
                    new ReportParameter("p_materia", _materia),
                };

                ReportHelper reporte = new ReportHelper(
                    "Reports/Mango/MangoMuestreo/rptMangoMuestreos.rdlc",
                    parametros,
                    _dtMaster);

                reporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message, "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InhabilitarControles()
        {
            cbEstado.Enabled = false;
            cbGrados.Enabled = false;
            cbFrutas.Enabled = false;
            cbVariedades.Enabled = false;
            cbGrados.Enabled = false;
            cbGradoPromedio.Enabled = false;
            cbProveedores.Enabled = false;
            cbLocalidades.Enabled = false;
            cbPredios.Enabled = false;
            cbPropietarios.Enabled = false;
            cbElabora.Enabled = false;
            cbVerifica.Enabled = false;

            dtpFecha.Enabled = false;

            txtRecepcion.Enabled = false;
            txtCalificacion.Enabled = false;
            txtCalificacionPromedio.Enabled = false;

            txtPlacas.Enabled = false;
            txtColor.Enabled = false;
            txtConductor.Enabled = false;
            txtMarcaModelo.Enabled = false;

            txtPesoCaja.Enabled = false;
            txtDesecho.Enabled = false;
            txtPorcentaje.Enabled = false;
            chkAceptado.Enabled = false;
            chkMateria.Enabled = false;

        }

        private void CargarDatosSectorSeleccionado()
        {
            if (_IdPredioSeleccionado > 0)
            {
                cbPropietarios.SelectedValue = this._IdPropietarioSeleccionado.ToString();
            }
        }

        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string _columnName)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                _columnName == "Editar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar"))
            {
                // Obtén el objeto actual (puede ser por ID)
                _IdMuestreoSeleccionado = Convert.ToInt32(dgvMuestreos.Rows[e.RowIndex].Cells["muestreo_id"].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarMuestreo(_IdMuestreoSeleccionado);
            }
            else if (e.RowIndex >= 0 &&
                _columnName == "Editar")
            {
                MessageBox.Show("No tiene permiso para editar registros.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarMuestreosPorRecepcion(DataTable _muestreos)
        {
            dgvMuestreos.DataSource = null;
            DataGridViewHelper.AddColumnEditar(dgvMuestreos, "Editar", "Editar", 50);
            dgvMuestreos.DataSource = _muestreos;


            dgvMuestreos.ReadOnly = true;

            foreach (DataGridViewColumn columna in dgvMuestreos.Columns)
            {
                // Nombre interno
                string nombre = columna.Name;

                // Cambiar header
                switch (nombre)
                {
                    case "muestreo_id":
                        columna.HeaderText = "Muestreo Id";
                        columna.Visible = false;
                        break;

                    case "muestreo_numero":
                        columna.HeaderText = "Muestra";
                        break;

                    case "mango_recepcion_id":
                        columna.HeaderText = "Recepción";
                        columna.Visible = false;
                        break;

                    case "peso_caja":
                        columna.HeaderText = "Peso por Caja";
                        columna.DefaultCellStyle.Format = "N2";
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "desecho":
                        columna.HeaderText = "Desecho";
                        columna.DefaultCellStyle.Format = "N2";
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "porcentaje":
                        columna.HeaderText = "Porcentaje Descuento";
                        columna.DefaultCellStyle.Format = "N2";
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "porcentaje_maduro":
                        columna.HeaderText = "Porcentaje Maduro";
                        columna.DefaultCellStyle.Format = "N2";
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "aceptado":
                        columna.HeaderText = "Aceptado";
                        break;

                    case "materia":
                        columna.HeaderText = "Materia Extraña";
                        break;

                    case "mango_bueno":
                        columna.HeaderText = "Porcentaje Mango Bueno";
                        columna.DefaultCellStyle.Format = "N2";
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "mango_grado":
                        columna.HeaderText = "Grado";
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "Editar":
                        columna.HeaderText = "Editar";
                        break;
                    default:
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                }

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
                txtPorcentajeMaduro.Text = "0.00";
            }
            else 
            { 

                txtPesoCaja.Text = _registro.peso_caja.ToString();
                txtDesecho.Text = _registro.desecho.ToString();
                txtPorcentaje.Text = _registro.porcentaje.ToString();
                txtObservaciones.Text = _registro.observaciones;
                chkAceptado.Checked = _registro.aceptado;
                chkMateria.Checked = _registro.materia;
                txtPorcentajeMaduro.Text = _registro.porcentaje_maduro.ToString();
            }
        }


        private void ConfigurarDataGridViewCondiciones(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "CondicionVehiculoRecepcionId", "CondicionVehiculoRecepcionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "VehiculoRecepcionId", "VehiculoRecepcionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "CondicionId", "CondicionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Condición", "Nombre", 250, true, true);
            DataGridViewHelper.AddColumnCheckBox(dgv, "Valor", "valor", 150, true, false);

            //dgv.CellValueChanged += dgvTarimas_CellValueChanged;

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void ConfigurarDataGridViewCalidades(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "CalidadRecepcionId", "CalidadRecepcionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "CalidadId", "CalidadId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Calidad", "Nombre", 250, true, true);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Valor", "valor", 150, true, false);
            DataGridViewHelper.AddColumnCheckBox(dgv, "AfectaCalificacion", "AfectaCalificacion", 100, false);
            DataGridViewHelper.AddColumnCheckBox(dgv, "UsoPorcentajeMangoChico", "UsoPorcentajeMangoChico", 100, false);

            //dgv.CellValueChanged += dgvTarimas_CellValueChanged;

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

        private void CargarDatosCalidadesActivas(List<MangoCalidadesRecepcionesViewModel> _registros, DataGridView _dgv)
        {
            _dgv.Rows.Clear();
            if (_registros.Count == 0) return;

            DataGridViewRow _fila = new DataGridViewRow();
            foreach (MangoCalidadesRecepcionesViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(_dgv);
                _fila.Cells[0].Value = t.CalidadRecepcionId;
                _fila.Cells[1].Value = t.CalidadId;
                _fila.Cells[2].Value = t.Nombre;
                _fila.Cells[3].Value = t.Valor.ToString("N2");
                _fila.Cells[4].Value = t.AfectaCalificacion;
                _fila.Cells[5].Value = t.UsoPorcentajeMangoChico;
                _dgv.Rows.Add(_fila);
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

            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoBruto);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtTara);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoNeto);

            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoCaja);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtDesecho);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentaje);

        }

        private void VerificarExisteCalidadMangoChico(List<MangoCalidadesRecepcionesViewModel> _calidadesMuestreos)
        {
            _existeMangoChico = _calidadesMuestreos.Where(c => c.UsoPorcentajeMangoChico == true).Any();

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

                    //if (Convert.ToBoolean(row.Cells["UsoPorcentajeMangoChico"].Value))
                    //    txtPorcentajeMangoChico.Text = (Convert.ToDecimal(row.Cells["Valor"].Value) * 100 / pesoCaja).ToString("N2");

                }

                CalcularPorcentajePesoChico();

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

        private void CalcularPorcentajePesoChico()
        {
            int _count = 0;
            decimal _porcentaje = 0;

            foreach (DataGridViewRow r in dgvMuestreos.Rows)
            {
                _count++;
                _porcentaje += Math.Round(Convert.ToDecimal(r.Cells["Tamaño chico (≤350 g)"].Value)*100/ Convert.ToDecimal(r.Cells["peso_caja"].Value),2);
            }

            txtPorcentajeMangoChico.Text = (_porcentaje / _count).ToString("N2");
            txtPorcentajeMangoChicoPromedio.Text = (_porcentaje / _count).ToString("N2");
        }

        private void EstablecerCalificacion()
        {
            try
            {
                txtCalificacion.Text = (100 - Convert.ToDecimal(txtPorcentaje.Text)).ToString("N2");

                txtCalificacionPromedio.Text = (100 - Convert.ToDecimal(txtPorcentajePromedio.Text)).ToString("N2");

                cbGrados.SelectedValue = _repoGrados.ObtenerGradoIdPorFrutaIdMangoCalificacion(_IdFrutaSeleccionada, Convert.ToInt16(decimal.Parse(txtCalificacion.Text)));
                cbGradoPromedio.SelectedValue = _repoGrados.ObtenerGradoIdPorFrutaIdMangoCalificacion(_IdFrutaSeleccionada, Convert.ToInt16(decimal.Parse(txtCalificacionPromedio.Text)));

            }
            catch (Exception)
            {
                //txtCalificacion.Text = Constantes.Calificacion.ToString("N0");
            }
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

        public void ActualizarDatosDescuentosPromedio(DataTable tableDescuentos)
        {
            if (tableDescuentos.Rows.Count > 0)
            {
                txtPesoCajaPromedio.Text = tableDescuentos.Rows[0]["peso_caja_promedio"].ToString();
                txtDesechoPromedio.Text = tableDescuentos.Rows[0]["desecho_promedio"].ToString();
                txtPorcentajePromedio.Text = tableDescuentos.Rows[0]["porcentaje_promedio"].ToString();
                chkAceptadoPromedio.Checked = Convert.ToBoolean(tableDescuentos.Rows[0]["aceptado"]);
                chkMateriaPromedio.Checked = Convert.ToBoolean(tableDescuentos.Rows[0]["materia"]);
                txtObservacionesPromedio.Text = tableDescuentos.Rows[0]["observaciones"].ToString();
                txtPorcentajeMaduroPromedio.Text = tableDescuentos.Rows[0]["porcentaje_maduro_promedio"].ToString();
            }
            else
            {
                txtPesoCajaPromedio.Text = "0.00";
                txtDesechoPromedio.Text = "0.00";
                txtPorcentajePromedio.Text = "0.00";
                chkAceptadoPromedio.Checked = false;
                chkMateriaPromedio.Checked = false;
                txtObservacionesPromedio.Text = string.Empty;
                txtPorcentajeMaduroPromedio.Text = "0.00";
            }
        }

        #endregion

        private void dgvMuestreos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvMuestreos.Columns[e.ColumnIndex].Name);
        }

        private void dgvMuestreos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

    }
}
