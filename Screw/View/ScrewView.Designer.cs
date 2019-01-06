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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.CloseKompas3D.Location = new System.Drawing.Point(255, 85);
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
            this.UnloadKompasApplicationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UnloadKompasApplicationLabel.Location = new System.Drawing.Point(5, 89);
            this.UnloadKompasApplicationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UnloadKompasApplicationLabel.Name = "UnloadKompasApplicationLabel";
            this.UnloadKompasApplicationLabel.Size = new System.Drawing.Size(242, 24);
            this.UnloadKompasApplicationLabel.TabIndex = 0;
            this.UnloadKompasApplicationLabel.Text = "Unload Kompas Application";
            // 
            // LoadKompas3D
            // 
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
            this.LoadKompasAppLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadKompasAppLabel.Location = new System.Drawing.Point(5, 37);
            this.LoadKompasAppLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LoadKompasAppLabel.Name = "LoadKompasAppLabel";
            this.LoadKompasAppLabel.Size = new System.Drawing.Size(224, 24);
            this.LoadKompasAppLabel.TabIndex = 0;
            this.LoadKompasAppLabel.Text = "Load Kompas Application";
            // 
            // Defaults
            // 
            this.Defaults.Location = new System.Drawing.Point(13, 491);
            this.Defaults.Margin = new System.Windows.Forms.Padding(4);
            this.Defaults.Name = "Defaults";
            this.Defaults.Size = new System.Drawing.Size(156, 42);
            this.Defaults.TabIndex = 7;
            this.Defaults.Text = "Standard parameters";
            this.Defaults.UseVisualStyleBackColor = true;
            this.Defaults.Click += new System.EventHandler(this.Defaults_Click_1);
            // 
            // RunButton
            // 
            this.RunButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RunButton.Location = new System.Drawing.Point(177, 491);
            this.RunButton.Margin = new System.Windows.Forms.Padding(4);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(204, 42);
            this.RunButton.TabIndex = 8;
            this.RunButton.Text = "Build screw";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click_1);
            // 
            // NutHeightLabel
            // 
            this.NutHeightLabel.AutoSize = true;
            this.NutHeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NutHeightLabel.Location = new System.Drawing.Point(10, 247);
            this.NutHeightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NutHeightLabel.Name = "NutHeightLabel";
            this.NutHeightLabel.Size = new System.Drawing.Size(126, 24);
            this.NutHeightLabel.TabIndex = 0;
            this.NutHeightLabel.Text = "Hat height (H)";
            // 
            // NutThreadDiameterLabel
            // 
            this.NutThreadDiameterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NutThreadDiameterLabel.Location = new System.Drawing.Point(10, 299);
            this.NutThreadDiameterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NutThreadDiameterLabel.Name = "NutThreadDiameterLabel";
            this.NutThreadDiameterLabel.Size = new System.Drawing.Size(144, 27);
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
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(13, 138);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(362, 343);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameters of screw";
            // 
            // screwHatInnerDiameter
            // 
            this.screwHatInnerDiameter.FormattingEnabled = true;
            this.screwHatInnerDiameter.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.screwHatInnerDiameter.Location = new System.Drawing.Point(254, 91);
            this.screwHatInnerDiameter.Name = "screwHatInnerDiameter";
            this.screwHatInnerDiameter.Size = new System.Drawing.Size(100, 32);
            this.screwHatInnerDiameter.TabIndex = 4;
            // 
            // NutThreadDiameter
            // 
            this.NutThreadDiameter.FormattingEnabled = true;
            this.NutThreadDiameter.Items.AddRange(new object[] {
            "2",
            "5",
            "8",
            "11",
            "14"});
            this.NutThreadDiameter.Location = new System.Drawing.Point(255, 296);
            this.NutThreadDiameter.Name = "NutThreadDiameter";
            this.NutThreadDiameter.Size = new System.Drawing.Size(99, 32);
            this.NutThreadDiameter.TabIndex = 8;
            // 
            // NutHeight
            // 
            this.NutHeight.FormattingEnabled = true;
            this.NutHeight.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.NutHeight.Location = new System.Drawing.Point(255, 244);
            this.NutHeight.Name = "NutHeight";
            this.NutHeight.Size = new System.Drawing.Size(99, 32);
            this.NutHeight.TabIndex = 7;
            // 
            // ScrewBaseThreadWidth
            // 
            this.ScrewBaseThreadWidth.FormattingEnabled = true;
            this.ScrewBaseThreadWidth.Items.AddRange(new object[] {
            "52",
            "58",
            "64",
            "70",
            "76"});
            this.ScrewBaseThreadWidth.Location = new System.Drawing.Point(255, 191);
            this.ScrewBaseThreadWidth.Name = "ScrewBaseThreadWidth";
            this.ScrewBaseThreadWidth.Size = new System.Drawing.Size(99, 32);
            this.ScrewBaseThreadWidth.TabIndex = 6;
            // 
            // ScrewBaseSmoothWidth
            // 
            this.ScrewBaseSmoothWidth.FormattingEnabled = true;
            this.ScrewBaseSmoothWidth.Items.AddRange(new object[] {
            "20",
            "22",
            "25",
            "28",
            "30"});
            this.ScrewBaseSmoothWidth.Location = new System.Drawing.Point(255, 142);
            this.ScrewBaseSmoothWidth.Name = "ScrewBaseSmoothWidth";
            this.ScrewBaseSmoothWidth.Size = new System.Drawing.Size(100, 32);
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
            this.ScrewHatWidth.FormattingEnabled = true;
            this.ScrewHatWidth.Items.AddRange(new object[] {
            "20 ",
            "21 ",
            "27 ",
            "30"});
            this.ScrewHatWidth.Location = new System.Drawing.Point(255, 38);
            this.ScrewHatWidth.Name = "ScrewHatWidth";
            this.ScrewHatWidth.Size = new System.Drawing.Size(100, 32);
            this.ScrewHatWidth.TabIndex = 3;
            // 
            // ScrewHatWidthLabel
            // 
            this.ScrewHatWidthLabel.AutoSize = true;
            this.ScrewHatWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewHatWidthLabel.Location = new System.Drawing.Point(8, 41);
            this.ScrewHatWidthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScrewHatWidthLabel.Name = "ScrewHatWidthLabel";
            this.ScrewHatWidthLabel.Size = new System.Drawing.Size(146, 24);
            this.ScrewHatWidthLabel.TabIndex = 0;
            this.ScrewHatWidthLabel.Text = "Hat diameter (D)";
            // 
            // ScrewBaseSmoothPartLabel
            // 
            this.ScrewBaseSmoothPartLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewBaseSmoothPartLabel.Location = new System.Drawing.Point(9, 145);
            this.ScrewBaseSmoothPartLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScrewBaseSmoothPartLabel.Name = "ScrewBaseSmoothPartLabel";
            this.ScrewBaseSmoothPartLabel.Size = new System.Drawing.Size(145, 27);
            this.ScrewBaseSmoothPartLabel.TabIndex = 0;
            this.ScrewBaseSmoothPartLabel.Text = "Smooth part (l)";
            // 
            // ScrewBaseThreadPartLabel
            // 
            this.ScrewBaseThreadPartLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewBaseThreadPartLabel.Location = new System.Drawing.Point(10, 194);
            this.ScrewBaseThreadPartLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScrewBaseThreadPartLabel.Name = "ScrewBaseThreadPartLabel";
            this.ScrewBaseThreadPartLabel.Size = new System.Drawing.Size(144, 27);
            this.ScrewBaseThreadPartLabel.TabIndex = 0;
            this.ScrewBaseThreadPartLabel.Text = "Thread part (b)";
            // 
            // ScrewHatInnerCircleLabel
            // 
            this.ScrewHatInnerCircleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScrewHatInnerCircleLabel.Location = new System.Drawing.Point(8, 94);
            this.ScrewHatInnerCircleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScrewHatInnerCircleLabel.Name = "ScrewHatInnerCircleLabel";
            this.ScrewHatInnerCircleLabel.Size = new System.Drawing.Size(99, 27);
            this.ScrewHatInnerCircleLabel.TabIndex = 0;
            this.ScrewHatInnerCircleLabel.Text = "Slot depth (m)";
            // 
            // ScrewView
            // 
            this.ClientSize = new System.Drawing.Size(388, 540);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Defaults);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.groupBox2);
            this.MaximumSize = new System.Drawing.Size(406, 587);
            this.MinimumSize = new System.Drawing.Size(406, 587);
            this.Name = "ScrewView";
            this.Text = "Build screw";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}

