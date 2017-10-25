using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.UWP.Models
{
    public class TopicModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string AvatarNormal { get; set; }
        public DateTime Created { get; set; }
    }
}
