using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.UI.Views;
using V2EX.UI.Views.Dashboard;
using V2EX.UI.Views.Explore;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace V2EX.UI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private Dictionary<string, UIElement> _contentControlDic = new Dictionary<string, UIElement>()
        {
            { "浏览",new ExploreControl()},
            { "节点",new DashboardControl()},
            { "通知",new BlankControl()},
            { "收藏",new BlankControl()},
            { "设置",new BlankControl()},
            { "反馈",new BlankControl()},
        };

        private const string PanoramicStateName = "PanoramicState";
        private const string WideStateName = "WideState";
        private const string NarrowStateName = "NarrowState";
        private const double WideStateMinWindowWidth = 720;
        private const double PanoramicStateMinWindowWidth = 1024;

        private bool _isPaneOpen = true;
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set { Set(ref _isPaneOpen, value); }
        }

        private SplitViewDisplayMode _displayMode = SplitViewDisplayMode.Inline;
        public SplitViewDisplayMode DisplayMode
        {
            get { return _displayMode; }
            set { Set(ref _displayMode, value); }
        }

        private Visibility _hamburgerMenuVisibility = Visibility.Collapsed;
        public Visibility HamburgerMenuVisibility
        {
            get { return _hamburgerMenuVisibility; }
            set { Set(ref _hamburgerMenuVisibility, value); }
        }

        private ShellNavigationItem _lastSelectedItem;
        public ShellNavigationItem LastSelectedItem
        {
            get { return _lastSelectedItem; }
            set { Set(ref _lastSelectedItem, value); }
        }

        private ObservableCollection<ShellNavigationItem> _primaryItems = new ObservableCollection<ShellNavigationItem>();
        public ObservableCollection<ShellNavigationItem> PrimaryItems
        {
            get { return _primaryItems; }
            set { Set(ref _primaryItems, value); }
        }

        private ObservableCollection<ShellNavigationItem> _secondaryItems = new ObservableCollection<ShellNavigationItem>();

        public ObservableCollection<ShellNavigationItem> SecondaryItems
        {
            get { return _secondaryItems; }
            set { Set(ref _secondaryItems, value); }
        }

        private UIElement _shellContent;
        public UIElement ShellContent
        {
            get { return _shellContent; }
            set { Set(ref _shellContent, value); }
        }

        private RelayCommand _showPaneCommand;
        public RelayCommand ShowPaneCommand
        {
            get
            {
                return _showPaneCommand ?? (_showPaneCommand = new RelayCommand(
                    () =>
                    {
                        if (HamburgerMenuVisibility == Visibility.Visible || DisplayMode == SplitViewDisplayMode.Overlay)
                        {
                            IsPaneOpen = !IsPaneOpen;
                        }
                    }));
            }
        }

        private RelayCommand<ItemClickEventArgs> _itemSelectedCommand;
        public RelayCommand<ItemClickEventArgs> ItemSelectedCommand
        {
            get
            {
                return _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(args =>
                {
                    var item = args.ClickedItem as ShellNavigationItem;
                    if (item != null && item.Label != LastSelectedItem?.Label)
                    {
                        _contentControlDic.TryGetValue(item.Label, out UIElement value);
                        this.ShellContent = value;
                        this.LastSelectedItem = item;
                    }
                }));
            }
        }

        public void Initialize()
        {
            this.PrimaryItems.Add(new ShellNavigationItem("ms-appx:///Assets/Menus/ic_explore_black_24dp.png", "浏览", true));
            this.PrimaryItems.Add(new ShellNavigationItem("ms-appx:///Assets/Menus/ic_dashboard_black_24dp.png", "节点", true));
            this.PrimaryItems.Add(new ShellNavigationItem("ms-appx:///Assets/Menus/ic_notifications_black_24dp.png", "通知", false));
            this.PrimaryItems.Add(new ShellNavigationItem("ms-appx:///Assets/Menus/ic_favorite_black_24dp.png", "收藏", false));
            this.SecondaryItems.Add(new ShellNavigationItem("ms-appx:///Assets/Menus/ic_settings_black_24dp.png", "设置", false));
            this.SecondaryItems.Add(new ShellNavigationItem("ms-appx:///Assets/Menus/ic_feedback_black_24dp.png", "反馈", false));

            //this.LastSelectedItem = this.PrimaryItems.FirstOrDefault();
            //_contentControlDic.TryGetValue(this.LastSelectedItem.Label, out UIElement value);
            //this.ShellContent = value;
        }
    }
}
