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

        private RelayCommand<TabViewModel> _itemSelectedCmd;
        public RelayCommand<TabViewModel> ItemSelectedCmd
        {
            get
            {
                return _itemSelectedCmd ?? (_itemSelectedCmd = new RelayCommand<TabViewModel>(
                     (args) =>
                    {
                        args?.LoadTabTopicsAsync();
                    }));
            }
        }

        private RelayCommand<Topic> _topicSelectedCmd;
        public RelayCommand<Topic> TopicSelectedCmd
        {
            get
            {
                return _topicSelectedCmd ?? (_topicSelectedCmd = new RelayCommand<Topic>(
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
            var dic = await WebService.Instance.GetHomeTabsAsync();
            foreach (var item in dic)
            {
                TabMenus.Add(new TabViewModel(item.Key, item.Value));
            }
            SelectedTab = TabMenus.FirstOrDefault();
            ItemSelectedCmd.Execute(SelectedTab);
        }
    }

    public class TabViewModel : ObservableObject
    {
        public object Header { get; set; }
        public string Tag { get; set; }

        private ObservableCollection<Topic> _topics = new ObservableCollection<Topic>();
        public ObservableCollection<Topic> Topics
        {
            get { return _topics; }
            set { Set(ref _topics, value); }
        }

        public TabViewModel(string header,string tag)
        {
            Header = header;
            Tag = tag;
        }

        internal async Task LoadTabTopicsAsync()
        {
            Topics.Clear();

            var list = await WebService.Instance.GetTabTopicsAsync(Tag);
            foreach (var item in list)
            {
                Topics.Add(item);
            }
        }

        public override string ToString()
        {
            return Header.ToString();
        }
    }
}
