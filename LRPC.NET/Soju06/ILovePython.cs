/* ========= Soju06 I LOVE PYTHON =========
 * RUNTIME: .NET Framework 4.7.2 | .NET Core 5.0 || Csharp 9.0
 * NAMESPACE: _
 * VERSION: 40
 * LICENSE: MIT
 * Copyright by Soju06
 * ========= Soju06 I LOVE PYTHON ========= */

// .NET preview

global using System;
global using System.IO;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Collections;
global using System.Collections.Generic;

// .NET preview

#pragma warning disable IDE0079 // 불필요한 비표시 오류(Suppression) 제거
#pragma warning disable IDE0056 // 인덱스 연산자 사용
#pragma warning disable CS8632 // nullable 참조 형식에 대한 주석은 코드에서 '#nullable' 주석 컨텍스트 내에만 사용되어야 합니다.
#pragma warning disable IDE1006 // 명명 스타일

#pragma warning disable CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
#pragma warning restore CS0105 // using 지시문을 이전에 이 네임스페이스에서 사용했습니다.

internal static class ILovePython {
    public const long _I_LOVE_PYTHON_VERSION_ = 40;

    #region Enumerable

    public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate) {
        var i = 0;
        foreach (var item in source)
            if (!predicate.Invoke(item, i++)) return false;
        return true;
    }

    public static TItem[] Copy<TItem>(this TItem[] s) {
        var t = new TItem[s.Length];
        s.CopyTo(t, 0);
        return t;
    }

    public static TList CastList<TItem, TList>(this IEnumerable<TItem> s) 
            where TList : IList<TItem>, new() {
        var list = new TList();
        foreach (var item in s) list.Add(item); 
        return list;
    }

    public static IEnumerable<T> SelectItems<T>(this IEnumerable<T> list, Func<T, bool> handle) =>
        Enumerable.Where(list, handle);

    public static IEnumerable<TRequest> CastAll<T, TRequest>(this IEnumerable<T> list, Func<T, TRequest> handle) =>
        Enumerable.Select(list, handle);

    public static T Select<T>(this IEnumerable<T> list, Func<T, bool> handle, T defaultValue = default) {
        foreach (var item in list) if (handle?.Invoke(item) == true) return item;
        return defaultValue;
    }

    public static bool TryGetValue<T>(this IList<T> list, int index, out T item, T defaultValue = default) {
        if (index < 0 || list == null || index >= list.Count) item = defaultValue;
        else { item = list[index]; return true; }
        return false;
    }

    public static T TryGet<T>(this IList<T> list, int index, T defaultValue = default) =>
        index < 0 || list == null || index >= list.Count ? defaultValue : list[index];

    public static T? TryGetStruct<T>(this IList<T> list, int index) where T : struct =>
        index < 0 || list == null || index >= list.Count ? null : list[index];

    public static bool Contains<T>(this T? e, params T[] es) => e != null && Enumerable.Contains(es, e);

    public static void Foreach<T>(this IEnumerable<T> ts, Action<T> func) {
        foreach (var item in ts) 
            func.Invoke(item);
    }
    
    public static void Foreach<T>(this IEnumerable<T> ts, Action<T, int> func) {
        int i = 0;
        foreach (var item in ts) 
            func.Invoke(item, i++);
    }

    #endregion

    #region Array

    public static int IndexOf<T>(this T[] ts, T obj) => Array.IndexOf(ts, obj);
    public static int IndexOf(this Array ts, object obj) => Array.IndexOf(ts, obj);

    #endregion

    #region List

    public static TItem add<TItem>(this IList<TItem> items, TItem item) {
        items.Add(item);
        return item;
    }
    
    public static IEnumerable<TItem> addRange<TItem>(this List<TItem> items, IEnumerable<TItem> aitems) {
        items.AddRange(aitems);
        return aitems;
    }

    public static IList<TItem> ReturnAdd<TItem>(this IList<TItem> items, TItem item) {
        items.Add(item);
        return items;
    }
    
    public static List<TItem> ReturnAddRange<TItem>(this List<TItem> items, IEnumerable<TItem> aitems) {
        items.AddRange(aitems);
        return items;
    }


    #endregion

    #region String

    public static bool IsNullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);
    public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

    public static byte[] Encode(this string s, Encoding encoding = null) =>
        (encoding ?? Encoding.UTF8).GetBytes(s);
    public static string Decode(this byte[] s, Encoding encoding = null) =>
        (encoding ?? Encoding.UTF8).GetString(s);
    public static string Decode(this byte[] s, int index, int count, Encoding encoding = null) =>
        (encoding ?? Encoding.UTF8).GetString(s, index, count);
    public static byte[] DecodeBase64(this string s) =>
        System.Convert.FromBase64String(s);
    public static string EncodeBase64(this string s, Encoding encoding = null) =>
        System.Convert.ToBase64String(s.Encode(encoding ?? Encoding.UTF8));
    public static string EncodeBase64(this byte[] s) =>
        System.Convert.ToBase64String(s);

    public static string Join(this IEnumerable<string> s, string sep) => s == null ? "" : string.Join(sep, s);

    public static Uri JoinUri(this string s, params string[] uris) {
        Uri r = new(s);
        for (int i = 0; i < uris.Length; i++)
            r = new Uri(r, uris[i]);
        return r;
    }
    
    public static Uri Uri(this string s) => new(s);

    public static bool TryUri(this string s, out Uri uri) {
        try {
            uri = new(s);
            return true;
        } catch {
            uri = null;
            return false;
        }
    }

    public static string JoinDir(this string s, params string[] ss) {
        var a = new string[ss.Length + 1];
        for (int i = 1; i < a.Length; i++)
            a[i] = ss[i - 1];
        a[0] = s;
        return Path.Combine(a);
    }

    public static string Format(this string s, params object[] parms) =>
        string.Format(s, args: parms);

    public static string GetHex(this Guid g, string joinSep = "") =>
        BitConverter.ToString(g.ToByteArray()).ToLower().Replace("-", joinSep);
    
    public static string UriEscape(this string s) => HttpUtility.UrlEncodeUnicode(s);
    
    public static void Replace(this IList<string> q, string n, string w) {
        for (int i = 0; i < q.Count; i++)
            q[i] = q[i].Replace(n, w);
    }
    
    public static void Replace(this IList<string> q, char n, char w) {
        for (int i = 0; i < q.Count; i++)
            q[i] = q[i].Replace(n, w);
    }
    
    public static string Format(this string q, string[][] s) {
        for (int i = 0; i < s.Length; i++) {
            var w = s[i];
            var e = w[w.Length - 1];
            for (int l = 0; l < w.Length - 1; l++)
                q = q.Replace(w[l], e);
        } return q;
    }

    public static string Format(this string q, params string[] s) => string.Format(q, s);

    public static string ToHex(this byte b, bool lower = true) {
        var r = BitConverter.ToString(new byte[] { b });
        if (lower) r = r.ToLower();
        return r;
    }

    public static string ToHex(this byte[] b, bool lower = true, string splitStr = null) {
        var r = BitConverter.ToString(b);
        if (lower) r = r.ToLower();
        if(splitStr != "-") 
            r = r.Replace("-", splitStr ?? "");
        return r;
    }

    public static string Join(this string[] s) {
        var w = "";
        var l = s.Length;
        for (int i = 0; i < l; i++) w += s[i];
        return w;
    }

    public static string Append(this string q, string s) => q + s;

    public static string Append(this string q, params string[] s) => q + Join(s);

    #region string convert type

    public static bool ToBoolean(this string s, bool defaultValue = false) => bool.TryParse(s, out var e) ? e : defaultValue;
    public static bool ToBoolean(this bool defaultValue, string s) => ToBoolean(s, defaultValue);
    public static byte ToByte(this string s, byte defaultValue = 0) => byte.TryParse(s, out var e) ? e : defaultValue;
    public static byte ToByte(this byte defaultValue, string s) => ToByte(s, defaultValue);
    public static sbyte ToSByte(this string s, sbyte defaultValue = 0) => sbyte.TryParse(s, out var e) ? e : defaultValue;
    public static sbyte ToSByte(this sbyte defaultValue, string s) => ToSByte(s, defaultValue);
    public static short ToInt16(this string s, short defaultValue = 0) => short.TryParse(s, out var e) ? e : defaultValue;
    public static short ToInt16(this short defaultValue, string s) => ToInt16(s, defaultValue);
    public static int ToInt32(this string s, int defaultValue = 0) => int.TryParse(s, out var e) ? e : defaultValue;
    public static int ToInt32(this int defaultValue, string s) => ToInt32(s, defaultValue);
    public static uint ToUInt32(this string s, uint defaultValue = 0) => uint.TryParse(s, out var e) ? e : defaultValue;
    public static uint ToUInt32(this uint defaultValue, string s) => ToUInt32(s, defaultValue);
    public static long ToInt64(this string s, long defaultValue = 0) => long.TryParse(s, out var e) ? e : defaultValue;
    public static long ToInt64(this long defaultValue, string s) => ToInt64(s, defaultValue);
    public static ulong ToUInt64(this string s, ulong defaultValue = 0) => ulong.TryParse(s, out var e) ? e : defaultValue;
    public static ulong ToUInt64(this ulong defaultValue, string s) => ToUInt64(s, defaultValue);
    public static bool ToTryDateTime(this string s, out DateTime date) => DateTime.TryParse(s, out date);
    public static bool ToTryTimeSpan(this string s, out TimeSpan time) => TimeSpan.TryParse(s, out time);

    public static TRes Convert<TRes, TReq>(this TReq obj) =>
        (TRes)System.Convert.ChangeType(obj, typeof(TRes));

    #endregion

    #endregion

    #region Uri

    public static Uri Join(this Uri uri, params Uri[] uris) {
        var r = uri;
        for (int i = 0; i < uris.Length; i++)
            r = new Uri(r, uris[i]);
        return r;
    }
        
    public static Uri Join(this Uri uri, params string[] uris) {
        var r = uri;
        for (int i = 0; i < uris.Length; i++)
            r = new Uri(r, uris[i]);
        return r;
    }

    #endregion

    #region Number

    public static ushort ToUInt16(this short i) =>
        BitConverter.ToUInt16(BitConverter.GetBytes(i), 0);

    public static uint ToUInt32(this int i) =>
        BitConverter.ToUInt32(BitConverter.GetBytes(i), 0);

    public static ulong ToUInt64(this long i) =>
        BitConverter.ToUInt64(BitConverter.GetBytes(i), 0);

    public static short ToInt16(this uint i) =>
        BitConverter.ToInt16(BitConverter.GetBytes(i), 0);

    public static int ToInt32(this uint i) =>
        BitConverter.ToInt32(BitConverter.GetBytes(i), 0);

    public static long ToInt64(this uint i) =>
        BitConverter.ToInt64(BitConverter.GetBytes(i), 0);

    public static byte[] Bytes(this short number) => BitConverter.GetBytes(number);
    public static byte[] Bytes(this ushort number) => BitConverter.GetBytes(number);
    public static byte[] Bytes(this int number) => BitConverter.GetBytes(number);
    public static byte[] Bytes(this uint number) => BitConverter.GetBytes(number);
    public static byte[] Bytes(this long number) => BitConverter.GetBytes(number);
    public static byte[] Bytes(this ulong number) => BitConverter.GetBytes(number);
    public static byte[] Bytes(this float number) => BitConverter.GetBytes(number);
    public static byte[] Bytes(this double number) => BitConverter.GetBytes(number);

    #endregion

    #region IntPtr

    public static bool IsZero(this IntPtr ptr) => ptr == IntPtr.Zero;
    public static bool IsZero(this UIntPtr ptr) => ptr == UIntPtr.Zero;

    #endregion

    #region Date, Time

    public static long ToUnixTimestamp(this DateTime time) => ((DateTimeOffset)time).ToUnixTimeSeconds();
    public static DateTime UnixTimestampToDateTime(this long time) => new DateTime(1970, 1, 1).AddSeconds(time);

    #endregion

    #region Enum

    public static TEnum ToEnum<TEnum>(this string s, bool ignoreCase = false) where TEnum : Enum => 
        (TEnum)Enum.Parse(typeof(TEnum), s, ignoreCase);

    public static bool ToTryEnum<TEnum>(this string s, out TEnum @enum,
        bool ignoreCase = false) where TEnum : struct, Enum =>
        Enum.TryParse(s, ignoreCase, out @enum);

    #endregion

    #region Type

    public static T New<T>(this Type type) =>
        (T)Activator.CreateInstance(type);

    public static T New<T>(this Type type, params object[] obs) =>
        (T)Activator.CreateInstance(type, obs);

    #endregion

    #region IDisposable

    public static TResult Using<TDisposbleObject, TResult>(this TDisposbleObject obj, 
        Func<TDisposbleObject, TResult> func) where TDisposbleObject : IDisposable {
        using var t = obj; return func.Invoke(t);
    }
    
    public static void Using<TDisposbleObject>(this TDisposbleObject obj, 
        Action<TDisposbleObject> func) where TDisposbleObject : IDisposable {
        using var t = obj; func.Invoke(t);
    }

    public static void Dispose(this IEnumerable<IDisposable> disposables) {
        foreach (var item in disposables) item?.Dispose();
    }

    public static void Dispose(this IDisposable[] disposables) {
        for (int i = 0; i < disposables.Length; i++) disposables[i]?.Dispose();
    }


    #endregion

    #region Null

    public static bool IsNull<T>(this T obj) where T : class => obj is null;

    public static int ClearNull<T>(this IList<T?> ts) {
        if (ts.IsReadOnly) throw new NotSupportedException("This list is read-only.");
        var e = 0;
        for (int i = 0; i < ts.Count; i++)
            if (ts[i] == null) {
                ts.RemoveAt(i--);
                e++;
            }
        return e;
    }

    public static IEnumerable<T> NullClear<T>(this IEnumerable<T?> ts) where T : struct =>
        ts.Where(t => t.HasValue).Select(x => x.Value);

    public static IEnumerable<T> NullClear<T>(this IEnumerable<T?> ts) where T : class =>
        ts.Where(t => t != null);

    public static TResult TryRun<TResult, TObject>(this TObject? obj, Func<TObject, TResult> func, TResult defaultValue = default)
        where TResult : class where TObject : class => obj != null ? func.Invoke(obj) : defaultValue;

    public static TResult TryRun<TResult, TObject>(this TObject? obj, Func<TObject, TResult> func, TResult defaultValue = default)
        where TResult : struct where TObject : struct => obj.HasValue ? func.Invoke(obj.Value) : defaultValue;

    public static void TryRun<TObject>(this TObject? obj, Action<TObject> func)
        where TObject : struct { if (obj != null) func?.Invoke(obj.Value); }
    
    public static void TryRun<TObject>(this TObject? obj, Action<TObject> func)
        where TObject : class { if (obj != null) func?.Invoke(obj); }

    public static Task TryRun(this Task obj) => obj ?? Task.CompletedTask;
    public static Task<T> TryRun<T>(this Task<T> obj, T defaultValue = default) => obj ?? Task.FromResult(defaultValue);

    #endregion

    #region Task

    public static void Run(this Task task) =>
        Task.Run(async () => await task).ConfigureAwait(false);

    public static void Run<TRes>(this Task<TRes> task) =>
        Task.Run(async () => await task);

    public static TRes RunWait<TRes>(this Task<TRes> task) {
        var t = Task.Run(async () => await task);
        t.Wait();
        return t.Result;
    }
    
    public static void RunWait(this Task task) =>
        Task.Run(async () => await task).Wait();

    #pragma warning disable CA1416 // 플랫폼 호환성 유효성 검사
    /// <summary>
    /// For WebAssembly it is not compatible.
    /// </summary>
    public static void WaitAll(this Task[] tasks) {
        if (tasks == null || tasks.Length < 1) return;
        Task.WaitAll(tasks);
    }
    /// <summary>
    /// For WebAssembly it is not compatible.
    /// </summary>
    public static Task Wait(this Task[] tasks) =>
        tasks == null || tasks.Length < 1 ? Task.CompletedTask : Task.Run(() => Task.WaitAll(tasks));
    #pragma warning restore CA1416 // 플랫폼 호환성 유효성 검사

    #endregion

    #region Object

    public static Dictionary<string, object> ToDictionary(this object obj) {
        var dictionary = new Dictionary<string, object>();
        foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(obj))
            dictionary.Add(propertyDescriptor.Name, propertyDescriptor.GetValue(obj));
        return dictionary;
    }
    
    public static IEnumerable<TOutput> ConvetAll<TInput, TOutput>(this IEnumerable<TInput> ts, Converter<TInput, TOutput> c) {
        foreach (var item in ts) yield return c.Invoke(item);
    }

    #endregion

    #region Dynamic

    public static dynamic ToExpandoObject(this object obj) {
        IDictionary<string, object> expando = new ExpandoObject();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj.GetType()))
            expando.Add(property.Name, property.GetValue(obj));
        return expando as ExpandoObject;
    }

    #endregion

    public static T Return<T>(this T t, Action<T> func) {
        func.Invoke(t);
        return t;
    }
}

#region Try

internal struct Try {
    Try(Action action, Action<Exception> catchFunc = null, Action<bool> finallyFunc = null) {
        Action = action;
        CatchFunc = catchFunc;
        FinallyFunc = finallyFunc;
    }

    readonly Action Action;
    Action<Exception> CatchFunc;
    Action<bool> FinallyFunc;

    public Try Catch(Action<Exception> catchFunc = null) {
        CatchFunc = catchFunc; return this;
    }
    
    public void CatchRun(Action<Exception> catchFunc = null) {
        CatchFunc = catchFunc; Start();
    }
    
    public Try Finally(Action<bool> finallyFunc = null) {
        FinallyFunc = finallyFunc; return this;
    }
    
    public void FinallyRun(Action<bool> finallyFunc = null) {
        FinallyFunc = finallyFunc; Start();
    }

    public void Start() {
        var i = false;
        try {
            Action?.Invoke();
            i = true;
        } catch (Exception ex) {
            CatchFunc?.Invoke(ex);
        } finally {
            FinallyFunc?.Invoke(i);
        }
    }

    public static Try Run(Action action, Action<Exception> catchFunc = null, Action<bool> finallyFunc = null) => 
        new(action, catchFunc, finallyFunc);

    public static Try<TResult> Run<TResult>(Func<TResult> action,
        Func<Exception, TResult> catchFunc = null, Action<bool> finallyFunc = null) =>
        new(action, catchFunc, finallyFunc);

    public static TResult EndRun<TResult>(Func<TResult> action,
        Func<Exception, TResult> catchFunc = null, Action<bool> finallyFunc = null) {
        var result = default(TResult);
        var isSuccess = false;
        try {
            result = action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {  
            result = catchFunc != null ? catchFunc.Invoke(ex) : default;
        } finally {
            finallyFunc?.Invoke(isSuccess);
        } return result;
    }
    
    public static async Task<TResult> EndRunAsync<TResult>(Func<Task<TResult>> action, 
        Func<Exception, TResult> catchFunc = null, Action<bool> finallyFunc = null) {
        var result = default(TResult);
        var isSuccess = false;
        try {
            result = await action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {
            result = catchFunc != null ? catchFunc.Invoke(ex) : default;
        } finally {
            finallyFunc?.Invoke(isSuccess);
        } return result;
    }
    
    public static async Task EndRunAsync(Func<Task> action, 
        Action<Exception> catchFunc = null, Action<bool> finallyFunc = null) {
        var isSuccess = false;
        try {
            await action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {
            if (catchFunc != null) catchFunc.Invoke(ex);
        } finally {
            finallyFunc?.Invoke(isSuccess);
        }
    }
    
    public static async Task EndRunAsync(Func<Task> action, 
        Func<Exception, Task> catchFunc = null, Action<bool> finallyFunc = null) {
        var isSuccess = false;
        try {
            await action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {
            if (catchFunc != null) await catchFunc.Invoke(ex);
        } finally {
            finallyFunc?.Invoke(isSuccess);
        }
    }

    public static void EndRun(Action action) {
        try {
            action.Invoke();
        } finally { }
    }
}

internal struct Try<TResult> {
    internal Try(Func<TResult> action, Func<Exception, TResult> catchFunc = null, Action<bool> finallyFunc = null) {
        Action = action;
        CatchFunc = catchFunc;
        FinallyFunc = finallyFunc;
    }

    readonly Func<TResult> Action;
    Func<Exception, TResult> CatchFunc;
    Action<bool> FinallyFunc;

    public Try<TResult> Catch(Func<Exception, TResult> catchFunc = null) {
        CatchFunc = catchFunc; return this;
    }
    
    public TResult CatchRun(Func<Exception, TResult> catchFunc = null) {
        CatchFunc = catchFunc; return Start();
    }
    
    public Try<TResult> Finally(Action<bool> finallyFunc = null) {
        FinallyFunc = finallyFunc; return this;
    }
    
    public TResult FinallyRun(Action<bool> finallyFunc = null) {
        FinallyFunc = finallyFunc; return Start();
    }

    public TResult Start() {
        TResult result = default;
        bool isSuccess = false;
        try {
            result = Action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {
            result = CatchFunc != null ? CatchFunc.Invoke(ex) : default;
        } finally {
            FinallyFunc?.Invoke(isSuccess);
        } return result;
    }
}

#endregion Try

#region Random

internal static class Ran {
    public static Random Current { get; } = new();

    public static int Next(int min = int.MinValue, int max = int.MaxValue) => Current.Next(min, max);
    public static double NextDouble() => Current.NextDouble();
    public static byte[] NextBytes(int length) => NextBytes(Current, length);
    public static void NextBytes(byte[] bf) => Current.NextBytes(bf);

    public static byte[] NextBytes(this Random random, int length) {
        byte[] buf = new byte[length];
        random.NextBytes(buf);
        return buf;
    }
}

#endregion

#pragma warning restore IDE0056 // 인덱스 연산자 사용
#pragma warning restore CS8632 // nullable 참조 형식에 대한 주석은 코드에서 '#nullable' 주석 컨텍스트 내에만 사용되어야 합니다.
#pragma warning restore IDE0079 // 불필요한 비표시 오류(Suppression) 제거
#pragma warning restore IDE1006 // 명명 스타일