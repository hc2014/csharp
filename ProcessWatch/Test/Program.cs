using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Data> list = new List<Data>();
            list.Add(new Data { Url = "http://localhost:8080/Api/Process/GetInfomation", PostData = new ProcessModel { PidList = new List<int> { 123, 456, 789 } } });
            list.Add(new Data { Url = "http://localhost:8080/Api/Process/Start", PostData = new ProcessModel { FileName = "C:\\Program Files (x86)\\Tencent\\QQ\\Bin\\QQ.exe" } });
            list.Add(new Data { Url = "http://localhost:8080/Api/Process/Start", PostData = new ProcessModel { FileName = @"C:\Users\Admin\AppData\Local\Google\Chrome\Application\chrome.exe" } });
            list.Add(new Data { Url = "http://localhost:8080/Api/Process/GetList" } );

            var json = JsonConvert.SerializeObject(new ProcessModel { FileName = @"C:\Users\Admin\AppData\Local\Google\Chrome\Application\chrome.exe" });

            for (int i = 0; i < 30; i++)
            {
                Task.Factory.StartNew(()=> {
                    Post("http://localhost:8080/Api/Process/Start", json);
                });
            }


            Console.ReadKey();
        }

        public static void Post(string url, string param)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/json";
                StringBuilder builder = new StringBuilder();
                byte[] data = Encoding.UTF8.GetBytes(param);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                var response = req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadToEnd();
                Console.WriteLine(string.Format("调用接口{0}成功,参数:{1}返回值:{2}", url, param, str));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("调用接口{0}是吧,参数:{1}报错:{2}", url, param, ex.Message));
            }

        }

        class Data
        {
            public string Url { get; set; }
            public ProcessModel PostData { get; set; }
        }

        public class ProcessModel
        {
            public int Pid { get; set; }

            public string Name { get; set; }

            public string FileName { get; set; }

            public List<int> PidList { get; set; }
        }

    }
}
