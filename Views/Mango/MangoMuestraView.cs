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
    public partial class MangoMuestraView : Form
    {
        private readonly MangoMuestreosController _repo;
        private readonly MangoCalidadesController _repoCalidades = new MangoCalidadesController();
        private readonly EmpleadosController _repoEmpleados = new EmpleadosController();

        private int _idMuestreoSeleccionado;
        private int _idRecepcionSeleccionada;
        private int _idResponsable;
        private int _idVerifica;

        private bool _existeMangoChico;

        private List<MangoCalidadesRecepcionesViewModel> _listCalidadesInfo;
        private List<MangoCalidadesRecepcionesViewModel> _listCalidadesActivas;


        public MangoMuestraView(int _idMuestreo, int _idRecepcion, int _frutaId, int _responsableId, int _verificaId)
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new MangoMuestreosController();

            ConfigurarDataGridViewCalidades(dgvCalidades);

            ConfigurarTextBoxNumericos();
            ConfigurarComboBoxFirmas();

            this._idMuestreoSeleccionado = _idMuestreo;
            this._idRecepcionSeleccionada = _idRecepcion;
            this._idResponsable = _responsableId;
            this._idVerifica = _verificaId;

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

        private void dgvCalidades_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // Ejemplo: columna 3
                if (e.ColumnIndex == 3)
                {
                    if (!decimal.TryParse(e.FormattedValue.ToString(), out _))
                    {
                        MessageBox.Show("Solo se permiten números con decimales.");
                        //e.Cancel = true; // evita que pierda el foco
                        dgvCalidades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                        e.Cancel = false; // no marcar error
                    }
                    //SumarPorcentajeDescuento();
                    CalcularPorcentajes();
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

                    //SumarPorcentajeDescuento();
                    CalcularPorcentajes();
                }
            }
            catch { }
        }

        private void txtPesoCaja_TextChanged(object sender, EventArgs e)
        {
            //CalcularPorcentaje();
            CalcularPorcentajes();
        }

        private void txtDesecho_TextChanged(object sender, EventArgs e)
        {
            CalcularPorcentajes();
            //CalcularPorcentaje();
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

        private void CargarDatosRegistroSeleccionado()
        {
            if (_idMuestreoSeleccionado > 0)
            {
                //Cargar info recepción
                var registro = _repo.ObtenerPorId(_idMuestreoSeleccionado);
                if (registro != null)
                {
                    dtpFecha.Enabled = false;
                    dtpFecha.Value = registro.fecha;
                    txtPesoCaja.Text = registro.peso_caja.ToString();
                    txtDesecho.Text = registro.desecho.ToString();
                    txtPorcentaje.Text = registro.porcentaje.ToString();
                    txtObservaciones.Text = registro.observaciones;
                    chkAceptado.Checked = registro.aceptado;
                    chkMateria.Checked = registro.materia;
                    txtPorcentajeMaduro.Text = registro.porcentaje_maduro.ToString();

                    //Cargar Info Calidades

                    _listCalidadesInfo = _repoCalidades.ListarCalidadesPorMuestreoId(_idMuestreoSeleccionado);
                    _listCalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorMuestreoId(_idMuestreoSeleccionado);

                    CargarDatosCalidadesRecepcion(_listCalidadesInfo);
                    CargarDatosCalidadesActivas(_listCalidadesActivas);

                    cbElabora.SelectedValue = this._idResponsable.ToString();
                    cbVerifica.SelectedValue = this._idVerifica.ToString();

                    VerificarExisteCalidadMangoChico(_listCalidadesInfo, _listCalidadesActivas);

                    HabilitarPorcentajeChicoManual();

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
                txtPorcentajeMaduro.Text = "0.00";

                //Cargar Info Calidades
                _listCalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorMuestreoId(_idMuestreoSeleccionado);
                CargarDatosCalidadesActivas(_listCalidadesActivas);

                CalcularPorcentaje();

                cbElabora.SelectedValue = this._idResponsable.ToString();
                cbVerifica.SelectedValue = this._idVerifica.ToString();

            }
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

                //Cargar info recepción
                var registro = _repo.ObtenerPorId(_idMuestreoSeleccionado);

                if (registro == null)
                    registro = new MangoMuestreos();

                registro.mango_recepcion_id = _idRecepcionSeleccionada;
                registro.fecha = dtpFecha.Value;
                registro.peso_caja = decimal.Parse(txtPesoCaja.Text);
                registro.desecho = decimal.Parse(txtDesecho.Text);
                registro.porcentaje = decimal.Parse(txtPorcentaje.Text);
                registro.observaciones = txtObservaciones.Text;
                registro.aceptado = chkAceptado.Checked;
                registro.materia = chkMateria.Checked;
                registro.porcentaje_maduro = decimal.Parse(txtPorcentajeMaduro.Text);
                registro.responsable_empleado_id = int.Parse(cbElabora.SelectedValue.ToString());
                registro.verifica_empleado_id = int.Parse(cbVerifica.SelectedValue.ToString());

                //Cargar Info Calidades
                List<MangoMuestreosCalidades> calidadesMuestreo = new List<MangoMuestreosCalidades>();
                foreach (DataGridViewRow row in dgvCalidades.Rows)
                {
                    MangoMuestreosCalidades calidad = new MangoMuestreosCalidades()
                    {
                        detalle_id = row.Cells["CalidadRecepcionId"].Value != null ? Convert.ToInt32(row.Cells["CalidadRecepcionId"].Value) : 0,
                        muestreo_id = _idMuestreoSeleccionado,
                        calidad_id = row.Cells["CalidadId"].Value != null ? Convert.ToInt32(row.Cells["CalidadId"].Value) : 0,
                        valor = row.Cells["Valor"].Value != null ? Convert.ToDecimal(row.Cells["Valor"].Value) : 0
                    };
                    calidadesMuestreo.Add(calidad);
                }

                if (_idMuestreoSeleccionado == 0)
                {
                    _idMuestreoSeleccionado = _repo.CrearMuestreoConCalidades(registro, calidadesMuestreo);
                    MessageBox.Show("Muestra creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repo.ActualizarMuestreoConCalidades(registro, calidadesMuestreo);
                    MessageBox.Show("Muestra actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _repo.ActualizarPromedioDescuentosRecepcion(_idRecepcionSeleccionada);
                _repo.ActualizarPromedioCalidadesRecepcion(_idRecepcionSeleccionada);

                this.Close();
                //CargarDatosRegistroSeleccionado();

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
                var registro = _repo.ObtenerPorId(_idMuestreoSeleccionado);

                if (registro == null)
                    return;

                _repo.EliminarMuestreoConCalidades(_idMuestreoSeleccionado);

                _repo.ActualizarPromedioDescuentosRecepcion(_idRecepcionSeleccionada);
                _repo.ActualizarPromedioCalidadesRecepcion(_idRecepcionSeleccionada);

                this.Close();

                MessageBox.Show("Muestra eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la muestra. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void ConfigurarDataGridViewCalidades(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.ReadOnly = false;
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
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPesoCaja);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtDesecho);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentaje);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentajeMaduro);
            Helper.TextBoxValidarNumerosDecimales.Attach(txtPorcentajeMangoChico);

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

        //private void SumarPorcentajeDescuento()
        //{
        //    decimal total = 0;
        //    foreach (DataGridViewRow row in dgvCalidades.Rows)
        //    {
        //        total += row.Cells["Valor"].Value != null ? Convert.ToDecimal(row.Cells["Valor"].Value) : 0;
        //    }

        //    txtDesecho.Text = total.ToString("N2");
        //}

        private void VerificarExisteCalidadMangoChico(List<MangoCalidadesRecepcionesViewModel> _calidadesMuestreos, List<MangoCalidadesRecepcionesViewModel> _calidadesSinValor)
        {
            _existeMangoChico = _calidadesMuestreos.Where(c => c.UsoPorcentajeMangoChico == true).Any();

            if (!_existeMangoChico) {
                _existeMangoChico = _calidadesSinValor.Where(c => c.UsoPorcentajeMangoChico == true).Any();
            }

        }

        private void HabilitarPorcentajeChicoManual()
        {
            txtPorcentajeMangoChico.Enabled = !_existeMangoChico;
        }

        private void CalcularPorcentajes()
        {
            try { 
            decimal porcentaje = 0;
            decimal pesoCaja = Convert.ToDecimal(txtPesoCaja.Text);

            foreach (DataGridViewRow row in dgvCalidades.Rows)
            {
                if(!Convert.ToBoolean(row.Cells["AfectaCalificacion"].Value))
                    continue;

                if(Convert.ToBoolean(row.Cells["UsoPorcentajeMangoChico"].Value))
                    porcentaje += (Convert.ToDecimal(row.Cells["Valor"].Value)/2);
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
        
        #endregion

    }
}
