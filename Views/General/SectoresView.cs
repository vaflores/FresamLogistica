using FresamLogistica.Controllers.Configuraciones;
using FresamLogistica.Controllers.General;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.General.Agricultores;
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
    public partial class SectoresView : Form
    {
        private readonly SectoresController _repo;
        private readonly AgricultoresController _repoAgricultores = new AgricultoresController();
        private readonly RanchosController _repoRanchos = new RanchosController();
        private readonly CiclosController _repoCiclos = new CiclosController();

        private int _IdAgricultorSeleccionado;
        private int _IdRanchoSeleccionado;
        private int _IdCicloSeleccionado;
        private const string _columnaSeleccionada = "sector_id";

        public SectoresView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new SectoresController();

            ConfigurarComboBoxRanchos();
            ConfigurarComboBoxAgricultores();
            ConfigurarComboBoxCiclos();

            ConfigurarDataGridView(dgvRegistros);

            CargarDatosRegistroSeleccionado();

            ValidarPermisos();
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
            else if (e.Control && e.KeyCode == Keys.P && tsbImprimir.Enabled) // Ctrl + P → Imprimir
            {
                ImprimirRegistro();
            }

        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.ListarPorRanchoCiclo(_IdRanchoSeleccionado, _IdCicloSeleccionado));
        }

        private void tsbSyncGrid_Click(object sender, EventArgs e)
        {
            CargarRegistrosGrid();
        }

        private void tsbAddGrid_Click(object sender, EventArgs e)
        {
            ActualizarRegistro(0, 0, 0);
        }

        private void cbAgricultor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdAgricultorSeleccionado = Convert.ToInt32(cbAgricultor.SelectedValue);
                ConfigurarComboBoxRanchos();
                CargarRegistrosGrid();
            }
            catch
            {

            }
        }

        private void cbRancho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdRanchoSeleccionado = Convert.ToInt32(cbRancho.SelectedValue);
                CargarRegistrosGrid();
            }
            catch
            {

            }
        }

        private void cbCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdCicloSeleccionado = Convert.ToInt32(cbCiclo.SelectedValue);
                CargarRegistrosGrid();
            }
            catch
            {

            }
        }
        #endregion


        #region MetodosPrivados
        private void ValidarPermisos()
        {
            // Aquí se deben implementar las validaciones de permisos según el usuario actual.
            // Por ahora, habilitamos todos los botones.
            tsbCrear.Enabled = false;
            tsbAddGrid.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Guardar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
        }

        private void ConfigurarComboBoxAgricultores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbAgricultor, _repoAgricultores.ListarComboBox());
            cbAgricultor.SelectedIndex = -1;
        }

        private void ConfigurarComboBoxRanchos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbRancho, _repoRanchos.ListarPorAgricultorComboBox(_IdAgricultorSeleccionado));
            cbRancho.SelectedIndex = -1;
        }

        private void ConfigurarComboBoxCiclos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbCiclo, _repoCiclos.ListarCombo());
            cbCiclo.SelectedIndex = -1;
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (_IdRanchoSeleccionado > 0 && _IdCicloSeleccionado > 0)
            {
                var registro = _repo.ListarPorRanchoCiclo(_IdRanchoSeleccionado, _IdCicloSeleccionado);
                if (registro != null)
                {

                    CargarRegistrosGrid();
                }
                else
                {
                    MessageBox.Show("No se encontrarón sectores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void GuardarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void BorrarCampos()
        {
            cbAgricultor.SelectedIndex = 0;
            cbRancho.SelectedIndex = 0;
            cbCiclo.SelectedIndex = 0;

            dgvRegistros.DataSource = null;
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ActualizarRegistro(int _idSector, int _idFruta, int _idVariedad)
        {
            Helper.FormulariosHelper.CargarFormulario(new SectorView(_idSector, _IdCicloSeleccionado, _IdRanchoSeleccionado, _idFruta, _idVariedad), this);
            CargarRegistrosGrid();
        }

        private void CargarRegistrosGrid()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repo.ListarPorRanchoCiclo(_IdRanchoSeleccionado, _IdCicloSeleccionado);
        }


        private void ConfigurarDataGridView(DataGridView dgv)
        {
            DataGridViewHelper.SetDataGridViewProperties(dgv);

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Sector Id", _columnaSeleccionada, 100);
            DataGridViewHelper.AddColumn(dgv, "Nombre", "nombre", 150);
            DataGridViewHelper.AddColumn(dgv, "Superficie", "superficie", 100);
            DataGridViewHelper.AddColumn(dgv, "Fruta Id", "fruta_id", 100, false);
            DataGridViewHelper.AddColumn(dgv, "Fruta", "nombre_fruta", 100);
            DataGridViewHelper.AddColumn(dgv, "Variedad Id", "variedad_id", 100, false);
            DataGridViewHelper.AddColumn(dgv, "Variedad", "nombre_variedad", 100);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);
            DataGridViewHelper.AddColumn(dgv, "Rancho Id", "rancho_id", 100, false);
            DataGridViewHelper.AddColumn(dgv, "Ciclo Id", "ciclo_id", 100, false);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);
        }

        #endregion

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, "Editar");
        }

        private void AbrirRegistroSeleccionado(DataGridViewCellEventArgs e, string nombreColumna)
        {
            // Verifica que sea la columna del botón y una fila válida
            if (e.RowIndex >= 0 &&
                nombreColumna == "Editar" &&
                SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar")
                )
            {
                // Obtén el objeto actual (puede ser por ID)
                var idSector = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);
                var idFruta = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[3].Value);
                var idVariedad = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[5].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(idSector, idFruta, idVariedad);
            }
            else if (e.RowIndex >= 0 &&
                nombreColumna == "Editar")
            {
                MessageBox.Show("No tiene permiso para editar registros.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
