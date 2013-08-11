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
using IcmWeather.Utils;

namespace IcmWeather.Forms
{
    public partial class UserConfig : Form
    {
        public static ForecastModel Model { get; private set; }
        public static string City { get; private set; }
        public static uint X { get; private set; }
        public static uint Y { get; private set; }
        public static uint RefreshRate { get; private set; }
        public static string Language { get; private set; }

        private const string INI_SETTINGS_FILENAME         = "user_settings.ini";
        private const string INI_SETTINGS_SECTION          = "settings";
        private const string INI_SETTINGS_KEY_MODEL        = "model";
        private const string INI_SETTINGS_KEY_CITY         = "city";
        private const string INI_SETTINGS_KEY_X            = "x";
        private const string INI_SETTINGS_KEY_Y            = "y";
        private const string INI_SETTINGS_KEY_REFRESH_RATE = "refresh";
        private const string INI_SETTINGS_KEY_LANGUAGE     = "language";

        private IniFile inifile = new IniFile(Path.Combine(
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Assembly.GetEntryAssembly().GetName().Name), INI_SETTINGS_FILENAME));

        private const string MODELS_DIR = "models";

        private const string INI_MODELBASE_FILENAME          = "base.ini";
        private const string INI_MODELBASE_SECTION           = "base";
        private const string INI_MODELBASE_KEY_NAME          = "name";
        private const string INI_MODELBASE_KEY_X_MIN         = "x_min";
        private const string INI_MODELBASE_KEY_X_MAX         = "x_max";
        private const string INI_MODELBASE_KEY_Y_MIN         = "y_min";
        private const string INI_MODELBASE_KEY_Y_MAX         = "y_max";
        private const string INI_MODELBASE_KEY_SIDEBAR_URL   = "sidebar_url";
        private const string INI_MODELBASE_KEY_METEOGRAM_URL = "meteogram_url";

        private const string CSV_CITIES_FILENAME = "cities.csv";
        private const string CSV_LANGUAGES_FILENAME = "languages.csv";
        private const string ICM_URL = "http://www.meteo.pl";

        private List<ForecastModel> models = new List<ForecastModel>();
        private List<string> languages = new List<string>();

        public delegate void RefreshDemandedHandler();
        public event RefreshDemandedHandler RefreshDemanded;

        public UserConfig()
        {
            InitializeComponent();
            Text = Assembly.GetEntryAssembly().GetName().Name + " " + Assembly.GetEntryAssembly().GetName().Version.ToString();

            CreateModels();
            CreateLanguages();

            LoadIniFile();
            UpdateFormControls();

            cbCity.SelectedIndexChanged += new EventHandler(cbCity_SelectedIndexChanged);
        }

        private void CreateModels()
        {
            string[] dirs = Directory.GetDirectories(MODELS_DIR);

            foreach (string dir in dirs)
            {
                IniFile inibase = new IniFile(Path.Combine(dir, INI_MODELBASE_FILENAME));

                string m_name = inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_NAME);
                ushort m_xmin = Convert.ToUInt16(inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_X_MIN));
                ushort m_xmax = Convert.ToUInt16(inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_X_MAX));
                ushort m_ymin = Convert.ToUInt16(inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_Y_MIN));
                ushort m_ymax = Convert.ToUInt16(inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_Y_MAX));
                string m_sidebar_url   = inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_SIDEBAR_URL);
                string m_meteogram_url = inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_METEOGRAM_URL);

                List<City> m_cities = new List<City>();

                List<string> cities_lines = new List<string>(System.IO.File.ReadAllLines(Path.Combine(dir, CSV_CITIES_FILENAME), new UTF8Encoding()));
                cities_lines.RemoveAt(0); // remove header

                foreach (string line in cities_lines)
                {
                    List<string> fields = new List<string>(line.Split(','));

                    ushort c_x = Convert.ToUInt16(fields[0]);
                    ushort c_y = Convert.ToUInt16(fields[1]);
                    string c_n = fields[2];

                    m_cities.Add(new City(new Tuple<ushort, ushort>(c_x, c_y), c_n));
                }

                models.Add(new ForecastModel(m_name, m_xmin, m_xmax, m_ymin, m_ymax, m_sidebar_url, m_meteogram_url, m_cities));
            }

            cbModel.DataSource = models;
            cbModel.DisplayMember = "Name";
            cbModel.SelectedIndex = 0;
        }

        private void CreateLanguages()
        {
            List<string> langs = new List<string>();

            List<string> lines = new List<string>(System.IO.File.ReadAllLines(CSV_LANGUAGES_FILENAME));
            lines.RemoveAt(0); // remove header

            foreach (string line in lines)
                languages.Add(line);

            cbLanguage.DataSource = languages;
        }

        private void LoadIniFile()
        {
            string iniModel = inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_MODEL);
            foreach (ForecastModel model in models)
            {
                if (model.Name == iniModel)
                {
                    Model = model;
                    break;
                }
            }

            City = inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_CITY);
            X = Convert.ToUInt32(inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_X));
            Y = Convert.ToUInt32(inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_Y));
            RefreshRate = Convert.ToUInt32(inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_REFRESH_RATE));
            Language = inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_LANGUAGE);
        }

        private void SaveIniFile()
        {
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_MODEL, cbModel.Text);
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_CITY, chbCustomLocation.Checked ? "" : cbCity.Text);
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_X, chbCustomLocation.Checked ? nudX.Value.ToString() : ((Tuple<ushort, ushort>)cbCity.SelectedValue).Item1.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_Y, chbCustomLocation.Checked ? nudY.Value.ToString() : ((Tuple<ushort, ushort>)cbCity.SelectedValue).Item2.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_REFRESH_RATE, nudRefresh.Value.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_LANGUAGE, cbLanguage.Text);
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

            LoadCitiesToCombobox(null);

            if (City.Equals(""))
                chbCustomLocation.Checked = true;
            else
                cbCity.Text = City;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Visible = false;
            SaveIniFile();

            Model = (ForecastModel)cbModel.SelectedItem;
            if (chbCustomLocation.Checked)
            {
                X = (uint)nudX.Value;
                Y = (uint)nudY.Value;
            }
            else
            {
                City city = (City)cbCity.SelectedItem;
                X = city.Location.Item1;
                Y = city.Location.Item2;
            }

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
            LoadIniFile();
            UpdateFormControls();
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
            if (Model != null)
            {
                cbCity.DataSource = (model == null ? Model.Cities : model.Cities);
                cbCity.DisplayMember = "Name";
                cbCity.ValueMember = "Location";
                cbCity.SelectedIndex = 0;
            }
            
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

 
    }
}
