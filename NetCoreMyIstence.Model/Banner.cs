using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreMyIstence.Model
{
    public class Banner
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public DateTime AddTime{ get; set; }
        public string Remark { get; set; }
    }
}
