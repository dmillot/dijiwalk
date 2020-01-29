using System;
using System.Globalization;
using Xamarin.Forms;

namespace DijiWalk.Mobile.Resources.converters
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? "finis.png" : "en_cours.png";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value == "finis.png") ? true : false;
        }
    }
}
