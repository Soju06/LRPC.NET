//using LRPC.NET.Http;
//using System.Net;
//using System.Net.WebSockets;

//var server = new HttpServer("http://localhost:8080/");

//WebSocket ws = null;

//server.Get("/", async (HttpRequest req, HttpResponse res) => {
//    if (!req.IsWebSocket) {
//        res.Content = server.GetMessageContnet("Hello world", "Hello world", HttpStatusCode.OK, req.RequestId);
//        await res.SendAsync();
//        return;
//    }
//    var wsc = await req.AcceptWebSocketAsync("asdf");
//    ws = wsc.WebSocket;
//    var buffer = new byte[8192];
//    while (ws.State == WebSocketState.Open) {
//        var receive = await ws.ReceiveAsync(buffer, default);
//        if (receive.MessageType == WebSocketMessageType.Close)
//            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", default);
//        else if (receive.MessageType == WebSocketMessageType.Text && receive.EndOfMessage) {
//            Console.WriteLine();
//            Console.WriteLine($"ws {req.IPEndPoint}: {buffer.Decode(0, receive.Count)}");
//        }
//        else await ws.CloseAsync(WebSocketCloseStatus.InvalidMessageType, "", default);
//    }
//});

//server.BeginListen();

//Task.Factory.StartNew(async () => {
//    while (true) {
//        Console.Title = $"LRPC.Test | WebSocket connections: {server.WebSocketConnectionsCount}";
//        await Task.Delay(500);
//    }
//});

//while (true) {
//    if (ws == null || ws.State != WebSocketState.Open) {
//        Thread.Sleep(100);
//        continue;
//    }

//    Console.WriteLine();
//    Console.Write("ws sent: ");
//    var m = Console.ReadLine();
//    if(ws != null) {
//        ws.SendAsync(new ArraySegment<byte>(m.Encode()), WebSocketMessageType.Text, true, default);
//    }
//}