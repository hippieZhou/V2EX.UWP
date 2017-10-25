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

namespace V2EX.UWP.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ExNavigationService NavigationService => ServiceLocator.Current.GetInstance<ExNavigationService>();

        private object _title;
        public object Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private NavigationViewItem _selectedItem;
        public NavigationViewItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                    Set(ref _selectedItem, value);
                this.Title = this.SelectedItem.Content;
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
                        if (args.InvokedItem == this.SelectedItem.Content)
                            return;

                        this.SelectedItem = this.PrimaryItems.FirstOrDefault(p => p.Content == args.InvokedItem);
                        this.NavigationService.Navigate(this.SelectedItem.Tag.ToString());
                    }));
            }
        }


        public void Initialize(Frame frame)
        {
            NavigationService.Frame = frame;
            NavigationService.CurrentPageKeyEventHanler += (sender, e) => 
            {
                this.SelectedItem = this.PrimaryItems.FirstOrDefault(p => p.Tag == e);
            };
            PopulateNavItems();
        }

        private void PopulateNavItems()
        {
            this.PrimaryItems.Clear();
            this.PrimaryItems.Add(new NavigationViewItem { Tag = typeof(HomeViewModel).FullName, Icon = new SymbolIcon(Symbol.Home), Content = "首页" });
            this.PrimaryItems.Add(new NavigationViewItem { Tag = typeof(NodesViewModel).FullName, Icon = new SymbolIcon(Symbol.ViewAll), Content = "节点" });
            this.PrimaryItems.Add(new NavigationViewItem { Tag = typeof(MessageViewModel).FullName, Icon = new SymbolIcon(Symbol.Message), Content = "通知" });
            this.PrimaryItems.Add(new NavigationViewItem { Tag = typeof(LibraryViewModel).FullName, Icon = new SymbolIcon(Symbol.Library), Content = "收藏" });

            this.SelectedItem = this.PrimaryItems.FirstOrDefault();
            this.NavigationService.Navigate(this.SelectedItem.Tag.ToString());
        }

        private void NavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            //if (this.NavigationService.CanGoBack)
            //{
            //    this.NavigationService.GoBack();
            //    e.Handled = true;
            //}
        }
    }
}
