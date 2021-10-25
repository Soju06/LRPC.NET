namespace LRPC.NET.Http {
    /// <summary>
    /// Http 서버 메시지 
    /// </summary>
    public abstract class HttpMessageContnet : HttpContent {
        public HttpMessageContnet(string title, string message,
            HttpStatusCode statusCode, string serverName, Guid requestId) {
            Title = title; 
            Message = message; 
            StatusCode = statusCode; 
            ServerName = serverName; 
            RequestId = requestId;
        }

        public HttpMessageContnet() {

        }

        /// <summary>
        /// 콘텐츠 설정
        /// </summary>
        public virtual void SetContent(string title, string message,
            HttpStatusCode statusCode, string serverName, Guid requestId) {
            Title = title;
            Message = message;
            StatusCode = statusCode;
            ServerName = serverName;
            RequestId = requestId;
        }

        /// <summary>
        /// 타이틀
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 메시지
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 상태 코드
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 서버 이름
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 요청 ID
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// 콘텐츠 복사
        /// </summary>
        protected abstract Task ContentCopyToAsync(Stream stream);

        public override async Task CopyToAsync(Stream stream) => 
            await ContentCopyToAsync(stream);
    }
}
