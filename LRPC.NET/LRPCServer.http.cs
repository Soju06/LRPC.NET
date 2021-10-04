using LRPC.NET.Routers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LRPC.NET {
    partial class LRPCServer {
        protected virtual void InitHttp() {
            Http.RouteRepository.Route(new ServerInfoRouter());
            http.BeginListen();
        }
    }
}
