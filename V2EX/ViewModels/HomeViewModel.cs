using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
                        args?.LoadNewsAsync();
                    }));
            }
        }

        public void Initialize()
        {
            InitializeMenus();
        }

        private void InitializeMenus()
        {
            TabMenus.Clear();
            TabMenus.Add(new TabViewModel("技术", "tech"));
            TabMenus.Add(new TabViewModel("创意", "creative"));
            TabMenus.Add(new TabViewModel("好玩", "play"));
            TabMenus.Add(new TabViewModel("Apple", "apple"));
            TabMenus.Add(new TabViewModel("酷工作", "jobs"));
            TabMenus.Add(new TabViewModel("交易", "deals"));
            TabMenus.Add(new TabViewModel("城市", "city"));
            TabMenus.Add(new TabViewModel("问与答", "qna"));
            TabMenus.Add(new TabViewModel("最热", "hot"));
            TabMenus.Add(new TabViewModel("全部", "all"));
            TabMenus.Add(new TabViewModel("R2", "r2"));

            SelectedTab = TabMenus.FirstOrDefault();
            ItemSelectedCmd.Execute(SelectedTab);
        }
    }

    public class TabViewModel : ObservableObject
    {
        public object Header { get; set; }
        public object Tag { get; set; }

        private ObservableCollection<Topic> _topics = new ObservableCollection<Topic>();
        public ObservableCollection<Topic> Topics
        {
            get { return _topics; }
            set { Set(ref _topics, value); }
        }

        public TabViewModel(string header,object tag)
        {
            Header = header;
            Tag = tag;
        }

        internal async Task LoadNewsAsync()
        {
            Topics.Clear();

            var list = await WebService.Instance.GetHotTopicsAsync();
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
