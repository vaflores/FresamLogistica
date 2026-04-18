using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Residuos;
using FresamLogistica.Helper;
using FresamLogistica.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Views.Residuos
{
    public partial class RecoleccionResiduosFolioView : Form
    {
        private DestinosResiduosController _repoDestinosResiduos = new DestinosResiduosController();
        private LoteFoliosController _repoLoteFolios = new LoteFoliosController();
        private FoliosController _repoFolios = new FoliosController();

        public int DestinoResiduoIdSeleccionado { get; set; }
        public List<FoliosRegistroResiduoRecolectadoViewModel> Folios { get; set; }

        public RecoleccionResiduosFolioView()
        {
            InitializeComponent();

            this.KeyPreview = true;

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

        private void FormView_KeyDown(object sender, KeyEventArgs e)
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

        #endregion


        #region MetodosPrivados

        private void ConfigurarComboBoxDestinos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbDestinos, _repoDestinosResiduos.ListarComboBoxDestinosResiduosRequierenFolioActivosYSeleccionado(DestinoResiduoIdSeleccionado));
        }

        private void ConfigurarComboBoxFolios()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFolios, _repoFolios.ListarComboBoxFoliosVigentesPorDestinoYSeleccionados(DestinoResiduoIdSeleccionado,Folios.Select(f=>f.FolioId).ToList()));
        }

        private void CargarDatosRegistroSeleccionado()
        {
            cbDestinos.SelectedValue = DestinoResiduoIdSeleccionado.ToString();
        }

        private void GuardarRegistro()
        {
            try
            {

                FoliosRegistroResiduoRecolectadoViewModel folio = new FoliosRegistroResiduoRecolectadoViewModel();
                folio.FolioId = Convert.ToInt32(cbFolios.SelectedValue.ToString());
                folio.numero = Convert.ToInt32(cbFolios.Text);

                if(folio.FolioId <= 0)
                {
                    MessageBox.Show("Debe seleccionar un folio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(Folios.Contains(folio))
                {
                    MessageBox.Show("El folio seleccionado ya ha sido agregado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Folios.Add(folio);

                MessageBox.Show("Detalle guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el registro. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BorrarCampos()
        {
            cbDestinos.SelectedValue = -1;
            cbFolios.SelectedValue = -1;
        }

        private void EliminarRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirRegistro()
        {
            Debug.WriteLine("Función no implementada.");
        }


        #endregion

        private void RecoleccionResiduosDetalleView_Load(object sender, EventArgs e)
        {
            ConfigurarComboBoxDestinos();
            ConfigurarComboBoxFolios();
            CargarDatosRegistroSeleccionado();
        }

        private void cbFruta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ConfigurarComboBoxFolios();
            }
            catch { }
        }
    }
}
