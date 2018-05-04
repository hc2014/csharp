using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;//
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;//
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AOP编程实例二
{
    public  class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var oUserTest1 = new User() { Name = "张三", PassWord = "123" };
                var oUserTest2 = new User() { Name = "李四", PassWord = "111" };

                var order1 = new Order() { Price = 1, Order_ID = "123" };
                var order = OrderOperation.GetInstance();
                order.Order_ID = "2222";
                order.GetPrice(order1);

                var oUser = UserOperation.GetInstance();
                oUser.Test(oUserTest1);
                oUser.Test2(oUserTest1, oUserTest2);
                oUser.Test3(oUserTest1);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

    }

    public class User
    {
        public string Name { set; get; }
        public string PassWord { set; get; }
    }

    public class Order
    {
        public int Price { get; set; }
        public string Order_ID { get; set; }
    }


    #region 1、定义特性方便使用
    public class LogHandlerAttribute : HandlerAttribute
    {
        public string LogInfo { set; get; }
        public int Order { get; set; }
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LogHandler() { Order = this.Order, LogInfo = this.LogInfo };
        }
    }
    #endregion

    #region 2、注册对需要的Handler拦截请求
    public class LogHandler : ICallHandler
    {
        public int Order { get; set; }
        public string LogInfo { set; get; }


        //public T GetMyType<T>(Type t)
        //{
        //    Type type = typeof(T);
        //    object entity = type.Assembly.CreateInstance(type.FullName);
        //    return (T)entity;
        //}


        //这个方法就是拦截的方法，可以规定在执行方法之前和之后的拦截
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("LogInfo内容" + LogInfo);
            //0.解析参数
            var arrInputs = input.Inputs;
            if (arrInputs.Count > 0)
            {
                //var className = input.MethodBase.GetParameters()[0].ParameterType.FullName;
                //Type type = Type.GetType(className);
                //object obj = type.Assembly.CreateInstance(className);

                //var p = type.GetProperties();

                //这里如果是公用方法，就不能直接转换为User对象，而是需要通过反射遍历属性，后获取各个属性的值
                var oUserTest1 = arrInputs[0] as User;
            }
            //1.执行方法之前的拦截
            Console.WriteLine("方法执行前拦截到了");
            //2.执行方法
            var messagereturn = getNext()(input, getNext);

            //3.执行方法之后的拦截
            Console.WriteLine("方法执行后拦截到了");
            return messagereturn;
        }
    }
    #endregion

    #region 3、用户定义接口和实现
    public interface IUserOperation
    {
        void Test(User oUser);
        void Test2(User oUser, User oUser2);
        
    }


    //这里必须要继承这个类MarshalByRefObject，否则报错
    public class UserOperation : MarshalByRefObject, IUserOperation
    {
        private static UserOperation oUserOpertion = null;
        public UserOperation()
        {
            //oUserOpertion = PolicyInjection.Create<UserOperation>();
        }

        //定义单例模式将PolicyInjection.Create<UserOperation>()产生的这个对象传出去，这样就避免了在调用处写这些东西
        public static UserOperation GetInstance()
        {
            if (oUserOpertion == null)
                oUserOpertion = PolicyInjection.Create<UserOperation>();

            return oUserOpertion;
        }
        //调用属性也会拦截
        public string Name { set; get; }

        //[LogHandler]，在方法上面加这个特性，只对此方法拦截
        [LogHandler(LogInfo = "Test的日志为aaaaa")]
        public void Test(User oUser)
        {
            Console.WriteLine("Test方法执行了");
        }

        [LogHandler(LogInfo = "Tes3t的日志为cccc")]
        public User Test3(User oUser)
        {
            Console.WriteLine("Test3方法执行了");
            return oUser;
        }


        [LogHandler(LogInfo = "Test2的日志为bbbbb")]
        public void Test2(User oUser, User oUser2)
        {
            Console.WriteLine("Test2方法执行了");
        }
    }
    #endregion


    public class OrderOperation : MarshalByRefObject
    {
        private static OrderOperation oOrderOpertion = null;

        public static OrderOperation GetInstance()
        {
            if (oOrderOpertion == null)
                oOrderOpertion = PolicyInjection.Create<OrderOperation>();

            return oOrderOpertion;
        }
        //调用属性也会拦截
        [LogHandler(LogInfo = "测试获取属性")]
        public string Order_ID { set; get; }

        //[LogHandler]，在方法上面加这个特性，只对此方法拦截
        [LogHandler(LogInfo = "GetPrice的日志为aaaaa")]
        public int GetPrice(Order order)
        {
            return order.Price;
        }
        
    }

}
