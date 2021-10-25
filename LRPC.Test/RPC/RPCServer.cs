using LRPC.NET.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Net;
using System.Net.WebSockets;

namespace LRPC.Test.RPC {
    internal class RPCServer : Router {
        readonly HttpServer Http = new();

        public Dictionary<string, Delegate> Methods = new();

        public RPCServer() {
            Http.RouteRepository.Route(this);
            Http.BeginListen();
        }

        [Route(HttpMethods.Get, "/")]
        public async Task OnIndex(HttpRequest req, HttpResponse res) {
            if (!req.IsWebSocket) {
                res.UseMessageContnet(HttpStatusCode.BadRequest);
                await res.SendAsync();
            } else {
                var wsc = await req.AcceptWebSocketAsync("LRPC");
                var ws = wsc.WebSocket;
                var receive = new byte[10000];

                await Task.Delay(4000);
                // 인사
                await ws.SendAsync(new ArraySegment<byte>("LRPC Protocol!!!".Encode()), WebSocketMessageType.Text, false, default);

                {
                    var w = await ws.ReceiveAsync(new ArraySegment<byte>(receive), default);
                    if (w.MessageType != WebSocketMessageType.Text) goto 프로토콜_오류;
                    if (receive.Decode(0, w.Count) != "LRPC Protocol!!!") goto 프로토콜_오류;
                    await ws.SendAsync(new ArraySegment<byte>("LRPC Protocol!!!".Encode()), WebSocketMessageType.Text, false, default);
                }

                Console.WriteLine(ws.State);
                while (ws.State == WebSocketState.Open) {
                    Console.WriteLine("On");
                    var r = await ws.ReceiveAsync(receive, default);
                    if (r.CloseStatus != null) {
                        Console.WriteLine($"연결 닫음: {r.CloseStatus} {r.CloseStatusDescription}");
                    }

                    // 마지막 메시지면 종료
                    if (r.EndOfMessage) break;

                    try {
                        var rpcInv = Bson.Deserialize<RpcInvoke>(receive, 0, r.Count);
                        var ret = new RpcReturn();

                        if (Methods.TryGetValue(rpcInv.Name, out var dele)) {
                            try {
                                ret.Return = dele.DynamicInvoke(rpcInv.Parameters);
                                ret.IsSuccess = true;
                                ret.StateCode = StateCode.OK;
                            } catch (Exception ex) {
                                Console.WriteLine($"원격 매소드 오류 {ex}");
                                ret.IsSuccess = false;
                                ret.StateCode = StateCode.Error;
                                ret.ErrorMessage = ex.ToString();
                            }
                        } else {
                            ret.IsSuccess = false;
                            ret.StateCode = StateCode.NotFound;
                        }

                        var buf = Bson.Serialize(ret);

                        Console.WriteLine(ws.CloseStatus);

                        await ws.SendAsync(buf, WebSocketMessageType.Binary, false, default);
                    } catch (Exception ex) {
                        Console.WriteLine($"처리 예외 {ex}");
                    }
                }

                Console.WriteLine("연결 종료.");
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "BYE!!", default);
                return;

                프로토콜_오류:
                Console.WriteLine("프로토콜 오류");
                await ws.CloseAsync(WebSocketCloseStatus.ProtocolError, "WTF!!", default);
            }
        }

        protected override void OnRouterException(string routerName, Exception ex) {
            Console.WriteLine($"{routerName} {ex}");
        }
    }

    public static class Bson {
        public static byte[] Serialize(object obj) {
            using var mem = new MemoryStream();
            using var bs = new BsonDataWriter(mem);
            new JsonSerializer().Serialize(bs, obj);
            return mem.ToArray();
        }

        public static T Deserialize<T>(byte[] bytes, int index, int count) {
            using var mem = new MemoryStream(bytes, index, count);
            using var bs = new BsonDataReader(mem);
            return new JsonSerializer().Deserialize<T>(bs);
        }
    }
    
    [JsonObject]
    public struct RpcInvoke {
        [JsonProperty("n")]
        public string Name { get; set; }
        
        [JsonProperty("p")]
        public object?[]? Parameters { get; set; }
    }

    [JsonObject]
    public struct RpcReturn {
        [JsonProperty("i")]
        public bool IsSuccess { get; set; }
        [JsonProperty("s")]
        public StateCode StateCode { get; set; }
        [JsonProperty("r")]
        public object? Return { get; set; }
        [JsonProperty("e")]
        public string? ErrorMessage { get; set; }
    }

    public enum StateCode : byte {
        NotFound = 0,
        OK = 1,
        Error = 2,
    }
}
