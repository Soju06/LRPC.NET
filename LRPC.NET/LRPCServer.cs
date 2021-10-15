global using LRPC.NET.Http;
global using System.Web;
global using System.Net;
global using System.Text.Json;
using LRPC.NET.Routers;

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
        }

        /// <summary>
        /// LRPC 서버를 만듭니다.
        /// 기본 주소는 http://localhost:8080/ 입니다.
        /// </summary>
        /// <param name="prefixes">접두사</param>
        public LRPCServer(params string[] prefixes) { 
            http = new HttpServer(prefixes);
        }

        public virtual void Start() {
            Init();
        }

        protected virtual void Init() {
            InitHttp();
        }
        
        /// <summary>
        /// LRPC 컴포넌트를 주입합니다
        /// </summary>
        public virtual T Inject<T>() where T : LRPComponent, new() {
            var type = typeof(T);
            var baseType = type.BaseType;
            if (baseType == typeof(RouterResources)) 
                return (T)InjectRouterResource(type);
            else throw new FormatException("지원하지 않는 컴포넌트입니다.");
        }

        /// <summary>
        /// Http 서버
        /// </summary>
        public virtual HttpServer Http => http;

        /// <summary>
        /// 서버를 종료하고, 개체를 제거합니다.
        /// </summary>
        public virtual void Close() =>
            Dispose();

        /// <summary>
        /// 제거되었는지
        /// </summary>
        public virtual bool IsDisposed => disposed;

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
