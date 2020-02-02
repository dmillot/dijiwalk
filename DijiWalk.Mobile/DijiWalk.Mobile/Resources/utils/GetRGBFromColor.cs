//-----------------------------------------------------------------------
// <copyright file="GetRGBFromColor.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Mobile.Resources.Utils
{

    /// <summary>
    /// GetRGBFromColor : Class static to get color and convert to rgb
    /// </summary>
    public static class GetRGBFromColor
    {
        /// <summary>
        /// Converti une couleur de type Color en text fill="rgb(R,G,B)"
        /// </summary>
        /// <param name="color">Couleur a convertir</param>
        /// <returns></returns>
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
