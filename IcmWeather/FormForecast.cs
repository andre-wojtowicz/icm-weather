using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IcmWeather
{
    public partial class FormForecast : Form
    {
        private Timer meteogramTimer;

        public FormForecast()
        {
            InitializeComponent();
            ResetTimerAndLoadMeteogram();
        }

        protected override void OnLoad(EventArgs e)
        {
            PlaceLowerRight();
            
            base.OnLoad(e);
        }

        public void ResetTimerAndLoadMeteogram()
        {
            if (meteogramTimer != null)
                meteogramTimer.Stop();
            meteogramTimer = new Timer();
            meteogramTimer.Tick += new EventHandler(LoadMeteogram);
            meteogramTimer.Interval = (int)FormUserConfig.RefreshRate * 60 * 1000;
            LoadMeteogram(null, null); // preload image
            meteogramTimer.Start();
        }

        private void LoadMeteogram(object sender, EventArgs e)
        {
            string url = FormUserConfig.Model.MeteogramUrl;
            string x = FormUserConfig.X.ToString();
            string y = FormUserConfig.Y.ToString();
            string lang = FormUserConfig.Language;

            url = url.Replace("{X}", x).Replace("{Y}", y).Replace("{LANG}", lang);
            pbMeteogram.Load(url);
        }

        //http://stackoverflow.com/questions/15188939/form-position-on-lower-right-corner-of-the-screen
        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }

        private void pbMeteogram_SizeChanged(object sender, EventArgs e)
        {
            Width = pbMeteogram.Width + 5;
            Height = pbMeteogram.Height + 5;
            PlaceLowerRight();
        }
    }
}
