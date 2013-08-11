using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using IcmWeather.Forms;

namespace IcmWeather
{
    public class IcmWeatherApp
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new SysTray());
        }
    }
}
