using System;

using V2EX.ViewModels;

using Windows.UI.Xaml.Controls;

namespace V2EX.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
