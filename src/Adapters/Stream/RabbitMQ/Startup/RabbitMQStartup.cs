using Becape.Core.Common.Startup;

namespace Becape.Stream.Rabbit.Startup;

public class RabbitMQStartup : IStartupRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<RabbitMQSettings>((c) => configuration.GetSection("StreamSettings:AzureServiceBus"));

        //services.AddSingleton((sp) =>
        //{
        //    var settings = sp.GetRequiredService<IOptions<RabbitMQSettings>>().Value;
        //    return new ServiceBusClient(settings.ConnectionString);
        //});

        return services;
    }
}
