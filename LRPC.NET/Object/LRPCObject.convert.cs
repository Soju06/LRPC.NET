namespace LRPC.NET.Object {
    partial class LRPCObject {
        delegate LRPCObject? ConvertFunc(object? obj);
        public delegate bool IsConvertibleFunc(Type type);

        /// <summary>
        /// 다음의 개체를 빌트-인 개체로 변환할 수 있는지 확인합니다.
        /// </summary>
        /// <param name="obj">개체</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsConvertible(Type type) { 
            if(type == null) throw new ArgumentNullException("type");
            // LRPCObject 타입이면 프리패스.
            if (type == typeof(LRPCObject)) return true;

            foreach (var item in ConvertibleTypes) {
                (var _, var _, var iscble, var hashset) = item.Value;
                if (IsConvertible(type, iscble, hashset)) return true;
            } return false;
        }

        /// <summary>
        /// 오브젝트를 LRPC 오브젝트로 변환합니다
        /// </summary>
        /// <param name="obj">개체</param>
        /// <param name="type">개체 타입</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static LRPCObject? Convert(object? obj, Type? type) {
            // 타입이 없을때 오브젝트의 타입을 박음. 오브젝트도 null일땐 터짐
            if (type == null) type = obj?.GetType() ?? throw new ArgumentNullException(nameof(obj),
                "개체가 null 일 때 명시된 타입이 없으므로 개체의 타입을 유추할 수 없습니다.");
            // LRPCObject 타입이면 프리패스.
            if (type == typeof(LRPCObject)) return obj as LRPCObject;

            if (obj is JsonElement element) {
                if (!element.TryGetProperty("_type", out var typeName)) goto NOT_ABLE;
                if ((type = GetObjectOriginalType(typeName.GetString() ?? "")) == null) goto NOT_ABLE;
            }
            foreach (var item in ConvertibleTypes) {
                (var _, var convet, var iscble, var hashset) = item.Value;

                // 변환가능한 타입 확인
                if (!IsConvertible(type, iscble, hashset)) continue;
                return convet.Invoke(obj);
            }

            NOT_ABLE:
            throw new ArgumentException(nameof(obj),
                "개체가 변환가능한 타입에 속해있지 않습니다.");
        }

        /// <summary>
        /// 타입 이름으로 개체 타입을 가져옵니다.
        ///     리턴값: 유효하지 않은 타입 이름 일 때 null을 반환합니다.
        /// </summary>
        /// <param name="typeName">타입 이름</param>
        public static Type? GetObjectType(string typeName) {
            foreach (var item in ConvertibleTypes) {
                var info = item.Value.Item1.Where(x => x.Item1 == typeName).FirstOrDefault();
                if (info == default) continue;
                return info.Item2;
            }
            return null;
        }
        
        /// <summary>
        /// 타입 이름으로 개체 원본 타입을 가져옵니다.
        ///     리턴값: 유효하지 않은 타입 이름 일 때 null을 반환합니다.
        /// </summary>
        /// <param name="typeName">타입 이름</param>
        public static Type? GetObjectOriginalType(string typeName) {
            foreach (var item in ConvertibleTypes) {
                var value = item.Value;
                var info = value.Item1.Where(x => x.Item1 == typeName).FirstOrDefault();
                if (info == default) continue;
                return info.Item2;
            }
            return null;
        }

        static bool IsConvertible(Type type, IsConvertibleFunc? iscble, HashSet<Type>? hashset) {
            // 빌트인 타입에서 지원하는 해시셋에 존재하면 true
            bool chk_f;
            if (hashset != null) {
                if (hashset.Contains(type)) {
                    if (iscble != null) {
                        chk_f = true;
                        goto CUSTOM_FUNC_CHECK;
                    }
                    else return true;
                } else return false;
            } else chk_f = true;
            // 지원여부 확인 전용함수
            CUSTOM_FUNC_CHECK:
            if (chk_f && iscble != null && iscble.Invoke(type))
                return true;
            return false;
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

        static readonly Dictionary<Type, ((string, Type)[], ConvertFunc, IsConvertibleFunc?, HashSet<Type>?)> ConvertibleTypes = new() {
            {
                typeof(LRPCNumber),
                new(
                    new (string, Type)[] {
                        new(LRPCObjectTypes.Int8, typeof(sbyte)),
                        new(LRPCObjectTypes.Int16, typeof(Int16)),
                        new(LRPCObjectTypes.Int32, typeof(Int32)),
                        new(LRPCObjectTypes.Int64, typeof(Int64)),
                        new(LRPCObjectTypes.UInt8, typeof(byte)),
                        new(LRPCObjectTypes.UInt16, typeof(UInt16)),
                        new(LRPCObjectTypes.UInt32, typeof(UInt32)),
                        new(LRPCObjectTypes.UInt64, typeof(UInt64)),
                        new(LRPCObjectTypes.Single, typeof(Single)),
                        new(LRPCObjectTypes.Double, typeof(Double))
                    },
                    obj => obj != null ? (obj is JsonElement e ? new LRPCNumber(e) : new LRPCNumber(obj, LRPCNumber.GetDisplayType(obj.GetType()))) : null,
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
                    new (string, Type)[] {
                        new(LRPCObjectTypes.String, typeof(string))
                    },
                    obj => obj is JsonElement e ? new LRPCString(e) : new LRPCString((string)(obj ?? "")),
                    null,
                    new(new [] {
                        typeof(string)
                    })
                )
            },
            {
                typeof(LRPCArray),
                new(
                   new (string, Type)[] {
                        new(LRPCObjectTypes.Array, typeof(Array))
                    },
                    obj => {
                        if (obj == null) return new LRPCArray();
                        var type = obj.GetType();

                        if (obj is JsonElement element)
                            return JsonSerializer.Deserialize<LRPCArray>(element.GetRawText());
                        if (type.IsArray) return new LRPCArray(objects:
                            LRPCArray.ConvertArray((Array)obj));
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
