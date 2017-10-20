using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using V2EX.UWP.Helpers;
using V2EX.UWP.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace V2EX.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        public readonly SystemNavigationManager NavigationManager = SystemNavigationManager.GetForCurrentView();
        private ShellViewModel ViewModel
        {
            get { return DataContext as ShellViewModel; }
        }
        public ShellPage()
        {
            this.InitializeComponent();
            ViewModel.Initialize(this.contentFrame);

            NavigationManager.BackRequested += NavigationManager_BackRequested;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            #region SetupTitleBar

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonForegroundColor = Colors.Black;
                    titleBar.ButtonBackgroundColor = Colors.Transparent;
                    titleBar.BackgroundColor = Colors.Transparent;
                    titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

                    CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                }
            }

            #endregion
        }

        private void NavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var nav = ViewModel.NavigationService;
            if (nav.CanGoBack)
            {
                nav.GoBack();
                e.Handled = true;
            }
        }
    }
}
