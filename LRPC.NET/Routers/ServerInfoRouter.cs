using System;
using System.Collections.Generic;
using System.Text;
using StringContent = LRPC.NET.Http.StringContent;

namespace LRPC.NET.Routers {
    internal class ServerInfoRouter : Router {
        public ServerInfoRouter() {

        }

        [Route(HttpMethods.Get, "/?type=json")]
        public async Task GetMain(HttpRequest req, HttpResponse res) {
            res.Content = StringContent.Json("{\"message\": \"hello world!\"}");
            await res.SendAsync();
        }

        string asdf = @"
<html><head>
        
    <title>403 Forbidden</title>
<style>a { text-decoration:none } @media only screen and (max-width: 600px) { body { padding: 0 1rem !important; } }
</style></head>
    <body style=""
    padding: 0 min(35rem, 30vw);
"">
        <center><h1>LRPC Protocol</h1></center>
        <hr>
        <center>
    HTTP 기반 RPC<br>
    <ul style = ""
    width: min(15rem, 70vw);
        border: 1px solid #fff;
    margin: 2rem;
    list-style: none;
    padding: 0;
"">
        <li><a href = ""/info"" > Remote Object</a></li><hr></ul></center><div></div>
    

</body></html>";
    }
}
