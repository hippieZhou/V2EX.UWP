using System;

using V2EX.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace V2EX.Views
{
    public sealed partial class HomeDetailControl : UserControl
    {
        public SampleOrder MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as SampleOrder; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(HomeDetailControl), new PropertyMetadata(null));

        public HomeDetailControl()
        {
            InitializeComponent();
        }
    }
}
