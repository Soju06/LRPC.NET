using System.Reflection;
using System.Text.Json.Serialization;

namespace LRPC.NET {
    /// <summary>
    /// LRPC 매소드 추적 정보
    /// </summary>
    public struct LRPCMethodTraceInfo {
        public LRPCMethodTraceInfo(MethodInfo method) {
            MethodName = method.Name;
            FullTraceName = method.DeclaringType.FullName + "." + MethodName;
        }

        /// <summary>
        /// 매소드 이름
        /// </summary>
        [JsonPropertyName("name")]
        public string MethodName { get; set; }

        /// <summary>
        /// 전체 추적 이름
        /// </summary>
        [JsonPropertyName("trace_name")]
        public string FullTraceName { get; set; }
    }
}
