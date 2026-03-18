using FresamLogistica.Model;
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
    public partial class ReportMasterDetailHelper : Form
    {
        private string rutaReporte;
        private List<ReportParameter> parametros;
        private DataTable infoMaestro;
        private DataTable infoDetalle;
        private string subReportName;
        private string dataSetName;
        private string paramFilterName;
        private string columFilterName;

        public ReportMasterDetailHelper(string _rutaReporte, List<ReportParameter> _parametros, 
            DataTable _infoMaestro, DataTable _infoDetalle,
            string _subReportName, string _dataSetName, string _paramFilterName, string _columFilterName)
        {
            InitializeComponent();

            rutaReporte = _rutaReporte;
            parametros = _parametros;
            infoMaestro = _infoMaestro;
            infoDetalle = _infoDetalle;
            subReportName = _subReportName;
            dataSetName = _dataSetName;
            paramFilterName = _paramFilterName;
            columFilterName = _columFilterName;

            rvInfo.LocalReport.SubreportProcessing += ReportViewer_SubreportProcessing;
        }

        private void ReportMasterDetailHelper_Load(object sender, EventArgs e)
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
                rvInfo.SetPageSettings(new System.Drawing.Printing.PageSettings()
                {
                    Landscape = true,
                    Margins = new System.Drawing.Printing.Margins(40, 40, 40, 40)
                });
            
                foreach(ReportParameter param in parametros)
                {
                    rvInfo.LocalReport.SetParameters(param);

                }

                if(this.infoMaestro != null)
                {
                    if(rvInfo.LocalReport.DataSources.Count > 0)
                    {
                        rvInfo.LocalReport.DataSources.Clear();
                    }
                    ReportDataSource rds = new ReportDataSource(infoMaestro.TableName, infoMaestro);
                    rvInfo.LocalReport.DataSources.Add(rds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.rvInfo.RefreshReport();
        }

        private void ReportViewer_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == subReportName)
            {
                string RecepcionId = e.Parameters[paramFilterName].Values[0].ToString();

                DataTable _dtDetalle = new DataTable();

                foreach(DataColumn col in infoDetalle.Columns)
                {
                    _dtDetalle.Columns.Add(col.ColumnName, col.DataType);
                }

                foreach ( DataRow r in infoDetalle.Rows)
                {
                    DataRow _r = _dtDetalle.NewRow();
                    _r.ItemArray = r.ItemArray;

                    if(_r[columFilterName].ToString() == RecepcionId)
                    {
                        _dtDetalle.Rows.Add(_r);
                    }
                }

                e.DataSources.Add(new ReportDataSource(dataSetName, _dtDetalle));
            }
        }
    }


}
