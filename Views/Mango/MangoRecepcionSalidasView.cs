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
    public partial class MangoRecepcionSalidasView : Form
    {

        private readonly MangoRecepcionesController _repo;
        private readonly MangoConfiguracionController _repoConfiguracion = new MangoConfiguracionController();
        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly MangoProveedoresController _repoProveedores = new MangoProveedoresController();
        private readonly MangoLocalidadesController _repoLocalidades = new MangoLocalidadesController();
        private readonly MangoPrediosController _repoPredios = new MangoPrediosController();
        private readonly MangoPropietariosController _repoPropietarios = new MangoPropietariosController();
        private readonly FrutasController _repoFrutas = new FrutasController();
        private readonly VariedadesController _repoVariedades = new VariedadesController();
        private readonly CondicionesController _repoCondiciones = new CondicionesController();
        private readonly MangoRecepcionesSalidasController _repoSalidas = new MangoRecepcionesSalidasController();

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

        private int _IdSalidaSeleccionado;

        public MangoRecepcionSalidasView(int _IdRegistro)
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new MangoRecepcionesController();

            ConfigurarComboBoxEstatus();
            ConfigurarComboBoxCiclos();
            ConfigurarComboBoxProveedores();
            ConfigurarComboBoxLocalidades();
            ConfigurarComboBoxPredios();
            ConfigurarComboBoxFrutas();
            ConfigurarComboBoxVariedades();

            ConfigurarDataGridViewCondiciones(dgvCondiciones);
            ConfigurarDataGridView(dgvSalidas);

            ConfigurarTextBoxNumericos();

            InhabilitarControles();

            this._IdRegistroSeleccionado = _IdRegistro;

            this.Tag = "40"; // Asignar un ID de pantalla único para permisos (ajustar según corresponda)

            ValidarPermisos();

        }

        #region EventosFormulario
        private void RecepcionView_Load(object sender, EventArgs e)
        {

            _IdFrutaSeleccionada = _repoConfiguracion.ObtenerMangoFrutaId(1);
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
            }

        }

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repoRanchos.ListarPorAgricultor(_IdRegistroSeleccionado));
        }

        private async void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            _repoSalidas.RecalcularMangoRecepcionesSalidas(_IdRegistroSeleccionado);
            //CargarMuestreosPorRecepcion(await _repoSalidas.ConsultarMangoRecepcionesSalidasPorRecepcion(_IdRegistroSeleccionado));
            CargarDatosRegistroSeleccionado();
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            //Abrir formulario para agregar nuevo muestreo
            Helper.FormulariosHelper.CargarDialogoFormulario(
                new MangoRecepcionSalidaView(
                    _IdRegistroSeleccionado,
                    0,
                     Convert.ToDecimal(txtPesoDisponible.Text),
                     dtpFecha.Value,
                     Convert.ToInt16(cbProveedores.SelectedValue),
                     txtFolioCiclo.Text,
                     cbVariedades.Text,
                     cbPredios.Text
                    )
                );
            _repoSalidas.RecalcularMangoRecepcionesSalidas(_IdRegistroSeleccionado);
            CargarDatosRegistroSeleccionado();
        }

        private void ActualizarMuestreo(int _registroId)
        {
            if (_registroId == 0)
            {
                return;
            }

            Helper.FormulariosHelper.CargarDialogoFormulario(
                new MangoRecepcionSalidaView(
                    _IdRegistroSeleccionado,
                    _registroId,
                     Convert.ToDecimal(txtPesoDisponible.Text),
                     dtpFecha.Value,
                     Convert.ToInt16(cbProveedores.SelectedValue),
                     txtFolioCiclo.Text,
                     cbVariedades.Text,
                     cbPredios.Text
                    )
                );
            _repoSalidas.RecalcularMangoRecepcionesSalidas(_IdRegistroSeleccionado);
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
            }
            catch (Exception)
            {
                _IdVariedadSeleccionada = 0;
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

        private async void CargarDatosRegistroSeleccionado()
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
                    cbEstado.SelectedValue = registro.Estado;
                    txtPesoBruto.Text = registro.PesoBruto.ToString("N2");
                    txtTara.Text = registro.Tara.ToString("N2");
                    txtPesoNeto.Text = registro.PesoNeto.ToString("N2");
                    txtPesoDisponible.Text = registro.PesoNetoDisponible.ToString("N2");

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

                    //Cargar Info Vehiculo
                    CargarVehiculoInfo(registro.VehiculoInfo);

                    //Cargar Info Muestreos
                    CargarMuestreosPorRecepcion(await _repoSalidas.ConsultarMangoRecepcionesSalidasPorRecepcionAsync(registro.RecepcionId));

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
                    //CargarDatosCalidadesActivas(registro.CalidadesActivas.OrderBy(i=>i.CalidadId).ToList(), dgvCalidades);

                    //Cargar Datos Promedio
                   //CargarDatosCalidadesActivas(_repoMuestreos.ConsultarPromedioCalidadesRecepcion(_IdRegistroSeleccionado), dgvCalidadesPromedio);


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
                //CargarDatosCalidadesActivas(_repoCalidades.ListarCalidadesActivasSinValorPorRecepcionId(0), dgvCalidades);
                //CargarDatosCalidadesActivas(_repoMuestreos.ConsultarPromedioCalidadesRecepcion(0), dgvCalidadesPromedio);

            }
        }

        private void GuardarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
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
            cbFrutas.Enabled = false;
            cbVariedades.Enabled = false;
            cbProveedores.Enabled = false;
            cbLocalidades.Enabled = false;
            cbPredios.Enabled = false;
            cbPropietarios.Enabled = false;
            dtpFecha.Enabled = false;

            txtRecepcion.Enabled = false;

            txtPlacas.Enabled = false;
            txtColor.Enabled = false;
            txtConductor.Enabled = false;
            txtMarcaModelo.Enabled = false;

            tsbDelGrid.Visible = false;

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
                _IdSalidaSeleccionado = Convert.ToInt32(dgvSalidas.Rows[e.RowIndex].Cells["mango_recepcion_salida_id"].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarMuestreo(_IdSalidaSeleccionado);
            }
            else if (e.RowIndex >= 0 &&
                _columnName == "Editar")
            {
                MessageBox.Show("No tiene permiso para editar registros.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarMuestreosPorRecepcion(DataTable _muestreos)
        {
            dgvSalidas.DataSource = null;
            dgvSalidas.DataSource = _muestreos;
            dgvSalidas.ReadOnly = true;
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Recepcion Salida Id", "mango_recepcion_salida_id", 50, false);
            DataGridViewHelper.AddColumn(dgv, "Recepcion Id", "mango_recepcion_id", 50, false);
            DataGridViewHelper.AddColumn(dgv, "Consecutivo", "consecutivo", 50);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha", "fecha", 150);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Traspaleo", "peso_salida", 50);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Pendiente", "saldo_restante", 150);
            DataGridViewHelper.AddColumn(dgv, "Observaciones", "observaciones", 100);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 50);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

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


            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoBruto);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtTara);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoDisponible);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoNeto);


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


        #endregion

        private void dgvMuestreos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvSalidas.Columns[e.ColumnIndex].Name);
        }

        private void dgvMuestreos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

    }
}
