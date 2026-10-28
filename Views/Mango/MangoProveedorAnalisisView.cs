using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.Herramientas;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels.Herramientas;
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

namespace FresamLogistica.Views.Mango
{
    public partial class MangoProveedorAnalisisView : Form
    {
        private readonly MangoProveedoresAnalisisCampoController _repo;
        private readonly MangoProveedorAnalisisCampoDetalleController _repoAnalisisDetalle = new MangoProveedorAnalisisCampoDetalleController();
        private readonly MangoProveedorAnalisisCampoDocumentoController _repoAnalisisDocumento = new MangoProveedorAnalisisCampoDocumentoController();

        private readonly MangoProveedoresController _repoProveedores = new MangoProveedoresController();
        private readonly CondicionesCampoController _repoCondicionesCampo = new CondicionesCampoController();
        private int _IdRegistroSeleccionado;
        private int _proveedorId;

        public MangoProveedorAnalisisView(int IdRegistro, int proveedorId)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this._IdRegistroSeleccionado = IdRegistro;
            this._proveedorId = proveedorId;

            _repo = new MangoProveedoresAnalisisCampoController();

            ConfigurarComboBoxEstado();
            ConfigurarComboBoxProveedores();

            ConfigurarDataGridViewCondiciones();
            ConfigurarDataGridViewDocumentos();

            CargarDatosRegistroSeleccionado();

            ValidarPermisos();
            ValidarPuedeEditarDetalle();
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

        private void UnidadMedidaView_KeyDown(object sender, KeyEventArgs e)
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
        private void ValidarPuedeEditarDetalle()
        {
            // Implementar la lógica para validar si se pueden editar las detalle
            if (_IdRegistroSeleccionado > 0)
            {
                tcOpciones.Enabled = true;
            }
            else
            {
                tcOpciones.Enabled = false;
            }

            //Si el estado es I, deshabilitar la edición de detalle
            if (cbEstado.SelectedValue != null && cbEstado.SelectedValue.ToString() == "I")
            {
                tcOpciones.Enabled = false;
            }
        }

        private void ConfigurarComboBoxEstado()
        {
            EstadosHelper.GenerarEstadosActivoInactivo(ref cbEstado);
        }

        private void ConfigurarComboBoxProveedores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbProveedores, _repoProveedores.ListarComboBox());
        }

        private void CargarDatosRegistroSeleccionado()
        {
            cbProveedores.SelectedValue = _proveedorId.ToString();
            if (_IdRegistroSeleccionado > 0)
            {
                var registro = _repo.ObtenerPorId(_IdRegistroSeleccionado);
                if (registro != null)
                {
                    _IdRegistroSeleccionado = registro.proveedor_analisis_campo_id;
                    _proveedorId = registro.proveedor_id;
                    dtpFecha.Value = registro.fecha_evaluacion;
                    cbEstado.SelectedValue = registro.estado;
                    txtObservaciones.Text = registro.observaciones;

                    dgvCondiciones.DataSource = _repoAnalisisDetalle.ListarPorIdCondiciones(_IdRegistroSeleccionado);
                    dgvDocumentos.DataSource = _repoAnalisisDocumento.ObtenerPorAnalisisCampoId(_IdRegistroSeleccionado);
                }
                else
                {
                    MessageBox.Show("No se encontró el análisis.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            try
            {

                //validar solo un registro en la misma fecha para el mismo proveedor
                if (_IdRegistroSeleccionado == 0)
                {
                    var registroExistente = _repo.ObtenerPorProveedorYFecha(_proveedorId, dtpFecha.Value);
                    if (registroExistente != null)
                    {
                        MessageBox.Show("Ya existe un análisis para este proveedor en la misma fecha.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                var registro = new Model.ProveedoresAnalisisCampo
                {
                    proveedor_analisis_campo_id = _IdRegistroSeleccionado,
                    proveedor_id = _proveedorId,
                    fecha_evaluacion = dtpFecha.Value,
                    observaciones = txtObservaciones.Text.Trim(),
                    estado = cbEstado.SelectedValue.ToString()
                };

                //Generar Lista de Condiciones desde el DataGridView
                List<CondicionesAnalisisCampoViewModel> condiciones = new List<CondicionesAnalisisCampoViewModel>();

                foreach (DataGridViewRow row in dgvCondiciones.Rows)
                {
                    if (row.IsNewRow) continue;
                    var condicion = new CondicionesAnalisisCampoViewModel
                    {
                        analisis_campo_detalle_id = Convert.ToInt32(row.Cells["analisis_campo_detalle_id"].Value),
                        analisis_campo_id = Convert.ToInt32(row.Cells["analisis_campo_id"].Value),
                        tipo_analisis_campo_id = Convert.ToInt32(row.Cells["tipo_analisis_campo_id"].Value),
                        codigo_analisis = row.Cells["codigo_analisis"].Value?.ToString(),
                        analisis_campo = row.Cells["analisis_campo"].Value?.ToString(),
                        cumple = Convert.ToBoolean(row.Cells["cumple"].Value)
                    };
                    condiciones.Add(condicion);
                }


                if (_IdRegistroSeleccionado == 0)
                {
                    _IdRegistroSeleccionado = _repo.CrearAnalisis(registro);
                    ListarCondiciones();
                    MessageBox.Show("Análisis creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.Actualizar(registro);
                    _repoAnalisisDetalle.ActualizarListaCondiciones(condiciones);
                    ListarCondiciones();
                    MessageBox.Show("Análisis actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ValidarPuedeEditarDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el registro. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            _IdRegistroSeleccionado = 0;

            txtObservaciones.Clear();
            dtpFecha.Value = DateTime.Now;
            cbEstado.SelectedIndex = 0;

            ValidarPuedeEditarDetalle();
            dgvCondiciones.DataSource = null;
            dgvDocumentos.DataSource = null;
        }

        private void ListarCondiciones()
        {
            if (_IdRegistroSeleccionado > 0)
            {
                dgvCondiciones.DataSource = _repoAnalisisDetalle.ListarPorIdCondiciones(_IdRegistroSeleccionado);
            }
            else
            {
                dgvCondiciones.DataSource = null;
            }
        }

        private void EliminarRegistro()
        {
            try
            {

                if (_IdRegistroSeleccionado > 0)
                {
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar el análisis?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        _repo.Eliminar(_IdRegistroSeleccionado);
                        MessageBox.Show("Análisis eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningún análisis seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ConfigurarDataGridViewCondiciones()
        {
            DataGridViewHelper.SetDataGridViewProperties(dgvCondiciones);

            dgvCondiciones.ReadOnly = false;
            dgvCondiciones.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgvCondiciones.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgvCondiciones, "Detalle Id", "analisis_campo_detalle_id", 150, false);
            DataGridViewHelper.AddColumn(dgvCondiciones, "Análisis Id", "analisis_campo_id", 150, false);
            DataGridViewHelper.AddColumn(dgvCondiciones, "Tipo Análisis Id", "tipo_analisis_campo_id", 150, false);
            DataGridViewHelper.AddColumn(dgvCondiciones, "Código", "codigo_analisis", 150);
            DataGridViewHelper.AddColumn(dgvCondiciones, "Análisis de Campo", "analisis_campo", 150);
            DataGridViewHelper.AddColumnCheckBox(dgvCondiciones, "Cumple", "cumple", 250, true, false);

            dgvCondiciones.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvCondiciones.IsCurrentCellDirty)
                    dgvCondiciones.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }


        private void ConfigurarDataGridViewDocumentos()
        {
            DataGridViewHelper.SetDataGridViewProperties(dgvDocumentos);

            dgvDocumentos.ReadOnly = true;
            dgvDocumentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDocumentos.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgvDocumentos, "Detalle Id", "proveedor_analisis_campo_documento_id", 150, false);
            DataGridViewHelper.AddColumn(dgvDocumentos, "Análisis Id", "proveedor_analisis_campo_id", 150, false);
            DataGridViewHelper.AddColumn(dgvDocumentos, "Nombre del Archivo", "nombre_archivo", 150);
            DataGridViewHelper.AddColumn(dgvDocumentos, "Tamaño", "tamano_bytes", 150,false);
            DataGridViewHelper.AddColumn(dgvDocumentos, "Fecha de carga", "fecha_crea", 150);

            dgvDocumentos.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvDocumentos.IsCurrentCellDirty)
                    dgvDocumentos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }
        #endregion

        private void tsbCargar_Click(object sender, EventArgs e)
        {
            // Implementar la lógica para subir archivo a la base de datos
            if (_IdRegistroSeleccionado > 0)
            {
                string nombreArchivo;
                byte[] contenido;
                contenido = HelperPdf.ConvertirABytes(this, out nombreArchivo);

                if (contenido != null && contenido.Length > 0)
                {
                    ProveedoresAnalisisCampoDocumentos _registro = new ProveedoresAnalisisCampoDocumentos();
                    _registro.proveedor_analisis_campo_id = _IdRegistroSeleccionado;
                    _registro.nombre_archivo = nombreArchivo;
                    _registro.contenido = contenido;
                    _registro.tamano_bytes = contenido.Length;
                    _repoAnalisisDocumento.Crear(_registro);
                    MessageBox.Show("Documento cargado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDocumentos.DataSource = _repoAnalisisDocumento.ObtenerPorAnalisisCampoId(_IdRegistroSeleccionado);
                }
                else
                {
                    MessageBox.Show("No se seleccionó ningún archivo o el archivo está vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay ningún análisis seleccionado para cargar documentos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            //Eliminar documento seleccionado
            if (dgvDocumentos.SelectedRows.Count > 0)
            {
                int documentoId = Convert.ToInt32(dgvDocumentos.SelectedRows[0].Cells["proveedor_analisis_campo_documento_id"].Value);
                var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar el documento seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    _repoAnalisisDocumento.Eliminar(documentoId);
                    MessageBox.Show("Documento eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDocumentos.DataSource = _repoAnalisisDocumento.ObtenerPorAnalisisCampoId(_IdRegistroSeleccionado);
                }
            }
        }

        private void tsbVisualizar_Click(object sender, EventArgs e)
        {
            //Visualizar documento seleccionado
            if (dgvDocumentos.SelectedRows.Count > 0)
            {
                int documentoId = Convert.ToInt32(dgvDocumentos.SelectedRows[0].Cells["proveedor_analisis_campo_documento_id"].Value);
                var documento = _repoAnalisisDocumento.ObtenerContenidoPorIdAnalisisCampo(documentoId);
                if (documento != null && documento.contenido != null && documento.contenido.Length > 0)
                {
                    HelperPdf.Visualizar(documento.contenido, documento.nombre_archivo);
                }
                else
                {
                    MessageBox.Show("El documento seleccionado no tiene contenido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsbDescargar_Click(object sender, EventArgs e)
        {
            //Descargar documento seleccionado
            if (dgvDocumentos.SelectedRows.Count > 0)
            {
                int documentoId = Convert.ToInt32(dgvDocumentos.SelectedRows[0].Cells["proveedor_analisis_campo_documento_id"].Value);
                var documento = _repoAnalisisDocumento.ObtenerContenidoPorIdAnalisisCampo(documentoId);
                if (documento != null && documento.contenido != null && documento.contenido.Length > 0)
                {
                    HelperPdf.Descargar(documento.contenido, documento.nombre_archivo);
                }
                else
                {
                    MessageBox.Show("El documento seleccionado no tiene contenido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
