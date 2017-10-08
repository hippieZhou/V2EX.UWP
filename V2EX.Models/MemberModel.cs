using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
    [DataContract]
    public class MemberModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "tagline")]
        public string TagLine { get; set; }

        [DataMember(Name = "avatar_mini")]
        public string AvatarMini { get; set; }

        [DataMember(Name = "avatar_normal")]
        public string AvatarNormal { get; set; }

        [DataMember(Name = "avatar_large")]
        public string AvatarLarge { get; set; }
    }
}
