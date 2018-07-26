### 汉字跟Unicode互转

```c#
public static string String2Unicode(string source)
{
  byte[] bytes = Encoding.Unicode.GetBytes(source);
  StringBuilder stringBuilder = new StringBuilder();
  for (int i = 0; i < bytes.Length; i += 2)
  {
    stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
  }
  return stringBuilder.ToString();
}

public static string Unicode2String(string source)
{
  return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
    source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
}
```

调用

```c#
var str2 = String2Unicode("我们");//返回 \u6211\u4eec

var str3 = Unicode2String("\u6211\u4eec");//返回“我们”
```

