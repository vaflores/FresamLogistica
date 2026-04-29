using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Helper;
using FresamLogistica.Views.Productos;
using FresamLogistica.Views.Residuos;
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

namespace FresamLogistica.Views.General
{
    public partial class KardexCajasView : Form
    {
        private readonly AgricultoresController _repoAgricultores = new AgricultoresController();
        private readonly KardexCajasController _repoKardexCajas;

        private int _IdRegistroSeleccionado = 0;

        public KardexCajasView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repoKardexCajas = new KardexCajasController();

            ConfigurarComboBoxAgricultores();

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, 0, _repoKardexCajas.ObtenerMovimientosKardex(_IdRegistroSeleccionado));

            //ValidarPermisos();
        }
        #region EventosFormulario
        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
            ActualizarRegistro(0);
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

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repoKardexCajas.ObtenerMovimientosKardex(_IdRegistroSeleccionado));
        }

        private void FrutasView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                ActualizarRegistro(0);
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
                CargarRegistros();
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
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Guardar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
        }

        private void ConfigurarComboBoxAgricultores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbAgricultor, _repoAgricultores.ListarComboBox());
        }

        private void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repoKardexCajas.ObtenerMovimientosKardex(_IdRegistroSeleccionado);
        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            //Helper.FormulariosHelper.CargarFormulario(new AgricultorView(_IdRegistro), this);
            CargarRegistros();
        }

        private void GuardarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }
        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Kardex Id", "kardex_id", 100, false);
            DataGridViewHelper.AddColumn(dgv, "Agricultor Id", "agricultor_id", 100, false);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha del Movimiento", "fecha", 100);
            DataGridViewHelper.AddColumn(dgv, "Tipo de Movimiento", "tipo_movimiento", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Entrada", "entrada", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Salida", "salida", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Saldo Anterior", "saldo_anterior", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Saldo Actualizado", "saldo_actual", 100);
            DataGridViewHelper.AddColumn(dgv, "Observaciones", "observaciones", 100);

        }

        #endregion

        private void cbAgricultor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdRegistroSeleccionado = Convert.ToInt32(cbAgricultor.SelectedValue);
                CargarRegistros();
            }
            catch
            {
                _IdRegistroSeleccionado = 0;
            }
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormulario(new KardexMofimientoView(_IdRegistroSeleccionado, true), this);
            CargarRegistros();
        }

        private void tsbDelGrid_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormulario(new KardexMofimientoView(_IdRegistroSeleccionado, false), this);
            CargarRegistros();
        }
    }
}
