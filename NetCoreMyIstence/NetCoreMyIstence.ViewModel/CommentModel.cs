using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.ViewModel
{
    //提交数据的实体模型
    public class CommentModel
    {
        public int Id { get; set; }
        public string NewsName { get; set; }
        public string Contents { get; set; }
        public DateTime AddTime { get; set; }
        public string Remark { get; set; }
        public string Floor { get; set; }
    }
}
