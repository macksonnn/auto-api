using AutoMais.Core.Common;
using AutoMais.Stream.ServiceBus.Service;
using Azure.Messaging.ServiceBus;
using Core.Common.Extensions;

namespace AutoMais.Stream.ServiceBus.Startup
{
    public class ServiceBusStartup : IStartupRegister
    {
        public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ServiceBusClient>((sp) => {
                var config = sp.GetRequiredService<IConfiguration>();
                var settings = config.GetSection("StreamSettings:AzureServiceBus").Get<ServiceBusSettings>();

                return new ServiceBusClient(settings?.ThrowIfNull()?.ConnectionString);
            });

            services.AddKeyedSingleton<IStream, ServiceBusStreamService>("AzureServiceBus");

            return services;
        }
    }
}
