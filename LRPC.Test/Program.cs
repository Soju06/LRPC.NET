using LRPC.NET;
using LRPC.NET.Object;
using System.Text.Json;

new LRPCServer("http://localhost:8080/").Return(server => {
    server.Methods.CreateMethod(() => { 

    });
    server.Methods.CreateMethod(() => {
        return "asdasd";
    });

    server.Methods.CreateMethod(() => {
        return 13;
    });
}).Start();

Console.Read();