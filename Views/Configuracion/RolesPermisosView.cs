using FresamLogistica.Controllers.Configuracion;
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

namespace FresamLogistica.Views.Configuracion
{
    public partial class RolesPermisosView : Form
    {
        private readonly RolesPermisosController _repo;
        private readonly RolesController _repoRoles = new RolesController();
        private string _IdRolSeleccionado;

        public RolesPermisosView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new RolesPermisosController();


            ConfigurarDataGridView(dgvRegistros);
            ConfigurarComboBoxRoles();
            CargarRegistros();
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, 0, ListarPermisos());
        }

        #region EventosFormulario
        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
        }

        private void tsbCrear_Click(object sender, EventArgs e)
        {
            ActualizarRegistro("");
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
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, ListarPermisos());
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                ActualizarRegistro("");
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
        private void ConfigurarComboBoxRoles()
        {
            var roles = _repoRoles.ListarComboBox();
            cbRoles.DataSource = roles;
            cbRoles.DisplayMember = "Value";
            cbRoles.ValueMember = "Key";
            cbRoles.SelectedIndex = -1;
        }

        private void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = ListarPermisos();
        }

        private List<RolesPermisosViewModel> ListarPermisos()
        {
            List<RolesPermisosViewModel> lista = _repo.ListarPermisos();
            List<RolesPermisosViewModel> listaAsignados = _repo.ListarPermisosAsignadosPorRol(_IdRolSeleccionado);

            foreach (RolesPermisosViewModel permiso in lista)
            {
                if (listaAsignados.Any(pa => pa.PermisoId == permiso.PermisoId))
                {
                    permiso.Asignado = true;
                }
            }

            return lista;
        }

        private void ActualizarRegistro(string _IdRegistro)
        {
            Helper.FormulariosHelper.CargarFormulario(new UsuarioView(_IdRegistro), this);
            CargarRegistros();
        }

        private void GuardarRegistro()
        {
            try { 
                if (cbRoles.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un rol para asignar los permisos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach(DataGridViewRow _row in dgvRegistros.Rows)
                {
                    RolesPermisosViewModel permiso = (RolesPermisosViewModel)_row.DataBoundItem;
                    RolesPermisos registroExistente = _repo.ObtenerPorPermisoIdYRolId(permiso.PermisoId, _IdRolSeleccionado);
                    if (permiso.Asignado)
                    {
                        if (registroExistente == null)
                        {
                            RolesPermisos nuevoRegistro = new RolesPermisos
                            {
                                id_rol = _IdRolSeleccionado,
                                id_permiso = permiso.PermisoId
                            };
                            _repo.Crear(nuevoRegistro);
                        }
                    }
                    else
                    {
                        if (registroExistente != null)
                        {
                            _repo.EliminarPorRolYPermiso(registroExistente.id_rol, registroExistente.id_permiso);
                        }
                    }
                }

                MessageBox.Show("Permisos asignados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los permisos. \nVerifique los datos ingresados. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Permiso Id", "PermisoId", 100, false, true);
            DataGridViewHelper.AddColumn(dgv, "Pantalla Id", "PantallaId", 100, false, true);
            DataGridViewHelper.AddColumn(dgv, "Pantalla", "NombrePantalla", 300, true, true);
            DataGridViewHelper.AddColumn(dgv, "Permiso", "NombrePermiso", 300, true, true);
            DataGridViewHelper.AddColumnCheckBox(dgv, "Asignado", "Asignado", 100, true, false);

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        #endregion

        private void cbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                _IdRolSeleccionado = cbRoles.SelectedValue.ToString();
                CargarRegistros();
            }
            catch (Exception)
            {
                _IdRolSeleccionado = "";
            }
        }
    }
}
