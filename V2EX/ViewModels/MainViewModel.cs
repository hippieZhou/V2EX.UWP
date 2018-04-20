using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Commons;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace V2EX.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationService NavigationService => ServiceLocator.Current.GetInstance<NavigationService>();

        private object _header;
        public object Header
        {
            get { return _header; }
            set { Set(ref _header, value); }
        }

        private ObservableCollection<NavigationViewItemBase> _primaryMenus = new ObservableCollection<NavigationViewItemBase>();
        public ObservableCollection<NavigationViewItemBase> PrimaryMenus
        {
            get { return _primaryMenus; }
            set { Set(ref _primaryMenus, value); }
        }

        private NavigationViewItem _selectedMenu;
        public NavigationViewItem SelectedMenu
        {
            get { return _selectedMenu; }
            set { Set(ref _selectedMenu, value); }
        }

        private RelayCommand<NavigationViewSelectionChangedEventArgs> _itemSelectedCmd;
        public RelayCommand<NavigationViewSelectionChangedEventArgs> ItemSelectedCmd
        {
            get
            {
                return _itemSelectedCmd ?? (_itemSelectedCmd = new RelayCommand<NavigationViewSelectionChangedEventArgs>(args =>
                {
                    NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                    if (item == null)
                        return;
                    NavigateToByNavigationViewItem(item);
                }));
            }
        }

        public void Initialize(Frame contentFrame)
        {
            InitializeNavigationService(contentFrame);

            InitializeMainMenus();
        }

        #region 初始化菜单
        private void InitializeMainMenus()
        {
            this.PrimaryMenus.Clear();
            PrimaryMenus.Add(CreateNavigationViewItem(new SymbolIcon(Symbol.Home), "PrimaryMenus_Home".GetLocalized(), typeof(HomeViewModel).FullName));
            PrimaryMenus.Add(CreateNavigationViewItem(new SymbolIcon(Symbol.ViewAll), "PrimaryMenus_Nodes".GetLocalized(), typeof(NodesViewModel).FullName));
            NavigateToByNavigationViewItem(PrimaryMenus.OfType<NavigationViewItem>().FirstOrDefault());
        }

        private NavigationViewItem CreateNavigationViewItem(IconElement icon, object content, object vm)
        {
            NavigationViewItem item = new NavigationViewItem
            {
                Icon = icon,
                Content = content,
                Tag = vm
            };
            ToolTipService.SetToolTip(item, content);
            return item;
        }

        #endregion

        #region 初始化导航
        private void InitializeNavigationService(Frame contentFrame)
        {
            NavigationService.CurrentFrame = contentFrame;
            NavigationService.CurrentFrame.Navigated += CurrentFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainViewModel_BackRequested;
        }
        private void NavigateToByNavigationViewItem(NavigationViewItem menu)
        {
            if (menu == null)
                return;
            NavigationService.NavigateTo(menu.Tag?.ToString());
        }
        private void CurrentFrame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            Frame cf = NavigationService.CurrentFrame;

            string key = NavigationService.GetKeyForPage(cf.CurrentSourcePageType);
            SelectedMenu = PrimaryMenus.OfType<NavigationViewItem>().FirstOrDefault(p => p.Tag?.ToString() == key);

            Header = SelectedMenu.Content;

            bool b = cf.BackStackDepth > 0;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                b ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        private void MainViewModel_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                e.Handled = true;
            }
        }
        #endregion

        public override void Cleanup()
        {
            NavigationService.CurrentFrame.Navigated -= CurrentFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested -= MainViewModel_BackRequested;
            base.Cleanup();
        }
    }
}
