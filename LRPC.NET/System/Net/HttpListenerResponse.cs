using System.Text;

namespace System.Net {
    public static class HttpListenerResponseStatic {
        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="stream">스트림</param>
        /// <param name="setStreamLength">스트림 길이를 응답 콘텐츠 길이에 씁니다.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, Stream stream, bool setStreamLength = true) {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (setStreamLength) response.ContentLength64 = stream.Length;
            stream.CopyTo(response.OutputStream);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="stream">스트림</param>
        /// <param name="setStreamLength">스트림 길이를 응답 콘텐츠 길이에 씁니다.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, HttpStatusCode statusCode, Stream stream, bool setStreamLength = true) {
            response.StatusCode = (int)statusCode;
            Content(response, stream, setStreamLength);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="stream">스트림</param>
        /// <param name="contentType">콘텐츠 타입</param>
        /// <param name="setStreamLength">스트림 길이를 응답 콘텐츠 길이에 씁니다.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, HttpStatusCode statusCode, Stream stream, string contentType, bool setStreamLength = true) {
            response.StatusCode = (int)statusCode;
            response.ContentType = contentType;
            Content(response, stream, setStreamLength);
        }
        
        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="stream">스트림</param>
        /// <param name="contentType">콘텐츠 타입</param>
        /// <param name="setStreamLength">스트림 길이를 응답 콘텐츠 길이에 씁니다.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, Stream stream, string contentType, bool setStreamLength = true) {
            response.ContentType = contentType;
            Content(response, stream, setStreamLength);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="text">텍스트</param>
        /// <param name="encoding">인코딩</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, string text, Encoding encoding) {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));
            Content(response, new MemoryStream(encoding.GetBytes(text)), true);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="text">텍스트</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, string text) =>
            Content(response, text, Encoding.UTF8);

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="text">텍스트</param>
        /// <param name="encoding">인코딩</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, HttpStatusCode statusCode, string text, Encoding encoding) {
            response.StatusCode = (int)statusCode;
            Content(response, text, encoding);
        }
        
        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="text">텍스트</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, HttpStatusCode statusCode, string text) {
            response.StatusCode = (int)statusCode;
            Content(response, text, Encoding.UTF8);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="text">텍스트</param>
        /// <param name="contentType">콘텐츠 타입</param>
        /// <param name="encoding">인코딩</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, string text, string contentType, Encoding encoding) {
            response.ContentType = contentType;
            Content(response, text, encoding);
        }
        
        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="text">텍스트</param>
        /// <param name="contentType">콘텐츠 타입</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, string text, string contentType) {
            response.ContentType = contentType;
            Content(response, text, Encoding.UTF8);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="text">텍스트</param>
        /// <param name="contentType">콘텐츠 타입</param>
        /// <param name="encoding">인코딩</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, HttpStatusCode statusCode, string text, string contentType, Encoding encoding) {
            response.StatusCode = (int)statusCode;
            response.ContentType = contentType;
            Content(response, text, encoding);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="text">텍스트</param>
        /// <param name="contentType">콘텐츠 타입</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Content(this HttpListenerResponse response, HttpStatusCode statusCode, string text, string contentType) {
            response.StatusCode = (int)statusCode;
            response.ContentType = contentType;
            Content(response, text, Encoding.UTF8);
        }
        
        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="html">HTML</param>
        public static void HtmlContent(this HttpListenerResponse response, HttpStatusCode statusCode, string html) {
            response.StatusCode = (int)statusCode;
            response.ContentType = ContentTypes.HTML;
            Content(response, html, Encoding.UTF8);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="statusCode">상태 코드</param>
        /// <param name="html">HTML</param>
        /// <param name="encoding">인코딩</param>
        public static void HtmlContent(this HttpListenerResponse response, HttpStatusCode statusCode, string html, Encoding encoding) {
            response.StatusCode = (int)statusCode;
            response.ContentType = ContentTypes.HTML;
            Content(response, html, encoding);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="html">HTML</param>
        public static void HtmlContent(this HttpListenerResponse response, string html) {
            response.ContentType = ContentTypes.HTML;
            Content(response, html, Encoding.UTF8);
        }

        /// <summary>
        /// 콘텐츠를 설정합니다.
        /// </summary>
        /// <param name="response">응답</param>
        /// <param name="html">HTML</param>
        /// <param name="encoding">인코딩</param>
        public static void HtmlContent(this HttpListenerResponse response, string html, Encoding encoding) {
            response.ContentType = ContentTypes.HTML;
            Content(response, html, encoding);
        }
    }
}
