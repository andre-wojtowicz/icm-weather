using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using IcmWeather.Utils;
using IcmWeather.Data;
using IcmWeather.Properties;

namespace IcmWeather.Forms
{
    public partial class Forecast : Form
    {
        private ForecastHelper forecastHelper;
        private bool _MeteogramAvailable = false;
        public bool MeteogramAvailable
        {
            get { return _MeteogramAvailable; }
            private set { _MeteogramAvailable = value; }
        }

        private static int _LastClosingTick = 0;
        public static int LastClosingTick
        {
            get { return _LastClosingTick; }
            private set { _LastClosingTick = value; }
        }
        private static int _LastOpeningTick = 0;
        public static int LastOpeningTick
        {
            get { return _LastOpeningTick; }
            private set { _LastOpeningTick = value; }
        }

        public Forecast(ForecastHelper _forecastHelper)
        {
            InitializeComponent();

            forecastHelper = _forecastHelper;
            forecastHelper.MeteogramDownloaded += new ForecastHelper.MeteogramDownloadedHandler(LoadMeteogram);

            if (_forecastHelper.Meteogram != null)
            {
                LoadMeteogram();
                MeteogramAvailable = true;
            }                

            PlaceNearNotifyIcon();
        }

        public void LoadMeteogram()
        {
            if (pbMeteogram.InvokeRequired)
                pbMeteogram.Invoke(new MethodInvoker(delegate { pbMeteogram.Image = forecastHelper.Meteogram; }));
            else
                pbMeteogram.Image = forecastHelper.Meteogram;

            // check if not too big for screen
            int s_w = Screen.PrimaryScreen.WorkingArea.Width;
            int s_h = Screen.PrimaryScreen.WorkingArea.Height;

            if (pbMeteogram.Image.Width > s_w || pbMeteogram.Image.Height > s_h)
                pbMeteogram.Image = ScaleImage(pbMeteogram.Image, s_w, s_h);
        }

        //http://stackoverflow.com/a/6501997
        private static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        private void PlaceNearNotifyIcon()
        {
            Screen primaryScreen = Screen.PrimaryScreen;
            Taskbar taskbar = new Taskbar();

            switch (taskbar.Position)
            {
                default:
                case TaskbarPosition.Bottom:
                    this.Left = primaryScreen.WorkingArea.Right - this.Width;
                    this.Top = primaryScreen.WorkingArea.Bottom - this.Height - (taskbar.AutoHide ? taskbar.Size.Height : 0);
                    break;
                case TaskbarPosition.Right:
                    this.Left = primaryScreen.WorkingArea.Right - this.Width - (taskbar.AutoHide ? taskbar.Size.Width : 0);
                    this.Top = primaryScreen.WorkingArea.Bottom - this.Height;
                    break;
                case TaskbarPosition.Top:
                    this.Left = primaryScreen.WorkingArea.Right - this.Width;
                    this.Top = primaryScreen.WorkingArea.Top + (taskbar.AutoHide ? taskbar.Size.Height : 0);;
                    break;
                case TaskbarPosition.Left:
                    this.Left = primaryScreen.WorkingArea.Left + (taskbar.AutoHide ? taskbar.Size.Width : 0);;
                    this.Top = primaryScreen.WorkingArea.Bottom - this.Height;
                    break;
            }
        }

        private void pbMeteogram_SizeChanged(object sender, EventArgs e)
        {
            Width  = pbMeteogram.Width  + SystemInformation.FrameBorderSize.Width;
            Height = pbMeteogram.Height + SystemInformation.FrameBorderSize.Height;
            PlaceNearNotifyIcon();
        }

        private void pbMeteogram_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormForecast_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormForecast_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            LastOpeningTick = Environment.TickCount;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            forecastHelper.MeteogramDownloaded -= LoadMeteogram;
            LastClosingTick = Environment.TickCount;
        }
    }
}
