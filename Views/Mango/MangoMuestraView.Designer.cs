namespace FresamLogistica.Views.Mango
{
    partial class MangoMuestraView
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
            this.tpCalidadDescuento = new System.Windows.Forms.TabPage();
            this.txtPorcentajeMaduro = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.chkMateria = new System.Windows.Forms.CheckBox();
            this.chkAceptado = new System.Windows.Forms.CheckBox();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.txtDesecho = new System.Windows.Forms.TextBox();
            this.txtPesoCaja = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dgvCalidades = new System.Windows.Forms.DataGridView();
            this.tcRecepcion = new System.Windows.Forms.TabControl();
            this.cbVerifica = new System.Windows.Forms.ComboBox();
            this.cbElabora = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtPorcentajeMangoChico = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tsMenu.SuspendLayout();
            this.tpCalidadDescuento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalidades)).BeginInit();
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
            this.tsbEliminar});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.MinimumSize = new System.Drawing.Size(0, 45);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMenu.Size = new System.Drawing.Size(986, 45);
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
            // tpCalidadDescuento
            // 
            this.tpCalidadDescuento.Controls.Add(this.txtPorcentajeMangoChico);
            this.tpCalidadDescuento.Controls.Add(this.label2);
            this.tpCalidadDescuento.Controls.Add(this.txtPorcentajeMaduro);
            this.tpCalidadDescuento.Controls.Add(this.label33);
            this.tpCalidadDescuento.Controls.Add(this.label1);
            this.tpCalidadDescuento.Controls.Add(this.lblFechaInicial);
            this.tpCalidadDescuento.Controls.Add(this.dtpFecha);
            this.tpCalidadDescuento.Controls.Add(this.label17);
            this.tpCalidadDescuento.Controls.Add(this.txtObservaciones);
            this.tpCalidadDescuento.Controls.Add(this.chkMateria);
            this.tpCalidadDescuento.Controls.Add(this.chkAceptado);
            this.tpCalidadDescuento.Controls.Add(this.txtPorcentaje);
            this.tpCalidadDescuento.Controls.Add(this.txtDesecho);
            this.tpCalidadDescuento.Controls.Add(this.txtPesoCaja);
            this.tpCalidadDescuento.Controls.Add(this.label15);
            this.tpCalidadDescuento.Controls.Add(this.label14);
            this.tpCalidadDescuento.Controls.Add(this.dgvCalidades);
            this.tpCalidadDescuento.Location = new System.Drawing.Point(4, 29);
            this.tpCalidadDescuento.Name = "tpCalidadDescuento";
            this.tpCalidadDescuento.Size = new System.Drawing.Size(954, 324);
            this.tpCalidadDescuento.TabIndex = 3;
            this.tpCalidadDescuento.Text = "Muestra";
            this.tpCalidadDescuento.UseVisualStyleBackColor = true;
            // 
            // txtPorcentajeMaduro
            // 
            this.txtPorcentajeMaduro.Location = new System.Drawing.Point(584, 119);
            this.txtPorcentajeMaduro.Name = "txtPorcentajeMaduro";
            this.txtPorcentajeMaduro.Size = new System.Drawing.Size(162, 26);
            this.txtPorcentajeMaduro.TabIndex = 6;
            this.txtPorcentajeMaduro.Text = "0";
            this.txtPorcentajeMaduro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(457, 122);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(85, 20);
            this.label33.TabIndex = 81;
            this.label33.Text = "% Maduro:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(457, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 79;
            this.label1.Text = "% Descuento:";
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Location = new System.Drawing.Point(457, 8);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(58, 20);
            this.lblFechaInicial.TabIndex = 65;
            this.lblFechaInicial.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(584, 3);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(162, 26);
            this.dtpFecha.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(457, 177);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(118, 20);
            this.label17.TabIndex = 59;
            this.label17.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObservaciones.Location = new System.Drawing.Point(456, 200);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(495, 119);
            this.txtObservaciones.TabIndex = 10;
            // 
            // chkMateria
            // 
            this.chkMateria.AutoSize = true;
            this.chkMateria.Location = new System.Drawing.Point(752, 36);
            this.chkMateria.Name = "chkMateria";
            this.chkMateria.Size = new System.Drawing.Size(140, 24);
            this.chkMateria.TabIndex = 9;
            this.chkMateria.Text = "Materia Extraña";
            this.chkMateria.UseVisualStyleBackColor = true;
            // 
            // chkAceptado
            // 
            this.chkAceptado.AutoSize = true;
            this.chkAceptado.Location = new System.Drawing.Point(752, 7);
            this.chkAceptado.Name = "chkAceptado";
            this.chkAceptado.Size = new System.Drawing.Size(97, 24);
            this.chkAceptado.TabIndex = 8;
            this.chkAceptado.Text = "Aceptado";
            this.chkAceptado.UseVisualStyleBackColor = true;
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.Enabled = false;
            this.txtPorcentaje.Location = new System.Drawing.Point(584, 148);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Size = new System.Drawing.Size(162, 26);
            this.txtPorcentaje.TabIndex = 7;
            this.txtPorcentaje.Text = "0";
            this.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDesecho
            // 
            this.txtDesecho.Enabled = false;
            this.txtDesecho.Location = new System.Drawing.Point(584, 61);
            this.txtDesecho.Name = "txtDesecho";
            this.txtDesecho.Size = new System.Drawing.Size(162, 26);
            this.txtDesecho.TabIndex = 4;
            this.txtDesecho.Text = "0";
            this.txtDesecho.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDesecho.TextChanged += new System.EventHandler(this.txtDesecho_TextChanged);
            // 
            // txtPesoCaja
            // 
            this.txtPesoCaja.Location = new System.Drawing.Point(584, 32);
            this.txtPesoCaja.Name = "txtPesoCaja";
            this.txtPesoCaja.Size = new System.Drawing.Size(162, 26);
            this.txtPesoCaja.TabIndex = 3;
            this.txtPesoCaja.Text = "0";
            this.txtPesoCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPesoCaja.TextChanged += new System.EventHandler(this.txtPesoCaja_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(457, 64);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 20);
            this.label15.TabIndex = 55;
            this.label15.Text = "Desecho:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(457, 35);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 20);
            this.label14.TabIndex = 55;
            this.label14.Text = "Peso por caja:";
            // 
            // dgvCalidades
            // 
            this.dgvCalidades.AllowUserToAddRows = false;
            this.dgvCalidades.AllowUserToDeleteRows = false;
            this.dgvCalidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvCalidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalidades.Location = new System.Drawing.Point(0, 0);
            this.dgvCalidades.Name = "dgvCalidades";
            this.dgvCalidades.ReadOnly = true;
            this.dgvCalidades.Size = new System.Drawing.Size(450, 321);
            this.dgvCalidades.TabIndex = 1;
            this.dgvCalidades.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalidades_CellEndEdit);
            this.dgvCalidades.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvCalidades_CellValidating);
            // 
            // tcRecepcion
            // 
            this.tcRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcRecepcion.Controls.Add(this.tpCalidadDescuento);
            this.tcRecepcion.Location = new System.Drawing.Point(12, 113);
            this.tcRecepcion.Name = "tcRecepcion";
            this.tcRecepcion.SelectedIndex = 0;
            this.tcRecepcion.Size = new System.Drawing.Size(962, 357);
            this.tcRecepcion.TabIndex = 15;
            // 
            // cbVerifica
            // 
            this.cbVerifica.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVerifica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVerifica.FormattingEnabled = true;
            this.cbVerifica.Location = new System.Drawing.Point(120, 79);
            this.cbVerifica.Name = "cbVerifica";
            this.cbVerifica.Size = new System.Drawing.Size(854, 28);
            this.cbVerifica.TabIndex = 66;
            // 
            // cbElabora
            // 
            this.cbElabora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbElabora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbElabora.FormattingEnabled = true;
            this.cbElabora.Location = new System.Drawing.Point(120, 48);
            this.cbElabora.Name = "cbElabora";
            this.cbElabora.Size = new System.Drawing.Size(854, 28);
            this.cbElabora.TabIndex = 65;
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
            // txtPorcentajeMangoChico
            // 
            this.txtPorcentajeMangoChico.Enabled = false;
            this.txtPorcentajeMangoChico.Location = new System.Drawing.Point(584, 90);
            this.txtPorcentajeMangoChico.Name = "txtPorcentajeMangoChico";
            this.txtPorcentajeMangoChico.Size = new System.Drawing.Size(162, 26);
            this.txtPorcentajeMangoChico.TabIndex = 5;
            this.txtPorcentajeMangoChico.Text = "0";
            this.txtPorcentajeMangoChico.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(457, 93);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 83;
            this.label2.Text = "% Mango Chico:";
            // 
            // MangoMuestraView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 482);
            this.Controls.Add(this.cbVerifica);
            this.Controls.Add(this.cbElabora);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.tcRecepcion);
            this.Controls.Add(this.tsMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MangoMuestraView";
            this.Tag = "35";
            this.Text = "Muestra";
            this.Load += new System.EventHandler(this.RecepcionView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tpCalidadDescuento.ResumeLayout(false);
            this.tpCalidadDescuento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalidades)).EndInit();
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
        private System.Windows.Forms.TabPage tpCalidadDescuento;
        private System.Windows.Forms.CheckBox chkAceptado;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.TextBox txtDesecho;
        private System.Windows.Forms.TextBox txtPesoCaja;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dgvCalidades;
        private System.Windows.Forms.TabControl tcRecepcion;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.CheckBox chkMateria;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtPorcentajeMaduro;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbVerifica;
        private System.Windows.Forms.ComboBox cbElabora;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtPorcentajeMangoChico;
        private System.Windows.Forms.Label label2;
    }
}