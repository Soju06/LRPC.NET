using System.Net;

namespace LRPC.NET.Http {
    /// <summary>
    /// 라우트 저장소
    /// </summary>
    public class HttpRouteRepository {
        /// <summary>
        /// GET
        /// </summary>
        public Dictionary<string, RouteFunc> GetRoutes { get; private set; } = new();
        /// <summary>
        /// POST
        /// </summary>
        public Dictionary<string, RouteFunc> PostRoutes { get; private set; } = new();
        /// <summary>
        /// PUT
        /// </summary>
        public Dictionary<string, RouteFunc> PutRoutes { get; private set; } = new();
        /// <summary>
        /// DELETE
        /// </summary>
        public Dictionary<string, RouteFunc> DeleteRoutes { get; private set; } = new();

        internal HttpRouteRepository() {

        }
        
        /// <summary>
        /// GET
        /// </summary>
        public void Get(string endpoint, RouteFunc handler) =>
            GetRoutes.Add(endpoint, handler);

        /// <summary>
        /// POST
        /// </summary>
        public void Post(string endpoint, RouteFunc handler) =>
            PostRoutes.Add(endpoint, handler);

        /// <summary>
        /// PUT
        /// </summary>
        public void Put(string endpoint, RouteFunc handler) =>
            PutRoutes.Add(endpoint, handler);

        /// <summary>
        /// DELETE
        /// </summary>
        public void Delete(string endpoint, RouteFunc handler) =>
            DeleteRoutes.Add(endpoint, handler);

        /// <summary>
        /// 라우터를 가져옵니다
        /// </summary>
        internal Dictionary<string, RouteFunc>? GetRoute(string method) =>
            method switch {
                HttpMethods.Get => GetRoutes,
                HttpMethods.Post => PostRoutes,
                HttpMethods.Put => PutRoutes,
                HttpMethods.Delete => DeleteRoutes,
                _ => null,
            };
    }

    /// <summary>
    /// 라우트 함수
    /// </summary>
    /// <param name="req">요청</param>
    /// <param name="res">응답</param>
    public delegate Task RouteFunc(HttpRequest req, HttpResponse res);
}
