using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using FresamLogistica.ViewModels.Mango;
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
    public partial class MangoRecepcionSalidaView : Form
    {
        private readonly MangoRecepcionesSalidasDetalleController _repo;
        private readonly MangoRecepcionesSalidasController _repoSalida = new MangoRecepcionesSalidasController();

        private int _recepcionId;
        private int _recepcionSalidaId;
        private decimal _pesoDisponible;
        
        public MangoRecepcionSalidaView(int recepcionId, int _recepcionSalida, decimal _pesoNetoDisponible)
        {
            InitializeComponent();

            _repo = new MangoRecepcionesSalidasDetalleController();

            this.KeyPreview = true;
            this._recepcionId = recepcionId;
            this._recepcionSalidaId = _recepcionSalida;
            this._pesoDisponible = _pesoNetoDisponible;

            txtPesoDisponible.Text = _pesoDisponible.ToString("N2");
            txtCantidad.Enabled = false;
            dtpFecha.Enabled = true;

            Helper.TextBoxValidarEnteros.Attach(txtCantidad);

            this.Tag = 40;
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
                decimal saldoActual = _pesoDisponible;

                if(Convert.ToDecimal(txtCantidad.Text) > saldoActual )
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
                    foreach (DataGridViewRow row in dgvCondicionesCajas.Rows)
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

                    if(_recepcionSalidaId == 0)
                    {
                        _repo.Crear(_salida, salidasRecepcion);
                    }
                    else
                    {
                        _repo.Actualizar(_salida, salidasRecepcion);
                    }

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
            txtPesoDisponible.Text = _pesoDisponible.ToString("N2");
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
            
            if(_recepcionSalidaId == 0)
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
                    _pesoDisponible = _pesoDisponible + registro.peso_salida;
                    txtPesoDisponible.Text = _pesoDisponible.ToString("N2");
                    txtObservaciones.Text = registro.observaciones;
                }
            }

            var condiciones = _repo.ListarTiposSalida(this._recepcionSalidaId);
            dgvCondicionesCajas.DataSource = condiciones;

        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "MangoRecepcionSalidaDetalleId", "MangoRecepcionSalidaDetalleId", 75, false);
            DataGridViewHelper.AddColumn(dgv, "MangoRecepcionSalidaId ", "MangoRecepcionSalidaId ", 100, false);
            DataGridViewHelper.AddColumn(dgv, "MangoTipoSalidaId", "MangoTipoSalidaId", 100, false);
            DataGridViewHelper.AddColumnFecha(dgv, "TipoSalida", "TipoSalida", 100, true);
            DataGridViewHelper.AddColumn(dgv, "Observaciones", "Observaciones", 200,false);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Peso", "Peso", 100,true, false);

        }

        private void SumarValorCondiciones()
        {
            dgvCondicionesCajas.EndEdit(); // Asegura que se guarden los cambios en la celda actual antes de sumar
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCondicionesCajas.Rows)
            {
                if (row.Cells["Peso"].Value != null && decimal.TryParse(row.Cells["Peso"].Value.ToString(), out decimal valor))
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
