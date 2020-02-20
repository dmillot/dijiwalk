using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DijiWalk.Mobile.Resources.converters
{
    public class IntToStringConverter : IValueConverter
    {

        #region IValueConverter Members

        // Define the Convert method to change a DateTime object to 
        // a month string.
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            return value.ToString();
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo language)
        {
            return (int)value;
        }

        #endregion
    }
}
