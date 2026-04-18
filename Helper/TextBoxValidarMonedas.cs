using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Helper
{
    internal class TextBoxValidarMonedas
    {
        public static void Attach(TextBox textBox)
        {
            textBox.KeyPress += TextBox_KeyPress;
            textBox.KeyDown += TextBox_KeyDown;
            textBox.Leave += TextBox_Leave;
            textBox.TextAlign = HorizontalAlignment.Right;
        }

        private static void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;

            // Permitir control, dígitos y 1 solo punto decimal
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            // Permitir solo un punto decimal
            if (e.KeyChar == '.')
            {
                if (!txt.Text.Contains("."))
                    return;
            }

            // Si no cumple nada anterior, bloquear
            e.Handled = true;
        }

        private static void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear pegar texto inválido (Ctrl+V)
            if (e.Control && e.KeyCode == Keys.V)
            {
                string texto = Clipboard.GetText();

                if (!decimal.TryParse(texto, out _))
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

        private static void TextBox_Leave(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;

            if (decimal.TryParse(txt.Text, out decimal valor))
            {
                // Formato de 2 decimales
                txt.Text = valor.ToString("C2");
            }
            else
            {
                // Si está vacío o malo, poner 0.00
                txt.Text = "$0.00";
            }
        }
    }
}
