using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessWatch
{
    public class RouteModel
    {
        /// <summary>
        /// 路由路径
        /// </summary>
        public string RoutePath { get; set; }

        /// <summary>
        /// 示例对象
        /// </summary>
        public object InstanceObject { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public MethodInfo Method { get; set; }



        public HttpMethodEnum HttpMethod { get; set; }
    }
}
