using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.Start();

            string[] prefixes = new string[] { "http://localhost:8080/" };

            _ = HttpListenerHelper.SimpleListenerExampleAsync(prefixes);

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
