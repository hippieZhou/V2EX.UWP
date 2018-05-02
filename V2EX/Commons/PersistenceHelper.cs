using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Models;

namespace V2EX.Commons
{
    public class PersistenceHelper
    {
        public static Dictionary<string, string> ParseTabs(string html)
        {
            Dictionary<string, string> tabs = new Dictionary<string, string>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            IEnumerable<HtmlNode> tabNodes = doc.DocumentNode.Descendants("a").Where(p => p.GetAttributeValue("class", "") == "tab_current" || p.GetAttributeValue("class", "") == "tab");
            foreach (var tabNode in tabNodes)
            {
                string href = tabNode.GetAttributeValue("href", "");
                string name = tabNode.InnerText;

                tabs.Add(href, name);
            }

            return tabs;
        }

        public static List<Topic> ParseTabTopics(string responseBody)
        {
            List<Topic> topicList = new List<Topic>();

            var doc = new HtmlDocument();
            doc.LoadHtml(responseBody);
            var nodes = doc.DocumentNode.Descendants("div").Where(p => p.GetAttributeValue("class", "") == "cell item");
            foreach (var node in nodes)
            {
                Topic topic = ParseTopicModel(node);
                topicList.Add(topic);
            }
            return topicList;
        }

        private static Topic ParseTopicModel(HtmlNode node)
        {
            IEnumerable<HtmlNode> tdNodes = node.Descendants("td");
            Topic topic = new Topic
            {
                Member = new Member(),
                Node = new Node()
            };
            foreach (var tdNode in tdNodes)
            {
                string content = tdNode.InnerHtml;
                if (content.Contains("class=\"avatar\""))
                {

                    HtmlNode userIdNode = tdNode.Descendants("a").FirstOrDefault();
                    if (userIdNode != null)
                    {
                        string idUrlString = userIdNode.GetAttributeValue("href", "");
                        topic.Member.Username = idUrlString.Replace("/member/", "");
                    }
                    HtmlNode avatarNode = tdNode.Descendants("img").FirstOrDefault();
                    if (avatarNode != null)
                    {
                        string avatarString = avatarNode.GetAttributeValue("src", "");
                        if (avatarString.StartsWith("//"))
                        {
                            avatarString = "https:" + avatarString;
                        }
                        topic.Member.Avatar_large = avatarString;
                        topic.Member.Avatar_mini = avatarString;
                        topic.Member.Avatar_normal = avatarString;
                    }
                }
                else if (content.Contains("class=\"item_title\""))
                {
                    IEnumerable<HtmlNode> aNodes = tdNode.Descendants("a");
                    foreach (var aNode in aNodes)
                    {
                        if (aNode.GetAttributeValue("class", "") == "node")
                        {
                            string nodeUrlStrng = aNode.GetAttributeValue("herf", "");
                            topic.Node.Name = nodeUrlStrng.Replace("/go/", "");
                            topic.Node.Title = aNode.InnerText;
                        }
                        else
                        {
                            if (aNode.GetAttributeValue("href", "").Contains("reply"))
                            {
                                topic.Title = aNode.InnerText;
                                string topicIdString = aNode.GetAttributeValue("href", "");
                                string[] subArray = topicIdString.Split("#");
                                topic.Id = Convert.ToInt32(subArray[0].Replace("/t/", ""));
                                topic.Replies = Convert.ToInt32(subArray[1].Replace("reply", ""));
                            }
                        }
                    }

                    IEnumerable<HtmlNode> spanNodes = tdNode.Descendants("span");
                    foreach (var spanNode in spanNodes)
                    {
                        string contentString = spanNode.InnerText;
                        if (contentString.Contains("最后回复")
                            || contentString.Contains("前")
                            || contentString.Contains("&nbsp;•&nbsp;"))
                        {
                            string[] components = contentString.Split("&nbsp;•&nbsp;");
                            if (components.Length <= 2) continue;

                            string dateString = components[2];
                            int created = DateTime.Now.Millisecond / 1000;
                            string[] stringArray = dateString.Trim().Split(" ");
                            if (stringArray.Length > 1)
                            {
                                string unitString = "";
                                try
                                {
                                    created = (int)DateTime.Now.Ticks / 1000;
                                    topic.Created = created;
                                }
                                catch (Exception)
                                {
                                }

                                int how = Convert.ToInt32(stringArray[0]);
                                string subString = stringArray[1].Substring(0, 1);
                                if (subString.Equals("分"))
                                {
                                    unitString = "分钟前";
                                    created -= 60 * how;
                                }
                                else if (subString.Equals("小"))
                                {
                                    unitString = "小时前";
                                    created -= 3600 * how;
                                }
                                else if (subString.Equals("天"))
                                {
                                    created -= 24 * 3600 * how;
                                    unitString = "天前";
                                }
                                dateString = String.Format("%s%s", stringArray[0], unitString);
                            }
                            else
                            {
                                dateString = "刚刚";
                            }
                            topic.Created = created;
                        }
                        else
                        {
                            topic.Created = -1;
                        }
                    }
                }
            }

            return topic;
        }
    }
}
