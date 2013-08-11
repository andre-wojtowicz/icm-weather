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
        public ushort Xmin { get; private set; }
        public ushort Xmax { get; private set; }
        public ushort Ymin { get; private set; }
        public ushort Ymax { get; private set; }
        public string SidebarUrl { get; private set; }
        public string MeteogramUrl { get; private set; }
        public List<City> Cities { get; private set; }

        public ForecastModel(string name, ushort xmin, ushort xmax, 
            ushort ymin, ushort ymax, string sidebarUrl, string meteogramUrl,
            List<City> cities)
        {
            Name = name;
            Xmin = xmin;
            Xmax = xmax;
            Ymin = ymin;
            Ymax = ymax;
            SidebarUrl = sidebarUrl;
            MeteogramUrl = meteogramUrl;
            Cities = cities;
        }
    }
}
