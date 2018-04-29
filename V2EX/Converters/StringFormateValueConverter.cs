using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Html;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace V2EX.Converters
{
    public class StringFormateValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (parameter == null)
                    return value;

                if (parameter.ToString() == "https:")
                    return new BitmapImage() { UriSource = new Uri($"{parameter}{value}") };

                if (parameter.ToString() == "html")
                {
                    string html = HtmlUtilities.ConvertToText(value.ToString());
                    return html;
                }

                if (parameter.ToString() == "..." && value.ToString().Length > 52)
                    return $"{value.ToString().Substring(0, 52)}{parameter}";

                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
