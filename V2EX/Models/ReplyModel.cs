using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
    public class Reply
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "thanks")]
        public int Thanks { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "content_rendered")]
        public string Content_rendered { get; set; }

        [JsonProperty(PropertyName = "member")]
        public Member Member { get; set; }

        [JsonProperty(PropertyName = "created")]
        public int Created { get; set; }

        [JsonProperty(PropertyName = "last_modified")]
        public int Last_modified { get; set; }
    }
}


