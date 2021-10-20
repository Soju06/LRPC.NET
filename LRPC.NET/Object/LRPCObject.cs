using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LRPC.NET.Object {
    /// <summary>
    /// 원격 개체용 오브젝트
    /// </summary>
    [Serializable]
    public abstract partial class LRPCObject {
        public LRPCObject() {
            TypeName = "object";
        }

        //public LRPCObject(SerializationInfo info, StreamingContext context) =>
        //    Serialize(info, context);

        //public void GetObjectData(SerializationInfo info, StreamingContext context) =>
        //    Deserialize(info, context);

        ///// <summary>
        ///// 직렬화
        ///// </summary>
        //protected virtual void Serialize(SerializationInfo info, StreamingContext context) {

        //}

        ///// <summary>
        ///// 역직렬화
        ///// </summary>
        //protected virtual void Deserialize(SerializationInfo info, StreamingContext context) {

        //}

        /// <summary>
        /// 타입
        /// </summary>
        [JsonPropertyName("_type")]
        public string TypeName { get; protected set; }
    }
}
