using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using NLog.Extensions.Logging;

namespace WebApi_Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册MVC到Container
            //默认首字母小写
            services.AddMvc();
            #region 
            //修改为首字母大写
            //services.AddMvc()
            //.AddJsonOptions(options =>
            // {
            //     if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
            //     {
            //         resolver.NamingStrategy = null;
            //     }
            // });

            //配置来添加xml格式
            //services.AddMvc()
            //.AddMvcOptions(options =>
            //  {
            //      options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            //  });
            #endregion

            #region Swagger设置
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
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //捕获并记录
                app.UseExceptionHandler();
            }

            //返回错误
            app.UseStatusCodePages();

            // 使中间件能够将生成的Swagger作为JSON端点。
            app.UseSwagger();

            // 使中间件能够服务swagger-ui（HTML，JS，CSS等），指定Swagger JSON端点。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwBusManagement API V1");
                c.ShowExtensions();
            });

            //使用mvc中间件
            app.UseMvc();
        }
    }
}
