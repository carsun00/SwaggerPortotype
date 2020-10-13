using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace NetCore_Swagger_Prototype
{
    /// <summary>
    ///     HackMD ����
    ///         https://hackmd.io/@Syuan/Swagger���
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
        ///     ����ɩһݭn�[�����A�ȡC
        /// </summary>
        /// <param name="services">IService</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Swagger Services

            services.AddSwaggerGen(UiAnnotation =>
            {
                /*  Document Info 
                 *  ����T
                 */
                UiAnnotation.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "�x����",
                    Description = "Microsoft Swagger Document",
                    TermsOfService = new Uri("https://docs.microsoft.com/zh-tw/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio"),
                    Contact = new OpenApiContact
                    {
                        Name = "Swageer",
                        Email = string.Empty,
                        Url = new Uri("https://swagger.io/"),
                    }
                });

                /*  Read annotation generate xml document file.
                 *  Ū�����Ѳ���xml�������C
                 */
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                UiAnnotation.IncludeXmlComments(xmlPath);
            }); ;

            #endregion
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        ///     ����ɩҨϥΪ�����Http�A�Ȱt�m
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger Setting

            /*  Enable middleware to serve generated Swagger as a JSON endpoint.
             *  ���\Swagger�ϥΤ����n�鲣�ͪ�JSON
             */
            app.UseSwagger();

            app.UseSwaggerUI(UiTitle =>
            {
                /* Swagger Json file path and Api page name
                 * Swagger Json ���|�P�����W��
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
