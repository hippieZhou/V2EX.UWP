using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
    public class Node
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "title_alternative")]
        public string Title_alternative { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "topics")]
        public int Topics { get; set; }

        [JsonProperty(PropertyName = "avatar_mini")]
        public string Avatar_mini { get; set; }

        [JsonProperty(PropertyName = "avatar_normal")]
        public string Avatar_normal { get; set; }

        [JsonProperty(PropertyName = "avatar_large")]
        public string Avatar_large { get; set; }

        #region Others
        [JsonProperty(PropertyName = "header")]
        public string Header { get; set; }
        [JsonProperty(PropertyName = "footer")]
        public string Footer { get; set; }
        [JsonProperty(PropertyName = "created")]
        public int Created { get; set; }
        #endregion
    }
}
