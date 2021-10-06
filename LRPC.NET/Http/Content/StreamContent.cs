namespace LRPC.NET.Http {
    /// <summary>
    /// 스트림 콘텐츠
    /// </summary>
    public class StreamContent : HttpContent {
        Stream stream;

        public StreamContent(Stream stream) {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
            ContentType = ContentTypes.BIN;
        }
        
        public StreamContent(Stream stream, string contentType) {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
            ContentType = contentType;
        }

        /// <summary>
        /// 콘텐츠
        /// </summary>
        public Stream Content {
            get => stream;
            set => _ = value == null ? throw new ArgumentNullException(
                nameof(value)) : stream = value;
        }

        public override async Task CopyToAsync(Stream stream) {
            await Content.CopyToAsync(stream);
        }

        public static implicit operator StreamContent(Stream content) => new(content);
        public static implicit operator StreamContent(byte[] content) => new(new MemoryStream(content));
    }
}
