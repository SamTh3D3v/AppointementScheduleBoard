using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ControlLibrary.Converters
{
    public class TimeSpanToDateTimeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;            
            return new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,((TimeSpan)value).Hours, ((TimeSpan)value).Minutes, ((TimeSpan)value).Seconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return new TimeSpan(((DateTime)value).Hour, ((DateTime)value).Minute, ((DateTime)value).Second);            
        }
    }
}
