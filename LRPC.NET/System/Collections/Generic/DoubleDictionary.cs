/* ========= Soju06 DoubleDictionary =========
 * Language Version: Csharp 9.0
 * NAMESPACE: System.Collections.Generic
 * VERSION: 1
 * LICENSE: MIT
 * Copyright by Soju06
 * ========= Soju06 DoubleDictionary ========= */
using System.ComponentModel;

namespace System.Collections.Generic {
    /// <summary>
    /// 더블 딕셔너리
    /// </summary>
    public class DoubleDictionary<TKey1, TKey2, TValue> : IEnumerable<DoubleKeyValuePair<TKey1, TKey2, TValue>> {
        readonly Dictionary<TKey1, TValue> Inner1 = new();
        readonly Dictionary<TKey2, TValue> Inner2 = new();
        readonly Dictionary<TKey1, TKey2> InnerKeys1 = new();
        readonly Dictionary<TKey2, TKey1> InnerKeys2 = new();

        public DoubleDictionary() {
            
        }
        
        public DoubleDictionary(IEnumerable<DoubleKeyValuePair<TKey1, TKey2, TValue>> items) {
            if (items == null) throw new ArgumentNullException("items");
            foreach (var item in items) 
                Add(item.Key1, item.Key2, item.Value);
        }

        /// <summary>
        /// 값을 설정합니다.
        /// </summary>
        /// <param name="key">키</param>
        /// <param name="value">값</param>
        public virtual void SetValue(TKey1 key, TValue value) {
            lock (this) {
                Inner1[key] = value;
                Inner2[InnerKeys1[key]] = value;
            }
        }

        /// <summary>
        /// 값을 설정합니다.
        /// </summary>
        /// <param name="key">키</param>
        /// <param name="value">값</param>
        public virtual void SetValue(TKey2 key, TValue value) {
            lock (this) {
                Inner2[key] = value;
                Inner1[InnerKeys2[key]] = value;
            }
        }

        public virtual TValue this[TKey1 key] {
            get => Inner1[key];
            set => SetValue(key, value);
        }
        public virtual TValue this[TKey2 key] {
            get => Inner2[key];
            set => SetValue(key, value);
        }

        public ICollection<TKey1> Keys1 => Inner1.Keys;
        public ICollection<TKey2> Keys2 => Inner2.Keys;

        public ICollection<TValue> Values => Inner1.Values;

        /// <summary>
        /// 갯수
        /// </summary>
        public int Count => Inner1.Count;

        public bool IsReadOnly => false;

        /// <summary>
        /// 지정된 키와 값을 사전에 추가합니다.
        /// </summary>
        /// <param name="key1">키1</param>
        /// <param name="key2">키2</param>
        /// <param name="value">값</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Add(TKey1 key1, TKey2 key2, TValue value) {
            lock (this) {
                if (key1 == null) throw new ArgumentNullException(nameof(key1));
                if (key2 == null) throw new ArgumentNullException(nameof(key2));
                if (value == null) throw new ArgumentNullException(nameof(value));
                Inner1.Add(key1, value);
                Inner2.Add(key2, value);
                InnerKeys1.Add(key1, key2);
                InnerKeys2.Add(key2, key1);
            }
        }

        /// <summary>
        /// 사전을 초기화합니다.
        /// </summary>
        public virtual void Clear() {
            lock (this) {
                Inner1.Clear();
                Inner2.Clear();
                InnerKeys1.Clear();
                InnerKeys2.Clear();
            }
        }

        public virtual bool Contains(DoubleKeyValuePair<TKey1, TKey2, TValue> item) {
            lock (this) {
                var (key1, key2, value) = item;
                return InnerKeys1.TryGetValue(key1, out var o_key2)
                    && (o_key2?.Equals(key2)) == true
                    && Inner1.TryGetValue(key1, out var o_value)
                    && (o_value?.Equals(value)) == true;
            }
        }

        public virtual bool Contains(KeyValuePair<TKey1, TValue> item) =>
            Inner1.Contains(item);

        public virtual bool Contains(KeyValuePair<TKey2, TValue> item) =>
            Inner2.Contains(item);

        public virtual bool ContainsKey(TKey1 key) =>
            Inner1.ContainsKey(key);

        public virtual bool ContainsKey(TKey2 key) =>
            Inner2.ContainsKey(key);

        /// <summary>
        /// 키1과 값을 반복하는 열거자를 반환합니다.
        /// </summary>
        public virtual IEnumerator<KeyValuePair<TKey1, TValue>> GetKey1Enumerator() =>
            Inner1.GetEnumerator();

        /// <summary>
        /// 키2와 값을 반복하는 열거자를 반환합니다.
        /// </summary>
        public virtual IEnumerator<KeyValuePair<TKey2, TValue>> GetKey2Enumerator() =>
            Inner2.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 키와 값을 반복하는 열거자를 반환합니다.
        /// </summary>
        public virtual IEnumerator<DoubleKeyValuePair<TKey1, TKey2, TValue>> GetEnumerator() {
            foreach (var item in InnerKeys2)
                yield return new(item.Value, item.Key, Inner2[item.Key]);
        }

        /// <summary>
        /// 사전에서 제거합니다.
        /// </summary>
        /// <param name="key">키</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual bool Remove(TKey1 key) {
            lock (this) {
                if (!InnerKeys1.TryGetValue(key, out var key2)) throw new KeyNotFoundException();
                return 
                    Inner1.Remove(key) &&
                    Inner2.Remove(key2) && 
                    InnerKeys1.Remove(key) && 
                    InnerKeys2.Remove(key2);
            }
        }

        /// <summary>
        /// 사전에서 제거합니다.
        /// </summary>
        public virtual bool Remove(DoubleKeyValuePair<TKey1, TKey2, TValue> item) {
            lock (this) {
                if (!Contains(item)) return false;
                return Remove(item.Key2);
            }
        }

        /// <summary>
        /// 사전에서 제거합니다.
        /// </summary>
        /// <param name="key">키</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual bool Remove(TKey2 key) {
            lock (this) {
                if (!InnerKeys2.TryGetValue(key, out var key1)) throw new KeyNotFoundException();
                return 
                    Inner2.Remove(key) &&
                    Inner1.Remove(key1) && 
                    InnerKeys2.Remove(key) && 
                    InnerKeys1.Remove(key1);
            }
        }

        public virtual bool TryGetValue(TKey1 key, out TValue value) =>
            Inner1.TryGetValue(key, out value);
        
        public virtual bool TryGetValue(TKey1 key, out TKey2 key2, out TValue value) {
            value = default;
            if (!InnerKeys1.TryGetValue(key, out key2)) return false;
            return Inner1.TryGetValue(key, out value);
        }

        public virtual bool TryGetValue(TKey2 key, out TValue value) =>
            Inner2.TryGetValue(key, out value);

        public virtual bool TryGetValue(TKey2 key, out TKey1 key1, out TValue value) {
            value = default;
            if (!InnerKeys2.TryGetValue(key, out key1)) return false;
            return Inner2.TryGetValue(key, out value);
        }
    }
    
    /// <summary>
    /// 2개의 키와 1개의 값 쌍
    /// </summary>
    /// <typeparam name="TKey1">키1</typeparam>
    /// <typeparam name="TKey2">키2</typeparam>
    /// <typeparam name="TValue">값</typeparam>
    public readonly struct DoubleKeyValuePair<TKey1, TKey2, TValue> {
        public DoubleKeyValuePair(TKey1 key1, TKey2 key2, TValue value) {
            Key1 = key1;
            Key2 = key2;
            Value = value;
        }

        /// <summary>
        /// 키1
        /// </summary>
        public TKey1 Key1 { get; }
        /// <summary>
        /// 키2
        /// </summary>
        public TKey2 Key2 { get; }
        /// <summary>
        /// 값
        /// </summary>
        public TValue Value { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out TKey1 key1, out TKey2 key2, out TValue value) {
            key1 = Key1;
            key2 = Key2;
            value = Value;
        }
    }
}
