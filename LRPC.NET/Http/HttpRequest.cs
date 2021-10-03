using System.Collections.Specialized;
using System.Net;

namespace LRPC.NET.Http {
    /// <summary>
    /// http 요청
    /// </summary>
    public class HttpRequest {
        readonly HttpListenerRequest Request;

        internal HttpRequest(HttpListenerRequest request) {
            Request = request;
            Url = request.Url;
            OriginalUrl = request.RawUrl;
            Method = request.HttpMethod;
            Query = request.QueryString;
            Cookies = request.Cookies;
            Headers = request.Headers;
            IPEndPoint = request.RemoteEndPoint;
            RequestId = request.RequestTraceIdentifier;
            User = new(request);
            Content = new(request);
        }

        /// <summary>
        /// 주소
        /// </summary>
        public Uri Url { get; private set; }

        /// <summary>
        /// 원본 주소
        /// </summary>
        public string OriginalUrl { get; private set; }

        /// <summary>
        /// 매소드
        /// </summary>
        public string Method { get; private set; }

        /// <summary>
        /// 파라미터
        /// </summary>
        public NameValueCollection Query { get; private set; }

        /// <summary>
        /// 쿠키
        /// </summary>
        public CookieCollection Cookies { get; private set; }

        /// <summary>
        /// 헤더
        /// </summary>
        public NameValueCollection Headers { get; private set; }
        
        /// <summary>
        /// ip 주소
        /// </summary>
        public IPEndPoint IPEndPoint { get; private set; }

        /// <summary>
        /// 요청 id
        /// </summary>
        public Guid RequestId { get; private set; }

        /// <summary>
        /// 사용자
        /// </summary>
        public HttpRequestUser User { get; private set; }

        /// <summary>
        /// 콘텐츠
        /// </summary>
        public HttpRequestContent Content { get; private set; }

        /// <summary>
        /// 베이스
        /// </summary>
        public HttpListenerRequest RequestBase => Request;
    }
}
