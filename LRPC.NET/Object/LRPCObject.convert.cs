using System;
using System.Collections.Generic;
using System.Text;

namespace LRPC.NET.Object {
    partial class LRPCObject {
        delegate LRPCObject? ConvertFunc(object obj);
        public delegate bool IsConvertibleFunc(Type type);

        /// <summary>
        /// 다음의 개체를 빌트-인 개체로 변환할 수 있는지 확인합니다.
        /// </summary>
        /// <param name="obj">개체</param>
        public static bool IsConvertible(Type type) { 
            if(type == null) throw new ArgumentNullException("type");
            // 이미 빌트인 타입이면 프리패스.
            if (Types.Contains(type)) return true;

            foreach (var item in ConvertibleTypes) {
                var container = item.Value;
                // 빌트인 타입에서 지원하는 해시셋에 존재하면 true
                var hashset = container.Item3;
                if (hashset != null && hashset.Contains(type)) return true;
                // 지원여부 확인 전용함수
                var iscble = container.Item2;
                if (iscble != null && iscble.Invoke(type)) return true;
            } return false;
        }

        static LRPCObject? Convert(object? obj, Type? type, bool checkConvertable) {
            // 타입이 없을때 오브젝트의 타입을 박음. 오브젝트도 null일땐 터짐
            if (type == null) type = obj?.GetType() ?? throw new ArgumentNullException(nameof(obj),
                "개체가 null 일 때 명시된 타입이 없으므로 개체의 타입을 유추할 수 없습니다.");
            // 변환가능한 타입 확인
            if (checkConvertable && !IsConvertible(type)) throw new ArgumentException(nameof(obj),
                "변환가능한 타입에 개체의 타입이 속해있지 않습니다.");

            foreach (var item in ConvertibleTypes) {
                // TODO: dd
            }
            return null;
        }

        /// <summary>
        /// 빌트-인 타입
        /// </summary>
        public static readonly Type[] BuiltInObjectTypes = { 
            typeof(LRPCNumber),
            typeof(LRPCString),
            typeof(LRPCArray)
        };

        static readonly HashSet<Type> Types = new(BuiltInObjectTypes);

        static readonly Dictionary<Type, (ConvertFunc, IsConvertibleFunc?, HashSet<Type>?)> ConvertibleTypes = new() {
            {
                typeof(LRPCNumber),
                new(
                    obj => obj != null ? new LRPCNumber(obj, LRPCNumber.GetDisplayType(obj.GetType())) : null,
                    null,
                    new(new [] {
                        typeof(sbyte),
                        typeof(byte),
                        typeof(short),
                        typeof(ushort),
                        typeof(int),
                        typeof(uint),
                        typeof(long),
                        typeof(ulong),
                        typeof(float),
                        typeof(double),
                    })
                )
            },
            {
                typeof(LRPCString),
                new(
                    obj => new LRPCString((string)obj),
                    null,
                    new(new [] {
                        typeof(string)
                    })
                )
            },
            {
                typeof(LRPCArray),
                new(
                    obj => {
                        if (obj == null) return new LRPCArray();
                        var type = obj.GetType();
                        if (type.IsArray) return new LRPCArray(objects:
                            LRPCArray.ConvertArray(array: (object[])obj));
                        else {
                            if (obj is IEnumerable e)
                                return new LRPCArray(e);
                            else if (type == typeof(Array))
                                return new LRPCArray((Array)obj);
                            else throw new ArgumentException("개체의 배열 특성을 찾지 못했습니다.");
                        }
                    },
                    type => type.IsArray || type == typeof(Array) || typeof(IEnumerable).IsAssignableFrom(type),
                    null
                )
            }
        };
    }
}
