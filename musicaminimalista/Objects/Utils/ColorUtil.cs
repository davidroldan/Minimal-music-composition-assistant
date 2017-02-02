using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MusicaMinimalista.Objects.Utils
{
    public class ColorUtil
    {
        public static Color increaseBrightness(Color color, int percentage)
        {
            int r = (255 - color.R) * percentage / 100;
            int g = (255 - color.G) * percentage / 100;
            int b = (255 - color.B) * percentage / 100;
            return Color.FromArgb(color.A, color.R + r, color.G + g, color.B + b);
        }

        public static Color decreaseBrightness(Color color, int percentage)
        {
            int r = color.R * percentage / 100;
            int g = color.G * percentage / 100;
            int b = color.B * percentage / 100;
            return Color.FromArgb(color.A, r, g, b);
        }
    }
}
