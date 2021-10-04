namespace LRPC.NET.Http {
    /// <summary>
    /// 라우터
    /// </summary>
    public interface IRouter {
        /// <summary>
        /// 라우터를 로드합니다.
        /// </summary>
        /// <param name="http">서버</param>
        /// <param name="repository">라우트 저장소</param>
        public void Load(HttpServer http, HttpRouteRepository repository);

        /// <summary>
        /// 라우터를 언로드 합니다.
        /// </summary>
        /// <param name="http">서버</param>
        /// <param name="repository">라우트 저장소</param>
        public void Unload(HttpServer http, HttpRouteRepository repository);
    }
}
