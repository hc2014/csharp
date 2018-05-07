本目录中有几个AOP编程实例，首先对其实现方式做一个简单的说明

[APO编程实力一](https://github.com/hc2014/csharp/tree/master/AOP%E7%BC%96%E7%A8%8B%E5%AE%9E%E5%8A%9B%E4%B8%80) 使用是第三方开源dll（KingAOP）这个来实现自动日志非常强大，它可以截获方法的开始之前、结束、方法返回值、方法参数以及执行异常。但是缺点是  实例化方法的时候 必须要用**dynamic**

```
dynamic test = new TestClass();
```



[AOP编程实例二](https://github.com/hc2014/csharp/tree/master/AOP%E7%BC%96%E7%A8%8B%E5%AE%9E%E4%BE%8B%E4%BA%8C) 使用的是微软企业库，通过nugen 安装 EnterpriseLibrary 相关的几个dll即可

```
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;//本例中未用到
using Microsoft.Practices.EnterpriseLibrary.Logging;//本例中未用到
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;//
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
```



AOP编程顺利三 是利用特性来拦截方法信息，其中最重要的一个特性类是**ContextBoundObject** 