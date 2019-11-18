using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessWatch
{
    public static class Common
    {
        public static Type GetModelType(string typeName)
        {
            switch (typeName)
            {
                case "TestModel":
                    return typeof(TestModel);
                case "ProcessModel":
                    return typeof(ProcessModel);
                default:
                    return null;
            }
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

    }
}
