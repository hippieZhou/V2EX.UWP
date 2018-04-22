using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Commons;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

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
        private bool _alwaysShowHeader = true;
        public bool AlwaysShowHeader
        {
            get { return _alwaysShowHeader; }
            set { Set(ref _alwaysShowHeader, value); }
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
                    NavigationViewItem item = args.IsSettingsSelected ?
                    CreateNavigationViewItem(new SymbolIcon(Symbol.Setting), "PrimaryMenus_Settings".GetLocalized(), typeof(SettingsViewModel).FullName) : args.SelectedItem as NavigationViewItem;

                    if (item == null)
                        return;
                    NavigationService.NavigateTo(item.Tag?.ToString());
                }));
            }
        }

        private BitmapImage _profilePicture;
        public BitmapImage ProfilePicture
        {
            get { return _profilePicture; }
            set { Set(ref _profilePicture, value); }
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
            var first = PrimaryMenus.OfType<NavigationViewItem>().FirstOrDefault();
            NavigationService.NavigateTo(first.Tag?.ToString());
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

        private void CurrentFrame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            Frame cf = NavigationService.CurrentFrame;

            string key = NavigationService.GetKeyForPage(cf.CurrentSourcePageType);
            SelectedMenu = PrimaryMenus.OfType<NavigationViewItem>().FirstOrDefault(p => p.Tag?.ToString() == key);

            switch (key)
            {
                case "V2EX.ViewModels.HomeViewModel":
                    Header = "PrimaryMenus_Home".GetLocalized();
                    AlwaysShowHeader = true;
                    break;
                case "V2EX.ViewModels.NodesViewModel":
                    Header = "PrimaryMenus_Nodes".GetLocalized();
                    AlwaysShowHeader = true;
                    break;
                case "V2EX.ViewModels.SettingsViewModel":
                    Header = "PrimaryMenus_Settings".GetLocalized();
                    AlwaysShowHeader = true;
                    break;
                case "V2EX.ViewModels.TopicViewModel":
                    AlwaysShowHeader = false;
                    break;
                default:
                    break;
            }

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
