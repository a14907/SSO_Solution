# SSO_Solution
项目演示步骤：
0.先编译，还原包，如有需要请开启EF的数据迁移功能
1.将server项目发布，设置域名为www.sso.com
2.发布site项目发布为三个站点，设置主页为：index.aspx，分别设置域名为：
  www.a.com
  www.b.com
  www.c.com
  （以上都是在本机上生效，需要写入hosts文件：向hosts文件中插入这些：
        127.0.0.1	www.sso.com
        127.0.0.1	www.a.com
        127.0.0.1	www.b.com
        127.0.0.1	www.c.com
  ）

3.此项目需使用到数据库，不过项目为EF，使用代码先行，直接运行就可
4.访问www.sso.com/home/register,先注册一个用户
5.访问：www.a.com/index.aspx，会跳转一个登录页面，登陆成功之后，访问其他的站点：www.b.com/index.aspx或www.c.com/index.aspx，就不需要再登陆了。
6.大概就这样，各位可根据自己需要自行拓展。
