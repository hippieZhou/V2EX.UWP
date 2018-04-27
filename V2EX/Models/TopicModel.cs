using Newtonsoft.Json;

namespace V2EX.Models
{
    public class Topic
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "content_rendered")]
        public string Content_rendered { get; set; }

        [JsonProperty(PropertyName = "replies")]
        public int Replies { get; set; }

        [JsonProperty(PropertyName = "member")]
        public Member Member { get; set; }

        [JsonProperty(PropertyName = "node")]
        public Node Node { get; set; }

        [JsonProperty(PropertyName = "created")]
        public int Created { get; set; }

        [JsonProperty(PropertyName = "last_modified")]
        public int Last_modified { get; set; }

        [JsonProperty(PropertyName = "last_touched")]
        public int Last_touched { get; set; }
    }
}


