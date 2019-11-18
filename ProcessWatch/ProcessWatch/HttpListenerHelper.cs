using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProcessWatch
{
    public class HttpListenerHelper
    {
        public static List<RouteModel> RouteModelsList = new List<RouteModel>();

        public static async Task SimpleListenerExampleAsync(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            HttpListener listener = new HttpListener();
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                HttpListenerContext context =await listener.GetContextAsync();
                try
                {
                    // 取得请求对象
                    HttpListenerRequest request = context.Request;

                    var mainUrl = request.RawUrl.ToLower().Split('?')[0].TrimStart('/');

                    Console.WriteLine("{0} {1} HTTP/1.1", request.HttpMethod, request.RawUrl);

                    Func<RouteModel, bool> predicate = (r) => r.RoutePath.ToLower() == mainUrl;

                    if (RouteModelsList.Count(predicate) <= 0)
                    {
                        NotFound(context);
                        continue;
                    }
                    var routeObj = RouteModelsList.First(predicate);

                    var method = request.HttpMethod.ToLower();
                    switch (method)
                    {
                        case "get":
                            GetMethod(context, request.RawUrl, routeObj);
                            break;
                        case "post":
                            PostMethod(context, request.RawUrl, routeObj);
                            break;
                        default:
                            NotFound(context);
                            break;
                    }
                }
                catch (Exception)
                {
                    ServiceError(context);
                    continue;
                }

                #region Context信息
                //Console.WriteLine("Accept: {0}", string.Join(",", request.AcceptTypes));
                //Console.WriteLine("Accept-Language: {0}",
                //    string.Join(",", request.UserLanguages));
                //Console.WriteLine("User-Agent: {0}", request.UserAgent);
                //Console.WriteLine("Accept-Encoding: {0}", request.Headers["Accept-Encoding"]);
                //Console.WriteLine("Connection: {0}",
                //    request.KeepAlive ? "Keep-Alive" : "close");
                //Console.WriteLine("Host: {0}", request.UserHostName);
                //Console.WriteLine("Pragma: {0}", request.Headers["Pragma"]);
                // 取得回应对象
                //HttpListenerResponse response = context.Response;
                // 构造回应内容
                //string responseString
                //    = @"<html>
                //<head><title>From HttpListener Server</title></head>
                //<body><h1>Hello, world.</h1></body></html>";
                //// 设置回应头部内容，长度，编码
                //response.ContentLength64
                //    = System.Text.Encoding.UTF8.GetByteCount(responseString);
                //response.ContentType = "text/html; charset=UTF-8";
                //// 输出回应内容
                //System.IO.Stream output = response.OutputStream;
                //System.IO.StreamWriter writer = new System.IO.StreamWriter(output);
                //writer.Write(responseString);

                //var data = Encoding.UTF8.GetString(ReadLineAsBytes(request.InputStream));

                //Console.WriteLine($"data:{data}");

                //response.StatusCode = 404;

                //response.Close();

                //string responseString
                //    = "{\"msg\":\"ok\"}";
                //// 设置回应头部内容，长度，编码
                //response.ContentLength64
                //    = System.Text.Encoding.UTF8.GetByteCount(responseString);

                //response.Headers.Add("Access-Control-Allow-Origin", "*");
                //response.ContentType = "application/json; charset=UTF-8";
                ////输出回应内容
                //System.IO.Stream output = response.OutputStream;
                //System.IO.StreamWriter writer = new System.IO.StreamWriter(output);
                //writer.Write(responseString);

                #endregion

                if (Console.KeyAvailable)
                    break;
            }


            listener.Stop();
        }


        public static void GetMethod(HttpListenerContext context,string routeUrl,RouteModel obj)
        {
            Task.Factory.StartNew(()=> {
                try
                {
                    object[] parms = FormatParams(routeUrl.ToLower().Split('?')[1]);

                    var returnObj = obj.Method.Invoke(obj.InstanceObject, parms);
                    var msg = returnObj ?? "";

                    WriteResponse(context, (string)msg);
                }
                catch (Exception)
                {
                    ServiceError(context);
                }
            });
        }

        
        public static void PostMethod(HttpListenerContext context, string routeUrl, RouteModel obj)
        {
            Task.Factory.StartNew(()=> {
                try
                {

                    object returnObj=null;
                    var paramCount = obj.Method.GetParameters().Count();
                    if (paramCount > 0)
                    {
                        var data = Encoding.UTF8.GetString(ReadLineAsBytes(context.Request.InputStream));

                        var postDataObj = JsonConvert.DeserializeObject(data, Common.GetModelType(obj.Method.GetParameters()[0].ParameterType.Name));
                        var parms = new object[] { postDataObj };
                        returnObj = obj.Method.Invoke(obj.InstanceObject, parms);
                    }
                    else
                    {
                         returnObj = obj.Method.Invoke(obj.InstanceObject, null);
                    }

                   
                    var msg = returnObj ?? "";

                    WriteResponse(context, (string)msg);
                }
                catch (Exception ex)
                {
                    ServiceError(context);
                }
            });
        }


        private static void WriteResponse(HttpListenerContext context, string msg)
        {
            var response = context.Response;
            response.ContentLength64 = System.Text.Encoding.UTF8.GetByteCount(msg);

            response.ContentType = "application/json; charset=UTF-8";
            System.IO.Stream output = response.OutputStream;
            System.IO.StreamWriter writer = new System.IO.StreamWriter(output);
            writer.Write(msg);
            writer.Close();
        }


        public static object[] FormatParams(string paramStr)
        {
            List<object> list = new List<object>();
            foreach (var item in paramStr.Split('&'))
            {
                list.Add(item.Split('=')[1].Trim());
            }
            return list.ToArray();
        }


        public static void NotFound(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            response.StatusCode = 404;
            response.Close();
        }


        public static void ServiceError(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            response.StatusCode = 502;
            response.Close();
        }


        private static byte[] ReadLineAsBytes(Stream SourceStream)
        {
            var resultStream = new MemoryStream();


            while (true)
            {
                int data = SourceStream.ReadByte();
                if (data < 0)
                    break;
                resultStream.WriteByte((byte)data);

            }
            resultStream.Position = 0;
            byte[] dataBytes = new byte[resultStream.Length];
            resultStream.Read(dataBytes, 0, dataBytes.Length);
            return dataBytes;
        }
    }
}
