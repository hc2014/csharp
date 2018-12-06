## RSA加密解密

[MSDN上面的代码](https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.rsacryptoserviceprovider?redirectedfrom=MSDN&view=netframework-4.7.2)



```c#
using System;

using System.Security.Cryptography;

using System.Text;

class RSACSPSample

{
    static void Main()
    {
        try
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");
            byte[] encryptedData;
            byte[] decryptedData;

            //Create a new instance of RSACryptoServiceProvider to generate
            //public and private key data.
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
            }
        }
        catch (ArgumentNullException)
        {
            //Catch this exception in case the encryption did
            //not succeed.
            Console.WriteLine("Encryption failed.");

        }
    }

    public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
    {
        try
        {
            byte[] encryptedData;
            //Create a new instance of RSACryptoServiceProvider.
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSAKeyInfo);

                encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            return encryptedData;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return null;
        }

    }

    public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
    {
        try
        {
            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSAKeyInfo);

                decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            return decryptedData;
        }
        //Catch and display a CryptographicException  
        //to the console.
        catch (CryptographicException e)
        {
            Console.WriteLine(e.ToString());

            return null;
        }

}
}
```


另外一个[帖子](https://www.cnblogs.com/yeye518/p/6051598.html)的代码:

```c#
 string str_Plain_Text = "How are you?How are you?How are you?How are you?=-popopolA";
            Console.WriteLine("明文：" + str_Plain_Text);
            Console.WriteLine("长度：" + str_Plain_Text.Length.ToString());
            Console.WriteLine();

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            string str_Public_Key;
            string str_Private_Key;
            string str_Cypher_Text = RSA_Encrypt(str_Plain_Text, out str_Public_Key, out str_Private_Key);
            Console.WriteLine("密文：" + str_Cypher_Text);
            Console.WriteLine("公钥：" + str_Public_Key);
            Console.WriteLine("私钥：" + str_Private_Key);
            
          string result=  RSA_Decrypt(str_Cypher_Text,str_Private_Key);
            
            
                  static public string RSA_Encrypt(string str_Plain_Text, out string str_Public_Key, out string str_Private_Key)
        {
            str_Public_Key = "";
            str_Private_Key = "";
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] DataToEncrypt = ByteConverter.GetBytes(str_Plain_Text);
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                str_Public_Key = Convert.ToBase64String(RSA.ExportCspBlob(false));
                str_Private_Key = Convert.ToBase64String(RSA.ExportCspBlob(true));

                //OAEP padding is only available on Microsoft Windows XP or later. 
                byte[] bytes_Cypher_Text = RSA.Encrypt(DataToEncrypt, false);
                str_Public_Key = Convert.ToBase64String(RSA.ExportCspBlob(false));
                str_Private_Key = Convert.ToBase64String(RSA.ExportCspBlob(true));
                string str_Cypher_Text = Convert.ToBase64String(bytes_Cypher_Text);
                return str_Cypher_Text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //RSA解密
        static public string RSA_Decrypt(string str_Cypher_Text, string str_Private_Key)
        {
            byte[] DataToDecrypt = Convert.FromBase64String(str_Cypher_Text);
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                byte[] bytes_Public_Key = Convert.FromBase64String(str_Private_Key);
                RSA.ImportCspBlob(bytes_Public_Key);
                
                byte[] bytes_Plain_Text = RSA.Decrypt(DataToDecrypt, false);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                string str_Plain_Text = ByteConverter.GetString(bytes_Plain_Text);
                return str_Plain_Text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

```





但是在跟java 交互的时候发现，java 发过来的私有秘钥无法解密，找了半天找到问题的原因了

.net 平台对于秘钥格式要求是xml，切是固定的xml格式的字符串,例如:

```xml
<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>
```

这个xml 是固定的格式:

```xml
<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>
```



所以需要把java的秘钥转成符合c#规则的秘钥

[RSA密钥之C#格式与Java格式转换](https://www.bbsmax.com/A/A7zg1lW54n/)

格式转换要用到一个开源加密库**Bouncy Castle Crypto APIs**官网地址： <http://www.bouncycastle.org/csharp/> 



代码如下:

```c#
 private void Form1_Load(object sender, EventArgs e)
{
	string str_Cypher_Text = @"java发过来的密文";
 	string str_Private_Key = @"java的私有私有秘钥";

 	string privateKey = RSAPrivateKeyJava2DotNet(str_Private_Key);
 	string result = RSADecrypt(privateKey, str_Cypher_Text);
}

public static string RSAPrivateKeyJava2DotNet(string privateKey)
{
    RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));

    return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                         Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                         Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
}

public static string RSADecrypt(string privatekey, string content)
{
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    byte[] cipherbytes;
    rsa.FromXmlString(privatekey);
    cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), true);

    return Encoding.UTF8.GetString(cipherbytes);
}
```



这样就可以了！！