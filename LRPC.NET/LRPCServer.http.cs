using LRPC.NET.Routers;
using LRPC.NET.Routers.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace LRPC.NET {
    partial class LRPCServer {
        /// <summary>
        /// 라우터 리소스
        /// </summary>
        public RouterResources RouterResources { get; private set; }

        protected virtual void InitHttp() {
            Inject<DefaultRouterResources>();
            Http.RouteRepository.Route(new ServerInfoRouter(this, RouterResources));
            Http.RouteRepository.Route(new RemoteRouter(this, RouterResources));
            http.BeginListen();
        }

        protected virtual object InjectRouterResource(Type type) {
            var obj = (RouterResources)Activator.CreateInstance(type);

            obj.ServerName = Http.ServerName;
            obj.Version = LRPCEnvironment.Version;
            RouterResources = obj;

            return obj;
        }
    }
}
