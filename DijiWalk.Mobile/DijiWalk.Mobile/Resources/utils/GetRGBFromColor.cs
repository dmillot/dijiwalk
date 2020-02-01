using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Mobile.Resources.utils
{
    public static class GetRGBFromColor
    {
        public static string GetRGBFill(Xamarin.Forms.Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var rgbFill = $"fill=\"rgb({red},{green},{blue})\"";
            return rgbFill;
        }
    }
}
