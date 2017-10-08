using Microsoft.Practices.ServiceLocation;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using V2EX.UI.ViewModels;
using V2EX.UI.ViewModels.Explore;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace V2EX.UI.Views.Explore
{
    public sealed partial class ExploreControl : UserControl
    {
        private ExploreViewModel ViewModel
        {
            get { return DataContext as ExploreViewModel; }
        }
        public ExploreControl()
        {
            this.InitializeComponent();
        }

        private void MasterDetailsView_ViewStateChanged(object sender, MasterDetailsViewState e)
        {
            switch (e)
            {
                case MasterDetailsViewState.Master:
                        ServiceLocator.Current.GetInstance<ShellViewModel>().HamburgerMenuVisibility = Visibility.Visible;
                    break;
                case MasterDetailsViewState.Details:
                        ServiceLocator.Current.GetInstance<ShellViewModel>().HamburgerMenuVisibility = Visibility.Collapsed;
                        ServiceLocator.Current.GetInstance<ShellViewModel>().IsPaneOpen = false;
                    break;
                case MasterDetailsViewState.Both:
                    ServiceLocator.Current.GetInstance<ShellViewModel>().HamburgerMenuVisibility = Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<ShellViewModel>().IsPaneOpen = true;
                    break;
                default:
                    break;
            }
        }
    }
}
