using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using V2EX.Models;

namespace V2EX.Services
{
    /// <summary>
    /// 参考：
    /// https://github.com/greatyao/v2ex-android/blob/v2ex2/app/src/main/java/com/yaoyumeng/v2ex2/api/V2EXManager.java
    /// </summary>
    public class V2EXDataService
    {
        #region API

        private static string HTTPS_BASE_URL = "https://www.v2ex.com";
        private static string SIGN_UP_URL = HTTPS_BASE_URL + "/signup";
        private static string SIGN_IN_URL = HTTPS_BASE_URL + "/signin";

        private static string HTTPS_API_URL = "https://www.v2ex.com/api";

        private static string API_LATEST = "/topics/latest.json";
        private static string API_HOT = "/topics/hot.json";
        private static string API_TOPIC = "/topics/show.json";
        private static string API_ALL_NODE = "/nodes/all.json";
        private static string API_REPLIES = "/replies/show.json";
        private static string API_USER = "/members/show.json";
        #endregion

        private static async Task<string> GetJsonAsync(string uri)
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
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        private static async Task<IEnumerable<TopicModel>> GetTopicsAsync(string url)
        {
            var list = new List<TopicModel>();
            var json = await GetJsonAsync(url);
            if (!string.IsNullOrWhiteSpace(json))
            {
                var data = JsonConvert.DeserializeObject<List<TopicModel>>(json);
                list.AddRange(data);
            }
            return list;
        }

        /// <summary>
        /// 获取最热话题
        /// </summary>
        public static async Task<IEnumerable<TopicModel>> GetHotTopicsAsync()
        {
            var url = HTTPS_API_URL + API_HOT;
            var list = await GetTopicsAsync(url);
            return list;
        }

        /// <summary>
        /// 获取最新话题
        /// </summary>
        public static async Task<IEnumerable<TopicModel>> GetLatestTopicsAsync()
        {
            var url = HTTPS_API_URL + API_LATEST;
            var list = await GetTopicsAsync(url);
            return list;
        }
    }
}
