using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcmWeather.Data
{
    public class City
    {
        public Tuple<ushort, ushort> Location { get; private set; }
        public string Name { get; private set; }

        public City(Tuple<ushort, ushort> _location, string _name)
        {
            Location = _location;
            Name = _name;
        }
    }
}
