using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;//
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AOPClassLibrary
{
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
}
