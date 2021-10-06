using System.Net;
using System.Text;

namespace LRPC.NET.Http {
    /// <summary>
    /// 문자열 콘텐츠
    /// </summary>
    public class StringContent : HttpContent {
        Encoding encoding = Encoding.UTF8;

        public StringContent() {

        }
        
        public StringContent(string content) {
            Content = content;
            ContentType = ContentTypes.TXT;
        }
        
        public StringContent(string content, string contentType) {
            Content = content;
            ContentType = contentType;
        }
        
        public StringContent(string content, string contentType, Encoding encoding) {
            Content = content;
            ContentType = contentType;
            Encoding = encoding;
        }

        /// <summary>
        /// 콘텐츠
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 인코딩
        /// </summary>
        public Encoding Encoding { 
            get => encoding; 
            set => _ = value == null ? throw new ArgumentNullException(
                nameof(value)) : encoding = value; 
        }

        public override async Task CopyToAsync(Stream stream) {
            if(Content != null)
                await stream.WriteAsync(encoding.GetBytes(Content));
        }

        /// <summary>
        /// HTML 형식으로 만듭니다.
        /// </summary>
        /// <param name="html">html</param>
        public static StringContent Html(string html) => new(html, ContentTypes.HTML);

        /// <summary>
        /// JSON 형식으로 만듭니다.
        /// </summary>
        /// <param name="json">json</param>
        public static StringContent Json(string json) => new(json, ContentTypes.JSON);

        public static implicit operator StringContent(string content) => new(content);
    }
}
