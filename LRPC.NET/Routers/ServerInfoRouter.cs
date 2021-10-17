using StringContent = LRPC.NET.Http.StringContent;

namespace LRPC.NET.Routers {
    internal partial class ServerInfoRouter : Router {
        protected RouterResources resources;
        protected LRPCServer server;

        public ServerInfoRouter(LRPCServer server, RouterResources resources) {
            this.resources = resources;
            this.server = server;
        }

        [Route(HttpMethods.Get, "/server-info")]
        public virtual async Task GetMain(HttpRequest req, HttpResponse res) {
            var contentType = req.Query["type"]?.ToLower();

            string? content;
            if ((string.IsNullOrWhiteSpace(contentType) || contentType == "html")
                && (content = resources.HomePage) != null) {
                res.Content = StringContent.Html(content);
            } else if (contentType == "json") {
                res.Content = StringContent.Json(new ServerInfo() { 
                    Name = resources.ServerName,
                    ProtocolVersion = LRPCEnvironment.ProtocolVersion,
                    Version = LRPCEnvironment.Version,
                    RemoteObjectURL = "/remote"
                });
            } else {
                res.UseMessageContnet(HttpStatusCode.BadRequest, $"다음의 요청 형식을 지원하지 않습니다. {contentType}");
                await res.SendAsync(HttpStatusCode.BadRequest);
                return;
            } await res.SendAsync();
        }
    }
}
