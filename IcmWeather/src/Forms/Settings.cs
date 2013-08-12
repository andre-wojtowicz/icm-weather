using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IcmWeather.Data;

namespace IcmWeather.Forms
{
    public partial class Settings : Form
    {
        private SettingsHelper settingsHelper;
        private ForecastHelper forecastHelper;

        private const string ICM_URL = "http://www.meteo.pl";

        public delegate void RefreshForecastDemandedHandler(object sender, EventArgs e);
        public event RefreshForecastDemandedHandler RefreshForecastDemanded;

        public delegate void SettingsUpdatedHandler(ForecastModel _model, bool _customLocation, string _city, 
            uint _x, uint _y, uint _refreshRate, string _meteogramLanguage);
        public event SettingsUpdatedHandler SettingsUpdated;

        public Settings(SettingsHelper _settingsHelper, ForecastHelper _forecastHelper)
        {
            settingsHelper = _settingsHelper;
            forecastHelper = _forecastHelper;

            InitializeComponent();
            Text = Assembly.GetEntryAssembly().GetName().Name + " " + Assembly.GetEntryAssembly().GetName().Version.ToString();         

            UpdateFormControls();

            SettingsUpdated += new SettingsUpdatedHandler(settingsHelper.SettingsUpdated);
            RefreshForecastDemanded += new RefreshForecastDemandedHandler(forecastHelper.NewMeteogramDemanded);
            cbCity.SelectedIndexChanged += new EventHandler(cbCity_SelectedIndexChanged);
        }

        private void UpdateFormControls()
        {
            cbModel.DataSource    = settingsHelper.AvailableModels;
            cbModel.DisplayMember = "Name";
            cbModel.SelectedItem  = settingsHelper.ChosenModel;

            nudX.Value   = settingsHelper.ChosenX;
            nudX.Maximum = settingsHelper.ChosenModel.Xmax;
            nudX.Minimum = settingsHelper.ChosenModel.Xmin;

            nudY.Value   = settingsHelper.ChosenY;
            nudY.Maximum = settingsHelper.ChosenModel.Ymax;
            nudY.Minimum = settingsHelper.ChosenModel.Ymin;

            nudRefresh.Value = settingsHelper.ChosenRefreshRate;

            cbMeteogramLanguage.DataSource   = settingsHelper.AvailableLanguages;
            cbMeteogramLanguage.SelectedItem = settingsHelper.ChosenMeteogramLanguage;

            LoadCitiesToCombobox(settingsHelper.ChosenModel);

            if (settingsHelper.ChosenCity.Equals(""))
            {
                chbCustomLocation.Checked = true;
                nudX.Enabled = true;
                nudY.Enabled = true;
            }
            else
            {
                cbCity.Text = settingsHelper.ChosenCity;
                nudX.Enabled = false;
                nudY.Enabled = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SettingsUpdatedHandler s_handler = SettingsUpdated;
            if (s_handler != null)
                s_handler((ForecastModel)cbModel.SelectedItem, chbCustomLocation.Checked, ((City)cbCity.SelectedItem).Name,
                    (uint)nudX.Value, (uint)nudY.Value, (uint)nudRefresh.Value, (string)cbMeteogramLanguage.SelectedItem);
            
            RefreshForecastDemandedHandler r_handler = RefreshForecastDemanded;
            if (r_handler != null)
                r_handler(sender, e);

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabelMeteo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(ICM_URL);
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ForecastModel model = (ForecastModel)cbModel.SelectedItem;
            nudX.Maximum = model.Xmax;
            nudX.Minimum = model.Xmin;
            nudY.Maximum = model.Ymax;
            nudY.Minimum = model.Ymin;

            LoadCitiesToCombobox(model);
        }

        private void LoadCitiesToCombobox(ForecastModel model)
        {
            cbCity.DataSource    = model.Cities;
            cbCity.DisplayMember = "Name";
            cbCity.ValueMember   = "Location";
            cbCity.SelectedIndex = 0;
        }

        private void chbCustomLocation_CheckedChanged(object sender, EventArgs e)
        {
            nudX.Enabled = chbCustomLocation.Checked;
            nudY.Enabled = chbCustomLocation.Checked;
            cbCity.Enabled = !chbCustomLocation.Checked;

            if (!chbCustomLocation.Checked)
                cbCity_SelectedIndexChanged(null, null);
        }

        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chbCustomLocation.Checked && cbCity.Items != null)
            {
                City city = (City)cbCity.SelectedItem;
                nudX.Value = city.Location.Item1;
                nudY.Value = city.Location.Item2;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            SettingsUpdated -= settingsHelper.SettingsUpdated;
            RefreshForecastDemanded -= forecastHelper.NewMeteogramDemanded;
        }
    }
}
