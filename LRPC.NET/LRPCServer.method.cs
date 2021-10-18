using System;
using System.Collections.Generic;
using System.Text;

namespace LRPC.NET {
    partial class LRPCServer {
        /// <summary>
        /// 매소드 저장소
        /// </summary>
        public LRPCMethodRepository Methods { get; set; } = new();
    }
}
