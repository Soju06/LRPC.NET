using System.Text.Json.Serialization;

namespace LRPC.NET.Routers {
    partial class ServerInfoRouter {
        protected class ServerInfo {
            [JsonPropertyName("server_name")]
            public string? Name { get; set; }
            [JsonPropertyName("server_version")]
            public string? Version { get; set; }
            [JsonPropertyName("server_protocol_version")]
            public string? ProtocolVersion { get; set; }
            [JsonPropertyName("remote_object")]
            public string? RemoteObjectURL { get; set; }
        }
    }
}
