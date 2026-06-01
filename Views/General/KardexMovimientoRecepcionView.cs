using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
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
    public partial class KardexMovimientoRecepcionView : Form
    {
        private readonly KardexCajasController _repo;
        private readonly KardexCondicionesCajasController _repoCondiciones = new KardexCondicionesCajasController();

        private int _agricultorId;
        private bool _tipoMovimiento;

        public KardexMovimientoRecepcionView(int idAgricultor, bool tipoMovimiento)
        {
            InitializeComponent();

            _repo = new KardexCajasController();

            this.KeyPreview = true;
            this._agricultorId = idAgricultor;
            this._tipoMovimiento = tipoMovimiento;

            txtTipoMovimiento.Text = _tipoMovimiento ? "Recepción" : "Entrega";
            txtCantidad.Enabled = false;

            Helper.TextBoxValidarEnteros.Attach(txtCantidad);

            this.Tag = 36;
            ValidarPermisos();
            ConfigurarDataGridView(dgvCondicionesCajas);
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
            Debug.WriteLine("Función no implementada.");
        }

        private void GuardarRegistro()
        {
            try
            {
                SumarValorCondiciones();
                decimal saldoActual = _repo.ObtenerSaldoActualCajas(_agricultorId);

                if(Convert.ToDecimal(txtCantidad.Text) > saldoActual )
                {
                    MessageBox.Show("No se puede guardar el movimiento. La cantidad ingresada excede el saldo actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToDecimal(txtCantidad.Text) > 0)
                {
                    int movimientoId = _repo.AgregarMovimientoKardex(_agricultorId, dtpFecha.Value, Convert.ToInt16(txtCantidad.Text), _tipoMovimiento, txtObservaciones.Text);

                    var CondicionesCajas = new List<CondicionesCajasViewModel>();
                    foreach (DataGridViewRow row in dgvCondicionesCajas.Rows)
                    {
                        if (row.Cells["CondicionCajaId"].Value != null && row.Cells["Valor"].Value != null)
                        {
                            CondicionesCajas.Add(new CondicionesCajasViewModel
                            {
                                KardexId = movimientoId,
                                CondicionCajaId = Convert.ToInt32(row.Cells["CondicionCajaId"].Value),
                                Valor = Convert.ToInt16(row.Cells["Valor"].Value)
                            });
                        }
                    }

                    _repoCondiciones.AlmacenarCondicionesCajas(movimientoId, CondicionesCajas);

                    MessageBox.Show("Movimiento creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else { 
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
            txtTipoMovimiento.Text = _tipoMovimiento ? "Recepción" : "Entrega";
            txtCantidad.Text = "0";
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
            var condiciones = _repoCondiciones.ListarCondicionesCajasActivas();
            dgvCondicionesCajas.DataSource = condiciones;
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "KardexCondicionCajaId", "KardexCondicionCajaId", 75, false);
            DataGridViewHelper.AddColumn(dgv, "KardexId", "KardexId", 100, false);
            DataGridViewHelper.AddColumnFecha(dgv, "CondicionID", "CondicionCajaId", 100, false);
            DataGridViewHelper.AddColumn(dgv, "Condición", "Nombre", 200,true,true);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Valor", "Valor", 100,true, false);

        }

        private void SumarValorCondiciones()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCondicionesCajas.Rows)
            {
                if (row.Cells["Valor"].Value != null && decimal.TryParse(row.Cells["Valor"].Value.ToString(), out decimal valor))
                {
                    total += valor;
                }
            }
            // Aquí puedes mostrar el total en un TextBox o Label si lo deseas
            txtCantidad.Text = total.ToString("N0");
        }
        #endregion

        private void dgvCondicionesCajas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Columna 1: N0
                if (e.ColumnIndex == 4)
                {
                    if (decimal.TryParse(dgvCondicionesCajas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out decimal valor))
                    {
                        dgvCondicionesCajas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor;
                    }
                    else
                    {
                        dgvCondicionesCajas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                    }
                }
                SumarValorCondiciones();
            }
            catch { }
        }
    }
}
