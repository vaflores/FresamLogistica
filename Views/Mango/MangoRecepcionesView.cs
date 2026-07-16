using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.Productos.Frutas;
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
    public partial class MangoRecepcionesView : Form
    {
        private readonly MangoRecepcionesController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();

        private int _IdRegistroSeleccionado;
        private int _IdCicloSeleccionado;

        const string _columnaSeleccionada = "MangoRecepcionId";
        public MangoRecepcionesView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repo = new MangoRecepcionesController();

            _IdRegistroSeleccionado = 0;

            ConfigurarComboBoxCiclos();

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();

            ValidarPermisos();
        }


        #region EventosFormulario
        private void RecepcionesView_Load(object sender, EventArgs e)
        {
            try
            {
                cbCiclos.SelectedIndex = this._IdCicloSeleccionado;
            }
            catch (Exception)
            {
                // Ignorar errores al cargar el ciclo seleccionado
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
            ActualizarRegistro(0, Convert.ToInt32(cbCiclos.SelectedValue));
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
                ActualizarRegistro(0, Convert.ToInt32(cbCiclos.SelectedValue));
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

        private async void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var registros = await _repo.ListarPorCiclo(_IdCicloSeleccionado, dtpFecha.Value);
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, registros);
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

        private void cbCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdCicloSeleccionado = Convert.ToInt32(cbCiclos.SelectedValue);

                CargarRegistros();
            }
            catch (Exception)
            {
                _IdCicloSeleccionado = 0;
            }
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

        private void ConfigurarComboBoxCiclos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbCiclos, _repoCiclos.ListarComboCiclosActivosOrdenDescendete());
        }

        private async void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            var registros = await _repo.ListarPorCiclo(_IdCicloSeleccionado, dtpFecha.Value);
            dgvRegistros.DataSource = registros;

            OrdernarPorFechaInicio();

        }

        private void ActualizarRegistro(int _IdRegistro, int _IdCiclo)
        {
            Helper.FormulariosHelper.CargarFormulario(new MangoRecepcionView(_IdRegistro, _IdCiclo), this);
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
            DataGridViewHelper.AddColumn(dgv, "Recepcion Id", "RecepcionId", 100);
            DataGridViewHelper.AddColumn(dgv, "Ciclo Id", "CicloId", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Folio recepcion", "FolioCiclo", 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha", "Fecha", 150);
            DataGridViewHelper.AddColumn(dgv, "Código Id", "ProveedorId", 0, true);
            DataGridViewHelper.AddColumn(dgv, "Proveedor", "ProveedorNombre", 150);
            DataGridViewHelper.AddColumn(dgv, "Predio Id", "PredioId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Predio", "PredioNombre", 150, true);
            DataGridViewHelper.AddColumn(dgv, "Propietario Id", "PropietarioId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Propietario", "PropietarioNombre", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Localidad Id", "LocalidadId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Localidad", "LocalidadNombre", 150, true);
            DataGridViewHelper.AddColumn(dgv, "Fruta Id", "FrutaId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Fruta", "FrutaNombre", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Variedad Id", "VariedadId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Variedad", "VariedadNombre", 150);
            DataGridViewHelper.AddColumn(dgv, "Guía Sanitaria", "GuiaSanitaria", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Ticket Bascula", "TicketBascula", 100, false);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Neto", "PesoNeto", 100);
            DataGridViewHelper.AddColumn(dgv, "Grado", "GradoId", 0);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Calidad", "CalidadPromedio", 100);
            DataGridViewHelper.AddColumn(dgv, "Placas Vehículo", "Placas", 150);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

        private async void OrdernarPorFechaInicio()
        {
            dgvRegistros.Tag = "ASC";
            var registros = await _repo.ListarPorCiclo(_IdCicloSeleccionado, dtpFecha.Value);
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, 2, registros);
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
                _IdCicloSeleccionado = Convert.ToInt32(cbCiclos.SelectedValue);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(_IdRegistroSeleccionado, _IdCicloSeleccionado);
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
            if ( tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                ActualizarRegistro(0, Convert.ToInt32(cbCiclos.SelectedValue));
            }
        }
    }
}
