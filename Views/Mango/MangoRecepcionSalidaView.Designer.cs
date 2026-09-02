namespace FresamLogistica.Views.Mango
{
    partial class MangoRecepcionSalidaView
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
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.txtPesoDisponible = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tcRegistros = new System.Windows.Forms.TabControl();
            this.tpRegistros = new System.Windows.Forms.TabPage();
            this.dgvCondiciones = new System.Windows.Forms.DataGridView();
            this.tpTarimas = new System.Windows.Forms.TabPage();
            this.tsOpciones = new System.Windows.Forms.ToolStrip();
            this.tsbSyncGrid = new System.Windows.Forms.ToolStripButton();
            this.tsbAddGrid = new System.Windows.Forms.ToolStripButton();
            this.tsbDelGrid = new System.Windows.Forms.ToolStripButton();
            this.dgvTarimas = new System.Windows.Forms.DataGridView();
            this.tsMenu.SuspendLayout();
            this.tcRegistros.SuspendLayout();
            this.tpRegistros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondiciones)).BeginInit();
            this.tpTarimas.SuspendLayout();
            this.tsOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarimas)).BeginInit();
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
            this.tsMenu.Size = new System.Drawing.Size(759, 45);
            this.tsMenu.TabIndex = 6;
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
            this.tsbImprimir.Enabled = false;
            this.tsbImprimir.Image = global::FresamLogistica.Properties.Resources.print_50;
            this.tsbImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImprimir.Name = "tsbImprimir";
            this.tsbImprimir.Size = new System.Drawing.Size(34, 42);
            this.tsbImprimir.Text = "Imprimir";
            this.tsbImprimir.Click += new System.EventHandler(this.tsbImprimir_Click);
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Location = new System.Drawing.Point(13, 83);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(0, 20);
            this.lblFechaInicial.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 56;
            this.label1.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.Location = new System.Drawing.Point(12, 138);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(735, 91);
            this.txtObservaciones.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 80);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 20);
            this.label6.TabIndex = 54;
            this.label6.Text = "Pendiente:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(128, 51);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(125, 26);
            this.dtpFecha.TabIndex = 48;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(344, 51);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(125, 26);
            this.txtCantidad.TabIndex = 50;
            // 
            // txtPesoDisponible
            // 
            this.txtPesoDisponible.Enabled = false;
            this.txtPesoDisponible.Location = new System.Drawing.Point(128, 80);
            this.txtPesoDisponible.Name = "txtPesoDisponible";
            this.txtPesoDisponible.Size = new System.Drawing.Size(125, 26);
            this.txtPesoDisponible.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 53;
            this.label3.Text = "Cantidad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 20);
            this.label4.TabIndex = 52;
            this.label4.Text = "Fecha:";
            // 
            // tcRegistros
            // 
            this.tcRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcRegistros.Controls.Add(this.tpRegistros);
            this.tcRegistros.Controls.Add(this.tpTarimas);
            this.tcRegistros.Location = new System.Drawing.Point(12, 235);
            this.tcRegistros.Name = "tcRegistros";
            this.tcRegistros.SelectedIndex = 0;
            this.tcRegistros.Size = new System.Drawing.Size(735, 343);
            this.tcRegistros.TabIndex = 57;
            // 
            // tpRegistros
            // 
            this.tpRegistros.Controls.Add(this.dgvCondiciones);
            this.tpRegistros.Location = new System.Drawing.Point(4, 29);
            this.tpRegistros.Name = "tpRegistros";
            this.tpRegistros.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegistros.Size = new System.Drawing.Size(727, 310);
            this.tpRegistros.TabIndex = 0;
            this.tpRegistros.Text = "Condiciones";
            this.tpRegistros.UseVisualStyleBackColor = true;
            // 
            // dgvCondiciones
            // 
            this.dgvCondiciones.AllowUserToAddRows = false;
            this.dgvCondiciones.AllowUserToDeleteRows = false;
            this.dgvCondiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCondiciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCondiciones.Location = new System.Drawing.Point(3, 3);
            this.dgvCondiciones.Name = "dgvCondiciones";
            this.dgvCondiciones.ReadOnly = true;
            this.dgvCondiciones.Size = new System.Drawing.Size(721, 304);
            this.dgvCondiciones.TabIndex = 0;
            this.dgvCondiciones.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCondiciones_CellEndEdit);
            this.dgvCondiciones.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvCondiciones_CellValidating);
            // 
            // tpTarimas
            // 
            this.tpTarimas.Controls.Add(this.tsOpciones);
            this.tpTarimas.Controls.Add(this.dgvTarimas);
            this.tpTarimas.Location = new System.Drawing.Point(4, 29);
            this.tpTarimas.Name = "tpTarimas";
            this.tpTarimas.Size = new System.Drawing.Size(727, 310);
            this.tpTarimas.TabIndex = 1;
            this.tpTarimas.Text = "Tarimas";
            this.tpTarimas.UseVisualStyleBackColor = true;
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
            this.tsOpciones.Size = new System.Drawing.Size(727, 45);
            this.tsOpciones.TabIndex = 9;
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
            this.dgvTarimas.Size = new System.Drawing.Size(721, 259);
            this.dgvTarimas.TabIndex = 8;
            this.dgvTarimas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTarimas_CellContentClick);
            this.dgvTarimas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTarimas_CellEndEdit);
            this.dgvTarimas.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvTarimas_CellValidating);
            // 
            // MangoRecepcionSalidaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 590);
            this.Controls.Add(this.tcRegistros);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.txtPesoDisponible);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblFechaInicial);
            this.Controls.Add(this.tsMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MangoRecepcionSalidaView";
            this.Text = "Traspaleo";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tcRegistros.ResumeLayout(false);
            this.tpRegistros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondiciones)).EndInit();
            this.tpTarimas.ResumeLayout(false);
            this.tpTarimas.PerformLayout();
            this.tsOpciones.ResumeLayout(false);
            this.tsOpciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarimas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.ToolStripButton tsbCrear;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ToolStripButton tsbImprimir;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TextBox txtPesoDisponible;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tcRegistros;
        private System.Windows.Forms.TabPage tpRegistros;
        private System.Windows.Forms.DataGridView dgvCondiciones;
        private System.Windows.Forms.TabPage tpTarimas;
        private System.Windows.Forms.ToolStrip tsOpciones;
        private System.Windows.Forms.ToolStripButton tsbSyncGrid;
        private System.Windows.Forms.ToolStripButton tsbAddGrid;
        private System.Windows.Forms.ToolStripButton tsbDelGrid;
        private System.Windows.Forms.DataGridView dgvTarimas;
    }
}