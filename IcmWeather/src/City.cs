using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcmWeather
{
    public class City
    {
        public Tuple<ushort, ushort> Location { get; private set; }
        public string Name { get; private set; }

        public City(Tuple<ushort, ushort> location, string name)
        {
            Location = location;
            Name = name;
        }
    }
}
