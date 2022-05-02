using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnnWPF.Models
{
    internal static class ColorExtension
    {
        public static Double AveragedValueOfRgb(this Color pixel) =>
            (pixel.R + pixel.G + pixel.B) / 3.0;
    }
}
