using System.Net;

namespace LRPC.NET.Http {
    public class HttpResponse {
        readonly HttpListenerResponse Response;

        internal HttpResponse(HttpListenerResponse response) {
            Response = response;
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
        /// 콘텐츠
        /// </summary>
        public HttpContent? Content { get; set; }

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
            string? contentType = null;

            if (Content != null) {
                contentType = Content.ContentType;
                using (Stream output = Response.OutputStream)
                    await Content.CopyToAsync(output);
            }

            if (ContentType != null) 
                contentType = ContentType;

            Response.ContentType = contentType ?? "";
            Response.StatusCode = (int)StatusCode;
            Response.Close();
        }
    }
}
