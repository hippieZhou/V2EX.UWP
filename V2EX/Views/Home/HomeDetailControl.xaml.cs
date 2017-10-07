using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using V2EX.Models;
using V2EX.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace V2EX.Views
{
    public sealed partial class HomeDetailControl : UserControl
    {
        public TopicModel MasterTopicItem
        {
            get { return (TopicModel)GetValue(MasterTopicItemProperty); }
            set { SetValue(MasterTopicItemProperty, value); }
        }

        public static readonly DependencyProperty MasterTopicItemProperty =
            DependencyProperty.Register("MasterTopicItem", typeof(TopicModel), typeof(HomeDetailControl), new PropertyMetadata(null, (d, e) =>
             {
                 var hanler = d as HomeDetailControl;
                 hanler?.LoadDataAsync(e.NewValue);
             }));

        public ObservableCollection<ReplyModel> Replies { get; private set; } = new ObservableCollection<ReplyModel>();

        public HomeDetailControl()
        {
            InitializeComponent();
        }

        private async  Task LoadDataAsync(object newValue)
        {
            if (newValue is TopicModel model)
            {
                this.Replies.Clear();
                var list = await V2EXDataService.GetRepliesByTopicId(model.Id);
                foreach (var item in list)
                {
                    this.Replies.Add(item);
                }
            }
        }
    }
}
