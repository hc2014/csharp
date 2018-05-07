using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AOPClassLibrary
{
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
            var messageReturn = getNext()(input, getNext);


            //发送异常后 拦截
            if (messageReturn.Exception != null)
            {
                Console.WriteLine("发生了异常:{0}", messageReturn.Exception);
            }


            //3.执行方法之后的拦截
            Console.WriteLine("方法执行后拦截到了");
            return messageReturn;
        }
    }
}
