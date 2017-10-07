using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
    [DataContract]
    public class TopicModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "content_rendered")]
        public string ContentRendered { get; set; }

        [DataMember(Name = "replies")]
        public int Replies { get; set; }

        [DataMember(Name = "member")]
        public MemberModel Member { get; set; }

        [DataMember(Name = "node")]
        public NodeModel Node { get; set; }

        [DataMember(Name = "created")]
        public long Created { get; set; }

        [DataMember(Name = "last_modified")]
        public long LastModified { get; set; }

        [DataMember(Name = "last_touched")]
        public long LastTouched { get; set; }
    }
}
