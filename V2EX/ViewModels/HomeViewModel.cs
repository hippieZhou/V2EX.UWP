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
using V2EX.Models;
using V2EX.Services;
using Windows.UI.Xaml.Controls;

namespace V2EX.ViewModels
{
    public class HomeViewModel:ViewModelBase
    {
        private ObservableCollection<TabViewModel> _tabMenus = new ObservableCollection<TabViewModel>();
        public ObservableCollection<TabViewModel> TabMenus
        {
            get { return _tabMenus; }
            set { Set(ref _tabMenus, value); }
        }

        private TabViewModel _selectedTab;
        public TabViewModel SelectedTab
        {
            get { return _selectedTab; }
            set { Set(ref _selectedTab, value); }
        }

        private RelayCommand<object> _topicSelectedCmd;
        public RelayCommand<object> TopicSelectedCmd
        {
            get
            {
                return _topicSelectedCmd ?? (_topicSelectedCmd = new RelayCommand<object>(
                    (topic) =>
                    {
                        var navigationService = ServiceLocator.Current.GetInstance<NavigationService>();
                        navigationService.NavigateTo(typeof(TopicViewModel).FullName, topic);
                    },
                    (topic) => topic != null));
            }
        }

        public async Task InitializeAsync()
        {
            await InitializeTabMenusAsync();
        }

        private async Task InitializeTabMenusAsync()
        {
            TabMenus.Clear();
            TabMenus.Add(new TabViewModel("最新", "latest", true));
            TabMenus.Add(new TabViewModel("最热", "hot", false));

            await UpdateTabItemAsync(TabMenus.FirstOrDefault());
        }

        public async Task UpdateTabItemAsync(TabViewModel tab)
        {
            if (tab == SelectedTab)
                return;

            SelectedTab = tab;
            await SelectedTab.LoadTabTopicsAsync(true);
        }
    }

    public class TabViewModel : ObservableObject
    {
        public object Header { get; set; }
        public string Tag { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { Set(ref _isChecked, value); }
        }

        private ObservableCollection<Topic> _topics = new ObservableCollection<Topic>();
        public ObservableCollection<Topic> Topics
        {
            get { return _topics; }
            set { Set(ref _topics, value); }
        }

        public TabViewModel(string header, string tag, bool isChecked)
        {
            Header = header;
            Tag = tag;
            IsChecked = isChecked;
        }

        private RelayCommand _tabItemSelectedCmd;
        public RelayCommand TabItemSelectedCmd
        {
            get
            {
                return _tabItemSelectedCmd ?? (_tabItemSelectedCmd = new RelayCommand(
                    async () =>
                    {
                        var home = ServiceLocator.Current.GetInstance<HomeViewModel>();
                        await home?.UpdateTabItemAsync(this);
                    }));
            }
        }

        public async Task LoadTabTopicsAsync(bool isRefresh)
        {
            Topics.Clear();

            if (isRefresh)
            {
                IEnumerable<Topic> list = new List<Topic>();
                switch (Tag)
                {
                    case "latest":
                        list = await WebService.Instance.GetLatestTopicsAsync(isRefresh);
                        break;
                    case "hot":
                        list = await WebService.Instance.GetHotTopicsAsync(isRefresh);
                        break;
                    default:
                        break;
                }
                foreach (var item in list)
                {
                    Topics.Add(item);
                }
            }
        }

        public override string ToString()
        {
            return Header.ToString();
        }
    }
}
