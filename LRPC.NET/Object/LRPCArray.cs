using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace LRPC.NET.Object {
    /// <summary>
    /// LRPC 배열
    /// </summary>
    public class LRPCArray : LRPCObject, ICollection<LRPCObject>, IEnumerable<LRPCObject>, ICloneable {
        bool isReadOnly;
        readonly List<LRPCObject> innor = new();

        public LRPCArray() {

        }

        public LRPCArray(IEnumerable<LRPCObject> objects) {
            TypeName = LRPCObjectTypes.Array;
            AddRange(objects);
        }
        
        public LRPCArray(IEnumerable objects) {
            TypeName = LRPCObjectTypes.Array;
            AddRange(objects);
        }
        
        public LRPCArray(params LRPCObject[] objects) {
            TypeName = LRPCObjectTypes.Array;
            AddRange(objects);
        }

        public LRPCArray(params object[] objects) {
            TypeName = LRPCObjectTypes.Array;
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

        [JsonPropertyName("items")]
        object[] Objects => CloneArray();

        /// <summary>
        /// 갯수
        /// </summary>
        [JsonPropertyName("_count")]
        public int Count => innor.Count;

        /// <summary>
        /// 읽기전용 여부
        /// </summary>
        [JsonPropertyName("_isreadonly")]
        public bool IsReadOnly => isReadOnly;

        public void Add(LRPCObject item) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.Add(item);
        }
        
        public void AddRange(params LRPCObject[] items) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.AddRange(items);
        }
        
        public void AddRange(IEnumerable<LRPCObject> items) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            innor.AddRange(items);
        }
        
        public void AddRange(params object[] items) {
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

        public bool Remove(LRPCObject item) {
            if (isReadOnly) throw new NotSupportedException("이 개체는 읽기 전용이므로 수정할 수 없습니다.");
            return innor.Remove(item);
        }
        
        public static LRPCObject[] ConvertArray(IEnumerable array) {

        }

        public static LRPCObject[] ConvertArray(params object[] array) {

        }
        
        public static LRPCObject[] ConvertArray(Array array) {

        }

        public bool Contains(LRPCObject item) =>
            innor.Contains(item);

        public object Clone() => CloneArray();

        public object[] CloneArray() => innor.ToArray();

        public void CopyTo(LRPCObject[] array, int arrayIndex) =>
            innor.CopyTo(array, arrayIndex);

        public IEnumerator<LRPCObject> GetEnumerator() {
            if (innor == null) yield break;
            foreach (var item in innor)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
