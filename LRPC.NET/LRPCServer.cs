global using LRPC.NET.Http;
global using System.Web;
global using System.Net;
global using System.Text.Json;

namespace LRPC.NET {
    /// <summary>
    /// LRPC 서버
    /// </summary>
    public partial class LRPCServer : IDisposable {
        readonly HttpServer http;
        bool disposed;

        /// <summary>
        /// LRPC 서버를 만듭니다.
        /// 기본 주소는 http://localhost:8080/ 입니다.
        /// </summary>
        public LRPCServer() {
            http = new HttpServer();
            Init();
        }

        /// <summary>
        /// LRPC 서버를 만듭니다.
        /// 기본 주소는 http://localhost:8080/ 입니다.
        /// </summary>
        /// <param name="prefixes">접두사</param>
        public LRPCServer(params string[] prefixes) { 
            http = new HttpServer(prefixes);
            Init(); 
        }

        void Init() {
            InitHttp();
        }

        /// <summary>
        /// Http 서버
        /// </summary>
        public HttpServer Http => http;

        /// <summary>
        /// 서버를 종료하고, 개체를 제거합니다.
        /// </summary>
        public void Close() =>
            Dispose();

        /// <summary>
        /// 제거되었는지
        /// </summary>
        public bool IsDisposed => disposed;

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    http?.Close();
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
