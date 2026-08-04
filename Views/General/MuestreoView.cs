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
    public partial class MuestreoView : Form
    {
        private readonly RecepcionesController _repo;
        private readonly GradosController _repoGrados = new GradosController();
        private readonly AgricultoresController _repoAgricultores = new AgricultoresController();
        private readonly RanchosController _repoRanchos = new RanchosController();
        private readonly SectoresController _repoSectores = new SectoresController();
        private readonly FrutasController _repoFrutas = new FrutasController();
        private readonly VariedadesController _repoVariedades = new VariedadesController();
        private readonly VehiculosController _repoVehiculos = new VehiculosController();
        private readonly CondicionesController _repoCondiciones = new CondicionesController();
        private readonly CalidadesController _repoCalidades = new CalidadesController();
        private readonly MuestreosRecepcionesController _repoMuestreos = new MuestreosRecepcionesController();

        private int _IdRegistroSeleccionado;
        private int _IdCicloSeleccionado;
        private int _idAgricultorSeleccionado;
        private int _IdRanchoSeleccionado;
        private int _IdSectorSeleccionado;
        private int _IdFrutaSeleccionada;
        private int _IdVariedadSeleccionada;
        private string _IdGradoSeleccionado;

        private int _IdMuestreoSeleccionado;

        public MuestreoView(int _IdRegistro)
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new RecepcionesController();

            ConfigurarComboBoxEstatus();
            ConfigurarComboBoxGrados();
            ConfigurarComboBoxAgricultores();
            ConfigurarComboBoxRanchos();
            ConfigurarComboBoxSectores();
            ConfigurarComboBoxFrutas();
            ConfigurarComboBoxVariedades();

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
            Helper.FormulariosHelper.CargarDialogoFormulario(new MuestraView(0, Convert.ToInt16(txtRecepcion.Text), _IdFrutaSeleccionada));
            CargarDatosRegistroSeleccionado();
        }

        private void ActualizarMuestreo(int _muestraId)
        {
            if (_muestraId == 0)
            {
                return;
            }

            Helper.FormulariosHelper.CargarFormulario(new MuestraView(_muestraId, Convert.ToInt16(txtRecepcion.Text), _IdFrutaSeleccionada), this);
            CargarDatosRegistroSeleccionado();
        }

        private void cbAgricultor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _idAgricultorSeleccionado = Convert.ToInt32(cbAgricultor.SelectedValue);
                ConfigurarComboBoxRanchos();
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
            CalcularPorcentaje();
        }

        private void txtDesecho_TextChanged(object sender, EventArgs e)
        {
            CalcularPorcentaje();
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

        private void ConfigurarComboBoxEstatus()
        {
            EstadosHelper.GenerarEstadosRecepciones(ref cbEstado);
        }
                
        private void ConfigurarComboBoxGrados()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbGrado, _repoGrados.ListarComboBoxFruta(this._IdFrutaSeleccionada));
            EstadosHelper.GenerarEstadosDesdeLista(ref cbGradoPromedio, _repoGrados.ListarComboBoxFruta(this._IdFrutaSeleccionada));
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
            EstadosHelper.GenerarEstadosDesdeLista(ref cbSector, _repoSectores.ListarPorRanchoComboBox(this._IdRanchoSeleccionado));
        }
        
        private void ConfigurarComboBoxFrutas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFruta, _repoFrutas.ListarComboBox());
        }
        
        private void ConfigurarComboBoxVariedades()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbVariedad, _repoVariedades.ListarPorFrutaComboBox(this._IdFrutaSeleccionada));
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
                    txtRecepcion.Text = registro.RecepcionId.ToString();
                    txtFolioDiario.Text = registro.FolioRecepcionDiaria.HasValue ? registro.FolioRecepcionDiaria.Value.ToString() : string.Empty;
                    cbAgricultor.SelectedValue = registro.AgriculorId.ToString();
                    cbRancho.SelectedValue = registro.RanchoId.ToString();
                    ConfigurarComboBoxSectores();
                    cbSector.SelectedValue = registro.SectorId.ToString();
                    cbFruta.SelectedValue = registro.FrutaId.ToString();
                    cbVariedad.SelectedValue = registro.VariedadId.ToString();
                    txtTotalCajas.Text = registro.TarimasInfo.Sum(t => t.cajas).ToString();
                    dtpFecha.Value = registro.Fecha;
                    cbEstado.SelectedValue = registro.Estado;

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
                    CargarDatosCalidadesActivas(registro.CalidadesActivas.OrderBy(i=>i.Nombre).ToList(), dgvCalidades);

                    EstablecerCalificacion();

                    //Cargar Datos Promedio
                    ActualizarDatosDescuentosPromedio(_repoMuestreos.ConsultarPromedioDescuentosRecepcion(_IdRegistroSeleccionado));
                    CargarDatosCalidadesActivas(_repoMuestreos.ConsultarPromedioCalidadesRecepcion(_IdRegistroSeleccionado), dgvCalidadesPromedio);

                    EstablecerCalificacion();

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
                cbAgricultor.SelectedIndex = -1;

                //Cargar Info Condiciones
                CargarDatosCondicionesActivas(_repoCondiciones.ListarCondicionesActivasSinValorPorRecepcionId(0));

                //Cargar Info Calidades
                CargarDatosCalidadesActivas(_repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(0), dgvCalidades);
                CargarDatosCalidadesActivas(_repoMuestreos.ConsultarPromedioCalidadesRecepcion(0), dgvCalidadesPromedio);

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
                    NoTarimas = int.Parse(txtTotalCajas.Text),
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

                //Cargar Info Muestreos
                List<Tarimas> tarimas = new List<Tarimas>();
                foreach (DataGridViewRow row in dgvMuestreos.Rows)
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
                        importe = (row.Cells["peso_neto"].Value != null ? Convert.ToDecimal(row.Cells["peso_neto"].Value) : 0) 
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
            Debug.WriteLine("Función no implementada.");
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void InhabilitarControles()
        {
            cbEstado.Enabled = false;
            cbGrado.Enabled = false;
            cbFruta.Enabled = false;
            cbVariedad.Enabled = false;
            cbGrado.Enabled = false;
            cbGradoPromedio.Enabled = false;
            cbAgricultor.Enabled = false;
            cbRancho.Enabled = false;
            cbSector.Enabled = false;
            cbFruta.Enabled = false;
            cbVariedad.Enabled = false;

            dtpFecha.Enabled = false;

            txtFolioDiario.Enabled = false;
            txtRecepcion.Enabled = false;
            txtTotalCajas.Enabled = false;
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

        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string _columnName)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                _columnName == "Editar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar"))
            {
                // Obtén el objeto actual (puede ser por ID)
                _IdMuestreoSeleccionado = Convert.ToInt32(dgvMuestreos.Rows[e.RowIndex].Cells[1].Value);

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
                        break;

                    case "recepcion_id":
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
                        columna.HeaderText = "Porcentaje";
                        columna.DefaultCellStyle.Format = "N2";
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "aceptado":
                        columna.HeaderText = "Aceptado";
                        break;

                    case "materia":
                        columna.HeaderText = "Materia Extraña";
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

        private void CargarDatosCalidadesActivas(List<CalidadesRecepcionesViewModel> _registros, DataGridView _dgv)
        {
            _dgv.Rows.Clear();
            if (_registros.Count == 0) return;

            DataGridViewRow _fila = new DataGridViewRow();
            foreach (CalidadesRecepcionesViewModel t in _registros)
            {
                _fila = new DataGridViewRow();
                _fila.CreateCells(_dgv);
                _fila.Cells[0].Value = t.CalidadRecepcionId;
                _fila.Cells[1].Value = t.CalidadId;
                _fila.Cells[2].Value = t.Nombre;
                _fila.Cells[3].Value = t.Valor.ToString("N0");
                _fila.Cells[4].Value = t.AfectaCalificacion;
                _dgv.Rows.Add(_fila);
            }
        }

        private void ConfigurarTextBoxNumericos()
        {
            Helper.TextBoxValidarEnteros.Attach(txtTotalCajas);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoCaja);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtDesecho);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentaje);

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

        private void EstablecerCalificacion()
        {
            try
            {
                txtCalificacion.Text = (Constantes.Calificacion - dgvCalidades.Rows.Cast<DataGridViewRow>()
                    .Where(r => r.Cells["valor"].Value != null && int.TryParse(r.Cells["valor"].Value.ToString(), out int val) && val > 0 && Convert.ToBoolean(r.Cells["AfectaCalificacion"].Value) == true)
                    .Sum(r => int.Parse(r.Cells["valor"].Value.ToString())))
                    .ToString("N0");

                txtCalificacionPromedio.Text = (Constantes.Calificacion - dgvCalidadesPromedio.Rows.Cast<DataGridViewRow>()
                    .Where(r => r.Cells["valor"].Value != null && int.TryParse(r.Cells["valor"].Value.ToString(), out int val) && val > 0 && Convert.ToBoolean(r.Cells["AfectaCalificacion"].Value) == true)
                    .Sum(r => int.Parse(r.Cells["valor"].Value.ToString())))
                    .ToString("N0");

                cbGrado.SelectedValue = _repoGrados.ObtenerGradoIdPorFrutaIdCalificacion(_IdFrutaSeleccionada, Int32.Parse(txtCalificacion.Text));
                cbGradoPromedio.SelectedValue = _repoGrados.ObtenerGradoIdPorFrutaIdCalificacion(_IdFrutaSeleccionada, Int32.Parse(txtCalificacionPromedio.Text));

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
            }
            else
            {
                txtPesoCajaPromedio.Text = "0.00";
                txtDesechoPromedio.Text = "0.00";
                txtPorcentajePromedio.Text = "0.00";
                chkAceptadoPromedio.Checked = false;
                chkMateriaPromedio.Checked = false;
                txtObservacionesPromedio.Text = string.Empty;
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
