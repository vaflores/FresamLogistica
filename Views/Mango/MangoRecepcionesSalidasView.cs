using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.Productos.Frutas;
using Microsoft.Reporting.WinForms;
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

namespace FresamLogistica.Views.Mango
{
    public partial class MangoRecepcionesSalidasView : Form
    {
        private readonly MangoRecepcionesSalidasController _repo;

        private int _IdRegistroSeleccionado;

        const string _columnaSeleccionada = "mango_recepcion_id";
        public MangoRecepcionesSalidasView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repo = new MangoRecepcionesSalidasController();

            _IdRegistroSeleccionado = 0;

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();

            this.Tag = "40"; // Asignar un ID de pantalla único para permisos (ajustar según corresponda)

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
            dgvRegistros.DataSource = await _repo.ConsultarMangoRecepcionesSalidasPorFecha(dtpFecha.Value);

            //OrdernarPorFechaInicio();

        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            if(_IdRegistro == 0)
            {
                return;
            }

            Helper.FormulariosHelper.CargarFormulario(new MangoRecepcionSalidasView(_IdRegistro), this);
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
            DataGridViewHelper.AddColumn(dgv, "Recepcion Id", "mango_recepcion_id", 50);
            DataGridViewHelper.AddColumn(dgv, "Folio Ciclo", "folio_ciclo", 50);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha", "fecha", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Código Id", "codigo", 50);
            DataGridViewHelper.AddColumn(dgv, "Distribuidor", "proveedor", 150);
            DataGridViewHelper.AddColumn(dgv, "Variedad", "nombre_variedad", 100);
            DataGridViewHelper.AddColumn(dgv, "Grado", "grado_id", 50);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Neto", "peso_neto", 120);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Procesado", "procesado", 120);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Disponible", "peso_neto_disponible", 120);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "% Procesado", "porcentaje_procesado", 70);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

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
