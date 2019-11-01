using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.ViewModel
{
    //返回响应的实体模型
    public class ResponseModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
}
