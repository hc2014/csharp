using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace AOPClassLibrary
{
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
            //调试的时候 这里会断点不执行，拖动断点让，代码继续执行，就会拦截到异常信息
            var a = "a";
            return Convert.ToInt32(a);

            //return order.Price;
        }

    }
}
