namespace IcmWeather.Forms
{
    partial class Forecast
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
            this.pbMeteogram = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMeteogram)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMeteogram
            // 
            this.pbMeteogram.InitialImage = null;
            this.pbMeteogram.Location = new System.Drawing.Point(0, 0);
            this.pbMeteogram.Margin = new System.Windows.Forms.Padding(0);
            this.pbMeteogram.Name = "pbMeteogram";
            this.pbMeteogram.Size = new System.Drawing.Size(20, 20);
            this.pbMeteogram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMeteogram.TabIndex = 0;
            this.pbMeteogram.TabStop = false;
            this.pbMeteogram.SizeChanged += new System.EventHandler(this.pbMeteogram_SizeChanged);
            this.pbMeteogram.Click += new System.EventHandler(this.pbMeteogram_Click);
            // 
            // Forecast
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(34, 34);
            this.ControlBox = false;
            this.Controls.Add(this.pbMeteogram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Forecast";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FormForecast_Deactivate);
            this.Click += new System.EventHandler(this.FormForecast_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pbMeteogram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMeteogram;

    }
}

