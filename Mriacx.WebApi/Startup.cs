using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mriacx.Utility.Common;
using Newtonsoft.Json.Serialization;

namespace Mriacx.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private string ProjectName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            //没这句话，有些机器可以跑，有些不行
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var assemblyName = Assembly.GetExecutingAssembly().FullName;
            ProjectName = assemblyName.RemoveEmptySplit(',').First().RemoveEmptySplit('.').Last();
            ProjectName = ProjectName == "WebApi" ? "" : ProjectName;

            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
           
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddCors(options => options.AddPolicy("all", p => p.AllowAnyOrigin()));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            var projectName = string.IsNullOrEmpty(ProjectName) ? string.Empty : $".{ProjectName}";
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DoCare7.0 Api",
                    Description = "描述",
                    Contact = new OpenApiContact
                    {
                        Name = "Mriacx的WebService",
                        Email = string.Empty,
                        Url = new Uri("http://www.engugo.com")
                    },
                });
                var basePath = AppContext.BaseDirectory;
                //c.IncludeXmlComments(Path.Combine(basePath, $"Mriacx.WebApi{projectName}.xml"));
                c.IncludeXmlComments(Path.Combine(basePath, Assembly.GetExecutingAssembly().GetName().Name + ".xml"));
                c.CustomSchemaIds(x => x.FullName);
                
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="apiExplorer"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            app.UseSession();
            ServiceInfoHelper.WriteJs(apiExplorer, FileHandler.GetMapPath(Path.Combine(UpLoadFilePathCode.BaseFileDirectory, "Res")));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //如果不想带/swagger路径访问的话，就放开下面的注释
                //options.RoutePrefix = string.Empty;
            });
            app.UseCors("all");
            app.UseMvc();
        }
    }
}
