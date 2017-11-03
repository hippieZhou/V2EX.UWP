using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.WebUI;
using Windows.UI.Core;
using GalaSoft.MvvmLight.Views;
using V2EX.UWP.Services;
using System.Diagnostics;
using Windows.UI.Xaml.Navigation;

namespace V2EX.UWP.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ExNavigationService NavigationService => ServiceLocator.Current.GetInstance<ExNavigationService>();

        private NavigationViewItem _selectedItem;
        public NavigationViewItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
                if (SelectedItem != null)
                    NavigationService.Navigate(SelectedItem.Tag.ToString());
            }
        }

        private ObservableCollection<NavigationViewItem> _primaryItems = new ObservableCollection<NavigationViewItem>();
        public ObservableCollection<NavigationViewItem> PrimaryItems
        {
            get { return _primaryItems; }
            set { Set(ref _primaryItems, value); }
        }

        private RelayCommand<NavigationViewItemInvokedEventArgs> _itemInvokedCommand;
        public RelayCommand<NavigationViewItemInvokedEventArgs> ItemInvokedCommand
        {
            get
            {
                return _itemInvokedCommand
                    ?? (_itemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(
                    args =>
                    {
                        SelectedItem = PrimaryItems.FirstOrDefault(p => p.Content == args.InvokedItem);
                    }));
            }
        }

        public void Initialize(Frame frame)
        {
            NavigationService.Frame = frame;
            NavigationService.Navigated += NavigationService_Navigated;

            PopulateNavItems();
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            SelectedItem = PrimaryItems.FirstOrDefault(p => p.Tag.ToString() == NavigationService.CurrentPageKey);
        }

        private void PopulateNavItems()
        {
            PrimaryItems.Clear();

            PrimaryItems.Add(new NavigationViewItem { Tag = typeof(HomeViewModel).FullName, Icon = new SymbolIcon(Symbol.Home), Content = "首页" });
            PrimaryItems.Add(new NavigationViewItem { Tag = typeof(NodesViewModel).FullName, Icon = new SymbolIcon(Symbol.ViewAll), Content = "节点" });
            PrimaryItems.Add(new NavigationViewItem { Tag = typeof(MessageViewModel).FullName, Icon = new SymbolIcon(Symbol.Message), Content = "通知" });
            PrimaryItems.Add(new NavigationViewItem { Tag = typeof(LibraryViewModel).FullName, Icon = new SymbolIcon(Symbol.Library), Content = "收藏" });

            SelectedItem = PrimaryItems.FirstOrDefault();
        }
    }
}
