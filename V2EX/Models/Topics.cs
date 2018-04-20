using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
    public class Rootobject
    {
        public Topic[] Topics { get; set; }
    }

    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public string Content_rendered { get; set; }
        public int Replies { get; set; }
        public Member Member { get; set; }
        public Node Node { get; set; }
        public int Created { get; set; }
        public int Last_modified { get; set; }
        public int Last_touched { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Tagline { get; set; }
        public string Avatar_mini { get; set; }
        public string Avatar_normal { get; set; }
        public string Avatar_large { get; set; }
    }

    public class Node
    {
        [PropertyName]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Title_alternative { get; set; }
        public string Url { get; set; }
        public int Topics { get; set; }
        public string Avatar_mini { get; set; }
        public string Avatar_normal { get; set; }
        public string Avatar_large { get; set; }
    }
}


