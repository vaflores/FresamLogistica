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
using FresamLogistica.Controllers.General;
using FresamLogistica.Helper;

namespace FresamLogistica.Views.General
{
    public partial class EmpresaView : Form
    {
        private readonly EmpresaController _repo;
        private int _idEmpresaSeleccionada;
        public EmpresaView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            _repo = new EmpresaController();
            _idEmpresaSeleccionada = 1; //ID por default de la empresa

            CargarDatosEmpresa();
            ValidarPermisos();

        }

        #region EventosFormulario
        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            CargarDatosEmpresa();
        }
        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            BorrarDatosEmpresa();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            EliminarDatosEmpresa();
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            ImprimirDatosEmpresa();
        }
        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            try
            {                 
                GuardarDatosEmpresa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos de la empresa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubirLogo_Click(object sender, EventArgs e)
        {
            try { 
                SubirLogo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al subir el logo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmpresaView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && tsbNuevo.Enabled) // Ctrl +  → Nuevo
            {
                BorrarDatosEmpresa();
            }
            else if (e.Control && e.KeyCode == Keys.G && tsbGuardar.Enabled) // Ctrl + G → Guardar
            {
                GuardarDatosEmpresa();
            }
            else if (e.Control && e.KeyCode == Keys.E && tsbEliminar.Enabled) // Ctrl + E → Eliminar
            {
                EliminarDatosEmpresa();
            }
            else if (e.Control && e.KeyCode == Keys.B && tsbBuscar.Enabled) // Ctrl + B → Buscar
            {
                CargarDatosEmpresa();
            }
            else if (e.Control && e.KeyCode == Keys.P && tsbImprimir.Enabled) // Ctrl + P → Imprimir
            {
                ImprimirDatosEmpresa();
            }
        }

        #endregion

        #region MetodosPrivados
        private void ValidarPermisos()
        {
            // Aquí se deben implementar las validaciones de permisos según el usuario actual.
            // Por ahora, habilitamos todos los botones.
            tsbNuevo.Enabled = SesionActualHelper.Permisos.Any(p=> p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Crear");
            tsbGuardar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Editar");
            tsbEliminar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Eliminar");
            tsbBuscar.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Consultar");
            tsbImprimir.Enabled = SesionActualHelper.Permisos.Any(p => p.PantallaId == Convert.ToInt32(this.Tag) && p.NombrePermiso == "Imprimir");
        }
        private void CargarDatosEmpresa()
        {
            var empresa = _repo.ObtenerPorId(_idEmpresaSeleccionada);
            if (empresa != null)
            {
                txtNombre.Text = empresa.nombre;
                txtRfc.Text = empresa.rfc;
                txtCalleNum.Text = empresa.calle;
                txtColonia.Text = empresa.colonia;
                txtCiudad.Text = empresa.ciudad;
                txtEstado.Text = empresa.estado;
                if (empresa.logo != null)
                {
                    using (var ms = new System.IO.MemoryStream(empresa.logo))
                    {
                        pbLogo.Image = Image.FromStream(ms);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se encontraron datos de la empresa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GuardarDatosEmpresa()
        {
            var empresa = new Model.Empresa
            {
                empresa_id = _idEmpresaSeleccionada,
                nombre = txtNombre.Text,
                rfc = txtRfc.Text,
                calle = txtCalleNum.Text,
                colonia = txtColonia.Text,
                ciudad = txtCiudad.Text,
                estado = txtEstado.Text
            };
            if (pbLogo.Image != null)
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    pbLogo.Image.Save(ms, pbLogo.Image.RawFormat);
                    empresa.logo = ms.ToArray();
                }
            }
            var existente = _repo.ObtenerPorId(_idEmpresaSeleccionada);
            if (existente != null)
            {
                _repo.Actualizar(empresa);
                MessageBox.Show("Datos de la empresa actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _repo.Crear(empresa);
                MessageBox.Show("Datos de la empresa guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BorrarDatosEmpresa()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void EliminarDatosEmpresa()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void ImprimirDatosEmpresa()
        {
            Debug.WriteLine("Función no implementada.");
        }

        private void SubirLogo()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar Logo de la Empresa";
                ofd.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbLogo.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        #endregion

        
    }
}
