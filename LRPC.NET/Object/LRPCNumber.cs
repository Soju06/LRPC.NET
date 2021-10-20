using System.Text.Json.Serialization;

namespace LRPC.NET.Object {
    /// <summary>
    /// LRPC 내장 개체
    /// </summary>
    public sealed class LRPCNumber : LRPCObject, IConvertible {
        object @object;

        public LRPCNumber(sbyte @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.Int8;
        }

        public LRPCNumber(Int16 @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.Int16;
        }

        public LRPCNumber(Int32 @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.Int32;
        }

        public LRPCNumber(Int64 @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.Int64;
        }

        public LRPCNumber(byte @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.UInt8;
        }

        public LRPCNumber(UInt16 @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.UInt16;
        }

        public LRPCNumber(UInt32 @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.UInt32;
        }

        public LRPCNumber(UInt64 @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.UInt64;
        }
        
        public LRPCNumber(Single @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.Single;
        }

        public LRPCNumber(Double @int) {
            @object = @int;
            TypeName = LRPCObjectTypes.Double;
        }

        [JsonConstructor]
        public LRPCNumber(object value, string typeName) {
            @object = value;
            TypeName = typeName;
        }

        /// <summary>
        /// byte 여부
        /// </summary>
        [JsonIgnore]
        public bool IsByte => LRPCObjectTypes.UInt8 == TypeName;
        /// <summary>
        /// sbyte 여부
        /// </summary>
        [JsonIgnore]
        public bool IsSByte => LRPCObjectTypes.Int8 == TypeName;

        /// <summary>
        /// short 여부
        /// </summary>
        [JsonIgnore]
        public bool IsShort => LRPCObjectTypes.Int16 == TypeName;
        /// <summary>
        /// ushort 여부
        /// </summary>
        [JsonIgnore]
        public bool IsUShort => LRPCObjectTypes.UInt16 == TypeName;
        /// <summary>
        /// int 여부
        /// </summary>
        [JsonIgnore]
        public bool IsInt => LRPCObjectTypes.Int32 == TypeName;
        /// <summary>
        /// uint 여부
        /// </summary>
        [JsonIgnore]
        public bool IsUInt => LRPCObjectTypes.UInt32 == TypeName;
        /// <summary>
        /// long 여부
        /// </summary>
        [JsonIgnore]
        public bool IsLong => LRPCObjectTypes.Int64 == TypeName;
        /// <summary>
        /// ulong 여부
        /// </summary>
        [JsonIgnore]
        public bool IsULong => LRPCObjectTypes.UInt64 == TypeName;

        /// <summary>
        /// single 여부
        /// </summary>
        [JsonIgnore]
        public bool IsSingle => LRPCObjectTypes.Single == TypeName;

        /// <summary>
        /// double 여부
        /// </summary>
        [JsonIgnore]
        public bool IsDouble => LRPCObjectTypes.Double == TypeName;

        /// <summary>
        /// 타입
        /// </summary>
        [JsonIgnore]
        public string Type => TypeName;
        /// <summary>
        /// 값
        /// </summary>
        [JsonPropertyName("value")]
        public object Value { get => @object; set => SetValue(value); }

        void SetValue(object value) {
            if (value != null) {
                if (string.IsNullOrWhiteSpace(TypeName)) {
                    TypeName = DefaultTypeName;
                    @object = Convert.ChangeType(value, DefaultType);
                } else {
                    if (Types.TryGetValue(TypeName, out var type))
                        @object = Convert.ChangeType(value, type);
                    else throw new ProtocolViolationException("명시된 타입과 값의 타입이 일치하지 않습니다.");
                }
            } else @object = DefaultValue;
        }

        /// <summary>
        /// byte
        /// </summary>
        public byte Byte() => (byte)@object;
        /// <summary>
        /// sbyte
        /// </summary>
        public sbyte SByte() => (sbyte)@object;
        /// <summary>
        /// short
        /// </summary>
        public short Short() => (short)@object;
        /// <summary>
        /// ushort
        /// </summary>
        public ushort UShort() => (ushort)@object;
        /// <summary>
        /// int
        /// </summary>
        public int Int() => (int)@object;
        /// <summary>
        /// uint
        /// </summary>
        public uint UInt() => (uint)@object;
        /// <summary>
        /// long
        /// </summary>
        public long Long() => (long)@object;
        /// <summary>
        /// ulong
        /// </summary>
        public ulong ULong() => (ulong)@object;
        /// <summary>
        /// single
        /// </summary>
        public Single Single() => (Single)@object;
        /// <summary>
        /// double
        /// </summary>
        public Double Double() => (Double)@object;

        public TypeCode GetTypeCode() {
            if (IsByte) return TypeCode.Byte;
            else if (IsSByte) return TypeCode.SByte;
            else if (IsShort) return TypeCode.Int16;
            else if (IsUShort) return TypeCode.UInt16;
            else if (IsInt) return TypeCode.Int32;
            else if (IsUInt) return TypeCode.UInt32;
            else if (IsLong) return TypeCode.Int64;
            else if (IsULong) return TypeCode.UInt64;
            else if (IsSingle) return TypeCode.Single;
            else if (IsDouble) return TypeCode.Double;
            else throw new NotSupportedException();
        }

        public bool ToBoolean(IFormatProvider provider) =>
            throw new NotSupportedException();
        public byte ToByte(IFormatProvider provider) => Byte();
        public char ToChar(IFormatProvider provider) =>
            throw new NotSupportedException();
        public DateTime ToDateTime(IFormatProvider provider) =>
            throw new NotSupportedException();
        public decimal ToDecimal(IFormatProvider provider) =>
            throw new NotSupportedException();
        public double ToDouble(IFormatProvider provider) => 
            Double();
        public short ToInt16(IFormatProvider provider) => Short();
        public int ToInt32(IFormatProvider provider) => Int();
        public long ToInt64(IFormatProvider provider) => Long();
        public sbyte ToSByte(IFormatProvider provider) => SByte();
        public float ToSingle(IFormatProvider provider) => Single();
        public string ToString(IFormatProvider provider) => @object.ToString();
        public object ToType(Type conversionType, IFormatProvider provider) =>
            Convert.ChangeType(@object, conversionType);
        public ushort ToUInt16(IFormatProvider provider) => UShort();
        public uint ToUInt32(IFormatProvider provider) => UInt();
        public ulong ToUInt64(IFormatProvider provider) => ULong();

        public override string ToString() => @object?.ToString() ?? "";

        public static implicit operator sbyte(LRPCNumber number) => number.SByte();
        public static implicit operator byte(LRPCNumber number) => number.Byte();
        public static implicit operator Int16(LRPCNumber number) => number.Short();
        public static implicit operator UInt16(LRPCNumber number) => number.UShort();
        public static implicit operator Int32(LRPCNumber number) => number.Int();
        public static implicit operator UInt32(LRPCNumber number) => number.UInt();
        public static implicit operator Int64(LRPCNumber number) => number.Long();
        public static implicit operator UInt64(LRPCNumber number) => number.ULong();
        public static implicit operator Single(LRPCNumber number) => number.Single();
        public static implicit operator Double(LRPCNumber number) => number.Double();

        public static implicit operator LRPCNumber(sbyte number) => new(number);
        public static implicit operator LRPCNumber(byte number) => new(number);
        public static implicit operator LRPCNumber(Int16 number) => new(number);
        public static implicit operator LRPCNumber(UInt16 number) => new(number);
        public static implicit operator LRPCNumber(Int32 number) => new(number);
        public static implicit operator LRPCNumber(UInt32 number) => new(number);
        public static implicit operator LRPCNumber(Int64 number) => new(number);
        public static implicit operator LRPCNumber(UInt64 number) => new(number);
        public static implicit operator LRPCNumber(Single number) => new(number);
        public static implicit operator LRPCNumber(Double number) => new(number);

        /// <summary>
        /// 표기 타입을 가져옵니다.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDisplayType(Type type) =>
            Types.FirstOrDefault(x => x.Value == type).Key;

        static readonly Dictionary<string, Type> Types = new() { 
            { LRPCObjectTypes.Int8, typeof(sbyte) },
            { LRPCObjectTypes.Int16, typeof(Int16) },
            { LRPCObjectTypes.Int32, typeof(Int32) },
            { LRPCObjectTypes.Int64, typeof(Int64) },
            { LRPCObjectTypes.UInt8, typeof(byte) },
            { LRPCObjectTypes.UInt16, typeof(UInt16) },
            { LRPCObjectTypes.UInt32, typeof(UInt32) },
            { LRPCObjectTypes.UInt64, typeof(UInt64) },
            { LRPCObjectTypes.Single, typeof(Single) },
            { LRPCObjectTypes.Double, typeof(Double) },
        };

        static readonly Type DefaultType = typeof(int);
        static readonly string DefaultTypeName = LRPCObjectTypes.Int32;
        static readonly object DefaultValue = 0;
    }
}
