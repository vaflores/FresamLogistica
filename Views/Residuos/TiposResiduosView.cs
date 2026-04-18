using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
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
    public partial class TiposResiduosView : Form
    {
        private readonly TiposResiduosController _repo;
        private readonly FrutasController _repoFrutas = new FrutasController();
        private int _IdFrutaSeleccionado;

        const string _columnaSeleccionada = "tipo_residuo_id";
        public TiposResiduosView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repo = new TiposResiduosController();

            ConfigurarComboBoxFrutas();

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();
            cbFrutas.SelectedIndex = -1;
            ValidarPermisos();
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

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.ListarPorFruta(_IdFrutaSeleccionado));
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
        }

        private void cbFrutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _IdFrutaSeleccionado = Convert.ToInt16(cbFrutas.SelectedValue.ToString());
            }
            catch { }
            CargarRegistros();
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
        private void ConfigurarComboBoxFrutas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFrutas, _repoFrutas.ListarComboBox());
        }

        private void CargarRegistros()
        {
            dgvRegistros.DataSource = _repo.ListarPorFruta(_IdFrutaSeleccionado);
        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            Helper.FormulariosHelper.CargarFormulario(new TipoResiduoView(_IdRegistro, _IdFrutaSeleccionado), this);
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
            DataGridViewHelper.AddColumn(dgv, "Tipo Residuo Id", _columnaSeleccionada, 100);
            DataGridViewHelper.AddColumn(dgv, "Nombre", "nombre", 500);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

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
                nombreColumna == "Editar")
            {
                // Obtén el objeto actual (puede ser por ID)
                var idTipoResiduo = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(idTipoResiduo);
            }
        }
    }
}
