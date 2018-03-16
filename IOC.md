## IOC

首先假设一个场景，快递公司发快递。

先创建一个快递类Express

```
 	/// <summary>
    /// 快递公司
    /// </summary>
    public class Express
    {
        public void SendGoods(ByCar car)
        {
            Console.WriteLine("这是"+car.Name);
        }
    }
```

创建一个快运方式的类ByCar(陆运)

```
 	public class ByCar
    {
        public string Name { get { return "陆运"; } }
    }
```

然后开始发快递了：

    static void Main(string[] args)    
    {
      	Express exp = new Express();
      	ByCar car = new ByCar();
      	exp.SendGoods(car);
    
    	Console.ReadKey();
    }
这样就可以完成需求了，但是假设现在客户需要发空运，并且指定快递公司必须是顺风，怎么搞？



所以需要加两个接口 IExpress,ILogostic

```

 	public interface IExpress
    {
        void SendGoods(ILogistic logistic);
    }

    public interface ILogistic
    {
        string Name { get;}
    }
```

然后创建一个SF快递

```
	public class SF : IExpress
    {
        public  void SendGoods(ILogistic logistic)
        {
            Console.WriteLine(logistic.Name);
        }
    }
```

创建陆运跟空运

```
	public class ByAir : ILogistic
    {
       public string Name { get {
                return "空运";
            } }
    }

    public class ByCar : ILogistic
    {
        public string Name { get { return "陆运"; } }
    }
```

现在用顺风快递发空运

```
 IExpress exp = new SF();
 ILogistic logis = new ByAir();
 xp.SendGoods(logis);
```

 

IOC还有一个重要的思想，叫做**依赖注入** 那么接着改造代码

新建一个 操作类

```
public class GoodsOperation
    {
        IExpress _express;
        ILogistic _logistic;
        public  GoodsOperation(IExpress exp, ILogistic logis)
        {
            _express = exp;
            _logistic = logis;
        }

        /// <summary>
        /// 发货
        /// </summary>
        public  void SendGoods()
        {
            _express.SendGoods(_logistic);
        }
    }
```

通过构造函数的方式把依赖关系注入到实际对象中，所以调用的时候就可以

```
GoodsOperation opera = new GoodsOperation(new SF(),new ByAir());
opera.SendGoods();
```



代码对应的就是IOC项目