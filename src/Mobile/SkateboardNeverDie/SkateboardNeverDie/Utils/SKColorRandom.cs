using SkiaSharp;
using System;

namespace SkateboardNeverDie.Utils
{
    public static class SKColorRandom
    {
        public static SKColor GetColor()
            => SKColor.Parse(GetHexColor());

        private static string GetHexColor()
        {
            var hexColor = $"#{Guid.NewGuid().ToString().Substring(0, 6)}";
            var rgb = HexToRGB(hexColor);

            // > 128 is Light.
            var colorBrightness = (299 * rgb[0] + 587 * rgb[1] + 114 * rgb[2]) / 1000;

            if (colorBrightness > 128)
                hexColor = GetHexColor();

            return hexColor.ToUpper();
        }
        private static int[] HexToRGB(string hex)
        {
            if (hex.Contains("#"))
                hex = hex.Replace("#", "");

            int[] rgb = new int[3];
            rgb[0] = Convert.ToInt32(hex.Substring(0, 2), 16);
            rgb[1] = Convert.ToInt32(hex.Substring(2, 2), 16);
            rgb[2] = Convert.ToInt32(hex.Substring(4, 2), 16);

            return rgb;
        }
    }
}
