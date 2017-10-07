using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
    [DataContract]
    public class ReplyModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "thanks")]
        public int Thanks { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "content_rendered")]
        public string ContentRendered { get; set; }

        [DataMember(Name = "member")]
        public MemberModel Member { get; set; }

        [DataMember(Name = "created")]
        public long Created { get; set; }

        [DataMember(Name = "last_modified")]
        public long LastModified { get; set; }
    }
}
