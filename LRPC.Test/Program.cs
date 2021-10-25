using LRPC.NET;
using LRPC.NET.Http;
using LRPC.NET.Object;
using LRPC.Test.RPC;
using System.Net;
using System.Net.WebSockets;
using System.Text.Json;

var wscl = new ClientWebSocket();
var server = new HttpServer();
server.RouteRepository.Get("/",
    async (HttpRequest req, HttpResponse res) => {
        if (!req.IsWebSocket) {
            await res.SendAsync(HttpStatusCode.BadRequest);
            return;
        }

        Console.WriteLine("웹소켓 연결중");
        var wsc = await req.AcceptWebSocketAsync();
        var ws = wsc.WebSocket;
        while (ws.State == WebSocketState.Connecting)
            await Task.Delay(100);
        Console.WriteLine("웹소켓 연결됨");

        var buf = new byte[10000];
        var d = 0;

        while (ws.State == WebSocketState.Open) {
            Console.Write($"웹소켓 송신: ");
            var content = Console.ReadLine();

            await ws.SendAsync(new ArraySegment<byte>(content.Encode()), WebSocketMessageType.Text, false, default);

            //if(++d % 2 == 0) {
            //    var e = await ws.ReceiveAsync(new ArraySegment<byte>(buf), default);
            //    Console.WriteLine($"웹소켓 수신: {buf.Decode(0, e.Count)}");
            //}

        }
    });
server.BeginListen();

await wscl.ConnectAsync("ws://localhost:8080/".Uri(), default);
var buf = new byte[10000];

while (true) {
    var e = await wscl.ReceiveAsync(new ArraySegment<byte>(buf), default);
    Console.WriteLine($"웹소켓 클 수신: " + buf.Decode(0, e.Count));
    //Console.WriteLine("웹소켓 클 송신");
    //await wscl.SendAsync(new ArraySegment<byte>(new Random().NextBytes(20)), WebSocketMessageType.Text, false, default);
}

//var ser = new RPCServer();
//ser.Methods.Add("1", () => "hello world!");
//var cli = new RPCClient("ws://localhost:8080/");
//await Task.Delay(4000);
//await cli.Greeting();

//Console.WriteLine(await cli.Rpc("1"));
//new LRPCServer("http://localhost:8080/").Return(server => {
//    server.Methods.CreateMethod(() => {

//    });
//    server.Methods.CreateMethod(() => {
//        return "asdasd";
//    });
//    server.Methods.CreateMethod(() => {
//        return 13;
//    });
//    server.Start();
//});

//var d = new DoubleDictionary<string, Guid, string>();

//d.Add("wasans6", new Guid("D625C8DB-0F06-436D-9B7A-A6105566410E"), "1");
//d.Add("wasans5", new Guid("D525C8DB-0F06-436D-9B7A-A6105566410E"), "2");
//d.Add("wasans4", new Guid("D425C8DB-0F06-436D-9B7A-A6105566410E"), "3");
//d.Add("wasans3", new Guid("D325C8DB-0F06-436D-9B7A-A6105566410E"), "4");
//d.Add("wasans2", new Guid("D225C8DB-0F06-436D-9B7A-A6105566410E"), "5");
//d.Add("wasans1", new Guid("D125C8DB-0F06-436D-9B7A-A6105566410E"), "6");

//Console.WriteLine(d["wasans6"]);
//Console.WriteLine(d[new Guid("D525C8DB-0F06-436D-9B7A-A6105566410E")]);
//Console.WriteLine(d["wasans1"]);

//d.TryGetValue("wasans5", out var a1, out var a2);
//Console.WriteLine($"{a1} {a2}");
//dd(LRPCObject.Convert(324254, null));
//dd(LRPCObject.Convert(324254, null));
//dd(LRPCObject.Convert("문자열", null));
//dd(LRPCObject.Convert(new[] { 1, 1, 2, 4, }, null));
//dd(LRPCObject.Convert(new object[] { "와", "샌즈", new object[] { 1, 32, "zzz" }, "아시는구나", new[] { "d", "f" } }, null));
//dd(LRPCObject.Convert(new object[] { "와", "샌즈", new[] { 1, 32 }, "아시는구나", new[] { "d", "f" } }, null));

//var d = new Dictionary<string, object> { { "asd", "dd" }, { "asdf", "sdfsd" } };
//dd(new LRPCDynamicObject(d));

Console.Read();

//void dd(object obj) {
//    var e = Serialize(obj);
//    Console.WriteLine(e);
//    Console.WriteLine();
//    Console.WriteLine(obj);
//    Console.WriteLine(Deserialized(e, obj.GetType()));
//    Console.WriteLine();
//    Console.WriteLine();
//}

//string Serialize(object dd) =>
//    JsonSerializer.Serialize(dd, dd.GetType());

//T Deserialize<T>(string json) =>
//    (T)JsonSerializer.Deserialize(json, typeof(T));

//object Deserialized(string json, Type type) =>
//    JsonSerializer.Deserialize(json, type);