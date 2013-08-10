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

using Ini;

namespace IcmWeather
{
    public partial class FormUserConfig : Form
    {
        public static ForecastModel Model { get; private set; }
        public static uint X { get; private set; }
        public static uint Y { get; private set; }
        public static uint RefreshRate { get; private set; }
        public static string Language { get; private set; }

        private IniFile inifile = new IniFile(Path.Combine(
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ICM Weather"), 
            "settings.ini"));
        private const string INI_SECTION          = "settings";
        private const string INI_KEY_MODEL        = "model";
        private const string INI_KEY_X            = "x";
        private const string INI_KEY_Y            = "y";
        private const string INI_KEY_REFRESH_RATE = "refresh";
        private const string INI_KEY_LANGUAGE     = "language";

        private List<ForecastModel> models = new List<ForecastModel>();
        private List<string> languages = new List<string>();

        public delegate void RefreshDemandedHandler();
        public event RefreshDemandedHandler RefreshDemanded;

        public FormUserConfig()
        {
            InitializeComponent();
            Text = Assembly.GetEntryAssembly().GetName().Name + " " + Assembly.GetEntryAssembly().GetName().Version.ToString();

            CreateModels();
            CreateLanguages();

            LoadIniFile();
            UpdateFormControls();
        }

        private void CreateModels()
        {
            models.Add(new ForecastModel("UM (60h)", 17, 430, 17, 598,
                "http://www.meteo.pl/um/metco/leg_um_pl_20120615.png",
                "http://www.meteo.pl/um/metco/mgram_pict.php?ntype=0u&col={X}&row={Y}&lang={LANG}"));

            models.Add(new ForecastModel("COAMPS (84h)", 8, 158, 10, 208,
                "http://www.meteo.pl/metco/leg4_pl.png",
                "http://www.meteo.pl/metco/mgram_pict.php?ntype=2n&col={X}&row={Y}&lang={LANG}"));

            cbModel.DataSource = models;
            cbModel.DisplayMember = "Name";
            cbModel.SelectedIndex = 0;
        }

        private void CreateLanguages()
        {
            languages.Add("en");
            languages.Add("pl");

            cbLanguage.DataSource = languages;
        }

        private void LoadIniFile()
        {
            string iniModel = inifile.IniReadValue(INI_SECTION, INI_KEY_MODEL);
            foreach (ForecastModel model in models)
            {
                if (model.Name == iniModel)
                {
                    Model = model;
                    break;
                }
            }

            X = Convert.ToUInt32(inifile.IniReadValue(INI_SECTION, INI_KEY_X));
            Y = Convert.ToUInt32(inifile.IniReadValue(INI_SECTION, INI_KEY_Y));
            RefreshRate = Convert.ToUInt32(inifile.IniReadValue(INI_SECTION, INI_KEY_REFRESH_RATE));
            Language = inifile.IniReadValue(INI_SECTION, INI_KEY_LANGUAGE);
        }

        private void SaveIniFile()
        {
            inifile.IniWriteValue(INI_SECTION, INI_KEY_MODEL, cbModel.Text);
            inifile.IniWriteValue(INI_SECTION, INI_KEY_X, nudX.Value.ToString());
            inifile.IniWriteValue(INI_SECTION, INI_KEY_Y, nudY.Value.ToString());
            inifile.IniWriteValue(INI_SECTION, INI_KEY_REFRESH_RATE, nudRefresh.Value.ToString());
            inifile.IniWriteValue(INI_SECTION, INI_KEY_LANGUAGE, cbLanguage.Text);
        }

        private void UpdateFormControls()
        {
            cbModel.SelectedItem = Model;
            nudX.Value = X;
            nudX.Maximum = Model.Xmax;
            nudX.Minimum = Model.Xmin;
            nudY.Value = Y;
            nudY.Maximum = Model.Ymax;
            nudY.Minimum = Model.Ymin;
            nudRefresh.Value = RefreshRate;
            cbLanguage.SelectedItem = Language;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Visible = false;
            SaveIniFile();

            Model = (ForecastModel)cbModel.SelectedItem;
            X = (uint)nudX.Value;
            Y = (uint)nudY.Value;
            RefreshRate = (uint)nudRefresh.Value;
            Language = (string)cbLanguage.SelectedItem;

            //ResetTimerAndLoadMeteogram()
            RefreshDemandedHandler handler = RefreshDemanded;
            if (handler != null)
                handler();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void linkLabelMeteo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.meteo.pl");
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ForecastModel model = (ForecastModel)cbModel.SelectedItem;
            nudX.Maximum = model.Xmax;
            nudX.Minimum = model.Xmin;
            nudY.Maximum = model.Ymax;
            nudY.Minimum = model.Ymin;
        }
    }
}
