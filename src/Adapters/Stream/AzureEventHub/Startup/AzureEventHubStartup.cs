using Becape.Stream.ServiceBus.Service;
using Becape.Core.Common.Startup;
using Becape.Core.Common.Stream;

namespace Becape.Stream.AzureEventHub.Startup;

public class AzureEventHubStartup : IStartupRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureEventHubSettings>((c) => configuration.GetSection("StreamSettings:AzureEventHub"));

        services.AddSingleton<AzureEventHubSettings>((sp) => {
            var config = sp.GetRequiredService<IConfiguration>();

            return config.GetSection("StreamSettings:AzureEventHubSettings").Get<AzureEventHubSettings>() ?? new AzureEventHubSettings();
        });

        services.AddKeyedSingleton<IStream, AzureEventHubStream>("AzureEventHub");

        return services;
    }
}
