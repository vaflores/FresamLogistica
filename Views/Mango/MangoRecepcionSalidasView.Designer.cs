namespace FresamLogistica.Views.Mango
{
    partial class MangoRecepcionSalidasView
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
            this.tpTraspaleos = new System.Windows.Forms.TabPage();
            this.tsOpciones = new System.Windows.Forms.ToolStrip();
            this.tsbSyncGrid = new System.Windows.Forms.ToolStripButton();
            this.tsbAddGrid = new System.Windows.Forms.ToolStripButton();
            this.tsbDelGrid = new System.Windows.Forms.ToolStripButton();
            this.dgvSalidas = new System.Windows.Forms.DataGridView();
            this.tpVehiculo = new System.Windows.Forms.TabPage();
            this.dgvCondiciones = new System.Windows.Forms.DataGridView();
            this.txtMarcaModelo = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.txtPlacas = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.cbPropietarios = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPredios = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbVariedades = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFrutas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLocalidades = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cbProveedores = new System.Windows.Forms.ComboBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.tcRecepcion = new System.Windows.Forms.TabControl();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPesoBruto = new System.Windows.Forms.TextBox();
            this.txtTara = new System.Windows.Forms.TextBox();
            this.txtPesoNeto = new System.Windows.Forms.TextBox();
            this.txtFolioProveedor = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtFolioCiclo = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cbEstado = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.txtRecepcion = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.cbCiclos = new System.Windows.Forms.ComboBox();
            this.lblGrados = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPesoDisponible = new System.Windows.Forms.TextBox();
            this.tsMenu.SuspendLayout();
            this.tpTraspaleos.SuspendLayout();
            this.tsOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalidas)).BeginInit();
            this.tpVehiculo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondiciones)).BeginInit();
            this.tpGeneral.SuspendLayout();
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
            this.tsMenu.Size = new System.Drawing.Size(965, 45);
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
            // tpTraspaleos
            // 
            this.tpTraspaleos.Controls.Add(this.tsOpciones);
            this.tpTraspaleos.Controls.Add(this.dgvSalidas);
            this.tpTraspaleos.Location = new System.Drawing.Point(4, 29);
            this.tpTraspaleos.Name = "tpTraspaleos";
            this.tpTraspaleos.Size = new System.Drawing.Size(933, 305);
            this.tpTraspaleos.TabIndex = 1;
            this.tpTraspaleos.Text = "Traspaleos";
            this.tpTraspaleos.UseVisualStyleBackColor = true;
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
            this.tsOpciones.Size = new System.Drawing.Size(933, 45);
            this.tsOpciones.TabIndex = 7;
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
            // 
            // dgvSalidas
            // 
            this.dgvSalidas.AllowUserToAddRows = false;
            this.dgvSalidas.AllowUserToDeleteRows = false;
            this.dgvSalidas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSalidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalidas.Location = new System.Drawing.Point(0, 48);
            this.dgvSalidas.Name = "dgvSalidas";
            this.dgvSalidas.Size = new System.Drawing.Size(933, 255);
            this.dgvSalidas.TabIndex = 6;
            this.dgvSalidas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMuestreos_CellContentClick);
            this.dgvSalidas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMuestreos_CellDoubleClick);
            // 
            // tpVehiculo
            // 
            this.tpVehiculo.Controls.Add(this.dgvCondiciones);
            this.tpVehiculo.Controls.Add(this.txtMarcaModelo);
            this.tpVehiculo.Controls.Add(this.txtColor);
            this.tpVehiculo.Controls.Add(this.txtConductor);
            this.tpVehiculo.Controls.Add(this.txtPlacas);
            this.tpVehiculo.Controls.Add(this.label13);
            this.tpVehiculo.Controls.Add(this.label12);
            this.tpVehiculo.Controls.Add(this.label11);
            this.tpVehiculo.Controls.Add(this.label10);
            this.tpVehiculo.Location = new System.Drawing.Point(4, 29);
            this.tpVehiculo.Name = "tpVehiculo";
            this.tpVehiculo.Size = new System.Drawing.Size(933, 305);
            this.tpVehiculo.TabIndex = 2;
            this.tpVehiculo.Text = "Vehiculo";
            this.tpVehiculo.UseVisualStyleBackColor = true;
            // 
            // dgvCondiciones
            // 
            this.dgvCondiciones.AllowUserToAddRows = false;
            this.dgvCondiciones.AllowUserToDeleteRows = false;
            this.dgvCondiciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCondiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCondiciones.Location = new System.Drawing.Point(472, 0);
            this.dgvCondiciones.Name = "dgvCondiciones";
            this.dgvCondiciones.ReadOnly = true;
            this.dgvCondiciones.Size = new System.Drawing.Size(431, 209);
            this.dgvCondiciones.TabIndex = 5;
            // 
            // txtMarcaModelo
            // 
            this.txtMarcaModelo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMarcaModelo.Location = new System.Drawing.Point(124, 102);
            this.txtMarcaModelo.Name = "txtMarcaModelo";
            this.txtMarcaModelo.Size = new System.Drawing.Size(342, 26);
            this.txtMarcaModelo.TabIndex = 4;
            // 
            // txtColor
            // 
            this.txtColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtColor.Location = new System.Drawing.Point(124, 70);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(342, 26);
            this.txtColor.TabIndex = 3;
            // 
            // txtConductor
            // 
            this.txtConductor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConductor.Location = new System.Drawing.Point(124, 38);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(342, 26);
            this.txtConductor.TabIndex = 2;
            // 
            // txtPlacas
            // 
            this.txtPlacas.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlacas.Location = new System.Drawing.Point(124, 6);
            this.txtPlacas.Name = "txtPlacas";
            this.txtPlacas.Size = new System.Drawing.Size(126, 26);
            this.txtPlacas.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 105);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 20);
            this.label13.TabIndex = 57;
            this.label13.Text = "Marca/Modelo:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 73);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 20);
            this.label12.TabIndex = 57;
            this.label12.Text = "Color:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 41);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 20);
            this.label11.TabIndex = 57;
            this.label11.Text = "Conductor:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 20);
            this.label10.TabIndex = 56;
            this.label10.Text = "Placas:";
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.cbPropietarios);
            this.tpGeneral.Controls.Add(this.label1);
            this.tpGeneral.Controls.Add(this.cbPredios);
            this.tpGeneral.Controls.Add(this.label2);
            this.tpGeneral.Controls.Add(this.cbVariedades);
            this.tpGeneral.Controls.Add(this.label3);
            this.tpGeneral.Controls.Add(this.cbFrutas);
            this.tpGeneral.Controls.Add(this.label4);
            this.tpGeneral.Controls.Add(this.cbLocalidades);
            this.tpGeneral.Controls.Add(this.label34);
            this.tpGeneral.Controls.Add(this.cbProveedores);
            this.tpGeneral.Controls.Add(this.lblNombre);
            this.tpGeneral.Location = new System.Drawing.Point(4, 29);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(933, 305);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // cbPropietarios
            // 
            this.cbPropietarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPropietarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPropietarios.Enabled = false;
            this.cbPropietarios.FormattingEnabled = true;
            this.cbPropietarios.Location = new System.Drawing.Point(110, 99);
            this.cbPropietarios.Name = "cbPropietarios";
            this.cbPropietarios.Size = new System.Drawing.Size(817, 28);
            this.cbPropietarios.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 59;
            this.label1.Text = "Propietario:";
            // 
            // cbPredios
            // 
            this.cbPredios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPredios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPredios.FormattingEnabled = true;
            this.cbPredios.Location = new System.Drawing.Point(110, 68);
            this.cbPredios.Name = "cbPredios";
            this.cbPredios.Size = new System.Drawing.Size(817, 28);
            this.cbPredios.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 60;
            this.label2.Text = "Predio:";
            // 
            // cbVariedades
            // 
            this.cbVariedades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVariedades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVariedades.FormattingEnabled = true;
            this.cbVariedades.Location = new System.Drawing.Point(110, 161);
            this.cbVariedades.Name = "cbVariedades";
            this.cbVariedades.Size = new System.Drawing.Size(817, 28);
            this.cbVariedades.TabIndex = 54;
            this.cbVariedades.SelectedIndexChanged += new System.EventHandler(this.cbVariedad_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 164);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 57;
            this.label3.Text = "Variedad:";
            // 
            // cbFrutas
            // 
            this.cbFrutas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFrutas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrutas.FormattingEnabled = true;
            this.cbFrutas.Location = new System.Drawing.Point(110, 130);
            this.cbFrutas.Name = "cbFrutas";
            this.cbFrutas.Size = new System.Drawing.Size(817, 28);
            this.cbFrutas.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 133);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 58;
            this.label4.Text = "Fruta:";
            // 
            // cbLocalidades
            // 
            this.cbLocalidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocalidades.FormattingEnabled = true;
            this.cbLocalidades.Location = new System.Drawing.Point(110, 37);
            this.cbLocalidades.Name = "cbLocalidades";
            this.cbLocalidades.Size = new System.Drawing.Size(817, 28);
            this.cbLocalidades.TabIndex = 50;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(7, 40);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(81, 20);
            this.label34.TabIndex = 56;
            this.label34.Text = "Localidad:";
            // 
            // cbProveedores
            // 
            this.cbProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedores.FormattingEnabled = true;
            this.cbProveedores.Location = new System.Drawing.Point(110, 6);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(817, 28);
            this.cbProveedores.TabIndex = 49;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(7, 9);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(85, 20);
            this.lblNombre.TabIndex = 55;
            this.lblNombre.Text = "Proveedor:";
            // 
            // tcRecepcion
            // 
            this.tcRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcRecepcion.Controls.Add(this.tpGeneral);
            this.tcRecepcion.Controls.Add(this.tpVehiculo);
            this.tcRecepcion.Controls.Add(this.tpTraspaleos);
            this.tcRecepcion.Location = new System.Drawing.Point(12, 141);
            this.tcRecepcion.Name = "tcRecepcion";
            this.tcRecepcion.SelectedIndex = 0;
            this.tcRecepcion.Size = new System.Drawing.Size(941, 338);
            this.tcRecepcion.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(454, 112);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 20);
            this.label7.TabIndex = 94;
            this.label7.Text = "Peso Neto:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(235, 112);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 20);
            this.label8.TabIndex = 93;
            this.label8.Text = "Tara:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 112);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 20);
            this.label20.TabIndex = 92;
            this.label20.Text = "Peso Bruto:";
            // 
            // txtPesoBruto
            // 
            this.txtPesoBruto.Enabled = false;
            this.txtPesoBruto.Location = new System.Drawing.Point(106, 109);
            this.txtPesoBruto.Name = "txtPesoBruto";
            this.txtPesoBruto.Size = new System.Drawing.Size(125, 26);
            this.txtPesoBruto.TabIndex = 78;
            this.txtPesoBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTara
            // 
            this.txtTara.Enabled = false;
            this.txtTara.Location = new System.Drawing.Point(326, 109);
            this.txtTara.Name = "txtTara";
            this.txtTara.Size = new System.Drawing.Size(125, 26);
            this.txtTara.TabIndex = 79;
            this.txtTara.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPesoNeto
            // 
            this.txtPesoNeto.Enabled = false;
            this.txtPesoNeto.Location = new System.Drawing.Point(545, 109);
            this.txtPesoNeto.Name = "txtPesoNeto";
            this.txtPesoNeto.Size = new System.Drawing.Size(125, 26);
            this.txtPesoNeto.TabIndex = 80;
            this.txtPesoNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFolioProveedor
            // 
            this.txtFolioProveedor.Enabled = false;
            this.txtFolioProveedor.Location = new System.Drawing.Point(799, 51);
            this.txtFolioProveedor.Name = "txtFolioProveedor";
            this.txtFolioProveedor.Size = new System.Drawing.Size(125, 26);
            this.txtFolioProveedor.TabIndex = 69;
            this.txtFolioProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(676, 54);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(124, 20);
            this.label25.TabIndex = 91;
            this.label25.Text = "Viaje Proveedor:";
            // 
            // txtFolioCiclo
            // 
            this.txtFolioCiclo.Enabled = false;
            this.txtFolioCiclo.Location = new System.Drawing.Point(545, 51);
            this.txtFolioCiclo.Name = "txtFolioCiclo";
            this.txtFolioCiclo.Size = new System.Drawing.Size(125, 26);
            this.txtFolioCiclo.TabIndex = 68;
            this.txtFolioCiclo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(454, 54);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(86, 20);
            this.label26.TabIndex = 90;
            this.label26.Text = "Viaje Ciclo:";
            // 
            // cbEstado
            // 
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.Enabled = false;
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(326, 79);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(125, 28);
            this.cbEstado.TabIndex = 72;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(235, 83);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(68, 20);
            this.label28.TabIndex = 88;
            this.label28.Text = "Estatus:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(106, 81);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(125, 26);
            this.dtpFecha.TabIndex = 70;
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Location = new System.Drawing.Point(13, 83);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(58, 20);
            this.lblFechaInicial.TabIndex = 81;
            this.lblFechaInicial.Text = "Fecha:";
            // 
            // txtRecepcion
            // 
            this.txtRecepcion.Enabled = false;
            this.txtRecepcion.Location = new System.Drawing.Point(326, 51);
            this.txtRecepcion.Name = "txtRecepcion";
            this.txtRecepcion.Size = new System.Drawing.Size(125, 26);
            this.txtRecepcion.TabIndex = 67;
            this.txtRecepcion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(235, 54);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(89, 20);
            this.label32.TabIndex = 86;
            this.label32.Text = "Recepción:";
            // 
            // cbCiclos
            // 
            this.cbCiclos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCiclos.Enabled = false;
            this.cbCiclos.FormattingEnabled = true;
            this.cbCiclos.Location = new System.Drawing.Point(106, 51);
            this.cbCiclos.Name = "cbCiclos";
            this.cbCiclos.Size = new System.Drawing.Size(125, 28);
            this.cbCiclos.TabIndex = 66;
            // 
            // lblGrados
            // 
            this.lblGrados.AutoSize = true;
            this.lblGrados.Location = new System.Drawing.Point(13, 51);
            this.lblGrados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrados.Name = "lblGrados";
            this.lblGrados.Size = new System.Drawing.Size(47, 20);
            this.lblGrados.TabIndex = 82;
            this.lblGrados.Text = "Ciclo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(676, 112);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 95;
            this.label5.Text = "Disponible:";
            // 
            // txtPesoDisponible
            // 
            this.txtPesoDisponible.Enabled = false;
            this.txtPesoDisponible.Location = new System.Drawing.Point(799, 109);
            this.txtPesoDisponible.Name = "txtPesoDisponible";
            this.txtPesoDisponible.Size = new System.Drawing.Size(125, 26);
            this.txtPesoDisponible.TabIndex = 96;
            this.txtPesoDisponible.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MangoRecepcionSalidasView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 491);
            this.Controls.Add(this.txtPesoDisponible);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtPesoBruto);
            this.Controls.Add(this.txtTara);
            this.Controls.Add(this.txtPesoNeto);
            this.Controls.Add(this.txtFolioProveedor);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtFolioCiclo);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.cbEstado);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFechaInicial);
            this.Controls.Add(this.txtRecepcion);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.cbCiclos);
            this.Controls.Add(this.lblGrados);
            this.Controls.Add(this.tcRecepcion);
            this.Controls.Add(this.tsMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MangoRecepcionSalidasView";
            this.Tag = "35";
            this.Text = "Traspaleos";
            this.Load += new System.EventHandler(this.RecepcionView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tpTraspaleos.ResumeLayout(false);
            this.tpTraspaleos.PerformLayout();
            this.tsOpciones.ResumeLayout(false);
            this.tsOpciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalidas)).EndInit();
            this.tpVehiculo.ResumeLayout(false);
            this.tpVehiculo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondiciones)).EndInit();
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
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
        private System.Windows.Forms.TabPage tpTraspaleos;
        private System.Windows.Forms.DataGridView dgvSalidas;
        private System.Windows.Forms.TabPage tpVehiculo;
        private System.Windows.Forms.TextBox txtMarcaModelo;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.TextBox txtPlacas;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabControl tcRecepcion;
        private System.Windows.Forms.DataGridView dgvCondiciones;
        private System.Windows.Forms.ToolStrip tsOpciones;
        private System.Windows.Forms.ToolStripButton tsbSyncGrid;
        private System.Windows.Forms.ToolStripButton tsbAddGrid;
        private System.Windows.Forms.ToolStripButton tsbDelGrid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtPesoBruto;
        private System.Windows.Forms.TextBox txtTara;
        private System.Windows.Forms.TextBox txtPesoNeto;
        private System.Windows.Forms.TextBox txtFolioProveedor;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtFolioCiclo;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cbEstado;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.TextBox txtRecepcion;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox cbCiclos;
        private System.Windows.Forms.Label lblGrados;
        private System.Windows.Forms.ComboBox cbPropietarios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPredios;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbVariedades;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFrutas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLocalidades;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cbProveedores;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.ToolStripButton tsbImprimir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPesoDisponible;
    }
}