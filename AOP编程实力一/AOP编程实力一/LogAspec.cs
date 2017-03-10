using KingAOP.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP编程实力一
{
    /// <summary>
    /// LogAspec为自定义日志输出类,目前来说是控制台输出,当然也是可以加入第三方日志类库的(log4)
    /// LogAspec 需要继承OnMethodBoundaryAspect，而OnMethodBoundaryAspect是一个第三方的开源框架,直接引用对应的dll即可用
    /// </summary>
    public class LogAspec : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("进入方法:{0}",args.Method.Name);
            //如果有参数 就输出
            if (args.Arguments.Count > 0)
            {
                args.Arguments.ToList().ForEach(item =>
                {
                    Console.WriteLine("方法参数类型为:{0},对应的值为:{1}", item.GetType().Name, item);
                });
            }
            Console.WriteLine("-".PadRight(50, '-'));
        }
        public override void OnException(MethodExecutionArgs args)
        {

            Console.WriteLine("方法:{0}抛出了异常:{1}",args.Method.Name,args.Exception.Message);
            Console.WriteLine("-".PadRight(50,'-'));
        }
        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("方法:{0},执行结果为:{1}", args.Method.Name,args.ReturnValue);
            Console.WriteLine("-".PadRight(50, '-'));
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            //Console.WriteLine("退出: Hello KingAOP--" + args.Method.Name);
        }
    }
}
