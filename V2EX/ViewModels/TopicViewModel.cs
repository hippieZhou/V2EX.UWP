using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Models;

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

        public void Initialize(object parameter)
        {
            this.Topic = parameter as Topic;
        }
    }
}
