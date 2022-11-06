using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Request.Response.Middleware.FileLogger.Library;
using Request.Response.Middleware.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mİddleware.Test.API
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
            services.AddLogging(cfg =>
            {
                cfg.AddConsole();
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mİddleware.Test.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mİddleware.Test.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.AddRequestResponseFileLoggerMiddleware(opt =>
            {
                opt.FileDirectory = AppDomain.CurrentDomain.BaseDirectory;
                opt.FileName = "App_Log";          // Default [logs]
                opt.Extension = "txt";             // Default [txt]
                opt.UseJsonFormat = true;          // Default false
                opt.ForceCreateDirectory = true;   // Default true
            });

            //app.AddRequestResponseMiddleware(opt =>
            //{
            //    opt.UseHandler(async context =>
            //    {
            //        Console.WriteLine($"RequestBody: {context.RequestBody}");
            //        Console.WriteLine($"ResponseBody: {context.ResponseBody}");
            //        Console.WriteLine($"Timing: {context.FormattedCreationTime}");
            //        Console.WriteLine($"Url: {context.Url}");
            //    });

            //    opt.UseLogger(app.ApplicationServices.GetRequiredService<ILoggerFactory>(), ops =>
            //    {
            //        ops.LogLevel = LogLevel.Warning;
            //        ops.LoggerCategoryName = "CustomCategoryName";

            //        ops.LoggingFields.Add(LogFields.Request);
            //        ops.LoggingFields.Add(LogFields.Response);
            //        ops.LoggingFields.Add(LogFields.ResponseTiming);
            //        ops.LoggingFields.Add(LogFields.Path);
            //        ops.LoggingFields.Add(LogFields.QueryString);
            //        ops.LoggingFields.Add(LogFields.HostName);
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
