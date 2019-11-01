using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCoreMyIstence.Model
{
    public class News
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string NewsClassifyId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Contents { get; set; }
        public DateTime PublishDate { get; set; }
        public string Remark { get; set; }
    }
}
