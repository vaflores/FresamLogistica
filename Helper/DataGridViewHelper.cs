using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Helper
{
    internal class DataGridViewHelper
    {
        public static void SetDataGridViewProperties(System.Windows.Forms.DataGridView dgv)
        {
            dgv.AllowUserToOrderColumns = true;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;

            //dgv.RowHeadersVisible = false;
            dgv.AutoGenerateColumns = false;
            dgv.ShowCellToolTips = false;

            // Ajustar el tamaño de las columnas automáticamente
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // (Opcional) ajustar modo de selección, solo filas completas
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // (Opcional) evitar que el usuario agregue o elimine filas directamente
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;

            // (Opcional) impedir edición directa en las celdas
            dgv.ReadOnly = true;
        }

        public static void SortDataGridViewByColumn<T>(System.Windows.Forms.DataGridView dgv, int columnIndex, List<T> Lista)
        {
            string columnName = dgv.Columns[columnIndex].DataPropertyName;

            // Ejemplo simple: alternar orden
            if (dgv.Tag as string == "ASC")
            {
                dgv.DataSource = Lista.OrderByDescending(p =>
                    p.GetType().GetProperty(columnName)?.GetValue(p, null)).ToList();
                dgv.Tag = "DESC";
            }
            else
            {
                dgv.DataSource = Lista.OrderBy(p =>
                    p.GetType().GetProperty(columnName)?.GetValue(p, null)).ToList();
                dgv.Tag = "ASC";
            }
        }

        public static void AddColumn(System.Windows.Forms.DataGridView dgv, string _headerText, string _dataPropertyName, int _width = 100, bool _visible = true, bool _readOnly = true)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                HeaderText = _headerText,
                DataPropertyName = _dataPropertyName,
                Name = _dataPropertyName,
                Width = _width,
                ReadOnly = _readOnly,
                Visible = _visible
            };
            dgv.Columns.Add(column);
        }

        public static void AddColumnFecha(System.Windows.Forms.DataGridView dgv, string headerText, string dataPropertyName, int width = 100, bool visible = true)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                Name = dataPropertyName,
                DefaultCellStyle = { Format = "dd/MM/yyyy" },
                Width = width,
                ReadOnly = true,
                Visible = visible
            };
            dgv.Columns.Add(column);
        }

        public static void AddColumnMoneda(System.Windows.Forms.DataGridView dgv, string headerText, string dataPropertyName, int width = 100, bool visible = true)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                Name = dataPropertyName,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = width,
                ReadOnly = true,
                Visible = visible
            };
            dgv.Columns.Add(column);
        }
        
        public static void AddColumnNumeroSinDecimales(System.Windows.Forms.DataGridView dgv, string _headerText, string _dataPropertyName, int _width = 100, bool _visible = true, bool _readOnly = true)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                HeaderText = _headerText,
                DataPropertyName = _dataPropertyName,
                Name = _dataPropertyName,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = _width,
                ReadOnly = _readOnly,
                Visible = _visible
            };
            dgv.Columns.Add(column);
        }
        
        public static void AddColumnNumeroConDecimales(System.Windows.Forms.DataGridView dgv, string _headerText, string _dataPropertyName, int _width = 100, bool _visible = true, bool _readOnly = true)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                HeaderText = _headerText,
                DataPropertyName = _dataPropertyName,
                Name = _dataPropertyName,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = _width,
                ReadOnly = _readOnly,
                Visible = _visible
            };
            dgv.Columns.Add(column);
        }

        public static void AddColumnCheckBox(System.Windows.Forms.DataGridView dgv, string _headerText, string _dataPropertyName, int _width = 100, bool _visible = true, bool _readOnly = true)
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn
            {
                HeaderText = _headerText,
                DataPropertyName = _dataPropertyName,
                Name = _dataPropertyName,
                Width = _width,
                ReadOnly = _readOnly,
                Visible = _visible
            };
            dgv.Columns.Add(column);
        }

        public static void AddColumnEditar(System.Windows.Forms.DataGridView dgv, string headerText, string dataPropertyName, int width = 50)
        {
            if (dgv.Columns["Editar"] == null)
            { 
                DataGridViewButtonColumn column = new DataGridViewButtonColumn
                {
                    HeaderText = headerText,
                    DataPropertyName = dataPropertyName,
                    Name = dataPropertyName,
                    Width = width,
                    Text = "Editar",
                    UseColumnTextForButtonValue = true
                };
                dgv.Columns.Add(column);
            }
        }
    }
}
