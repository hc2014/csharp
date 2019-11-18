using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessWatch
{
    public class HomeController
    {

        [Route("api/Home/Index",HttpMethodEnum.Post)]
        public void Index(TestModel test)
        {
            Console.WriteLine("index方法执行了");
        }

        [Route("api/Home/GetName", HttpMethodEnum.Post)]
        public string GetName(TestModel test)
        {
            Console.WriteLine("index方法执行了");
            return test.Name;
        }


        public void OtherFun()
        { 
        
        }

        [Route("api/Home/One", HttpMethodEnum.Get)]
        public string OneFun(string age,string name)
        {
            Console.WriteLine($"one方法执行了,参数age是:{age},name:{name}");
            return "OK";
        }
    }

    public class TestModel
    {
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
