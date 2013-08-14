using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using System.Globalization;

using IcmWeather.Utils;

namespace IcmWeather.Data
{
    public class SettingsHelper
    {
        public ForecastModel ChosenModel             { get; private set; }
        public string        ChosenCity              { get; private set; }
        public uint          ChosenX                 { get; private set; }
        public uint          ChosenY                 { get; private set; }
        public uint          ChosenRefreshRate       { get; private set; }
        public bool          ChosenShowSidebar       { get; private set; }

        public string        Language                { get; private set; }
        public string        IcmWebpageUrl         { get; private set; }

        private string       defaultLanguage         = "en";

        public List<ForecastModel> AvailableModels    { get; private set; }

        private const string XML_MODEL_NAME = "/model/name";
        private const string XML_MODEL_DISPLAY_NAME = "/model/display_name";
        private const string XML_MODEL_LANGUAGES = "/model/languages";
        private const string XML_MODEL_LANGUAGE_NAME = "name";
        private const string XML_MODEL_X_MIN = "/model/x_min";
        private const string XML_MODEL_X_MAX = "/model/x_max";
        private const string XML_MODEL_Y_MIN = "/model/y_min";
        private const string XML_MODEL_Y_MAX = "/model/y_max";
        private const string XML_MODEL_METEOGRAM_URL = "/model/meteogram_url";
        private const string XML_MODEL_SIDEBAR_URL = "/model/sidebar_url";
        private const string XML_MODEL_CITIES = "/model/cities";
        private const string XML_MODEL_CITY_X = "x";
        private const string XML_MODEL_CITY_Y = "y";

        private const string INI_SETTINGS_FILENAME               = "user_settings.ini";
        private const string INI_SETTINGS_SECTION                = "settings";
        private const string INI_SETTINGS_KEY_MODEL              = "model";
        private const string INI_SETTINGS_KEY_CITY               = "city";
        private const string INI_SETTINGS_KEY_X                  = "x";
        private const string INI_SETTINGS_KEY_Y                  = "y";
        private const string INI_SETTINGS_KEY_REFRESH_RATE       = "refresh_rate";
        private const string INI_SETTINGS_KEY_SHOW_SIDEBAR       = "show_sidebar";

        private IniFile inifile = new IniFile(Path.Combine(
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Assembly.GetEntryAssembly().GetName().Name), INI_SETTINGS_FILENAME));

        private const string MODELS_DIR = "models";
        private const string ICM_URL = "http://www.meteo.pl/index{LANG}.php";
        
        public SettingsHelper()
        {
            AvailableModels    = new List<ForecastModel>();

            LoadModels();

            LoadSettings();
        }

        private void LoadModels()
        {
            Debug.WriteLine("Loading models...");

            string[] files = Directory.GetFiles(MODELS_DIR);

            foreach (string file in files)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);

                string m_name          = doc.DocumentElement.SelectSingleNode(XML_MODEL_NAME).InnerText;
                string m_display_name  = doc.DocumentElement.SelectSingleNode(XML_MODEL_DISPLAY_NAME).InnerText;

                List<string> m_languages = new List<string>();
                foreach (XmlNode node in doc.DocumentElement.SelectSingleNode(XML_MODEL_LANGUAGES).ChildNodes)
                {
                    string l_name      = node.Attributes[XML_MODEL_LANGUAGE_NAME].InnerText;
                    string l_shortcode = node.InnerText;
                    m_languages.Add(l_shortcode);
                }

                string system_lang = CultureInfo.CurrentUICulture.Parent.Name;
                Language = (m_languages.Any(system_lang.Equals) ? system_lang : defaultLanguage);

                IcmWebpageUrl = ICM_URL.Replace("{LANG}", (Language.Equals("pl") ? "" : "_"+Language));

                ushort m_xmin          = Convert.ToUInt16(doc.DocumentElement.SelectSingleNode(XML_MODEL_X_MIN).InnerText);
                ushort m_xmax          = Convert.ToUInt16(doc.DocumentElement.SelectSingleNode(XML_MODEL_X_MAX).InnerText);
                ushort m_ymin          = Convert.ToUInt16(doc.DocumentElement.SelectSingleNode(XML_MODEL_Y_MIN).InnerText);
                ushort m_ymax          = Convert.ToUInt16(doc.DocumentElement.SelectSingleNode(XML_MODEL_Y_MAX).InnerText);
                string m_meteogram_url = doc.DocumentElement.SelectSingleNode(XML_MODEL_METEOGRAM_URL).InnerText;
                string m_sidebar_url   = doc.DocumentElement.SelectSingleNode(XML_MODEL_SIDEBAR_URL).InnerText;

                List<City> m_cities = new List<City>();

                foreach (XmlNode node in doc.DocumentElement.SelectSingleNode(XML_MODEL_CITIES).ChildNodes)
                {
                    ushort c_x = Convert.ToUInt16(node.Attributes[XML_MODEL_CITY_X].InnerText);
                    ushort c_y = Convert.ToUInt16(node.Attributes[XML_MODEL_CITY_Y].InnerText);

                    string c_name = node.Attributes[Language].InnerText;

                    m_cities.Add(new City(new Tuple<ushort, ushort>(c_x, c_y), c_name));
                }

                AvailableModels.Add(new ForecastModel(m_name, m_display_name, m_xmin, m_xmax, m_ymin, m_ymax,
                    m_meteogram_url, m_sidebar_url, m_cities));
            }

            Debug.WriteLine("Models loaded.");
        }

        private void LoadSettings()
        {
            Debug.WriteLine("Loading settings...");

            string iniModel = inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_MODEL);
            foreach (ForecastModel model in AvailableModels)
                if (model.Name == iniModel)
                {
                    ChosenModel = model;
                    break;
                }

            ChosenCity        = inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_CITY);
            ChosenX           = Convert.ToUInt32(inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_X));
            ChosenY           = Convert.ToUInt32(inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_Y));
            ChosenRefreshRate = Convert.ToUInt32(inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_REFRESH_RATE));
            ChosenShowSidebar = Convert.ToBoolean(inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_SHOW_SIDEBAR));

            Debug.WriteLine("Settings loaded.");
        }

        private void SaveSettings(string _model, string _city, uint _x, uint _y, uint _refreshRate, bool _showSidebar)
        {
            Debug.WriteLine("Saving settings...");

            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_MODEL,              _model);
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_CITY,               _city);
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_X,                  _x.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_Y,                  _y.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_REFRESH_RATE,       _refreshRate.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_SHOW_SIDEBAR,       _showSidebar.ToString());

            Debug.WriteLine("Settings saved.");
        }

        public void SettingsUpdated(ForecastModel _model, bool _customLocation, string _city, uint _x, uint _y, uint _refreshRate,
            bool _showSidebar)
        {
            Debug.WriteLine("Updating settings...");

            ChosenModel = _model;

            if (_customLocation)
                _city = "";

            ChosenCity = _city;

            ChosenX = _x;
            ChosenY = _y;

            ChosenRefreshRate = _refreshRate;
            ChosenShowSidebar = _showSidebar;

            SaveSettings(_model.Name, _city, _x, _y, _refreshRate, _showSidebar);

            Debug.WriteLine("Settings updated.");
        }
    }
}
