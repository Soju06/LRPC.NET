using System.Runtime.Serialization;

namespace LRPC.NET.Object {
    /// <summary>
    /// LRPC 내장 개체
    /// </summary>
    public sealed class LRPCNumber : LRPCObject, IConvertible {
        string typeName;
        object @object;

        public override string TypeName => typeName;

        public LRPCNumber(sbyte @int) {
            @object = @int;
            typeName = LRPCObjectTypes.Int8;
        }

        public LRPCNumber(Int16 @int) {
            @object = @int;
            typeName = LRPCObjectTypes.Int16;
        }
        
        public LRPCNumber(Int32 @int) {
            @object = @int;
            typeName = LRPCObjectTypes.Int32;
        }

        public LRPCNumber(Int64 @int) {
            @object = @int;
            typeName = LRPCObjectTypes.Int64;
        }
        
        public LRPCNumber(byte @int) {
            @object = @int;
            typeName = LRPCObjectTypes.UInt8;
        }
        
        public LRPCNumber(UInt16 @int) {
            @object = @int;
            typeName = LRPCObjectTypes.UInt16;
        }
        
        public LRPCNumber(UInt32 @int) {
            @object = @int;
            typeName = LRPCObjectTypes.UInt32;
        }
        
        public LRPCNumber(UInt64 @int) {
            @object = @int;
            typeName = LRPCObjectTypes.UInt64;
        }

        public LRPCNumber(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

        protected override void Serialize(SerializationInfo info, StreamingContext context) {
            info.AddValue("_type", typeName, typeof(string));
            info.AddValue("value", @object, Types[typeName]);
        }

        protected override void Deserialize(SerializationInfo info, StreamingContext context) {
            typeName = info.GetValue<string>("_type");
            if (Types.TryGetValue(typeName, out var type)) {
                @object = info.GetValue("value", type);
            } else throw new FormatException($"지원하지 않는 포멧입니다. {typeName}");
        }

        /// <summary>
        /// byte 여부
        /// </summary>
        public bool IsByte => LRPCObjectTypes.UInt8 == typeName;
        /// <summary>
        /// sbyte 여부
        /// </summary>
        public bool IsSByte => LRPCObjectTypes.Int8 == typeName;

        /// <summary>
        /// short 여부
        /// </summary>
        public bool IsShort => LRPCObjectTypes.Int16 == typeName;
        /// <summary>
        /// ushort 여부
        /// </summary>
        public bool IsUShort => LRPCObjectTypes.UInt16 == typeName;
        /// <summary>
        /// int 여부
        /// </summary>
        public bool IsInt => LRPCObjectTypes.Int32 == typeName;
        /// <summary>
        /// uint 여부
        /// </summary>
        public bool IsUInt => LRPCObjectTypes.UInt32 == typeName;
        /// <summary>
        /// long 여부
        /// </summary>
        public bool IsLong => LRPCObjectTypes.Int64 == typeName;
        /// <summary>
        /// ulong 여부
        /// </summary>
        public bool IsULong => LRPCObjectTypes.UInt64 == typeName;

        /// <summary>
        /// single 여부
        /// </summary>
        public bool IsSingle => LRPCObjectTypes.Single == typeName;

        /// <summary>
        /// double 여부
        /// </summary>
        public bool IsDouble => LRPCObjectTypes.Double == typeName;

        /// <summary>
        /// 타입
        /// </summary>
        public string Type => typeName;
        /// <summary>
        /// 값
        /// </summary>
        public object Value => @object;

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
    }
}
