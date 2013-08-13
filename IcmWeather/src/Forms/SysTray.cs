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
        private const int SHOW_TIME_THRESHOLD = 500; // ms

        public delegate void RefreshForecastDemandedHandler(object sender, EventArgs e);
        public event RefreshForecastDemandedHandler RefreshForecastDemanded;

        public SysTray()
        {
            InitializeComponent();

            forecastHelper = new ForecastHelper(settingsHelper);
            RefreshForecastDemanded += new RefreshForecastDemandedHandler(forecastHelper.NewMeteogramDemanded);
            forecastHelper.MeteogramDownloaded += new ForecastHelper.MeteogramDownloadedHandler(ChangeTrayIconToWeather);
            forecastHelper.DownloadingMeteogram += new ForecastHelper.DownloadingMeteogramHandler(ChangeTrayIconToRefresh);
            forecastHelper.ErrorDownload += new ForecastHelper.ErrorDownloadHandler(ChangeTrayIconToError);
            forecastHelper.Launch();

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
            if (((MouseEventArgs)e).Button == MouseButtons.Left 
                && Environment.TickCount - Forecast.LastClosingTick > SHOW_TIME_THRESHOLD 
                && Environment.TickCount - Forecast.LastOpeningTick > SHOW_TIME_THRESHOLD)
            {
                forecastForm = new Forecast(forecastHelper);
                if (forecastForm.MeteogramAvailable)
                {
                    forecastForm.FormClosed += ForecastFormClosed;
                    forecastForm.Show();
                }
            }
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            settingsForm = new Settings(settingsHelper, forecastHelper);
            settingsForm.FormClosed += SettingsFormClosed;
            settingsForm.Show();
        }

        private void ChangeTrayIconToWeather()
        {
            trayIcon.Icon = Properties.Resources.weather_ico;
            trayIcon.Text = Assembly.GetExecutingAssembly().GetName().Name;
        }

        private void ChangeTrayIconToRefresh()
        {
            trayIcon.Icon = Properties.Resources.refresh_ico;
            trayIcon.Text = String.Format("{0} - {1}", Assembly.GetExecutingAssembly().GetName().Name, "downloading meteogram");
        }

        private void ChangeTrayIconToError()
        {
            trayIcon.Icon = Properties.Resources.error_ico;
            trayIcon.Text = String.Format("{0} - {1}", Assembly.GetExecutingAssembly().GetName().Name, "can't download meteogram");
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
