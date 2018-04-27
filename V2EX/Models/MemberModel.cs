using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
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
}
