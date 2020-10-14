using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NetCore_Swagger_Prototype.Models.Prototype.Demo;
using System;
using System.IO;
using System.Reflection;

namespace NetCore_Swagger_Prototype
{
    /// <summary>
    ///     HackMD 說明
    ///         https://hackmd.io/@Syuan/Swagger文件
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        ///     執行時所需要加載的服務。
        /// </summary>
        /// <param name="services">IService</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DemoContext>(opt =>
               opt.UseInMemoryDatabase("DemoList"));

            services.AddControllers();

            #region Swagger Services

            services.AddSwaggerGen(UiAnnotation =>
            {
                /*  Document Info 
                 *  文件資訊
                 */
                UiAnnotation.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Title",
                    Description = "API Description",
                    TermsOfService = new Uri("https://docs.microsoft.com/zh-tw/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio"),
                    Contact = new OpenApiContact
                    {
                        Name = "聯繫方式",
                        Email = "Someone@Email.com",
                        Url = new Uri("http://example.com/Contatc"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "授權",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                /*  Read annotation generate xml document file.
                 *  讀取註解產生xml說明文件。
                 */
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                UiAnnotation.IncludeXmlComments(xmlPath);

                //  DemoSwaggerResponse
                UiAnnotation.EnableAnnotations();
            }); ;

            #endregion
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        ///     執行時所使用的相關Http服務配置
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger Setting

            /*  Enable middleware to serve generated Swagger as a JSON endpoint.
             *  允許Swagger使用中介軟體產生的JSON
             */
            app.UseSwagger();

            app.UseSwaggerUI(UiTitle =>
            {
                /* Swagger Json file path and Api page name
                 * Swagger Json 路徑與頁面名稱
                 */
                UiTitle.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger prototype API");
            });
            #endregion

            #region Defaul Setting

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #endregion
        }
    }
}
