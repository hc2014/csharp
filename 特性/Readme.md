###自定义Attribute
```
    [Serializable]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,Inherited=false,AllowMultiple=true)]
    [ComVisible(true)]
    public class CustomAttribute : Attribute
    {
        
        public CustomAttribute(string name, string datetime)
        {
            DateTime = Convert.ToDateTime(datetime);
            Name = name;

        }
        public string Msg { get; set; }
        public string Name { get; private set; }
        public DateTime DateTime { get; private set; }
    }
```

Attribute的参数分为两种,一种是写在Attribute构造函数参数列表里面的,称为位置参数(Positional Parameters)，
另外一种称为命名参数(Named Parameters).位置参数在调用Attribute的构造函数的时候参数顺序必须跟设定的一致，而命名参数就无所谓了。
而且位子参数对应的属性只能有一个get;是对外开发的,上面的那个写法是我偷懒了，其实应该这样写:
```
    [Serializable]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,Inherited=false,AllowMultiple=true)]
    [ComVisible(true)]
    public class CustomAttribute : Attribute
    {
        private string name = string.Empty;
        private DateTime dateTime;

        public CustomAttribute(string name, string datetime)
        {
            this.dateTime = Convert.ToDateTime(datetime);
            this.name = name;

        }
        public string Msg { get; set; }
        public string Name { get { return name; } }
        public DateTime DateTime { get { return dateTime; } }
    }
```

如果给位置参数也加了set的话,那么编译器就弄不起这个参数到底是什么东西，那位置参数跟命名参数都可以对这个属性赋值。
