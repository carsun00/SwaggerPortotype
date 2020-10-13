using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
///     HackMD ����
///         https://hackmd.io/@Syuan/Swagger���
/// </summary>
namespace NetCore_Swagger_Prototype
{
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

            services.AddSwaggerGen();

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
