using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Helper
{
    public partial class ReportHelper : Form
    {
        private string rutaReporte;
        private List<ReportParameter> parametros;
        private DataTable infoReporte;
        public ReportHelper(string _rutaReporte, List<ReportParameter> _parametros, DataTable _infoReporte = null)
        {
            InitializeComponent();

            rutaReporte = _rutaReporte;
            parametros = _parametros;
            infoReporte = _infoReporte;
        }

        private void ReportHelper_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.rvInfo.RefreshReport();
            try
            { 
                rvInfo.ProcessingMode = ProcessingMode.Local;
                rvInfo.LocalReport.ReportPath = rutaReporte;
                rvInfo.LocalReport.EnableExternalImages = true;
                rvInfo.SetDisplayMode(DisplayMode.PrintLayout);
                rvInfo.ZoomMode = ZoomMode.PageWidth;
            
                foreach(ReportParameter param in parametros)
                {
                    rvInfo.LocalReport.SetParameters(param);

                }

                if(this.infoReporte != null)
                {
                    if(rvInfo.LocalReport.DataSources.Count > 0)
                    {
                        rvInfo.LocalReport.DataSources.Clear();
                    }
                    ReportDataSource rds = new ReportDataSource(infoReporte.TableName, infoReporte);
                    rvInfo.LocalReport.DataSources.Add(rds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.rvInfo.RefreshReport();

        }
    }


}
