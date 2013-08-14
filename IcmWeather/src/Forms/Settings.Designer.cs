namespace IcmWeather.Forms
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.cbModel = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudRefresh = new System.Windows.Forms.NumericUpDown();
            this.cbMeteogramLanguage = new System.Windows.Forms.ComboBox();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelRefresh = new System.Windows.Forms.Label();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.linkLabelMeteo = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbCustomLocation = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefresh)).BeginInit();
            this.SuspendLayout();
            // 
            // cbModel
            // 
            this.cbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModel.FormattingEnabled = true;
            this.cbModel.Location = new System.Drawing.Point(119, 34);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(134, 21);
            this.cbModel.Sorted = true;
            this.cbModel.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 207);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // nudX
            // 
            this.nudX.CausesValidation = false;
            this.nudX.Enabled = false;
            this.nudX.Location = new System.Drawing.Point(119, 115);
            this.nudX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(54, 20);
            this.nudX.TabIndex = 4;
            // 
            // nudY
            // 
            this.nudY.CausesValidation = false;
            this.nudY.Enabled = false;
            this.nudY.Location = new System.Drawing.Point(198, 115);
            this.nudY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(53, 20);
            this.nudY.TabIndex = 5;
            // 
            // nudRefresh
            // 
            this.nudRefresh.Location = new System.Drawing.Point(119, 141);
            this.nudRefresh.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudRefresh.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRefresh.Name = "nudRefresh";
            this.nudRefresh.Size = new System.Drawing.Size(53, 20);
            this.nudRefresh.TabIndex = 6;
            this.nudRefresh.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // cbMeteogramLanguage
            // 
            this.cbMeteogramLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMeteogramLanguage.FormattingEnabled = true;
            this.cbMeteogramLanguage.Items.AddRange(new object[] {
            ""});
            this.cbMeteogramLanguage.Location = new System.Drawing.Point(119, 167);
            this.cbMeteogramLanguage.Name = "cbMeteogramLanguage";
            this.cbMeteogramLanguage.Size = new System.Drawing.Size(133, 21);
            this.cbMeteogramLanguage.Sorted = true;
            this.cbMeteogramLanguage.TabIndex = 7;
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(78, 37);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(36, 13);
            this.labelModel.TabIndex = 6;
            this.labelModel.Text = "Model";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(100, 117);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(14, 13);
            this.labelX.TabIndex = 6;
            this.labelX.Text = "X";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(179, 117);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(14, 13);
            this.labelY.TabIndex = 6;
            this.labelY.Text = "Y";
            // 
            // labelRefresh
            // 
            this.labelRefresh.AutoSize = true;
            this.labelRefresh.Location = new System.Drawing.Point(45, 143);
            this.labelRefresh.Name = "labelRefresh";
            this.labelRefresh.Size = new System.Drawing.Size(69, 13);
            this.labelRefresh.TabIndex = 6;
            this.labelRefresh.Text = "Refresh (min)";
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(7, 170);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(107, 13);
            this.labelLanguage.TabIndex = 6;
            this.labelLanguage.Text = "Meteogram language";
            // 
            // linkLabelMeteo
            // 
            this.linkLabelMeteo.AutoSize = true;
            this.linkLabelMeteo.Location = new System.Drawing.Point(92, 9);
            this.linkLabelMeteo.Name = "linkLabelMeteo";
            this.linkLabelMeteo.Size = new System.Drawing.Size(74, 13);
            this.linkLabelMeteo.TabIndex = 0;
            this.linkLabelMeteo.TabStop = true;
            this.linkLabelMeteo.Text = "www.meteo.pl";
            this.linkLabelMeteo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMeteo_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(178, 207);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // cbCity
            // 
            this.cbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(119, 65);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(134, 21);
            this.cbCity.Sorted = true;
            this.cbCity.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "City";
            // 
            // chbCustomLocation
            // 
            this.chbCustomLocation.AutoSize = true;
            this.chbCustomLocation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbCustomLocation.Location = new System.Drawing.Point(29, 93);
            this.chbCustomLocation.Name = "chbCustomLocation";
            this.chbCustomLocation.Size = new System.Drawing.Size(101, 17);
            this.chbCustomLocation.TabIndex = 3;
            this.chbCustomLocation.Text = "Custom location";
            this.chbCustomLocation.UseVisualStyleBackColor = true;
            this.chbCustomLocation.CheckedChanged += new System.EventHandler(this.chbCustomLocation_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 241);
            this.ControlBox = false;
            this.Controls.Add(this.chbCustomLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCity);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.linkLabelMeteo);
            this.Controls.Add(this.labelLanguage);
            this.Controls.Add(this.labelRefresh);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.cbMeteogramLanguage);
            this.Controls.Add(this.nudRefresh);
            this.Controls.Add(this.nudY);
            this.Controls.Add(this.nudX);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.cbModel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICM Weather";
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
        private System.Windows.Forms.ComboBox cbMeteogramLanguage;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelRefresh;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.LinkLabel linkLabelMeteo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbCustomLocation;
    }
}