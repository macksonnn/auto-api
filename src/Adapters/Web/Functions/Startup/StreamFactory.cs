
namespace AutoMais.Integrations.Startup
{
    public class StreamSettings
    {
        public enum StreamType
        {
            AzureEventHub,
            AzureServiceBus
        }

        public Enum Service { get; set; }
    }

    public static class StreamFactory
    {
        public static IServiceCollection RegisterStream(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StreamSettings>((c) => configuration.GetSection("StreamSettings"));

            var settings = services.BuildServiceProvider().GetRequiredService<IOptions<StreamSettings>>().Value;

            //switch (settings.Service)
            //{
            //    case StreamSettings.StreamType.AzureEventHub:
            //        services.RegisterEventHub(configuration);
            //        break;
            //    case StreamSettings.StreamType.AzureServiceBus:
            //        services.RegisterServiceBus(configuration);
            //        break;
            //    default:
            //        throw new ArgumentException("Invalid stream type", nameof(settings.Service));
            //}

            return services;
        }
    }
}
