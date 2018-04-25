using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using V2EX.Commons;
using V2EX.Models;

namespace V2EX.Services
{
    /// <summary>
    /// 参考如下方式进行请求
    /// https://github.com/greatyao/v2ex-android/blob/v2ex2/app/src/main/java/com/yaoyumeng/v2ex2/api/V2EXManager.java
    /// </summary>
    public class WebService: SingletonProvider<WebService>
    {
        #region URLs
        private const string HTTPS_API_URL = "https://www.v2ex.com/api";
        public const string HTTPS_BASE_URL = "https://www.v2ex.com";

        private const string API_LATEST = "/topics/latest.json";
        private const string API_HOT = "/topics/hot.json";
        private const string API_ALL_NODE = "/nodes/all.json";
        private const string API_REPLIES = "/replies/show.json";
        private const string API_TOPIC = "/topics/show.json";
        private const string API_USER = "/members/show.json";

        public const string SIGN_UP_URL = HTTPS_BASE_URL + "/signup";
        public const string SIGN_IN_URL = HTTPS_BASE_URL + "/signin";
        #endregion

        private async Task GetJsonAsync(string uri, Action<string, Exception> callback)
        {
            using (var handler = new HttpClientHandler())
            {
                try
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip;
                    using (var client = new HttpClient(handler))
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                        client.DefaultRequestHeaders.ExpectContinue = false;
                        var response = await client.GetAsync(uri);
                        //response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        callback(json, null);
                    }
                }
                catch (Exception ex)
                {
                    callback(null, ex);
                }
            }
        }

        private void DeserializeObject<T>(string json, Action<T, Exception> callback)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var obj = JsonConvert.DeserializeAnonymousType(json, default(T), settings);
                callback(obj, null);
            }
            catch (Exception ex)
            {
                callback(default(T), ex);
            }
        }

        private async Task GetTopicsAsync(string uri, Action<IEnumerable<Topic>, Exception> callback)
        {
            await GetJsonAsync(uri, (json, ex) =>
            {
                if (!string.IsNullOrWhiteSpace(json) && ex == null)
                {
                    DeserializeObject<IEnumerable<Topic>>(json, (list, innerEx) =>
                     {
                         if (list != null && innerEx == null)
                             callback(list, null);
                         else
                             callback(null, innerEx);
                     });
                }
                else
                    callback(null, ex);
            });
        }

        public async Task<IEnumerable<Topic>> GetHotTopicsAsync(bool refresh = true)
        {
            List<Topic> topics = new List<Topic>();
            if (refresh)
            {
                await GetTopicsAsync(HTTPS_API_URL + API_HOT, (list, ex) =>
                {
                    if (ex == null)
                        topics.AddRange(list);
                });
            }
            else
            {

            }
            return topics;
        }

        public async Task<IEnumerable<Topic>> GetLatestTopicsAsync(bool refresh = true)
        {
            List<Topic> topics = new List<Topic>();
            if (refresh)
            {
                await GetTopicsAsync(HTTPS_API_URL + API_LATEST, (list, ex) =>
                {
                    if (ex == null)
                        topics.AddRange(list);
                });
            }
            else
            {

            }
            return topics;
        }

        public async Task<Dictionary<string, string>> GetHomeTabsAsync()
        {
            Dictionary<string, string> tabDic = new Dictionary<string, string>();
            await GetJsonAsync(HTTPS_BASE_URL, (html, ex) => 
            {
                if (!string.IsNullOrWhiteSpace(html) && ex == null)
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);
                    var nodes = doc.DocumentNode.SelectNodes("//a[starts-with(@class,'tab')]");
                    if (nodes != null)
                    {
                        foreach (var node in nodes)
                        {
                            string text = node.InnerText;
                            string href = node.GetAttributeValue("href", "");
                            if (!string.IsNullOrWhiteSpace(text))
                                tabDic.Add(text, href);
                        }
                    }
                }
            });
            return tabDic;
        }

        public async Task<IEnumerable<Topic>> GetTabTopicsAsync(string url)
        {
            List<Topic> list = new List<Topic>();

            string nav = HTTPS_BASE_URL + url;
            await GetJsonAsync(nav, (html, ex) => 
            {
                if (!string.IsNullOrWhiteSpace(html) && ex == null)
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);
                    var nodes = doc.DocumentNode.SelectNodes("//div[@class='cell item']");
                    if (nodes != null)
                    {
                        foreach (var item in nodes)
                        {
                            var n = item.Descendants(4);
                            var img = item.SelectSingleNode("//img[@class='avatar']");
                            var node = item.SelectSingleNode("//a[@class='node']");
                            var member = item.SelectSingleNode("//strong");
                            var title = item.SelectSingleNode("//span[@class='item_title']");
                            var fade = item.SelectSingleNode("//span[@class='small fade']");
                            try
                            {
                                var topic = new Topic { Title = title.InnerText };

                                var href = img.GetAttributeValue("src", "");
                                topic.Member = new Member
                                {
                                    Avatar_large = href,
                                    Avatar_mini = href,
                                    Avatar_normal = href,
                                    Username = member.InnerText
                                };

                                list.Add(topic);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
            });

            return list;
        }
    }
}
