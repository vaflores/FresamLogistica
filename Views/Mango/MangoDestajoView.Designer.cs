namespace FresamLogistica.Views.Mango
{
    partial class MangoDestajoView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.tsbCrear = new System.Windows.Forms.ToolStripButton();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.tsbImprimir = new System.Windows.Forms.ToolStripButton();
            this.tpRegistros = new System.Windows.Forms.TabPage();
            this.tsOpciones = new System.Windows.Forms.ToolStrip();
            this.tsbSyncGrid = new System.Windows.Forms.ToolStripButton();
            this.tsbAddGrid = new System.Windows.Forms.ToolStripButton();
            this.tsbDelGrid = new System.Windows.Forms.ToolStripButton();
            this.dgvTarimas = new System.Windows.Forms.DataGridView();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.txtPesoNeto = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tcRecepcion = new System.Windows.Forms.TabControl();
            this.cbVerifica = new System.Windows.Forms.ComboBox();
            this.cbElabora = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.dtpFechaTermino = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEstado = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalCajas = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tsMenu.SuspendLayout();
            this.tpRegistros.SuspendLayout();
            this.tsOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarimas)).BeginInit();
            this.tcRecepcion.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMenu
            // 
            this.tsMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBuscar,
            this.tsbCrear,
            this.tsbGuardar,
            this.tsbEliminar,
            this.tsbImprimir});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.MinimumSize = new System.Drawing.Size(0, 45);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMenu.Size = new System.Drawing.Size(980, 45);
            this.tsMenu.TabIndex = 8;
            this.tsMenu.Text = "tsMenu";
            // 
            // tsbBuscar
            // 
            this.tsbBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBuscar.Image = global::FresamLogistica.Properties.Resources.find_50;
            this.tsbBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuscar.Name = "tsbBuscar";
            this.tsbBuscar.Size = new System.Drawing.Size(34, 42);
            this.tsbBuscar.Text = "Buscar";
            this.tsbBuscar.Click += new System.EventHandler(this.tsbBuscar_Click);
            // 
            // tsbCrear
            // 
            this.tsbCrear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCrear.Image = global::FresamLogistica.Properties.Resources.create_50;
            this.tsbCrear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCrear.Name = "tsbCrear";
            this.tsbCrear.Size = new System.Drawing.Size(34, 42);
            this.tsbCrear.Text = "Crear";
            this.tsbCrear.Click += new System.EventHandler(this.tsbCrear_Click);
            // 
            // tsbGuardar
            // 
            this.tsbGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGuardar.Image = global::FresamLogistica.Properties.Resources.save_50;
            this.tsbGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGuardar.Name = "tsbGuardar";
            this.tsbGuardar.Size = new System.Drawing.Size(34, 42);
            this.tsbGuardar.Text = "Guardar";
            this.tsbGuardar.Click += new System.EventHandler(this.tsbGuardar_Click);
            // 
            // tsbEliminar
            // 
            this.tsbEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEliminar.Image = global::FresamLogistica.Properties.Resources.delete_50;
            this.tsbEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEliminar.Name = "tsbEliminar";
            this.tsbEliminar.Size = new System.Drawing.Size(34, 42);
            this.tsbEliminar.Text = "Eliminar";
            this.tsbEliminar.Click += new System.EventHandler(this.tsbEliminar_Click);
            // 
            // tsbImprimir
            // 
            this.tsbImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImprimir.Image = global::FresamLogistica.Properties.Resources.control_50;
            this.tsbImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImprimir.Name = "tsbImprimir";
            this.tsbImprimir.Size = new System.Drawing.Size(34, 42);
            this.tsbImprimir.Text = "Recibo";
            this.tsbImprimir.Click += new System.EventHandler(this.tsbImprimir_Click);
            // 
            // tpRegistros
            // 
            this.tpRegistros.Controls.Add(this.tsOpciones);
            this.tpRegistros.Controls.Add(this.dgvTarimas);
            this.tpRegistros.Location = new System.Drawing.Point(4, 29);
            this.tpRegistros.Name = "tpRegistros";
            this.tpRegistros.Size = new System.Drawing.Size(948, 308);
            this.tpRegistros.TabIndex = 3;
            this.tpRegistros.Text = "Tarimas procesadas";
            this.tpRegistros.UseVisualStyleBackColor = true;
            // 
            // tsOpciones
            // 
            this.tsOpciones.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsOpciones.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.tsOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSyncGrid,
            this.tsbAddGrid,
            this.tsbDelGrid});
            this.tsOpciones.Location = new System.Drawing.Point(0, 0);
            this.tsOpciones.MinimumSize = new System.Drawing.Size(0, 45);
            this.tsOpciones.Name = "tsOpciones";
            this.tsOpciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsOpciones.Size = new System.Drawing.Size(948, 45);
            this.tsOpciones.TabIndex = 8;
            this.tsOpciones.Text = "toolStrip1";
            // 
            // tsbSyncGrid
            // 
            this.tsbSyncGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSyncGrid.Image = global::FresamLogistica.Properties.Resources.sync_50;
            this.tsbSyncGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSyncGrid.Name = "tsbSyncGrid";
            this.tsbSyncGrid.Size = new System.Drawing.Size(34, 42);
            this.tsbSyncGrid.Text = "Buscar";
            this.tsbSyncGrid.Click += new System.EventHandler(this.tsbSyncGrid_Click);
            // 
            // tsbAddGrid
            // 
            this.tsbAddGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddGrid.Image = global::FresamLogistica.Properties.Resources.create_50;
            this.tsbAddGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddGrid.Name = "tsbAddGrid";
            this.tsbAddGrid.Size = new System.Drawing.Size(34, 42);
            this.tsbAddGrid.Text = "Crear";
            this.tsbAddGrid.Click += new System.EventHandler(this.tsbAddGrid_Click);
            // 
            // tsbDelGrid
            // 
            this.tsbDelGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelGrid.Image = global::FresamLogistica.Properties.Resources.remove_50;
            this.tsbDelGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelGrid.Name = "tsbDelGrid";
            this.tsbDelGrid.Size = new System.Drawing.Size(34, 42);
            this.tsbDelGrid.Text = "Eliminar";
            this.tsbDelGrid.Click += new System.EventHandler(this.tsbDelGrid_Click);
            // 
            // dgvTarimas
            // 
            this.dgvTarimas.AllowUserToAddRows = false;
            this.dgvTarimas.AllowUserToDeleteRows = false;
            this.dgvTarimas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTarimas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTarimas.Location = new System.Drawing.Point(3, 48);
            this.dgvTarimas.Name = "dgvTarimas";
            this.dgvTarimas.ReadOnly = true;
            this.dgvTarimas.Size = new System.Drawing.Size(942, 257);
            this.dgvTarimas.TabIndex = 1;
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Location = new System.Drawing.Point(12, 115);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(119, 20);
            this.lblFechaInicial.TabIndex = 65;
            this.lblFechaInicial.Text = "Fecha de inicio:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(137, 110);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(162, 26);
            this.dtpFechaInicio.TabIndex = 3;
            // 
            // txtPesoNeto
            // 
            this.txtPesoNeto.Location = new System.Drawing.Point(398, 141);
            this.txtPesoNeto.Name = "txtPesoNeto";
            this.txtPesoNeto.Size = new System.Drawing.Size(150, 26);
            this.txtPesoNeto.TabIndex = 6;
            this.txtPesoNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(306, 144);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 20);
            this.label14.TabIndex = 55;
            this.label14.Text = "Peso neto:";
            // 
            // tcRecepcion
            // 
            this.tcRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcRecepcion.Controls.Add(this.tpRegistros);
            this.tcRecepcion.Location = new System.Drawing.Point(12, 173);
            this.tcRecepcion.Name = "tcRecepcion";
            this.tcRecepcion.SelectedIndex = 0;
            this.tcRecepcion.Size = new System.Drawing.Size(956, 341);
            this.tcRecepcion.TabIndex = 15;
            // 
            // cbVerifica
            // 
            this.cbVerifica.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVerifica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVerifica.FormattingEnabled = true;
            this.cbVerifica.Location = new System.Drawing.Point(137, 79);
            this.cbVerifica.Name = "cbVerifica";
            this.cbVerifica.Size = new System.Drawing.Size(831, 28);
            this.cbVerifica.TabIndex = 2;
            // 
            // cbElabora
            // 
            this.cbElabora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbElabora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbElabora.FormattingEnabled = true;
            this.cbElabora.Location = new System.Drawing.Point(137, 48);
            this.cbElabora.Name = "cbElabora";
            this.cbElabora.Size = new System.Drawing.Size(831, 28);
            this.cbElabora.TabIndex = 1;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(12, 82);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(66, 20);
            this.label35.TabIndex = 68;
            this.label35.Text = "Verifica:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(12, 51);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(107, 20);
            this.label36.TabIndex = 67;
            this.label36.Text = "Responsable:";
            // 
            // dtpFechaTermino
            // 
            this.dtpFechaTermino.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaTermino.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaTermino.Location = new System.Drawing.Point(137, 139);
            this.dtpFechaTermino.Name = "dtpFechaTermino";
            this.dtpFechaTermino.Size = new System.Drawing.Size(162, 26);
            this.dtpFechaTermino.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 144);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 70;
            this.label3.Text = "Fecha de cierre:";
            // 
            // cbEstado
            // 
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(398, 110);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(150, 28);
            this.cbEstado.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(306, 115);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 20);
            this.label7.TabIndex = 72;
            this.label7.Text = "Estatus:";
            // 
            // txtTotalCajas
            // 
            this.txtTotalCajas.Location = new System.Drawing.Point(667, 141);
            this.txtTotalCajas.Name = "txtTotalCajas";
            this.txtTotalCajas.Size = new System.Drawing.Size(150, 26);
            this.txtTotalCajas.TabIndex = 7;
            this.txtTotalCajas.Text = "0";
            this.txtTotalCajas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(554, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 20);
            this.label4.TabIndex = 74;
            this.label4.Text = "Total de cajas:";
            // 
            // MangoDestajoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 526);
            this.Controls.Add(this.txtTotalCajas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbEstado);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpFechaTermino);
            this.Controls.Add(this.cbVerifica);
            this.Controls.Add(this.cbElabora);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.tcRecepcion);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.lblFechaInicial);
            this.Controls.Add(this.txtPesoNeto);
            this.Controls.Add(this.tsMenu);
            this.Controls.Add(this.label14);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MangoDestajoView";
            this.Tag = "35";
            this.Text = "Producción";
            this.Load += new System.EventHandler(this.RecepcionView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tpRegistros.ResumeLayout(false);
            this.tpRegistros.PerformLayout();
            this.tsOpciones.ResumeLayout(false);
            this.tsOpciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarimas)).EndInit();
            this.tcRecepcion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.ToolStripButton tsbCrear;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.TabPage tpRegistros;
        private System.Windows.Forms.TextBox txtPesoNeto;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dgvTarimas;
        private System.Windows.Forms.TabControl tcRecepcion;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.ComboBox cbVerifica;
        private System.Windows.Forms.ComboBox cbElabora;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.DateTimePicker dtpFechaTermino;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbEstado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalCajas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip tsOpciones;
        private System.Windows.Forms.ToolStripButton tsbSyncGrid;
        private System.Windows.Forms.ToolStripButton tsbAddGrid;
        private System.Windows.Forms.ToolStripButton tsbDelGrid;
        private System.Windows.Forms.ToolStripButton tsbImprimir;
    }
}