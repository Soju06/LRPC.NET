using System;
using System.Collections.Generic;
using System.Text;
using StringContent = LRPC.NET.Http.StringContent;

namespace LRPC.NET.Routers {
    internal class ServerInfoRouter : Router {
        public ServerInfoRouter() {

        }

        [Route(HttpMethods.Get, "/")]
        public async Task GetMain(HttpRequest req, HttpResponse res) {
            res.Content = StringContent.Html("asdf");
            await res.SendAsync();
        }
    }
}
