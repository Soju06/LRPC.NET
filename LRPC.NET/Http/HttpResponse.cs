namespace LRPC.NET.Http {
    public class HttpResponse {
        readonly HttpListenerResponse Response;

        internal HttpResponse(HttpServer server, HttpListenerContext context) {
            Response = context.Response;
            Context = context;
            Server = server;
        }

        /// <summary>
        /// 헤더
        /// </summary>
        public WebHeaderCollection Headers { 
            get => Response.Headers; 
            set => Response.Headers = value; 
        }

        /// <summary>
        /// 쿠키
        /// </summary>
        public CookieCollection Cookies { 
            get => Response.Cookies; 
            set => Response.Cookies = value; 
        }

        /// <summary>
        /// 콘텐츠 타입
        /// </summary>
        public string? ContentType { get; set; }

        /// <summary>
        /// 상태 설명
        /// </summary>
        public string? StatusDescription { get; set; }

        /// <summary>
        /// 콘텐츠
        /// </summary>
        public HttpContent? Content { get; set; }

        /// <summary>
        /// 콘텍스트
        /// </summary>
        public HttpListenerContext Context { get; set; }

        /// <summary>
        /// 응답 베이스
        /// </summary>
        public HttpListenerResponse ResponseBase => Response;

        /// <summary>
        /// Http 서버
        /// </summary>
        public HttpServer Server { get; private set; }

        /// <summary>
        /// 상태 코드
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// 리디렉션
        /// </summary>
        public void Redirect(string url) => Response.Redirect(url);

        /// <summary>
        /// 리디렉션
        /// </summary>
        public void SendRedirect(string url) {
            Response.Redirect(url);
            Response.Close();
        }

        /// <summary>
        /// 데이터를 보내지 않고 클라이언트와 연결을 끊습니다.
        /// </summary>
        public void Abort() => Response.Abort();


        /// <summary>
        /// 응답을 보냅니다.
        /// </summary>
        /// <param name="statusCode">상태 코드</param>
        public Task SendAsync(HttpStatusCode statusCode) {
            StatusCode = statusCode;
            return SendAsync();
        }

        /// <summary>
        /// 응답을 보냅니다.
        /// </summary>
        public async Task SendAsync() {
            Response.StatusCode = (int)StatusCode;
            Response.StatusDescription = StatusDescription ?? StatusCode.ToString();
            Response.ContentType = (Content == null ? ContentType : Content.ContentType) ?? ContentTypes.BIN;

            if (Content != null)
                using (Stream output = Response.OutputStream)
                    await Content.CopyToAsync(output);

            Response.Close();
        }

        /// <summary>
        /// 메시지 콘텐츠를 사용합니다.
        /// </summary>
        /// <param name="title">타이틀</param>
        /// <param name="message">메시지</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="requestId">요청 Id</param>
        public HttpContent UseMessageContnet(string title, string message, HttpStatusCode statusCode, Guid requestId) =>
            Content = Server.GetMessageContnet(title, message, statusCode, requestId);

        /// <summary>
        /// 메시지 콘텐츠를 사용합니다.
        /// </summary>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="requestId">요청 Id</param>
        /// <param name="message">메시지</param>
        public HttpContent UseMessageContnet(HttpStatusCode statusCode, Guid requestId, string? message = null) =>
            Content = Server.GetMessageContnet(statusCode, requestId, message);

        /// <summary>
        /// 메시지 콘텐츠를 사용합니다.
        /// </summary>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="message">메시지</param>
        public HttpContent UseMessageContnet(HttpStatusCode statusCode, string? message = null) =>
            Content = Server.GetMessageContnet(statusCode, Context.Request.RequestTraceIdentifier, message);
    }
}
