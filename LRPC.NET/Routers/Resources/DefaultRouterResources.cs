namespace LRPC.NET.Routers.Resources {
    public class DefaultRouterResources : RouterResources {
        public override string? HomePage => Home.Replace("$SERVER_NAME", ServerName)
            .Replace("$VERSION", Version);

        /// <summary>
        /// 홈 페이지
        /// </summary>
        public static string Home { get; set; } = DefaultResources.HomePage;
    }
}