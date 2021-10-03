using System.Net;

namespace LRPC.NET.Http {
    /// <summary>
    /// 사용자
    /// </summary>
    public class HttpRequestUser {
        public HttpRequestUser(HttpListenerRequest request) {
            UserAgent = request.UserAgent;
            UserLanguages = request.UserLanguages;
            IsAuthenticated = request.IsAuthenticated;
        }

        /// <summary>
        /// user agent
        /// </summary>
        public string UserAgent { get; private set; }

        /// <summary>
        /// 사용자 언어
        /// </summary>
        public string[] UserLanguages { get; private set; }

        /// <summary>
        /// 인증됨
        /// </summary>
        public bool IsAuthenticated { get; private set; }
    }
}
