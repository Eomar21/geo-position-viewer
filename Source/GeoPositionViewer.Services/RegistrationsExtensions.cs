using Microsoft.Extensions.DependencyInjection;

namespace GeoPositionViewer.Services
{
    public static class RegistrationsExtensions
    {

        public static IServiceCollection WithPositioningServices(this IServiceCollection services)
        {
            services.AddSingleton<IGeoPositionProcessor, GeoPositionProcessor>();
            services.AddSingleton<PositionSimulator>();
            services.AddHostedService(provider => provider.GetRequiredService<PositionSimulator>());
            return services;
        }
    }
}
