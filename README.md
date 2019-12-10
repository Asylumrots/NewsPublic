# NewsPublic

基于.NET Core MVC  + SqlSugerCore开发的新闻发布系统  
.net core 2.2 + sqlSugerCore 5.0.0.7  

后台管理登录 用户名：admin 密码：123456   

在线访问： [线上地址](http://47.102.210.25:70)   （可能暂无新闻数据）


## 预览页面

主页
![demo](https://github.com/Asylumrots/NewsPublic/blob/master/NetCoreMyIstence/wwwroot/img/main.png)

后台管理
![demo](https://github.com/Asylumrots/NewsPublic/blob/master/NetCoreMyIstence/wwwroot/img/out.png)


## 项目目录
``` 
├── 1.WebUI
│      ├── NetCoreMyIstence.Web 网站
├── 2.Application
│      ├── NetCoreMyIstence.IServices 服务接口
│      ├── NetCoreMyIstence.IServices 服务
├── 3.Rspository
│      ├── NetCoreMyIstence.IRepository 仓储接口
│      ├── NetCoreMyIstence.Repository.SqlServer SqlServer数据库服务   (后续可能会支持其他数据库)
├── 4.Entiry
│      ├── NetCoreMyIstence.Model 模型类  
│      ├── NetCoreMyIstence.ViewModel 视图模型类
├── 5.Uility
│      ├── NetCoreMyIstence.Tool 工具类
├── 6.Test
│      ├── NetCoreMyIstence.Test MSTest测试
```


## 许可

[MIT license](https://github.com/Asylumrots/NewsPublic/blob/master/LICENSE)

Copyright (c) 2019-present Asylumrots