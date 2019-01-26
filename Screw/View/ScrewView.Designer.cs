namespace Screw
{
    partial class ScrewView
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CloseKompas3D = new System.Windows.Forms.Button();
            this.UnloadKompasApplicationLabel = new System.Windows.Forms.Label();
            this.LoadKompas3D = new System.Windows.Forms.Button();
            this.LoadKompasAppLabel = new System.Windows.Forms.Label();
            this.Defaults = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.NutHeightLabel = new System.Windows.Forms.Label();
            this.NutThreadDiameterLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.screwHatInnerDiameter = new System.Windows.Forms.ComboBox();
            this.NutThreadDiameter = new System.Windows.Forms.ComboBox();
            this.NutHeight = new System.Windows.Forms.ComboBox();
            this.ScrewBaseThreadWidth = new System.Windows.Forms.ComboBox();
            this.ScrewBaseSmoothWidth = new System.Windows.Forms.ComboBox();
            this.ScrewHatWidth = new System.Windows.Forms.ComboBox();
            this.ScrewHatWidthLabel = new System.Windows.Forms.Label();
            this.ScrewBaseSmoothPartLabel = new System.Windows.Forms.Label();
            this.ScrewBaseThreadPartLabel = new System.Windows.Forms.Label();
            this.ScrewHatInnerCircleLabel = new System.Windows.Forms.Label();
            this.TypeOfScrewdriverHoleGroupBox = new System.Windows.Forms.GroupBox();
            this.RegularPolygonScrewdriverRadioButton = new System.Windows.Forms.RadioButton();
            this.WithoutHoleRadioButton = new System.Windows.Forms.RadioButton();
            this.CrossheadScrewdriverRadioButton = new System.Windows.Forms.RadioButton();
            this.FlatheadScrewdriverRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TypeOfScrewdriverHoleGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CloseKompas3D);
            this.groupBox1.Controls.Add(this.UnloadKompasApplicationLabel);
            this.groupBox1.Controls.Add(this.LoadKompas3D);
            this.groupBox1.Controls.Add(this.LoadKompasAppLabel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(13, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(362, 127);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kompas Application";
            // 
            // CloseKompas3D
            // 
            this.CloseKompas3D.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseKompas3D.Location = new System.Drawing.Point(255, 75);
            this.CloseKompas3D.Margin = new System.Windows.Forms.Padding(4);
            this.CloseKompas3D.Name = "CloseKompas3D";
            this.CloseKompas3D.Size = new System.Drawing.Size(99, 32);
            this.CloseKompas3D.TabIndex = 2;
            this.CloseKompas3D.Text = "Unload";
            this.CloseKompas3D.UseVisualStyleBackColor = true;
            this.CloseKompas3D.Click += new System.EventHandler(this.CloseKompas3D_Click);
            // 
            // UnloadKompasApplicationLabel
            // 
            this.UnloadKompasApplicationLabel.AutoSize = true;
            this.UnloadKompasApplicationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UnloadKompasApplicationLabel.Location = new System.Drawing.Point(5, 82);
            this.UnloadKompasApplicationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UnloadKompasApplicationLabel.Name = "UnloadKompasApplicationLabel";
            this.UnloadKompasApplicationLabel.Size = new System.Drawing.Size(214, 20);
            this.UnloadKompasApplicationLabel.TabIndex = 0;
            this.UnloadKompasApplicationLabel.Text = "Unload Kompas Application";
            // 
            // LoadKompas3D
            // 
            this.LoadKompas3D.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadKompas3D.Location = new System.Drawing.Point(255, 33);
            this.LoadKompas3D.Margin = new System.Windows.Forms.Padding(4);
            this.LoadKompas3D.Name = "LoadKompas3D";
            this.LoadKompas3D.Size = new System.Drawing.Size(99, 32);
            this.LoadKompas3D.TabIndex = 1;
            this.LoadKompas3D.Text = "Load";
            this.LoadKompas3D.UseVisualStyleBackColor = true;
            this.LoadKompas3D.Click += new System.EventHandler(this.LoadKompas3D_Click);
            // 
            // LoadKompasAppLabel
            // 
            this.LoadKompasAppLabel.AutoSize = true;
            this.LoadKompasAppLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadKompasAppLabel.Location = new System.Drawing.Point(5, 40);
            this.LoadKompasAppLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LoadKompasAppLabel.Name = "LoadKompasAppLabel";
            this.LoadKompasAppLabel.Size = new System.Drawing.Size(199, 20);
            this.LoadKompasAppLabel.TabIndex = 0;
            this.LoadKompasAppLabel.Text = "Load Kompas Application";
            // 
            // Defaults
            // 
            this.Defaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Defaults.Location = new System.Drawing.Point(13, 574);
            this.Defaults.Margin = new System.Windows.Forms.Padding(4);
            this.Defaults.Name = "Defaults";
            this.Defaults.Size = new System.Drawing.Size(156, 42);
            this.Defaults.TabIndex = 7;
            this.Defaults.Text = "Standard parameters";
            this.Defaults.UseVisualStyleBackColor = true;
            this.Defaults.Click += new System.EventHandler(this.Defaults_Click);
            // 
            // RunButton
            // 
            this.RunButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RunButton.Location = new System.Drawing.Point(177, 574);
            this.RunButton.Margin = new System.Windows.Forms.Padding(4);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(198, 42);
            this.RunButton.TabIndex = 8;
            this.RunButton.Text = "Build screw";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click_1);
            // 
            // NutHeightLabel
            // 
            this.NutHeightLabel.AutoSize = true;
            this.NutHeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NutHeightLabel.Location = new System.Drawing.Point(9, 173);
            this.NutHeightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NutHeightLabel.Name = "NutHeightLabel";
            this.NutHeightLabel.Size = new System.Drawing.Size(116, 20);
            this.NutHeightLabel.TabIndex = 0;
            this.NutHeightLabel.Text = "Hat height (H)";
            // 
            // NutThreadDiameterLabel
            // 
            this.NutThreadDiameterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NutThreadDiameterLabel.Location = new System.Drawing.Point(9, 207);
            this.NutThreadDiameterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NutThreadDiameterLabel.Name = "NutThreadDiameterLabel";
            this.NutThreadDiameterLabel.Size = new System.Drawing.Size(117, 27);
            this.NutThreadDiameterLabel.TabIndex = 0;
            this.NutThreadDiameterLabel.Text = "Slot width (n)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.screwHatInnerDiameter);
            this.groupBox2.Controls.Add(this.NutThreadDiameter);
            this.groupBox2.Controls.Add(this.NutHeight);
            this.groupBox2.Controls.Add(this.ScrewBaseThreadWidth);
            this.groupBox2.Controls.Add(this.ScrewBaseSmoothWidth);
            this.groupBox2.Controls.Add(this.ScrewHatWidth);
            this.groupBox2.Controls.Add(this.ScrewHatWidthLabel);
            this.groupBox2.Controls.Add(this.NutThreadDiameterLabel);
            this.groupBox2.Controls.Add(this.NutHeightLabel);
            this.groupBox2.Controls.Add(this.ScrewBaseSmoothPartLabel);
            this.groupBox2.Controls.Add(this.ScrewBaseThreadPartLabel);
            this.groupBox2.Controls.Add(this.ScrewHatInnerCircleLabel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(13, 138);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(362, 243);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameters of screw";
            // 
            // screwHatInnerDiameter
            // 
            this.screwHatInnerDiameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.screwHatInnerDiameter.FormattingEnabled = true;
            this.screwHatInnerDiameter.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.screwHatInnerDiameter.Location = new System.Drawing.Point(255, 68);
            this.screwHatInnerDiameter.Name = "screwHatInnerDiameter";
            this.screwHatInnerDiameter.Size = new System.Drawing.Size(100, 28);
            this.screwHatInnerDiameter.TabIndex = 4;
            // 
            // NutThreadDiameter
            // 
            this.NutThreadDiameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NutThreadDiameter.FormattingEnabled = true;
            this.NutThreadDiameter.Items.AddRange(new object[] {
            "2",
            "5",
            "8",
            "11",
            "14"});
            this.NutThreadDiameter.Location = new System.Drawing.Point(255, 204);
            this.NutThreadDiameter.Name = "NutThreadDiameter";
            this.NutThreadDiameter.Size = new System.Drawing.Size(100, 28);
            this.NutThreadDiameter.TabIndex = 8;
            // 
            // NutHeight
            // 
            this.NutHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NutHeight.FormattingEnabled = true;
            this.NutHeight.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.NutHeight.Location = new System.Drawing.Point(255, 170);
            this.NutHeight.Name = "NutHeight";
            this.NutHeight.Size = new System.Drawing.Size(100, 28);
            this.NutHeight.TabIndex = 7;
            // 
            // ScrewBaseThreadWidth
            // 
            this.ScrewBaseThreadWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewBaseThreadWidth.FormattingEnabled = true;
            this.ScrewBaseThreadWidth.Items.AddRange(new object[] {
            "52",
            "58",
            "64",
            "70",
            "76"});
            this.ScrewBaseThreadWidth.Location = new System.Drawing.Point(255, 136);
            this.ScrewBaseThreadWidth.Name = "ScrewBaseThreadWidth";
            this.ScrewBaseThreadWidth.Size = new System.Drawing.Size(100, 28);
            this.ScrewBaseThreadWidth.TabIndex = 6;
            // 
            // ScrewBaseSmoothWidth
            // 
            this.ScrewBaseSmoothWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewBaseSmoothWidth.FormattingEnabled = true;
            this.ScrewBaseSmoothWidth.Items.AddRange(new object[] {
            "20",
            "22",
            "25",
            "28",
            "30"});
            this.ScrewBaseSmoothWidth.Location = new System.Drawing.Point(255, 102);
            this.ScrewBaseSmoothWidth.Name = "ScrewBaseSmoothWidth";
            this.ScrewBaseSmoothWidth.Size = new System.Drawing.Size(100, 28);
            this.ScrewBaseSmoothWidth.TabIndex = 5;
            // 
            // ScrewHatWidth
            // 
            this.ScrewHatWidth.AutoCompleteCustomSource.AddRange(new string[] {
            "21",
            "24",
            "27",
            "30",
            "33"});
            this.ScrewHatWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewHatWidth.FormattingEnabled = true;
            this.ScrewHatWidth.Items.AddRange(new object[] {
            "20 ",
            "21 ",
            "27 ",
            "30"});
            this.ScrewHatWidth.Location = new System.Drawing.Point(255, 35);
            this.ScrewHatWidth.Name = "ScrewHatWidth";
            this.ScrewHatWidth.Size = new System.Drawing.Size(100, 28);
            this.ScrewHatWidth.TabIndex = 3;
            // 
            // ScrewHatWidthLabel
            // 
            this.ScrewHatWidthLabel.AutoSize = true;
            this.ScrewHatWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewHatWidthLabel.Location = new System.Drawing.Point(10, 38);
            this.ScrewHatWidthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScrewHatWidthLabel.Name = "ScrewHatWidthLabel";
            this.ScrewHatWidthLabel.Size = new System.Drawing.Size(136, 20);
            this.ScrewHatWidthLabel.TabIndex = 0;
            this.ScrewHatWidthLabel.Text = "Hat diameter (D)";
            // 
            // ScrewBaseSmoothPartLabel
            // 
            this.ScrewBaseSmoothPartLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewBaseSmoothPartLabel.Location = new System.Drawing.Point(9, 105);
            this.ScrewBaseSmoothPartLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScrewBaseSmoothPartLabel.Name = "ScrewBaseSmoothPartLabel";
            this.ScrewBaseSmoothPartLabel.Size = new System.Drawing.Size(145, 27);
            this.ScrewBaseSmoothPartLabel.TabIndex = 0;
            this.ScrewBaseSmoothPartLabel.Text = "Smooth part (l)";
            // 
            // ScrewBaseThreadPartLabel
            // 
            this.ScrewBaseThreadPartLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewBaseThreadPartLabel.Location = new System.Drawing.Point(10, 139);
            this.ScrewBaseThreadPartLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScrewBaseThreadPartLabel.Name = "ScrewBaseThreadPartLabel";
            this.ScrewBaseThreadPartLabel.Size = new System.Drawing.Size(144, 27);
            this.ScrewBaseThreadPartLabel.TabIndex = 0;
            this.ScrewBaseThreadPartLabel.Text = "Thread part (b)";
            // 
            // ScrewHatInnerCircleLabel
            // 
            this.ScrewHatInnerCircleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewHatInnerCircleLabel.Location = new System.Drawing.Point(10, 71);
            this.ScrewHatInnerCircleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScrewHatInnerCircleLabel.Name = "ScrewHatInnerCircleLabel";
            this.ScrewHatInnerCircleLabel.Size = new System.Drawing.Size(128, 27);
            this.ScrewHatInnerCircleLabel.TabIndex = 0;
            this.ScrewHatInnerCircleLabel.Text = "Slot depth (m)";
            // 
            // TypeOfScrewdriverHoleGroupBox
            // 
            this.TypeOfScrewdriverHoleGroupBox.Controls.Add(this.RegularPolygonScrewdriverRadioButton);
            this.TypeOfScrewdriverHoleGroupBox.Controls.Add(this.WithoutHoleRadioButton);
            this.TypeOfScrewdriverHoleGroupBox.Controls.Add(this.CrossheadScrewdriverRadioButton);
            this.TypeOfScrewdriverHoleGroupBox.Controls.Add(this.FlatheadScrewdriverRadioButton);
            this.TypeOfScrewdriverHoleGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TypeOfScrewdriverHoleGroupBox.Location = new System.Drawing.Point(13, 389);
            this.TypeOfScrewdriverHoleGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.TypeOfScrewdriverHoleGroupBox.Name = "TypeOfScrewdriverHoleGroupBox";
            this.TypeOfScrewdriverHoleGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.TypeOfScrewdriverHoleGroupBox.Size = new System.Drawing.Size(362, 177);
            this.TypeOfScrewdriverHoleGroupBox.TabIndex = 21;
            this.TypeOfScrewdriverHoleGroupBox.TabStop = false;
            this.TypeOfScrewdriverHoleGroupBox.Text = "Type of screwdriver hole";
            // 
            // RegularPolygonScrewdriverRadioButton
            // 
            this.RegularPolygonScrewdriverRadioButton.AutoSize = true;
            this.RegularPolygonScrewdriverRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RegularPolygonScrewdriverRadioButton.Location = new System.Drawing.Point(13, 138);
            this.RegularPolygonScrewdriverRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.RegularPolygonScrewdriverRadioButton.Name = "RegularPolygonScrewdriverRadioButton";
            this.RegularPolygonScrewdriverRadioButton.Size = new System.Drawing.Size(246, 24);
            this.RegularPolygonScrewdriverRadioButton.TabIndex = 19;
            this.RegularPolygonScrewdriverRadioButton.TabStop = true;
            this.RegularPolygonScrewdriverRadioButton.Text = "Regular Polygon Screwdriver\r\n";
            this.RegularPolygonScrewdriverRadioButton.UseVisualStyleBackColor = true;
            // 
            // WithoutHoleRadioButton
            // 
            this.WithoutHoleRadioButton.AutoSize = true;
            this.WithoutHoleRadioButton.Checked = true;
            this.WithoutHoleRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WithoutHoleRadioButton.Location = new System.Drawing.Point(13, 42);
            this.WithoutHoleRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.WithoutHoleRadioButton.Name = "WithoutHoleRadioButton";
            this.WithoutHoleRadioButton.Size = new System.Drawing.Size(137, 24);
            this.WithoutHoleRadioButton.TabIndex = 16;
            this.WithoutHoleRadioButton.TabStop = true;
            this.WithoutHoleRadioButton.Text = "Without a hole";
            this.WithoutHoleRadioButton.UseVisualStyleBackColor = true;
            // 
            // CrossheadScrewdriverRadioButton
            // 
            this.CrossheadScrewdriverRadioButton.AutoSize = true;
            this.CrossheadScrewdriverRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CrossheadScrewdriverRadioButton.Location = new System.Drawing.Point(13, 74);
            this.CrossheadScrewdriverRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.CrossheadScrewdriverRadioButton.Name = "CrossheadScrewdriverRadioButton";
            this.CrossheadScrewdriverRadioButton.Size = new System.Drawing.Size(203, 24);
            this.CrossheadScrewdriverRadioButton.TabIndex = 17;
            this.CrossheadScrewdriverRadioButton.TabStop = true;
            this.CrossheadScrewdriverRadioButton.Text = "Crosshead screwdriver";
            this.CrossheadScrewdriverRadioButton.UseVisualStyleBackColor = true;
            // 
            // FlatheadScrewdriverRadioButton
            // 
            this.FlatheadScrewdriverRadioButton.AutoSize = true;
            this.FlatheadScrewdriverRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlatheadScrewdriverRadioButton.Location = new System.Drawing.Point(13, 106);
            this.FlatheadScrewdriverRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.FlatheadScrewdriverRadioButton.Name = "FlatheadScrewdriverRadioButton";
            this.FlatheadScrewdriverRadioButton.Size = new System.Drawing.Size(186, 24);
            this.FlatheadScrewdriverRadioButton.TabIndex = 18;
            this.FlatheadScrewdriverRadioButton.TabStop = true;
            this.FlatheadScrewdriverRadioButton.Text = "Flathead screwdriver";
            this.FlatheadScrewdriverRadioButton.UseVisualStyleBackColor = true;
            // 
            // ScrewView
            // 
            this.ClientSize = new System.Drawing.Size(388, 623);
            this.Controls.Add(this.TypeOfScrewdriverHoleGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Defaults);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.groupBox2);
            this.MaximumSize = new System.Drawing.Size(406, 670);
            this.MinimumSize = new System.Drawing.Size(406, 670);
            this.Name = "ScrewView";
            this.Text = "Build screw";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.TypeOfScrewdriverHoleGroupBox.ResumeLayout(false);
            this.TypeOfScrewdriverHoleGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CloseKompas3D;
        private System.Windows.Forms.Label UnloadKompasApplicationLabel;
        private System.Windows.Forms.Button LoadKompas3D;
        private System.Windows.Forms.Label LoadKompasAppLabel;
        private System.Windows.Forms.Button Defaults;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label NutHeightLabel;
        private System.Windows.Forms.Label NutThreadDiameterLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label ScrewHatWidthLabel;
        private System.Windows.Forms.Label ScrewBaseSmoothPartLabel;
        private System.Windows.Forms.Label ScrewBaseThreadPartLabel;
        private System.Windows.Forms.Label ScrewHatInnerCircleLabel;
        private System.Windows.Forms.ComboBox ScrewHatWidth;
        private System.Windows.Forms.ComboBox NutThreadDiameter;
        private System.Windows.Forms.ComboBox NutHeight;
        private System.Windows.Forms.ComboBox ScrewBaseThreadWidth;
        private System.Windows.Forms.ComboBox ScrewBaseSmoothWidth;
        private System.Windows.Forms.ComboBox screwHatInnerDiameter;
        private System.Windows.Forms.GroupBox TypeOfScrewdriverHoleGroupBox;
        private System.Windows.Forms.RadioButton WithoutHoleRadioButton;
        private System.Windows.Forms.RadioButton CrossheadScrewdriverRadioButton;
        private System.Windows.Forms.RadioButton FlatheadScrewdriverRadioButton;
        private System.Windows.Forms.RadioButton RegularPolygonScrewdriverRadioButton;
    }
}

