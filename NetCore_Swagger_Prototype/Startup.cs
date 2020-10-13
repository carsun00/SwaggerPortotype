using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
///     HackMD 說明
///         https://hackmd.io/@Syuan/Swagger文件
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

        //  執行時所需要加載的服務。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Swagger Services

            services.AddSwaggerGen();

            #endregion
        }
        
        // 執行時所使用的相關Http服務配置
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger Setting

            // 允許Swagger使用中介軟體產生的JSON
            app.UseSwagger();

            app.UseSwaggerUI(UiTitle =>
            {
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
