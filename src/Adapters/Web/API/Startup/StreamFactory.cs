using AutoMais.Core.Application.Adapters.Stream;

namespace AutoMais.Ticket.Api.Startup
{
    public class StreamSettings
    {
        public enum StreamType
        {
            AzureEventHub,
            AzureServiceBus,
            RabbitMQ
        }

        public StreamType Service { get; set; }
    }

    public static class StreamFactory
    {
        public static IServiceCollection RegisterStreams(this IServiceCollection services, IConfiguration configuration)
        {
            //Load and register the Settings in a Singleton
            var settings = configuration.GetSection("StreamSettings").Get<StreamSettings>();
            services.AddSingleton<StreamSettings>(settings);                

            services.AddSingleton<IStream>(serviceProvider =>
            {
                //Get the settings from memory
                var env = serviceProvider.GetRequiredService<StreamSettings>();
                //Return the service based on the settings
                return serviceProvider.GetRequiredKeyedService<IStream>(env.Service.ToString());
            });

            return services;
        }
    }
}
