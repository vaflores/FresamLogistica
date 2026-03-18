using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.Views;
using FresamLogistica.Views.Configuracion;
using FresamLogistica.Views.General;
using FresamLogistica.Views.Herramientas;
using FresamLogistica.Views.Productos;
using FresamLogistica.Views.Reportes;
using FresamLogistica.Views.Reportes.Titulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica
{
    public partial class MDIPrincipal : Form
    {

        public MDIPrincipal()
        {
            InitializeComponent();

        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormularios();
        }

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new EmpresaView(), this);
        }

        private void unidadesDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new UnidadesMedidaView(),this);
        }

        private void frutasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new FrutasView(), this);
        }

        private void agricultoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new AgricultoresView(), this);
        }

        private void inspeccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new TitulosInspeccionesView(), this);
        }

        private void calidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new TitulosCalidadesView(), this);
        }

        private void gradosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new TitulosGradosView(), this);
        }

        private void calidadesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new CalidadesView(), this);
        }

        private void condicionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new CondicionesView(), this);
        }

        private void ciclosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new CiclosView(), this);
        }

        private void sectoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new SectoresView(), this);
        }

        private void recepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new RecepcionesView(), this);
        }

        private void vehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new VehiculosView(), this);
        }

        private void etiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new TitulosEtiquetasView(), this);
        }

        private void inspecciónDeVehículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new InspeccionVehiculosView(), this);
        }

        private void gradosDeCalidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new GradosCalidadView(), this);
        }

        private void gradoDeCalidadDeLaFrutaProductoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new GradosCalidadProductoresView(), this);
        }

        private void muestreoDeCalidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new MuestreoCalidadView(), this);
        }

        private void recepcionesPorAgricultorYCicloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new RecepcionesAgricultorCicloView(), this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new UsuariosView(), this);
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new RolesView(), this);
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormularios();
            Helper.SesionActualHelper.Instancia.CerrarSesion();
            Helper.FormulariosHelper.CargarFormulario(new LoginView(), this);

            if (!Helper.SesionActualHelper.Instancia.HaySesionActiva)
            {
                this.Close();
            }

            configuraciónToolStripMenuItem.Visible = SesionActualHelper.Permisos.Any(p => p.PantallaId == 20 && p.NombrePermiso == "Configuracion de Usuarios y Permisos"); ;
        }

        private void MDIPrincipal_Load(object sender, EventArgs e)
        {

            Helper.FormulariosHelper.CargarFormulario(new LoginView(), this);

            if (!Helper.SesionActualHelper.Instancia.HaySesionActiva)
            {
                this.Close();
            }

            configuraciónToolStripMenuItem.Visible = SesionActualHelper.Permisos.Any(p => p.PantallaId == 20 && p.NombrePermiso == "Configuracion de Usuarios y Permisos"); ;
        }

        private void permisosARolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.FormulariosHelper.CargarFormularioHijo(new RolesPermisosView(), this);
        }

        private void CerrarFormularios()
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
    }
}
