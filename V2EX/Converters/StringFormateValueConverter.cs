﻿using Html2Markdown;
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
                if (value == null || parameter == null)
                    return value;

                if (parameter.ToString() == "https:")
                {
                    if (value.ToString().Contains("https"))
                        return new BitmapImage() { UriSource = new Uri($"{value}") };
                    else
                        return new BitmapImage() { UriSource = new Uri($"{parameter}{value}") };
                }

                if (parameter.ToString() == "HTML")
                {
                    var converter = new Converter();
                    
                    //ISSUE: var s1 = HtmlNode.CreateNode(value.ToString());
                    var markdown = converter.Convert(value.ToString());
                    //string html = HtmlUtilities.ConvertToText(value.ToString());

                    return markdown;
                }

                if (parameter.ToString() == "..." && value.ToString().Length > 52)
                    return $"{value.ToString().Substring(0, 52)}{parameter}";

                return value;
            }
            catch (Exception ex)
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
