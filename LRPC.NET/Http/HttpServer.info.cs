namespace LRPC.NET.Http {
    partial class HttpServer {
        int maxConcurrentRequests = 100;
        int maxConcurrentWebSocketConnections = 500;

        /// <summary>
        /// 서버 이름
        ///     기본값: LRPC.NET
        /// </summary>
        public string ServerName { get; set; } = "LRPC.NET";

        /// <summary>
        /// 최대 동시 요청 처리
        ///     기본값: 100
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException" />
        public int MaxConcurrentRequests { 
            get => maxConcurrentRequests; 
            set => _ = value < 1 ? throw new ArgumentOutOfRangeException(nameof(value), "The minimum value must be positive.")
                : maxConcurrentRequests = value;
        }

        /// <summary>
        /// 최대 동시 웹소켓 연결 수
        ///     기본값: 500
        /// </summary>
        public int MaxConcurrentWebSocketConnections { 
            get => maxConcurrentWebSocketConnections; 
            set => _ = value < 1 ? throw new ArgumentOutOfRangeException(nameof(value), "The minimum value must be positive.")
                : maxConcurrentWebSocketConnections = value;
        }
    }
}
