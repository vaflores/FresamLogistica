namespace FresamLogistica.Views.Residuos
{
    partial class RecoleccionResiduosView
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
            this.cbEmpresa = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRecoleccion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbEstado = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label22 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbOperador = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbDestino = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.tpResiduos = new System.Windows.Forms.TabPage();
            this.tsMenuResiduos = new System.Windows.Forms.ToolStrip();
            this.tsbSyncGridResiduos = new System.Windows.Forms.ToolStripButton();
            this.tsbAddGridResiduos = new System.Windows.Forms.ToolStripButton();
            this.tsbDelGridResiduos = new System.Windows.Forms.ToolStripButton();
            this.dgvResiduos = new System.Windows.Forms.DataGridView();
            this.txtMarcaModelo = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtPlacas = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tcRecepcion = new System.Windows.Forms.TabControl();
            this.tpFolios = new System.Windows.Forms.TabPage();
            this.tsbSyncGridFolios = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbAddGridFolios = new System.Windows.Forms.ToolStripButton();
            this.tsbDelGridFolios = new System.Windows.Forms.ToolStripButton();
            this.dgvFolios = new System.Windows.Forms.DataGridView();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantFolios = new System.Windows.Forms.TextBox();
            this.tsbImprimir = new System.Windows.Forms.ToolStripButton();
            this.tsMenu.SuspendLayout();
            this.tpResiduos.SuspendLayout();
            this.tsMenuResiduos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResiduos)).BeginInit();
            this.tcRecepcion.SuspendLayout();
            this.tpFolios.SuspendLayout();
            this.tsbSyncGridFolios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolios)).BeginInit();
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
            this.tsMenu.Size = new System.Drawing.Size(659, 45);
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
            // cbEmpresa
            // 
            this.cbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmpresa.FormattingEnabled = true;
            this.cbEmpresa.Location = new System.Drawing.Point(127, 78);
            this.cbEmpresa.Name = "cbEmpresa";
            this.cbEmpresa.Size = new System.Drawing.Size(319, 28);
            this.cbEmpresa.TabIndex = 5;
            this.cbEmpresa.SelectedIndexChanged += new System.EventHandler(this.cbEmpresa_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 112);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 45;
            this.label5.Text = "Operador:";
            // 
            // txtRecoleccion
            // 
            this.txtRecoleccion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRecoleccion.Location = new System.Drawing.Point(127, 48);
            this.txtRecoleccion.Name = "txtRecoleccion";
            this.txtRecoleccion.ReadOnly = true;
            this.txtRecoleccion.Size = new System.Drawing.Size(125, 26);
            this.txtRecoleccion.TabIndex = 2;
            this.txtRecoleccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 51);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 54;
            this.label8.Text = "Recepción:";
            // 
            // cbEstado
            // 
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(519, 48);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(125, 28);
            this.cbEstado.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(453, 51);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 20);
            this.label7.TabIndex = 56;
            this.label7.Text = "Estatus:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(321, 48);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(125, 26);
            this.dtpFecha.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(256, 51);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 20);
            this.label22.TabIndex = 65;
            this.label22.Text = "Fecha:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 81);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 45;
            this.label6.Text = "Empresa:";
            // 
            // cbOperador
            // 
            this.cbOperador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperador.FormattingEnabled = true;
            this.cbOperador.Location = new System.Drawing.Point(127, 109);
            this.cbOperador.Name = "cbOperador";
            this.cbOperador.Size = new System.Drawing.Size(319, 28);
            this.cbOperador.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 143);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 20);
            this.label9.TabIndex = 45;
            this.label9.Text = "Destino:";
            // 
            // cbDestino
            // 
            this.cbDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestino.FormattingEnabled = true;
            this.cbDestino.Location = new System.Drawing.Point(127, 140);
            this.cbDestino.Name = "cbDestino";
            this.cbDestino.Size = new System.Drawing.Size(319, 28);
            this.cbDestino.TabIndex = 7;
            this.cbDestino.SelectedIndexChanged += new System.EventHandler(this.cbDestino_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(453, 143);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 20);
            this.label18.TabIndex = 56;
            this.label18.Text = "Folios:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(13, 232);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(118, 20);
            this.label19.TabIndex = 54;
            this.label19.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(127, 229);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(319, 26);
            this.txtObservaciones.TabIndex = 12;
            this.txtObservaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tpResiduos
            // 
            this.tpResiduos.Controls.Add(this.tsMenuResiduos);
            this.tpResiduos.Controls.Add(this.dgvResiduos);
            this.tpResiduos.Location = new System.Drawing.Point(4, 29);
            this.tpResiduos.Name = "tpResiduos";
            this.tpResiduos.Size = new System.Drawing.Size(625, 178);
            this.tpResiduos.TabIndex = 3;
            this.tpResiduos.Text = "Residuos";
            this.tpResiduos.UseVisualStyleBackColor = true;
            // 
            // tsMenuResiduos
            // 
            this.tsMenuResiduos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenuResiduos.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.tsMenuResiduos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSyncGridResiduos,
            this.tsbAddGridResiduos,
            this.tsbDelGridResiduos});
            this.tsMenuResiduos.Location = new System.Drawing.Point(0, 0);
            this.tsMenuResiduos.MinimumSize = new System.Drawing.Size(0, 45);
            this.tsMenuResiduos.Name = "tsMenuResiduos";
            this.tsMenuResiduos.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMenuResiduos.Size = new System.Drawing.Size(625, 45);
            this.tsMenuResiduos.TabIndex = 8;
            this.tsMenuResiduos.Text = "toolStrip1";
            // 
            // tsbSyncGridResiduos
            // 
            this.tsbSyncGridResiduos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSyncGridResiduos.Image = global::FresamLogistica.Properties.Resources.sync_50;
            this.tsbSyncGridResiduos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSyncGridResiduos.Name = "tsbSyncGridResiduos";
            this.tsbSyncGridResiduos.Size = new System.Drawing.Size(34, 42);
            this.tsbSyncGridResiduos.Text = "Buscar";
            this.tsbSyncGridResiduos.Visible = false;
            // 
            // tsbAddGridResiduos
            // 
            this.tsbAddGridResiduos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddGridResiduos.Image = global::FresamLogistica.Properties.Resources.create_50;
            this.tsbAddGridResiduos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddGridResiduos.Name = "tsbAddGridResiduos";
            this.tsbAddGridResiduos.Size = new System.Drawing.Size(34, 42);
            this.tsbAddGridResiduos.Text = "Crear";
            this.tsbAddGridResiduos.Click += new System.EventHandler(this.tsbAddGrid_Click);
            // 
            // tsbDelGridResiduos
            // 
            this.tsbDelGridResiduos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelGridResiduos.Image = global::FresamLogistica.Properties.Resources.remove_50;
            this.tsbDelGridResiduos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelGridResiduos.Name = "tsbDelGridResiduos";
            this.tsbDelGridResiduos.Size = new System.Drawing.Size(34, 42);
            this.tsbDelGridResiduos.Text = "Eliminar";
            this.tsbDelGridResiduos.Click += new System.EventHandler(this.tsbDelGrid_Click);
            // 
            // dgvResiduos
            // 
            this.dgvResiduos.AllowUserToAddRows = false;
            this.dgvResiduos.AllowUserToDeleteRows = false;
            this.dgvResiduos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResiduos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResiduos.Location = new System.Drawing.Point(0, 48);
            this.dgvResiduos.Name = "dgvResiduos";
            this.dgvResiduos.ReadOnly = true;
            this.dgvResiduos.Size = new System.Drawing.Size(625, 130);
            this.dgvResiduos.TabIndex = 1;
            // 
            // txtMarcaModelo
            // 
            this.txtMarcaModelo.Location = new System.Drawing.Point(127, 200);
            this.txtMarcaModelo.Name = "txtMarcaModelo";
            this.txtMarcaModelo.Size = new System.Drawing.Size(319, 26);
            this.txtMarcaModelo.TabIndex = 11;
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(321, 171);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(125, 26);
            this.txtColor.TabIndex = 10;
            // 
            // txtPlacas
            // 
            this.txtPlacas.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlacas.Location = new System.Drawing.Point(127, 171);
            this.txtPlacas.Name = "txtPlacas";
            this.txtPlacas.Size = new System.Drawing.Size(125, 26);
            this.txtPlacas.TabIndex = 9;
            this.txtPlacas.TextChanged += new System.EventHandler(this.txtPlacas_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 203);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 20);
            this.label13.TabIndex = 57;
            this.label13.Text = "Marca/Modelo:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(256, 174);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 20);
            this.label12.TabIndex = 57;
            this.label12.Text = "Color:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 174);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 20);
            this.label10.TabIndex = 56;
            this.label10.Text = "Placas:";
            // 
            // tcRecepcion
            // 
            this.tcRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcRecepcion.Controls.Add(this.tpResiduos);
            this.tcRecepcion.Controls.Add(this.tpFolios);
            this.tcRecepcion.Location = new System.Drawing.Point(12, 261);
            this.tcRecepcion.Name = "tcRecepcion";
            this.tcRecepcion.SelectedIndex = 0;
            this.tcRecepcion.Size = new System.Drawing.Size(633, 211);
            this.tcRecepcion.TabIndex = 15;
            // 
            // tpFolios
            // 
            this.tpFolios.Controls.Add(this.tsbSyncGridFolios);
            this.tpFolios.Controls.Add(this.dgvFolios);
            this.tpFolios.Location = new System.Drawing.Point(4, 29);
            this.tpFolios.Name = "tpFolios";
            this.tpFolios.Size = new System.Drawing.Size(625, 178);
            this.tpFolios.TabIndex = 4;
            this.tpFolios.Text = "Folios";
            this.tpFolios.UseVisualStyleBackColor = true;
            // 
            // tsbSyncGridFolios
            // 
            this.tsbSyncGridFolios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbSyncGridFolios.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.tsbSyncGridFolios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbAddGridFolios,
            this.tsbDelGridFolios});
            this.tsbSyncGridFolios.Location = new System.Drawing.Point(0, 0);
            this.tsbSyncGridFolios.MinimumSize = new System.Drawing.Size(0, 45);
            this.tsbSyncGridFolios.Name = "tsbSyncGridFolios";
            this.tsbSyncGridFolios.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsbSyncGridFolios.Size = new System.Drawing.Size(625, 45);
            this.tsbSyncGridFolios.TabIndex = 10;
            this.tsbSyncGridFolios.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::FresamLogistica.Properties.Resources.sync_50;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(34, 42);
            this.toolStripButton1.Text = "Buscar";
            this.toolStripButton1.Visible = false;
            // 
            // tsbAddGridFolios
            // 
            this.tsbAddGridFolios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddGridFolios.Image = global::FresamLogistica.Properties.Resources.create_50;
            this.tsbAddGridFolios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddGridFolios.Name = "tsbAddGridFolios";
            this.tsbAddGridFolios.Size = new System.Drawing.Size(34, 42);
            this.tsbAddGridFolios.Text = "Crear";
            this.tsbAddGridFolios.Click += new System.EventHandler(this.tsbAddGridFolios_Click);
            // 
            // tsbDelGridFolios
            // 
            this.tsbDelGridFolios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelGridFolios.Image = global::FresamLogistica.Properties.Resources.remove_50;
            this.tsbDelGridFolios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelGridFolios.Name = "tsbDelGridFolios";
            this.tsbDelGridFolios.Size = new System.Drawing.Size(34, 42);
            this.tsbDelGridFolios.Text = "Eliminar";
            this.tsbDelGridFolios.Click += new System.EventHandler(this.tsbDelGridFolios_Click);
            // 
            // dgvFolios
            // 
            this.dgvFolios.AllowUserToAddRows = false;
            this.dgvFolios.AllowUserToDeleteRows = false;
            this.dgvFolios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFolios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFolios.Location = new System.Drawing.Point(0, 48);
            this.dgvFolios.Name = "dgvFolios";
            this.dgvFolios.ReadOnly = true;
            this.dgvFolios.Size = new System.Drawing.Size(625, 130);
            this.dgvFolios.TabIndex = 9;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(519, 229);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(125, 26);
            this.txtTotal.TabIndex = 13;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(453, 232);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 67;
            this.label1.Text = "Total:";
            // 
            // txtCantFolios
            // 
            this.txtCantFolios.Location = new System.Drawing.Point(519, 140);
            this.txtCantFolios.Name = "txtCantFolios";
            this.txtCantFolios.Size = new System.Drawing.Size(125, 26);
            this.txtCantFolios.TabIndex = 68;
            this.txtCantFolios.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tsbImprimir
            // 
            this.tsbImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImprimir.Image = global::FresamLogistica.Properties.Resources.print_50;
            this.tsbImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImprimir.Name = "tsbImprimir";
            this.tsbImprimir.Size = new System.Drawing.Size(34, 42);
            this.tsbImprimir.Text = "Imprimir";
            // 
            // RecoleccionResiduosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 484);
            this.Controls.Add(this.txtCantFolios);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMarcaModelo);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtPlacas);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cbEstado);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtRecoleccion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tcRecepcion);
            this.Controls.Add(this.tsMenu);
            this.Controls.Add(this.cbDestino);
            this.Controls.Add(this.cbOperador);
            this.Controls.Add(this.cbEmpresa);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RecoleccionResiduosView";
            this.Tag = "1";
            this.Text = "Recolección de Residuos";
            this.Load += new System.EventHandler(this.RecepcionView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tpResiduos.ResumeLayout(false);
            this.tpResiduos.PerformLayout();
            this.tsMenuResiduos.ResumeLayout(false);
            this.tsMenuResiduos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResiduos)).EndInit();
            this.tcRecepcion.ResumeLayout(false);
            this.tpFolios.ResumeLayout(false);
            this.tpFolios.PerformLayout();
            this.tsbSyncGridFolios.ResumeLayout(false);
            this.tsbSyncGridFolios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.ToolStripButton tsbCrear;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ComboBox cbEmpresa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRecoleccion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbEstado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbOperador;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbDestino;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.TabPage tpResiduos;
        private System.Windows.Forms.DataGridView dgvResiduos;
        private System.Windows.Forms.TextBox txtMarcaModelo;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.TextBox txtPlacas;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabControl tcRecepcion;
        private System.Windows.Forms.ToolStrip tsMenuResiduos;
        private System.Windows.Forms.ToolStripButton tsbSyncGridResiduos;
        private System.Windows.Forms.ToolStripButton tsbAddGridResiduos;
        private System.Windows.Forms.ToolStripButton tsbDelGridResiduos;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpFolios;
        private System.Windows.Forms.ToolStrip tsbSyncGridFolios;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbAddGridFolios;
        private System.Windows.Forms.ToolStripButton tsbDelGridFolios;
        private System.Windows.Forms.DataGridView dgvFolios;
        private System.Windows.Forms.TextBox txtCantFolios;
        private System.Windows.Forms.ToolStripButton tsbImprimir;
    }
}