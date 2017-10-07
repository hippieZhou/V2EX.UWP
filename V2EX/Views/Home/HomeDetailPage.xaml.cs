using System;

using V2EX.Models;
using V2EX.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace V2EX.Views
{
    public sealed partial class HomeDetailPage : Page
    {
        private HomeDetailViewModel ViewModel
        {
            get { return DataContext as HomeDetailViewModel; }
        }

        public HomeDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Item = e.Parameter as TopicModel;
        }
    }
}
