using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LRPC.NET.Object {
    /// <summary>
    /// LRPC 다이나믹 오브젝트
    /// </summary>
    public class LRPCDynamicObject : LRPCObject, ICloneable {
        readonly Dictionary<string, LRPCObject?> objects = new();

        public LRPCDynamicObject() {
            TypeName = LRPCObjectTypes.Dynamic;
        }
        
        public LRPCDynamicObject(IEnumerable<KeyValuePair<string, LRPCObject?>> objects) {
            AddObjectRange(objects);
        }

        public LRPCDynamicObject(IEnumerable<KeyValuePair<string, object?>> objects) {
            AddObjectRange(objects);
        }


        /// <summary>
        /// 오브젝트를 가져옵니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="KeyNotFoundException" />
        public LRPCObject? GetObject(string name) =>
            objects[name];

        /// <summary>
        /// 오브젝트를 가져오거나 설정합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="KeyNotFoundException" />
        public LRPCObject? this[string name] {
            get => objects[name];
            set => objects[name] = value;
        }

        /// <summary>
        /// 개체를 추가합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <param name="object">개체</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddObject(string name, object? @object) {
            if (name == null) throw new ArgumentNullException("name");
            EnsureNamingConvention(name);
            objects.Add(name, Convert(@object, @object?.GetType() ?? typeof(LRPCObject)));
        }

        /// <summary>
        /// 개체를 추가합니다.
        /// </summary>
        /// <param name="objects">개체</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddObjectRange(IEnumerable<KeyValuePair<string, LRPCObject?>> objects) {
            if (objects == null) throw new ArgumentNullException("objects");
            foreach (var item in objects)
                AddObject(item.Key, item.Value);
        }
        
        /// <summary>
        /// 개체를 추가합니다.
        /// </summary>
        /// <param name="objects">개체</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddObjectRange(IEnumerable<KeyValuePair<string, object?>> objects) {
            if (objects == null) throw new ArgumentNullException("objects");
            foreach (var item in objects)
                AddObject(item.Key, item.Value);
        }

        /// <summary>
        /// 직렬화용 개체를 만듭니다.
        /// </summary>
        [JsonPropertyName("objects")]
        public Dictionary<string, object?> Objects => CloneArray();

        public object Clone() => CloneArray();

        public Dictionary<string, object?> CloneArray() {
            var dic = new Dictionary<string, object?>();
            foreach (var item in objects) { 
                var key = item.Key;
                var type = item.Value?.GetType();
                object? value;
                if (type == null) value = null;
                else value = System.Convert.ChangeType(item.Value, type);
                dic.Add(key, value);
            } return dic;
        }

        /// <summary>
        /// 네이밍 규칙에 일치하는지 확인합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void EnsureNamingConvention(string? name) {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name) || name.Contains(' ')) throw new ArgumentException("이름은 공백문자를 지원하지 않습니다.");
            if (char.IsDigit(name[0])) throw new ArgumentException("이름의 시작문자는 숫자를 사용할 수 없습니다.");
            if (!name.All(c => c == '_' || !char.IsPunctuation(c))) throw new ArgumentException("이름에 특수문자가 있습니다.");
        }
    }
}
