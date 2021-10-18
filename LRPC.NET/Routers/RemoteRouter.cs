using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using StringContent = LRPC.NET.Http.StringContent;

namespace LRPC.NET.Routers {
    /// <summary>
    /// 원격 개체
    /// </summary>
    internal class RemoteRouter : Router {
        protected RouterResources resources;
        protected LRPCServer server;
        protected LRPCMethodRepository methods;

        public RemoteRouter(LRPCServer server, RouterResources resources) {
            this.resources = resources;
            this.server = server;
            methods = server.Methods;
        }

        [Route(HttpMethods.Get, "/remote")]
        [Route(HttpMethods.Get, "/r")]
        public virtual async Task OnRemote(HttpRequest req, HttpResponse res) {
            var contentType = req.Query["type"]?.ToLower();

            if ((string.IsNullOrWhiteSpace(contentType) || contentType == "html")) {
                res.Content = StringContent.Html("낫 써포뜨");
            } else if (contentType == "json") {
                res.Content = StringContent.Json(methods.GetJsonArray());
            } else {
                res.UseMessageContnet(HttpStatusCode.BadRequest, $"다음의 요청 형식을 지원하지 않습니다. {contentType}");
                await res.SendAsync(HttpStatusCode.BadRequest);
                return;
            } await res.SendAsync();
        }

        [Route(HttpMethods.Get, "/remote/invoke")]
        [Route(HttpMethods.Get, "/r/i")]
        public virtual async Task OnInvoke(HttpRequest req, HttpResponse res) {
            var contentType = req.Query["type"]?.ToLower();

            string? content;
            if ((string.IsNullOrWhiteSpace(contentType) || contentType == "html")
                && (content = resources.HomePage) != null) {
                res.Content = StringContent.Html(content);
            } else if (contentType == "json") {
                
            } else {
                res.UseMessageContnet(HttpStatusCode.BadRequest, $"다음의 요청 형식을 지원하지 않습니다. {contentType}");
                await res.SendAsync(HttpStatusCode.BadRequest);
                return;
            } await res.SendAsync();
        }

        protected override void OnRouterException(string routerName, Exception ex) {
            Console.WriteLine($"원격 라우트 예외, 라우터: {routerName}\n예외: {ex}");
        }
    }
}
