namespace FresamLogistica.Views.Residuos
{
    partial class RecoleccionesResiduosView
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
            this.components = new System.ComponentModel.Container();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.tsbCrear = new System.Windows.Forms.ToolStripButton();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.tsbImprimir = new System.Windows.Forms.ToolStripButton();
            this.tcRegistros = new System.Windows.Forms.TabControl();
            this.tpRegistros = new System.Windows.Forms.TabPage();
            this.dgvRegistros = new System.Windows.Forms.DataGridView();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.crearNuevaRecepcionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.tsMenu.SuspendLayout();
            this.tcRegistros.SuspendLayout();
            this.tpRegistros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistros)).BeginInit();
            this.cmsMenu.SuspendLayout();
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
            this.tsMenu.Size = new System.Drawing.Size(984, 45);
            this.tsMenu.TabIndex = 7;
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
            this.tsbImprimir.Image = global::FresamLogistica.Properties.Resources.print_50;
            this.tsbImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImprimir.Name = "tsbImprimir";
            this.tsbImprimir.Size = new System.Drawing.Size(34, 42);
            this.tsbImprimir.Text = "Imprimir";
            this.tsbImprimir.Click += new System.EventHandler(this.tsbImprimir_Click);
            // 
            // tcRegistros
            // 
            this.tcRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcRegistros.Controls.Add(this.tpRegistros);
            this.tcRegistros.Location = new System.Drawing.Point(12, 82);
            this.tcRegistros.Name = "tcRegistros";
            this.tcRegistros.SelectedIndex = 0;
            this.tcRegistros.Size = new System.Drawing.Size(960, 367);
            this.tcRegistros.TabIndex = 8;
            // 
            // tpRegistros
            // 
            this.tpRegistros.Controls.Add(this.dgvRegistros);
            this.tpRegistros.Location = new System.Drawing.Point(4, 29);
            this.tpRegistros.Name = "tpRegistros";
            this.tpRegistros.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegistros.Size = new System.Drawing.Size(952, 334);
            this.tpRegistros.TabIndex = 0;
            this.tpRegistros.Text = "Registros";
            this.tpRegistros.UseVisualStyleBackColor = true;
            // 
            // dgvRegistros
            // 
            this.dgvRegistros.AllowUserToAddRows = false;
            this.dgvRegistros.AllowUserToDeleteRows = false;
            this.dgvRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistros.ContextMenuStrip = this.cmsMenu;
            this.dgvRegistros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRegistros.Location = new System.Drawing.Point(3, 3);
            this.dgvRegistros.Name = "dgvRegistros";
            this.dgvRegistros.ReadOnly = true;
            this.dgvRegistros.Size = new System.Drawing.Size(946, 328);
            this.dgvRegistros.TabIndex = 0;
            this.dgvRegistros.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRegistros_CellContentClick);
            this.dgvRegistros.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRegistros_CellDoubleClick);
            this.dgvRegistros.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRegistros_ColumnHeaderMouseClick);
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crearNuevaRecepcionToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(202, 26);
            // 
            // crearNuevaRecepcionToolStripMenuItem
            // 
            this.crearNuevaRecepcionToolStripMenuItem.Name = "crearNuevaRecepcionToolStripMenuItem";
            this.crearNuevaRecepcionToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.crearNuevaRecepcionToolStripMenuItem.Text = "Crear nueva recolección";
            this.crearNuevaRecepcionToolStripMenuItem.Click += new System.EventHandler(this.crearNuevaRecepcionToolStripMenuItem_Click);
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(80, 48);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(125, 26);
            this.dtpFecha.TabIndex = 37;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Location = new System.Drawing.Point(15, 51);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(58, 20);
            this.lblFechaInicial.TabIndex = 38;
            this.lblFechaInicial.Text = "Fecha:";
            // 
            // RecoleccionesResiduosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFechaInicial);
            this.Controls.Add(this.tsMenu);
            this.Controls.Add(this.tcRegistros);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RecoleccionesResiduosView";
            this.Tag = "27";
            this.Text = "Recolecciones de Residuos";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tcRegistros.ResumeLayout(false);
            this.tpRegistros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistros)).EndInit();
            this.cmsMenu.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tcRegistros;
        private System.Windows.Forms.TabPage tpRegistros;
        private System.Windows.Forms.DataGridView dgvRegistros;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem crearNuevaRecepcionToolStripMenuItem;
    }
}