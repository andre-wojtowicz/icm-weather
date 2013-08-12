using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        public string        ChosenMeteogramLanguage { get; private set; }

        public List<ForecastModel> AvailableModels    { get; private set; }
        public List<string>        AvailableLanguages { get; private set; }

        private const string INI_SETTINGS_FILENAME               = "user_settings.ini";
        private const string INI_SETTINGS_SECTION                = "settings";
        private const string INI_SETTINGS_KEY_MODEL              = "model";
        private const string INI_SETTINGS_KEY_CITY               = "city";
        private const string INI_SETTINGS_KEY_X                  = "x";
        private const string INI_SETTINGS_KEY_Y                  = "y";
        private const string INI_SETTINGS_KEY_REFRESH_RATE       = "refresh_rate";
        private const string INI_SETTINGS_KEY_METEOGRAM_LANGUAGE = "meteogram_language";

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
        private const string INI_MODELBASE_KEY_METEOGRAM_URL = "meteogram_url";

        private const string CSV_CITIES_FILENAME    = "cities.csv";
        private const string CSV_LANGUAGES_FILENAME = "languages.csv";
        
        public SettingsHelper()
        {
            AvailableModels    = new List<ForecastModel>();
            AvailableLanguages = new List<string>();

            LoadModels();
            LoadLanguages();

            LoadSettings();
        }

        private void LoadModels()
        {
            string[] dirs = Directory.GetDirectories(MODELS_DIR);

            foreach (string dir in dirs)
            {
                IniFile inibase = new IniFile(Path.Combine(dir, INI_MODELBASE_FILENAME));

                string m_name          = inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_NAME);
                ushort m_xmin          = Convert.ToUInt16(inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_X_MIN));
                ushort m_xmax          = Convert.ToUInt16(inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_X_MAX));
                ushort m_ymin          = Convert.ToUInt16(inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_Y_MIN));
                ushort m_ymax          = Convert.ToUInt16(inibase.IniReadValue(INI_MODELBASE_SECTION, INI_MODELBASE_KEY_Y_MAX));
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

                AvailableModels.Add(new ForecastModel(m_name, m_xmin, m_xmax, m_ymin, m_ymax, m_meteogram_url, m_cities));
            }
        }

        private void LoadLanguages()
        {
            List<string> langs = new List<string>();

            List<string> lines = new List<string>(System.IO.File.ReadAllLines(CSV_LANGUAGES_FILENAME));
            lines.RemoveAt(0); // remove header

            foreach (string line in lines)
                AvailableLanguages.Add(line);

        }

        private void LoadSettings()
        {
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
            ChosenMeteogramLanguage    = inifile.IniReadValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_METEOGRAM_LANGUAGE);
        }

        private void SaveSettings(string _model, string _city, uint _x, uint _y, uint _refreshRate, string _meteogramLanguage)
        {
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_MODEL,              _model);
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_CITY,               _city);
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_X,                  _x.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_Y,                  _y.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_REFRESH_RATE,       _refreshRate.ToString());
            inifile.IniWriteValue(INI_SETTINGS_SECTION, INI_SETTINGS_KEY_METEOGRAM_LANGUAGE, _meteogramLanguage);
        }

        public void SettingsUpdated(ForecastModel _model, bool _customLocation, string _city, uint _x, uint _y, uint _refreshRate, string _meteogramLanguage)
        {
            ChosenModel = _model;

            if (_customLocation)
                _city = "";

            ChosenCity = _city;

            ChosenX = _x;
            ChosenY = _y;

            ChosenRefreshRate = _refreshRate;
            ChosenMeteogramLanguage = _meteogramLanguage;

            SaveSettings(_model.Name, _city, _x, _y, _refreshRate, _meteogramLanguage);
        }
    }
}
