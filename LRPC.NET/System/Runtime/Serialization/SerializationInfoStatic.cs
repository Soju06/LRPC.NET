namespace System.Runtime.Serialization {
    public static class SerializationInfoStatic {
        public static T GetValue<T>(this SerializationInfo info, string name) =>
            (T)info.GetValue(name, typeof(T));
    }
}
