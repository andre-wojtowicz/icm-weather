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
        public Hashtable Names { get; private set; }

        public City(Tuple<ushort, ushort> _location, Hashtable _names)
        {
            Location = _location;
            Names = _names;
        }
    }
}
