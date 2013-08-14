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
            this.labelModel = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelRefresh = new System.Windows.Forms.Label();
            this.linkLabelMeteo = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.labelCity = new System.Windows.Forms.Label();
            this.chbCustomLocation = new System.Windows.Forms.CheckBox();
            this.cbShowSidebar = new System.Windows.Forms.CheckBox();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefresh)).BeginInit();
            this.SuspendLayout();
            // 
            // cbModel
            // 
            this.cbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModel.FormattingEnabled = true;
            resources.ApplyResources(this.cbModel, "cbModel");
            this.cbModel.Name = "cbModel";
            this.cbModel.Sorted = true;
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // nudX
            // 
            this.nudX.CausesValidation = false;
            resources.ApplyResources(this.nudX, "nudX");
            this.nudX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudX.Name = "nudX";
            // 
            // nudY
            // 
            this.nudY.CausesValidation = false;
            resources.ApplyResources(this.nudY, "nudY");
            this.nudY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            // 
            // nudRefresh
            // 
            resources.ApplyResources(this.nudRefresh, "nudRefresh");
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
            this.nudRefresh.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // labelModel
            // 
            resources.ApplyResources(this.labelModel, "labelModel");
            this.labelModel.Name = "labelModel";
            // 
            // labelX
            // 
            resources.ApplyResources(this.labelX, "labelX");
            this.labelX.Name = "labelX";
            // 
            // labelY
            // 
            resources.ApplyResources(this.labelY, "labelY");
            this.labelY.Name = "labelY";
            // 
            // labelRefresh
            // 
            resources.ApplyResources(this.labelRefresh, "labelRefresh");
            this.labelRefresh.Name = "labelRefresh";
            // 
            // linkLabelMeteo
            // 
            resources.ApplyResources(this.linkLabelMeteo, "linkLabelMeteo");
            this.linkLabelMeteo.Name = "linkLabelMeteo";
            this.linkLabelMeteo.TabStop = true;
            this.linkLabelMeteo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMeteo_LinkClicked);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // cbCity
            // 
            this.cbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCity.FormattingEnabled = true;
            resources.ApplyResources(this.cbCity, "cbCity");
            this.cbCity.Name = "cbCity";
            // 
            // labelCity
            // 
            resources.ApplyResources(this.labelCity, "labelCity");
            this.labelCity.Name = "labelCity";
            // 
            // chbCustomLocation
            // 
            resources.ApplyResources(this.chbCustomLocation, "chbCustomLocation");
            this.chbCustomLocation.Name = "chbCustomLocation";
            this.chbCustomLocation.UseVisualStyleBackColor = true;
            this.chbCustomLocation.CheckedChanged += new System.EventHandler(this.chbCustomLocation_CheckedChanged);
            // 
            // cbShowSidebar
            // 
            resources.ApplyResources(this.cbShowSidebar, "cbShowSidebar");
            this.cbShowSidebar.Name = "cbShowSidebar";
            this.cbShowSidebar.UseVisualStyleBackColor = true;
            // 
            // linkLabelGithub
            // 
            resources.ApplyResources(this.linkLabelGithub, "linkLabelGithub");
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGithub_LinkClicked);
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.linkLabelGithub);
            this.Controls.Add(this.cbShowSidebar);
            this.Controls.Add(this.chbCustomLocation);
            this.Controls.Add(this.labelCity);
            this.Controls.Add(this.cbCity);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.linkLabelMeteo);
            this.Controls.Add(this.labelRefresh);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.nudRefresh);
            this.Controls.Add(this.nudY);
            this.Controls.Add(this.nudX);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.cbModel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
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
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelRefresh;
        private System.Windows.Forms.LinkLabel linkLabelMeteo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.CheckBox chbCustomLocation;
        private System.Windows.Forms.CheckBox cbShowSidebar;
        private System.Windows.Forms.LinkLabel linkLabelGithub;
    }
}