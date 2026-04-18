namespace FresamLogistica.Views.Residuos
{
    partial class EmpresaGestoraResiduosView
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEsPersonaMoral = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.txtLicenciaSCT = new System.Windows.Forms.TextBox();
            this.txtLicenciaAmbiental = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tcVariedades = new System.Windows.Forms.TabControl();
            this.tpRegistros = new System.Windows.Forms.TabPage();
            this.tsRanchos = new System.Windows.Forms.ToolStrip();
            this.tsbSyncGrid = new System.Windows.Forms.ToolStripButton();
            this.tsbAddGrid = new System.Windows.Forms.ToolStripButton();
            this.dgvRegistros = new System.Windows.Forms.DataGridView();
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
            this.tsMenu.Size = new System.Drawing.Size(799, 45);
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
            this.cbEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(670, 48);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(117, 28);
            this.cbEstado.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(599, 51);
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
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.Location = new System.Drawing.Point(157, 48);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(436, 26);
            this.txtNombre.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 33;
            this.label3.Text = "RFC:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 33;
            this.label4.Text = "Licencia SCT:";
            // 
            // chkEsPersonaMoral
            // 
            this.chkEsPersonaMoral.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEsPersonaMoral.AutoSize = true;
            this.chkEsPersonaMoral.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEsPersonaMoral.Location = new System.Drawing.Point(599, 81);
            this.chkEsPersonaMoral.Name = "chkEsPersonaMoral";
            this.chkEsPersonaMoral.Size = new System.Drawing.Size(157, 24);
            this.chkEsPersonaMoral.TabIndex = 4;
            this.chkEsPersonaMoral.Text = "Es Persona Moral:";
            this.chkEsPersonaMoral.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 144);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "Licencia Ambiental:";
            // 
            // txtRFC
            // 
            this.txtRFC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRFC.Location = new System.Drawing.Point(157, 79);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(436, 26);
            this.txtRFC.TabIndex = 3;
            // 
            // txtLicenciaSCT
            // 
            this.txtLicenciaSCT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicenciaSCT.Location = new System.Drawing.Point(157, 110);
            this.txtLicenciaSCT.Name = "txtLicenciaSCT";
            this.txtLicenciaSCT.Size = new System.Drawing.Size(436, 26);
            this.txtLicenciaSCT.TabIndex = 5;
            // 
            // txtLicenciaAmbiental
            // 
            this.txtLicenciaAmbiental.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicenciaAmbiental.Location = new System.Drawing.Point(157, 141);
            this.txtLicenciaAmbiental.Name = "txtLicenciaAmbiental";
            this.txtLicenciaAmbiental.Size = new System.Drawing.Size(436, 26);
            this.txtLicenciaAmbiental.TabIndex = 6;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelefono.Location = new System.Drawing.Point(157, 172);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(436, 26);
            this.txtTelefono.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 175);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 40;
            this.label6.Text = "Teléfono:";
            // 
            // tcVariedades
            // 
            this.tcVariedades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcVariedades.Controls.Add(this.tpRegistros);
            this.tcVariedades.Location = new System.Drawing.Point(12, 204);
            this.tcVariedades.Name = "tcVariedades";
            this.tcVariedades.SelectedIndex = 0;
            this.tcVariedades.Size = new System.Drawing.Size(775, 262);
            this.tcVariedades.TabIndex = 42;
            // 
            // tpRegistros
            // 
            this.tpRegistros.Controls.Add(this.tsRanchos);
            this.tpRegistros.Controls.Add(this.dgvRegistros);
            this.tpRegistros.Location = new System.Drawing.Point(4, 29);
            this.tpRegistros.Name = "tpRegistros";
            this.tpRegistros.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegistros.Size = new System.Drawing.Size(767, 229);
            this.tpRegistros.TabIndex = 0;
            this.tpRegistros.Text = "Operadores";
            this.tpRegistros.UseVisualStyleBackColor = true;
            // 
            // tsRanchos
            // 
            this.tsRanchos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsRanchos.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.tsRanchos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSyncGrid,
            this.tsbAddGrid});
            this.tsRanchos.Location = new System.Drawing.Point(3, 3);
            this.tsRanchos.MinimumSize = new System.Drawing.Size(0, 45);
            this.tsRanchos.Name = "tsRanchos";
            this.tsRanchos.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsRanchos.Size = new System.Drawing.Size(761, 45);
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
            // dgvRegistros
            // 
            this.dgvRegistros.AllowUserToAddRows = false;
            this.dgvRegistros.AllowUserToDeleteRows = false;
            this.dgvRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistros.Location = new System.Drawing.Point(3, 51);
            this.dgvRegistros.Name = "dgvRegistros";
            this.dgvRegistros.ReadOnly = true;
            this.dgvRegistros.Size = new System.Drawing.Size(761, 175);
            this.dgvRegistros.TabIndex = 0;
            this.dgvRegistros.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRegistros_CellContentClick);
            this.dgvRegistros.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRegistros_CellDoubleClick);
            this.dgvRegistros.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRegistros_ColumnHeaderMouseClick);
            // 
            // EmpresaGestoraResiduosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 478);
            this.Controls.Add(this.tcVariedades);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtLicenciaAmbiental);
            this.Controls.Add(this.txtLicenciaSCT);
            this.Controls.Add(this.txtRFC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkEsPersonaMoral);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.cbEstado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFechaInicial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tsMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "EmpresaGestoraResiduosView";
            this.Text = "Empresa Gestora de Residuos";
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
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkEsPersonaMoral;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.TextBox txtLicenciaSCT;
        private System.Windows.Forms.TextBox txtLicenciaAmbiental;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tcVariedades;
        private System.Windows.Forms.TabPage tpRegistros;
        private System.Windows.Forms.ToolStrip tsRanchos;
        private System.Windows.Forms.ToolStripButton tsbSyncGrid;
        private System.Windows.Forms.ToolStripButton tsbAddGrid;
        private System.Windows.Forms.DataGridView dgvRegistros;
    }
}