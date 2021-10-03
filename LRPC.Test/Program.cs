using LRPC.NET.Http;
using System;
using System.Net;

var server = new HttpServer("http://localhost:8080/");

server.Get("/", async (HttpRequest req, HttpResponse res) => {
    res.Content = new StringContent("<h1>Hello world!</h1>");
    await res.SendAsync(HttpStatusCode.OK);
});

server.BeginListen();
Console.ReadLine();