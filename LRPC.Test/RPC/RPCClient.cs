using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace LRPC.Test.RPC
{
    internal class RPCClient
    {
        ClientWebSocket Client;
        byte[] dataBuf = new byte[10000];

        public RPCClient(string p)
        {
            var client = new ClientWebSocket();
            client.Options.AddSubProtocol("LRPC");
            client.ConnectAsync(p.Uri(), default).Wait();
            Client = client;
        }

        public async Task Greeting() {
            var geBuf = new byte[256];
            var re = await Client.ReceiveAsync(geBuf, new CancellationTokenSource(5000).Token);

            if (re.EndOfMessage) throw new NotSupportedException();
            if (re.MessageType != WebSocketMessageType.Text) throw new NotSupportedException();
            if (geBuf.Decode(0, re.Count) != "LRPC Protocol!!!") throw new NotSupportedException();
            await Client.SendAsync(new ArraySegment<byte>("LRPC Protocol!!!".Encode()), WebSocketMessageType.Text, false, default);
            await Task.Delay(1000);
            await Client.ReceiveAsync(new ArraySegment<byte>(geBuf), default);
        }

        public async Task<object> Rpc(string name, params object?[]? par)
        {
            await Client.SendAsync(Bson.Serialize(new RpcInvoke() { Name = name, Parameters = par }), WebSocketMessageType.Binary, false, default);
            var re = await Client.ReceiveAsync(dataBuf, default);
            if (re.EndOfMessage) throw new NotSupportedException();
            var ret = Bson.Deserialize<RpcReturn>(dataBuf, 0, re.Count);
            if (!ret.IsSuccess) throw new Exception(ret.StateCode == StateCode.NotFound ? "method not found" : ret.StateCode == StateCode.Error ? ret.ErrorMessage : ret.Return.ToString());
            return ret.Return;
        }
    }
}
