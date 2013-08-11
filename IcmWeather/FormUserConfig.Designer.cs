namespace IcmWeather
{
    partial class FormUserConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserConfig));
            this.cbModel = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudRefresh = new System.Windows.Forms.NumericUpDown();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelRefresh = new System.Windows.Forms.Label();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.linkLabelMeteo = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefresh)).BeginInit();
            this.SuspendLayout();
            // 
            // cbModel
            // 
            this.cbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModel.FormattingEnabled = true;
            this.cbModel.Location = new System.Drawing.Point(82, 33);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(121, 21);
            this.cbModel.TabIndex = 0;
            this.cbModel.SelectedIndexChanged += new System.EventHandler(this.cbModel_SelectedIndexChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 175);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // nudX
            // 
            this.nudX.Location = new System.Drawing.Point(82, 60);
            this.nudX.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(54, 20);
            this.nudX.TabIndex = 1;
            // 
            // nudY
            // 
            this.nudY.Location = new System.Drawing.Point(83, 86);
            this.nudY.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(53, 20);
            this.nudY.TabIndex = 2;
            // 
            // nudRefresh
            // 
            this.nudRefresh.Location = new System.Drawing.Point(83, 112);
            this.nudRefresh.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudRefresh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRefresh.Name = "nudRefresh";
            this.nudRefresh.Size = new System.Drawing.Size(53, 20);
            this.nudRefresh.TabIndex = 3;
            this.nudRefresh.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Items.AddRange(new object[] {
            ""});
            this.cbLanguage.Location = new System.Drawing.Point(82, 138);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbLanguage.TabIndex = 4;
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(40, 36);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(36, 13);
            this.labelModel.TabIndex = 6;
            this.labelModel.Text = "Model";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(62, 62);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(14, 13);
            this.labelX.TabIndex = 6;
            this.labelX.Text = "X";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(62, 88);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(14, 13);
            this.labelY.TabIndex = 6;
            this.labelY.Text = "Y";
            // 
            // labelRefresh
            // 
            this.labelRefresh.AutoSize = true;
            this.labelRefresh.Location = new System.Drawing.Point(7, 114);
            this.labelRefresh.Name = "labelRefresh";
            this.labelRefresh.Size = new System.Drawing.Size(69, 13);
            this.labelRefresh.TabIndex = 6;
            this.labelRefresh.Text = "Refresh (min)";
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(21, 141);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(55, 13);
            this.labelLanguage.TabIndex = 6;
            this.labelLanguage.Text = "Language";
            // 
            // linkLabelMeteo
            // 
            this.linkLabelMeteo.AutoSize = true;
            this.linkLabelMeteo.Location = new System.Drawing.Point(71, 9);
            this.linkLabelMeteo.Name = "linkLabelMeteo";
            this.linkLabelMeteo.Size = new System.Drawing.Size(74, 13);
            this.linkLabelMeteo.TabIndex = 7;
            this.linkLabelMeteo.TabStop = true;
            this.linkLabelMeteo.Text = "www.meteo.pl";
            this.linkLabelMeteo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMeteo_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(128, 175);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormUserConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 208);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.linkLabelMeteo);
            this.Controls.Add(this.labelLanguage);
            this.Controls.Add(this.labelRefresh);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.nudRefresh);
            this.Controls.Add(this.nudY);
            this.Controls.Add(this.nudX);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.cbModel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormUserConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICM Weather";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefresh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbModel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.NumericUpDown nudRefresh;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelRefresh;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.LinkLabel linkLabelMeteo;
        private System.Windows.Forms.Button buttonCancel;
    }
}