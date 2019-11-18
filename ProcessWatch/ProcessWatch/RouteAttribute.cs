using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessWatch
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RouteAttribute:Attribute
    {
        public string RoutePath;
        public HttpMethodEnum HttpMethod;
        public RouteAttribute(string routePath, HttpMethodEnum httpMethod= HttpMethodEnum.Post) //name为定位参数
        {
            this.RoutePath = routePath;
            this.HttpMethod = httpMethod;
        }
    }
}
