using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace V2EX.UI.ViewModels
{
    public class ShellNavigationItem:ViewModelBase
    {
        public string Src { get; set; }
        public string Label { get; set; }

        private bool _isEnable;
        public bool IsEnable
        {
            get { return _isEnable; }
            set { Set(ref _isEnable, value); }
        }

        public ShellNavigationItem(string src, string label, bool isEnable)
        {
            Src = src;
            Label = label;
            IsEnable = isEnable;
        }
    }
}
