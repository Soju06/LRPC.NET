using System.Collections.Specialized;
using System.Net.WebSockets;

namespace LRPC.NET.Http {
    /// <summary>
    /// http 요청
    /// </summary>
    public class HttpRequest {
        readonly HttpListenerRequest Request;

        internal HttpRequest(HttpServer server, HttpListenerContext context) {
            var request = context.Request;
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
            Context = context;
            Server = server;
            IsWebSocket = request.IsWebSocketRequest;
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

        /// <summary>
        /// 컨텍스트
        /// </summary>
        public HttpListenerContext Context { get; private set; }

        /// <summary>
        /// Http 서버
        /// </summary>
        public HttpServer Server { get; private set; }

        /// <summary>
        /// 웹 소켓 여부
        /// </summary>
        public bool IsWebSocket { get; private set; }

        /// <summary>
        /// 웹소켓을 만듭니다.
        /// </summary>
        public async Task<WebSocketContext> AcceptWebSocketAsync(string? subProtocol = null) {
            var context = await Context.AcceptWebSocketAsync(subProtocol);
            Server.AcceptWebSocket(context.WebSocket);
            return context;
        }
    }
}
