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

using IcmWeather.Data;

namespace IcmWeather.Forms
{
    public partial class SysTray : Form
    {
        private ContextMenu trayMenu = new ContextMenu();
        private Forecast forecastForm;
        private Settings settingsForm;

        private ForecastHelper forecastHelper;
        private SettingsHelper settingsHelper = new SettingsHelper();

        private const bool ALLOW_SHOW_DISPLAY = false;

        public delegate void RefreshForecastDemandedHandler(object sender, EventArgs e);
        public event RefreshForecastDemandedHandler RefreshForecastDemanded;

        public SysTray()
        {
            InitializeComponent();

            forecastHelper = new ForecastHelper(settingsHelper);
            RefreshForecastDemanded += new RefreshForecastDemandedHandler(forecastHelper.NewMeteogramDemanded);

            trayIcon.Text = Assembly.GetExecutingAssembly().GetName().Name;
            trayIcon.ContextMenu = trayMenu;

            trayMenu.MenuItems.Add("Settings", ShowSettings);
            trayMenu.MenuItems.Add("Refresh", RefreshForecast);
            trayMenu.MenuItems.Add("Exit", OnExit);
        }

        public void RefreshForecast(object sender, EventArgs e)
        {
            RefreshForecastDemandedHandler handler = RefreshForecastDemanded;
            if (handler != null)
                handler(sender, e);
        }

        private void ShowForecast(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left && Environment.TickCount - Forecast.LastClosingTick > 500)
            {
                forecastForm = new Forecast(forecastHelper);
                forecastForm.FormClosed += ForecastFormClosed;
                forecastForm.Show();
            }
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            settingsForm = new Settings(settingsHelper, forecastHelper);
            settingsForm.FormClosed += SettingsFormClosed;
            settingsForm.Show();
        }

        private void ForecastFormClosed(object sender, EventArgs e)
        {
            forecastForm = null;
        }

        private void SettingsFormClosed(object sender, EventArgs e)
        {
            settingsForm = null;
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(ALLOW_SHOW_DISPLAY ? value : ALLOW_SHOW_DISPLAY);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
