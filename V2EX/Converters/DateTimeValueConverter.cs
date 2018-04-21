using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace V2EX.Converters
{
    public class DateTimeValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime current = dt.AddSeconds(long.Parse(value.ToString()));
                return current.ToString("HH:ss:mm");
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var date = new DateTime(1970, 1, 1, 0, 0, 0);
                DateTime time = DateTime.Parse(value.ToString());
                var unixTimestamp = System.Convert.ToInt64((time - date).TotalSeconds);
                return unixTimestamp;
            }
            else
                return new DateTime(1970, 1, 1, 0, 0, 0);
        }
    }
}
