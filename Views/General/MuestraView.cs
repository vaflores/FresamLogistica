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
    public partial class MuestraView : Form
    {
        private readonly MuestreosRecepcionesController _repo;
        private readonly GradosController _repoGrados = new GradosController();
        private readonly CalidadesController _repoCalidades = new CalidadesController();

        private int _idMuestreoSeleccionado;
        private int _idRecepcionSeleccionada;
        private int _frutaIdSeleccionada;

        private List<CalidadesRecepcionesViewModel> _listCalidadesInfo;
        private List<CalidadesRecepcionesViewModel> _listCalidadesActivas;


        public MuestraView(int _idMuestreo, int _idRecepcion, int _frutaId)
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new MuestreosRecepcionesController();

            ConfigurarDataGridViewCalidades(dgvCalidades);

            ConfigurarTextBoxNumericos();

            this._idMuestreoSeleccionado = _idMuestreo;
            this._idRecepcionSeleccionada = _idRecepcion;
            this._frutaIdSeleccionada = _frutaId;

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

                    //Cargar Info Calidades

                    _listCalidadesInfo = _repoCalidades.ListarCalidadesPorMuestreoId(_idMuestreoSeleccionado);
                    _listCalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorMuestreoId(_idMuestreoSeleccionado);

                    CargarDatosCalidadesRecepcion(_listCalidadesInfo);
                    CargarDatosCalidadesActivas(_listCalidadesActivas);


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

                //Cargar Info Calidades
                _listCalidadesActivas = _repoCalidades.ListarCalidadesActivasSinValorPorMuestreoId(_idMuestreoSeleccionado);
                CargarDatosCalidadesActivas(_listCalidadesActivas);

                CalcularPorcentaje();

            }
        }

        private void GuardarRegistro()
        {
            try
            {

                //Cargar info recepción
                var registro = _repo.ObtenerPorId(_idMuestreoSeleccionado);

                if (registro == null)
                    registro = new MuestreosRecepciones();

                registro.recepcion_id = _idRecepcionSeleccionada;
                registro.fecha = dtpFecha.Value;
                registro.peso_caja = decimal.Parse(txtPesoCaja.Text);
                registro.desecho = decimal.Parse(txtDesecho.Text);
                registro.porcentaje = decimal.Parse(txtPorcentaje.Text);
                registro.observaciones = txtObservaciones.Text;
                registro.aceptado = chkAceptado.Checked;
                registro.materia = chkMateria.Checked;
                


                //Cargar Info Calidades
                List<MuestreosCalidadesRecepciones> calidadesMuestreo = new List<MuestreosCalidadesRecepciones>();
                foreach (DataGridViewRow row in dgvCalidades.Rows)
                {
                    MuestreosCalidadesRecepciones calidad = new MuestreosCalidadesRecepciones()
                    {
                        muestreo_calidad_id = row.Cells["CalidadRecepcionId"].Value != null ? Convert.ToInt32(row.Cells["CalidadRecepcionId"].Value) : 0,
                        muestreo_id = _idMuestreoSeleccionado,
                        calidad_id = row.Cells["CalidadId"].Value != null ? Convert.ToInt32(row.Cells["CalidadId"].Value) : 0,
                        valor = row.Cells["Valor"].Value != null ? Convert.ToInt32(row.Cells["Valor"].Value) : 0
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

            //dgv.CellValueChanged += dgvTarimas_CellValueChanged;

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
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

        #endregion

    }
}
