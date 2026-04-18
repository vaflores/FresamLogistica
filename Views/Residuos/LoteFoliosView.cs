using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
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

namespace FresamLogistica.Views.Residuos
{
    public partial class LoteFoliosView : Form
    {
        private readonly LoteFoliosController _repo;
        private readonly FoliosController _repoFolios = new FoliosController();
        private readonly DestinosResiduosController _repoDestinosResiduos = new DestinosResiduosController();

        private int _loteFoliosIdSeleccionado;
        private bool _loteFoliosTieneFoliosUtilizados;

        public LoteFoliosView(int _idRegistro)
        {
            InitializeComponent();

            _repo = new LoteFoliosController();

            this.KeyPreview = true;
            this._loteFoliosIdSeleccionado = _idRegistro;

            _loteFoliosTieneFoliosUtilizados = _repoFolios.VerificarLoteFoliosTieneFoliosUtilizados(this._loteFoliosIdSeleccionado);

            ConfigurarComboBoxEmpresas();
            ConfigurarComboBoxEstado();

            ValidarPermisoActulizarFolios();

            Helper.TextBoxValidarEnteros.Attach(txtFolioInicio);
            Helper.TextBoxValidarEnteros.Attach(txtFolioFinal);
            Helper.TextBoxValidarMonedas.Attach(txtImporte);
            CargarDatosRegistroSeleccionado();

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();

            this.Tag = 26;
            ValidarPermisos();
        }

        #region EventosFormulario

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
                CargarDatosRegistroSeleccionado();
            }
            else if (e.Control && e.KeyCode == Keys.P && tsbImprimir.Enabled) // Ctrl + P → Imprimir
            {
                ImprimirRegistro();
            }

        }
        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.Listar());
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarRegistros();
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

        private void ConfigurarComboBoxEmpresas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbDestinoResiduos, _repoDestinosResiduos.ListarComboBoxDestinosResiduos());
        }

        private void ConfigurarComboBoxEstado()
        {
            EstadosHelper.GenerarEstadosActivoInactivo(ref cbEstado);
        }

        private void ValidarPermisoActulizarFolios()
        {       
            if (_loteFoliosTieneFoliosUtilizados)
            {
                txtFolioInicio.Enabled = false;
                txtFolioFinal.Enabled = false;
                txtImporte.Enabled = false;
            }
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (_loteFoliosIdSeleccionado != 0)
            {
                var registro = _repo.ObtenerPorId(_loteFoliosIdSeleccionado);
                if (registro != null)
                {
                    dtpFechaCompra.Value = registro.fecha_compra;
                    dtpVigencia.Value = registro.fecha_vigencia;
                    cbDestinoResiduos.SelectedValue = registro.destino_residuo_id.ToString();
                    txtFolioInicio.Text = registro.folio_inicio.ToString();
                    txtFolioFinal.Text = registro.folio_fin.ToString();
                    txtImporte.Text = registro.importe.ToString("C2");
                    cbEstado.SelectedValue = registro.estado;
                }
                else
                {
                    MessageBox.Show("No se encontró el lote de folios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string nombreColumna)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                nombreColumna == "Editar")
            {
                // Obtén el objeto actual (puede ser por ID)
                var idTipoResiduo = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                //ActualizarRegistro(idTipoResiduo);
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                ValidarCampos();
                ValidarRangoFolios();   

                var registro = new Model.LoteFolios
                {
                    lote_folio_id = _loteFoliosIdSeleccionado,
                    fecha_compra = dtpFechaCompra.Value,
                    fecha_vigencia = dtpVigencia.Value,
                    destino_residuo_id = Convert.ToInt32(cbDestinoResiduos.SelectedValue.ToString()),
                    folio_inicio = Convert.ToInt32(txtFolioInicio.Text),    
                    folio_fin = Convert.ToInt32(txtFolioFinal.Text),
                    importe = decimal.Parse(txtImporte.Text, System.Globalization.NumberStyles.Currency),
                    estado = cbEstado.SelectedValue.ToString()
                };

                if (_loteFoliosIdSeleccionado == 0)
                {
                    _repo.Crear(registro);
                    MessageBox.Show("Lote de Folios creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Actualizar(registro);
                    MessageBox.Show("Lote de Folios guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CargarRegistros();
                //this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            _loteFoliosIdSeleccionado = 0;
            dtpFechaCompra.Value = DateTime.Today;
            dtpVigencia.Value = DateTime.Today;
            cbDestinoResiduos.SelectedIndex = 0;
            txtFolioInicio.Text = string.Empty;
            txtFolioFinal.Text = string.Empty;
            txtImporte.Text = string.Empty;
            cbEstado.SelectedIndex = 0;
            CargarRegistros();
        }

        private void EliminarRegistro()
        {
            try
            {

                if (_loteFoliosIdSeleccionado != 0)
                {
                    if(_loteFoliosTieneFoliosUtilizados)
                    {
                        MessageBox.Show("No se puede eliminar este lote de folios porque tiene folios utilizados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este lote de folios?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_loteFoliosIdSeleccionado);
                        MessageBox.Show("Lote de Folios eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun lote de folios seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }
        private void CargarRegistros()
        {
            dgvRegistros.DataSource = _repoFolios.ListarFoliosPorLote(this._loteFoliosIdSeleccionado);
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Folio Id", "folio_id", 100, false);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Número", "numero", 100);
            DataGridViewHelper.AddColumnMoneda(dgv, "Costo", "costo", 100);
            DataGridViewHelper.AddColumnCheckBox(dgv, "Utilizado", "utilizado", 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha de Uso", "fecha_uso", 100);

            //DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

        private void ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtFolioInicio.Text))
            {
                txtFolioInicio.Focus();
                throw new Exception("El campo 'Folio Inicial' es obligatorio.");

            }
            if (string.IsNullOrWhiteSpace(txtFolioFinal.Text))
            {
                txtFolioFinal.Focus();
                throw new Exception("El campo 'Folio Final' es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txtImporte.Text))
            {
                txtImporte.Focus();
                throw new Exception("El campo 'Importe' es obligatorio.");
            }
        }

        private void ValidarRangoFolios()
        {
            int folioInicio = Convert.ToInt32(txtFolioInicio.Text);
            int folioFinal = Convert.ToInt32(txtFolioFinal.Text);
            if (folioInicio > folioFinal)
            {
                txtFolioInicio.Focus();
                throw new Exception("El 'Folio Inicial' no puede ser mayor que el 'Folio Final'.");
            }
        }


        #endregion

        private void cbDestinoResiduos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
