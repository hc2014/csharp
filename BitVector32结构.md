# BitVector32结构

提供一个简单结构，该结构以 32 位内存存储布尔值和小整数。



## 构造函数

|                                          | 名称                                       | 说明                                       |
| ---------------------------------------- | ---------------------------------------- | ---------------------------------------- |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [BitVector32(BitVector32)](https://msdn.microsoft.com/zh-cn/library/c655e8ss(v=vs.110).aspx) | 新实例初始化 BitVector32 包含表示在现有的数据结构 BitVector32 结构。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [BitVector32(Int32)](https://msdn.microsoft.com/zh-cn/library/2zxz5d6h(v=vs.110).aspx) | 新实例初始化 BitVector32 包含整数形式表示的数据结构。        |

## 属性

|                                          | 名称                                       | 说明                                       |
| ---------------------------------------- | ---------------------------------------- | ---------------------------------------- |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [Data](https://msdn.microsoft.com/zh-cn/library/system.collections.specialized.bitvector32.data(v=vs.110).aspx) | 获取的值 BitVector32 为整数。                    |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [Item[Int32\]](https://msdn.microsoft.com/zh-cn/library/a3ddh1xt(v=vs.110).aspx) | 获取或设置由指定屏蔽指示的位标志的状态。                     |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [Item[BitVector32.Section\]](https://msdn.microsoft.com/zh-cn/library/2hh1kh1b(v=vs.110).aspx) | 获取或设置存储在指定的值 [BitVector32.Section](https://msdn.microsoft.com/zh-cn/library/system.collections.specialized.bitvector32.section(v=vs.110).aspx)。 |

## 方法

|                                          | 名称                                       | 说明                                       |
| ---------------------------------------- | ---------------------------------------- | ---------------------------------------- |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg)![System_CAPS_static](https://i-msdn.sec.s-msft.com/dynimg/IC64394.jpeg) | [CreateMask()](https://msdn.microsoft.com/zh-cn/library/asbhstzz(v=vs.110).aspx) | 在该系列屏蔽可以用于检索中的单个位中创建的第一个掩码 BitVector32作为位标志设置。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg)![System_CAPS_static](https://i-msdn.sec.s-msft.com/dynimg/IC64394.jpeg) | [CreateMask(Int32)](https://msdn.microsoft.com/zh-cn/library/4tceh7wh(v=vs.110).aspx) | 创建后屏蔽可以用于检索中的单个位掩码的一系列中的指定的其他掩码 BitVector32 作为位标志设置。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg)![System_CAPS_static](https://i-msdn.sec.s-msft.com/dynimg/IC64394.jpeg) | [CreateSection(Int16)](https://msdn.microsoft.com/zh-cn/library/9sxdk3tb(v=vs.110).aspx) | 创建第一个 [BitVector32.Section](https://msdn.microsoft.com/zh-cn/library/system.collections.specialized.bitvector32.section(v=vs.110).aspx) 中的一系列节包含小整数。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg)![System_CAPS_static](https://i-msdn.sec.s-msft.com/dynimg/IC64394.jpeg) | [CreateSection(Int16, BitVector32.Section)](https://msdn.microsoft.com/zh-cn/library/530d8529(v=vs.110).aspx) | 创建一个新 [BitVector32.Section](https://msdn.microsoft.com/zh-cn/library/system.collections.specialized.bitvector32.section(v=vs.110).aspx) 后面指定 [BitVector32.Section](https://msdn.microsoft.com/zh-cn/library/system.collections.specialized.bitvector32.section(v=vs.110).aspx) 中的一系列节包含小整数。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Equals(Object)](https://msdn.microsoft.com/zh-cn/library/xtz9abhe(v=vs.110).aspx) | 确定指定的对象是否等于 BitVector32。（覆盖 [ValueType.Equals(Object)](https://msdn.microsoft.com/zh-cn/library/2dts52z7(v=vs.110).aspx)。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [GetHashCode()](https://msdn.microsoft.com/zh-cn/library/system.collections.specialized.bitvector32.gethashcode(v=vs.110).aspx) | 用作哈希函数 BitVector32。（覆盖 [ValueType.GetHashCode()](https://msdn.microsoft.com/zh-cn/library/system.valuetype.gethashcode(v=vs.110).aspx)。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [GetType()](https://msdn.microsoft.com/zh-cn/library/system.object.gettype(v=vs.110).aspx) | 获取当前实例的 [Type](https://msdn.microsoft.com/zh-cn/library/system.type(v=vs.110).aspx)。（继承自 [Object](https://msdn.microsoft.com/zh-cn/library/system.object(v=vs.110).aspx)。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [ToString()](https://msdn.microsoft.com/zh-cn/library/xc3t0k0k(v=vs.110).aspx) | 返回表示当前 BitVector32 的字符串。（覆盖 [ValueType.ToString()](https://msdn.microsoft.com/zh-cn/library/wb77sz3h(v=vs.110).aspx)。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg)![System_CAPS_static](https://i-msdn.sec.s-msft.com/dynimg/IC64394.jpeg) | [ToString(BitVector32)](https://msdn.microsoft.com/zh-cn/library/42cxswk0(v=vs.110).aspx) | 返回一个字符串，表示指定 BitVector32。                |



### 示例一

```c#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public static void Main()
        {

            BitVector32 myBV = new BitVector32(0);

            // Creates masks to isolate each of the first five bit flags.
            int myBit1 = BitVector32.CreateMask();
            int myBit2 = BitVector32.CreateMask(myBit1);
            int myBit3 = BitVector32.CreateMask(myBit2);
            int myBit4 = BitVector32.CreateMask(myBit3);
            int myBit5 = BitVector32.CreateMask(myBit4);

            // Sets the alternating bits to TRUE.
            Console.WriteLine("Setting alternating bits to TRUE:");
            Console.WriteLine("   Initial:         {0}", myBV.ToString());
            myBV[myBit1] = true;
            Console.WriteLine("   myBit1 = TRUE:   {0}", myBV.ToString());
            myBV[myBit3] = true;
            Console.WriteLine("   myBit3 = TRUE:   {0}", myBV.ToString());
            myBV[myBit5] = true;
            Console.WriteLine("   myBit5 = TRUE:   {0}", myBV.ToString());

            Console.ReadKey();
        }
    }
}
```

输出:

```c#
Setting alternating bits to TRUE:
   Initial:         BitVector32{00000000000000000000000000000000}
   myBit1 = TRUE:   BitVector32{00000000000000000000000000000001}
   myBit3 = TRUE:   BitVector32{00000000000000000000000000000101}
   myBit5 = TRUE:   BitVector32{00000000000000000000000000010101}
```



### 实例二

```c#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public static void Main()
        {

            // Creates and initializes a BitVector32.
            BitVector32 myBV = new BitVector32(0);

            // Creates four sections in the BitVector32 with maximum values 6, 3, 1, and 15.
            // mySect3, which uses exactly one bit, can also be used as a bit flag.
            BitVector32.Section mySect1 = BitVector32.CreateSection(6);
            BitVector32.Section mySect2 = BitVector32.CreateSection(3, mySect1);
            BitVector32.Section mySect3 = BitVector32.CreateSection(1, mySect2);
            BitVector32.Section mySect4 = BitVector32.CreateSection(15, mySect3);

            // Displays the values of the sections.
            Console.WriteLine("Initial values:");
            Console.WriteLine("\tmySect1: {0}", myBV[mySect1]);
            Console.WriteLine("\tmySect2: {0}", myBV[mySect2]);
            Console.WriteLine("\tmySect3: {0}", myBV[mySect3]);
            Console.WriteLine("\tmySect4: {0}", myBV[mySect4]);

            // Sets each section to a new value and displays the value of the BitVector32 at each step.
            Console.WriteLine("Changing the values of each section:");
            Console.WriteLine("\tInitial:    \t{0}", myBV.ToString());
            myBV[mySect1] = 5;
            Console.WriteLine("\tmySect1 = 5:\t{0}", myBV.ToString());
            myBV[mySect2] = 3;
            Console.WriteLine("\tmySect2 = 3:\t{0}", myBV.ToString());
            myBV[mySect3] = 1;
            Console.WriteLine("\tmySect3 = 1:\t{0}", myBV.ToString());
            myBV[mySect4] = 9;
            Console.WriteLine("\tmySect4 = 9:\t{0}", myBV.ToString());

            // Displays the values of the sections.
            Console.WriteLine("New values:");
            Console.WriteLine("\tmySect1: {0}", myBV[mySect1]);
            Console.WriteLine("\tmySect2: {0}", myBV[mySect2]);
            Console.WriteLine("\tmySect3: {0}", myBV[mySect3]);
            Console.WriteLine("\tmySect4: {0}", myBV[mySect4]);

            Console.ReadKey();
        }
    }
}
```

这个东西 没看懂干嘛用的...