using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessWatch
{
   public class Startup
    {
        public static void Start()
        {
            var list = Assembly.GetExecutingAssembly().GetTypes();
            var controllerList = list.Where(r => r.Name.EndsWith("Controller"));

            foreach (var controller in controllerList)
            {
                var obj = Activator.CreateInstance(controller);
                var tmpMethodList = controller.GetMethods().Where(r => r.CustomAttributes.Count(attr => attr.AttributeType == typeof(RouteAttribute)) > 0);
                foreach (var item in tmpMethodList)
                {
                    HttpListenerHelper.RouteModelsList.Add(new RouteModel
                    {
                        InstanceObject = obj,
                        Method = item,
                        HttpMethod = item.GetCustomAttribute<RouteAttribute>().HttpMethod,
                        RoutePath = item.GetCustomAttribute<RouteAttribute>().RoutePath
                    });
                }
            }
        }
    }
}
