### 1.别名

在工作中经常会碰到调用别人接口，拿到的数据 跟数据库或者说跟自己命名规则一样的 JSON字符串，那么怎么把这个字符串反序列化成自己想要的类呢？

以下所有代码 都需要引用

```c#
using Newtonsoft.Json;
```



现在有个json字符串

```
 string jsonStr= "{\"first_anem\":\"Zhang\",\"last_anem\":\"SanFeng\",\"age\":\"18\"}";
```

反序列化后的对象模型应该是

```c#
 class TestObj
 {
   public string first_anem { get; set; }
   public string last_anem { get; set; }
   public string age { get; set; }
 }
```

然后我实际想要的数据结构是

```c#
 class TestObj
 {
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public string Age { get; set; }
 }
```

怎么搞？只需要在类中，每一个属性上面加一个特性**JsonProperty**

```c#
class TestObj
{
  [JsonProperty(PropertyName = "first_anem")]
  public string FirstName { get; set; }

  [JsonProperty(PropertyName = "last_anem")]
  public string LastName { get; set; }

  [JsonProperty(PropertyName = "age")]
  public string Age { get; set; }
}
```





### 2.部分序列化

Json.Net序列化的模式:OptOut 和 OptIn

| OptOut | 默认值,类中所有公有成员会被序列化,如果不想被序列化,可以用特性JsonIgnore |
| :----: | ---------------------------------------- |
| OptIn  | 默认情况下,所有的成员不会被序列化,类中的成员只有标有特性JsonProperty的才会被序列化,当类的成员很多,但客户端仅仅需要一部分数据时,很有用 |



修改TestObj,在FirstName 属性上面添加**JsonIgnore** 特性

```c#
class TestObj
{
  [JsonProperty(PropertyName = "first_anem"), JsonIgnore]
  public string FirstName { get; set; }

  [JsonProperty(PropertyName = "last_anem")]
  public string LastName { get; set; }

  [JsonProperty(PropertyName = "age")]
  public string Age { get; set; }
}
```



```c#
TestObj obj = JsonConvert.DeserializeObject<TestObj>(jsonStr);
```

obj对象的FirstName 属性就是为null



修改TestObj对象为 OptIn 模式

```c#
[JsonObject(MemberSerialization.OptIn)] 
class TestObj
 {
   [JsonProperty(PropertyName = "first_anem")]
   public string FirstName { get; set; }

   public string LastName { get; set; }

   public string Age { get; set; }
 }
```

只有添加了JsonProperty 特性的字段才可以被 反序列化





### 3.默认值

修改json字符串，拿掉age属性,obj.Age的值就是null

```c#
 string jsonStr= "{\"first_anem\":\"Zhang\",\"last_anem\":\"SanFeng\"}";
 TestObj obj = JsonConvert.DeserializeObject<TestObj>(jsonStr);
//obj.Age=null;
```



然后给Age属性添加一个默认值

```c#
public string Age { get; set; } = "10";
//或者
[DefaultValue(10)]
public string Age { get; set; };
```

再次执行

```
string jsonStr= "{\"first_anem\":\"Zhang\",\"last_anem\":\"SanFeng\"}";
 TestObj obj = JsonConvert.DeserializeObject<TestObj>(jsonStr);
//obj.Age="10";
```



那么这个默认值的问题怎么自己来控制是否显示呢？

```c#
TestObj p = new TestObj { Age = "10", FirstName= "Zhang", LastName= "SanFeng" };
JsonSerializerSettings jsetting = new JsonSerializerSettings();
jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;

var str = JsonConvert.SerializeObject(p, Formatting.Indented, jsetting);
```

因为选择了 DefaultValueHandling.Ignore 枚举值（过滤默认值），所以str字符串中是没有age这个字段的

这里有个有意思的地方，明明传入了Age="10"为什么还被过滤了？那是因为 默认值是也是“10”，如果传入的时候Age="11"，那就会显示了。

相反的DefaultValueHandling.Include 就是表示包含默认值





#### 4.关于null

```c#
TestObj p = new TestObj { FirstName= "Zhang", LastName= "SanFeng",Age=null };
JsonSerializerSettings jsetting = new JsonSerializerSettings();
jsetting.NullValueHandling = NullValueHandling.Ignore;
string str = JsonConvert.SerializeObject(p, Formatting.Indented, jsetting);
```

这样 str 中就没有Age这个字段了

可以单个属性设置

```c#
 [JsonProperty(PropertyName = "age",NullValueHandling = NullValueHandling.Ignore), DefaultValue("10")]
public string Age { get; set; }
```

然后调用

```c#
 TestObj p = new TestObj { FirstName= "Zhang", LastName= "SanFeng",Age=null };
 string str = JsonConvert.SerializeObject(p);
```



### 5.自定义数据格式

现在添加一个 DateTime类型的字段

```
public DateTime Birthday { get; set; }
//调用
 TestObj p = new TestObj { FirstName= "Zhang", LastName= "SanFeng",Age="10",Birthday=DateTime.Now };
 
string str = JsonConvert.SerializeObject(p);
```

直接序列化后的结果：

```json
{"first_anem":"Zhang","last_anem":"SanFeng","age":"10","Birthday":"2018-07-19T10:58:55.2430726+08:00"}
```

这个时间格式明显不是我们想要的

自定义一个时间转换器

```c#
public class ChinaDateTimeConverter : DateTimeConverterBase
{
  private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd hh:mm:ss" };

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    dtConverter.WriteJson(writer, value, serializer);
  }
}
```

然后在 时间类型的字段上添加这个转换器

```c#
[JsonConverter(typeof(ChinaDateTimeConverter))]
public DateTime Birthday { get; set; }
```

调用

```c#
 TestObj p = new TestObj { FirstName= "Zhang", LastName= "SanFeng",Age="10",Birthday=DateTime.Now };
string str = JsonConvert.SerializeObject(p);
//str={"first_anem":"Zhang","last_anem":"SanFeng","age":"10","Birthday":"2018-07-19 11:14:18"}
```



如果有枚举类型的值，可以直接用 Newtonsoft.Json内置的转换器->StringEnumConverter

添加枚举

```c#
public enum SexEnum
{
  男=1,
  女=0
}

```

添加属性

```
public SexEnum Sex { get; set; }
```

执行

```
TestObj p = new TestObj { FirstName= "Zhang", LastName= "SanFeng",Age="10",Birthday=DateTime.Now, Sex= SexEnum.女 };
string str = JsonConvert.SerializeObject(p);
// str中sex=0
```

加上StringEnumConverter特性

```
[JsonConverter(typeof(StringEnumConverter))]
public SexEnum Sex { get; set; }
```

执行后str字符串中sex="女"



参考地址:https://www.cnblogs.com/linyijia/p/5845771.html