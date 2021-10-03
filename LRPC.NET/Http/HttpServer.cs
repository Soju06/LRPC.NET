using System.Net;

namespace LRPC.NET.Http {
    /// <summary>
    /// Http 서버
    /// </summary>
    public partial class HttpServer : IDisposable {
        readonly HttpListener Http = new();
        readonly CancellationTokenSource Cancel = new();
        bool disposed;
        int maxConcurrentRequests = 10;

        /// <summary>
        /// Http 서버를 만듭니다.
        /// 기본 주소는 http://localhost:8080/ 입니다.
        /// </summary>
        public HttpServer() =>
            Init(new[] { "http://localhost:8080/" });

        /// <summary>
        /// Http 서버를 만듭니다.
        /// 기본 주소는 http://localhost:8080/ 입니다.
        /// </summary>
        /// <param name="prefixes">접두사</param>
        /// <example>
        /// <code>
        /// var server = new LRPCServer("http://localhost:8080/");
        /// </code>
        /// </example>
        public HttpServer(params string[] prefixes) =>
            Init(prefixes.Length < 1 ? new[] { "http://*:8080/" } : prefixes);

        void Init(string[] prefixes) {
            foreach (var prefixe in prefixes)
                Http.Prefixes.Add(prefixe);
        }

        /// <summary>
        /// 비동기로 시작합니다.
        /// </summary>
        public void BeginListen() => BeginListen(10);

        /// <summary>
        /// 비동기로 시작합니다.
        /// </summary>
        /// <param name="maxConcurrentRequests">최대 동시 요청 처리 수</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public void BeginListen(int maxConcurrentRequests) {
            if (disposed) throw new ObjectDisposedException(nameof(Http));
            if (Http.IsListening) throw new NotSupportedException("The server is already running.");
            this.maxConcurrentRequests = maxConcurrentRequests;
            Task.Factory.StartNew(ListenAsync);
        }

        #region ListenAsync 확장

        /// <summary>
        /// 서버를 시작합니다.
        /// </summary>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public Task ListenAsync() => ListenAsync(default(CancellationToken));

        /// <summary>
        /// 서버를 시작합니다.
        /// </summary>
        /// <param name="maxConcurrentRequests">최대 동시 요청 처리 수</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public Task ListenAsync(int maxConcurrentRequests) {
            this.maxConcurrentRequests = maxConcurrentRequests;
            return ListenAsync();
        }

        /// <summary>
        /// 서버를 시작합니다.
        /// </summary>
        /// <param name="maxConcurrentRequests">최대 동시 요청 처리 수</param>
        /// <param name="token">취소 토큰</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public Task ListenAsync(int maxConcurrentRequests, CancellationToken token) {
            this.maxConcurrentRequests = maxConcurrentRequests;
            return ListenAsync(token);
        }

        #endregion

        /// <summary>
        /// 서버를 시작합니다.
        /// </summary>
        /// <param name="token">취소 토큰</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public async Task ListenAsync(CancellationToken token) {
            if (Http.IsListening) throw new NotSupportedException("The server is already running.");
            Http.Start();
            var requests = new HashSet<Task>();
            var ctoken = Cancel.Token;
            for (int i = 0; i < maxConcurrentRequests; i++)
                requests.Add(Http.GetContextAsync());

            token.ThrowIfCancellationRequested();
            ctoken.ThrowIfCancellationRequested();

            while (Http.IsListening) {
                token.ThrowIfCancellationRequested();
                ctoken.ThrowIfCancellationRequested();
                var t = await Task.WhenAny(requests);
                requests.Remove(t);

                if (t is Task<HttpListenerContext> task) {
                    var context = task.Result;
                    requests.Add(OnRequestAsync(context));
                    requests.Add(Http.GetContextAsync());
                }
            }
        }

        /// <summary>
        /// 최대 동시 요청 처리
        /// </summary>
        public int MaxConcurrentRequests => maxConcurrentRequests;

        /// <summary>
        /// 이미 제거되었는지
        /// </summary>
        public bool IsDisposed => disposed;

        /// <summary>
        /// 서버를 종료하고, 개체를 제거합니다.
        /// </summary>
        public void Close() =>
            Dispose();

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    if(Cancel?.IsCancellationRequested != true)
                        Cancel?.Cancel();
                    if (Http.IsListening)
                        Http?.Close();
                    Cancel?.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
