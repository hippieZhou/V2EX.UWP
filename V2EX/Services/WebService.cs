using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Services
{
    /// <summary>
    /// 参考如下方式进行请求
    /// https://github.com/greatyao/v2ex-android/blob/v2ex2/app/src/main/java/com/yaoyumeng/v2ex2/api/V2EXManager.java
    /// </summary>
    public class WebService
    {
        #region URLs
        private const string HTTP_API_URL = "http://www.v2ex.com/api";
        private const string HTTPS_API_URL = "https://www.v2ex.com/api";
        public const string HTTP_BASE_URL = "http://www.v2ex.com";
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
    }
}
