using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Remoting.Messaging;

namespace Calculator
{
    //AOP方法处理类,实现了IMessageSink接口,以便返回给IContributeObjectSink接口的GetObjectSink方法
    public sealed class MyAopHandler:IMessageSink
    {
       
        //下一个接收器
        private IMessageSink nextSink;

        public MyAopHandler(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }

        public IMessageSink NextSink
        {
            get { return nextSink; }
        }

        //同步处理方法
        public IMessage SyncProcessMessage(IMessage msg)
        {

            IMessage message = null;

            //方法调用接口
            IMethodCallMessage callMessage = msg as IMethodCallMessage;

            //如果被调用的方法没打MyCalculatorMethodAttribute标签
            if (callMessage == null || (Attribute.GetCustomAttribute(callMessage.MethodBase, typeof(MyCalculatorMethodAttribute))) == null)
            {
                message = nextSink.SyncProcessMessage(msg);
            }
            else
            {
                PreProceed(msg);
                message = nextSink.SyncProcessMessage(msg);
                PostProceed(message);
            }

            return message;
        }

        //异步处理方法
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            Console.WriteLine("异步处理方法...");
            return null;
        }

        //方法执行前
        public void PreProceed(IMessage msg)
        {
            IMethodMessage message = (IMethodMessage)msg;
            Console.WriteLine("方法开始...");
            Console.WriteLine("方法: {0}", message.MethodName);
            Console.WriteLine("这个方法一共有{0}个参数:", message.ArgCount);
            for (int i = 0; i < message.ArgCount; i++)
            {
                Console.WriteLine("参数{0}：值: {1}.",i+1,message.Args[i]);
            }
        }

        //方法执行后
        public void PostProceed(IMessage msg)
        {
            IMethodReturnMessage message = (IMethodReturnMessage)msg;

            Console.WriteLine("方法的返回值: {0}", message.ReturnValue);
            Console.WriteLine("方法结束\n");
        }
    }
}
