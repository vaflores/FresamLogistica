using FresamLogistica.Controllers.General;
using FresamLogistica.Controllers.Mango;
using FresamLogistica.Controllers.Productos;
using FresamLogistica.Controllers.Reportes;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views.Productos.Frutas;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Diagnostics.Internal;
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

namespace FresamLogistica.Views.Helpers
{
    public partial class SeleccionarHoraView : Form
    {
        public DateTime HoraSeleccionada
        {
            get
            {
                return dtpHora.Value;
            }
        }

        public SeleccionarHoraView()
        {
            InitializeComponent();

            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            dtpHora.Format = DateTimePickerFormat.Custom;
            dtpHora.CustomFormat = "hh:mm tt";
            dtpHora.ShowUpDown = true;

            // Iniciar con la hora actual
            dtpHora.Value = DateTime.Now;

            // El formulario se cierra al presionar Enter
            AcceptButton = btnAceptar;

            // El formulario se cancela con Esc
            CancelButton = btnCancelar;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
