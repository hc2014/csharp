### c++与c#类型转换

```
//c++:HANDLE(void *) ---- c#:System.IntPtr  

   //c++:Byte(unsigned char) ---- c#:System.Byte  

   //c++:SHORT(short) ---- c#:System.Int16  

   //c++:WORD(unsigned short) ---- c#:System.UInt16  

   //c++:INT(int) ---- c#:System.Int16

   //c++:INT(int) ---- c#:System.Int32  

   //c++:UINT(unsigned int) ---- c#:System.UInt16

   //c++:UINT(unsigned int) ---- c#:System.UInt32

   //c++:LONG(long) ---- c#:System.Int32  

   //c++:ULONG(unsigned long) ---- c#:System.UInt32  

   //c++:DWORD(unsigned long) ---- c#:System.UInt32  

   //c++:DECIMAL ---- c#:System.Decimal  

   //c++:BOOL(long) ---- c#:System.Boolean  

   //c++:CHAR(char) ---- c#:System.Char  

   //c++:LPSTR(char *) ---- c#:System.String  

   //c++:LPWSTR(wchar_t *) ---- c#:System.String  

   //c++:LPCSTR(const char *) ---- c#:System.String  

   //c++:LPCWSTR(const wchar_t *) ---- c#:System.String  

   //c++:PCAHR(char *) ---- c#:System.String  

   //c++:BSTR ---- c#:System.String  

   //c++:FLOAT(float) ---- c#:System.Single  

   //c++:DOUBLE(double) ---- c#:System.Double  

   //c++:VARIANT ---- c#:System.Object  

   //c++:PBYTE(byte *) ---- c#:System.Byte[]  

  //c++:BSTR ---- c#:StringBuilder

   //c++:LPCTSTR ---- c#:StringBuilder

   //c++:LPCTSTR ---- c#:string

   //c++:LPTSTR ---- c#:[MarshalAs(UnmanagedType.LPTStr)] string  

   //c++:LPTSTR 输出变量名 ---- c#:StringBuilder 输出变量名

   //c++:LPCWSTR ---- c#:IntPtr

   //c++:BOOL ---- c#:bool   

   //c++:HMODULE ---- c#:IntPtr   

   //c++:HINSTANCE ---- c#:IntPtr  

   //c++:结构体 ---- c#:public struct 结构体{};  

   //c++:结构体 **变量名 ---- c#:out 变量名 //C#中提前申明一个结构体实例化后的变量名

   //c++:结构体 &变量名 ---- c#:ref 结构体 变量名



  //c++:WORD ---- c#:ushort

   //c++:DWORD ---- c#:uint

   //c++:DWORD ---- c#:int

  //c++:UCHAR ---- c#:int

   //c++:UCHAR ---- c#:byte

   //c++:UCHAR* ---- c#:string

   //c++:UCHAR* ---- c#:IntPtr

  //c++:GUID ---- c#:Guid

   //c++:Handle ---- c#:IntPtr

   //c++:HWND ---- c#:IntPtr

   //c++:DWORD ---- c#:int

   //c++:COLORREF ---- c#:uint



  //c++:unsigned char ---- c#:byte

   //c++:unsigned char * ---- c#:ref byte

   //c++:unsigned char * ---- c#:[MarshalAs(UnmanagedType.LPArray)] byte[]

   //c++:unsigned char * ---- c#:[MarshalAs(UnmanagedType.LPArray)] Intptr

  //c++:unsigned char & ---- c#:ref byte

   //c++:unsigned char 变量名 ---- c#:byte 变量名

   //c++:unsigned short 变量名 ---- c#:ushort 变量名

   //c++:unsigned int 变量名 ---- c#:uint 变量名

   //c++:unsigned long 变量名 ---- c#:ulong 变量名

  //c++:char 变量名 ---- c#:byte 变量名 //C++中一个字符用一个字节表示,C#中一个字符用两个字节表示

   //c++:char 数组名[数组大小] ---- c#:MarshalAs(UnmanagedType.ByValTStr, SizeConst = 数组大小)] public string 数组名; ushort

  //c++:char * ---- c#:string //传入参数

   //c++:char * ---- c#:StringBuilder//传出参数

   //c++:char *变量名 ---- c#:ref string 变量名

   //c++:char *输入变量名 ---- c#:string 输入变量名

   //c++:char *输出变量名 ---- c#:[MarshalAs(UnmanagedType.LPStr)] StringBuilder 输出变量名

  //c++:char ** ---- c#:string

   //c++:char **变量名 ---- c#:ref string 变量名

   //c++:const char * ---- c#:string

   //c++:char[] ---- c#:string

   //c++:char 变量名[数组大小] ---- c#:[MarshalAs(UnmanagedType.ByValTStr,SizeConst=数组大小)] public string 变量名;  

  //c++:struct 结构体名 *变量名 ---- c#:ref 结构体名 变量名

   //c++:委托 变量名 ---- c#:委托 变量名

  //c++:int ---- c#:int

   //c++:int ---- c#:ref int

   //c++:int & ---- c#:ref int

   //c++:int * ---- c#:ref int //C#中调用前需定义int 变量名 = 0;

  //c++:*int ---- c#:IntPtr

   //c++:int32 PIPTR * ---- c#:int32[]

   //c++:float PIPTR * ---- c#:float[]



  //c++:double** 数组名 ---- c#:ref double 数组名

   //c++:double*[] 数组名 ---- c#:ref double 数组名

   //c++:long ---- c#:int

   //c++:ulong ---- c#:int

   //c++:UINT8 * ---- c#:ref byte //C#中调用前需定义byte 变量名 = new byte();   



  //c++:handle ---- c#:IntPtr

   //c++:hwnd ---- c#:IntPtr

   //c++:void * ---- c#:IntPtr   

   //c++:void * user_obj_param ---- c#:IntPtr user_obj_param

   //c++:void * 对象名称 ---- c#:([MarshalAs(UnmanagedType.AsAny)]Object 对象名称





   //c++:char, INT8, SBYTE, CHAR ---- c#:System.SByte   

   //c++:short, short int, INT16, SHORT ---- c#:System.Int16   

   //c++:int, long, long int, INT32, LONG32, BOOL , INT ---- c#:System.Int32   

   //c++:__int64, INT64, LONGLONG ---- c#:System.Int64   

   //c++:unsigned char, UINT8, UCHAR , BYTE ---- c#:System.Byte   

   //c++:unsigned short, UINT16, USHORT, WORD, ATOM, WCHAR , __wchar_t ---- c#:System.UInt16   

   //c++:unsigned, unsigned int, UINT32, ULONG32, DWORD32, ULONG, DWORD, UINT ---- c#:System.UInt32   

   //c++:unsigned __int64, UINT64, DWORDLONG, ULONGLONG ---- c#:System.UInt64   

   //c++:float, FLOAT ---- c#:System.Single   

   //c++:double, long double, DOUBLE ---- c#:System.Double   

  //Win32 Types ---- CLR Type   



  //Struct需要在C#里重新定义一个Struct

   //CallBack回调函数需要封装在一个委托里，delegate static extern int FunCallBack(string str);

  //unsigned char** ppImage替换成IntPtr ppImage

   //int& nWidth替换成ref int nWidth

   //int*, int&, 则都可用 ref int 对应

   //双针指类型参数，可以用 ref IntPtr

   //函数指针使用c++: typedef double (*fun_type1)(double); 对应 c#:public delegate double fun_type1(double);

   //char* 的操作c++: char*; 对应 c#:StringBuilder;

   //c#中使用指针:在需要使用指针的地方 加 unsafe

```



https://blog.csdn.net/hadstj/article/details/50464656

https://blog.csdn.net/lphbtm/article/details/41891987