using FresamLogistica.Controllers.Configuracion;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Views
{
    public partial class LoginView : Form
    {
        private readonly UsuariosController _repo;
        public LoginView()
        {
            InitializeComponent();

            _repo = new UsuariosController();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(txtUsuario.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuarios _usuario = _repo.ValidarCredenciales(txtUsuario.Text.Trim(), PasswordHelper.Encrypt(txtPassword.Text.Trim(),Constantes.claveSecreta));

            if (_usuario != null)
            {
                SesionActualHelper.Instancia.RegistraSesionCrea(_usuario.usuario_id, _usuario.rol_id);
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
