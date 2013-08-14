using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Globalization;
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

        private ResourceManager locRM;

        private ForecastHelper forecastHelper;
        private SettingsHelper settingsHelper = new SettingsHelper();

        private const bool ALLOW_SHOW_DISPLAY = false;
        private const int SHOW_TIME_THRESHOLD = 500; // ms

        public delegate void RefreshForecastDemandedHandler(object sender, EventArgs e);
        public event RefreshForecastDemandedHandler RefreshForecastDemanded;

        public SysTray()
        {
            InitializeComponent();

            locRM = new ResourceManager("IcmWeather.src.Localizations.Messages", this.GetType().Assembly);

            forecastHelper = new ForecastHelper(settingsHelper);
            RefreshForecastDemanded += new RefreshForecastDemandedHandler(forecastHelper.NewMeteogramDemanded);
            forecastHelper.MeteogramDownloaded += new ForecastHelper.MeteogramDownloadedHandler(ChangeTrayIconToWeather);
            forecastHelper.DownloadingMeteogram += new ForecastHelper.DownloadingMeteogramHandler(ChangeTrayIconToRefresh);
            forecastHelper.ErrorDownload += new ForecastHelper.ErrorDownloadHandler(ChangeTrayIconToError);
            forecastHelper.Launch();

            trayIcon.Text = Assembly.GetExecutingAssembly().GetName().Name;
            trayIcon.ContextMenu = trayMenu;

            trayMenu.MenuItems.Add(locRM.GetString("settings"), ShowSettings);
            trayMenu.MenuItems.Add(locRM.GetString("refresh"), RefreshForecast);
            trayMenu.MenuItems.Add(locRM.GetString("exit"), OnExit);
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
                && Environment.TickCount - Forecast.LastOpeningTick > SHOW_TIME_THRESHOLD
                && forecastForm == null)
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
            if (settingsForm == null)
            {
                settingsForm = new Settings(settingsHelper, forecastHelper);
                settingsForm.FormClosed += SettingsFormClosed;
                settingsForm.Show();
            }
        }

        private void ChangeTrayIconToWeather()
        {
            trayIcon.Icon = Properties.Resources.weather_ico;
            trayIcon.Text = Assembly.GetExecutingAssembly().GetName().Name;
        }

        private void ChangeTrayIconToRefresh()
        {
            trayIcon.Icon = Properties.Resources.refresh_ico;
            trayIcon.Text = String.Format("{0} - {1}", Assembly.GetExecutingAssembly().GetName().Name, locRM.GetString("downloading"));
        }

        private void ChangeTrayIconToError()
        {
            trayIcon.Icon = Properties.Resources.error_ico;
            trayIcon.Text = String.Format("{0} - {1}", Assembly.GetExecutingAssembly().GetName().Name, locRM.GetString("error_download"));
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
