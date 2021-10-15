using System;
using System.Collections.Generic;
using System.Text;
using StringContent = LRPC.NET.Http.StringContent;

namespace LRPC.NET.Routers {
    /// <summary>
    /// 원격 개체
    /// </summary>
    internal class RemoteRouter : Router {
        protected RouterResources resources;
        protected LRPCServer server;

        public RemoteRouter(LRPCServer server, RouterResources resources) {
            this.resources = resources;
            this.server = server;
        }

        [Route(HttpMethods.Get, "/remote")]
        public virtual async Task OnRemote(HttpRequest req, HttpResponse res) {
            var contentType = req.Query["type"]?.ToLower();

            string? content;
            if ((string.IsNullOrWhiteSpace(contentType) || contentType == "html")
                && (content = resources.HomePage) != null) {
                res.Content = StringContent.Html(content);
            } else if (contentType == "json") {
                
            } else if (contentType == "websocket") {
                await req.AcceptWebSocketAsync();
            } else {
                res.UseMessageContnet(HttpStatusCode.BadRequest, $"다음의 요청 형식을 지원하지 않습니다. {contentType}");
                await res.SendAsync(HttpStatusCode.BadRequest);
                return;
            } await res.SendAsync();
        }
    }
}
