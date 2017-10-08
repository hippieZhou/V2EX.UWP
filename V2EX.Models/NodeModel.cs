using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
    [DataContract]
    public class NodeModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "title_alternative")]
        public string TitleAltemative { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "topics")]
        public int Topics { get; set; }

        [DataMember(Name = "stars")]
        public int Stars { get; set; }

        [DataMember(Name = "avatar_mini")]
        public string AvatarMini { get; set; }

        [DataMember(Name = "avatar_normal")]
        public string AvatarNormal { get; set; }

        [DataMember(Name = "avatar_large")]
        public string AvatarLarge { get; set; }

        #region For All Nodes

        [DataMember(Name = "header")]
        public string Header { get; set; }

        [DataMember(Name = "footer")]
        public string Footer { get; set; }
        [DataMember(Name = "created")]
        public long Created { get; set; }

        #endregion
    }
}
