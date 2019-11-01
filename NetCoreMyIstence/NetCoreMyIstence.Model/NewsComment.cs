using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCoreMyIstence.Model
{
    public class NewsComment
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id{ get; set; }
        public int NewsId { get; set; }
        public string Contents { get; set; }
        public DateTime AddTime{ get; set; }
        public string Remark { get; set; }
    }
}
