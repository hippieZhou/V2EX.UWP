using Newtonsoft.Json;

namespace V2EX.Models
{
    public class Rootobject
    {
        public Topic[] Topics { get; set; }
    }

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

    public class Member
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "tagline")]
        public string Tagline { get; set; }

        [JsonProperty(PropertyName = "avatar_mini")]
        public string Avatar_mini { get; set; }

        [JsonProperty(PropertyName = "avatar_normal")]
        public string Avatar_normal { get; set; }

        [JsonProperty(PropertyName = "avatar_large")]
        public string Avatar_large { get; set; }
    }

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
    }
}


