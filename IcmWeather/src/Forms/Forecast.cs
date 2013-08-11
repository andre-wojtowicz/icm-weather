﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IcmWeather.Utils;

namespace IcmWeather.Forms
{
    public partial class Forecast : Form
    {
        private Timer meteogramTimer;

        public Forecast()
        {
            InitializeComponent();
            ResetTimerAndLoadMeteogram();
        }
        
        public void ResetTimerAndLoadMeteogram()
        {
            if (meteogramTimer != null)
                meteogramTimer.Stop();
            meteogramTimer = new Timer();
            meteogramTimer.Tick += new EventHandler(LoadMeteogram);
            meteogramTimer.Interval = (int)UserConfig.RefreshRate * 60 * 1000;
            LoadMeteogram(null, null); // preload image
            meteogramTimer.Start();
        }

        private void LoadMeteogram(object sender, EventArgs e)
        {
            string url = UserConfig.Model.MeteogramUrl;
            string x = UserConfig.X.ToString();
            string y = UserConfig.Y.ToString();
            string lang = UserConfig.Language;

            url = url.Replace("{X}", x).Replace("{Y}", y).Replace("{LANG}", lang);
            pbMeteogram.Load(url);
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
            Width = pbMeteogram.Width + SystemInformation.FrameBorderSize.Width;
            Height = pbMeteogram.Height + SystemInformation.FrameBorderSize.Width;
            PlaceNearNotifyIcon();
        }

        private void FormForecast_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                PlaceNearNotifyIcon();
        }

        private void pbMeteogram_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void FormForecast_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void FormForecast_Deactivate(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}