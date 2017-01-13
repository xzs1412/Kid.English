添加邮件发送参考
http://www.cnblogs.com/dongshuangjie/p/5306307.html
http://forum.aspnetboilerplate.com/viewtopic.php?p=927
https://github.com/aspnetboilerplate/aspnetboilerplate/blob/master/src/Abp/Net/Mail/EmailSettingProvider.cs
http://forum.aspnetboilerplate.com/viewtopic.php?t=87&p=153
这里有两个小坑:
1.EmailSettingProvider里的设置是在AbpKernelModule的PostInitialize里通过SetttingManager读取各个Provider的值存起来,所以如果把加载Provider的代码放到模块的PostInitialize里就无法覆盖Abp里的EmailSettingProvider里的值
2.Abp会通过缓存读取Abp.Net.Mail.DefaultFromAddress和Abp.Net.Mail.DefaultFromDisplayName,如果缓存里没有,就去数据库AbpSettings表里取,而表中的这两个值在迁移时写在了Seed里,所以解决办法是改表里的数据或直接删除表里这两个值

=================================================

这个Demo涉及到的方面:
1.发送邮件
2.获取包含被软删除的记录
3.领域事件(EntityEvent类里)
4.hangfire