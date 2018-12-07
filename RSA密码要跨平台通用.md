[bbsmax](https://www.bbsmax.com/)

# RSA密钥的跨平台通用

chy710	2015-10-22 [原文](https://www.bbsmax.com/link/Wk9KUFpaNjJkdg==)

RSA使用public key加密，用private key解密(签名相反，使用private key签名，用public key验证签名)。比如我跟合作方D之间的数据传输，我使用D提供给我的public key进行加密后，传给D，他使用他的private key解密后得到原文；response时，D使用我提供给他的public key加密，我收到后使用我的private key解密得到原文。一个常用的场景是两方之间的数据传输使用AES加密，再把AES的密钥通过RSA加密后一并传输。(AES的性能高过RSA)。

关于密钥的生成，[Linux下常用OpenSSL生成](http://m.blog.csdn.net/blog/u012664888/40784095)，也可以使用特定语言平台所提供的方法生成。

```
// c#版本 (参数false生成私钥,public生成公钥)
RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
rsa.ExportParameters(false); //对像
rsa.ExportCspBlob(false); //字符串
rsa.ToXmlString(false); //xml格式
```

```
# python版本
import rsa

(pubkey, privkey) = rsa.newkeys(1024)

pub = pubkey.save_pkcs1()  #public key
pri = privkey.save_pkcs1() #private key
```

也可以使用[OpenSSL.NET](https://github.com/openssl-net/openssl-net)生成，这是一个OpenSSL在.net上的实现。里边有个cli 项目可以在命令行下运行，他生成的应该和linux下使用OpenSSL生成的一致的。

```
genrsa -out rsa_private_key.pem 1024
rsa -in rsa_private_key.pem -pubout -out rsa_public_key.pem
```

然并卵，每个平台语言之间的RSA密钥不能通用，c#生成的java,python上用不了，OpenSSL生成的C#里用不了，[异常信息：不正确的提供程序版本](http://segmentfault.com/q/1010000000254486)

如果使用OpenSSL生成的密钥，在.net中使用OpenSSL.NET加密解密是个不错的选择。另一个办法就是[把密钥转换成](http://www.nullskull.com/q/61768/rsa-between--net-and-openssl-question.aspx)各自语言所需的格式，比如这个<http://csslab.s3.amazonaws.com/csslabs/Siva/opensslkey.cs>

可以把OpenSSL Key转换成c#需要的xml格式，这样就可以直接使用.net framework里的方法进行RSA加密解密了。

有时候合作方给你的可能不是一个public key,而一个cert证书文件，这就需要[从这个证书里提取出public key](http://www.oschina.net/question/104733_113159)了

```
#在linux下，通过openssl提取
openssl x509 -in ca.crt -pubkey
```

## [RSA密钥的跨平台通用的更多相关文章](https://www.bbsmax.com/R/ZOJPZZ62dv/)

1. [Java与.NET兼容的RSA密钥持久化方法](https://www.bbsmax.com/A/x9J2ZyDnJ6/)

2. [RSA密钥之C#格式与Java格式转换](https://www.bbsmax.com/A/A7zg1lW54n/)

3. [RSA密钥生成与使用](https://www.bbsmax.com/A/kjdwXbjzNp/)

4. [https://www.bbsmax.com/A/QV5ZZo75yb/](https://www.bbsmax.com/A/QV5ZZo75yb/)

5. [JAVA，NET RSA密钥格式转换](https://www.bbsmax.com/A/Gkz1opLg5R/)

6. [Atitit.rsa密钥生成器的attilax总结](https://www.bbsmax.com/A/A7zgvW6Pd4/)

7. [cmd命令进行RSA 密钥加密操作](https://www.bbsmax.com/A/pRdBrO41dn/)

8. [.NET Core RSA密钥的xml、pkcs1、pkcs8格式转换和JavaScript、Java等语言进行对接](https://www.bbsmax.com/A/o75NXA9ezW/)

9. [RSA加密(跨平台通用的)](https://www.bbsmax.com/A/kPzOGPW7zx/)