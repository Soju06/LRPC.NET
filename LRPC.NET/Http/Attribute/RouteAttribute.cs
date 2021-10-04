namespace LRPC.NET.Http {
    /// <summary>
    /// 라우트
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class RouteAttribute : Attribute {
        public RouteAttribute(string method, string location, bool ignoreDuplicates) {
            Method = method;
            Location = location;
            IgnoreDuplicates = ignoreDuplicates;
        }
        public RouteAttribute(string method, string location) {
            Method = method;
            Location = location;
        }
        
        public RouteAttribute(string location) {
            Location = location;
            Method = HttpMethods.Get;
        }

        /// <summary>
        /// http 메소드
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 주소
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 중복 오류 무시
        /// </summary>
        public bool IgnoreDuplicates { get; set; }
    }
}
