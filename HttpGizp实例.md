# Http Gzip 加密 解压数据

全部的代码

```c#
public static async Task<string> PostAsync(string url, string param)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/json";
                req.Headers.Add("Accept-Encoding", "gzip");
                StringBuilder builder = new StringBuilder();
                byte[] data = Encoding.UTF8.GetBytes(param);
                //req.ContentLength = data.Length;


                using (Stream postStream = req.GetRequestStream())
                {
                    using (var zipStream = new GZipStream(postStream, CompressionMode.Compress))
                    {
                       await zipStream.WriteAsync(data, 0, data.Length);
                    }
                }

                string str = "";
                var response = req.GetResponse();
                using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(stream))
                    {
                         str = reader.ReadToEnd();
                    }
                }

                return str;
            }
            catch (Exception ex)
            {
                LogHelper.Info(string.Format("调用接口{0},参数:{1}报错:{2}", url, param, ex.Message));
                return "";
            }

        }
```



这里面有几个坑

#### 1.ContentLength:

```
req.ContentLength = data.Length;
```

如果定义了ContentLength,那么在 执行完  zipStream.WriteAsync 后会报错 的，错误信息大概是 **System.IO.IOException: 在写入所有字节之前不能关闭流**

```
using (Stream postStream = req.GetRequestStream())
{
   using (var zipStream = new GZipStream(postStream, CompressionMode.Compress))
    {
       await zipStream.WriteAsync(data, 0, data.Length);
     }
}
```



#### 2.使用 **GZipStream**

加密跟解压是不能够直接用**stream**或者**StreamReader**  需要通过 **GZipStream**转一次



