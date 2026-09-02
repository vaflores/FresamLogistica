using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using FresamLogistica.ViewModels.Mango;
using FresamLogistica.Views.Reportes.Titulos;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace FresamLogistica.Views.Mango
{
    public partial class MangoRecepcionSalidaView : Form
    {
        private readonly MangoRecepcionesSalidasDetalleController _repo;
        private readonly MangoRecepcionesSalidasController _repoSalida = new MangoRecepcionesSalidasController();

        private readonly CiclosController _repoCiclos = new CiclosController();
        private readonly TitulosMangoTarimasController _repoTitulos = new TitulosMangoTarimasController();
        private readonly EmpresaController _repoEmpresa = new EmpresaController();
        private readonly MangoProveedoresController _repoProveedores = new MangoProveedoresController();

        private int _recepcionId;
        private int _recepcionSalidaId;

        private DateTime _fechaRecepcion;
        private int _proveedor;
        private string _viaje;
        private string _variedad;
        private string _predio;

        private bool isLoad = true;

        private decimal _pesoDisponible;
        const string campoLavado = "LAVADO";

        public MangoRecepcionSalidaView(int recepcionId, int _recepcionSalida, decimal _pesoNetoDisponible,
            DateTime _fechaRecepcion,
            int _proveedor,
            string _viaje,
            string _variedad,
            string _predio)
        {
            InitializeComponent();

            _repo = new MangoRecepcionesSalidasDetalleController();

            this.KeyPreview = true;
            this._recepcionId = recepcionId;
            this._recepcionSalidaId = _recepcionSalida;
            this._pesoDisponible = _pesoNetoDisponible;

            this._fechaRecepcion = _fechaRecepcion;
            this._proveedor = _proveedor;
            this._viaje = _viaje;
            this._variedad = _variedad;
            this._predio = _predio;

            txtPesoDisponible.Text = _pesoDisponible.ToString("N2");
            txtCantidad.Enabled = false;
            dtpFecha.Enabled = true;
            tsbSyncGrid.Visible = false;

            Helper.TextBoxValidarEnteros.Attach(txtCantidad);

            this.Tag = 40;
            ValidarPermisos();
            ConfigurarDataGridView(dgvCondiciones);
            ConfigurarTarimasDataGridView(dgvTarimas);
            CargarRegistros();
        }

        #region EventosFormulario

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            BuscarRegistro();
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
            ImprimirRegistro();
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
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
                BuscarRegistro();
            }
            else if (e.Control && e.KeyCode == Keys.P && tsbImprimir.Enabled) // Ctrl + P → Imprimir
            {
                ImprimirRegistro();
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
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");

        }

        private void BuscarRegistro()
        {
            CargarRegistros();
        }

        private void GuardarRegistro()
        {
            try
            {
                SumarValorCondiciones();
                decimal saldoActual = Convert.ToDecimal(txtPesoDisponible.Text);

                if (Convert.ToDecimal(txtCantidad.Text) > saldoActual)
                {
                    MessageBox.Show("No se puede guardar el movimiento. La cantidad ingresada excede el saldo actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    MessageBox.Show("No se puede guardar el movimiento. El traspaleo no puede ser cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToDecimal(txtCantidad.Text) > 0)
                {

                    MangoRecepcionesSalidas _salida = new MangoRecepcionesSalidas
                    {
                        mango_recepcion_salida_id = _recepcionSalidaId,
                        mango_recepcion_id = _recepcionId,
                        fecha = dtpFecha.Value,
                        peso_salida = Convert.ToDecimal(txtCantidad.Text),
                        saldo_restante = saldoActual - Convert.ToDecimal(txtCantidad.Text),
                        observaciones = txtObservaciones.Text,
                        estado = "A"
                    };

                    var salidasRecepcion = new List<MangoRecepcionSalidaDetalleViewModel>();
                    foreach (DataGridViewRow row in dgvCondiciones.Rows)
                    {
                        if (row.Cells["MangoTipoSalidaId"].Value != null && row.Cells["Peso"].Value != null)
                        {
                            salidasRecepcion.Add(new MangoRecepcionSalidaDetalleViewModel
                            {
                                //KardexId = movimientoId,
                                MangoTipoSalidaId = Convert.ToInt32(row.Cells["MangoTipoSalidaId"].Value),
                                Peso = Convert.ToInt16(row.Cells["Peso"].Value)
                            });
                        }
                    }

                    //tarimas desde grid
                    var salidasTarimas = new List<MangoRecepcionesSalidasTarimas>();
                    foreach (DataGridViewRow row in dgvTarimas.Rows)
                    {
                        if (row.Cells["numero_tarima"].Value != null && row.Cells["cantidad_cajas"].Value != null && row.Cells["peso_neto"].Value != null)
                        {
                            //Validar que la cantidad de cajas es entre 1 y 56
                            int cantidadCajas = Convert.ToInt32(row.Cells["cantidad_cajas"].Value);
                            if(cantidadCajas < 1 || cantidadCajas > 56)
                            {
                                MessageBox.Show("La cantidad de cajas debe ser entre 1 y 56.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            salidasTarimas.Add(new MangoRecepcionesSalidasTarimas
                            {
                                mango_recepcion_salida_tarima_id = Convert.ToInt32(row.Cells["mango_recepcion_salida_tarima_id"].Value),
                                mango_recepcion_salida_id = Convert.ToInt32(row.Cells["mango_recepcion_salida_id"].Value),
                                numero_tarima = Convert.ToInt32(row.Cells["numero_tarima"].Value),
                                cantidad_cajas = Convert.ToDecimal(row.Cells["cantidad_cajas"].Value),
                                peso_neto = Convert.ToDecimal(row.Cells["peso_neto"].Value),
                                aprobada = Convert.ToBoolean(row.Cells["aprobada"].Value),
                                cancelada = Convert.ToBoolean(row.Cells["cancelada"].Value)
                            });
                        }
                    }

                    if (_recepcionSalidaId == 0)
                    {
                        this._recepcionSalidaId = _repo.Crear(_salida, salidasRecepcion, salidasTarimas);
                    }
                    else
                    {
                        _repo.Actualizar(_salida, salidasRecepcion, salidasTarimas);
                    }

                    MessageBox.Show("Movimiento creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    CargarRegistros();
                }
                else
                {
                    MessageBox.Show("La cantidad debe ser mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            dtpFecha.Value = DateTime.Today;
            if (isLoad)
            {
                txtPesoDisponible.Text = (_pesoDisponible).ToString("N2");
                isLoad = false;
            }
            txtCantidad.Text = "0.00";
            txtObservaciones.Text = string.Empty;
            dgvTarimas.Rows.Clear();
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }
        private void CargarRegistros()
        {

            if (_recepcionSalidaId == 0)
            {
                BorrarCampos();

            }
            else
            {
                var registro = _repoSalida.ObtenerPorId(_recepcionSalidaId);
                if (registro != null)
                {
                    dtpFecha.Value = registro.fecha;
                    txtCantidad.Text = registro.peso_salida.ToString("N2");
                    if(isLoad)
                    {
                        txtPesoDisponible.Text = (_pesoDisponible + registro.peso_salida).ToString("N2");
                        isLoad = false;
                    }
                    txtObservaciones.Text = registro.observaciones;
                }
            }

            var condiciones = _repo.ListarTiposSalida(this._recepcionSalidaId);
            dgvCondiciones.DataSource = condiciones;

            var tarimas = _repo.ListarTarimas(this._recepcionSalidaId);
            //Agregar las tarimas al DataGridView con el fomarto adecuado

            dgvTarimas.Rows.Clear();

            foreach(var tarima in tarimas)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dgvTarimas);
                newRow.Cells[0].Value = tarima.mango_recepcion_salida_tarima_id; // MangoRecepcionSalidaTarimaId
                newRow.Cells[1].Value = tarima.mango_recepcion_salida_id; // MangoRecepcionSalidaId
                newRow.Cells[2].Value = tarima.numero_tarima; // Número de Tarima
                newRow.Cells[3].Value = tarima.cantidad_cajas; // Cantidad de Cajas
                newRow.Cells[4].Value = tarima.peso_neto; // Peso Neto
                newRow.Cells[5].Value = Convert.ToBoolean(tarima.aprobada); // Aprobada
                newRow.Cells[6].Value = Convert.ToBoolean(tarima.cancelada); // Cancelada

                if (Convert.ToBoolean(tarima.aprobada) || Convert.ToBoolean(tarima.cancelada))
                    newRow.ReadOnly = true;

                dgvTarimas.Rows.Add(newRow);
            }

        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "MangoRecepcionSalidaDetalleId", "MangoRecepcionSalidaDetalleId", 75, false);
            DataGridViewHelper.AddColumn(dgv, "MangoRecepcionSalidaId ", "MangoRecepcionSalidaId", 100, false);
            DataGridViewHelper.AddColumn(dgv, "MangoTipoSalidaId", "MangoTipoSalidaId", 100, false);
            DataGridViewHelper.AddColumn(dgv, "Tipo de Salida", "TipoSalida", 100, true);
            DataGridViewHelper.AddColumn(dgv, "Observaciones", "Observaciones", 200, false);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Peso", "Peso", 100, true, false);

        }

        private void ConfigurarTarimasDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "MangoRecepcionSalidaTarimaId", "mango_recepcion_salida_tarima_id", 75, false);
            DataGridViewHelper.AddColumn(dgv, "MangoRecepcionSalidaId ", "mango_recepcion_salida_id", 100, false);
            DataGridViewHelper.AddColumn(dgv, "Número de Tarima", "numero_tarima", 100, true);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Cantidad de Cajas", "cantidad_cajas", 100, true, false);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Peso Neto", "peso_neto", 100, true);
            DataGridViewHelper.AddColumnCheckBox(dgv, "Aprobada", "aprobada", 100, true);
            DataGridViewHelper.AddColumnCheckBox(dgv, "Cancelada", "cancelada", 100, true);

            DataGridViewHelper.AddButtonColumn(dgv, "Imprimir Etiqueta", "Imprimir", 75);

        }

        private void SumarValorCondiciones()
        {
            AsignarPesoNetoAMangoLavadoEnGridCondiciones();
            dgvCondiciones.EndEdit(); // Asegura que se guarden los cambios en la celda actual antes de sumar
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCondiciones.Rows)
            {
                if (row.Cells["Peso"].Value != null && decimal.TryParse(row.Cells["Peso"].Value.ToString(), out decimal valor))
                {
                    total += valor;
                }
            }
            // Aquí puedes mostrar el total en un TextBox o Label si lo deseas
            txtCantidad.Text = total.ToString("N2");
        }
        #endregion

        private void dgvCondiciones_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Columna 1: N0
                if (e.ColumnIndex == 5)
                {
                    if (decimal.TryParse(dgvCondiciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out decimal valor))
                    {
                        dgvCondiciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor;
                    }
                    else
                    {
                        dgvCondiciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                    }
                }
                SumarValorCondiciones();
            }
            catch { }
        }

        private decimal SumarPesoNetoTarimas()
        {
            dgvTarimas.EndEdit(); // Asegura que se guarden los cambios en la celda actual antes de sumar
            decimal total = 0;
            foreach (DataGridViewRow row in dgvTarimas.Rows)
            {
                if (row.Cells["peso_neto"].Value != null 
                    && decimal.TryParse(row.Cells["peso_neto"].Value.ToString(), out decimal valor)
                    && row.Cells["cantidad_cajas"].Value != null
                    && Convert.ToBoolean(row.Cells["cancelada"].Value) == false)
                {
                    valor = Convert.ToDecimal(row.Cells["cantidad_cajas"].Value) * 15;
                    row.Cells["peso_neto"].Value = valor;
                    total += valor;
                }
            }
            // Aquí puedes mostrar el total en un TextBox o Label si lo deseas

            return total;
        }

        private void AsignarPesoNetoAMangoLavadoEnGridCondiciones()
        {
            decimal pesoNetoTotal = SumarPesoNetoTarimas();
            if (pesoNetoTotal <= 0)
            {
                throw new Exception("El peso neto total de mango lavado debe de ser mayor a cero.");
            }
            foreach (DataGridViewRow row in dgvCondiciones.Rows)
            {
                if (row.Cells["TipoSalida"].Value.ToString() == campoLavado)
                {
                    // Aquí puedes asignar el peso neto total a la columna correspondiente en el DataGridView  
                    row.Cells["Peso"].Value = pesoNetoTotal;
                    return;
                }
            }
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            //GuardarRegistro();
            // Agregar una nueva fila al DataGridView
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dgvTarimas);
            newRow.Cells[0].Value = 0; // MangoRecepcionSalidaTarimaId
            newRow.Cells[1].Value = _recepcionSalidaId; // MangoRecepcionSalidaId
            newRow.Cells[2].Value = 0 ; // Número de Tarima
            newRow.Cells[3].Value = 56; // Cantidad de Cajas
            newRow.Cells[4].Value = (56 * 15); // Peso Neto
            newRow.Cells[5].Value = false; // Aprobada
            newRow.Cells[6].Value = false; // Cancelada

            dgvTarimas.Rows.Add(newRow);

            SumarValorCondiciones();
        }

        private void tsbDelGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTarimas.CurrentRow == null)
                {
                    MessageBox.Show("No hay registros para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //validar que una tarima aprobada no se pueda eliminar
                if (dgvTarimas.CurrentRow.Cells["aprobada"].Value != null && Convert.ToBoolean(dgvTarimas.CurrentRow.Cells["aprobada"].Value))
                {
                    MessageBox.Show("No se puede eliminar una tarima aprobada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //validar tarima con numeracion diferente a cero
                if (Convert.ToInt16(dgvTarimas.CurrentRow.Cells["numero_tarima"].Value) != 0)
                {
                    dgvTarimas.CurrentRow.Cells["cancelada"].Value = true;
                    SumarValorCondiciones();
                    return;
                }
                dgvTarimas.Rows.RemoveAt(dgvTarimas.CurrentRow.Index);
                SumarValorCondiciones();
            }
            catch
            {
                MessageBox.Show("No hay registros para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCondiciones_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // Ejemplo: columna 5
                if (e.ColumnIndex == 5)
                {
                    if (!decimal.TryParse(e.FormattedValue.ToString(), out _))
                    {
                        MessageBox.Show("Solo se permiten números.");
                        dgvCondiciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                        e.Cancel = true; // no marcar error
                    }
                }
            }
            catch { }
        }

        private void dgvTarimas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 3)
                {
                    if (decimal.TryParse(dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out decimal valor))
                    {
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor.ToString("N0");
                    }
                    else
                    {
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                    }
                    SumarValorCondiciones();
                }
            }
            catch { }
        }

        private void dgvTarimas_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // Ejemplo: columna 3
                if (e.ColumnIndex == 3)
                {
                    decimal valor;
                    if (!decimal.TryParse(e.FormattedValue.ToString(), out valor))
                    {
                        MessageBox.Show("Solo se permiten números.");
                        //e.Cancel = true; // evita que pierda el foco
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "1";
                        e.Cancel = true; // no marcar error
                    }
                    if (valor > 56)
                    {
                        MessageBox.Show("Solo se permite hasta 56 cajas por tarima.");
                        //e.Cancel = true; // evita que pierda el foco
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "56";
                        e.Cancel = true; // no marcar error
                    }
                    if (valor < 1)
                    {
                        MessageBox.Show("Solo se permite un mínimo de 1 caja por tarima.");
                        //e.Cancel = true; // evita que pierda el foco
                        dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "1";
                        e.Cancel = true; // no marcar error
                    }
                    int value = Convert.ToInt32(e.FormattedValue);
                    dgvTarimas.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value = (value*15).ToString();
                }
            }
            catch { }
        }


        private void dgvTarimas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ImprimirRegistroSeleccionado(e, dgvTarimas.Columns[e.ColumnIndex].Name);
        }

        private void ImprimirRegistroSeleccionado(DataGridViewCellEventArgs e, string _columnName)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                _columnName == "Imprimir" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir"))
            {
                // Obtén el objeto actual (puede ser por ID)
                int tarima = Convert.ToInt32(dgvTarimas.Rows[e.RowIndex].Cells["mango_recepcion_salida_tarima_id"].Value);

                string numTarima =dgvTarimas.Rows[e.RowIndex].Cells["numero_tarima"].Value.ToString();
                string cantCajas = Convert.ToInt16(dgvTarimas.Rows[e.RowIndex].Cells["cantidad_cajas"].Value).ToString();
                bool cancelada = Convert.ToBoolean(dgvTarimas.Rows[e.RowIndex].Cells["cancelada"].Value);

                if (numTarima == "0")
                {
                    MessageBox.Show("No se puede imprimir una tarima con número 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(cancelada)
                {
                    MessageBox.Show("No se puede imprimir una tarima cancelada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lógica de edición (por ejemplo, abrir otro formulario)
                try {

                    var _ciclos = _repoCiclos.ObtenerCicloPorFecha(dtpFecha.Value);
                    Ciclos _ciclo = _repoCiclos.ObtenerPorCicloId(_ciclos);
                    var _titulos = _repoTitulos.ObtenerPorCicloId(_ciclos);
                    var _empresa = _repoEmpresa.ObtenerPorId(1);
                    byte[] _logoByte = _empresa.logo;

                    var proveedor = _repoProveedores.ObtenerPorId(_proveedor);

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

                        new ReportParameter("numeroTarima", numTarima.ToString()),
                        new ReportParameter("fechaRecepcion", _fechaRecepcion.ToShortDateString()),
                        new ReportParameter("fechaTraspaleo", dtpFecha.Value.ToShortDateString()),
                        new ReportParameter("distribuidor", proveedor.nombre.ToString()),
                        new ReportParameter("codigo", proveedor.codigo.ToString()),
                        new ReportParameter("viaje", _viaje.ToString()),
                        new ReportParameter("cantidadCajas", cantCajas.ToString()),
                        new ReportParameter("variedad", _variedad.ToString()),
                        new ReportParameter("predio", _predio.ToString())

                    };

                    ReportHelper reporte = new ReportHelper("Reports/Mango/MangoTarimas/rptMangoTarimas.rdlc", parametros);

                    reporte.ShowDialog();
                }
                catch { }
            }
        }

    }
 }
