using System;

using V2EX.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace V2EX.Views
{
    public sealed partial class HomeDetailControl : UserControl
    {
        public TopicModel MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as TopicModel; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(TopicModel), typeof(HomeDetailControl), new PropertyMetadata(null));

        public HomeDetailControl()
        {
            InitializeComponent();
        }
    }
}
