using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IcmWeather.Forms
{
    public partial class SysTray : Form
    {
        private ContextMenu trayMenu;
        private Forecast forecastForm;
        private UserConfig userConfigForm;

        private bool allowshowdisplay = false;

        public delegate void RefreshDemandedHandler();
        public event RefreshDemandedHandler RefreshDemanded;

        public SysTray()
        {
            InitializeComponent();

            trayIcon.Text = Assembly.GetExecutingAssembly().GetName().Name;

            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Settings", ShowSettings);
            trayMenu.MenuItems.Add("Refresh", RefreshForecast);
            trayMenu.MenuItems.Add("Exit", OnExit);
            trayIcon.ContextMenu = trayMenu;

            userConfigForm = new UserConfig();
            forecastForm = new Forecast();

            RefreshDemanded += new RefreshDemandedHandler(forecastForm.ResetTimerAndLoadMeteogram);
            userConfigForm.RefreshDemanded += new UserConfig.RefreshDemandedHandler(forecastForm.ResetTimerAndLoadMeteogram);
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


    }
}
