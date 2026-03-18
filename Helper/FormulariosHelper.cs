using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Helper
{
    public static class FormulariosHelper
    {
        public static void CargarFormularioHijo(Form formulario, Form formularioPadre)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == formulario.GetType())
                {
                    frm.BringToFront();
                    return;
                }
            }
            foreach (Form frm in Application.OpenForms)
            {
                if (frm == formularioPadre)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    break;
                }
            }
            formulario.Owner = formularioPadre;
            formulario.StartPosition = FormStartPosition.CenterParent;
            formulario.MdiParent = formularioPadre;
            formulario.Show();
        }

        public static void CargarFormulario(Form formulario, Form formularioPadre)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == formulario.GetType())
                {
                    frm.BringToFront();
                    return;
                }
            }
            formulario.StartPosition = FormStartPosition.CenterParent;
            formulario.Owner = formularioPadre;
            formulario.FormBorderStyle = FormBorderStyle.FixedDialog;
            formulario.MaximizeBox = false;
            formulario.ShowDialog();
        }

        public static void CargarDialogoFormulario(Form formulario)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == formulario.GetType())
                {
                    frm.BringToFront();
                    return;
                }
            }
            formulario.ShowDialog();
        }
    }
}
