namespace FresamLogistica.Views.General
{
    partial class ProduccionView
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
            this.lblLimiteInferior = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.cbFrutas = new System.Windows.Forms.ComboBox();
            this.lblGrados = new System.Windows.Forms.Label();
            this.tsMenu.SuspendLayout();
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
            this.tsMenu.Size = new System.Drawing.Size(270, 45);
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
            // lblLimiteInferior
            // 
            this.lblLimiteInferior.AutoSize = true;
            this.lblLimiteInferior.Location = new System.Drawing.Point(9, 85);
            this.lblLimiteInferior.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLimiteInferior.Name = "lblLimiteInferior";
            this.lblLimiteInferior.Size = new System.Drawing.Size(58, 20);
            this.lblLimiteInferior.TabIndex = 27;
            this.lblLimiteInferior.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(85, 82);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(168, 26);
            this.dtpFecha.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Cantidad:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(85, 114);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(168, 26);
            this.txtCantidad.TabIndex = 31;
            // 
            // cbFrutas
            // 
            this.cbFrutas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrutas.Enabled = false;
            this.cbFrutas.FormattingEnabled = true;
            this.cbFrutas.Location = new System.Drawing.Point(85, 48);
            this.cbFrutas.Name = "cbFrutas";
            this.cbFrutas.Size = new System.Drawing.Size(168, 28);
            this.cbFrutas.TabIndex = 34;
            // 
            // lblGrados
            // 
            this.lblGrados.AutoSize = true;
            this.lblGrados.Enabled = false;
            this.lblGrados.Location = new System.Drawing.Point(9, 51);
            this.lblGrados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrados.Name = "lblGrados";
            this.lblGrados.Size = new System.Drawing.Size(51, 20);
            this.lblGrados.TabIndex = 33;
            this.lblGrados.Text = "Fruta:";
            // 
            // ProduccionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 160);
            this.Controls.Add(this.cbFrutas);
            this.Controls.Add(this.lblGrados);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFechaInicial);
            this.Controls.Add(this.lblLimiteInferior);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tsMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ProduccionView";
            this.Text = "Tipo de Destino de Residuo";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
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
        private System.Windows.Forms.Label lblLimiteInferior;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.ComboBox cbFrutas;
        private System.Windows.Forms.Label lblGrados;
    }
}