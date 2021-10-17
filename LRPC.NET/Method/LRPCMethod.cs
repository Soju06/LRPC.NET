using System.Reflection;
using System.Text.Json.Serialization;

namespace LRPC.NET {
    /// <summary>
    /// LRPC 매소드
    /// </summary>
    public class LRPCMethod : LRPCMethodBase {
        internal LRPCMethod(LRPCMethodRepository repository, Delegate method) : base(repository, method) {

        }

        /// <summary>
        /// LRPC 호출용 매소드를 만듭니다.
        /// </summary>
        /// <param name="repository">저장소</param>
        /// <param name="method">대상 매소드</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static LRPCMethod Create(LRPCMethodRepository repository, Delegate method) { 
            if (method == null) throw new ArgumentNullException(nameof(method));
            return new(repository, method);
        }
    }

    public static class LRPCMethodStatic {
        /// <summary>
        /// LRPC 호출용 매소드를 만듭니다.
        /// </summary>
        /// <param name="repository">저장소</param>
        /// <param name="method">대상 매소드</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static LRPCMethod CreateMethod(this LRPCMethodRepository repository, Delegate method) {
            if (method == null) throw new ArgumentNullException(nameof(method));
            return new(repository, method);
        }
    }

    /// <summary>
    /// LRPC 매소드 베이스
    /// </summary>
    public class LRPCMethodBase {
        internal LRPCMethodBase(LRPCMethodRepository repository, Delegate method) {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            this.method = method ?? throw new ArgumentNullException(nameof(method));
            var met = method.Method;
            IsVoid = met.ReturnType == typeof(void);
            ReturnType = met.ReturnType;
            Parameters = met.GetParameters();
            MethodId = repository.CreateMethodId(met);
        }

        readonly Delegate method;

        /// <summary>
        /// void 매소드 여부
        /// </summary>
        [JsonPropertyName("is_void")]
        public bool IsVoid { get; private set; }

        /// <summary>
        /// 리턴 타입
        /// </summary>
        public Type ReturnType { get; private set; }

        /// <summary>
        /// 파라미터
        /// </summary>
        public ParameterInfo[] Parameters { get; private set; }

        /// <summary>
        /// 고유 메소드 Id
        /// </summary>
        [JsonPropertyName("id")]
        public Guid MethodId { get; private set; }

        /// <summary>
        /// 예외 로깅 매소드 Id
        /// </summary>
        [JsonPropertyName("logging_id")]
        public Guid ExceptionLoggingId { get; private set; }

        /// <summary>
        /// 추적 정보
        /// </summary>
        [JsonPropertyName("trace")]
        public LRPCMethodTraceInfo TraceInfo { get; private set; }

        /// <summary>
        /// 매소드 호출
        /// </summary>
        /// <param name="parameters">파라미터</param>
        /// <returns>리턴 값</returns>
        public object Invoke(params object[] parameters) =>
            method.DynamicInvoke(args: parameters);
    }
}