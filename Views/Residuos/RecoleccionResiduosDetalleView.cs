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
    public partial class RecoleccionResiduosDetalleView : Form
    {
        private readonly FrutasController _repoFrutas = new FrutasController();
        private readonly TiposResiduosController _repoTipoResiduos = new TiposResiduosController();

        public DetalleRegistroResiduoRecolectadoViewModel Detalle { get; set; }

        public RecoleccionResiduosDetalleView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            Helper.TextBoxValidarNumerosDecimales.Attach(txtCantidad);
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

        private void ConfigurarComboBoxFrutas()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbFruta, _repoFrutas.ListarComboBox(Detalle.FrutaId));
        }

        private void ConfigurarComboBoxTiposResiduos()
        {
            EstadosHelper.GenerarEstadosDesdeLista(ref cbTipoResiduo, _repoTipoResiduos.ListarComboBoxPorFruta(Detalle.FrutaId, Detalle.TipoResiduoId));
        }

        private void CargarDatosRegistroSeleccionado()
        {
            if (Detalle.DetalleRegistroId != 0)
            {
                cbFruta.SelectedValue = Detalle.FrutaId.ToString();
                cbTipoResiduo.SelectedValue = Detalle.TipoResiduoId.ToString();
                txtCantidad.Text = Detalle.Cantidad.ToString("N2");
            }
            else
            {
                BorrarCampos();
            }
        }

        private void GuardarRegistro()
        {
            try
            {
                Detalle.FrutaId = Convert.ToInt32(cbFruta.SelectedValue);
                Detalle.NombreFruta = cbFruta.Text;
                Detalle.TipoResiduoId = Convert.ToInt32(cbTipoResiduo.SelectedValue);
                Detalle.NombreTipoResiduo = cbTipoResiduo.Text;
                Detalle.Cantidad = Convert.ToDecimal(txtCantidad.Text);

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
            cbFruta.SelectedValue = -1;
            cbTipoResiduo.SelectedValue = -1;
            txtCantidad.Text = "0.0";
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
            ConfigurarComboBoxFrutas();
            ConfigurarComboBoxTiposResiduos();
            CargarDatosRegistroSeleccionado();
        }

        private void cbFruta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Detalle.FrutaId = Convert.ToInt32(cbFruta.SelectedValue); 
                EstadosHelper.GenerarEstadosDesdeLista(ref cbTipoResiduo, _repoTipoResiduos.ListarComboBoxPorFruta(Detalle.FrutaId, Detalle.TipoResiduoId));
            }
            catch { }
        }
    }
}
