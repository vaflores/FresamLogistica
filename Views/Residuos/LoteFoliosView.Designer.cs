namespace FresamLogistica.Views.Residuos
{
    partial class LoteFoliosView
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
            this.cbEstado = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFolioInicio = new System.Windows.Forms.TextBox();
            this.txtFolioFinal = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.tcVariedades = new System.Windows.Forms.TabControl();
            this.tpRegistros = new System.Windows.Forms.TabPage();
            this.tsRanchos = new System.Windows.Forms.ToolStrip();
            this.tsbSyncGrid = new System.Windows.Forms.ToolStripButton();
            this.dgvRegistros = new System.Windows.Forms.DataGridView();
            this.dtpFechaCompra = new System.Windows.Forms.DateTimePicker();
            this.dtpVigencia = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDestinoResiduos = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tsMenu.SuspendLayout();
            this.tcVariedades.SuspendLayout();
            this.tpRegistros.SuspendLayout();
            this.tsRanchos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistros)).BeginInit();
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
            this.tsMenu.Size = new System.Drawing.Size(520, 45);
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
            // cbEstado
            // 
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(380, 143);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(125, 28);
            this.cbEstado.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 146);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 29;
            this.label1.Text = "Estado:";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Fecha de compra:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 33;
            this.label3.Text = "Folio inicial:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 33;
            this.label4.Text = "Folio final:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 146);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "Costo:";
            // 
            // txtFolioInicio
            // 
            this.txtFolioInicio.Location = new System.Drawing.Point(166, 112);
            this.txtFolioInicio.Name = "txtFolioInicio";
            this.txtFolioInicio.Size = new System.Drawing.Size(125, 26);
            this.txtFolioInicio.TabIndex = 4;
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.Location = new System.Drawing.Point(380, 112);
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.Size = new System.Drawing.Size(125, 26);
            this.txtFolioFinal.TabIndex = 5;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(166, 143);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(125, 26);
            this.txtImporte.TabIndex = 6;
            // 
            // tcVariedades
            // 
            this.tcVariedades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcVariedades.Controls.Add(this.tpRegistros);
            this.tcVariedades.Location = new System.Drawing.Point(12, 175);
            this.tcVariedades.Name = "tcVariedades";
            this.tcVariedades.SelectedIndex = 0;
            this.tcVariedades.Size = new System.Drawing.Size(496, 291);
            this.tcVariedades.TabIndex = 42;
            // 
            // tpRegistros
            // 
            this.tpRegistros.Controls.Add(this.tsRanchos);
            this.tpRegistros.Controls.Add(this.dgvRegistros);
            this.tpRegistros.Location = new System.Drawing.Point(4, 29);
            this.tpRegistros.Name = "tpRegistros";
            this.tpRegistros.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegistros.Size = new System.Drawing.Size(488, 258);
            this.tpRegistros.TabIndex = 0;
            this.tpRegistros.Text = "Operadores";
            this.tpRegistros.UseVisualStyleBackColor = true;
            // 
            // tsRanchos
            // 
            this.tsRanchos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsRanchos.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.tsRanchos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSyncGrid});
            this.tsRanchos.Location = new System.Drawing.Point(3, 3);
            this.tsRanchos.MinimumSize = new System.Drawing.Size(0, 45);
            this.tsRanchos.Name = "tsRanchos";
            this.tsRanchos.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsRanchos.Size = new System.Drawing.Size(482, 45);
            this.tsRanchos.TabIndex = 5;
            this.tsRanchos.Text = "toolStrip1";
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
            // dgvRegistros
            // 
            this.dgvRegistros.AllowUserToAddRows = false;
            this.dgvRegistros.AllowUserToDeleteRows = false;
            this.dgvRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistros.Location = new System.Drawing.Point(0, 51);
            this.dgvRegistros.Name = "dgvRegistros";
            this.dgvRegistros.ReadOnly = true;
            this.dgvRegistros.Size = new System.Drawing.Size(488, 207);
            this.dgvRegistros.TabIndex = 0;
            this.dgvRegistros.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRegistros_CellContentClick);
            this.dgvRegistros.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRegistros_CellDoubleClick);
            this.dgvRegistros.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRegistros_ColumnHeaderMouseClick);
            // 
            // dtpFechaCompra
            // 
            this.dtpFechaCompra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaCompra.Location = new System.Drawing.Point(166, 48);
            this.dtpFechaCompra.Name = "dtpFechaCompra";
            this.dtpFechaCompra.Size = new System.Drawing.Size(125, 26);
            this.dtpFechaCompra.TabIndex = 1;
            // 
            // dtpVigencia
            // 
            this.dtpVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVigencia.Location = new System.Drawing.Point(380, 48);
            this.dtpVigencia.Name = "dtpVigencia";
            this.dtpVigencia.Size = new System.Drawing.Size(125, 26);
            this.dtpVigencia.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 50);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 47;
            this.label7.Text = "Vigencia:";
            // 
            // cbDestinoResiduos
            // 
            this.cbDestinoResiduos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestinoResiduos.Enabled = false;
            this.cbDestinoResiduos.FormattingEnabled = true;
            this.cbDestinoResiduos.Location = new System.Drawing.Point(166, 79);
            this.cbDestinoResiduos.Name = "cbDestinoResiduos";
            this.cbDestinoResiduos.Size = new System.Drawing.Size(339, 28);
            this.cbDestinoResiduos.TabIndex = 3;
            this.cbDestinoResiduos.SelectedIndexChanged += new System.EventHandler(this.cbDestinoResiduos_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 82);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 20);
            this.label6.TabIndex = 45;
            this.label6.Text = "Destino de residuos:";
            // 
            // LoteFoliosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 478);
            this.Controls.Add(this.dtpVigencia);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbDestinoResiduos);
            this.Controls.Add(this.dtpFechaCompra);
            this.Controls.Add(this.tcVariedades);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.txtFolioFinal);
            this.Controls.Add(this.txtFolioInicio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbEstado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFechaInicial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tsMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LoteFoliosView";
            this.Text = "Lote de Folios";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tcVariedades.ResumeLayout(false);
            this.tpRegistros.ResumeLayout(false);
            this.tpRegistros.PerformLayout();
            this.tsRanchos.ResumeLayout(false);
            this.tsRanchos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistros)).EndInit();
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
        private System.Windows.Forms.ComboBox cbEstado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFolioInicio;
        private System.Windows.Forms.TextBox txtFolioFinal;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TabControl tcVariedades;
        private System.Windows.Forms.TabPage tpRegistros;
        private System.Windows.Forms.ToolStrip tsRanchos;
        private System.Windows.Forms.ToolStripButton tsbSyncGrid;
        private System.Windows.Forms.DataGridView dgvRegistros;
        private System.Windows.Forms.DateTimePicker dtpFechaCompra;
        private System.Windows.Forms.DateTimePicker dtpVigencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbDestinoResiduos;
        private System.Windows.Forms.Label label6;
    }
}