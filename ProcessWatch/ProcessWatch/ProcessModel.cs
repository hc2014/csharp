using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessWatch
{
    public class ProcessModel
    {
        public int Pid { get; set; }

        public string Name { get; set; }

        public string  FileName { get; set; }

        public List<int> PidList { get; set; }
    }
}
