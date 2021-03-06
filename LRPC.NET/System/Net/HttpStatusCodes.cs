namespace System.Net {
    /// <summary>
    /// http 상태 코드
    /// </summary>
    public class HttpStatusCodes {
        /// <summary>
        /// Equivalent to HTTP status 300. System.Net.HttpStatusCode.Ambiguous indicates
        /// that the requested information has multiple representations. The default action
        /// is to treat this status as a redirect and follow the contents of the Location
        /// header associated with this response. Ambiguous is a synonym for MultipleChoices.
        /// </summary>
        public const int Ambiguous = 300;
        /// <summary>
        /// Equivalent to HTTP status 502. System.Net.HttpStatusCode.BadGateway indicates
        /// that an intermediate proxy server received a bad response from another proxy
        /// or the origin server.
        /// </summary>
        public const int BadGateway = 502;
        /// <summary>
        /// Equivalent to HTTP status 400. System.Net.HttpStatusCode.BadRequest indicates
        /// that the request could not be understood by the server. System.Net.HttpStatusCode.BadRequest
        /// is sent when no other error is applicable, or if the exact error is unknown or
        /// does not have its own error code.
        /// </summary>
        public const int BadRequest = 400;
        /// <summary>
        /// Equivalent to HTTP status 409. System.Net.HttpStatusCode.Conflict indicates that
        /// the request could not be carried out because of a conflict on the server.
        /// </summary>
        public const int Conflict = 409;
        /// <summary>
        /// Equivalent to HTTP status 100. System.Net.HttpStatusCode.Continue indicates that
        /// the client can continue with its request.
        /// </summary>
        public const int Continue = 100;
        /// <summary>
        /// Equivalent to HTTP status 201. System.Net.HttpStatusCode.Created indicates that
        /// the request resulted in a new resource created before the response was sent.
        /// </summary>
        public const int Created = 201;
        public const int EarlyHints = 103;
        /// <summary>
        /// Equivalent to HTTP status 417. System.Net.HttpStatusCode.ExpectationFailed indicates
        /// that an expectation given in an Expect header could not be met by the server.
        /// </summary>
        public const int ExpectationFailed = 417;
        public const int FailedDependency = 424;
        /// <summary>
        /// Equivalent to HTTP status 403. System.Net.HttpStatusCode.Forbidden indicates
        /// that the server refuses to fulfill the request.
        /// </summary>
        public const int Forbidden = 403;
        /// <summary>
        /// Equivalent to HTTP status 302. System.Net.HttpStatusCode.Found indicates that
        /// the requested information is located at the URI specified in the Location header.
        /// The default action when this status is received is to follow the Location header
        /// associated with the response. When the original request method was POST, the
        /// redirected request will use the GET method. Found is a synonym for Redirect.
        /// </summary>
        public const int Found = 302;
        /// <summary>
        /// Equivalent to HTTP status 504. System.Net.HttpStatusCode.GatewayTimeout indicates
        /// that an intermediate proxy server timed out while waiting for a response from
        /// another proxy or the origin server.
        /// </summary>
        public const int GatewayTimeout = 504;
        /// <summary>
        /// Equivalent to HTTP status 410. System.Net.HttpStatusCode.Gone indicates that
        /// the requested resource is no longer available.
        /// </summary>
        public const int Gone = 410;
        /// <summary>
        /// Equivalent to HTTP status 505. System.Net.HttpStatusCode.HttpVersionNotSupported
        /// indicates that the requested HTTP version is not supported by the server.
        /// </summary>
        public const int HttpVersionNotSupported = 505;
        public const int IMUsed = 226;
        public const int InsufficientStorage = 507;
        /// <summary>
        /// Equivalent to HTTP status 500. System.Net.HttpStatusCode.InternalServerError
        /// indicates that a generic error has occurred on the server.
        /// </summary>
        public const int InternalServerError = 500;
        /// <summary>
        /// Equivalent to HTTP status 411. System.Net.HttpStatusCode.LengthRequired indicates
        /// that the required Content-length header is missing.
        /// </summary>
        public const int LengthRequired = 411;
        public const int Locked = 423;
        public const int LoopDetected = 508;
        /// <summary>
        /// Equivalent to HTTP status 405. System.Net.HttpStatusCode.MethodNotAllowed indicates
        /// that the request method (POST or GET) is not allowed on the requested resource.
        /// </summary>
        public const int MethodNotAllowed = 405;
        public const int MisdirectedRequest = 421;
        /// <summary>
        /// Equivalent to HTTP status 301. System.Net.HttpStatusCode.Moved indicates that
        /// the requested information has been moved to the URI specified in the Location
        /// header. The default action when this status is received is to follow the Location
        /// header associated with the response. When the original request method was POST,
        /// the redirected request will use the GET method. Moved is a synonym for MovedPermanently.
        /// </summary>
        public const int Moved = 301;
        /// <summary>
        /// Equivalent to HTTP status 301. System.Net.HttpStatusCode.MovedPermanently indicates
        /// that the requested information has been moved to the URI specified in the Location
        /// header. The default action when this status is received is to follow the Location
        /// header associated with the response. MovedPermanently is a synonym for Moved.
        /// </summary>
        public const int MovedPermanently = 301;
        /// <summary>
        /// Equivalent to HTTP status 300. System.Net.HttpStatusCode.MultipleChoices indicates
        /// that the requested information has multiple representations. The default action
        /// is to treat this status as a redirect and follow the contents of the Location
        /// header associated with this response. MultipleChoices is a synonym for Ambiguous.
        /// </summary>
        public const int MultipleChoices = 300;
        public const int MultiStatus = 207;
        public const int NetworkAuthenticationRequired = 0x1FF;
        /// <summary>
        /// Equivalent to HTTP status 204. System.Net.HttpStatusCode.NoContent indicates
        /// that the request has been successfully processed and that the response is intentionally
        /// blank.
        /// </summary>
        public const int NoContent = 204;
        /// <summary>
        /// Equivalent to HTTP status 203. System.Net.HttpStatusCode.NonAuthoritativeInformation
        /// indicates that the returned metainformation is from a cached copy instead of
        /// the origin server and therefore may be incorrect.
        /// </summary>
        public const int NonAuthoritativeInformation = 203;
        /// <summary>
        /// Equivalent to HTTP status 406. System.Net.HttpStatusCode.NotAcceptable indicates
        /// that the client has indicated with Accept headers that it will not accept any
        /// of the available representations of the resource.
        /// </summary>
        public const int NotAcceptable = 406;
        public const int NotExtended = 510;
        /// <summary>
        /// Equivalent to HTTP status 404. System.Net.HttpStatusCode.NotFound indicates that
        /// the requested resource does not exist on the server.
        /// </summary>
        public const int NotFound = 404;
        /// <summary>
        /// Equivalent to HTTP status 501. System.Net.HttpStatusCode.NotImplemented indicates
        /// that the server does not support the requested function.
        /// </summary>
        public const int NotImplemented = 501;
        /// <summary>
        /// Equivalent to HTTP status 304. System.Net.HttpStatusCode.NotModified indicates
        /// that the client's cached copy is up to date. The contents of the resource are
        /// not transferred.
        /// </summary>
        public const int NotModified = 304;
        /// <summary>
        /// Equivalent to HTTP status 200. System.Net.HttpStatusCode.OK indicates that the
        /// request succeeded and that the requested information is in the response. This
        /// is the most common status code to receive.
        /// </summary>
        public const int OK = 200;
        /// <summary>
        /// Equivalent to HTTP status 206. System.Net.HttpStatusCode.PartialContent indicates
        /// that the response is a partial response as requested by a GET request that includes
        /// a byte range.
        /// </summary>
        public const int PartialContent = 206;
        /// <summary>
        /// Equivalent to HTTP status 402. System.Net.HttpStatusCode.PaymentRequired is reserved
        /// for future use.
        /// </summary>
        public const int PaymentRequired = 402;
        public const int PermanentRedirect = 308;
        /// <summary>
        /// Equivalent to HTTP status 412. System.Net.HttpStatusCode.PreconditionFailed indicates
        /// that a condition set for this request failed, and the request cannot be carried
        /// out. Conditions are set with conditional request headers like If-Match, If-None-Match,
        /// or If-Unmodified-Since.
        /// </summary>
        public const int PreconditionFailed = 412;
        public const int PreconditionRequired = 428;
        public const int Processing = 102;
        /// <summary>
        /// Equivalent to HTTP status 407. System.Net.HttpStatusCode.ProxyAuthenticationRequired
        /// indicates that the requested proxy requires authentication. The Proxy-authenticate
        /// header contains the details of how to perform the authentication.
        /// </summary>
        public const int ProxyAuthenticationRequired = 407;
        /// <summary>
        /// Equivalent to HTTP status 302. System.Net.HttpStatusCode.Redirect indicates that
        /// the requested information is located at the URI specified in the Location header.
        /// The default action when this status is received is to follow the Location header
        /// associated with the response. When the original request method was POST, the
        /// redirected request will use the GET method. Redirect is a synonym for Found.
        /// </summary>
        public const int Redirect = 302;
        /// <summary>
        /// Equivalent to HTTP status 307. System.Net.HttpStatusCode.RedirectKeepVerb indicates
        /// that the request information is located at the URI specified in the Location
        /// header. The default action when this status is received is to follow the Location
        /// header associated with the response. When the original request method was POST,
        /// the redirected request will also use the POST method. RedirectKeepVerb is a synonym
        /// for TemporaryRedirect.
        /// </summary>
        public const int RedirectKeepVerb = 307;
        /// <summary>
        /// Equivalent to HTTP status 303. System.Net.HttpStatusCode.RedirectMethod automatically
        /// redirects the client to the URI specified in the Location header as the result
        /// of a POST. The request to the resource specified by the Location header will
        /// be made with a GET. RedirectMethod is a synonym for SeeOther.
        /// </summary>
        public const int RedirectMethod = 303;
        /// <summary>
        /// Equivalent to HTTP status 416. System.Net.HttpStatusCode.RequestedRangeNotSatisfiable
        /// indicates that the range of data requested from the resource cannot be returned,
        /// either because the beginning of the range is before the beginning of the resource,
        /// or the end of the range is after the end of the resource.
        /// </summary>
        public const int RequestedRangeNotSatisfiable = 416;
        /// <summary>
        /// Equivalent to HTTP status 413. System.Net.HttpStatusCode.RequestEntityTooLarge
        /// indicates that the request is too large for the server to process.
        /// </summary>
        public const int RequestEntityTooLarge = 413;
        public const int RequestHeaderFieldsTooLarge = 431;
        /// <summary>
        /// Equivalent to HTTP status 408. System.Net.HttpStatusCode.RequestTimeout indicates
        /// that the client did not send a request within the time the server was expecting
        /// the request.
        /// </summary>
        public const int RequestTimeout = 408;
        /// <summary>
        /// Equivalent to HTTP status 414. System.Net.HttpStatusCode.RequestUriTooLong indicates
        /// that the URI is too long.
        /// </summary>
        public const int RequestUriTooLong = 414;
        /// <summary>
        /// Equivalent to HTTP status 205. System.Net.HttpStatusCode.ResetContent indicates
        /// that the client should reset (not reload) the current resource.
        /// </summary>
        public const int ResetContent = 205;
        /// <summary>
        /// Equivalent to HTTP status 303. System.Net.HttpStatusCode.SeeOther automatically
        /// redirects the client to the URI specified in the Location header as the result
        /// of a POST. The request to the resource specified by the Location header will
        /// be made with a GET. SeeOther is a synonym for RedirectMethod
        /// </summary>
        public const int SeeOther = 303;
        /// <summary>
        /// Equivalent to HTTP status 503. System.Net.HttpStatusCode.ServiceUnavailable indicates
        /// that the server is temporarily unavailable, usually due to high load or maintenance.
        /// </summary>
        public const int ServiceUnavailable = 503;
        /// <summary>
        /// Equivalent to HTTP status 101. System.Net.HttpStatusCode.SwitchingProtocols indicates
        /// that the protocol version or protocol is being changed.
        /// </summary>
        public const int SwitchingProtocols = 101;
        /// <summary>
        /// Equivalent to HTTP status 307. System.Net.HttpStatusCode.TemporaryRedirect indicates
        /// that the request information is located at the URI specified in the Location
        /// header. The default action when this status is received is to follow the Location
        /// header associated with the response. When the original request method was POST,
        /// the redirected request will also use the POST method. TemporaryRedirect is a
        /// synonym for RedirectKeepVerb.
        /// </summary>
        public const int TemporaryRedirect = 307;
        public const int TooManyRequests = 429;
        /// <summary>
        /// Equivalent to HTTP status 401. System.Net.HttpStatusCode.Unauthorized indicates
        /// that the requested resource requires authentication. The WWW-Authenticate header
        /// contains the details of how to perform the authentication.
        /// </summary>
        public const int Unauthorized = 401;
        public const int UnavailableForLegalReasons = 451;
        public const int UnprocessableEntity = 422;
        /// <summary>
        /// Equivalent to HTTP status 415. System.Net.HttpStatusCode.UnsupportedMediaType
        /// indicates that the request is an unsupported type.
        /// </summary>
        public const int UnsupportedMediaType = 415;
        /// <summary>
        /// Equivalent to HTTP status 306. System.Net.HttpStatusCode.Unused is a proposed
        /// extension to the HTTP/1.1 specification that is not fully specified.
        /// </summary>
        public const int Unused = 306;
        /// <summary>
        /// Equivalent to HTTP status 426. System.Net.HttpStatusCode.UpgradeRequired indicates
        /// that the client should switch to a different protocol such as TLS/1.0.
        /// </summary>
        public const int UpgradeRequired = 426;
        /// <summary>
        /// Equivalent to HTTP status 305. System.Net.HttpStatusCode.UseProxy indicates that
        /// the request should use the proxy server at the URI specified in the Location
        /// header.
        /// </summary>
        public const int UseProxy = 305;
    }
}
