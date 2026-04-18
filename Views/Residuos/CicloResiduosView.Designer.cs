namespace FresamLogistica.Views.Residuos
{
    partial class CicloResiduosView
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
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.cbEstado = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.lblLimiteInferior = new System.Windows.Forms.Label();
            this.lblGrados = new System.Windows.Forms.Label();
            this.txtCiclo = new System.Windows.Forms.TextBox();
            this.txtProxRevision = new System.Windows.Forms.TextBox();
            this.txtFechaEmision = new System.Windows.Forms.TextBox();
            this.txtEdicion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.tsMenu.Size = new System.Drawing.Size(458, 45);
            this.tsMenu.TabIndex = 33;
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
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(136, 80);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(120, 26);
            this.dtpFechaInicio.TabIndex = 3;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(329, 83);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(120, 26);
            this.dtpFechaFin.TabIndex = 41;
            this.dtpFechaFin.Visible = false;
            // 
            // cbEstado
            // 
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(329, 48);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(117, 28);
            this.cbEstado.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(263, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "Estado:";
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Location = new System.Drawing.Point(13, 83);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(97, 20);
            this.lblFechaInicial.TabIndex = 35;
            this.lblFechaInicial.Text = "Fecha inicio:";
            // 
            // lblLimiteInferior
            // 
            this.lblLimiteInferior.AutoSize = true;
            this.lblLimiteInferior.Location = new System.Drawing.Point(263, 83);
            this.lblLimiteInferior.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLimiteInferior.Name = "lblLimiteInferior";
            this.lblLimiteInferior.Size = new System.Drawing.Size(79, 20);
            this.lblLimiteInferior.TabIndex = 36;
            this.lblLimiteInferior.Text = "Fecha fin:";
            this.lblLimiteInferior.Visible = false;
            // 
            // lblGrados
            // 
            this.lblGrados.AutoSize = true;
            this.lblGrados.Enabled = false;
            this.lblGrados.Location = new System.Drawing.Point(13, 51);
            this.lblGrados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrados.Name = "lblGrados";
            this.lblGrados.Size = new System.Drawing.Size(47, 20);
            this.lblGrados.TabIndex = 38;
            this.lblGrados.Text = "Ciclo:";
            // 
            // txtCiclo
            // 
            this.txtCiclo.Enabled = false;
            this.txtCiclo.Location = new System.Drawing.Point(136, 48);
            this.txtCiclo.Name = "txtCiclo";
            this.txtCiclo.Size = new System.Drawing.Size(120, 26);
            this.txtCiclo.TabIndex = 1;
            // 
            // txtProxRevision
            // 
            this.txtProxRevision.Location = new System.Drawing.Point(136, 115);
            this.txtProxRevision.Name = "txtProxRevision";
            this.txtProxRevision.Size = new System.Drawing.Size(310, 26);
            this.txtProxRevision.TabIndex = 4;
            // 
            // txtFechaEmision
            // 
            this.txtFechaEmision.Location = new System.Drawing.Point(136, 147);
            this.txtFechaEmision.Name = "txtFechaEmision";
            this.txtFechaEmision.Size = new System.Drawing.Size(310, 26);
            this.txtFechaEmision.TabIndex = 5;
            // 
            // txtEdicion
            // 
            this.txtEdicion.Location = new System.Drawing.Point(136, 179);
            this.txtEdicion.Name = "txtEdicion";
            this.txtEdicion.Size = new System.Drawing.Size(310, 26);
            this.txtEdicion.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 147);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 45;
            this.label2.Text = "Fecha Emisión:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 179);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 46;
            this.label3.Text = "Edición:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 47;
            this.label4.Text = "Prox. Revisión:";
            // 
            // CicloView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 213);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEdicion);
            this.Controls.Add(this.txtFechaEmision);
            this.Controls.Add(this.txtProxRevision);
            this.Controls.Add(this.txtCiclo);
            this.Controls.Add(this.tsMenu);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.cbEstado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFechaInicial);
            this.Controls.Add(this.lblLimiteInferior);
            this.Controls.Add(this.lblGrados);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CicloView";
            this.Tag = "2";
            this.Text = "Ciclo de Residuos";
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
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.ComboBox cbEstado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.Label lblLimiteInferior;
        private System.Windows.Forms.Label lblGrados;
        private System.Windows.Forms.TextBox txtCiclo;
        private System.Windows.Forms.TextBox txtProxRevision;
        private System.Windows.Forms.TextBox txtFechaEmision;
        private System.Windows.Forms.TextBox txtEdicion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}