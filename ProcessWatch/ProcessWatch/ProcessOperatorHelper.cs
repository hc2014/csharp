using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProcessWatch
{
    public class ProcessOperatorHelper
    {
        /*
       0: 隐藏, 并且任务栏也没有最小化图标
       1: 用最近的大小和位置显示, 激活
       2: 最小化, 激活
       3: 最大化, 激活
       4: 用最近的大小和位置显示, 不激活
       5: 同 1
       6: 最小化, 不激活
       7: 同 3
       8: 同 3
       9: 同 1
       10: 同 1
       */
#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments
        [DllImport("kernel32.dll")]
#pragma warning restore CA2101 // Specify marshaling for P/Invoke string arguments
#pragma warning disable CA1401 // P/Invokes should not be visible
        public static extern int WinExec(string exeName, int operType);
#pragma warning restore CA1401 // P/Invokes should not be visible

        public static void KillProcess(int pid)
        {
            Process myProcessA = Process.GetProcessById(pid);
            myProcessA.Kill();
        }

        public static int StartProcess(string fileName)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = fileName;
                p.Start();
                return p.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public static async Task<List<ProcessModel>> GetAllProcessListAsync()
        {
            List<ProcessModel> list = new List<ProcessModel>();

            await Task.Run(()=> {
                Process[] procs = Process.GetProcesses();
                foreach (var item in procs)
                {
                    if (item.ProcessName.ToLower() != "system" && item.ProcessName.ToLower() != "idle")
                    {
                        try
                        {
                            list.Add(new ProcessModel { FileName = item.MainModule.FileName, Name = item.ProcessName, Pid = item.Id });
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            });

            return list;
        }

    }
}
