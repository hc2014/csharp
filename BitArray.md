# BitArray类

BitArray类 是一个引用类型，用于管理位值的压缩数组，这些值以布尔值的形式表示，其中 **true** 表示此位为开 (1)，**false** 表示此位为关 (0)。



### 构造函数

|                                          | 名称                                       | 说明                                       |
| ---------------------------------------- | ---------------------------------------- | ---------------------------------------- |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [BitArray(BitArray)](https://msdn.microsoft.com/zh-cn/library/z7sfcbhe.aspx) | 初始化包含从指定 BitArray 复制的位值的 BitArray 类的新实例。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [BitArray(Boolean)](https://msdn.microsoft.com/zh-cn/library/twk7f5c5.aspx) | 初始化 BitArray 类的新实例，该实例包含从布尔值指定数组复制的位值。   |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [BitArray(Byte)](https://msdn.microsoft.com/zh-cn/library/x1xda43a.aspx) | 初始化 BitArray 的新实例，该实例包含从指定的字节数组复制的位值。    |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [BitArray(Int32)](https://msdn.microsoft.com/zh-cn/library/4ty2t3fx.aspx) | 初始化 BitArray 类的新实例，该类可拥有指定数目的位值，位值最初设置为 **false**。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [BitArray(Int32, Boolean)](https://msdn.microsoft.com/zh-cn/library/ebw14dhc.aspx) | 初始化 BitArray 类的新实例，此实例可以容纳指定数量的位值，并且将其初始值设置为指定值。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [BitArray(Int32)](https://msdn.microsoft.com/zh-cn/library/b3d1dwck.aspx) | 初始化 BitArray 类的新实例，该类包含从指定的 32 位整数数组复制的位值。 |

### 属性

|                                          | 名称                                       | 说明                                  |
| ---------------------------------------- | ---------------------------------------- | ----------------------------------- |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [Count](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.count.aspx) | 获取 BitArray 中包含的元素数。                |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [IsReadOnly](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.isreadonly.aspx) | 获取一个值，该值指示 BitArray 是否为只读。          |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [IsSynchronized](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.issynchronized.aspx) | 获取一个值，该值指示是否同步对 BitArray 的访问（线程安全）。 |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [Item[Int32]](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.item.aspx) | 获取或设置 BitArray 中特定位置的位值。            |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [Length](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.length.aspx) | 获取或设置 BitArray 中的元素数。               |
| ![System_CAPS_pubproperty](https://i-msdn.sec.s-msft.com/dynimg/IC74937.jpeg) | [SyncRoot](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.syncroot.aspx) | 获取可用于同步对 BitArray 的访问的对象。           |

### 方法

|                                          | 名称                                       | 说明                                       |
| ---------------------------------------- | ---------------------------------------- | ---------------------------------------- |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [And(BitArray)](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.and.aspx) | 在当前 BitArray 对象中的元素和指定数组中的相应元素之间执行按位“与”运算。 将修改当前 BitArray 对象，以存储按位“与”运算的结果。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Clone()](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.clone.aspx) | 创建 BitArray 的浅表副本。                       |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [CopyTo(Array, Int32)](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.copyto.aspx) | 从目标数组的指定索引处开始将整个 BitArray 复制到兼容的一维 [Array](https://msdn.microsoft.com/zh-cn/library/system.array.aspx)。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Equals(Object)](https://msdn.microsoft.com/zh-cn/library/bsc2ak47.aspx) | 确定指定的对象是否等于当前对象。（继承自 [Object](https://msdn.microsoft.com/zh-cn/library/system.object.aspx)。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Get(Int32)](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.get.aspx) | 获取 BitArray 中特定位置处的位值。                   |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [GetEnumerator()](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.getenumerator.aspx) | 返回循环访问 BitArray 的枚举数。                    |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [GetHashCode()](https://msdn.microsoft.com/zh-cn/library/system.object.gethashcode.aspx) | 作为默认哈希函数。（继承自 [Object](https://msdn.microsoft.com/zh-cn/library/system.object.aspx)。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [GetType()](https://msdn.microsoft.com/zh-cn/library/system.object.gettype.aspx) | 获取当前实例的 [Type](https://msdn.microsoft.com/zh-cn/library/system.type.aspx)。（继承自 [Object](https://msdn.microsoft.com/zh-cn/library/system.object.aspx)。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Not()](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.not.aspx) | 反转当前 BitArray 中的所有位值，以便将设置为 **true** 的元素更改为 **false**；将设置为 **false**的元素更改为 **true**。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Or(BitArray)](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.or.aspx) | 在当前 BitArray 对象中的元素和指定数组中的相应元素之间执行按位“或”运算。 将修改当前 BitArray 对象，以存储按位“或”运算的结果。 |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Set(Int32, Boolean)](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.set.aspx) | 将 BitArray 中特定位置处的位设置为指定值。               |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [SetAll(Boolean)](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.setall.aspx) | 将 BitArray 中的所有位设置为指定值。                  |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [ToString()](https://msdn.microsoft.com/zh-cn/library/system.object.tostring.aspx) | 返回表示当前对象的字符串。（继承自 [Object](https://msdn.microsoft.com/zh-cn/library/system.object.aspx)。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Xor(BitArray)](https://msdn.microsoft.com/zh-cn/library/system.collections.bitarray.xor.aspx) | 针对指定数组中的相应元素，在当前 BitArray 对象中的元素间执行按位“异或”运算。 将修改当前 BitArray 对象，以存储按位“异或”运算的结果。 |

### 扩展方法

|                                          | 名称                                       | 说明                                       |
| ---------------------------------------- | ---------------------------------------- | ---------------------------------------- |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [AsParallel()](https://msdn.microsoft.com/zh-cn/library/dd413237.aspx) | 已重载。启用查询的并行化。（由 [ParallelEnumerable](https://msdn.microsoft.com/zh-cn/library/system.linq.parallelenumerable.aspx) 定义。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [AsQueryable()](https://msdn.microsoft.com/zh-cn/library/bb353734.aspx) | 已重载。将转换 [IEnumerable](https://msdn.microsoft.com/zh-cn/library/system.collections.ienumerable.aspx) 到 [IQueryable](https://msdn.microsoft.com/zh-cn/library/system.linq.iqueryable.aspx)。（由 [Queryable](https://msdn.microsoft.com/zh-cn/library/system.linq.queryable.aspx) 定义。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [Cast()](https://msdn.microsoft.com/zh-cn/library/bb341406.aspx) | 将强制转换的元素 [IEnumerable](https://msdn.microsoft.com/zh-cn/library/system.collections.ienumerable.aspx) 为指定的类型。（由 [Enumerable](https://msdn.microsoft.com/zh-cn/library/system.linq.enumerable.aspx) 定义。） |
| ![System_CAPS_pubmethod](https://i-msdn.sec.s-msft.com/dynimg/IC91302.jpeg) | [OfType()](https://msdn.microsoft.com/zh-cn/library/bb360913.aspx) | 筛选的元素 [IEnumerable](https://msdn.microsoft.com/zh-cn/library/system.collections.ienumerable.aspx) 根据指定的类型。（由 [Enumerable](https://msdn.microsoft.com/zh-cn/library/system.linq.enumerable.aspx) 定义。） |



### 示例代码

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

            byte a = 5;
            BitArray myBit1 = new BitArray(a);//5个字位，初始化为false
            //myBit1为5位，取用默认初始化，5位都为 False。即为0，由此可见，平时一位的值0和1在C#里面变成False和True；
            myBit1[0] = true;
            myBit1[1] = true;

            Console.Write("my Bit1     Count:{0},length:{1},值如下:\n", myBit1.Count, myBit1.Length);
            PrintValues(myBit1, 8);//每8个元素为一行打印元素

            byte[] myByte1 = new byte[] { 1, 2, 3, 4, 5 };//字节数组，byte为0-255的类型
            BitArray myBit2 = new BitArray(myByte1);
            //使用myByte1初始化myBit2;共有5*8个字节位；
            //myByte2为byte数组，内容为1，2，3，4，5；二进制就是00000001，00000010，00000011，00000100，00000101，myBA3就为相应的5*8=40位
            //在myByte2中数字按照二进制数从右到左存取
            Console.Write("my  Bit2     Count:{0},length:{1},值如下:\n", myBit2.Count, myBit2.Length);
            PrintValues(myBit2, 8);//每8个元素为一行打印元素

            bool[] myBools = new bool[5] { true, false, true, true, false };
            BitArray myBA4 = new BitArray(myBools);
            //看输出，bool就想当于一位，myBools是长度为5的数组，变成myBA4后，也是5位；
            Console.Write("myBA4     Count:{0},length:{1},值如下:\n", myBA4.Count, myBA4.Length);
            PrintValues(myBA4, 8);//每8个元素为一行打印元素

            int[] myInts = new int[5] { 6, 7, 8, 9, 10 };
            BitArray myBA5 = new BitArray(myInts);
            //int是32位的，5个，换成BitArray当然就是5*32=160。
            Console.Write("myBA5    Count:{0},length:{1},值如下:\n", myBA5.Count, myBA5.Length);
            PrintValues(myBA5, 8);//每8个元素为一行打印元素

            Console.ReadKey();
        }

        public static void PrintValues(IEnumerable myList, int myWidth)
        {
            int i = myWidth;
            foreach (Object obj in myList)
            {
                if (i <= 0)
                {
                    i = myWidth;
                    Console.WriteLine();
                }
                i--;
                Console.Write("{0,8}", obj);
            }
            Console.WriteLine();
        }
    }
}
```



