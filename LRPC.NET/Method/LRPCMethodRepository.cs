using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace LRPC.NET {
    public class LRPCMethodRepository : DoubleDictionary<Guid, string, LRPCMethodBase> {
        public LRPCMethodRepository() {

        }

        /// <summary>
        /// 현재 개체를 직렬화 가능한 배열을 만듭니다.
        /// </summary>
        public LRPCMethodBase[] GetSerializableArray() {
            var methods = new LRPCMethodBase[Count];
            Values.CopyTo(methods, 0);
            return methods;
        }

        /// <summary>
        /// 매소드를 추가합니다.
        /// </summary>
        /// <param name="method">매소드</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddMethod(LRPCMethodBase method) {
            if (method == null) throw new ArgumentNullException("method");
            //Add(method.MethodId, method);
        }

        /// <summary>
        /// 해당 매소드를 호출합니다
        /// </summary>
        /// <param name="id">매소드 id</param>
        /// <param name="parameters">파라미터</param>
        /// <returns>리턴값</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public object Invoke(Guid id, params object[] parameters) {
            if (TryGetValue(id, out LRPCMethodBase method))
                return method.Invoke(parameters: parameters);
            else throw new KeyNotFoundException($"method does not exist for id {id}");
        }

        /// <summary>
        /// 매소드를 찾습니다.
        ///     리턴값: 매소드가 없으면 null를 반환합니다.
        /// </summary>
        /// <param name="id"></param>
        public LRPCMethodBase? Find(Guid id) {
            if (TryGetValue(id, out LRPCMethodBase method)) return method;
            return null;
        }

        /// <summary>
        /// 매소드가 존재하는지 찾습니다.
        ///     리턴값: 매소드가 존재하면 true, 아니면 false
        /// </summary>
        /// <param name="id">매소드 id</param>
        public bool Exists(Guid id) =>
            ContainsKey(id);

        /// <summary>
        /// 충돌되지 않는 매소드 고유 Id를 만듭니다.
        /// </summary>
        /// <param name="method">매소드 정보</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Guid CreateMethodId(MethodInfo method) {
            if (method == null) throw new ArgumentNullException("method");
            int collided = 0;
            var decType = method.DeclaringType;
            var input = decType.FullName + "." + method.Name;
            Guid nguid;

            SP_POINT:
            if (collided <= 1) {
                if (collided > 0) input += "/" + decType.Assembly.FullName;

                byte[] hash;
                using (var alg = SHA1.Create()) hash = alg.ComputeHash(Encoding.
                    UTF8.GetBytes(input));

                byte[] guid = new byte[16];
                Array.Copy(hash, 0, guid, 0, 16);
                guid[6] = (byte)((guid[6] & 0x0F) | (5 << 4));
                guid[8] = (byte)((guid[8] & 0x3F) | 0x80);

                SwapBytes(guid, 0, 3);
                SwapBytes(guid, 1, 2);
                SwapBytes(guid, 4, 5);
                SwapBytes(guid, 6, 7);

                nguid = new Guid(guid);
            } else nguid = Guid.NewGuid();

            if (ContainsKey(nguid)) {
                collided++;
                goto SP_POINT;
            }

            return nguid;

            static void SwapBytes(byte[] guid, int left, int right) {
                var temp = guid[left];
                guid[left] = guid[right];
                guid[right] = temp;
            }
        }
    }

    public static class LRPCMethodRepositoryStatic {
        public static string GetJsonArray(this LRPCMethodRepository repository, JsonSerializerOptions? options = null) {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            var array = repository.GetSerializableArray();
            return JsonSerializer.Serialize(array, options);
        }
    }
}