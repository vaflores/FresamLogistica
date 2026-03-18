using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Helper
{
    internal class TextBoxValidarEnteros
    {
        public static void Attach(TextBox textBox)
        {
            textBox.KeyPress += TextBox_KeyPress;
            textBox.Leave += TextBox_Leave;
            textBox.KeyDown += TextBox_KeyDown;
        }

        private static void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private static void TextBox_Leave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            // Si no es un entero válido, lo reseteamos
            if (!int.TryParse(textBox.Text, out _))
            {
                textBox.Text = "0"; // o vacío ""
            }
        }

        private static void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear pegar Ctrl+V si el texto no es numérico
            if (e.Control && e.KeyCode == Keys.V)
            {
                string textoPegado = Clipboard.GetText();

                if (!int.TryParse(textoPegado, out _))
                {
                    e.SuppressKeyPress = true; // cancela el pegado
                }
            }
        }
    }
}
