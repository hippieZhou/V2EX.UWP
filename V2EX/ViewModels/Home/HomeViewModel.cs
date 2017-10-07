using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using V2EX.Models;
using V2EX.Services;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;

namespace V2EX.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<TabNavigationItem> _tabItems = new ObservableCollection<TabNavigationItem>();
        public ObservableCollection<TabNavigationItem> TabItems
        {
            get { return _tabItems; }
            set { Set(ref _tabItems, value); }
        }


        private ObservableCollection<TopicModel> _tabTopics = new ObservableCollection<TopicModel>();
        public ObservableCollection<TopicModel> TabTopics
        {
            get { return _tabTopics; }
            private set { Set(ref _tabTopics, value); }
        }


        public NavigationServiceEx NavigationService
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }

        private const string NarrowStateName = "NarrowState";
        private const string WideStateName = "WideState";

        private VisualState _currentState;

        private TopicModel _selectedTopic;
        public TopicModel SelectedTopic
        {
            get { return _selectedTopic; }
            set { Set(ref _selectedTopic, value); }
        }


        private RelayCommand<ItemClickEventArgs> _tabItemClickCommand;
        public RelayCommand<ItemClickEventArgs> TabItemClickCommand
        {
            get
            {
                return _tabItemClickCommand ?? (_tabItemClickCommand = new RelayCommand<ItemClickEventArgs>(async args =>
                {
                    TabTopics.Clear();
                    if (args?.ClickedItem is TabNavigationItem model)
                    {
                        var list = new List<TopicModel>();
                        if (model.Name == "hot")
                            list = (await V2EXDataService.GetHotTopicsAsync()).ToList();
                        else if (model.Name == "latest")
                            list = (await V2EXDataService.GetLatestTopicsAsync()).ToList();
                        foreach (var item in list)
                            TabTopics.Add(item);
                    }
                }));
            }
        }


        private RelayCommand<ItemClickEventArgs> _itemClickCommand;

        public RelayCommand<ItemClickEventArgs> ItemClickCommand
        {
            get
            {
                return _itemClickCommand ?? (_itemClickCommand = new RelayCommand<ItemClickEventArgs>(args =>
                {
                    if (args?.ClickedItem is TopicModel item)
                    {
                        if (_currentState.Name == NarrowStateName)
                        {
                            NavigationService.Navigate(typeof(HomeDetailViewModel).FullName, item);
                        }
                        SelectedTopic = item;
                    }
                }));
            }
        }

        private ICommand _stateChangedCommand;

        public ICommand StateChangedCommand
        {
            get
            {
                if (_stateChangedCommand == null)
                {
                    _stateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>(OnStateChanged);
                }

                return _stateChangedCommand;
            }
        }

        public HomeViewModel()
        {
            #region Tab页初始化
            TabItems.Clear();

            //TabItems.Add(new TabNavigationItem("全部", "all"));
            TabItems.Add(new TabNavigationItem("最新", "latest"));
            TabItems.Add(new TabNavigationItem("最热", "hot"));
            //TabItems.Add(new TabNavigationItem("技术", "tech"));
            //TabItems.Add(new TabNavigationItem("创意", "creative"));
            //TabItems.Add(new TabNavigationItem("好玩", "play"));
            //TabItems.Add(new TabNavigationItem("Apple", "apple"));
            //TabItems.Add(new TabNavigationItem("酷工作", "jobs"));
            //TabItems.Add(new TabNavigationItem("交易", "deals"));
            //TabItems.Add(new TabNavigationItem("城市", "city"));
            //TabItems.Add(new TabNavigationItem("问与答", "qna"));
            //TabItems.Add(new TabNavigationItem("R2", "r2"));
            #endregion
        }

        public async Task LoadDataAsync(VisualState currentState)
        {
            _currentState = currentState;

            await Task.CompletedTask;
        }

        private void OnStateChanged(VisualStateChangedEventArgs args)
        {
            _currentState = args.NewState;
        }
    }
}
