using FresamLogistica.Controllers.Residuos;
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

namespace FresamLogistica.Views.Residuos
{
    public partial class RecoleccionesResiduosView : Form
    {
        private readonly RegistroResiduosRecolectadosController _repo;

        private int _IdRegistroSeleccionado;
        private int _IdCicloSeleccionado;

        const string _columnaSeleccionada = "RegistroResiduoId";
        public RecoleccionesResiduosView()
        {
            _repo = new RegistroResiduosRecolectadosController();
            InitializeComponent();
            this.KeyPreview = true;

            _IdRegistroSeleccionado = 0;

            ConfigurarDataGridView(dgvRegistros);
            CargarRegistros();

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

        private void dgvRegistros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.ConsultarRegistrosResiduosRecolectadosPorFecha(dtpFecha.Value));
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


        private void CargarRegistros()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = _repo.ConsultarRegistrosResiduosRecolectadosPorFecha(dtpFecha.Value);

        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            Helper.FormulariosHelper.CargarFormulario(new RecoleccionResiduosView(_IdRegistro), this);
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

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgv.Columns.Clear();

            //Crear columnas
            DataGridViewHelper.AddColumn(dgv, "Recolección Id", "RegistroResiduoId", 70);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha", "Fecha", 100);
            DataGridViewHelper.AddColumn(dgv, "Empresa", "EmpresaResiduo", 150);
            DataGridViewHelper.AddColumn(dgv, "Operador", "Operador", 150);
            DataGridViewHelper.AddColumn(dgv, "Destino", "Destino", 150);
            DataGridViewHelper.AddColumn(dgv, "Folio Id", "FolioId", 70);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Cantidad Total de Residuos", "CantidadTotal", 100);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Producción Total del Día", "ProduccionTotal", 100, false);
            DataGridViewHelper.AddColumnNumeroConDecimales(dgv, "Porcentaje de Merma", "PorcentajeMerma", 100, false);
            DataGridViewHelper.AddColumn(dgv, "Observaciones", "Observaciones", 200);
            DataGridViewHelper.AddColumn(dgv, "Estado", "estado", 100);

            DataGridViewHelper.AddColumnEditar(dgv, "Editar", "Editar", 70);

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
            if ( tsbCrear.Enabled) // Ctrl +  → Nuevo
            {
                ActualizarRegistro(0);
            }
        }
    }
}
