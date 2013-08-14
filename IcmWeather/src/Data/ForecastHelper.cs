using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace IcmWeather.Data
{
    public class ForecastHelper
    {
        public Image Meteogram { get; private set; }
        private Timer meteogramTimer;

        private BackgroundWorker worker = new BackgroundWorker();

        private enum DownloadStatus { OK, ERROR }
        private const int DOWNLOAD_RETRY_INTERVAL = 10; // seconds

        private const string TITLEBAR_FONT_NAME = "Verdana";
        private const int TITLEBAR_FONT_SIZE = 20;
        private Brush TITLEBAR_TEXT_COLOR = Brushes.Black;

        private SettingsHelper settingsHelper;

        public delegate void DownloadingMeteogramHandler();
        public event DownloadingMeteogramHandler DownloadingMeteogram;

        public delegate void MeteogramDownloadedHandler();
        public event MeteogramDownloadedHandler MeteogramDownloaded;

        public delegate void ErrorDownloadHandler();
        public event ErrorDownloadHandler ErrorDownload;

        public ForecastHelper(SettingsHelper _settingsHelper)
        {
            settingsHelper = _settingsHelper;
            worker.DoWork += new DoWorkEventHandler(bw_DoWork);
        }

        public void Launch()
        {
            NewMeteogramDemanded(null, null); // TODO: background worker
        }

        public void NewMeteogramDemanded(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (meteogramTimer != null)
                meteogramTimer.Stop();

            DownloadStatus s = DownloadMeteogram();
            if (s == DownloadStatus.OK)
            {
                AddTitleBar(settingsHelper.ChosenModel.DisplayName, settingsHelper.ChosenCity);

                MeteogramDownloadedHandler md_handler = MeteogramDownloaded;
                if (md_handler != null)
                    md_handler();

                StartTimer((int)settingsHelper.ChosenRefreshRate * 60); // minutes to seconds
            }
            else // ERROR
                StartTimer(DOWNLOAD_RETRY_INTERVAL);
        }

        private void StartTimer(int secs)
        {
            meteogramTimer = new Timer();
            meteogramTimer.Elapsed += new ElapsedEventHandler(NewMeteogramDemanded);
            meteogramTimer.Interval = secs * 1000;
            meteogramTimer.Start();
        }

        private DownloadStatus DownloadMeteogram()
        {
            Debug.WriteLine("Downloading meteogram...");

            DownloadingMeteogramHandler dm_handler = DownloadingMeteogram;
            if (dm_handler != null)
                dm_handler();

            string url = settingsHelper.ChosenModel.MeteogramUrl;
            string x = settingsHelper.ChosenX.ToString();
            string y = settingsHelper.ChosenY.ToString();
            string lang = settingsHelper.ChosenMeteogramLanguage;

            url = url.Replace("{X}", x).Replace("{Y}", y).Replace("{LANG}", lang);

            //http://stackoverflow.com/a/4071052
            var request = WebRequest.Create(url);

            try
            {
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    Meteogram = Bitmap.FromStream(stream);
                }
            }
            catch (System.Net.WebException err)
            {
                Debug.WriteLine("Error downloading meteogram: " + err.Message);
                Debug.WriteLine("Retry in " + DOWNLOAD_RETRY_INTERVAL.ToString() + " seconds.");

                ErrorDownloadHandler ed_handler = ErrorDownload;
                if (ed_handler != null)
                    ed_handler();

                return DownloadStatus.ERROR;
            }

            Debug.WriteLine("Download OK.");

            return DownloadStatus.OK;
        }

        private void AddTitleBar(string modelName, string cityName)
        {
            Font font = new Font(TITLEBAR_FONT_NAME, TITLEBAR_FONT_SIZE);

            SizeF stringSize = new SizeF();

            using (var bmp = new Bitmap(Meteogram.Width, Meteogram.Height))
            {
                var tmp = Graphics.FromImage(bmp);
                tmp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                stringSize = tmp.MeasureString(modelName, font);
            }

            var newMeteogram = new Bitmap(Meteogram.Width,
                Meteogram.Height + (int)stringSize.Height);

            var gr = Graphics.FromImage(newMeteogram);
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gr.DrawImageUnscaled(Meteogram, 0, (int)stringSize.Height);

            gr.DrawString(modelName, font, TITLEBAR_TEXT_COLOR, new PointF(0, 0));

            if (!cityName.Equals(""))
            {
                stringSize = gr.MeasureString(cityName, font);
                gr.DrawString(cityName, font, TITLEBAR_TEXT_COLOR,
                    new PointF(Meteogram.Width - stringSize.Width, 0));
            }

            Meteogram = newMeteogram;
        }
    }
}
