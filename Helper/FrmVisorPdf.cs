using Microsoft.Web.WebView2.WinForms;
using System;
using System.Windows.Forms;

namespace FresamLogistica.Helper
{
    public class FrmVisorPdf : Form
    {
        private readonly string _rutaPdf;
        private readonly WebView2 _webView;

        public FrmVisorPdf(
            string rutaPdf,
            string titulo = "Visor de PDF")
        {
            _rutaPdf = rutaPdf;

            Text = titulo;

            Width = 1000;
            Height = 700;

            StartPosition =
                FormStartPosition.CenterScreen;

            _webView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(_webView);

            Load += FrmVisorPdf_Load;
        }

        private async void FrmVisorPdf_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                await _webView.EnsureCoreWebView2Async();

                _webView.CoreWebView2.Navigate(
                    new Uri(_rutaPdf).AbsoluteUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No fue posible visualizar el PDF.\n\n{ex.Message}",
                    "Visor de PDF",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
            }
        }
    }
}
