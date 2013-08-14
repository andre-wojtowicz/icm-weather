using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcmWeather.Data
{
    public class ForecastModel
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public ushort Xmin { get; private set; }
        public ushort Xmax { get; private set; }
        public ushort Ymin { get; private set; }
        public ushort Ymax { get; private set; }
        public string MeteogramUrl { get; private set; }
        public string SidebarUrl { get; private set; }
        public List<City> Cities { get; private set; }

        public ForecastModel(string _name, string _displayName, ushort _xmin, ushort _xmax,
            ushort _ymin, ushort _ymax, string _meteogramUrl, string _sidebarUrl,
            List<City> _cities)
        {
            Name = _name;
            DisplayName = _displayName;
            Xmin = _xmin;
            Xmax = _xmax;
            Ymin = _ymin;
            Ymax = _ymax;
            MeteogramUrl = _meteogramUrl;
            SidebarUrl = _sidebarUrl;
            Cities = _cities;
        }
    }
}
