using EPR.Accreditation.Facade.Helpers;
using EPR.Accreditation.Facade.Middleware;
using System.Security.Authentication;

namespace EPR.Accreditation.Facade
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IHostEnvironment env, IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services
                .AddHttpClient("HttpClient")
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        SslProtocols = SslProtocols.Tls12
                    };
                });
            services.AddFacadeDependencies(_config);
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
