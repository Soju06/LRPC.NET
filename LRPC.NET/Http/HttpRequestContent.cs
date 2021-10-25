namespace LRPC.NET.Http {
    /// <summary>
    /// http 요청 콘텐츠
    /// </summary>
    public class HttpRequestContent {
        public HttpRequestContent(HttpListenerRequest request) {
            Content = request.InputStream;
            HasContent = request.HasEntityBody;
            ContentType = request.ContentType;
            Length = request.ContentLength64;
        }

        /// <summary>
        /// 콘텐츠 존재 여부
        /// </summary>
        public bool HasContent { get; private set; }

        /// <summary>
        /// 콘텐츠
        /// </summary>
        public Stream? Content { get; private set; }

        /// <summary>
        /// 콘텐츠 타입
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// 길이
        /// </summary>
        public long Length { get; private set; }

        /// <summary>
        /// 콘텐츠를 문자열로 읽습니다.
        /// </summary>
        /// <returns>문자열</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> ReadAsStringAsync() {
            if (Content == null) throw new ArgumentNullException(nameof(Content));
            using var sr = new StreamReader(Content);
            return await sr.ReadToEndAsync();
        }
    }

    public static class HttpRequestContentStatic {
        /// <summary>
        /// 콘텐츠를 json 개체로 읽습니다.
        /// </summary>
        /// <typeparam name="T">개체</typeparam>
        /// <param name="request">콘텐츠</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<T?> ReadAsJsonAsync<T>(this HttpRequestContent request) =>
            request.Content == null ? throw new ArgumentNullException(nameof(request.Content)) 
                : await JsonSerializer.DeserializeAsync<T>(request.Content);

        /// <summary>
        /// 콘텐츠를 json 개체로 읽습니다.
        /// </summary>
        /// <param name="request">콘텐츠</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<JsonDocument> ReadAsJsonAsync(this HttpRequestContent request) =>
            request.Content == null ? throw new ArgumentNullException(nameof(request.Content))
                : await JsonDocument.ParseAsync(request.Content);
    }
}
