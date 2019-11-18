using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessWatch
{
   public class ProcessController
    {
        [Route("Api/Process/GetList", HttpMethodEnum.Post)]
        public string GetList()
        {
            var list =ProcessOperatorHelper.GetAllProcessListAsync();
            return list.ToJson();
        }

        [Route("Api/Process/Kill", HttpMethodEnum.Get)]
        public string Kill(int pid)
        {
            ProcessOperatorHelper.KillProcess(pid);

            return new {Success="true",Msg="Ok" }.ToJson();
        }


        [Route("Api/Process/Start", HttpMethodEnum.Post)]
        public string Start(ProcessModel model)
        {
            var id = ProcessOperatorHelper.StartProcess(model.FileName);

            return new { Success = id > 0 ? "true" : "false", Data = new { Pid = id } }.ToJson();
        }

        [Route("Api/Process/GetInfomation", HttpMethodEnum.Post)]
        public string GetInfomation(ProcessModel model)
        {
            var ids = model.PidList;
            return "OK";
        }
    }
}
