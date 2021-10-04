using System.Net.WebSockets;

namespace LRPC.NET.Http {
    partial class HttpServer {
        int webSocketConnectionsCount;

        /// <summary>
        /// 요청 처리자를 웹소켓 처리자로 설정합니다.
        /// </summary>
        /// <param name="routeFunc">요청 처리자</param>
        internal void AcceptWebSocket(WebSocket socket) =>
            Task.Factory.StartNew(() => CheckWebSocketConnenction(socket));

        protected virtual async void CheckWebSocketConnenction(WebSocket socket) {
            webSocketConnectionsCount++;
            try {
                while (socket != null && socket.State switch {
                    WebSocketState.Open or WebSocketState.Connecting => true,
                    _ => false }) await Task.Delay(100);
            } catch {

            }
            webSocketConnectionsCount--;
        }

        /// <summary>
        /// 웹소켓 연결 수
        /// </summary>
        public int WebSocketConnectionsCount => webSocketConnectionsCount;
    }
}
