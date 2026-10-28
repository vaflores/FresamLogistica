using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Controllers.Mango;
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

namespace FresamLogistica.Views.Mango
{
    public partial class MangoProveedoresAnalisisView : Form
    {
        private readonly MangoProveedoresAnalisisCampoController _repo;
        private readonly MangoProveedoresController _repoProveedores = new MangoProveedoresController();
        const string _columnaSeleccionada = "proveedor_analisis_campo_id";

        private int _proveedorId = 1;

        public MangoProveedoresAnalisisView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new MangoProveedoresAnalisisCampoController();

            ConfigurarComboBoxProveedores();
            cbProveedores.SelectedIndex = -1;

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();
            
            if(_proveedorId != 0)
                DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, 0, _repo.Listar());

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

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(_proveedorId == 0) return; 
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.ListarPorProveedorId(_proveedorId));
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

        private void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            if (_proveedorId == 0) return;
            dgvRegistros.DataSource = _repo.ListarPorProveedorId(_proveedorId);
        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            Helper.FormulariosHelper.CargarFormulario(new MangoProveedorAnalisisView(_IdRegistro, _proveedorId), this);
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
            DataGridViewHelper.AddColumn(dgv, "Id", _columnaSeleccionada, 100);
            DataGridViewHelper.AddColumn(dgv, "Proveedor Id", "proveedor_id", 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha de evaluación", "fecha_evaluacion", 200);
            DataGridViewHelper.AddColumn(dgv, "Observaciones", "observaciones", 300);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 50);

        }

        private void ConfigurarComboBoxProveedores()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbProveedores, _repoProveedores.ListarComboBox());
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
                var id = Convert.ToInt32(dgvRegistros.Rows[e.RowIndex].Cells[0].Value);

                // Lógica de edición (por ejemplo, abrir otro formulario)
                ActualizarRegistro(id);
            }
            else if (e.RowIndex >= 0 &&
                nombreColumna == "Editar")
            {
                MessageBox.Show("No tiene permiso para editar registros.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _proveedorId = Convert.ToInt32(cbProveedores.SelectedValue);
            }
            catch (Exception ex)
            {
                _proveedorId = 0; 
                cbProveedores.SelectedIndex = -1;
            }
            CargarRegistros();
        }
    }
}
