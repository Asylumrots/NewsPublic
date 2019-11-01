using NetCoreMyIstence.Model;
using SqlSugar;
using System;

namespace NetCoreMyIstence.Repository.SqlServer
{
    public class DbContext
    {
        public DbContext()
        {
            //创建SqlSuger对象
            Db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = "Data Source =.; Initial Catalog = NewsPublish; Integrated Security = True",//数据库连接字符串必填
                DbType = DbType.SqlServer,//数据库类型必填
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });
        }

        public SqlSugarClient Db;
        public SimpleClient<Banner> BannerDb { get { return new SimpleClient<Banner>(Db); } }
        public SimpleClient<News> NewsDb { get { return new SimpleClient<News>(Db); } }
        public SimpleClient<NewsClassify> NewsClassifyDb { get { return new SimpleClient<NewsClassify>(Db); } }
        public SimpleClient<NewsComment> NewsCommentDb { get { return new SimpleClient<NewsComment>(Db); } }
    }
}
