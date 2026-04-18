using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.Model;
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
    public partial class LotesFoliosView : Form
    {
        private readonly LoteFoliosController _repo;
        private readonly DestinosResiduosController _repoDestinos = new DestinosResiduosController();

        private int _destinoResiduosSeleccionado;

        const string _columnaSeleccionada = "lote_folio_id";
        public LotesFoliosView()
        {
            InitializeComponent();
            this.KeyPreview = true;
            _repo = new LoteFoliosController();

            Helper.EstadosHelper.GenerarEstadosDesdeLista(ref cbDestinoResiduos, _repoDestinos.ListarComboBoxDestinosResiduosRequierenFolio());

            cbDestinoResiduos.SelectedIndex = -1;
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
            DataGridViewHelper.SortDataGridViewByColumn(dgvRegistros, e.ColumnIndex, _repo.Listar());
        }

        private void dgvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirRegistroSeleccionado(e, dgvRegistros.Columns[e.ColumnIndex].Name);
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
            dgvRegistros.DataSource = _repo.ListarPorDestinoResiduos(this._destinoResiduosSeleccionado);
        }

        private void ActualizarRegistro(int _IdRegistro)
        {
            Helper.FormulariosHelper.CargarFormulario(new LoteFoliosView(_IdRegistro), this);
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
            DataGridViewHelper.AddColumn(dgv, "Lote Id", _columnaSeleccionada, 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Fecha de Compra", "fecha_compra", 100);
            DataGridViewHelper.AddColumnFecha(dgv, "Vigencia", "fecha_vigencia", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Folio Inicial", "folio_inicio", 100);
            DataGridViewHelper.AddColumnNumeroSinDecimales(dgv, "Folio Final", "folio_fin", 100);
            DataGridViewHelper.AddColumnMoneda(dgv, "Costo", "importe", 100);
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

        private void cbDestinoResiduos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbDestinoResiduos.SelectedValue != null)
                {
                    this._destinoResiduosSeleccionado = Convert.ToInt32(cbDestinoResiduos.SelectedValue);
                }
            }
            catch 
            {
            }

            CargarRegistros();
        }
    }
}
