using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Remoting.Contexts;

namespace Calculator
{
    //贴上类的标签
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    //当对一个类应用 sealed 修饰符时，此修饰符会阻止其他类从该类继承
    //ContextAttribute:构造器带有一个参数，用来设置ContextAttribute的名称。
    public sealed class MyCalculatorAttribute : ContextAttribute,
        IContributeObjectSink
    {

        public MyCalculatorAttribute()
            : base("MyCalculator")
        {
            //直接在构造函数里面捕获异常
            //AppDomain.CurrentDomain.FirstChanceException += (ss, ee) => {

            //};
            Console.WriteLine("MyCalculatorAttribute...构造函数");
        }


        //实现IContributeObjectSink接口当中的消息接收器接口
        public System.Runtime.Remoting.Messaging.IMessageSink GetObjectSink(MarshalByRefObject obj, System.Runtime.Remoting.Messaging.IMessageSink nextSink)
        {
            return new MyAopHandler(nextSink);
        }
    }
}
