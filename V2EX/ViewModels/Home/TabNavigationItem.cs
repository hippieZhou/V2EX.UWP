using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.ViewModels
{
    public class TabNavigationItem : ViewModelBase
    {
        public string Label { get; set; }

        public string Name { get; set; }


        public TabNavigationItem(string label, string name)
        {
            Label = label;
            Name = name;
        }
    }
}
