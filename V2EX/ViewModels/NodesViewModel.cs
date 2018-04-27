using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Models;
using V2EX.Services;

namespace V2EX.ViewModels
{
    public class NodesViewModel : ViewModelBase
    {
        private ObservableCollection<ControlInfoDataGroup> _groups = new ObservableCollection<ControlInfoDataGroup>();
        public ObservableCollection<ControlInfoDataGroup> Groups
        {
            get { return _groups = new ObservableCollection<ControlInfoDataGroup>(); }
            set { Set(ref _groups, value); }
        }



        public async Task InitializeAsync()
        {
            Groups.Clear();
            IEnumerable<Node> list = await WebService.Instance.GetAllNodesAsync();
            var groups = list.GroupBy(p => p.Title);
            foreach (var group in groups)
            {
                Groups.Add(new ControlInfoDataGroup(group.Key, group));
            }
        }
    }
}
