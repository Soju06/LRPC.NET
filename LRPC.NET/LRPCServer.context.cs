using System.Net;

namespace LRPC.NET {
    partial class LRPCServer {

        protected virtual Task OnRequestAsync(HttpListenerContext context) =>
            Task.Factory.StartNew(() => {
                context.Response.HtmlContent("<h1>Hello world!</h1>");
                context.Response.Close();
            });
    }
}
