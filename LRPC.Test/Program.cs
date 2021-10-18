using LRPC.NET;

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