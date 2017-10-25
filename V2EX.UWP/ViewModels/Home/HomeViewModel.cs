using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.UWP.Models;
using V2EX.UWP.Services;

namespace V2EX.UWP.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<TopicModel> _items = new ObservableCollection<TopicModel>();
        public ObservableCollection<TopicModel> Items
        {
            get { return _items; }
            set { Set("Items", ref _items, value); }
        }

        public async Task LoadDataAsync()
        {
            this.Items.Clear();
            var data = await SampleDataService.GetAllTopicsAsync();
            foreach (var item in data)
            {
                this.Items.Add(item);
            }
        }
    }
}
