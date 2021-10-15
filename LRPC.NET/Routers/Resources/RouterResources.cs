namespace LRPC.NET.Routers {
    /// <summary>
    /// 페이지 리소스
    /// </summary>
    public abstract class RouterResources : LRPComponent {
        /// <summary>
        /// 서버 이름
        /// </summary>
        public string? ServerName { get; set; }

        /// <summary>
        /// 서버 버전
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// 홈 페이지
        /// </summary>
        public abstract string? HomePage { get; }
    }
}