using FresamLogistica.Controllers.General;
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

namespace FresamLogistica.Views.General
{
    public partial class RecepcionesView : Form
    {
        private readonly RecepcionesController _repo;
        private readonly CiclosController _repoCiclos = new CiclosController();

        private int _IdRegistroSeleccionado;
        private int _IdCicloSeleccionado;

        const string _columnaSeleccionada = "RecepcionId";
        public RecepcionesView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repo = new RecepcionesController();

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

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.ListarPorCiclo(_IdCicloSeleccionado, dtpFecha.Value));
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

        private void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repo.ListarPorCiclo(_IdCicloSeleccionado, dtpFecha.Value);

            OrdernarPorFechaInicio();

        }

        private void ActualizarRegistro(int _IdRegistro, int _IdCiclo)
        {
            Helper.FormulariosHelper.CargarFormulario(new RecepcionView(_IdRegistro, _IdCiclo), this);
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
            DataGridViewHelper.AddColumn(dgv, "Folio recepcion diaria", "FolioRecepcionDiaria", 100);
            DataGridViewHelper.AddColumn(dgv, "Ciclo Id", "CicloId", 150, false);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha", "Fecha", 150);
            DataGridViewHelper.AddColumn(dgv, "Código Id", "AgriculorId", 0, true);
            DataGridViewHelper.AddColumn(dgv, "Agricultor", "AgricultorNombre", 150);
            DataGridViewHelper.AddColumn(dgv, "Rancho Id", "RanchoId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Rancho", "RanchoNombre", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Sector Id", "SectorId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Sector", "SectorNombre", 150, false);
            DataGridViewHelper.AddColumn(dgv, "Fruta Id", "FrutaId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Fruta", "FrutaNombre", 150);
            DataGridViewHelper.AddColumn(dgv, "Variedad Id", "VariedadId", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Variedad", "VariedadNombre", 150);
            DataGridViewHelper.AddColumn(dgv, "Placas Vehículo", "Placas", 150);
            DataGridViewHelper.AddColumn(dgv, "No. Tarimas", "NoTarimas", 100);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Peso Neto", "PesoNeto", 100);
            DataGridViewHelper.AddColumnMoneda(dgv, "Importe", "importe", 100);
            DataGridViewHelper.AddColumn(dgv, "Grado", "GradoId", 0);
            DataGridViewHelper.AddColumn(dgv, "Folio", "Folio", 0, false);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

        private void OrdernarPorFechaInicio()
        {
            dgvRegistros.Tag = "ASC";
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, 2, _repo.ListarPorCiclo(_IdCicloSeleccionado, dtpFecha.Value));
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
