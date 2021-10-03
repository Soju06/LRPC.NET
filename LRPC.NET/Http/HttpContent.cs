namespace LRPC.NET.Http {
    /// <summary>
    /// 콘텐츠
    /// </summary>
    public abstract class HttpContent {
        /// <summary>
        /// 스트림에 복사합니다
        /// </summary>
        public abstract Task CopyToAsync(Stream stream);

        /// <summary>
        /// 콘텐츠 타입
        /// </summary>
        public virtual string? ContentType { get; protected set; }
    }
}
