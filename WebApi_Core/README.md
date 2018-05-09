



<!--添加Swagger 开始-->

**依赖项——右键——管理NuGet程序包——浏览——输入以下内容**

```
Install-Package Swashbuckle.AspNetCore -Pre
```



**双击Properties——点击生成——勾选XML文档文件**



**双击Startup.cs——在ConfigureServices、Configure中添加以下内容：**

**ConfigureServices：**

```
services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new Info
   {
    Version = "v1",
    Title = "接口文档",
    Description = "接口文档",
    TermsOfService = "None",
   Contact = new Contact { Name = "唐少", Email = "tang14455@163.com", Url = "" }
   });

    //设置swagger json和ui的注释路径。
    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
    var xmlPath = Path.Combine(basePath, "WebApi_Core.xml");//同XML文档文件中名称相同
    c.IncludeXmlComments(xmlPath);
    //  c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
 });
```



**Configure：**

```
         // 使中间件能够将生成的Swagger作为JSON端点。
        app.UseSwagger();
        // 使中间件能够服务swagger-ui（HTML，JS，CSS等），指定Swagger JSON端点。
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwBusManagement API V1");
            c.ShowExtensions();
        });
```



**双击Properties下launchSettings.json——更改launchUrl中值为swagger（默认打开Swagger帮助页）：**



**运行测试**



<!--添加Swagger 结束-->

<!--添加NLog 开始-->

**依赖项——右键——管理NuGet程序包——浏览——输入以下内容**

```
Install-Package NLog.Extensions.Logging -Pre
```



**在根目录下添加nlog.config**



**更改nlog.config中内容如下：**

```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <targets>
<target name="logfile" xsi:type="File" fileName="logs/${shortdate}.log" />
  </targets>
  
  <rules>
<logger name="*" minlevel="Info" writeTo="logfile" />
  </rules>
</nlog>
```



**选择项目——右键——在文件资源管理器中打开文件夹打开——打开WebApi_Core.csproj——添加以下内容：**

```
<ItemGroup>
    <Content Update="nlog.config" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>
```



**双击Startup.cs——更改Configure中内容：**

```
ILoggerFactory loggerFactory
loggerFactory.AddNLog();
```



**在Controller中添加以下内容测试：**





**选择项目——右键——在文件资源管理器中打开文件夹打开——打开bin—Debug—netcoreapp2.0—logs：**





<!--添加NLog 结束-->









































































