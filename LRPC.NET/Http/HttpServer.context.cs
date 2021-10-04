using System.Net;
using System.Net.WebSockets;

namespace LRPC.NET.Http {
    partial class HttpServer {
        /// <summary>
        /// 메시지 콘텐츠 탬플릿 타입
        /// </summary>
        protected virtual Type MessageContentType { get; set; } = typeof(HttpDefaultMessageContent);

        /// <summary>
        /// 메시지 콘텐츠 템플릿을 설정합니다.
        /// </summary>
        /// <typeparam name="T">템플릿</typeparam>
        public virtual void SetMessageContnet<T>() where T : HttpMessageContnet, new() =>
            MessageContentType = typeof(T);

        /// <summary>
        /// 메시지 콘텐츠를 만듭니다.
        /// </summary>
        /// <param name="title">타이틀</param>
        /// <param name="message">메시지</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="requestId">요청 Id</param>
        public virtual HttpMessageContnet GetMessageContnet(string title, string message, HttpStatusCode statusCode, Guid requestId) {
            var content = MessageContentType.New<HttpMessageContnet>();
            content.SetContent(title, message, statusCode, ServerName, requestId);
            return content;
        }

        /// <summary>
        /// 메시지 콘텐츠를 만듭니다.
        /// </summary>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="requestId">요청 Id</param>
        /// <param name="message">메시지</param>
        public virtual HttpMessageContnet GetMessageContnet(HttpStatusCode statusCode, Guid requestId, string message = null) =>
            GetMessageContnet($"{(int)statusCode} {message ?? statusCode.ToString()}", message ?? statusCode.ToString(), statusCode, requestId);
    }
}
