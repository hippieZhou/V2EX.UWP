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
    public class TopicViewModel : ViewModelBase
    {
        private Topic _topic;
        public Topic Topic
        {
            get { return _topic; }
            set { Set(ref _topic, value); }
        }


        private ObservableCollection<Reply> _replies = new ObservableCollection<Reply>();
        public ObservableCollection<Reply> Replies
        {
            get { return _replies; }
            set { Set(ref _replies, value); }
        }

        public async Task InitializeAsync(object parameter)
        {
            this.Topic = parameter as Topic;
            if (Topic == null)
                return;
            Topic = await WebService.Instance.GetTopicByTopicId(Topic.Id);
            var replies = await WebService.Instance.GetRepliesByTopicIdAsync(Topic.Id);
            foreach (var reply in replies)
            {
                Replies.Add(reply);
            }
        }
    }
}
