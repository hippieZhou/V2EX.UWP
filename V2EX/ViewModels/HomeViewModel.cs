using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }

    public class TabViewModel : ObservableObject
    {
        public object Header { get; set; }
        public object Tag { get; set; }

        private ObservableCollection<object> _news = new ObservableCollection<object>();
        public ObservableCollection<object> News
        {
            get { return _news; }
            set { Set(ref _news, value); }
        }


        public TabViewModel(string header,object tag)
        {
            Header = header;
            Tag = tag;

            News.Clear();
            for (int i = 0; i < 100; i++)
            {
                News.Add(i);
            }
        }

        public override string ToString()
        {
            return Header.ToString();
        }
    }
}
