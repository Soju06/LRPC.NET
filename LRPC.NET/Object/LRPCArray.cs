using System.Collections;
using System.Text.Json.Serialization;

namespace LRPC.NET.Object {
    /// <summary>
    /// LRPC 배열
    /// </summary>
    public class LRPCArray : LRPCObject, ICloneable {
        readonly List<LRPCObject?> innor = new();
        bool isReadOnly;

        public LRPCArray() {

        }

        public LRPCArray(IEnumerable<LRPCObject?> objects) {
            TypeName = LRPCObjectTypes.Array;
            AddRange(objects);
        }
        
        public LRPCArray(IEnumerable objects) {
            TypeName = LRPCObjectTypes.Array;
            AddRange(objects);
        }
        
        public LRPCArray(params LRPCObject?[] objects) {
            TypeName = LRPCObjectTypes.Array;
            isReadOnly = true;
            AddRange(objects);
        }

        public LRPCArray(params object?[] objects) {
            TypeName = LRPCObjectTypes.Array;
            isReadOnly = true;
            AddRange(objects);
        }

        [JsonConstructor]
        public LRPCArray(int count, object[] objects, bool isReadOnly) {
            TypeName = LRPCObjectTypes.Array;
            this.isReadOnly = isReadOnly;
            if (objects != null) {
                if (count != objects.Length) throw new ProtocolViolationException(
                    "명시된 갯수와 오브젝트의 갯수가 일치하지 않습니다.");
                AddRange(objects);
            }
        }

        /// <summary>
        /// 직렬화 가능한 개체를 반환합니다.
        /// </summary>
        [JsonPropertyName("items")]
        public object?[] Objects => CloneArray();

        /// <summary>
        /// 갯수
        /// </summary>
        [JsonPropertyName("_count")]
        public int Count => innor.Count;

        /// <summary>
        /// 길이
        /// </summary>
        [JsonIgnore]
        public int Length => innor.Count;

        /// <summary>
        /// 읽기전용 여부
        /// </summary>
        [JsonPropertyName("_isreadonly")]
        public bool IsReadOnly => isReadOnly;

        public LRPCObject? this[int index] { get => innor[index]; set => innor[index] = value; }

        public void Add(LRPCObject? item) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.Add(item);
        }
        
        public void AddRange(params LRPCObject?[] items) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.AddRange(items);
        }
        
        public void AddRange(IEnumerable<LRPCObject?> items) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.AddRange(items);
        }
        
        public void AddRange(params object?[] items) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            AddRange((IEnumerable<object>)items);
        }
        
        public void AddRange(IEnumerable items) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.AddRange(ConvertArray(items));
        }

        public void Clear() {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.Clear();
        }

        public bool Remove(LRPCObject? item) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            return innor.Remove(item);
        }
        
        public void RemoveAt(int index) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.RemoveAt(index);
        }

        public int IndexOf(LRPCObject? item) =>
            innor.IndexOf(item);

        public void Insert(int index, LRPCObject? item) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.Insert(index, item);
        }
        
        public static IEnumerable<LRPCObject?> ConvertArray(IEnumerable array) {
            foreach (var item in array) {
                if (item == null) yield return null;
                yield return Convert(item, null);
            }
        }

        public static IEnumerable<LRPCObject?> ConvertArray(params object[] array) {
            var length = array.Length;
            for (int i = 0; i < length; i++) {
                var item = array[i];
                if (item == null) yield return null;
                yield return Convert(item, null);
            }
        }
        
        public static IEnumerable<LRPCObject?> ConvertArray(Array array) {
            var length = array.Length;
            for (int i = 0; i < length; i++) {
                var item = array.GetValue(i);
                if (item == null) yield return null;
                yield return Convert(item, null);
            }
        }

        public bool Contains(LRPCObject? item) =>
            innor.Contains(item);

        public object Clone() => CloneArray();

        public object?[] CloneArray() {
            var copy = innor.ToArray();
            var length = copy.Length;
            var result = new object?[length];
            for (int i = 0; i < length; i++) {
                var type = copy[i]?.GetType();
                if (type == null)
                    result[i] = copy[i];
                else result[i] = System.Convert.ChangeType(copy[i], type);
            } return result;
        }

        public void CopyTo(LRPCObject?[] array, int arrayIndex) =>
            innor.CopyTo(array, arrayIndex);

        public IEnumerator<LRPCObject?> GetEnumerator() {
            if (innor == null) yield break;
            foreach (var item in innor)
                yield return item;
        }

        public IEnumerable<LRPCObject?> GetEnumerable() {
            if (innor == null) yield break;
            foreach (var item in innor)
                yield return item;
        }

        /// <summary>
        /// 개체를 읽기전용으로 잠굽니다.
        /// </summary>
        /// <returns></returns>
        public void Lock() {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            else isReadOnly = true;
        }

        public override string ToString() => string.Join(' ', innor);
    }
}
