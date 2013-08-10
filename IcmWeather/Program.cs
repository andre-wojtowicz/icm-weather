using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IcmWeather
{
    public class SysTrayApp : Form
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new SysTrayApp());
        }

        //private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        private FormForecast forecastForm;
        private FormUserConfig userConfigForm;
        private NotifyIcon trayIcon;
        private System.ComponentModel.IContainer components;

        private bool allowshowdisplay = false;

        public delegate void RefreshDemandedHandler();
        public event RefreshDemandedHandler RefreshDemanded;

        public SysTrayApp()
        {
            InitializeComponent();

            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Settings", ShowSettings);
            trayMenu.MenuItems.Add("Refresh", RefreshForecast);
            trayMenu.MenuItems.Add("Exit", OnExit);
            trayIcon.ContextMenu = trayMenu;

            userConfigForm = new FormUserConfig();
            forecastForm = new FormForecast();

            RefreshDemanded += new RefreshDemandedHandler(forecastForm.ResetTimerAndLoadMeteogram);
            userConfigForm.RefreshDemanded += new FormUserConfig.RefreshDemandedHandler(forecastForm.ResetTimerAndLoadMeteogram);
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            userConfigForm.Visible = true;
        }

        public void RefreshForecast(object sender, EventArgs e)
        {
            RefreshDemandedHandler handler = RefreshDemanded;
            if (handler != null)
                handler();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void trayIcon_MouseClick(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
                forecastForm.Visible = forecastForm.Visible ? false : true;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysTrayApp));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "ICM Weather";
            this.trayIcon.Visible = true;
            this.trayIcon.Click += new System.EventHandler(this.trayIcon_MouseClick);
            // 
            // SysTrayApp
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "SysTrayApp";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }
    }
}
