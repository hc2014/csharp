using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AOPClassLibrary
{
    public class LogHandlerAttribute : HandlerAttribute
    {
        public string LogInfo { set; get; }
        public int Order { get; set; }
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LogHandler() { Order = this.Order, LogInfo = this.LogInfo };
        }
    }
}
