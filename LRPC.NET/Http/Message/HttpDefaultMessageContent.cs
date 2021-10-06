namespace LRPC.NET.Http {
    /// <summary>
    /// Http 기본 메시지
    /// </summary>
    public class HttpDefaultMessageContent : HttpMessageContnet {
        public HttpDefaultMessageContent() {
            ContentType = ContentTypes.HTML;
        }

        protected override async Task ContentCopyToAsync(Stream stream) =>
            await stream.WriteAsync(HTML.Format(Title ?? $"{StatusCode} {Message}", Message, (int)StatusCode, ServerName, RequestId).Encode());

        public static string HTML = @"
<html>
    <head>
        <title>{0}</title>
    </head>
    <body>
        <center><h1>{2} {1}</h1></center>
        <hr>
        <center>{3} | RQ_ID: {4}</center>
    </body>
</html>
";
    }
}
