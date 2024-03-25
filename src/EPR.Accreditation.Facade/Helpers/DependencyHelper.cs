using EPR.Accreditation.Facade.Common.RESTservices;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Configuration;
using EPR.Accreditation.Facade.Services;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EPR.Accreditation.Facade.Helpers
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddFacadeDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .Configure<ServicesConfiguration>(configuration.GetSection(ServicesConfiguration.SectionName));

            services
                .AddScoped<IAccreditationService, AccreditationService>()
                .AddScoped<IMaterialService, MaterialService>()
                .AddScoped<IHttpMaterialService>(s =>
                    new HttpMaterialService(
                        s.GetRequiredService<IHttpContextAccessor>(),
                        s.GetRequiredService<IHttpClientFactory>(),
                        s.GetRequiredService<IOptions<ServicesConfiguration>>().Value.AccreditationAPI.Url,
                        "Material")
                )
                .AddScoped<IHttpAccreditationService>(s =>
                    new HttpAccreditationService(
                        s.GetRequiredService<IHttpContextAccessor>(),
                        s.GetRequiredService<IHttpClientFactory>(),
                        s.GetRequiredService<IOptions<ServicesConfiguration>>().Value.AccreditationAPI.Url,
                        "Accreditation")
                );

            services
                .AddScoped<ISaveAndContinueService, SaveAndContinueService>()
                .AddScoped<IHttpSaveAndContinueService>(s =>
                    new HttpSaveAndContinueService(
                        s.GetRequiredService<IHttpContextAccessor>(),
                        s.GetRequiredService<IHttpClientFactory>(),
                        s.GetRequiredService<IOptions<ServicesConfiguration>>().Value.AccreditationFacade.Url,
                        "SaveAndContinue"
                    )
            );

            return services;
        }
    }
}