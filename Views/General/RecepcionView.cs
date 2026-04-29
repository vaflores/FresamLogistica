using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
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

namespace FresamLogistica.Views.General
{
    public partial class RecepcionView : Form
    {
        private readonly RecepcionesController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly GradosController _repoGrados = new GradosController();
        private readonly AgricultoresController _repoAgricultores = new AgricultoresController();
        private readonly RanchosController _repoRanchos = new RanchosController();
        private readonly SectoresController _repoSectores = new SectoresController();
        private readonly FrutasController _repoFrutas = new FrutasController();
        private readonly VariedadesController _repoVariedades = new VariedadesController();
        private readonly PreciosController _repoPrecios = new PreciosController();
        private readonly VehiculosController _repoVehiculos = new VehiculosController();
        private readonly CondicionesController _repoCondiciones = new CondicionesController();
        private readonly CalidadesController _repoCalidades = new CalidadesController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly TitulosEtiquetasController titulosEtiquetasController = new TitulosEtiquetasController();
        private readonly KardexCajasController _repoKardexCajas = new KardexCajasController();

        private int _IdRegistroSeleccionado;
        private int _IdCicloSeleccionado;
        private int _idAgricultorSeleccionado;
        private int _IdRanchoSeleccionado;
        private int _IdSectorSeleccionado;
        private int _IdFrutaSeleccionada;
        private int _IdVariedadSeleccionada;
        private string _IdGradoSeleccionado;
        private decimal _totalPesoNeto = 0;
        private decimal _totalPrecio = 0;
        private decimal _precioFrutaGrado = 0;
        private int _totalCajas = 0;

        private System.Windows.Forms.Timer delayTimerVehiculo;
        private System.Windows.Forms.Timer delayTimerPrecioUnitario;

        public RecepcionView(int _IdRegistro, int _IdCiclo)
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new RecepcionesController();

            ConfigurarComboBoxEstatus();
            ConfigurarComboBoxCiclos();
            ConfigurarComboBoxGrados();
            ConfigurarComboBoxAgricultores();
            ConfigurarComboBoxRanchos();
            ConfigurarComboBoxSectores();
            ConfigurarComboBoxFrutas();
            ConfigurarComboBoxVariedades();

            ConfigurarDataGridViewTarimas(dgvTarimas);
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

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repoRanchos.ListarPorAgricultor(_IdRegistroSeleccionado));
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {

        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            CrearFilaTarimaDefault();
            NumerarTarimas();
        }

        private void tsbDelGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTarimas.Rows.Count == 0)
                {
                    MessageBox.Show("No hay filas para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvTarimas.CurrentRow == null)
                {
                    MessageBox.Show("No hay ninguna fila seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dgvTarimas.Rows.RemoveAt(dgvTarimas.CurrentRow.Index);

                NumerarTarimas();
                EstablecerCalificacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la fila: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                _idAgricultorSeleccionado = Convert.ToInt32(cbAgricultor.SelectedValue);
                ConfigurarComboBoxRanchos();
                CargarTotalCajasEnPrestamo();

            }
            catch (Exception)
            {
                _idAgricultorSeleccionado = 0;
            }
        }

        private void cbRancho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdRanchoSeleccionado = Convert.ToInt32(cbRancho.SelectedValue);
                ConfigurarComboBoxSectores();
            }
            catch (Exception)
            {
                _IdRanchoSeleccionado = 0;
            }
        }

        private void cbSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdSectorSeleccionado = Convert.ToInt32(cbSector.SelectedValue);
                ConfigurarComboBoxFrutas();
                ConfigurarComboBoxVariedades();
                CargarDatosSectorSeleccionado();
            }
            catch (Exception)
            {
                _IdSectorSeleccionado = 0;
            }
        }

        private void cbFruta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdFrutaSeleccionada = Convert.ToInt32(cbFruta.SelectedValue);
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
                _IdVariedadSeleccionada = Convert.ToInt32(cbVariedad.SelectedValue);
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

        private void dgvTarimas_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try { 
            // Ejemplo: columna 1 y 2 deben ser números 
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out _))
                {
                    MessageBox.Show("Solo se permiten números enteros.");
                    e.Cancel = true; // evita que pierda el foco
                    //dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                    //e.Cancel = false; // no marcar error
                }
            }

            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5) // la columna 2 acepta decimales
            {
                if (!decimal.TryParse(e.FormattedValue.ToString(), out _))
                {
                    MessageBox.Show("Ingrese un valor numérico válido.");
                    e.Cancel = true;
                    //dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                    //e.Cancel = false; // no marcar error
                }
            }
            }
            catch { }
        }

        private void dgvTarimas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                // Columna 1: N0
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    if (decimal.TryParse(dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out decimal valor))
                    {
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor.ToString("N0");
                    }
                    else
                    {
                        dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                    }
                }

                // Columna 2: N2
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    if (decimal.TryParse(dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out decimal valor))
                    {
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor.ToString("N2");

                        CalcularPesoNeto(dgvTarimas.Rows[e.RowIndex]);
                    }
                    else
                    {
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                    }
                }
            }
            catch { }
        }

        private void dgvTarimas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                CalcularPesoNeto(dgvTarimas.Rows[e.RowIndex]);
            }
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
                    if (decimal.TryParse(dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out decimal valor))
                    {
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor.ToString("N0");
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
            CalcularPorcentaje();
        }

        private void txtDesecho_TextChanged(object sender, EventArgs e)
        {
            CalcularPorcentaje();
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
            EstablecerCalificacion();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            EstablecerCalificacion();
        }

        private void chkPrecioUnario_CheckedChanged(object sender, EventArgs e)
        {
            txtPrecioUnitario.Enabled = chkPrecioUnario.Checked;
            EstablecerCalificacion();
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
            chkPrecioUnario.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar Precio Unitario");
            tpCalidadDescuento.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Modificar Calidad/Descuento");
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
            EstadosHelper.GenerarEstadosDesdeLista(ref cbGrado, _repoGrados.ListarComboBoxFruta(this._IdFrutaSeleccionada));
        }
        
        private void ConfigurarComboBoxAgricultores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbAgricultor, _repoAgricultores.ListarComboBox());
        }
        
        private void ConfigurarComboBoxRanchos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbRancho, _repoRanchos.ListarPorAgricultorComboBox(this._idAgricultorSeleccionado));
        }
        
        private void ConfigurarComboBoxSectores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbSector, _repoSectores.ListarPorRanchoCicloComboBox(this._IdRanchoSeleccionado, this._IdCicloSeleccionado));
        }
        
        private void ConfigurarComboBoxFrutas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFruta, _repoFrutas.ListarComboBox());
        }
        
        private void ConfigurarComboBoxVariedades()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbVariedad, _repoVariedades.ListarPorFrutaComboBox(this._IdFrutaSeleccionada));
        }

        private void CargarTotalCajasEnPrestamo()
        {
            txtTotalCajas.Text = _repoKardexCajas.ObtenerSaldoActualCajas(this._idAgricultorSeleccionado).ToString();
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
                    txtFolioDiario.Text = registro.FolioRecepcionDiaria.HasValue ? registro.FolioRecepcionDiaria.Value.ToString() : string.Empty;
                    cbAgricultor.SelectedValue = registro.AgriculorId.ToString();
                    cbRancho.SelectedValue = registro.RanchoId.ToString();
                    ConfigurarComboBoxSectores();
                    cbSector.SelectedValue = registro.SectorId.ToString();
                    cbFruta.SelectedValue = registro.FrutaId.ToString();
                    cbVariedad.SelectedValue = registro.VariedadId.ToString();
                    txtTarimas.Text = registro.NoTarimas.ToString();
                    txtImporte.Text = registro.Importe.HasValue ? registro.Importe.Value.ToString("C2") : "$0.00";
                    dtpFecha.Value = registro.Fecha;
                    cbEstado.SelectedValue = registro.Estado;

                    //Cargar Info Vehiculo
                    CargarVehiculoInfo(registro.VehiculoInfo);

                    //Cargar Info Tarimas
                    CargarDatosTarimasGrid(registro.TarimasInfo);

                    foreach (DataGridViewRow row in dgvTarimas.Rows)
                    {
                        CalcularPesoNeto(row);
                    }

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
                    CargarDatosCalidadesRecepcion(registro.CalidadesInfo);
                    CargarDatosCalidadesActivas(registro.CalidadesActivas);

                    EstablecerCalificacion();

                    if(registro.PrecioUnitario.HasValue)
                    {
                        if(registro.PrecioUnitario != _precioFrutaGrado)
                        { 
                            chkPrecioUnario.Checked = true;
                            txtPrecioUnitario.Text = registro.PrecioUnitario.Value.ToString("N2");
                        }
                    }
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
                cbAgricultor.SelectedIndex = -1;

                //Cargar Info Condiciones
                CargarDatosCondicionesActivas(_repoCondiciones.ListarCondicionesActivasSinValorPorRecepcionId(0));

                //Cargar Info Calidades
                CargarDatosCalidadesActivas(_repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(0));

                EstablecerCalificacion();
                CalcularPorcentaje();
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                //Cargar info recepción
                var registro = new RecepcionesViewModel()
                {
                    RecepcionId = _IdRegistroSeleccionado,
                    FolioRecepcionDiaria = string.IsNullOrWhiteSpace(txtFolioDiario.Text) ? (int?)null : int.Parse(txtFolioDiario.Text),
                    CicloId = _IdCicloSeleccionado,
                    Fecha = dtpFecha.Value,
                    AgriculorId = _idAgricultorSeleccionado,
                    RanchoId = _IdRanchoSeleccionado,
                    SectorId = _IdSectorSeleccionado,
                    FrutaId = _IdFrutaSeleccionada,
                    VariedadId = _IdVariedadSeleccionada,
                    NoTarimas = int.Parse(txtTarimas.Text),
                    Importe = decimal.Parse(txtImporte.Text, System.Globalization.NumberStyles.Currency),
                    PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text, System.Globalization.NumberStyles.Currency),
                    GradoId = _IdGradoSeleccionado,
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

                //Cargar Info Tarimas
                NumerarTarimas();
                List<Tarimas> tarimas = new List<Tarimas>();
                foreach (DataGridViewRow row in dgvTarimas.Rows)
                {
                    Tarimas tarima = new Tarimas()
                    {
                        tarima_id = row.Cells["tarima_id"].Value != null ? Convert.ToInt32(row.Cells["tarima_id"].Value) : 0,
                        no_tarima = row.Cells["no_tarima"].Value != null ? Convert.ToInt32(row.Cells["no_tarima"].Value) : 0,
                        cajas = row.Cells["cajas"].Value != null ? Convert.ToInt32(row.Cells["cajas"].Value) : 0,
                        peso_bruto = row.Cells["peso_bruto"].Value != null ? Convert.ToDecimal(row.Cells["peso_bruto"].Value) : 0,
                        tara1 = row.Cells["tara1"].Value != null ? Convert.ToDecimal(row.Cells["tara1"].Value) : 0,
                        tara2 = row.Cells["tara2"].Value != null ? Convert.ToDecimal(row.Cells["tara2"].Value) : 0,
                        peso_neto = row.Cells["peso_neto"].Value != null ? Convert.ToDecimal(row.Cells["peso_neto"].Value) : 0,
                        grado_id = registro.GradoId,
                        calificacion = int.Parse(txtCalificacion.Text),
                        importe = (row.Cells["peso_neto"].Value != null ? Convert.ToDecimal(row.Cells["peso_neto"].Value) : 0) * _precioFrutaGrado
                    };
                    tarimas.Add(tarima);
                }
                registro.TarimasInfo = tarimas;

                //Cargar Info Descuentos
                DescuentosRecepciones descuentos = new DescuentosRecepciones()
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
                List<CalidadesRecepcionesViewModel> calidadesRecepcion = new List<CalidadesRecepcionesViewModel>();
                foreach (DataGridViewRow row in dgvCalidades.Rows)
                {
                    CalidadesRecepcionesViewModel calidad = new CalidadesRecepcionesViewModel()
                    {
                        CalidadRecepcionId = row.Cells["CalidadRecepcionId"].Value != null ? Convert.ToInt32(row.Cells["CalidadRecepcionId"].Value) : 0,
                        CalidadId = row.Cells["CalidadId"].Value != null ? Convert.ToInt32(row.Cells["CalidadId"].Value) : 0,
                        Nombre = row.Cells["Nombre"].Value != null ? row.Cells["Nombre"].Value.ToString() : string.Empty,
                        Valor = row.Cells["Valor"].Value != null ? Convert.ToInt32(row.Cells["Valor"].Value) : 0
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
            this._IdRegistroSeleccionado = 0;
            CargarDatosRegistroSeleccionado();
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirReciboRecepcion()
        {
            byte[] _logoByte = _repoEmpresa.ObtenerPorId(1).logo;

            DataTable _dt = new DataTable("calidadesDataSet");

            _dt.Columns.Add("RecepcionId", typeof(string));
            _dt.Columns.Add("CalidadId", typeof(string));
            _dt.Columns.Add("CalidadNombre", typeof(string));
            _dt.Columns.Add("CalidadValor", typeof(string));

            foreach(var _c in dgvCalidades.Rows)
            {
                DataRow row = _dt.NewRow();
                row["RecepcionId"] = _IdRegistroSeleccionado.ToString();
                row["CalidadId"] = ((DataGridViewRow)_c).Cells["CalidadId"].Value != null ? ((DataGridViewRow)_c).Cells["CalidadId"].Value.ToString() : string.Empty;
                row["CalidadNombre"] = ((DataGridViewRow)_c).Cells["Nombre"].Value != null ? ((DataGridViewRow)_c).Cells["Nombre"].Value.ToString() : string.Empty;
                row["CalidadValor"] = ((DataGridViewRow)_c).Cells["Valor"].Value != null ? ((DataGridViewRow)_c).Cells["Valor"].Value.ToString() : string.Empty;
                _dt.Rows.Add(row);
            }

            DataRow total = _dt.NewRow();
            total["RecepcionId"] = _IdRegistroSeleccionado.ToString();
            total["CalidadId"] = String.Empty;
            total["CalidadNombre"] = "Total";
            total["CalidadValor"] = (Constantes.Calificacion - int.Parse(txtCalificacion.Text)).ToString();
            _dt.Rows.Add(total);

            List<ReportParameter> parametros = new List<ReportParameter>()
            {
                new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),
                new ReportParameter("recepcionIdParam", _IdRegistroSeleccionado.ToString().ToUpper()),
                new ReportParameter("agricultorParam", cbAgricultor.Text.ToUpper()),
                new ReportParameter("fechaParam", dtpFecha.Value.ToString("dd/MMMM/yyyy hh:mm tt").ToUpper()),
                new ReportParameter("frutaParam", cbFruta.Text.ToUpper()),
                new ReportParameter("variedadParam", cbVariedad.Text.ToUpper()),
                new ReportParameter("gradoParam", GenerarGradoReporte().ToUpper()),
                new ReportParameter("pesoParam", _totalPesoNeto.ToString("N2")),
                new ReportParameter("calificacionParam", GenerarCalidadReporte()),
                new ReportParameter("importeParam", txtImporte.Text),
                new ReportParameter("folioDiarioIdParam", txtFolioDiario.Text),
                new ReportParameter("totalCajasParam", _totalCajas.ToString()),
                new ReportParameter("mostrarImporteParam", chkMostrarImporte.Checked.ToString()),
                new ReportParameter("totalCajasPrestamo", txtTotalCajas.Text),
                new ReportParameter("precioCaja", _repoKardexCajas.ObtenerPrecioCaja().ToString("N2"))
            };

            ReportHelper reporte = new ReportHelper("Reports/General/RecepcionReport/rptReciboRecepcion.rdlc", parametros, _dt);

            reporte.ShowDialog();

        }

        private void ImprimirEtiquetaRecepcion()
        {
            var _empresa = _repoEmpresa.ObtenerPorId(1);
            var _titulos = titulosEtiquetasController.ObtenerTitulosPorCiclo(_IdCicloSeleccionado);
            
            byte[] _logoByte = _empresa.logo;

            DataTable _dt = new DataTable("recepcionDataSet");

            _dt.Columns.Add("RecepcionId", typeof(string));
            _dt.Columns.Add("TarimaNo", typeof(string));
            _dt.Columns.Add("AgricultorId", typeof(string));
            _dt.Columns.Add("AgricultorNombre", typeof(string));
            _dt.Columns.Add("RanchoId", typeof(string));
            _dt.Columns.Add("RanchoNombre", typeof(string));
            _dt.Columns.Add("SectorId", typeof(string));
            _dt.Columns.Add("SectorNombre", typeof(string));
            _dt.Columns.Add("FrutaId", typeof(string));
            _dt.Columns.Add("FrutaNombre", typeof(string));
            _dt.Columns.Add("VariedadId", typeof(string));
            _dt.Columns.Add("VariedadNombre", typeof(string));
            _dt.Columns.Add("TotalCajas", typeof(string));
            _dt.Columns.Add("TotalKgs", typeof(string));
            _dt.Columns.Add("GradoId", typeof(string));
            _dt.Columns.Add("Fecha", typeof(string));
            _dt.Columns.Add("Hora", typeof(string));
            _dt.Columns.Add("FolioDia", typeof(string));

            foreach (DataGridViewRow _t in dgvTarimas.Rows)
            {
                DataRow row = _dt.NewRow();

                row["RecepcionId"] = _IdRegistroSeleccionado.ToString();
                row["TarimaNo"] = _t.Cells["no_tarima"].Value != null ? _t.Cells["no_tarima"].Value.ToString() : string.Empty;
                row["AgricultorId"] = _idAgricultorSeleccionado.ToString();
                row["AgricultorNombre"] = cbAgricultor.Text;
                row["RanchoId"] = _IdRanchoSeleccionado.ToString();
                row["RanchoNombre"] = cbRancho.Text;
                row["SectorId"] = _IdSectorSeleccionado.ToString();
                row["SectorNombre"] = cbSector.Text;
                row["FrutaId"] = _IdFrutaSeleccionada.ToString();
                row["FrutaNombre"] = cbFruta.Text;
                row["VariedadId"] = _IdVariedadSeleccionada.ToString();
                row["VariedadNombre"] = cbVariedad.Text;
                row["TotalCajas"] = _t.Cells["cajas"].Value != null ? _t.Cells["cajas"].Value.ToString() : "0";
                row["TotalKgs"] = _t.Cells["peso_neto"].Value != null ? _t.Cells["peso_neto"].Value.ToString() : "0.00";
                row["GradoId"] = GenerarGradoReporte().ToUpper();
                row["Fecha"] = dtpFecha.Value.ToString("dd/MM/yyyy");
                row["Hora"] = dtpFecha.Value.ToString("hh:mm tt");
                row["FolioDia"] = txtFolioDiario.Text;
                _dt.Rows.Add(row);
            }

            Ciclos _ciclo = _repoCiclos.ObtenerPorId(_IdCicloSeleccionado);

            List<ReportParameter> parametros = new List<ReportParameter>()
            {
                new ReportParameter("nombreEmpresaParam", _empresa.nombre.ToUpper()),
                new ReportParameter("logoParam", Convert.ToBase64String(_logoByte)),

                new ReportParameter("titulo1Param", _titulos != null ? _titulos.titulo1 : string.Empty),
                new ReportParameter("titulo2Param", _titulos != null ? _titulos.titulo2 : string.Empty),
                new ReportParameter("titulo3Param", _titulos != null ? _titulos.titulo3 : string.Empty),

                new ReportParameter("agricultorIdParam", _idAgricultorSeleccionado.ToString()),
                new ReportParameter("proximaRevisionParam", _ciclo?.proxima_revision),
                new ReportParameter("fechaEmisionParam", _ciclo?.fecha_emision),
                new ReportParameter("edicionParam", _ciclo?.edicion)

            };

            ReportHelper reporte = new ReportHelper("Reports/General/RecepcionReport/rptEtiquetaRecepcion.rdlc", parametros, _dt);

            reporte.ShowDialog();

        }

        private void InhabilitarControles()
        {
            cbCiclos.Enabled = false;
            cbFruta.Enabled = false;
            cbVariedad.Enabled = false;
            cbGrado.Enabled = false;

            txtFolioDiario.Enabled = false;
            txtPrecioUnitario.Enabled = false;
            txtTarimas.Enabled = false;
            txtImporte.Enabled = false;
            txtPesoNeto.Enabled = false;
        }

        private void CargarDatosSectorSeleccionado()
        {
            if (_IdSectorSeleccionado > 0)
            {
                _IdVariedadSeleccionada = 0;
                var sector = _repoSectores.ObtenerPorId(_IdSectorSeleccionado);
                if (sector != null)
                {
                    _IdVariedadSeleccionada = sector.variedad_id;

                    if (_IdVariedadSeleccionada > 0)
                    {
                        var variedad = _repoVariedades.ObtenerPorId(_IdVariedadSeleccionada);
                        if (variedad != null)
                        {
                            _IdFrutaSeleccionada = variedad.id_fruta;
                            cbFruta.SelectedValue = _IdFrutaSeleccionada.ToString();
                            cbVariedad.SelectedValue = _IdVariedadSeleccionada.ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el sector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigurarDataGridViewTarimas(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "TarimaRecepcionId", "tarima_id", 150, false);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "No. Tarima", "no_tarima", 150, true, true);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Cajas", "cajas", 150, true, false);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Bruto", "peso_bruto", 150, true, false);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Tara 1", "tara1", 150, true, false);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Tara 2", "tara2", 150, true, false);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Neto", "peso_neto", 150, true, true);

            dgv.CellValueChanged += dgvTarimas_CellValueChanged;

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        public void NumerarTarimas()
        {
            for (int i = 0; i < dgvTarimas.Rows.Count; i++)
            {
                dgvTarimas.Rows[i].Cells[1].Value = (i + 1);
            }

            txtTarimas.Text = (dgvTarimas.Rows.Count).ToString();
        }

        private void CalcularPesoNeto(DataGridViewRow row)
        {
            if (row == null) return;

            decimal pesoBruto = 0;
            decimal tara1 = 0;
            decimal tara2 = 0;

            // Validar valores numéricos
            decimal.TryParse(Convert.ToString(row.Cells["peso_bruto"].Value), out pesoBruto);
            decimal.TryParse(Convert.ToString(row.Cells["tara1"].Value), out tara1);
            decimal.TryParse(Convert.ToString(row.Cells["tara2"].Value), out tara2);

            // Si ambos son válidos, calcular; si no, poner 0
            if (pesoBruto > 0 && tara1 >= 0 && tara2 >= 0)
                row.Cells["peso_neto"].Value = (pesoBruto - tara1 - tara2).ToString("N2");
            else
                row.Cells["peso_neto"].Value = "0.00";

            CalcularPrecioTotal();
            CalcularTara2();
        }

        private void CargarDatosDescuentoPorRecepcion(DescuentosRecepciones _registro)
        {
            if (_registro == null)
            {
                txtPesoCaja.Text = "0.00";
                txtDesecho.Text = "0.00";
                txtPorcentaje.Text = "0.00";
                txtObservaciones.Text = string.Empty;
                chkAceptado.Checked = false;
                chkMateria.Checked = false;
            }
            else 
            { 

                txtPesoCaja.Text = _registro.peso_caja.ToString();
                txtDesecho.Text = _registro.desecho.ToString();
                txtPorcentaje.Text = _registro.porcentaje.ToString();
                txtObservaciones.Text = _registro.observaciones;
                chkAceptado.Checked = _registro.aceptado;
                chkMateria.Checked = _registro.materia;
            }
        }

        private void CargarDatosTarimasGrid(List<Tarimas> _registros)
        {
            dgvTarimas.Rows.Clear();
            DataGridViewRow _fila = new DataGridViewRow();
            foreach (Tarimas t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvTarimas);
                _fila.Cells[0].Value = t.tarima_id;
                _fila.Cells[1].Value = t.no_tarima;
                _fila.Cells[2].Value = t.cajas;
                _fila.Cells[3].Value = t.peso_bruto.ToString("N2");
                _fila.Cells[4].Value = t.tara1.ToString("N2");
                _fila.Cells[5].Value = t.tara2.ToString("N2");
                _fila.Cells[6].Value = t.peso_neto.ToString("N2");
                dgvTarimas.Rows.Add(_fila);
            }
        }

        private void CrearFilaTarimaDefault()
        {
            DataGridViewRow _fila = new DataGridViewRow();

                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvTarimas);
                _fila.Cells[0].Value = 0;
                _fila.Cells[1].Value = 0;
                _fila.Cells[2].Value = 0;
                _fila.Cells[3].Value = 0.ToString("N2");
                _fila.Cells[4].Value = 0.ToString("N2");
                _fila.Cells[5].Value = 0.ToString("N2");
                _fila.Cells[6].Value = 0.ToString("N2");
            dgvTarimas.Rows.Add(_fila);

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

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "CalidadRecepcionId", "CalidadRecepcionId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "CalidadId", "CalidadId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Calidad", "Nombre", 250, true, true);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Valor", "valor", 150, true, false);
            DataGridViewHelper.AddColumnCheckBox(dgv, "AfectaCalificacion", "AfectaCalificacion", 100, false);

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

        private void CargarDatosCalidadesActivas(List<CalidadesRecepcionesViewModel> _registros)
        {
            if (_registros.Count == 0) return;

            DataGridViewRow _fila = new DataGridViewRow();
            foreach (CalidadesRecepcionesViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvCalidades);
                _fila.Cells[0].Value = t.CalidadRecepcionId;
                _fila.Cells[1].Value = t.CalidadId;
                _fila.Cells[2].Value = t.Nombre;
                _fila.Cells[3].Value = t.Valor.ToString("N0");
                _fila.Cells[4].Value = t.AfectaCalificacion;
                dgvCalidades.Rows.Add(_fila);
            }
        }

        private void CargarDatosCalidadesRecepcion(List<CalidadesRecepcionesViewModel> _registros)
        {
            dgvCalidades.Rows.Clear();
            DataGridViewRow _fila = new DataGridViewRow();
            foreach (CalidadesRecepcionesViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(dgvCalidades);
                _fila.Cells[0].Value = t.CalidadRecepcionId;
                _fila.Cells[1].Value = t.CalidadId;
                _fila.Cells[2].Value = t.Nombre;
                _fila.Cells[3].Value = t.Valor.ToString("N0");
                _fila.Cells[4].Value = t.AfectaCalificacion;
                dgvCalidades.Rows.Add(_fila);
            }
        }

        private void ConfigurarTextBoxNumericos()
        {
            Helper.TextBoxValidarEnteros.Attach(txtTarimas);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPrecioUnitario);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoCaja);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtDesecho);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentaje);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtTotalCajas);

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
            CalcularTara2();
        }

        private void CalcularPrecioTotal()
        {
            try
            {

                _totalPesoNeto = 0;
                _totalCajas = 0;
                _IdGradoSeleccionado = cbGrado.SelectedValue.ToString();

                if (chkPrecioUnario.Checked)
                    _precioFrutaGrado = txtPrecioUnitario.Text.Length > 0 ? decimal.Parse(txtPrecioUnitario.Text) : 0;
                else
                {
                    _precioFrutaGrado = _repoPrecios.ObtenerPrecioActual(_IdFrutaSeleccionada, _IdGradoSeleccionado, dtpFecha.Value);
                    txtPrecioUnitario.Text = _precioFrutaGrado.ToString("N2");
                }

                foreach (DataGridViewRow row in dgvTarimas.Rows)
                {
                    decimal pesoNeto = 0;
                    decimal.TryParse(Convert.ToString(row.Cells["peso_neto"].Value), out pesoNeto);
                    _totalPesoNeto += pesoNeto;

                    int noCajas = 0;
                    int.TryParse(Convert.ToString(row.Cells["cajas"].Value), out noCajas);
                    _totalCajas += noCajas;
                }

                _totalPrecio = _totalPesoNeto * _precioFrutaGrado;
                txtImporte.Text = _totalPrecio.ToString("C2");
                txtPesoNeto.Text = _totalPesoNeto.ToString("N2");

            }
            catch (Exception)
            {
                // Ignorar errores de cálculo
            }
        }

        private void CalcularTara2()
        {
            foreach(DataGridViewRow row in dgvTarimas.Rows)
            {
                decimal pesoBruto = 0;
                decimal tara1 = 0;
                decimal porcentaje = 0;
                // Validar valores numéricos
                decimal.TryParse(Convert.ToString(row.Cells["peso_bruto"].Value), out pesoBruto);
                decimal.TryParse(Convert.ToString(row.Cells["tara1"].Value), out tara1);
                decimal.TryParse(txtPorcentaje.Text, out porcentaje);
                if (pesoBruto > 0 && tara1 >= 0 && porcentaje >=0)
                    row.Cells["tara2"].Value = ((pesoBruto - tara1)*porcentaje/100).ToString("N2");
                else
                    row.Cells["tara2"].Value = "0.00";
            }
        }

        private void EstablecerCalificacion()
        {
            try
            {
                txtCalificacion.Text = (Constantes.Calificacion - dgvCalidades.Rows.Cast<DataGridViewRow>()
                    .Where(r => r.Cells["valor"].Value != null && int.TryParse(r.Cells["valor"].Value.ToString(), out int val) && val > 0 && Convert.ToBoolean(r.Cells["AfectaCalificacion"].Value) == true)
                    .Sum(r => int.Parse(r.Cells["valor"].Value.ToString())))
                    .ToString("N0");

                cbGrado.SelectedValue = _repoGrados.ObtenerGradoIdPorFrutaIdCalificacion(_IdFrutaSeleccionada, Int32.Parse(txtCalificacion.Text));

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
            CargarVehiculoInfo(_repoVehiculos.ObtenerPorPlacas(txtPlacas.Text.Trim(), _idAgricultorSeleccionado));
        }
        
        private string GenerarCalidadReporte()
        {
            if(txtImporte.Text != "$0.00" && _totalPesoNeto > 0)
            {
                return txtCalificacion.Text;
            }
            else
            {
                return "";
            }
        }

        private string GenerarGradoReporte()
        {
            if (txtImporte.Text != "$0.00" && _totalPesoNeto > 0)
            {
                return cbGrado.Text;
            }
            else
            {
                return "";
            }
        }


        private void EstablecerFechasValidasEnElCiclo()
        {
            var _ciclo = _repoCiclos.ObtenerPorCicloId(_IdCicloSeleccionado);

            dtpFecha.MinDate = _ciclo.fecha_inicio;
            if (_ciclo.fecha_fin.HasValue)
                dtpFecha.MaxDate = (DateTime)_ciclo.fecha_fin;
        }
        #endregion

        private void tsbDuplicar_Click(object sender, EventArgs e)
        {
            if (dgvTarimas.CurrentRow == null || dgvTarimas.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccione una fila válida.");
                return;
            }

            int rowIndex = dgvTarimas.Rows.Add();

            for (int i = 0; i < dgvTarimas.Columns.Count; i++)
            {
                if (dgvTarimas.Columns[i].Name == "tarima_id")
                {
                    dgvTarimas.Rows[rowIndex].Cells[i].Value = 0;
                } else
                    dgvTarimas.Rows[rowIndex].Cells[i].Value =
                        dgvTarimas.CurrentRow.Cells[i].Value;
            }

            NumerarTarimas();
        }
    }
}
