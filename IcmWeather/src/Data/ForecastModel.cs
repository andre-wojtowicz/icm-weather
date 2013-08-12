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
        public string MeteogramUrl { get; private set; }
        public List<City> Cities { get; private set; }

        public ForecastModel(string _name, ushort _xmin, ushort _xmax, 
            ushort _ymin, ushort _ymax, string _meteogramUrl,
            List<City> _cities)
        {
            Name = _name;
            Xmin = _xmin;
            Xmax = _xmax;
            Ymin = _ymin;
            Ymax = _ymax;
            MeteogramUrl = _meteogramUrl;
            Cities = _cities;
        }
    }
}
