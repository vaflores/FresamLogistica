namespace FresamLogistica.Views.Reportes
{
    partial class MangoPorcentajeViajesProcesadosView
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
            this.btnGenerar = new System.Windows.Forms.Button();
            this.cbCiclos = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkMostrarViajesSinSaldo = new System.Windows.Forms.CheckBox();
            this.chkMostrarDetalleProcesado = new System.Windows.Forms.CheckBox();
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
            this.tsMenu.Size = new System.Drawing.Size(511, 45);
            this.tsMenu.TabIndex = 37;
            this.tsMenu.Text = "tsMenu";
            // 
            // tsbBuscar
            // 
            this.tsbBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBuscar.Enabled = false;
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
            this.tsbCrear.Enabled = false;
            this.tsbCrear.Image = global::FresamLogistica.Properties.Resources.create_50;
            this.tsbCrear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCrear.Name = "tsbCrear";
            this.tsbCrear.Size = new System.Drawing.Size(34, 42);
            this.tsbCrear.Text = "Crear";
            this.tsbCrear.Click += new System.EventHandler(this.tsbNuevo_Click);
            // 
            // tsbGuardar
            // 
            this.tsbGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGuardar.Enabled = false;
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
            this.tsbEliminar.Enabled = false;
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
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(416, 48);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(83, 60);
            this.btnGenerar.TabIndex = 42;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // cbCiclos
            // 
            this.cbCiclos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCiclos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCiclos.FormattingEnabled = true;
            this.cbCiclos.Location = new System.Drawing.Point(62, 48);
            this.cbCiclos.Name = "cbCiclos";
            this.cbCiclos.Size = new System.Drawing.Size(348, 28);
            this.cbCiclos.TabIndex = 43;
            this.cbCiclos.SelectedIndexChanged += new System.EventHandler(this.cbCiclos_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 44;
            this.label4.Text = "Ciclo:";
            // 
            // chkMostrarViajesSinSaldo
            // 
            this.chkMostrarViajesSinSaldo.AutoSize = true;
            this.chkMostrarViajesSinSaldo.Location = new System.Drawing.Point(17, 84);
            this.chkMostrarViajesSinSaldo.Name = "chkMostrarViajesSinSaldo";
            this.chkMostrarViajesSinSaldo.Size = new System.Drawing.Size(272, 24);
            this.chkMostrarViajesSinSaldo.TabIndex = 45;
            this.chkMostrarViajesSinSaldo.Text = "Mostrar viajes con saldo pendiente";
            this.chkMostrarViajesSinSaldo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMostrarViajesSinSaldo.UseVisualStyleBackColor = true;
            // 
            // chkMostrarDetalleProcesado
            // 
            this.chkMostrarDetalleProcesado.AutoSize = true;
            this.chkMostrarDetalleProcesado.Location = new System.Drawing.Point(17, 114);
            this.chkMostrarDetalleProcesado.Name = "chkMostrarDetalleProcesado";
            this.chkMostrarDetalleProcesado.Size = new System.Drawing.Size(216, 24);
            this.chkMostrarDetalleProcesado.TabIndex = 46;
            this.chkMostrarDetalleProcesado.Text = "Mostrar Detalle Procesado";
            this.chkMostrarDetalleProcesado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMostrarDetalleProcesado.UseVisualStyleBackColor = true;
            // 
            // MangoPorcentajeViajesProcesadosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 147);
            this.Controls.Add(this.chkMostrarDetalleProcesado);
            this.Controls.Add(this.chkMostrarViajesSinSaldo);
            this.Controls.Add(this.cbCiclos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tsMenu);
            this.Controls.Add(this.btnGenerar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MangoPorcentajeViajesProcesadosView";
            this.Tag = "11";
            this.Text = "Porcentaje de Viajes Procesados";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EmpresaView_KeyDown);
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
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.ComboBox cbCiclos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkMostrarViajesSinSaldo;
        private System.Windows.Forms.CheckBox chkMostrarDetalleProcesado;
    }
}