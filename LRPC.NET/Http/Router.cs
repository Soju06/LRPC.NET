using System.Reflection;

namespace LRPC.NET.Http {
    /// <summary>
    /// 라우터
    /// </summary>
    public abstract class Router : IRouter {
        static readonly ParameterInfo[] RouteFuncParameter = typeof(RouteFunc).GetMethod("Invoke").GetParameters();
        readonly List<(string, string)> Funcations = new();
        bool loaded;

        // TODO: HttpServer Server prop


        // 버그 있음
        public virtual void Load(HttpServer http, HttpRouteRepository repository) {
            if (loaded) return;
            loaded = true;
            var type = GetType();
            type.GetMethods(BindingFlags.Public).Foreach(m => {
                var routeAtt = m.GetCustomAttribute<RouteAttribute>();
                if (routeAtt == null || m.ReturnType != typeof(void) || m.ReturnType != typeof(Task)) return;

                if (!RouteFuncParameter.SequenceEqual(m.GetParameters())) throw new FormatException(
                    $"Route parameter format does not match {m.DeclaringType.FullName}");

                var method = routeAtt.Method;
                var routes = repository.GetRoute(method);
                if (routes == null) throw new FormatException(
                    $"There is no HTTP method of {method} format. {m.DeclaringType.FullName}");
                else {
                    var loc = routeAtt.Location;
                    if (routes.ContainsKey(loc))
                        if (routeAtt.IgnoreDuplicates) throw new ArgumentException(
                            "There are overlapping routers.");
                        else return;

                    // TODO: void method support
                    var routeFunc = (RouteFunc)Delegate.CreateDelegate(typeof(RouteFunc), this, m);

                    Funcations.Add(new(method, loc));
                    routes.Add(loc, routeFunc);
                }
            });
        }

        public virtual void Unload(HttpServer http, HttpRouteRepository repository) {
            if (!loaded) return;
            while (Funcations.Count > 0) {
                var item = Funcations[0];
                var routes = repository.GetRoute(item.Item1);
                if (routes == null) continue;
                routes.Remove(item.Item2);
            } loaded = false;
        }
    }
}
