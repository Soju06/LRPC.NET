using System.Text.Json.Serialization;

namespace LRPC.NET.Object {
    /// <summary>
    /// LRPC 문자열
    /// </summary>
    public class LRPCString : LRPCObject {
        string value;

        [JsonConstructor]
        public LRPCString(string value) {
            this.value = value;
            TypeName = LRPCObjectTypes.String;
        }

        /// <summary>
        /// 값
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get => value; set => this.value = value; }

        public static implicit operator string(LRPCString str) => str?.Value ?? "";
        public static implicit operator LRPCString(string str) => new(str);
    }
}
