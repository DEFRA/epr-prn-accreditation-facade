using AutoMapper;
using EPR.Accreditation.Facade.Helpers;
using EPR.Accreditation.Facade.Middleware;
using EPR.Accreditation.Facade.Profiles;
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
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AccreditationProfile());
                mc.AllowNullCollections = true;
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
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
