using Microsoft.Practices.ServiceLocation;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.UI.ViewModels;
using Windows.UI.Xaml;

namespace V2EX.UI.Services
{
    public class NavigationServiceEx
    {
        public static void SetHamburgerMenuVisibility(MasterDetailsViewState e)
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
