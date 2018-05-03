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
    /// <summary>
    /// 私有部分
    /// </summary>
    public partial class WebService : SingletonProvider<WebService>
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
                        string json = string.Empty;
                        if (Uri.TryCreate(uri, UriKind.RelativeOrAbsolute, out Uri result))
                        {
                            var response = await client.GetAsync(result);
                            //response.EnsureSuccessStatusCode();
                            json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        }
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
    }


    public partial class WebService: SingletonProvider<WebService>
    {
        /// <summary>
        /// 获取首页Tab页中话题
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetTabList()
        {
            Dictionary<string, string> tabs = new Dictionary<string, string>();

            await GetJsonAsync(HTTPS_BASE_URL, (html, ex) => 
            {
                if (!string.IsNullOrWhiteSpace(html) && ex == null)
                {
                    var dic = PersistenceHelper.ParseTabs(html);
                    foreach (var tab in dic)
                    {
                        tabs.Add(tab.Key, tab.Value);
                    }
                }
            });

            return tabs;
        }

        /// <summary>
        /// 获取首页各Tab页中的话题信息
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Topic>> GetTopicsByTabAsync(string tab)
        {
            List<Topic> list = new List<Topic>();

            string uri = $"{HTTPS_BASE_URL}{tab}";
            await GetJsonAsync(uri, (html, ex) =>
            {
                if (!string.IsNullOrWhiteSpace(html) && ex == null)
                {
                    List<Topic> topics = PersistenceHelper.ParseTabTopics(html);
                    list.AddRange(topics);
                }
            });
            return list;
        }

        /// <summary>
        /// 依据话题ID获取相应话题内容
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public async Task<Topic> GetTopicByTopicId(int topicId)
        {
            Topic topic = null;
            string uri = HTTPS_API_URL + API_TOPIC + "?id=" + topicId;
            await GetJsonAsync(uri, (json, ex) =>
            {
                if (!string.IsNullOrWhiteSpace(json) && ex == null)
                {
                    DeserializeObject<IEnumerable<Topic>>(json, (list, innerEx) =>
                    {
                        topic = list?.FirstOrDefault();
                    });
                }
            });
            return topic;
        }

        public async Task<IEnumerable<Reply>> GetRepliesByTopicIdAsync(int topicId)
        {
            List<Reply> replies = new List<Reply>();
            string uri = $"{HTTPS_API_URL}{API_REPLIES}?topic_id={topicId}";
            await GetJsonAsync(uri, (json, ex) => 
            {
                if (!string.IsNullOrWhiteSpace(json) && ex == null)
                    DeserializeObject<IEnumerable<Reply>>(json, (list, innerEx) =>
                    {
                        replies.AddRange(list);
                    });
            });
            return replies;
        }


        /// <summary>
        /// 获取最热话题
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Topic>> GetHotTopicsAsync()
        {
            List<Topic> topics = new List<Topic>();
            await GetTopicsAsync(HTTPS_API_URL + API_HOT, (list, ex) =>
            {
                if (ex == null)
                    topics.AddRange(list);
            });
            return topics;
        }

        /// <summary>
        /// 获取最新话题
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Topic>> GetLatestTopicsAsync()
        {
            List<Topic> topics = new List<Topic>();
            await GetTopicsAsync(HTTPS_API_URL + API_LATEST, (list, ex) =>
            {
                if (ex == null)
                    topics.AddRange(list);
            });
            return topics;
        }

        /// <summary>
        /// 获取所有节点
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Node>> GetAllNodesAsync()
        {
            List<Node> nodes = new List<Node>();
            string uri = HTTPS_API_URL + API_ALL_NODE;
            await GetJsonAsync(uri, (json, ex) =>
            {
                if (!string.IsNullOrWhiteSpace(json) && ex == null)
                {
                    DeserializeObject<IEnumerable<Node>>(json, (list, innerEx) =>
                    {
                        if (list != null && innerEx == null)
                            nodes.AddRange(list);
                    });
                }
            });
            return nodes;
        }
    }
}
