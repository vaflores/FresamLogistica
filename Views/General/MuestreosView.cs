using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.Productos.Frutas;
using Microsoft.ReportingServices.Diagnostics.Internal;
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
    public partial class MuestreosView : Form
    {
        private readonly MuestreosRecepcionesController _repo;

        private int _IdRegistroSeleccionado;

        const string _columnaSeleccionada = "recepcion_id";
        public MuestreosView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repo = new MuestreosRecepcionesController();

            _IdRegistroSeleccionado = 0;

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();

            this.Tag = "35"; // Asignar un ID de pantalla único para permisos (ajustar según corresponda)

            ValidarPermisos();
        }


        #region EventosFormulario
        private void RecepcionesView_Load(object sender, EventArgs e)
        {
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
           Debug.WriteLine("Función no implementada.");
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

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

        #endregion

        #region MetodosPrivados

        private void ValidarPermisos()
        {
            tsbCrear.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Guardar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
        }


        private async void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = await _repo.ObteneMuestreosPorFechaAsync(dtpFecha.Value);

            //OrdernarPorFechaInicio();

        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            if(_IdRegistro == 0)
            {
                return;
            }

            Helper.FormulariosHelper.CargarFormulario(new MuestreoView(_IdRegistro), this);
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
            DataGridViewHelper.AddColumn(dgv, "Recepcion Id", "recepcion_id", 100);
            DataGridViewHelper.AddColumn(dgv, "Folio recepcion diaria", "id_folio_diario", 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha", "Fecha", 150);
            DataGridViewHelper.AddColumn(dgv, "Código Id", "AgriculorId", 0, true);
            DataGridViewHelper.AddColumn(dgv, "Agricultor", "nombre_agricultor", 150);
            DataGridViewHelper.AddColumn(dgv, "Fruta", "nombre_fruta", 150);
            DataGridViewHelper.AddColumn(dgv, "Variedad", "nombre:variedad", 150);
            DataGridViewHelper.AddColumn(dgv, "Placas Vehículo", "Placas", 150);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "No. Tarimas", "total_tarimas", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Total Cajas", "total_cajas", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Total Muestreos", "total_muestreos", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

        //private void OrdernarPorFechaInicio()
        //{
        //    dgvRegistros.Tag = "ASC";

        //    // Especificar explícitamente el tipo de la lista como DataRow
        //    DataTable dataTable = _repo.ObteneMuestreosPorFechaAsync(dtpFecha.Value).Result;
        //    List<DataRow> lista = dataTable.AsEnumerable().ToList();

        //    DataGridViewHelper.SortDataGridViewByColumn<DataRow>(dgvRegistros, 2, lista);
        //}


        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string _columnName)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                _columnName == "Editar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar"))
            {
                // Obtén el objeto actual (puede ser por ID)
                _IdRegistroSeleccionado = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(_IdRegistroSeleccionado);
            }
            else if (e.RowIndex >= 0 &&
                _columnName == "Editar")
            {
                MessageBox.Show("No tiene permiso para editar registros.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void crearNuevaRecepcionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Función no implementada.");
        }
    }
}
