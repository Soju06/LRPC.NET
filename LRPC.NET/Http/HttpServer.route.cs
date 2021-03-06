using System.Diagnostics;

namespace LRPC.NET.Http {
    partial class HttpServer {
        public HttpRouteRepository RouteRepository { get; private set; }

        /// <summary>
        /// 요청을 처리합니다.
        /// </summary>
        protected virtual async Task OnRequestAsync(HttpListenerContext context) {
            var req = new HttpRequest(this, context);
            var res = new HttpResponse(this, context);
            var func = GetRouteFunc(req.Method, req.Url);
            if (func == null) goto RouteNotFound;
            else {
                try {
                    var f = func?.Invoke(req, res);
                    if (f == null) goto RouteNotFound;
                    else {
                        res.ResponseBase.Close();
                    }
                } catch (Exception ex) { 
                    Debug.WriteLine($"route exception, func: {func.Method.DeclaringType.FullName}.{func.Method.Name}\nexception: {ex}");
                }
            }

            return;

            RouteNotFound:
            res.Content = GetMessageContnet(HttpStatusCode.Forbidden, req.RequestId);
            await res.SendAsync(HttpStatusCode.Forbidden);
        }

        /// <summary>
        /// 라우트를 가져옵니다.
        /// </summary>
        protected virtual RouteFunc? GetRouteFunc(string method, Uri uri) {
            var routes = GetMethodRoutes(method);
            if (routes == null) return null;
            foreach (var name in ParseRouteMatch(uri))
                if (routes.TryGetValue(name, out var func))
                    return func;
            return null;
        }

        /// <summary>
        /// 라우트 이름 매치를 만듭니다.
        /// </summary>
        /// <param name="uri">소스 주소</param>
        protected virtual string[] ParseRouteMatch(Uri uri) {
            var absolutePath = uri.AbsolutePath;
            // https://asdf.com/dotnet/system?pivots=dotnet
            var paths = new List<string> {
                // /dotnet/system?pivots=dotnet
                absolutePath.IsNullOrWhiteSpace() ? "/" : absolutePath + uri.Query
            };

            // if uri is / root
            // add (Empty), /
            if (absolutePath == "/" || absolutePath.IsNullOrWhiteSpace()) {
                paths.Add("");
                paths.Add("/");
            }
            // /dotnet/system
            // /dotnet/system/
            // /dotnet/
            // /dotnet
            // (Empty)
            var seg = uri.Segments;
            var segLength = seg.Length;
            for (var i = segLength - 1; i >= 0; i--) {
                var path = "";
                for (var l = 0; l < i; l++) path += seg[l];
                path += seg[i];
                if (path.IsNullOrWhiteSpace() || path == "/") continue;
                paths.Add(path);
                if (path[^1] == '/')
                    paths.Add(path[0..^1]);
                else paths.Add(path + "/");
            }

            return paths.ToArray();
        }

        /// <summary>
        /// 매소드 라우터를 가져옵니다.
        /// </summary>
        /// <param name="method">매소드</param>
        /// <returns>일치하는 라우터가 없으면 null를 반환합니다.</returns>
        protected virtual Dictionary<string, RouteFunc>? GetMethodRoutes(string method) =>
            RouteRepository.GetRoute(method);

        /// <summary>
        /// GET
        /// </summary>
        public void Get(string endpoint, RouteFunc handler) =>
            RouteRepository.Get(endpoint, handler);

        /// <summary>
        /// POST
        /// </summary>
        public void Post(string endpoint, RouteFunc handler) =>
            RouteRepository.Post(endpoint, handler);

        /// <summary>
        /// PUT
        /// </summary>
        public void Put(string endpoint, RouteFunc handler) =>
            RouteRepository.Put(endpoint, handler);

        /// <summary>
        /// DELETE
        /// </summary>
        public void Delete(string endpoint, RouteFunc handler) =>
            RouteRepository.Delete(endpoint, handler);
    }
}
